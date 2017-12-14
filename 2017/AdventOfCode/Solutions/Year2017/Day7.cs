using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2017
{
    public class Day7 : IPuzzleSolution
    {
        public int Year => 2017;
        public int Day => 7;

        public string SolvePart1(params string[] input)
        {
            //no tree (:
            var childSet = new HashSet<string>();
            var disks = input.Select(Disk.Parse).ToList();
            foreach (var disk in disks)
            {
                foreach (var child in disk.Children)
                {
                    childSet.Add(child);
                }
            }

            var bottom = disks.Single(x => !childSet.Contains(x.Name));

            return bottom.Name;
        }

        public string SolvePart2(params string[] input)
        {
            var d = input.Select(Disk.Parse).ToDictionary(x => x.Name).ToImmutableDictionary();

            var root = d[SolvePart1(input)];

            try
            {
                CalculateTowerWeight(root, d);
            }
            catch (WrongWeightException e)
            {
                return e.CorrectedWeight.ToString();
            }
            return "";
        }

        private static int CalculateTowerWeight(Disk root, IReadOnlyDictionary<string, Disk> d)
        {
            var children = root.Children.Select(c => d[c]).ToList();
            var childTowerWeights = children.Select(c => (child: c, tw: CalculateTowerWeight(c, d))).ToList();

            //if one weight is not equal to the others
            if (childTowerWeights.Select(x => x.tw).Distinct().Count() > 1)
            {
                //get deviant weight
                var wrongWeight = childTowerWeights
                    .GroupBy(x => x.tw)
                    .Select(x => (tw: x.Key, count: x.Count()))
                    .Single(x => x.count == 1).tw;
                var correctWeight = childTowerWeights.Select(x => x.tw).First(tw => tw != wrongWeight);

                var diff = wrongWeight - correctWeight;
                var wrongChild = childTowerWeights.Single(x => x.tw == wrongWeight).child;

                throw new WrongWeightException(wrongChild.Weight - diff);
            }

            //otherwise return tower weight
            return root.Weight + childTowerWeights.Sum(x => x.tw);
        }

        private class WrongWeightException : Exception
        {
            public int CorrectedWeight { get; }

            public WrongWeightException(int correctedWeight)
            {
                CorrectedWeight = correctedWeight;
            }
        }

        private class Disk
        {
            public string Name { get; private set; }
            public int Weight { get; private set; }

            public string[] Children { get; private set; }

            private static readonly Regex R = new Regex(@"^(?<id>\w+)\s\((?<weight>\d+)\)(?:\s->\s(?<children>.+))?$");

            public static Disk Parse(string input)
            {
                var matches = R.Match(input);

                var disk = new Disk
                {
                    Name = matches.Groups["id"].Captures[0].Value,
                    Weight = int.Parse(matches.Groups["weight"].Captures[0].Value),
                };

                if (matches.Groups["children"].Success)
                {
                    disk.Children = matches.Groups["children"].Captures[0].Value.Split(", ").ToArray();
                }
                else
                {
                    disk.Children = new string[0];
                }

                return disk;
            }
        }

        public ICollection<bool> Assertions => new[]
        {
            true,
            //SolvePart1("") == "",
            //SolvePart2("") == "",
        };
    }
}