using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2017
{
    public class Day15 : IPuzzleSolution
    {
        public int Year => 2017;
        public int Day => 15;

        public string SolvePart1(params string[] input)
        {
            var sequenceA = Generate(289, 16807, 2147483647);
            var sequenceB = Generate(629, 48271, 2147483647);

            return sequenceA.Zip(sequenceB, (a, b) => (a: a, b: b))
                .TakeWhile((pair, i) => i < 40_000_000)
                .Sum(p => Compare(p.a, p.b) ? 1 : 0).ToString();
        }

        public string SolvePart2(params string[] input)
        {
            var sequenceA = Generate(289, 16807, 2147483647, 4);
            var sequenceB = Generate(629, 48271, 2147483647, 8);

            return sequenceA.Zip(sequenceB, (a, b) => (a: a, b: b))
                .TakeWhile((pair, i) => i < 5_000_000)
                .Sum(p => Compare(p.a, p.b) ? 1 : 0).ToString();
        }

        private static bool Compare(int a, int b)
        {
            return (a & 0xFFFF) == (b & 0xFFFF);
        }

        private static IEnumerable<int> Generate(long seed, long factor, long denominator, long multiple = 1)
        {
            var previousValue = seed;
            while (true)
            {
                var currentValue = previousValue * factor % denominator;
                if (multiple == 1 || currentValue % multiple == 0)
                {
                    yield return (int) currentValue;
                }
                previousValue = currentValue;
            }
        }

        public ICollection<bool> Assertions => new bool[]
        {
            SolvePart1("") == "638",
            SolvePart2("") == "343",
        };
    }
}