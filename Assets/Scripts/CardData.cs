using System.Collections.Generic;
using UnityEngine;
using MackySoft.SerializeReferenceExtensions;

[CreateAssetMenu(menuName = "Card/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int cost; // -1 for X cost, -2 for no cost
    public CardColor cardColor;
    public CardRarity cardRarity;
    public CardType cardType;
    public TargetType targetType;

    [SerializeReference, SubclassSelector]
    public List<IEffect> effectList = new();
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

public enum TargetType
{
    None, Enemy, Player, Any
}