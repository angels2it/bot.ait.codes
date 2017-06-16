using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace bot.ait.codes.Services
{
    public class DataService
    {
        private readonly at_botEntities _context;

        public DataService(at_botEntities context)
        {
            _context = context;
        }

        public async Task AddPost(string groupId, string postId)
        {
            _context.Posts.Add(new Post()
            {
                GroupId = groupId,
                PostId = postId
            });
            await _context.SaveChangesAsync();
        }

        private async Task<List<string>> GetPosts(string groupId)
        {
            try
            {
                var ids = await _context.Posts.Where(e => e.GroupId == groupId).Select(e => e.PostId).ToListAsync();
                return ids;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public async Task<List<Chat>> GetChatIds()
        {
            return await _context.Chats.ToListAsync() ?? new List<Chat>();
        }
        public async Task RemoveChatId(string chatId)
        {
            var chatIds = await GetChatIds();
            chatIds.RemoveAll(e => e.ConversasionId == chatId);
        }

        public async Task AddChatId(IActivity activity)
        {
            var chatIds = await GetChatIds();
            if (chatIds.Any(e => e.ConversasionId == activity.Conversation.Id))
                return;
            _context.Chats.Add(new Chat()
            {
                SubcriberId = activity.From.Id,
                ServiceUrl = activity.ServiceUrl,
                ConversasionId = activity.Conversation.Id,
                ChannelId = activity.ChannelId
            });
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetLatestId(string groupId)
        {
            try
            {
                var ids = await GetPosts(groupId);
                return ids.LastOrDefault();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
