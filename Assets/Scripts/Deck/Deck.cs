using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Deck
{
    protected readonly List<CardInstance> cards = new();

    public IReadOnlyList<CardInstance> Cards => cards;
    public int Count => cards.Count;

    public virtual void Add(CardInstance card) => cards.Add(card);
    public virtual bool Remove(CardInstance card) => cards.Remove(card);
    public virtual void Clear() => cards.Clear();
    public virtual CardInstance RemoveAt(int index)
    {
        CardInstance card = cards[index];
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