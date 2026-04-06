using System.Collections.Generic;
using UnityEngine;
using MackySoft.SerializeReferenceExtensions;
using System;

[CreateAssetMenu(menuName = "Card/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public UpgradeValue<int> cost; // -1 for X cost, -2 for no cost
    public CardColor cardColor;
    public CardRarity cardRarity;
    public CardType cardType;
    public CardEnchantment cardEnchantment;
    public TargetType targetType;

    [SerializeReference, SubclassSelector]
    public List<IEffect> baseEffectList = new();
    [SerializeReference, SubclassSelector]
    public List<IEffect> upgradeEffectList = new();
    public UpgradeValue<string> textOverride; // If not written, the text will be generated automatically.
}

[System.Serializable]
public class UpgradeValue<T>
{
    public T baseValue;
    public T upgradedValue;

    public T Get(bool isUpgraded)
    {
        return isUpgraded ? upgradedValue : baseValue;
    }
}

public enum CardColor
{
    Colorless,
    Ironclad,
    Regent,
    Necrobinder,
    Defect,
}

public enum CardRarity
{
    Starter, Common, Uncommon, Rare
}

public enum CardType
{
    Attack, Skill, Power, Status, Curse, Quest
}

public enum CardEnchantment
{
    None, Adroit, Clone, Corrupted
}

public enum TargetType
{
    None, Enemy, Player, Any
}