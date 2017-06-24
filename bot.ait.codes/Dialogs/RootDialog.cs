using System;
using System.Threading.Tasks;
using bot.ait.codes.Commands;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace bot.ait.codes.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private static CommandHandlerList _handlerList;
        public RootDialog(CommandHandlerList handlerList)
        {
            _handlerList = handlerList;
        }
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            await _handlerList.Process(context, activity?.ChannelId, activity?.Text);
            // return our reply to the user
            context.Wait(MessageReceivedAsync);
        }
    }
}