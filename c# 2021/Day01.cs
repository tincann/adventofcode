namespace c__2021;

public class Day01
{
    public void Part1(IEnumerable<string> lines)
    {
        int? previousValue = null;
        var increasedCount = 0;
        foreach (var currentValue in lines.Select(int.Parse))
        {
            if (currentValue > previousValue)
            {
                increasedCount++;
            }

            previousValue = currentValue;
        }

        Console.WriteLine(increasedCount);
    }

    public void Part2(IEnumerable<string> lines)
    {
        var values = lines.Select(int.Parse).ToList();
        var increasedCount = 0;
        for (var index = 3; index < values.Count; index++)
        {
            var a = values[index - 3] + values[index - 2] + values[index - 1];
            var b = values[index - 2] + values[index - 1] + values[index];

            if (b > a)
            {
                increasedCount++;
            }
        }

        Console.WriteLine(increasedCount);
    }
}