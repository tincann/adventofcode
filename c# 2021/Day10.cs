namespace c__2021;

public class Day10
{
    private readonly Pair[] _pairs = {
        new('(', ')', 3, 1),
        new('[', ']', 57, 2),
        new('{', '}', 1197, 3),
        new('<', '>', 25137, 4),
    };

    record Pair(char Opening, char Closing, int SyntaxScore, int AutoCompleteScore);
    
    public void Part1(IEnumerable<string> lines)
    {
        var illegalPairs = new List<Pair>();
        foreach (var line in lines)
        {
            if (!ParseLine(line, out var illegalPair))
            {
                illegalPairs.Add(illegalPair);
            }
        }

        Console.WriteLine(illegalPairs.Sum(x => x.SyntaxScore));
    }

    
    private bool ParseLine(string line, out Pair illegalPair)
    {
        var stack = new Stack<char>();

        foreach (var c in line)
        {
            var pair = _pairs.FirstOrDefault(p => p.Closing == c);
            if (pair is { })
            {
                var opening = stack.Peek();
                if (opening != pair.Opening)
                {
                    illegalPair = pair;
                    return false;
                }

                stack.Pop();
            }
            else
            {
                stack.Push(c);
            }
        }

        illegalPair = null!;
        return true;
    }

    public void Part2(IEnumerable<string> lines)
    {
        var scores = new List<long>();
        foreach (var line in lines)
        {
            if (!ParseLine(line, out _))
            {
                continue;
            }
            
            var opened = new List<char>();
            foreach (var c in line)
            {
                var pair = _pairs.First(p => p.Opening == c || p.Closing == c);
                if (pair.Opening == c)
                {
                    opened.Add(c);
                }
                else
                {
                    var last = opened.Last();
                    if (last == pair.Opening)
                    {
                        opened.RemoveAt(opened.Count - 1);
                    }
                }
            }

            
            var missing = opened.Select(x => _pairs.First(p => p.Opening == x)).Reverse();

            var score = 0L;
            foreach (var m in missing)
            {
                score = score * 5 + m.AutoCompleteScore;
            }
            
            scores.Add(score);
        }

        scores = scores.OrderBy(x => x).ToList();
        Console.WriteLine(scores[scores.Count / 2]);
    }
}