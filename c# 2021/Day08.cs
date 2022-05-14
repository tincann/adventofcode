using System.Collections;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace c__2021;

public class Day08
{
    public void Part1(IEnumerable<string> lines)
    {
        Console.WriteLine(lines.SelectMany(x => x.SplitBy('|')[1].SplitBy(' '))
            .Count(digit => digit.Length is 2 or 3 or 4 or 7));
    }


    record Display(string[] Patterns, string[] Output)
    {
        public static Display Parse(string s)
        {
            var parts = s.SplitBy('|');
            return new(parts[0].SplitBy(' '), parts[1].SplitBy(' '));
        }
    }

    /// <summary>
    /// Number of segments per digit:
    /// 0 = 6
    /// 1 = 2
    /// 2 = 5
    /// 3 = 5
    /// 4 = 4
    /// 5 = 5
    /// 6 = 6
    /// 7 = 3
    /// 8 = 7
    /// 9 = 6
    ///
    /// By count:
    /// 2: [1], 3: [7], 4: [4], 7: [8]
    /// 5: [2,3,5]
    /// 6: [0,6,9]
    ///
    /// this one was difficult... there is probably a more generic solution
    /// </summary>
    public void Part2(IEnumerable<string> lines)
    {
        bool IsSubset(string main, string subset)
        {
            return subset.All(main.Contains);
        }
        
        
        var total = 0;
        foreach (var display in lines.Select(Display.Parse))
        {
            var candidates = display.Patterns.ToList();

            string PopCandidate(Func<string, bool> pred)
            {
                var candidate = candidates.Single(pred);
                candidates.Remove(candidate);
                return new string(candidate.OrderBy(x => x).ToArray());
            }

            var translated = new string[10];
            
            translated[1] = PopCandidate(x => x.Length == 2);
            translated[7] = PopCandidate(x => x.Length == 3);
            translated[4] = PopCandidate(x => x.Length == 4);
            translated[8] = PopCandidate(x => x.Length == 7);

            translated[3] = PopCandidate(p => p.Length == 5 && IsSubset(p, translated[7]));

            var fourMinusOne = new string(translated[4].Except(translated[1]).ToArray());
            translated[5] = PopCandidate(p => p.Length == 5 && IsSubset(p, fourMinusOne));
            translated[2] = PopCandidate(p => p.Length == 5 && p != translated[3] && p != translated[5]);
            translated[9] = PopCandidate(p => p.Length == 6 && IsSubset(p, translated[4]));
            translated[6] = PopCandidate(p => p.Length == 6 && IsSubset(p, translated[5]));
            translated[0] = PopCandidate(_ => true);

            var digits = display.Output.Select(output => Array.IndexOf(translated, new string(output.OrderBy(x => x).ToArray()))).ToList();
            var number = int.Parse(string.Join("", digits));

            total += number;
        }
        
        Console.WriteLine(total);
    }
}