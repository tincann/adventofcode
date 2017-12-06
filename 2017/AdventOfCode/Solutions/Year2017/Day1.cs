using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2017
{
    public class Day1 : IPuzzleSolution
    {
	    public int Year => 2017;
	    public int Day => 1;

	    public string SolvePart1(params string[] lines)
	    {
		    var input = lines[0];
		    return input
				.Where((t, i) => t == input[(i + 1) % input.Length])
				.Sum(t => t - '0').ToString();
	    }

	    public string SolvePart2(params string[] lines)
	    {
		    var input = lines[0];
			return input
				.Where((t, i) => t == input[(i + input.Length / 2) % input.Length])
				.Sum(t => t - '0').ToString();
		}

	    public ICollection<bool> Assertions => new []
	    {
			SolvePart1("1122") == "3",
		    SolvePart1("1111") == "4",
		    SolvePart1("1234") == "0",
		    SolvePart1("91212129") == "9",

		    SolvePart2("1212") == "6",
		    SolvePart2("1221") == "0",
		    SolvePart2("123425") == "4",
		    SolvePart2("123123") == "12",
		    SolvePart2("12131415") == "4",
		};
    }
}
