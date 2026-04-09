using System;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpriteParser : ISpriteParser<BackgroundSprite>
{
    public bool TryParse(string fileName, Sprite sprite, out BackgroundSprite result)
    {
        result = default;

        List<string> parts = SpriteParseUtility.SplitByCapital(fileName);

        if (parts.Count != 3)
            return false;

        if (parts[0] != "Bg")
            return false;

        if (!Enum.TryParse(parts[1], out CardType type))
            return false;

        if (!Enum.TryParse(parts[2], out CardColor color))
            return false;

        result = new BackgroundSprite
        {
            Type = type,
            Color = color,
            Sprite = sprite
        };

        return true;
    }
}