namespace c__2021;

public interface ILineReader : IEnumerable<string>
{
    IReadOnlyList<string> LinesRead { get; }
}