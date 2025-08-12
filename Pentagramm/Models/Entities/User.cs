using Microsoft.AspNetCore.Identity;
using Pentagramm.Infrastructure;

namespace Pentagramm.Models.Entities
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Role { get; set; }
        public List<ChatMember> ChatMember { get; set; } = [];
        public List<Message> Messages { get; set; } = [];
    }
}
