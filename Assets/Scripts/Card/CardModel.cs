using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardModel
{
    public CardData Data { get; private set; }

    public bool IsUpgraded { get; private set; }
    public CardEnchantment CardEnchantment { get; private set; }

    public int CurrentCost { get; private set; }

    public List<IEffect> AdditionalEffectsBefore { get; private set; } = new();
    public List<IEffect> AdditionalEffectsAfter { get; private set; } = new();

    public CardModel(CardData data, bool isUpgraded = false, CardEnchantment enchantment = CardEnchantment.None)
    {
        Data = data;
        IsUpgraded = isUpgraded;
        CardEnchantment = enchantment;

        ResetRuntimeValues();
    }

    public void ResetRuntimeValues()
    {
        CurrentCost = Data.cost.Get(IsUpgraded);

        AdditionalEffectsBefore.Clear();
        AdditionalEffectsAfter.Clear();
    }

    public CardModel CloneForBattle()
    {
        return new CardModel(Data, IsUpgraded, CardEnchantment);
    }

    public void Upgrade()
    {
        IsUpgraded = true;
        CurrentCost = Data.cost.Get(true);
    }

    public void SetCost(int cost)
    {
        CurrentCost = cost;
    }

    public string CardName => Data.cardName;
    public CardColor CardColor => Data.cardColor;
    public CardRarity CardRarity => Data.cardRarity;
    public CardType CardType => Data.cardType;
    public Sprite CardImage => Data.cardImage;
    public TargetType TargetType => Data.targetType.Get(IsUpgraded);
    public int TargetCount => Data.targetCount.Get(IsUpgraded);
    public string TextOverride => Data.textOverride.Get(IsUpgraded);

    public IReadOnlyList<IEffect> GetBaseEffects()
    {
        return IsUpgraded ? Data.upgradeEffectList : Data.baseEffectList;
    }

    public List<IEffect> GetAllEffects()
    {
        List<IEffect> result = new();

        if (AdditionalEffectsBefore.Count > 0)
            result.AddRange(AdditionalEffectsBefore);

        var baseEffects = GetBaseEffects();
        if (baseEffects != null)
            result.AddRange(baseEffects);

        if (AdditionalEffectsAfter.Count > 0)
            result.AddRange(AdditionalEffectsAfter);

        return result;
    }
}