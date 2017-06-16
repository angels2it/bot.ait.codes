using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace bot.ait.codes.Commands
{
    public class UpdateGroupCommand : BaseCommand
    {
        public UpdateGroupCommand() : base(Command.UpdateGroup)
        {
        }

        public override async Task Handle(IDialogContext bot, string message)
        {
        }
    }
}