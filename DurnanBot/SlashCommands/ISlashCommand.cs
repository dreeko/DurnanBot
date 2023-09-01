using Discord;
using Discord.WebSocket;

namespace DurnanBot.SlashCommands;

public abstract class SlashCommand
{
    protected SlashCommand(string name, string description, ulong guildId)
    {
        Name = name;
        Description = description;
        GuildId = guildId;
    }

    public ulong GuildId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    private Task<SlashCommandProperties> GetGuildCommandProperties()
    {
        var builder = new SlashCommandBuilder();
        builder.WithDescription(Description);
        builder.WithName(Name);
        return Task.FromResult(builder.Build());
    }

    public async Task<SocketApplicationCommand> RegisterGuildCommand(SocketGuild guild)
    {
        var props = await GetGuildCommandProperties();
        return await guild.CreateApplicationCommandAsync(props);
    }

    public virtual async Task Handle(SocketSlashCommand command)
    {
        await command.RespondAsync($"you executed {command.Data.Name}");
    }
}