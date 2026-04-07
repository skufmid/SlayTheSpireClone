using UnityEngine;

public class PlayerTurn : IBattleState
{
    public void Enter()
    {
        Debug.Log("Player's Turn!");
        BattleManager.Instance.StartTurnDraw();
    }

    public void Exit()
    {
        BattleManager.Instance.EndTurnDiscard();
    }

    public void Update()
    {

    }
}
