using Unity.VisualScripting;
using UnityEngine;

public class BattleDeck
{
    public Deck DrawPile { get; } = new();
    public Deck Hand { get; } = new();
    public Deck DiscardPile { get; } = new();
    public Deck ExhaustPile { get; } = new();
    
    private const int MAX_HAND_SIZE = 10;
    private const int DRAW_PER_TURN = 5;

    public void InitializeFrom(PlayerDeck playerDeck)
    {
        DrawPile.Clear();
        Hand.Clear();
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

    public CardInstance DrawOne()
    {
        if (DrawPile.Count == 0)
        {
            DiscardPileToDrawPile();
        }

        if (DrawPile.Count == 0)
            return null;

        CardInstance card = DrawPile.RemoveAt(DrawPile.Count - 1);
        Hand.Add(card);
        return card;
    }
}