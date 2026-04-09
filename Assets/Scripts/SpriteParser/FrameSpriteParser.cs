using System;
using System.Collections.Generic;
using UnityEngine;

public class FrameSpriteParser : ISpriteParser<FrameSprite>
{
    public bool TryParse(string fileName, Sprite sprite, out FrameSprite result)
    {
        result = default;

        List<string> parts = SpriteParseUtility.SplitByCapital(fileName);

        if (parts.Count != 3)
            return false;

        if (parts[0] != "Frame")
            return false;

        if (!Enum.TryParse(parts[1], out CardType type))
            return false;

        if (!Enum.TryParse(parts[2], out CardRarity rarity))
            return false;

        result = new FrameSprite
        {
            Type = type,
            Rarity = rarity,
            Sprite = sprite
        };

        return true;
    }
}