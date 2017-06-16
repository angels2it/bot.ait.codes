﻿using System.Threading.Tasks;
using Hangfire;
using Microsoft.Bot.Builder.Dialogs;

namespace bot.ait.codes.Commands
{
    public class UpdateCommand : BaseCommand
    {
        public UpdateCommand() : base(Command.Update)
        {
        }

        public override async Task Handle(IDialogContext bot, string message)
        {
            var groupId = message.Split(' ')[1];
            RecurringJob.Trigger("StartUpdateTask");
            await bot.PostAsync("Ok");
        }
    }
}
