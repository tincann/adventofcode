// See https://aka.ms/new-console-template for more information

using c__2021;


var lines = new List<string>();
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

var day = new Day10();

Console.WriteLine($"Running {day.GetType().Name}");
Console.WriteLine("Part 1:");
day.Part1(lines);

Console.WriteLine("Part 2:");
day.Part2(lines);