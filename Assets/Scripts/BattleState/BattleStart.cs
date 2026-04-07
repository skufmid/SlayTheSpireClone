using UnityEngine;

public class BattleStart : IBattleState
{
    public void Enter()
    {
        Debug.Log("Battle Start!");
        BattleManager.Instance.InitializeBattle();
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }
}
