using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bot.ait.codes.Commands;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace bot.ait.codes.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private readonly CommandList _commandList;
        public RootDialog(IEnumerable<BaseCommand> commands)
        {
            _commandList = new CommandList();
            foreach (var command in commands)
            {
                _commandList.Add(command);
            }
        }
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            await _commandList.Process(context, activity?.ChannelId, activity?.Text);
            // return our reply to the user
            context.Wait(MessageReceivedAsync);
        }
    }
}