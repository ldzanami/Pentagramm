namespace Pentagramm.DTOs.Member
{
    public class GetMemberDto
    {
        public string UserId { get; set; }
        public string ChatId { get; set; }
        public string Role { get; set; }
        public DateTime JoinedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
