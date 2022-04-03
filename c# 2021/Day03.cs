namespace c__2021;

public class Day03
{
    public void Part1(IEnumerable<string> lines)
    {
        var width = lines.First().Length;
        
        var gamma = "";
        var epsilon = "";
        for (var x = 0; x < width; x++)
        {
            var (gammaPart, epsilonPart) = CountFrequencies(lines.Select(line => line[x]).ToList());
            gamma += gammaPart;
            epsilon += epsilonPart;
        }

        Console.WriteLine(Convert.ToInt16(gamma, 2) * Convert.ToInt16(epsilon, 2));
        

    }
    
    private (char Max, char Min) CountFrequencies(IEnumerable<char> chars)
    {
        var freqs = chars
            .GroupBy(x => x, (c, group) => new { Char = c, Count = group.Count() })
            .ToList();

        return (freqs.MaxBy(x => x.Count)!.Char, freqs.MinBy(x => x.Count)!.Char);
    }
    
    public void Part2(IEnumerable<string> lines)
    {
        var width = lines.First().Length;

        var sequences = new Dictionary<Rating, List<string>>
        {
            [Rating.Oxygen] = lines.ToList(),
            [Rating.Co2] = lines.ToList(),
        };
        
        for (var x = 0; x < width; x++)
        {
            foreach (var rating in sequences.Keys)
            {
                var sequence = sequences[rating];
                if (sequence.Count <= 1)
                {
                    break;
                }

                sequences[rating] = FilterSequences(rating, x, sequence);
            }
        }
        
        Console.WriteLine(Convert.ToInt16(sequences[Rating.Oxygen].Single(), 2) * Convert.ToInt16(sequences[Rating.Co2].Single(), 2));
    }


    enum Rating
    {
        Oxygen,
        Co2
    }
    private List<string> FilterSequences(Rating rating, int bitPosition, IEnumerable<string> sequences)
    {
        
        var freqs = sequences
            .GroupBy(seq => seq[bitPosition])
            .ToList();

        return rating switch
        {
            Rating.Oxygen => freqs.OrderByDescending(x => x.Key).MaxBy(x => x.Count())!.ToList(),
            Rating.Co2 => freqs.OrderBy(x => x.Key).MinBy(x => x.Count())!.ToList(),
            _ => throw new ArgumentOutOfRangeException(nameof(rating), rating, null)
        };
    }
}