using System.Collections;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace c__2021;

public class Day09
{
    public void Part1(IEnumerable<string> lines)
    {
        var rows = lines.Select(line => line.Select(c => c - '0').ToList()).ToList();
        var width = rows.First().Count;
        var height = rows.Count;

        var minima = new List<int>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var above = y > 0 ? rows[y - 1][x] : 10;
                var below = y < height - 1 ? rows[y + 1][x] : 10;
                var right = x < width - 1 ? rows[y][x + 1] : 10;
                var left = x > 0 ? rows[y][x - 1] : 10;
                
                var num = rows[y][x];

                if (num < above && num < below && num < right && num < left)
                {
                    Console.WriteLine($"Minimum: {num}");
                    minima.Add(num);
                }
            }
        }

        Console.WriteLine(minima.Sum(x => x + 1));
    }

    public void Part2(IEnumerable<string> lines)
    {
        var rows = lines.Select((line, y) => line.Select((c, x) => new Cell(x, y, c - '0')).ToList()).ToList();

        var visited = new HashSet<Cell>();
        
        var width = rows.First().Count;
        var height = rows.Count;

        var basinSizes = new List<int>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var startCell = rows[y][x];
                if (visited.Contains(startCell) || startCell.Height == 9)
                {
                    continue;
                }
                var basin = Flood(rows, startCell);
                basinSizes.Add(basin.Count);
                foreach (var c in basin)
                {
                    visited.Add(c);
                }
            }
        }

        var top3 = basinSizes.OrderByDescending(x => x).Take(3).ToList();
        
        Console.WriteLine(top3.Aggregate((a, b) => a * b));
    }

    private static List<Cell> Flood(List<List<Cell>> rows, Cell startCell)
    {
        var visited = new HashSet<Cell>();
        
        var queue = new Queue<Cell>();
        queue.Enqueue(startCell);
        visited.Add(startCell);

        while (queue.Count > 0)
        {
            var cell = queue.Dequeue();
            var validNeighbours = GetNeighbours(cell).Where(x => x.Height != 9 && !visited.Contains(x));

            foreach (var neighbour in validNeighbours)
            {
                queue.Enqueue(neighbour);
                visited.Add(neighbour);
            }
        }

        return visited.ToList();


        IEnumerable<Cell> GetNeighbours(Cell cell)
        {
            if(cell.Y > 0)
            {
                yield return rows[cell.Y - 1][cell.X];
            }

            if (cell.Y < rows.Count - 1)
            {
                yield return rows[cell.Y + 1][cell.X];
            }

            if (cell.X < rows.First().Count - 1)
            {
                yield return rows[cell.Y][cell.X + 1];
            }

            if (cell.X > 0)
            {
                yield return rows[cell.Y][cell.X - 1];
            }
        }
    }

    record Cell(int X, int Y, int Height);
}