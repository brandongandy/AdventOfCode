using System.Diagnostics;

namespace AdventOfCode.Y24;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Initializing AOC.");

        var day = args.ElementAtOrDefault(0);
        var fileInput = args.ElementAtOrDefault(1);

        if (day is null)
        {
            Console.WriteLine("Cannot run AOC: No Day provided.");
            return;
        }

        if (!int.TryParse(day, out var dayValue) ||
            dayValue < 1 ||
            dayValue > 25)
        {
            Console.WriteLine($"Cannot run AOC: Day '{dayValue}' must be between 1 and 25 inclusive.");
            return;
        }

        if (fileInput is null)
        {
            // if no explicit input is provided, we will assume that the file
            // lives in the Assets folder for ease of use
            fileInput = $"Assets/day{dayValue:00}.txt";
        }

        if (File.Exists(fileInput))
        {
            fileInput = File.ReadAllText(fileInput);
        }

        if (string.IsNullOrEmpty(fileInput))
        {
            Console.WriteLine("Cannot run AOC: No valid input found.");
            return;
        }

        var className = $"AdventOfCode.Y24.Day{dayValue:00}";
        Console.WriteLine($"Initializing class {className}");

        var startTime = Stopwatch.GetTimestamp();
        var dayClass = typeof(Program).Assembly.GetType(className);
        if (dayClass is null)
        {
            Console.WriteLine($"Cannot run AOC: Unable to find class {className}.");
            return;
        }

        var ctor = dayClass.GetConstructor([typeof(string)]);
        var inst = ctor!.Invoke([fileInput]) as Day;
        if (inst is null)
        {
            Console.WriteLine($"Cannot run AOC: No valid constructor found for {className}");
            return;
        }

        Console.WriteLine($"Class initialized. Time: {Stopwatch.GetElapsedTime(startTime)}");

        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
        startTime = Stopwatch.GetTimestamp();
        var partOneAnswer = inst.Part1();
        Console.WriteLine($"Answer: {partOneAnswer}");
        Console.WriteLine($"Time: {Stopwatch.GetElapsedTime(startTime)}");

        Console.BackgroundColor = ConsoleColor.DarkGreen;
        startTime = Stopwatch.GetTimestamp();
        var partTwoAnswer = inst.Part2();
        Console.WriteLine($"Answer: {partTwoAnswer}");
        Console.WriteLine($"Time: {Stopwatch.GetElapsedTime(startTime)}");

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ReadLine();
    }
}
