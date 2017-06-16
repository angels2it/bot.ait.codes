using System.Collections.Generic;
using System.Threading.Tasks;
using bot.ait.codes.Services;
using Microsoft.Bot.Builder.Dialogs;

namespace bot.ait.codes.Commands
{
    public class ResetGroupCommand : BaseCommand
    {
        private readonly DataService _dataService;
        public ResetGroupCommand(DataService dataService) : base(Command.ResetGroup)
        {
            _dataService = dataService;
        }

        public override async Task Handle(IDialogContext bot, string message)
        {
            try
            {
                var data = message.Split(' ');
                await _dataService.AddPost(data[1], data[2]);
                await bot.PostAsync($"Ok");
            }
            catch (KeyNotFoundException)
            {
                // ignored
            }
        }
    }
}