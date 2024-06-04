using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class MovingState : BaseState
{
    public MovingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit() 
    { 
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        _input = _player.MoveInput.ReadValue<Vector3>();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        if( _input == Vector3.zero ) 
        {
            _stateMachine.ChangeState(((MovementStateMachine)_stateMachine).standingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        //HandleRotate();
        HandleMove();
    }

    public void HandleRotate()
    {
        var rota = Quaternion.LookRotation(_input, Vector3.up);
        _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, rota, _player.TurnSpeed * Time.deltaTime);
    }

    public void HandleMove()
    {
        var velocity = _player.CharController.isGrounded.VelocityCal();
        _player.CharController.Move(velocity + _input.normalized * _input.normalized.magnitude * _player.MoveSpeed * Time.deltaTime);
    }
}

