using System;
using System.Collections.Generic;
using UnityEngine;

public class TypeSpriteParser : ISpriteParser<TypeSprite>
{
    public bool TryParse(string fileName, Sprite sprite, out TypeSprite result)
    {
        result = default;

        List<string> parts = SpriteParseUtility.SplitByCapital(fileName);

        if (parts.Count != 2)
            return false;

        if (parts[0] != "Type")
            return false;

        if (!Enum.TryParse(parts[1], out CardRarity rarity))
            return false;

        result = new TypeSprite
        {
            Rarity = rarity,
            Sprite = sprite
        };

        return true;
    }
}