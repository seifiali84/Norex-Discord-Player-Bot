using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.VoiceNext;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        //commands
    }
}
