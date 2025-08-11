using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pentagramm.Models.Entities;

namespace Pentagramm.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMember> ChatMember { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasMany(user => user.ChatMember)
                                  .WithOne(mem => mem.User)
                                  .HasForeignKey(mem => mem.UserId)
                                  .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<User>().HasMany(user => user.Notifications)
                                  .WithOne(not => not.User)
                                  .HasForeignKey(not => not.UserId)
                                  .OnDelete(DeleteBehavior.Cascade);

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
