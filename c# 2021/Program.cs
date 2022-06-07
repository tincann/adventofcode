// See https://aka.ms/new-console-template for more information

using c__2021;


var day = new Day18();

var cacheLines = new List<string>();

var cachePath = $"{day.GetType().Name}_input.cache";
if (File.Exists(cachePath))
{
    Console.WriteLine("Use previous input? [y/n]");
    if (Console.ReadLine() == "y")
    {
        cacheLines.AddRange(await File.ReadAllLinesAsync(cachePath));
        Console.WriteLine("Using input:");
        Console.WriteLine(String.Join("\n", cacheLines));
        Console.WriteLine();
    }
}

ILineReader lineReader = cacheLines.Any() ? new ConstantReader(cacheLines) : new ConsoleReader();
try
{
    Console.WriteLine($"Running {day.GetType().Name}");
    
    Console.WriteLine("Please give puzzle input and end with an empty line:");
    Console.WriteLine();
    
    Console.WriteLine("Part 1:");
    day.Part1(lineReader);

    Console.WriteLine("Part 2:");
    day.Part2( new ConstantReader(lineReader.LinesRead));

}
finally
{
    if (!cacheLines.Any())
    {
        await File.WriteAllLinesAsync(cachePath, lineReader.LinesRead);
    }
}