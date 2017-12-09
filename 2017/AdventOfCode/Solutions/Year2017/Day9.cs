using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2017
{
	public class Day9 : IPuzzleSolution
	{
		public int Year => 2017;
		public int Day => 9;

		public string SolvePart1(params string[] input)
		{
			int depth = 0, score = 0;
			var garbage = false;
			for(var i = 0; i < input[0].Length; i++)
			{
				var c = input[0][i];

				switch (c)
				{
					case '{':
						if(!garbage) depth++;
						break;
					case '}':
						if (!garbage)
						{
							score += depth;
							depth--;
						}
						break;
					case '<':
						garbage = true;
						break;
					case '>':
						garbage = false;
						break;
					case '!':
						i++;
						break;
				}
			}
			return score.ToString();
		}

		public string SolvePart2(params string[] input)
		{
			var count = 0;
			var garbage = false;
			for (var i = 0; i < input[0].Length; i++)
			{
				var c = input[0][i];

				switch (c)
				{
					case '<':
						if (!garbage) { count--; }

						garbage = true;
						break;
					case '>':
						garbage = false;
						break;
					case '!':
						i++;
						break;
				}

				if (garbage && c != '!')
				{
					count++;
				}
			}
			return count.ToString();
		}

		public ICollection<bool> Assertions => new[]
		{
			SolvePart1("{}") == "1",
			SolvePart1("{{{}}}") == "6",
			SolvePart1("{{},{}}") == "5",
			SolvePart1("{{{},{},{{}}}}") == "16",
			SolvePart1("{<a>,<a>,<a>,<a>}") == "1",
			SolvePart1("{{<ab>},{<ab>},{<ab>},{<ab>}}") == "9",
			SolvePart1("{{<!!>},{<!!>},{<!!>},{<!!>}}") == "9",
			SolvePart1("{{<a!>},{<a!>},{<a!>},{<ab>}}") == "3",

			SolvePart2("<>") == "0",
			SolvePart2("<random characters>") == "17",
			SolvePart2("<<<<>") == "3",
			SolvePart2("<{!>}>") == "2",
			SolvePart2("<!!>") == "0",
			SolvePart2("<!!!>>") == "0",
			SolvePart2(@"<{o""i!a,<{i<a>") == "10",

			
		};
	}
}
