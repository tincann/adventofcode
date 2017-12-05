using System.Collections.Generic;

namespace AdventOfCode.Solutions
{
    public interface IPuzzleSolution
    {
		int Year { get; }
		int Day { get; }

	    string SolvePart1(string[] input);
	    string SolvePart2(string[] input);

		ICollection<bool> Assertions { get; }
	}
}
