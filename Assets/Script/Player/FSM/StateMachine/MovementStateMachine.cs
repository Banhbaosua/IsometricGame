using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateMachine : StateMachine
{
    public MovingState movingState;
    public StandingState standingState;
    private void Awake()
    {
        Player playerLocomotion = GetComponent<Player>();
        standingState = new StandingState(playerLocomotion, this);
        movingState = new MovingState(playerLocomotion, this);

        Initialize(standingState);
    }
}
