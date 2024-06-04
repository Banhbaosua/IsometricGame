using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class AnimationHandler : MonoBehaviour
{
    PlayerInput _inputManager;
    InputAction _action;
    Vector3 _input;
    Animator _animController;

    private void Awake()
    {
        _inputManager = GetComponent<PlayerInput>();
        _action = _inputManager.actions["Move"];
    }
    public void SetAnimator(Animator animator)
    {
        if(animator != null)
            _animController = animator;
    }
    public void SetFloatAnim(float animateSmoothDamp)
    {
        _input = _action.ReadValue<Vector3>();
        if(_animController != null) 
        {
            _animController.SetFloat("InputX", _input.x, animateSmoothDamp, Time.deltaTime);
            _animController.SetFloat("InputZ", _input.z, animateSmoothDamp, Time.deltaTime);
        }
    }

    public void HandleAnimation(float animateSmoothDamp)
    {
        SetFloatAnim(animateSmoothDamp);
    }
}
