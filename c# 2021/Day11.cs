namespace c__2021;

public class Day11
{
    class Octopus
    {
        public int Energy { get; set; }
    };

    public void Part1(IEnumerable<string> lines)
    {
        var grid = Grid<Octopus>.FromChars(lines.Select(x => x.Select(c => c)), c => new Octopus { Energy = c - '0' });
        
        var totalFlashes = SimulateStep(grid).Take(100).Sum();

        Console.WriteLine(totalFlashes);
    }
    
    public void Part2(IEnumerable<string> lines)
    {
        var grid = Grid<Octopus>.FromChars(lines.Select(x => x.Select(c => c)), c => new Octopus { Energy = c - '0' });
        
        var firstStep = SimulateStep(grid)
            .TakeWhile(flashes => flashes < grid.Height * grid.Width)
            .Count();

        Console.WriteLine(firstStep + 1);
    }

    private static IEnumerable<int> SimulateStep(Grid<Octopus> grid)
    {
        while(true)
        {
            foreach (var cell in grid)
            {
                cell.Value.Energy++;
            }

            var flashed = new HashSet<Cell<Octopus>>();
            var startedFlashing = new Queue<Cell<Octopus>>();

            foreach (var cell in grid.Where(x => x.Value.Energy > 9))
            {
                startedFlashing.Enqueue(cell);
                flashed.Add(cell);
            }

            while (startedFlashing.Count > 0)
            {
                var cell = startedFlashing.Dequeue();
                var neighbours = grid.GetNeighbours(cell);
                foreach (var neighbour in neighbours.Where(x => !flashed.Contains(x)))
                {
                    neighbour.Value.Energy++;

                    if (neighbour.Value.Energy > 9)
                    {
                        startedFlashing.Enqueue(neighbour);
                        flashed.Add(neighbour);
                    }
                }
            }

            foreach (var oc in flashed)
            {
                oc.Value.Energy = 0;
            }

            // Print(grid, step);

            yield return flashed.Count;
        }
    }
}