using UnityEngine;

public class PlayerStateMachine 
{
    [Header("State")]
    public PlayerState currentState {  get; private set; }

    public void Initialize(PlayerState _state)
    {
        currentState = _state;
        currentState.EnterState();
            
    }

    public void ChangeState(PlayerState _newState)
    {
        currentState.ExitState();
        currentState = _newState;   
        currentState.EnterState();
    }
}
