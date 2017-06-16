using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAiSDK;
using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json.Linq;

namespace bot.ait.codes.Commands
{
    public class CommandList : List<ICommandHandler>
    {
        static AIConfiguration config = new AIConfiguration("ca83e0ee08054e6c87e06883fdcad740", SupportedLanguage.English);
        static ApiAi apiAi = new ApiAi(config);
        public async Task Process(IDialogContext bot, string channel, string message)
        {
            var messageText = Helpers.GetMessage(message);
            string command = messageText.Split(' ')[0].ToLower().Trim();
            var processer = this.FirstOrDefault(e => e.Command.ToString().ToLower() == command);
            if (processer != null)
            {
                try
                {
                    await processer.Handle(bot, messageText);
                }
                catch (Exception e)
                {
                    await bot.PostAsync(e.StackTrace);
                }
            }
            else
            {
                try
                {
                    var response = apiAi.TextRequest(messageText);
                    if (response.IsError)
                        await bot.PostAsync("Command not support yet");
                    else
                    {
                        var messages =
                            response.Result.Fulfillment.Messages?.Cast<JObject>()
                                .Where(e => e["platform"]?.ToString() == channel).ToList() ?? new List<JObject>();
                        if (messages.Count == 0)
                        {
                            await bot.PostAsync(response.Result.Fulfillment.Speech);
                            return;
                        }
                        foreach (var mes in messages)
                        {
                            var speech = mes.Value<string>("speech");
                            if (!string.IsNullOrEmpty(speech))
                                await bot.PostAsync(speech);
                        }
                    }
                }
                catch (ApiAiSDK.AIServiceException e)
                {
                    await bot.PostAsync((e.InnerException as AIServiceException)?.Response?.Result?.Fulfillment?.Speech ?? "Unkown what you mean");
                }
            }
        }
    }
}
