using Discord;
using Discord.WebSocket;
using DurnanBot.SlashCommands;

namespace DurnanBot;

public class App
{
    private const ulong _nynstrom = 1050386566926831648;

    public readonly DiscordSocketClient _client;
    private readonly string _token;

    public App(DiscordSocketClient client, IAppLogger logger, string token)
    {
        _client = client;
        _token = token;
        _client.Log += logger.Log;
    }

    public async Task Run()
    {
        await _client.StartAsync();
        Console.WriteLine("Init");
        var count = 0;
        while (true)
        {
            count++;
            await Task.Delay(TimeSpan.FromSeconds(5));
            Console.WriteLine("running");
            if (count > 10_1000)
                break;
        }
    }

    public async Task Init()
    {
        _client.Ready += ClientReady;
        await _client.LoginAsync(TokenType.Bot, _token);
    }

    private async Task ClientReady()
    {
        var nynstromGuild = _client.GetGuild(_nynstrom);
        var echoSlashCommand = new EchoSlashCommand("echoslash", "babbys first slash command", _nynstrom);
        var loreDumpCommand = new LoreDumpSlashCommand("loredump", "lore dump ;)", _nynstrom);
        var registerResult = await echoSlashCommand.RegisterGuildCommand(nynstromGuild);
        _ = await loreDumpCommand.RegisterGuildCommand(nynstromGuild);
        _client.SlashCommandExecuted += loreDumpCommand.Handle;
        Console.WriteLine($"registered {echoSlashCommand.Name} @ {registerResult.CreatedAt}");
    }
}