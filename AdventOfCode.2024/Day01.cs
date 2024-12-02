using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y24;

public class Day01 : Day
{
    private long[] ListA { get; }
    private long[] ListB { get; }

    public Day01(string input)
    {
        var bufferA = new List<long>();
        var bufferB = new List<long>();
        foreach (var line in input.ReplaceLineEndings("\n").TrimEnd().Split("\n"))
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Debug.Assert(parts.Length == 2);
            bufferA.Add(long.Parse(parts[0]));
            bufferB.Add(long.Parse(parts[1]));
        }
        ListA = bufferA.ToArray();
        ListB = bufferB.ToArray();
    }

    public override string Part1()
    {
        var sortedA = ListA.Order().ToList();
        var sortedB = ListB.Order().ToList();

        long totalDistance = 0;
        for (var i = 0; i < sortedA.Count; i++)
        {
            totalDistance += Math.Abs(sortedA[i] - sortedB[i]);
        }

        return totalDistance.ToString();
    }

    public override string Part2()
    {
        var similarityA = ListA.GroupBy(n => n).ToDictionary(g => g.Key, g => g.Count());
        long similarityIndex = 0;
        foreach (var b in ListB)
        {
            similarityIndex += b * similarityA.GetValueOrDefault(b, 0);
        }
        return similarityIndex.ToString();
    }
}
