using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class SpriteParseUtility
{
    private const string PREFIX = "StS2_";

    public static List<string> SplitFileName(string fileName)
    {
        if (fileName.StartsWith(PREFIX))
            fileName = fileName.Substring(PREFIX.Length);

        return Regex.Matches(fileName, @"[A-Z][a-z0-9]*")
            .Cast<Match>()
            .Select(m => m.Value)
            .ToList();
    }
}