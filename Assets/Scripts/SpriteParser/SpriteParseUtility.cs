using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class SpriteParseUtility
{
    private const string PREFIX = "StS2_";

    public static List<string> SplitByCapital(string name)
    {
        if (name.StartsWith(PREFIX))
            name = name.Substring(PREFIX.Length);

        return Regex.Matches(name, @"[A-Z][a-z0-9]*")
            .Cast<Match>()
            .Select(m => m.Value)
            .ToList();
    }

    public static List<string> SplitByDash(string name)
    {
        if (name.StartsWith(PREFIX))
            name = name.Substring(PREFIX.Length);

        return new List<string>(name.Split('-'));
    }
}