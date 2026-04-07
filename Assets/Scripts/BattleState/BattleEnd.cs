using UnityEngine;

public class BattleEnd : IBattleState
{
    public void Enter()
    {
        Debug.Log("Battle End!");
        BattleManager.Instance.EndBattleClear();
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }
}
