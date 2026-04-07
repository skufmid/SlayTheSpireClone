using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    BattleStateMachine fsm = new BattleStateMachine();
    BattleDeck battleDeck = new BattleDeck();

    PlayerDeck playerDeck = new PlayerDeck(); // 임시로 생성 , 실제로는 플레이어 데이터에서 가져와야 함
    [SerializeField] CardData card;

    BattleStart battleStart = new BattleStart();
    PlayerTurn playerTurn = new PlayerTurn();
    EnemyTurn enemyTurn = new EnemyTurn();
    BattleEnd battleEnd = new BattleEnd();

    private const int DRAW_PER_TURN = 5;

    void Start()
    {
        // 임시로 카드 데이터를 생성하여 플레이어 덱에 추가, 실제로는 플레이어 데이터에서 가져와야 함
        CardInstance cardInstance = new CardInstance(card, false, CardEnchantment.None);
        playerDeck.Add(cardInstance);
        playerDeck.Add(cardInstance);
        playerDeck.Add(cardInstance);
        playerDeck.Add(cardInstance);
        playerDeck.Add(cardInstance);
        playerDeck.Add(cardInstance);
        playerDeck.Add(cardInstance);

        StartBattle();
    }

    void Update()
    {
        fsm.Update();

        // 아래는 디버깅용 임시로 만들어진 입력 처리, 실제로는 UI 버튼이나 다른 이벤트에 의해 호출되어야 함
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

        if (Input.GetKeyDown(KeyCode.F))
            battleDeck.Logging();

    }

    public void InitializeBattle()
    {
        battleDeck.InitializeFrom(playerDeck);
    }

    public void StartTurnDraw()
    {
        battleDeck.DrawHands(DRAW_PER_TURN);
    }

    public void EndTurnDiscard()
    {
        battleDeck.HandPileToDiscardPile(IsEndTurn: true);
    }

    public void EndBattleClear()
    {
        battleDeck.ClearEveryPile();
    }

    // 아래는 임시로 만들어진 메서드들, 실제로는 적 행동이나 승패 조건 등에 따라 호출되어야 함
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