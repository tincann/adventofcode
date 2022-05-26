using System.Collections;

namespace c__2021;

public class ConsoleReader : ILineReader
{
    private readonly List<string> _linesRead = new();
    public IReadOnlyList<string> LinesRead => _linesRead;

    private IEnumerable<string> ReadLines()
    {
        while (true)
        {
            var line = Console.ReadLine() ?? "";
            _linesRead.Add(line);
            yield return line;
        }
    }

    public IEnumerator<string> GetEnumerator()
    {
        return ReadLines().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}