using System.Diagnostics;

namespace c__2021;

public class Day13
{
    
    enum Axis { X, Y };
    record FoldInstruction(Axis Axis, int Coordinate);
    public void Part1(ICollection<string> lines)
    {
        var (points, folds) = Parse(lines);

        var transformedPoints = Fold(folds.Take(1), points);
        
        Console.WriteLine(transformedPoints.Distinct().Count());
    }
    
    public void Part2(ICollection<string> lines)
    {
        var (points, folds) = Parse(lines);

        var transformedPoints = Fold(folds, points);

        Print(transformedPoints);
    }

    private static (List<Point>, IEnumerable<FoldInstruction>) Parse(ICollection<string> lines)
    {
        var points = lines
            .TakeWhile(line => line.Length > 0)
            .Select(line =>
            {
                var parts = line.SplitBy(',');
                return new Point(int.Parse(parts[0]), int.Parse(parts[1]));
            }).ToList();

        var folds = lines.Skip(points.Count + 1).Select(line =>
        {
            var parts = line.SplitBy(' ')[2].Split('=');
            return new FoldInstruction(
                parts[0] switch
                {
                    "x" => Axis.X,
                    "y" => Axis.Y,
                    _ => throw new ArgumentOutOfRangeException()
                }, int.Parse(parts[1]));
        });
        return (points, folds);
    }

    private static IEnumerable<Point> Fold(IEnumerable<FoldInstruction> folds, List<Point> points)
    {
        return folds.Aggregate(points.AsEnumerable(), (pts, fold) => fold.Axis switch
        {
            Axis.X => pts.Select(point =>
                point.X > fold.Coordinate
                    ? point with { X = fold.Coordinate - (point.X - fold.Coordinate) }
                    : point),
            _ => pts.Select(point =>
                point.Y > fold.Coordinate
                    ? point with { Y = fold.Coordinate - (point.Y - fold.Coordinate) }
                    : point)
        });
    }

    private static void Print(IEnumerable<Point> transformedPoints)
    {
        var points = transformedPoints.ToList();
        var width = points.Max(x => x.X) + 1;
        var height = points.Max(x => x.Y) + 1;

        var grid = new bool[width, height];
        foreach (var point in points)
        {
            grid[point.X, point.Y] = true;
        }

        Console.WriteLine();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Console.Write(grid[x, y] ? '#' : '.');
            }
            Console.WriteLine();
        }
    }

 
}