namespace c__2021;

public class Day15
{
    record Position(int Risk)
    {
        public bool Visited { get; set; }
    }
    public void Part1(ILineReader lines)
    {
        var grid = Grid<Position>.FromTokens(lines.TakeWhile(x => x != ""), c => new Position(c - '0'));;
        Console.WriteLine(CalculatePath(grid));
    }
    
    public void Part2(IEnumerable<string> lines)
    {
        var list = lines.TakeWhile(x => x != "").Select(line => line.Select(c => c - '0')).ToList();
        var expanded = Enumerable.Range(0, 5)
            .SelectMany(iy => 
                list.Select(line => 
                    Enumerable.Range(0, 5)
                        .SelectMany(ix => 
                            line.Select(value => (value + ix + iy - 1) % 9 + 1))));
        
        var grid = Grid<Position>.FromTokens(expanded, c => new Position(c));
        Console.WriteLine(CalculatePath(grid));
    }

    private static int CalculatePath(Grid<Position> grid)
    {
        var queue = new PriorityQueue<Cell<Position>, int>();

        queue.Enqueue(grid.First(), 0);

        while (queue.TryDequeue(out var cell, out var riskToStart))
        {
            if (cell.X == grid.Width - 1 && cell.Y == grid.Height - 1)
            {
                return riskToStart;
            }

            var neighbours = grid.GetNeighbours(cell).Where(x => !x.Value.Visited);

            foreach (var neighbour in neighbours)
            {
                neighbour.Value.Visited = true;
                queue.Enqueue(neighbour, riskToStart + neighbour.Value.Risk);
            }
        }

        return -1;
    }
}