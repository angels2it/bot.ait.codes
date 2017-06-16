using System.Threading.Tasks;
using bot.ait.codes.Services;
using Microsoft.Bot.Builder.Dialogs;

namespace bot.ait.codes.Commands
{
    public class StartCommand : BaseCommand
    {
        private readonly DataService _dataService;
        public StartCommand(DataService dataService) : base(Command.Start)
        {
            _dataService = dataService;
        }

        public override async Task Handle(IDialogContext bot, string message)
        {
            await _dataService.AddChatId(bot.Activity);
            await bot.PostAsync("OK");
        }
    }
}