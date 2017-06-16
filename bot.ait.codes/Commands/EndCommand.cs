using System.Threading.Tasks;
using bot.ait.codes.Services;
using Microsoft.Bot.Builder.Dialogs;

namespace bot.ait.codes.Commands
{
    public class EndCommand : BaseCommand
    {
        private readonly DataService _dataService;
        public EndCommand(DataService dataService) : base(Command.End)
        {
            _dataService = dataService;
        }

        public override async Task Handle(IDialogContext bot, string message)
        {
            await _dataService.RemoveChatId(bot.Activity.Id);
            await bot.PostAsync("Ok, I'm removed");
        }

    }
}