using System.Collections;

namespace c__2021;

public record Cell<T>(int X, int Y, T Value);

public record Grid<T>(IReadOnlyList<IReadOnlyList<Cell<T>>> Cells) : IEnumerable<Cell<T>>
{
    public int Width { get; } = Cells.First().Count;
    public int Height { get; } = Cells.Count;

    public static Grid<T> FromTokens<TToken>(IEnumerable<IEnumerable<TToken>> tokens, Func<TToken, T> parseFunc)
    {
        var cells = tokens.Select((yChars, y) => yChars.Select((xChar, x) => new Cell<T>(x, y, parseFunc(xChar))).ToList()).ToList();
        return new Grid<T>(cells);
    }
    
    public IEnumerable<Cell<T>> GetNeighboursDiagonal(Cell<T> cell)
    {
        for(var dy = -1; dy <= 1; dy++)
        for (var dx = -1; dx <= 1; dx++)
        {
            if (dx == 0 && dy == 0)
            {
                continue;
            }
            var xx = cell.X + dx;
            var yy = cell.Y + dy;
            if (xx >= 0 && xx < Width && yy >= 0 && yy < Height)
            {
                yield return Cells[yy][xx];
            }
        }
    }
    
    public IEnumerable<Cell<T>> GetNeighbours(Cell<T> cell)
    {
        for(var dy = -1; dy <= 1; dy++)
        for (var dx = -1; dx <= 1; dx++)
        {
            if (dx == 0 && dy == 0 || dx != 0 && dy != 0)
            {
                continue;
            }
            var xx = cell.X + dx;
            var yy = cell.Y + dy;
            if (xx >= 0 && xx < Width && yy >= 0 && yy < Height)
            {
                yield return Cells[yy][xx];
            }
        }
    }

    public IEnumerator<Cell<T>> GetEnumerator()
    {
        return Cells.SelectMany(x => x).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
