using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : BaseState
{
    public StandingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
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
        if ( _input != Vector3.zero ) 
        {
            _stateMachine.ChangeState(((MovementStateMachine)_stateMachine).movingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        var velocity = _player.CharController.isGrounded.VelocityCal();
        _player.CharController.Move(velocity * _player.MoveSpeed* Time.deltaTime);
    }
}
