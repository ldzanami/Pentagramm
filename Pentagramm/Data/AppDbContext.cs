using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pentagramm.Infrastructure.SupportClasses;
using Pentagramm.Models.Entities;
using System.Security.Claims;

namespace Pentagramm.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set; }
        public DbSet<Message> Messages { get; set; }

        public async Task<object> CheckForNull(string[]? userIds = null,
                                               string? chatId = null,
                                               string[]? memberIds = null,
                                               string[]? messageIds = null)
        {
            if(userIds != null)
            {
                foreach(var userId in userIds)
                {
                    if(!await Users.AnyAsync(user => user.Id == userId))
                    {
                        return Constants.ErrorFactory(Constants.UserNotFoundError, userId);
                    }
                }
            }

            if (chatId != null)
            {
                if (!await Chats.AnyAsync(chat => chat.Id == chatId))
                {
                    return Constants.ErrorFactory(Constants.UserNotFoundError, chatId);
                }
            }

            if (memberIds != null && chatId != null)
            {
                foreach (var memberId in memberIds)
                {
                    if (!await ChatMembers.AnyAsync(mem => mem.UserId == memberId && mem.ChatId == chatId))
                    {
                        return Constants.ErrorFactory(Constants.UserNotFoundError, memberId);
                    }
                }
            }

            if (messageIds != null)
            {
                foreach (var messageId in messageIds)
                {
                    if (!await Messages.AnyAsync(mes => mes.Id == messageId))
                    {
                        return Constants.ErrorFactory(Constants.UserNotFoundError, messageId);
                    }
                }
            }

            return null;
        }

        public string GetUserId(ClaimsPrincipal user) => user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasMany(user => user.ChatMember)
                                  .WithOne(mem => mem.User)
                                  .HasForeignKey(mem => mem.UserId)
                                  .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<User>().HasMany(mem => mem.Messages)
                                  .WithOne(mes => mes.Author)
                                  .HasForeignKey(mem => mem.AuthorId)
                                  .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Chat>().HasMany(chat => chat.Members)
                                  .WithOne(mem => mem.Chat)
                                  .HasForeignKey(mem => mem.ChatId)
                                  .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Chat>().HasMany(chat => chat.MessageStory)
                                  .WithOne(mes => mes.Chat)
                                  .HasForeignKey(mes => mes.ChatId)
                                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
