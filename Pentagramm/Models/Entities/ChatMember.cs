using Microsoft.EntityFrameworkCore;
using Pentagramm.Infrastructure;

namespace Pentagramm.Models.Entities
{
    [PrimaryKey(nameof(UserId), nameof(ChatId))]
    public class ChatMember
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ChatId { get; set; }
        public string Role { get; set; }
        public User? User { get; set; }
        public Chat? Chat { get; set; }
    }
}
