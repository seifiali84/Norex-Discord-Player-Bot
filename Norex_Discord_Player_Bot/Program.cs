using DSharpPlus;
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

            await discord.ConnectAsync();
            Task.Delay(-1);
        }
    }
}