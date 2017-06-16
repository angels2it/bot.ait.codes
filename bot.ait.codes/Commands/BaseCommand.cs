using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace bot.ait.codes.Commands
{
    public abstract class BaseCommand : ICommandHandler
    {
        public Command Command { get; private set; }

        protected BaseCommand(Command command)
        {
            Command = command;
        }
        public abstract Task Handle(IDialogContext bot, string message);
    }
}