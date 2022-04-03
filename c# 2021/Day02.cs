namespace c__2021;

public class Day02
{
    enum Direction
    {
        Forward,
        Down,
        Up
    }

    record Command(Direction Direction, int Value)
    {
        public static Command Parse(string line)
        {
            var parts = line.Split();
            return new Command(parts[0] switch
            {
                "forward" => Direction.Forward,
                "down" => Direction.Down,
                "up" => Direction.Up,
                _ => throw new ArgumentOutOfRangeException()
            }, int.Parse(parts[1]));
        }
    }

    public void Part1(IEnumerable<string> lines)
    {
        
        var horizontal = 0;
        var depth = 0;

        foreach (var command in lines.Select(Command.Parse))
        {
            switch (command.Direction)
            {
                case Direction.Forward:
                    horizontal += command.Value;
                    break;
                case Direction.Down:
                    depth += command.Value;
                    break;
                case Direction.Up:
                    depth -= command.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        Console.WriteLine(horizontal * depth);
    }

    public void Part2(IEnumerable<string> lines)
    {
        var horizontal = 0;
        var depth = 0;
        var aim = 0;

        foreach (var command in lines.Select(Command.Parse))
        {
            switch (command.Direction)
            {
                case Direction.Forward:
                    horizontal += command.Value;
                    depth += aim * command.Value;
                    break;
                case Direction.Down:
                    aim += command.Value;
                    break;
                case Direction.Up:
                    aim -= command.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        Console.WriteLine(horizontal * depth);
    }
}