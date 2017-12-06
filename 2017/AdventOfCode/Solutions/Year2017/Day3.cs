using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2017
{
	public class Day3 : IPuzzleSolution
	{
		public int Year => 2017;
		public int Day => 3;

		public string SolvePart1(params string[] input)
		{
			var number = int.Parse(input[0]) - 1;
			var (x, y) = Spiral().ElementAt(number);

			var distance = Math.Abs(x) + Math.Abs(y);
			return distance.ToString();
		}

		public string SolvePart2(params string[] input)
		{
			var answer = int.Parse(input[0]);
			var grid = new int[5000, 5000];
			var spiral = Spiral();

			var centerX = grid.GetLength(0) / 2;
			var centerY = grid.GetLength(1) / 2;
			grid[centerX, centerY] = 1;

			foreach (var (x, y) in spiral)
			{
				var gridX = centerX + x;
				var gridY = centerY + y;

				var value = 0;
				for (var dx = -1; dx <= 1; dx++)
				for (var dy = -1; dy <= 1; dy++)
				{
					value += grid[gridX + dx, gridY + dy];
				}

				if (value > answer)
				{
					return value.ToString();
				}

				grid[gridX, gridY] = value;
			}

			throw new Exception();
		}

		private static IEnumerable<(int x, int y)> Spiral()
		{
			int x = 0, y = 0;
			var w = 1;
			var dir = 1;
			while (true)
			{
				for (var dx = 0; dx < w; dx++)
				{
					yield return (x, y);
					x += dir;
				}

				for (var dy = 0; dy < w; dy++)
				{
					yield return (x, y);
					y += dir;
				}

				w++;
				dir *= -1;
			}
		}

		public ICollection<bool> Assertions => new[]
		{
			SolvePart1("1") == "0",
			SolvePart1("12") == "3",
			SolvePart1("23") == "2",
			SolvePart1("1024") == "31",


			//SolvePart2("1") == "1",
			//SolvePart2("2") == "1",
			//SolvePart2("3") == "2",
			//SolvePart2("4") == "4",
			//SolvePart2("5") == "5",
			//SolvePart2("6") == "10",
		};
	}
}
