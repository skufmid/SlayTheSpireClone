using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int cost; // -1 for X cost, -2 for no cost
    public CardColor cardColor;
    public CardRarity cardRarity;
    public CardType cardType;
    public TargetType targetType;
    [SerializeReference] public List<EffectBase> effectList;
}

public enum CardColor
{
    Colorless,
    Ironclad,
    Silent,
    Defect,
    Watcher
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