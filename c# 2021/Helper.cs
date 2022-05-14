namespace c__2021;

public static class Helper
{
    public static string[] SplitBy(this string line, char c) =>
        line.Split(c, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    
    public static string[] SplitBy(this string line, string str) =>
        line.Split(str, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
}