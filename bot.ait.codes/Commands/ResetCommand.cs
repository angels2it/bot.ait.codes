using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace bot.ait.codes.Commands
{
    public class ResetCommand : BaseCommand
    {
        public ResetCommand() : base(Command.Reset)
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