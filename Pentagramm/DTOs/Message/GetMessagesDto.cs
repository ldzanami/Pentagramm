namespace Pentagramm.DTOs.Message
{
    public class GetMessagesDto
    {
        public string ChatId { get; set; }
        public List<GetMessageDto> Messages { get; set; }
    }
}
