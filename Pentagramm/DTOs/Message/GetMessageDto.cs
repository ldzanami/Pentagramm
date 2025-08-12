namespace Pentagramm.DTOs.Message
{
    public class GetMessageDto
    {
        public string Id { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsReaded { get; set; }
        public string? Text { get; set; }
    }
}
