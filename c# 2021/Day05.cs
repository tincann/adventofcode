using System.Collections;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace c__2021;

public class Day05
{
    record Point(int X, int Y)
    {
        public static Point Parse(string input)
        {
            var coords = input.SplitBy(',');
            return new(int.Parse(coords[0]), int.Parse(coords[1]));
        }
    }

    record Line(Point P1, Point P2)
    {
        public static Line Parse(string input)
        {
            var points = input.SplitBy("->");
            return new (Point.Parse(points[0]), Point.Parse(points[1]));
        }
    }
    
    private static IEnumerable<Point> GetPoints(Line line)
    {
        var (p1, p2) = line;

        var dx = Math.Sign(p2.X - p1.X);
        var dy = Math.Sign(p2.Y - p1.Y);

        var x = p1.X;
        var y = p1.Y;

        while (true)
        {
            yield return new Point(x, y);

            if (x == p2.X && y == p2.Y)
            {
                yield break;
            }

            if (x != p2.X)
            {
                x += dx;
            }

            if (y != p2.Y)
            {
                y += dy;
            }
        }
    }

    public void Part1(IEnumerable<string> lines)
    {
        var lineSegments = lines.Select(Line.Parse)
            .Where(line => line.P1.X == line.P2.X || line.P1.Y == line.P2.Y)
            .ToList();

        var lookup = lineSegments.SelectMany(GetPoints).ToLookup(p => (p.X, p.Y));
        var intersections = lookup.Count(group => group.Count() >= 2);

        Console.WriteLine(intersections);
    }

    public void Part2(IEnumerable<string> lines)
    {
        var lineSegments = lines.Select(Line.Parse).ToList();

        var lookup = lineSegments.SelectMany(GetPoints).ToLookup(p => (p.X, p.Y));
        var intersections = lookup.Count(group => group.Count() >= 2);

        Console.WriteLine(intersections);
    }
}