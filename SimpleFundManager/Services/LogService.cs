using System.IO;

namespace SimpleFundManager.Services;

public static class LogService
{
    private static readonly string LogPath = "log.txt";

    public static void Write(string message)
    {
        var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}  {message}";
        File.AppendAllLines(LogPath, new[] { line });
    }
}