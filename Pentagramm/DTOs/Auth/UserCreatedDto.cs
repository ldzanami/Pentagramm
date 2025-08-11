using Pentagramm.Infrastructure;

namespace Pentagramm.DTOs.Auth
{
    public class UserCreatedDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}
