using System.Collections;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace c__2021;

public class Day06
{

    private static long Simulate(IEnumerable<string> lines, int iterations)
    {
        var states = new long[9];
        foreach (var number in lines.First().Split(',').Select(x => x[0] - '0'))
        {
            states[number]++;
        }

        for (var i = 0; i < iterations; i++)
        {
            var reset = states[0];
            for (var x = 1; x < states.Length; x++)
            {
                states[x - 1] = states[x];
            }

            states[6] += reset;
            states[^1] = reset;
        }

        return states.Sum();
    }

    public void Part1(IEnumerable<string> lines)
    {
        Console.WriteLine(Simulate(lines, 80));
    }

    public void Part2(IEnumerable<string> lines)
    {
        Console.WriteLine(Simulate(lines, 256));
    }
}