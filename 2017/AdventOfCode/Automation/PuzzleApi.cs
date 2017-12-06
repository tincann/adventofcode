using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AdventOfCode.Solutions;

namespace AdventOfCode.Automation
{
    public class PuzzleApi
    {
	    private readonly SessionProvider _sessionProvider;

	    //private static Logger logger = LogManager.GetCurrentClassLogger();
		private readonly HttpClient _client = new HttpClient();

	    private readonly string _baseUrl;
	    private string GetPuzzleUrl(int year, int day) => $"{_baseUrl}/{year}/day/{day}";
		private string GetPuzzleInputPath(int year, int day) => Path.Combine("inputs", $"{year}_{day}.txt");

		public PuzzleApi(string baseUrl, SessionProvider sessionProvider)
		{
			_sessionProvider = sessionProvider;
			_baseUrl = baseUrl.TrimEnd('/');
		}

		public async Task<string> LoadInput(int year, int day)
		{
			var puzzlePath = GetPuzzleInputPath(year, day);
			if (!File.Exists(puzzlePath))
			{
				var sessionId = await _sessionProvider.GetSessionToken();
				_client.DefaultRequestHeaders.Add("Cookie", $"session={sessionId}");

				Directory.CreateDirectory(Path.GetDirectoryName(puzzlePath));

				Console.WriteLine("Loading input from web");
				var inputUrl = $"{GetPuzzleUrl(year, day)}/input";
				var input = await _client.GetByteArrayAsync(inputUrl);
				await File.WriteAllBytesAsync(puzzlePath, input);
			}

			return await LoadInputFromDisk(puzzlePath);
		}

	    private static async Task<string> LoadInputFromDisk(string path)
	    {
		    using (var reader = new StreamReader(File.OpenRead(path)))
		    {
			    var text = await reader.ReadToEndAsync();
			    return text.TrimEnd('\n');
		    }
	    }

		public async Task<bool> SubmitAnswer(int year, int day, PuzzlePart part, string answer)
	    {
		    var puzzleUrl = GetPuzzleUrl(year, day);

		    var content = new FormUrlEncodedContent(new []
		    {
			    new KeyValuePair<string, string>("level", ((int)part).ToString()),
				new KeyValuePair<string, string>("answer", answer),
		    });

		    var sessionId = _sessionProvider.GetSessionToken();
		    _client.DefaultRequestHeaders.Add("Cookie", $"session={sessionId}");

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
