// See https://aka.ms/new-console-template for more information

using Discord;
using Discord.WebSocket;
using DurnanBot.Config;
using Microsoft.Extensions.Configuration;

namespace DurnanBot;

public class Program
{

    public static Task Log(LogMessage logMessage)
    {
        Console.WriteLine(logMessage.Message);
        return Task.CompletedTask;
    }

    public static async Task Main()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.dev.json", optional: true);

        var configurationRoot = builder.Build();
        var tokenSection = configurationRoot.GetSection(DiscordSettings.key);
        var token = tokenSection["Token"];
        ArgumentNullException.ThrowIfNull(token);

        var app = new App(new DiscordSocketClient(), new ConsoleLogger(), token);
        await app.Init();
        await app.Run();

    }
}

