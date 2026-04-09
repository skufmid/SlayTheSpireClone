using System;
using System.Collections.Generic;
using UnityEngine;

public class CardOrbSpriteParser : ISpriteParser<CardOrbSprite>
{
    public bool TryParse(string fileName, Sprite sprite, out CardOrbSprite result)
    {
        result = default;

        List<string> parts = SpriteParseUtility.SplitByCapital(fileName);

        if (parts.Count != 3)
            return false;

        if (parts[0] != "Card" || parts[2] != "Orb")
            return false;

        if (!Enum.TryParse(parts[1], out CardColor color))
            return false;

        result = new CardOrbSprite
        {
            Color = color,
            Sprite = sprite
        };

        return true;
    }
}