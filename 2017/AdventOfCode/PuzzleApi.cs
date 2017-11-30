using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AdventOfCode.Solutions;
using NLog;

namespace AdventOfCode.Core
{
    public class PuzzleApi
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private readonly HttpClient _client = new HttpClient();

	    private readonly string _baseUrl;
	    private string GetPuzzleUrl(int year, int day) => $"{_baseUrl}/{year}/day/{day}";
		private string GetPuzzleInputPath(int year, int day) => Path.Combine("inputs", $"{year}_{day}.txt");

		public PuzzleApi(string baseUrl, string sessionId)
	    {
		    _baseUrl = baseUrl.TrimEnd('/');
		    _client.DefaultRequestHeaders.Add("Cookie", $"session={sessionId}");
		}

		public async Task<string> LoadInput(int year, int day)
		{
			var puzzlePath = GetPuzzleInputPath(year, day);
			if (File.Exists(puzzlePath))
			{
				logger.Info("Loading input from disk");
				return await File.ReadAllTextAsync(puzzlePath);
			}

			Directory.CreateDirectory(Path.GetDirectoryName(puzzlePath));

			logger.Info("Loading input from web");
			var inputUrl = $"{GetPuzzleUrl(year, day)}/input";
			var input = await _client.GetStringAsync(inputUrl);
			await File.WriteAllTextAsync(puzzlePath, input);
			return input;
		}

	    public async Task<bool> SubmitAnswer(int year, int day, PuzzlePart part, string answer)
	    {
		    var puzzleUrl = GetPuzzleUrl(year, day);

		    var content = new FormUrlEncodedContent(new []
		    {
			    new KeyValuePair<string, string>("level", ((int)part).ToString()),
				new KeyValuePair<string, string>("answer", answer), 
		    });
			
		    var response = await _client.PostAsync($"{puzzleUrl}", content);
		    var body = await response.Content.ReadAsStringAsync();
		    if (body.Contains("That's not the right answer"))
		    {
			    return false;
		    }

		    return true;
	    }
	}
}
