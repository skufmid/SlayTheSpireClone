using UnityEngine;

public interface IBattleState
{
    void Enter();
    void Exit();
    void Update();
}

public class BattleStateMachine
{
    public IBattleState CurrentState { get; private set; }

    public void ChangeState(IBattleState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}