using AdventOfCode.Utility;
using AdventOfCode.Y24;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y24;

public class Day02 : Day
{
    private int[][] Reports { get; }
    public Day02(string input)
    {
        Reports = input
            .TrimEnd()
            .ReplaceLineEndings("\n")
            .Split('\n')
            .Select(line => line.Split(' ').Select(int.Parse).ToArray())
            .ToArray();
    }

    public override string Part1()
    {
        var safeReports = 0;
        foreach (var report in Reports)
        {
            if (IsReportSafe(report))
            {
                safeReports++;
            }
        }

        return safeReports.ToString();
    }

    public override string Part2()
    {
        var safeReports = 0;
        foreach (var report in Reports)
        {
            if (IsReportSafe(report))
            {
                safeReports++;
                continue;
            }
            for (int i = 0; i < report.Length; i++)
            {
                // iterate through report, dropping one item at a time
                // to add some "fault tolerance"
                var trimmedReport = report.Where((item, index) => index != i).ToArray();
                if (IsReportSafe(trimmedReport))
                {
                    safeReports++;
                    break;
                }
            }
        }

        return safeReports.ToString();
    }

    private bool IsReportSafe(int[] report)
    {
        var direction = 0;
        var isReportScaled = true;
        for (var i = 0; i < report.Length - 1; i++)
        {
            var change = report[i].CompareTo(report[i + 1]);
            if (direction == 0)
            {
                direction = change;
            }
            else
            {
                if (direction != change)
                {
                    isReportScaled = false;
                    break;
                }
            }
        }
        
        if (!isReportScaled)
        {
            return false;
        }

        var isReportSmooth = true;
        for (var i = 0; i < report.Length - 1; i++)
        {
            var change = Math.Abs(report[i] - report[i + 1]);
            if (!change.IsBetween(1, 3))
            {
                isReportSmooth = false;
                break;
            }
        }

        return isReportSmooth;
    }
}
