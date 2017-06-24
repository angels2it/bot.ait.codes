using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace bot.ait.codes.Commands
{
    [Serializable]

    public class ResetCommandHandler : BaseCommandHandler
    {
        public ResetCommandHandler() : base(Command.Reset)
        {
        }

        public override async Task Handle(IDialogContext bot, string message)
        {
            try
            {
                await bot.PostAsync($"Ok");
            }
            catch (KeyNotFoundException)
            {
                // ignored
            }
        }
    }
}