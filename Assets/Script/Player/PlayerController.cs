using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MonsterLove.StateMachine;

[RequireComponent(typeof(HealthController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] List<Transform> weapons;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Animator animator;
    [SerializeField] float _speed;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] CharacterData characterData;

    private Vector3 _direction;
    private float _velocity;
    private float _gravity = -Physics.gravity.magnitude;

    StateMachine<MovementStates,GameCharacter> fsm;
    private void Awake()
    {
        fsm = new StateMachine<MovementStates,GameCharacter>(this);
        fsm.ChangeState(MovementStates.Move);

        characterData.InjectHealthCtl(GetComponent<HealthController>());
        characterData.HealthController.OnDeath += fsm.Driver.OnDeath.Invoke;
    }
    void Update()
    {
        fsm.Driver.Update.Invoke();
    }

    public void SetWeapon(int tier)
    {
        for(int i = 0; i< weapons.Count; i++) 
        {
            if (tier == i +1)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
                weapons[i].gameObject.SetActive(false);
        }
    }

    void Move_Update()
    {
        if (Time.timeScale > 0)
        {
            MoveCharacter();
            SetCharacterAnimateBlend();
            LookAtMouse();
        }
    }

    void Move_Enter()
    {
        Debug.Log("enter");
    }

    void Move_OnDeath(bool value)
    {
        if (value)
        {
            fsm.ChangeState(MovementStates.Death);
        }
    }

    void Death_Enter()
    {
        characterData.HealthController.OnDeath -= fsm.Driver.OnDeath.Invoke;
    }

    private void MoveCharacter()
    {
        _velocity += _gravity * Time.deltaTime;
        _direction.y = _velocity;
        characterController.Move(_speed * Time.deltaTime * _direction);
    }

    private void SetCharacterAnimateBlend()
    {
        animator.SetFloat("SidewaysMovement", _direction.x,0.1f,Time.deltaTime);
        animator.SetFloat("ForwardMovement", _direction.z,0.1f, Time.deltaTime);
    }

    private void LookAtMouse()
    {
        var mousePos = GetMousePos();
        transform.forward = mousePos - transform.localPosition;
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    private Vector3 GetMousePos()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var hitPoint,Mathf.Infinity,groundMask);
        
        return hitPoint.point;
    }
    //Player Input player event
    public void GetMoveDirection(InputAction.CallbackContext context)
    { 
        _direction = context.ReadValue<Vector3>().normalized;
        
    }

    public enum MovementStates
    {
        Move,
        Pause,
        Dash,
        Death,
    }

    public class GameCharacter
    {
        public StateEvent Update;
        public StateEvent<bool> OnDeath;
        public StateEvent<Collision> OnCollision;
    }
}
