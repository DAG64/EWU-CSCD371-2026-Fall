namespace Logger;
using System;
using System.IO;

public class FileLogger : BaseLogger
{
    private readonly string _filePath;
    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }
    public override void Log(LogLevel logLevel, string message)
    {
        string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{ClassName}] [{logLevel}] {message}";
        File.AppendAllText(_filePath, logMessage + Environment.NewLine);
    }
}