using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2017
{
	public class Day2 : IPuzzleSolution
	{
		public int Year => 2017;
		public int Day => 2;

		public string SolvePart1(string input)
		{
			return input
				.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(line => line.Split()
				.Select(int.Parse).ToList())
				.Select(digits => digits.Max() - digits.Min())
				.Sum().ToString();
		}

		public string SolvePart2(string input)
		{
			return input
				.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(line => line.Split()
					.Select(int.Parse).ToList())
				.Select(GetEvenDivision)
				.Sum().ToString();
		}

		private static int GetEvenDivision(IList<int> numbers)
		{
			for (int i = 0; i < numbers.Count; i++)
			for (int j = 0; j < numbers.Count; j++)
			{
				if (i == j)
				{
					continue;
				}

				if (numbers[i] % numbers[j] == 0)
				{
					return numbers[i] / numbers[j];
				}
			}
			return 0;
		}

		public ICollection<bool> Assertions => new[]
		{
SolvePart1(
	@"5 1 9 5
7 5 3
2 4 6 8") == "18",

SolvePart2(
	@"5 9 2 8
9 4 7 3
3 8 6 5") == "9",

		};
}
}
