using System.Diagnostics;
using System.Text;

namespace c__2021;

public class Day18
{
    abstract record Node
    {
        public abstract void Accept(IVisitor visitor, int depth);
        
        public record Number(int Value) : Node
        {
            public override void Accept(IVisitor visitor, int depth) => visitor.VisitNumber(this, depth);
        }

        public record Pair(Node Left, Node Right) : Node
        {
            public override void Accept(IVisitor visitor, int depth) => visitor.VisitPair(this, depth);
        }
    }

    public void Part1(IEnumerable<string> lines)
    {
        var numbers = lines.TakeWhile(x => x != "").Select(x => Parse(x)).ToList();
        var answer = numbers.Aggregate(Sum);
        var left = LeftMostNumber(answer);
        var right = RightMostNumber(answer);
        Printer.Print(answer);
        Console.WriteLine(left.Value * 3 + right.Value * 2);
    }
    
    //todo, reduce result -> none | explode(left, right) | split 

    private Node.Number LeftMostNumber(Node node) => node switch
    {
        Node.Pair(var left, _) => LeftMostNumber(left),
        Node.Number number => number,
    };
    private Node.Number RightMostNumber(Node node) => node switch
    {
        Node.Pair(_, var right) => RightMostNumber(right),
        Node.Number number => number,
    };

    private Node Sum(Node n1, Node n2)
    {
        var summed = new Node.Pair(n1, n2);

        return ReduceVisitor.Reduce(summed);
    }

    public void Part2(IEnumerable<string> lines)
    {
        
    }

    interface IVisitor
    {
        void VisitPair(Node.Pair pair, int depth);
        void VisitNumber(Node.Number number, int depth);
    }

    class Printer : IVisitor
    {
        private readonly StringBuilder _sb = new();
        public static void Print(Node node)
        {
            var printer = new Printer();
            node.Accept(printer, 0);
            Console.WriteLine(printer._sb.ToString());
        }

        public void VisitPair(Node.Pair pair, int depth)
        {
            _sb.Append('[');
            pair.Left.Accept(this, depth + 1);
            _sb.Append(',');
            pair.Right.Accept(this, depth + 1);
            _sb.Append(']');
        }

        public void VisitNumber(Node.Number number, int depth)
        {
            _sb.Append(number.Value);
        }
    }

    class ReduceVisitor : IVisitor
    {
        private readonly Stack<Node> _reducedNodes = new();

        public static Node Reduce(Node node)
        {
            var visitor = new ReduceVisitor();
            node.Accept(visitor, 0);
            return visitor._reducedNodes.Pop();
        }
        
        public void VisitPair(Node.Pair pair, int depth)
        {
            pair.Left.Accept(this, depth + 1);
            pair.Right.Accept(this, depth + 1);

            var right = _reducedNodes.Pop();
            var left = _reducedNodes.Pop();
            
            if (depth == 4)
            {
                Explode();
            }
            
            _reducedNodes.Push(new Node.Pair(left, right));
        }

        private void Explode()
        {
            //explode
            var left = (_reducedNodes.Pop() as Node.Number)!;
            var right = (_reducedNodes.Pop() as Node.Number)!;

            _reducedNodes.Push(new Node.Number(0));
        }
        
        private static Node.Pair Split(Node.Number number)
        {
            var half = number.Value / 2;
            return new Node.Pair(new Node.Number(half), new Node.Number(number.Value - half));
        }

        public void VisitNumber(Node.Number number, int depth)
        {
            _reducedNodes.Push(number.Value > 9 ? Split(number) : number);
        }
    }
    
    
    private static Node Parse(ReadOnlySpan<char> input)
    {
        ParseInner(input, out var node);
        return node;
        
        ReadOnlySpan<char> ParseInner(ReadOnlySpan<char> input, out Node node)
        {
            switch (input[0])
            {
                case '[':
                    var remaining = ParseInner(input[1..], out var left);
                    Debug.Assert(remaining[0] == ',');
                    remaining = ParseInner(remaining[1..], out var right);
                    Debug.Assert(remaining[0] == ']');
                    node = new Node.Pair(left, right);
                    return remaining[1..];
                case {} c when Char.IsDigit(c):
                    node = new Node.Number(input[0] - '0');
                    return input[1..];
                default:
                    throw new ArgumentException();
            }
        }
    }

}