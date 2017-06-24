using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using bot.ait.codes.Services;
using Microsoft.Bot.Connector;

namespace bot.ait.codes
{
    public class BotJob
    {
        private readonly FacebookService _facebookService;
        private readonly at_botEntities _context;
        public BotJob(FacebookService facebookService, at_botEntities context)
        {
            _facebookService = facebookService;
            _context = context;
        }
        public Task Run()
        {
            var groups = _context.Groups.ToList();
            List<Task> tasks = groups.Select(ProcessForGroup).ToList();
            return Task.WhenAll(tasks);
        }
        private async Task ProcessForGroup(Group @group)
        {
            var images = await _facebookService.GetLatestPhotoAsync(group.Key);
            if (images == null || images.Count == 0)
                return;
            var chats = _context.Chats.ToList();
            var tasks = chats.Select(chat => SendImagesToChannel(chat, images)).ToList();
            await Task.WhenAll(tasks);
        }
        private Task SendImagesToChannel(Chat chat, List<string> images)
        {
            var tasks = images.Select(image => SendImageToChannel(chat, image)).ToList();
            return Task.WhenAll(tasks);
        }
        private Task SendImageToChannel(Chat chat, string image)
        {
            try
            {
                var connector = new ConnectorClient(new Uri(chat.ServiceUrl));
                IMessageActivity newMessage = Activity.CreateMessageActivity();
                newMessage.ChannelId = chat.ChannelId;
                newMessage.Type = ActivityTypes.Message;
                newMessage.From = new ChannelAccount(ConfigurationManager.AppSettings["BotId"]);
                newMessage.Conversation = new ConversationAccount(false, chat.ConversasionId);
                newMessage.Recipient = new ChannelAccount(chat.SubcriberId);
                newMessage.Attachments = new List<Attachment>()
                                {
                                    new Attachment()
                                    {
                                        ContentType = "image/png",
                                        ContentUrl = image
                                    }
                                };
                return connector.Conversations.SendToConversationAsync((Activity)newMessage);
            }
            catch (Exception)
            {
                // ignored
                return Task.FromResult(false);
            }
        }
    }
}