using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2017
{
    public class Day16 : IPuzzleSolution
    {
        public int Year => 2017;
        public int Day => 16;

        public string SolvePart1(params string[] input)
        {
            var commands = input[0].Split(',');

            var people = Enumerable.Range(0, 16).Select(x => ((char) (x + 97)).ToString()).ToList();

            foreach (var command in commands) {
                switch (command[0])
                {
                    case 's': //spin
                        var count = int.Parse(command.Substring(1));
                        people = people.TakeLast(count).Concat(people.Take(people.Count - count)).ToList();
                        break;
                    case 'x': //swap
                        var positions = command.Substring(1).Split('/').Select(int.Parse).ToList();
                        people = Swap(people, positions[0], positions[1]).ToList();
                        break;
                    case 'p': //partner
                        var partners = command.Substring(1).Split('/');
                        var pos1 = people.FindIndex(x => x == partners[0]);
                        var pos2 = people.FindIndex(x => x == partners[1]);
                        people = Swap(people, pos1, pos2).ToList();
                        break;
                }
            }

            return string.Join("", people);
        }

        private static IEnumerable<string> Swap(IEnumerable<string> people, int pos1, int pos2)
        {
            var p = people.ToArray();
            var a = p[pos1];
            p[pos1] = p[pos2];
            p[pos2] = a;
            return p;
        }

        public string SolvePart2(params string[] input)
        {
            throw new NotImplementedException();
        }

        private static (char cmd, string[] args) Parse(string s)
        {
            switch (s[0])
            {
                case 's':
                    return (s[0], new [] {s.Substring(1)} );
                case 'x':
                case 'p':
                    return (s[0], s.Substring(1).Split('/'));
                default:
                    throw new Exception("");
            }
        }

        public ICollection<bool> Assertions => new bool[]
        {
            SolvePart1("s1,x3/4,pe/b") == "baedc",
            //SolvePart2("") == "",
        };
    }
}