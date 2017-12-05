using System;
using AdventOfCode.Automation;
using AdventOfCode.Solutions.Year2017;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
	        var api = new PuzzleApi("http://adventofcode.com", new SessionProvider());
	        var runner = new SolutionRunner(api);
	        runner.Run(new Day5()).Wait();
        }	
    }
}
