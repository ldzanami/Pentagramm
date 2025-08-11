using Microsoft.AspNetCore.Identity;

namespace Pentagramm.Models.Entities
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<ChatMember> ChatMember { get; set; } = [];
        public List<Notification> Notifications { get; set; } = [];
        public List<Message> Messages { get; set; } = [];
    }
}
