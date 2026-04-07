using Unity.VisualScripting;
using UnityEngine;

public class BattleDeck
{
    public Deck DrawPile { get; } = new();
    public Deck HandPile { get; } = new();
    public Deck DiscardPile { get; } = new();
    public Deck ExhaustPile { get; } = new();
    
    private const int MAX_HAND_SIZE = 10;


    public void InitializeFrom(PlayerDeck playerDeck)
    {
        DrawPile.Clear();
        HandPile.Clear();
        DiscardPile.Clear();
        ExhaustPile.Clear();

        foreach (var card in playerDeck.Cards)
        {
            DrawPile.Add(card.CloneForBattle());
        }

        DrawPile.Shuffle();
    }

    public void DiscardPileToDrawPile()
    {
        foreach (var card in DiscardPile.Cards)
        {
            DrawPile.Add(card);
        }
        DiscardPile.Clear();
        DrawPile.Shuffle();
    }

    public void HandPileToDiscardPile(bool IsEndTurn = false)
    {
        for (int i = HandPile.Cards.Count - 1; i >= 0; i--)
        {
            var card = HandPile.Cards[i];

            if (IsEndTurn && true) // Todo: 보존이나 유물 효과 등으로 버려지지 않는 카드인지 확인
            {
                DiscardPile.Add(card);
                HandPile.RemoveAt(i);
            }
        }
    }

    public void MoveOneCard(Deck from, Deck to, CardInstance card)
    {
        if (from.Remove(card))
        {
            to.Add(card);
        }
        else
        {
            Debug.LogError("Card not found in the source deck.");
        }
    }

    public void ClearEveryPile()
    {
        DrawPile.Clear();
        HandPile.Clear();
        DiscardPile.Clear();
        ExhaustPile.Clear();
    }

    public CardInstance DrawOne()
    {
        if (DrawPile.Count == 0)
        {
            DiscardPileToDrawPile();
        }

        if (DrawPile.Count == 0)
            return null;

        CardInstance card = DrawPile.RemoveAt(DrawPile.Count - 1);
        HandPile.Add(card);
        return card;
    }

    public void DrawHands(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (HandPile.Count >= MAX_HAND_SIZE)
                break;
            DrawOne();
        }
    }

    public void Logging()
    {
        Debug.Log($"DrawPile: {DrawPile.Count} cards, HandPile: {HandPile.Count} cards, DiscardPile: {DiscardPile.Count} cards, ExhaustPile: {ExhaustPile.Count} cards");
    }
}