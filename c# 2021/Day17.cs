using System.Text.RegularExpressions;

namespace c__2021;

public class Day17
{
    public void Part1(IEnumerable<string> lines)
    {
        var target = Area.Parse(lines.First());

        var sy = 0m;
        var ay = -1;

        var vy = Enumerable.Range(0, Math.Abs(target.YMin)).Last(vy => FindYIntersections(sy, vy, ay, target).Any());
        var max = vy * (vy + 1) / 2;
        Console.WriteLine(max);
    }

    public void Part2(IEnumerable<string> lines)
    {
        var target = Area.Parse(lines.First());

        var sy = 0m;
        var ay = -1;
        
        var vys = Enumerable.Range(target.YMin, Math.Abs(target.YMin * 2))
            .Select(vy => (vy, steps: FindYIntersections(sy, vy, ay, target).ToHashSet()))
            .Where(x => x.steps.Any())
            .ToList();
        var vxs = Enumerable.Range(1, target.XMax)
            .Select(vx => (vx, steps: FindXIntersections(vx, target)))
            .Where(x => x.steps.Any())
            .ToList();
        
        var count = 0;
        foreach (var (vy, ySteps) in vys)
        {
            var maxYStep = ySteps.Max();
            foreach (var (vx, xSteps) in vxs)
            {
                //is there an x and y target intersection at the same step?
                if (!xSteps.TakeWhile(x => x <= maxYStep).Any(xStep => ySteps.Contains(xStep))) continue;
                
                count++;
                Console.WriteLine($"{vx},{vy}");
            }
        }
        
        Console.WriteLine(count);
    }

    private static IEnumerable<int> FindXIntersections(int v, Area target)
    {
        var x = 0;
        for (var step = 0; (v > 0 || x >= target.XMin) && x <= target.XMax; step++)
        {
            if (x >= target.XMin)
            {
                yield return step;
            }
            
            x += v;
            v = Math.Max(0, v - 1);
        }
    }
    
    private static IEnumerable<int> FindYIntersections(decimal s, decimal v, decimal a, Area target)
    {
        var step = GetIntersection(s, v, a, target.YMax);
        if (step < 0)
        {
            yield break;
        }

        var stepRounded = Math.Ceiling(step);
        while (GetPosition(s, v, a, stepRounded) >= target.YMin)
        {
            yield return (int)stepRounded;
            stepRounded++;
        }
    }

    private static decimal GetPosition(decimal s, decimal v, decimal a, decimal t)
    {
        // y = s + vt + 0.5at^2
        var vCorrected = v + 0.5m; //correction for simulation order
        return s + vCorrected * t + 0.5m * a * t * t;
    }

    private static decimal GetIntersection(decimal s, decimal v, decimal a, decimal st)
    {
        // ax^2 + bx + c = 0
        var fa = a / 2m;
        var fb = v + 0.5m; //v + correction;
        var fc = s - st;

        var ac = fb * fb - 4 * fa * fc;
        if (ac <= 0)
        {
            throw new NotSupportedException();
        }

        return (-fb - (decimal)Math.Sqrt((double)ac)) / (2 * fa);
    }


    record Area(int XMin, int XMax, int YMin, int YMax)
    {
        public static Area Parse(string str)
        {
            var match = Regex.Match(str, @"x=(-?\d+)\.\.(-?\d+), y=(-?\d+)\.\.(-?\d+)");
            return new Area(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
        }
    }
}