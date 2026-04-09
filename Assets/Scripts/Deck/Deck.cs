using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Deck
{
    protected readonly List<CardModel> cards = new();

    public IReadOnlyList<CardModel> Cards => cards;
    public int Count => cards.Count;

    public virtual void Add(CardModel card) => cards.Add(card);
    public virtual bool Remove(CardModel card) => cards.Remove(card);
    public virtual void Clear() => cards.Clear();
    public virtual CardModel RemoveAt(int index)
    {
        CardModel card = cards[index];
        cards.RemoveAt(index);
        return card;
    }

    public virtual void Shuffle()
    {
        // Fisher-yates shuffle
        for (int i = Count - 1; i >= 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);

            var temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }

}