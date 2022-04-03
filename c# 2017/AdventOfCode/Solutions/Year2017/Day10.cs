using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2017
{
    public class Day10 : IPuzzleSolution
    {
        public int Year => 2017;
        public int Day => 10;

        public string SolvePart1(params string[] input)
        {
            var lengths = input[0].Split(',').Select(int.Parse).ToArray();

            var elements = Enumerable.Range(0, 256).ToArray();
            Hash(elements, lengths, 0, 0);
            return (elements[0] * elements[1]).ToString();
        }

        public string SolvePart2(params string[] input)
        {
            var lengths = Encoding.ASCII.GetBytes(input[0]).Select(x => (int) x).ToList();
            lengths.AddRange(new[] {17, 31, 73, 47, 23});

            var elements = Enumerable.Range(0, 256).Select(x => BitConverter.GetBytes(x)[0]).ToArray();
            int position = 0, skip = 0;
            for (var round = 0; round < 64; round++)
            {
                (position, skip) = Hash(elements, lengths, position, skip);
            }

            return DenseHash(elements);
        }

        private static string DenseHash(IList<byte> elements)
        {
            var dense = new byte[16];
            for (var blockIndex = 0; blockIndex < 16; blockIndex++)
            {
                var block = elements.Skip(blockIndex * 16).Take(16).ToList();
                dense[blockIndex] = block[0];
                for (var i = 1; i < 16; i++)
                {
                    dense[blockIndex] ^= block[i];
                }
            }

            var hex = BitConverter.ToString(dense);
            return hex.Replace("-", "").ToLower();
        }

        private static (int position, int skip) Hash<T>(IList<T> elements, IEnumerable<int> lengths, int position,
            int skip)
        {
            foreach (var length in lengths)
            {
                Reverse(elements, position, length);
                position = (position + length + skip) % elements.Count;
                skip++;
            }
            return (position, skip);
        }

        private static void Reverse<T>(IList<T> array, int offset, int count)
        {
            var copy = new T[count];
            for (var i = 0; i < count; i++)
            {
                copy[i] = array[(offset + i) % array.Count];
            }
            Array.Reverse(copy);
            for (var i = 0; i < count; i++)
            {
                array[(offset + i) % array.Count] = copy[i];
            }
        }


        public ICollection<bool> Assertions => new[]
        {
            SolvePart1("83,0,193,1,254,237,187,40,88,27,2,255,149,29,42,100") == "20056",
            SolvePart2("83,0,193,1,254,237,187,40,88,27,2,255,149,29,42,100") == "d9a7de4a809c56bf3a9465cb84392c8e"
        };
    }
}