using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player))]
public class StateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    BaseState _currentState;

    private void Update()
    {
        _currentState.HandleInput();
        _currentState.LogicalUpdate();
    }
    private void FixedUpdate()
    {
        _currentState.PhysicsUpdate();
    }
    public void Initialize(BaseState startingState)
    {
        _currentState = startingState;
        _currentState.Enter();
    }

    public void ChangeState(BaseState newState) 
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
