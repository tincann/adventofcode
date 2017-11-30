using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Core;
using AdventOfCode.Solutions;
using NLog;

namespace AdventOfCode.Runner
{
    public class SolutionRunner
    {
	    private static Logger _logger = LogManager.GetCurrentClassLogger();

	    private readonly PuzzleApi _api;

	    public SolutionRunner(PuzzleApi api)
	    {
		    _api = api;
	    }

	    public async Task RunTests(IPuzzleSolution solution)
	    {
		    _logger.Info($"# Testing puzzle {solution.Year} - {solution.Day}");
			var input = await _api.LoadInput(solution.Year, solution.Day);

		    try
		    {
			    var output1 = solution.SolvePart1(input);
			    var output2 = solution.SolvePart2(input);
				//todo
			}
			catch (NotImplementedException)
		    {
			    _logger.Warn("Not all parts implemented yet");
		    }
	    }
    }
}
