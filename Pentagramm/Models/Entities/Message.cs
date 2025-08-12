namespace Pentagramm.Models.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string? Text { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string? ChatId { get; set; }
        public bool IsReaded { get; set; } = false;
        public Chat? Chat { get; set; }
        public User? Author { get; set; }
    }
}
