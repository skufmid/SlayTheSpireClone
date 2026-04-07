using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    BattleStateMachine fsm = new BattleStateMachine();

    BattleStart battleStart = new BattleStart();
    PlayerTurn playerTurn = new PlayerTurn();
    EnemyTurn enemyTurn = new EnemyTurn();
    BattleEnd battleEnd = new BattleEnd();

    void Start()
    {
        StartBattle();
    }

    void Update()
    {
        fsm.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fsm.CurrentState == playerTurn)
            {
                EnemyTurn();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (fsm.CurrentState == playerTurn)
            {
                EndBattle();
            }
        }
    }

    public void StartBattle()
    {
        fsm.ChangeState(battleStart);
        Invoke(nameof(PlayerTurn), 2f); // Simulate a delay before the player's turn starts
    }

    public void PlayerTurn()
    {
        fsm.ChangeState(playerTurn);
    }

    public void EnemyTurn()
    {
        fsm.ChangeState(enemyTurn);
        Invoke(nameof(PlayerTurn), 2f); // Simulate a delay before the battle ends
    }

    public void EndBattle()
    {
        fsm.ChangeState(battleEnd);
    }
}