using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace bot.ait.codes.Commands
{
    public interface ICommandHandler
    {
        Command Command { get; }
        Task Handle(IDialogContext bot, string message);
    }
}