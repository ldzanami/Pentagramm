namespace Pentagramm.Models.Entities
{
    public class Chat
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public List<ChatMember> Members { get; set; } = [];
        public List<Message> MessageStory { get; set; } = [];
    }
}
