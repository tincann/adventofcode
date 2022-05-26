using System.Diagnostics;

namespace c__2021;

public class Day14
{
    record InsertionRule(IReadOnlyList<char> Pattern, char Insertion);
    public void Part1(ILineReader lines)
    {
        var (template, rules) = Parse(lines);
        
        var frequencies = CalculateFrequencies(template, 10, rules);

        var max = frequencies.Values.Max();
        var min = frequencies.Values.Min();

        Console.WriteLine(max - min);
    }

    public void Part2(ILineReader lines)
    {
        var (template, rules) = Parse(lines);
        
        var frequencies = CalculateFrequencies(template, 40, rules);

        var max = frequencies.Values.Max();
        var min = frequencies.Values.Min();

        Console.WriteLine(max - min);
    }


    private static Dictionary<char, long> CalculateFrequencies(string template, int steps, Dictionary<(char, char), InsertionRule> rules)
    {
        var frequencies = template
            .GroupBy(x => x, (c, group) => new { c, Count = @group.Count() })
            .ToDictionary(x => x.c, x => (long)x.Count);

        var activePairs = new Dictionary<(char, char), long>();
        for (var i = 1; i < template.Length; i++)
        {
            var pair = (template[i - 1], template[i]);
            activePairs.AddOrUpdate(pair, x => x + 1, 1);
        }

        for (var step = 0; step < steps; step++)
        {
            Console.WriteLine($"Step {step + 1}");
            var newActivePairs = activePairs.ToDictionary(x => x.Key, x => x.Value);
            foreach (var (activePair, pairCount) in activePairs)
            {
                if (!rules.TryGetValue(activePair, out var rule) || pairCount == 0) continue;

                newActivePairs.AddOrUpdate(activePair, x => x - pairCount,  0);
                newActivePairs.AddOrUpdate((activePair.Item1, rule.Insertion), x => x + pairCount, pairCount);
                newActivePairs.AddOrUpdate((rule.Insertion, activePair.Item2), x => x + pairCount, pairCount);

                frequencies.AddOrUpdate(rule.Insertion, x => x + pairCount, pairCount);
            }

            activePairs = newActivePairs;
        }

        return frequencies;
    }

    private static (string template, Dictionary<(char, char), InsertionRule> rules) Parse(ILineReader lines)
    {
        return (lines.First(), lines.Skip(1)
            .TakeWhile(x => x != "")
            .Select(line =>
            {
                var parts = line.SplitBy(" -> ");
                return new InsertionRule(parts[0].ToList(), parts[1][0]);
            })
            .ToDictionary(x => (x.Pattern[0], x.Pattern[1])));
    }
}