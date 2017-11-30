using System;
using AdventOfCode.Core;
using AdventOfCode.Runner;
using AdventOfCode.Solutions.Year2017;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
	        if (args.Length == 0)
	        {
		        Console.WriteLine("Session token:");
				args = new string[1];
		        args[0] = Console.ReadLine();
	        }

	        var api = new PuzzleApi("http://adventofcode.com", args[0]);
	        var runner = new SolutionRunner(api);
	        runner.RunTests(new Day1()).Wait();
        }	
    }
}
