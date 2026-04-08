using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BackgroundSprite
{
    public CardType Type;
    public CardColor Color;
    public Sprite Sprite;
}

[Serializable]
public struct FrameSprite
{
    public CardType Type;
    public CardRarity Rarity;
    public Sprite Sprite;
}

[Serializable]
public struct BannerSprite
{
    public CardRarity Rarity;
    public Sprite Sprite;
}

[Serializable]
public struct TypeSprite
{
    public CardRarity Rarity;
    public Sprite Sprite;
}

[Serializable]
public struct CardOrbSprite
{
    public CardColor Color;
    public Sprite Sprite;
}

[CreateAssetMenu(menuName = "Card/Card Visual Database")]
public class CardVisualDatabase : ScriptableObject
{
    [Header("Background")]
    public List<BackgroundSprite> backgroundSprites = new();

    [Header("Frame")]
    public List<FrameSprite> frameSprites = new();

    [Header("Banner")]
    public List<BannerSprite> bannerSprites = new();

    [Header("Type Icon")]
    public List<TypeSprite> typeIconSprites = new();

    [Header("Card Orb")]
    public List<CardOrbSprite> cardOrbSprites = new();

    private Dictionary<(CardType, CardColor), Sprite> _backgroundDict;
    private Dictionary<(CardType, CardRarity), Sprite> _frameDict;
    private Dictionary<CardRarity, Sprite> _bannerDict;
    private Dictionary<CardRarity, Sprite> _typeIconDict;
    private Dictionary<CardColor, Sprite> _cardOrbDict;

    private bool _initialized;

    private void Initialize()
    {
        if (_initialized) return;

        _backgroundDict = new ();
        _frameDict = new ();
        _bannerDict = new ();
        _typeIconDict = new ();
        _cardOrbDict = new ();

        foreach (var entry in backgroundSprites)
        {
            if (!_backgroundDict.ContainsKey((entry.Type, entry.Color)))
                _backgroundDict.Add((entry.Type, entry.Color), entry.Sprite);
        }

        foreach (var entry in frameSprites)
        {
            if (!_frameDict.ContainsKey((entry.Type, entry.Rarity)))
                _frameDict.Add((entry.Type, entry.Rarity), entry.Sprite);
        }

        foreach (var entry in bannerSprites)
        {
            if (!_bannerDict.ContainsKey(entry.Rarity))
                _bannerDict.Add(entry.Rarity, entry.Sprite);
        }

        foreach (var entry in typeIconSprites)
        {
            if (!_typeIconDict.ContainsKey(entry.Rarity))
                _typeIconDict.Add(entry.Rarity, entry.Sprite);
        }

        foreach (var entry in cardOrbSprites)
        {
            if (!_cardOrbDict.ContainsKey(entry.Color))
                _cardOrbDict.Add(entry.Color, entry.Sprite);
        }

        _initialized = true;
    }

    public Sprite GetBackground(CardType type, CardColor color)
    {
        Initialize();
        return _backgroundDict.TryGetValue((type, color), out var sprite) ? sprite : null;
    }

    public Sprite GetFrame(CardType type, CardRarity rarity)
    {
        Initialize();
        return _frameDict.TryGetValue((type, rarity), out var sprite) ? sprite : null;
    }

    public Sprite GetBanner(CardRarity rarity)
    {
        Initialize();
        return _bannerDict.TryGetValue(rarity, out var sprite) ? sprite : null;
    }

    public Sprite GetTypeIcon(CardRarity rarity)
    {
        Initialize();
        return _typeIconDict.TryGetValue(rarity, out var sprite) ? sprite : null;
    }

    public Sprite GetCardOrb(CardColor color)
    {
        Initialize();
        return _cardOrbDict.TryGetValue(color, out var sprite) ? sprite : null;
    }
}