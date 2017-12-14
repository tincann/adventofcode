using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2017
{
    public class Day6 : IPuzzleSolution
    {
        public int Year => 2017;
        public int Day => 6;

        public string SolvePart1(params string[] input)
        {
            var banks = input[0].Split().Select(int.Parse).ToArray();
            var confs = new HashSet<string>();

            string hash;
            var cycles = 0;
            while (!confs.Contains(hash = ConfHash(banks)))
            {
                confs.Add(hash);

                var (value, index) = MaxAt(banks);

                banks[index] = 0;
                while (value > 0)
                {
                    banks[++index % banks.Length] += 1;
                    value--;
                }

                cycles++;
            }

            return cycles.ToString();
        }

        public string SolvePart2(params string[] input)
        {
            var banks = input[0].Split().Select(int.Parse).ToArray();
            var confs = new Dictionary<string, int>();

            string hash;
            var cycles = 0;
            while (!confs.ContainsKey(hash = ConfHash(banks)))
            {
                confs.Add(hash, cycles);

                var (value, index) = MaxAt(banks);

                banks[index] = 0;
                while (value > 0)
                {
                    banks[++index % banks.Length] += 1;
                    value--;
                }

                cycles++;
            }

            return (cycles - confs[hash]).ToString();
        }

        private static string ConfHash(IEnumerable<int> banks) => string.Join("_", banks);

        private static (int max, int index) MaxAt(IEnumerable<int> seq)
        {
            var max = int.MinValue;
            var maxIndex = 0;
            var i = 0;
            foreach (var value in seq)
            {
                if (value > max)
                {
                    max = value;
                    maxIndex = i;
                }
                i++;
            }
            return (max, maxIndex);
        }

        public ICollection<bool> Assertions => new[]
        {
            SolvePart1("2	8	8	5	4	2	3	1	5	5	1	2	15	13	5	14") == "3156",
            SolvePart2("2	8	8	5	4	2	3	1	5	5	1	2	15	13	5	14") == "1610",
        };
    }
}