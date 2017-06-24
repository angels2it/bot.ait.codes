using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace bot.ait.codes.Commands
{
    [Serializable]

    public class UpdateGroupCommandHandler : BaseCommandHandler
    {
        public UpdateGroupCommandHandler() : base(Command.UpdateGroup)
        {
        }

        public override async Task Handle(IDialogContext bot, string message)
        {
        }
    }
}