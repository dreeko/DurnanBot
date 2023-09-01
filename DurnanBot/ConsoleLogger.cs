using Discord;

namespace DurnanBot;

public class ConsoleLogger : IAppLogger
{
    public async Task Log(LogMessage logMessage)
    {
        Console.WriteLine(logMessage.Message);
    }
}

public interface IAppLogger
{
    public Task Log(LogMessage logMessage);
}