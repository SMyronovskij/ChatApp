using System.Diagnostics;
using ChatApplication.BL.Services.Interfaces;

namespace ChatApplication.BL.Services.Implementations;

public class LoggerService : ILoggerService
{
    public Action<string> OnChatLog { get; set; }

    public void Log(string message, LogType logType)
    {
        switch (logType)
        {
            case LogType.ChatLog:
                OnChatLog?.Invoke(message);
                break;
            case LogType.ErrorLog:
                Debug.WriteLine($"Error: {message}");
                break;
            case LogType.DebugLog:
                Debug.WriteLine($"Debug: {message}");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
        }
    }
}

public enum LogType
{
    ChatLog,
    ErrorLog,
    DebugLog
}