using Pentagramm.Infrastructure;

namespace Pentagramm.Models.Entities
{
    public class ChatMember
    {
        public string UserId { get; set; }
        public string ChatId { get; set; }
        public ChatRolesEnum Role { get; set; }
        public User? User { get; set; }
        public Chat? Chat { get; set; }
    }
}
