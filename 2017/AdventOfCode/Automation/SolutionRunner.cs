using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Solutions;

namespace AdventOfCode.Automation
{
    public class SolutionRunner
    {
	    private readonly PuzzleApi _api;

	    public SolutionRunner(PuzzleApi api)
	    {
		    _api = api;
	    }

	    public async Task Run(IPuzzleSolution solution)
	    {
			Console.WriteLine($"# Testing puzzle {solution.Year} - {solution.Day}");
			var input = await _api.LoadInput(solution.Year, solution.Day);
		    var inputLines = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
			try
			{
				if (!solution.Assertions.All(x => x))
				{
					Console.WriteLine("Assertions failed:");
					Console.WriteLine(string.Join("\n", solution.Assertions));
				}
				else
				{
					Console.WriteLine($"All {solution.Assertions.Count} assertions passed");
				}

				var sw = new Stopwatch();
				foreach (var part in new [] { PuzzlePart.Part1, PuzzlePart.Part2 })
				{
					sw.Restart();
					var output = part == PuzzlePart.Part1 ? 
						solution.SolvePart1(inputLines) : 
						solution.SolvePart2(inputLines);

					Console.WriteLine(sw.Elapsed.Milliseconds + "ms");

					await AskSubmit(solution, part, output);
				}
				Console.ReadKey();
			}
			catch (NotImplementedException e)
		    {
			    Console.WriteLine($"Not all parts implemented yet {e.StackTrace}");
		    }
	    }

	    private async Task AskSubmit(IPuzzleSolution solution, PuzzlePart part, string answer)
	    {
			Console.WriteLine("Answer:");
			Console.WriteLine(answer);
		    Console.WriteLine($"Submit {part}? y/n");
		    if (Console.ReadLine() == "y")
		    {
			    var result = await _api.SubmitAnswer(solution.Year, solution.Day, part, answer);
			    Console.WriteLine(result ? "Answer was correct!" : "Answer was NOT correct!");
		    }
	    }
    }
}
