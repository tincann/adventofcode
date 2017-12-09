using System;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Automation;
using AdventOfCode.Solutions;
using AdventOfCode.Solutions.Year2017;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
	        var api = new PuzzleApi("http://adventofcode.com", new SessionProvider());
	        var runner = new SolutionRunner(api);

	        var days = new IPuzzleSolution[]
	        {
				new Day1(), 
		        new Day2(),
		        new Day3(),
		        new Day4(),
		        new Day5(),
		        new Day6(),
		        new Day7(),
		        new Day8(),
			}.ToList();
			//days.ForEach(x => runner.Run(x).Wait());


			runner.Run(new Day9()).Wait();
        }	
    }
}
