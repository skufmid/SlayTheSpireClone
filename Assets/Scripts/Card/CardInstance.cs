using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class CardInstance
{
    public CardData Data;

    public int cost; // -1 for X cost, -2 for no cost
    public CardColor cardColor => Data.cardColor;
    public CardRarity cardRarity => Data.cardRarity;
    public CardType cardType => Data.cardType;
    public TargetType targetType => IsUpgraded ? Data.targetType.upgradedValue : Data.targetType.baseValue;

    public List<IEffect> AdditionalEffectsBefore;
    public List<IEffect> AdditionalEffectsAfter;
    public bool IsUpgraded;
    public CardEnchantment cardEnchantment;

    public CardInstance(CardData data, bool isUpgraded, CardEnchantment _cardEnchantment)
    {
        Data = data;
        IsUpgraded = isUpgraded;
        cardEnchantment = _cardEnchantment;

        cost = GetBaseCost();
    }

    public CardInstance CloneForBattle()
    {
        return new CardInstance(Data, IsUpgraded, cardEnchantment);
    }

    private int GetBaseCost()
    {
        return IsUpgraded ? Data.cost.upgradedValue : Data.cost.baseValue;
    }
}