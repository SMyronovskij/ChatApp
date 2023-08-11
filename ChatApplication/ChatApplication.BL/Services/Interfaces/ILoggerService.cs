using ChatApplication.BL.Services.Implementations;

namespace ChatApplication.BL.Services.Interfaces;

public interface ILoggerService
{
    public Action<string> OnChatLog { get; set; }

    public void Log(string message, LogType logType);
}