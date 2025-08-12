using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Pentagramm.Data;
using Pentagramm.Models.Entities;
using Pentagramm.DTOs.Message;
using System.Collections.Concurrent;

namespace Pentagramm.Hubs
{
    public class ChatHub(AppDbContext appDbContext) : Hub
    {
        private AppDbContext AppDbContext { get; set; } = appDbContext;

        private static readonly ConcurrentDictionary<string, ConcurrentBag<string>> _userConnections = new();

        public async Task SendMessage(string chatId, string userId, string text)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new HubException("Unauthenticated");
            }

            var isMember = await AppDbContext.ChatMembers.AnyAsync(mem => mem.ChatId == chatId && mem.UserId == userId);

            if (!isMember)
            {
                throw new HubException("No access");
            }

            var message = new Message
            {
                AuthorId = userId,
                AuthorName = AppDbContext.Users.FirstOrDefault(user => user.Id == userId).UserName,
                Text = text,
                CreatedAt = DateTime.UtcNow,
                ChatId = chatId,
                Id = Guid.NewGuid().ToString()
            };

            AppDbContext.Add(message);
            await AppDbContext.SaveChangesAsync();

            await Clients.Group(chatId).SendAsync("ReceiveMessage", new GetMessageDto
            {
                Id = message.Id,
                CreatedAt = message.CreatedAt,
                Text = message.Text,
                AuthorId = message.AuthorId,
                AuthorName = message.AuthorName,
                UpdatedAt = message.UpdatedAt,

            });
        }

        public async Task JoinChat(string chatId)
        {
            if(!Guid.TryParse(chatId, out var cId))
            {
                throw new HubException("Invalid Chat Id");
            }

            var chatExists = await AppDbContext.ChatMembers.AnyAsync(chat => chat.ChatId == chatId);

            if(!chatExists)
            {
                throw new HubException("Chat Not Found");
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
