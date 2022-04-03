using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2017
{
    public class Day12 : IPuzzleSolution
    {
        public int Year => 2017;
        public int Day => 12;


        private readonly Regex _r = new Regex(@"^(?<d1>\d+) <-> (?<nb>[\d, ]+)$");

        public string SolvePart1(params string[] input)
        {
            var progs = ParseInput(input);

            return GroupSize(progs["0"], progs, 0).ToString();
        }

        public string SolvePart2(params string[] input)
        {
            var progs = ParseInput(input);

            var groupId = 0;
            while (true)
            {
                var unassigned = progs.Values.FirstOrDefault(x => !x.GroupId.HasValue);
                if (unassigned == null)
                {
                    break;
                }

                GroupSize(unassigned, progs, groupId++);
            }

            return groupId.ToString();
        }

        private Dictionary<string, Prog> ParseInput(IEnumerable<string> input)
        {
            return input
                .Select(x => _r.Match(x))
                .Select(x => (id: x.Groups["d1"].Value, nbs: x.Groups["nb"].Value.Split(", ")))
                .Select(x => new Prog {Id = x.id, Neighbours = x.nbs})
                .ToDictionary(x => x.Id);
        }


        private static int GroupSize(Prog root, IReadOnlyDictionary<string, Prog> progs, int groupId)
        {
            var q = new Queue<Prog>();
            q.Enqueue(root);

            var seen = new HashSet<string>();
            var count = 0;
            while (q.Count > 0)
            {
                var p = q.Dequeue();

                p.GroupId = groupId;

                seen.Add(p.Id);

                foreach (var nb in p.Neighbours.Select(x => progs[x]).Where(x => !seen.Contains(x.Id)))
                {
                    q.Enqueue(nb);
                }

                count++;
            }

            return count;
        }

        private class Prog
        {
            public string Id { get; set; }
            public IList<string> Neighbours { get; set; }

            public int? GroupId { get; set; }
        }


        public ICollection<bool> Assertions => new[]
        {
            true
        };
    }
}