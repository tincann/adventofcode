using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2017
{
	public class Day11 : IPuzzleSolution
	{
		public int Year => 2017;
		public int Day => 11;

		//great resource: https://www.redblobgames.com/grids/hexagons/
		public string SolvePart1(params string[] input)
		{
			var steps = input[0].Split(',').Select(d => _directions[d]);
			var position = (x: 0, y: 0, z: 0);
			foreach (var (x, y, z) in steps)
			{
				position.x += x;
				position.y += y;
				position.z += z;
			}

			return CubeDistance((0, 0, 0), position).ToString();
		}

		public string SolvePart2(params string[] input)
		{
			var steps = input[0].Split(',').Select(d => _directions[d]);
			var position = (x: 0, y: 0, z: 0);

			var maxDistance = 0;
			foreach (var (x, y, z) in steps)
			{
				position.x += x;
				position.y += y;
				position.z += z;

				var d = CubeDistance((0, 0, 0), position);
				maxDistance = Math.Max(d, maxDistance);
			}

			return maxDistance.ToString();
		}

		private static int CubeDistance((int x, int y, int z) a, (int x, int y, int z) b)
		{
			return (Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + Math.Abs(a.z - b.z)) / 2;
		}

		readonly Dictionary<string, (int x, int y, int z)> _directions = new Dictionary<string, (int x, int y, int z)>
		{
			{"se", (1, -1, 0)},
			{"ne", (1, 0, -1)},
			{"n", (0, 1, -1)},
			{"nw", (-1, 1, 0)},
			{"sw", (-1, 0, 1)},
			{"s", (0, -1, 1)},
		};

		public ICollection<bool> Assertions => new bool[]
		{
			//SolvePart1("") == "",
			//SolvePart2("") == "",
		};
	}
}
