using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2017
{
	public class Day4 : IPuzzleSolution
	{
		public int Year => 2017;
		public int Day => 4;

		public string SolvePart1(params string[] input)
		{
			var valid = input
				.Select(line => line.Split())
				.Count(words => words.Length == words.Distinct().Count() );

			return valid.ToString();
		}

		public string SolvePart2(params string[] input)
		{
			var valid = input
				.Select(line => line.Split().Select(x => string.Concat(x.OrderBy(y => y))).ToList())
				.Count(words => words.Count == words.Distinct().Count());

			return valid.ToString();
		}

		public ICollection<bool> Assertions => new[]
		{
			SolvePart1("aa bb cc dd ee") == "1",
			SolvePart1("aa bb cc dd aa") == "0",
			SolvePart1("aa bb cc dd aaa") == "1",
			SolvePart1("aa bb cc dd aaa") == "1",
		};
	}
}
