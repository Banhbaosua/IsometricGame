using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseState
{
    public Player _player;
    public StateMachine _stateMachine;

    protected Vector3 _input;
    private InputAction _moveInput;

    public BaseState(Player player,StateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
    }
    // Start is called before the first frame update
    public virtual void Enter()
    {

    }
    public virtual void HandleInput()
    {
        
    }
    public virtual void LogicalUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }
    public virtual void Exit() 
    { 

    }

}
