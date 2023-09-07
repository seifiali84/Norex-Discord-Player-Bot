using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.VoiceNext;

namespace Norex_Discord_Player_Bot
{

    public class SlashCommands : ApplicationCommandModule
    {

        [SlashCommand("test", "A slash command made to test the DSharpPlus Slash Commands extension!")]
        public async Task TestCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Success!"));
        }
        [SlashCommand("delaytest", "A slash command made to test the DSharpPlus Slash Commands extension!")]
        public async Task DelayTestCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            //Some time consuming task like a database call or a complex operation

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Thanks for waiting!"));
        }
        [SlashCommand("MessageTest", "this is a test for sending global message")]
        public async Task MessageTest(InteractionContext ctx)
        {
            var msg = await new DiscordMessageBuilder()
        .WithContent("Here is a really dumb file that I am testing with.")
        .SendAsync(ctx.Channel);

        }
        [SlashCommand("join", "this command join the bot in your channel")]
        public async Task JoinCommand(InteractionContext ctx)
        {
            var user = ctx.Member;
            var voiceChannel = user.VoiceState?.Channel;

            if (voiceChannel != null)
            {
                var connection = await voiceChannel.ConnectAsync();
                // Handle the successful connection, e.g., start playing audio
            }
            else
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Join to a Voice Channel First!"));
            }

        }

        [SlashCommand("play", "this command play a music in your channel")]
        public async Task PlayCommand(InteractionContext ctx, [Option("Music", "music")] string path = "https://dl.hitava.ir/musics/Reza%20Pishro/moshkel-dari-Reza-Pishro.mp3")
        {
            if (ctx.Client.GetVoiceNext().GetConnection(ctx.Guild) != null)
            {
                var vnext = ctx.Client.GetVoiceNext();
                var connection = vnext.GetConnection(ctx.Guild);
                var transmit = connection.GetTransmitSink();
                Console.WriteLine("Connecting to channel");
                var pcm = ConvertAudioToPcm(path);
                Console.WriteLine("Convert Music File to PCM");
                await pcm.CopyToAsync(transmit);
                Console.WriteLine("File Played Success.");
                await pcm.DisposeAsync();
                Console.WriteLine("End Playing ....");
            }
            else
            {
                await JoinCommand(ctx);
                var vnext = ctx.Client.GetVoiceNext();
                var connection = vnext.GetConnection(ctx.Guild);
                var transmit = connection.GetTransmitSink();
                Console.WriteLine("Connecting to channel");
                var pcm = ConvertAudioToPcm(path);
                Console.WriteLine("Convert Music File to PCM");
                await pcm.CopyToAsync(transmit);
                Console.WriteLine("File Played Success.");
                await pcm.DisposeAsync();
                Console.WriteLine("End Playing ....");
            }
        }
        [SlashCommand("leave", "use this command to bot leaving your voice chat alone...")]
        public async Task LeaveCommand(InteractionContext ctx)
        {
            var vnext = ctx.Client.GetVoiceNext();
            var connection = vnext.GetConnection(ctx.Guild);

            connection.Disconnect();
        }

        private Stream ConvertAudioToPcm(string filePath)
        {
            Console.WriteLine("We are in Convert Function");
            var ffmpeg = Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $@"-i ""{filePath}"" -ac 2 -f s16le -ar 48000 pipe:1",
                RedirectStandardOutput = true,
                UseShellExecute = false
            });
            Console.WriteLine("Proccess Was Done!");
            return ffmpeg.StandardOutput.BaseStream;
        }
        //commands
    }
}
