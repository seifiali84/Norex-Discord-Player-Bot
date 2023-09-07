using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.SlashCommands;
using DSharpPlus.VoiceNext;
using System;
using System.Threading.Tasks;


namespace Norex_Discord_Player_Bot
{
    public class Program
    {
        public static async Task Main(string[] Args)
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = File.ReadAllText("token.txt"),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            // just available in PV Chat 
            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                {
                    await e.Message.RespondAsync("pong!");
                    Console.WriteLine(e.Message.Content);
                }
                Console.WriteLine(e.Message.Content.Length);
            };
            var slash = discord.UseSlashCommands();
            discord.UseVoiceNext();
            //var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            //{
            //    StringPrefixes = new[] { "/" }
            //});


            slash.RegisterCommands<SlashCommands>();
            //commands.RegisterCommands<Commands>();
            await discord.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}