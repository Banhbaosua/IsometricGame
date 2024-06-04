using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MonsterLove.StateMachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
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

        characterData.HealthController.OnDeath += fsm.Driver.OnDeath.Invoke;
    }
    void Start()
    {
        
    }
    void Update()
    {
        fsm.Driver.Update.Invoke();
    }

    void Move_Update()
    {
        MoveCharacter();
        SetCharacterAnimateBlend();
        LookAtMouse();
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
        characterController.Move(_direction * _speed * Time.deltaTime);
    }

    private void SetCharacterAnimateBlend()
    {
        animator.SetFloat("X", _direction.x,0.1f,Time.deltaTime);
        animator.SetFloat("Y", _direction.z,0.1f, Time.deltaTime);
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

    public void GetMoveDirection(InputAction.CallbackContext context)
    { 
        _direction = context.ReadValue<Vector3>().normalized;
        
    }

    public enum MovementStates
    {
        Move,
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
