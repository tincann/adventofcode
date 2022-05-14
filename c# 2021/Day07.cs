using System.Collections;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace c__2021;

public class Day07
{
    public void Part1(IEnumerable<string> lines)
    {
        var positions = lines.First().SplitBy(',').Select(int.Parse).ToList();
        var minFuel = positions.Min(position => positions.Sum(p => Math.Abs(position - p)));
        Console.WriteLine(minFuel);
    }

    public void Part2(IEnumerable<string> lines)
    {
        var positions = lines.First().SplitBy(',').Select(int.Parse).ToList();
        var min = positions.Min(x => x);
        var max = positions.Max(x => x);
        var range = Enumerable.Range(min, max - min);
        var minFuel = range.Min(candidate => positions.Sum(position =>
        {
            var n = Math.Abs(candidate - position);
            return n * (n + 1) / 2;
        }));
        Console.WriteLine(minFuel);
    }
}