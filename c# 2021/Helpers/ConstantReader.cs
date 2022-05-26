using System.Collections;

namespace c__2021;

public class ConstantReader : ILineReader
{
    private readonly List<string> _lines;
    public IReadOnlyList<string> LinesRead => _lines;
    private int _currentLine = 0;

    public ConstantReader(IEnumerable<string> lines)
    {
        _lines = lines.ToList();
    }
    
    private IEnumerable<string> ReadLines()
    {
        while (_currentLine < _lines.Count)
        {
            yield return _lines[_currentLine++];
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