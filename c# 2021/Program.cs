// See https://aka.ms/new-console-template for more information

using c__2021;


var day = new Day11();

var lines = new List<string>();

var cachePath = $"{day.GetType().Name}_input.cache";

if (File.Exists(cachePath))
{
    Console.WriteLine("Use previous input? [y/n]");
    if (Console.ReadLine() == "y")
    {
        lines.AddRange(await File.ReadAllLinesAsync(cachePath));
        Console.WriteLine("Using input:");
        Console.WriteLine(String.Join("\n", lines));
        Console.WriteLine();
    }
}

if (lines.Count == 0)
{
    Console.WriteLine("Please give puzzle input and end with an empty line:");
    while (true)
    {
        var line = Console.ReadLine();
        if (line is "")
        {
            break;
        }

        lines.Add(line);
    }
    await File.WriteAllLinesAsync(cachePath, lines);
}

Console.WriteLine($"Running {day.GetType().Name}");
Console.WriteLine("Part 1:");
day.Part1(lines);

Console.WriteLine("Part 2:");
day.Part2(lines);