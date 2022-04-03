using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2017
{
    public class Day13 : IPuzzleSolution
    {
        public int Year => 2017;
        public int Day => 13;
        
        public string SolvePart1(params string[] input)
        {
            var layers = input.Select(Layer.Parse);
            var damage = Walk(layers, 0);
            return damage.ToString();
        }

        public string SolvePart2(params string[] input)
        {
            var layers = input.Select(Layer.Parse).ToList();
            
            var delay = 0;
            while (true)
            {
                var damage = Walk(layers, delay, stopAtDetection: true);
                if (damage == 0 && delay >= 10)
                {
                    return delay.ToString();
                }

                delay++;
            }
        }

        private static int Walk(IEnumerable<Layer> layers, int startTime, bool stopAtDetection = false)
        {
            var damage = 0;
            foreach(var layer in layers)
            { 
                if (layer.ScannerPosition(startTime + layer.Depth) == 0)
                {
                    damage += layer.Depth * layer.Range;
                    if (stopAtDetection)
                    {
                        return 1;
                    }
                }
            }
            return damage;
        }

        private class Layer
        {
            public int Depth { get; private set; }
            public int Range { get; private set; }

            public int ScannerPosition(int time)
            {
                return time % ((Range - 1) * 2);
            }

            public static Layer Parse(string s)
            {
                var match = R.Match(s);
                return new Layer
                {
                    Depth = int.Parse(match.Groups[1].Value),
                    Range = int.Parse(match.Groups[2].Value)
                };
            }

            private static readonly Regex R = new Regex(@"(\d+): (\d+)");
        }

        public ICollection<bool> Assertions => new bool[]
        {
        };
    }
}