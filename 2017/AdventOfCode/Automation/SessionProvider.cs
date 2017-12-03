using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode.Automation
{
    public class SessionProvider
    {
	    private const string TokenPath = "session.cookie";
	    public async Task<string> GetSessionToken()
	    {
		    if (File.Exists(TokenPath))
		    {
			    return await File.ReadAllTextAsync(TokenPath);
		    }

			Console.WriteLine("Provide session id:");
		    var token = Console.ReadLine();
		    await File.WriteAllTextAsync(TokenPath, token);
		    return token;
	    }
    }
}
