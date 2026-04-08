using System;
using System.Collections.Generic;
using UnityEngine;

public class BannerSpriteParser : ISpriteParser<BannerSprite>
{
    public bool TryParse(string fileName, Sprite sprite, out BannerSprite result)
    {
        result = default;

        List<string> parts = SpriteParseUtility.SplitFileName(fileName);

        if (parts.Count != 2)
            return false;

        if (parts[0] != "Banner")
            return false;

        if (!Enum.TryParse(parts[1], out CardRarity rarity))
            return false;

        result = new BannerSprite
        {
            Rarity = rarity,
            Sprite = sprite
        };

        return true;
    }
}