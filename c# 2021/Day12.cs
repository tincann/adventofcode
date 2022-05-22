using System.Collections.Immutable;

namespace c__2021;

public class Day12
{
    record Node(string Name)
    {
        private readonly List<Node> _neighbours = new();
        public IReadOnlyList<Node> Neighbours => _neighbours;

        public bool Small { get; } = Name.Any(Char.IsLower);
        public void AddNeighbour(Node neighbour)
        {
            _neighbours.Add(neighbour);
        }
    }
    
    public void Part1(IEnumerable<string> lines)
    {
        var start = CreateGraph(lines);
        var paths = FindPaths(start, ImmutableList.Create(start), true);
        Console.WriteLine(paths.Count());
    }
    
    
    public void Part2(IEnumerable<string> lines)
    {
        var start = CreateGraph(lines);
        var paths = FindPaths(start, ImmutableList.Create(start));
        
        Console.WriteLine(paths.Count());
    }

    private static IEnumerable<ImmutableList<Node>> FindPaths(Node start, ImmutableList<Node> visitedNodes, bool visitedTwice = false)
    {
        return start.Neighbours.SelectMany(neighbour => neighbour switch
            {
                { Name: "start"} => Enumerable.Empty<ImmutableList<Node>>(),
                { Name: "end"} => new [] { visitedNodes.Add(neighbour) },
                { Small: true } when visitedNodes.Contains(neighbour) =>
                    visitedTwice ? Enumerable.Empty<ImmutableList<Node>>() : FindPaths(neighbour, visitedNodes.Add(neighbour), true),
                _ => FindPaths(neighbour, visitedNodes.Add(neighbour), visitedTwice), 
                
            }
        );
    }
    
    private static Node CreateGraph(IEnumerable<string> lines)
    {
        Dictionary<string, Node> nodes = new();
        foreach (var line in lines)
        {
            var parts = line.SplitBy('-');

            var node1 = GetOrCreateNode(parts[0]);
            var node2 = GetOrCreateNode(parts[1]);

            node1.AddNeighbour(node2);
            node2.AddNeighbour(node1);
        }

        return nodes["start"];

        Node GetOrCreateNode(string name) => nodes.TryGetValue(name, out var node) ? node : nodes[name] = new Node(name);
    }
}