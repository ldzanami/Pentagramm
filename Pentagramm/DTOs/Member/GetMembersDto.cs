namespace Pentagramm.DTOs.Member
{
    public class GetMembersDto
    {
        public string ChatId { get; set; }
        public List<GetMemberDto> Members { get; set; }
    }
}
