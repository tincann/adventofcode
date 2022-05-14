using System.Collections;
using System.Collections.Immutable;

namespace c__2021;

public class Day04
{
    public record Cell(int Value)
    {
        public bool Hit { get; set; }
    }
    
    public void Part1(IEnumerable<string> lines)
    {
        var sections = GetSections(lines);
        sections.MoveNext();
        var numbers = sections.Current.First().Split(',').Select(int.Parse).ToList();
        
        List<List<Cell>>? bestBoard = null;
        var lowestIndex = int.MaxValue;
        while (sections.MoveNext())
        {
            var rows = sections.Current.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(w => new Cell(int.Parse(w))).ToList()).ToList();
            var columns = Enumerable.Range(0, rows.First().Count).Select(columnIndex => rows.Select(row => row[columnIndex]).ToList()).ToList();
            
            var lookup = rows.SelectMany(x => x).ToLookup(x => x.Value);

            for (var currentNumber = 0; currentNumber < numbers.Count && currentNumber < lowestIndex; currentNumber++)
            {
                var number = numbers[currentNumber];
                foreach (var hit in lookup[number])
                {
                    hit.Hit = true;
                }

                if (rows.Any(row => row.All(c => c.Hit)) || columns.Any(column => column.All(c => c.Hit)))
                {
                    bestBoard = rows;
                    lowestIndex = currentNumber;
                    break;
                }
            }
        }

        var answer = bestBoard.SelectMany(x => x).Where(x => !x.Hit).Sum(x => x.Value) * numbers[lowestIndex];
        Console.WriteLine(answer);
    }


    public void Part2(IEnumerable<string> lines)
    {
        var sections = GetSections(lines);
        sections.MoveNext();
        var numbers = sections.Current.First().Split(',').Select(int.Parse).ToList();
        
        List<List<Cell>>? bestBoard = null;
        var lowestIndex = int.MinValue;
        while (sections.MoveNext())
        {
            var rows = sections.Current.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(w => new Cell(int.Parse(w))).ToList()).ToList();
            var columns = Enumerable.Range(0, rows.First().Count).Select(columnIndex => rows.Select(row => row[columnIndex]).ToList()).ToList();
            
            var lookup = rows.SelectMany(x => x).ToLookup(x => x.Value);

            for (var currentNumber = 0; currentNumber < numbers.Count; currentNumber++)
            {
                var number = numbers[currentNumber];
                foreach (var hit in lookup[number])
                {
                    hit.Hit = true;
                }

                if (rows.Any(row => row.All(c => c.Hit)) || columns.Any(column => column.All(c => c.Hit)))
                {
                    if (currentNumber > lowestIndex)
                    {
                        bestBoard = rows;
                        lowestIndex = currentNumber;
                    }

                    break;
                }
            }
        }
        var answer = bestBoard.SelectMany(x => x).Where(x => !x.Hit).Sum(x => x.Value) * numbers[lowestIndex];
        Console.WriteLine(answer);
    }
    

    private static IEnumerator<IReadOnlyList<string>> GetSections(IEnumerable<string> lines)
    {
        var acc = new List<string>();
        using var enumerator = lines.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (String.IsNullOrEmpty(enumerator.Current) && acc.Count > 0)
            {
                yield return acc;
                acc = new List<string>();
            }
            else
            {
                acc.Add(enumerator.Current);
            }
        }

        if (acc.Count > 0)
        {
            yield return acc;
        }
    }
}