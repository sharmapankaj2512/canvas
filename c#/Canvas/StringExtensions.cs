using System;
using System.Collections.Generic;
using System.Linq;

namespace Canvas;

public static class StringExtensions
{
    public static List<string> Unlines(string text)
    {
        return text.Split(Environment.NewLine)
            .Select(line => line.Trim())
            .ToList();
    }
}