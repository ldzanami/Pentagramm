using Pentagramm.Infrastructure;
using Pentagramm.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Pentagramm.DTOs.Auth
{
    public class RegisterDto
    {
        [PentaPhone]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Role { get; set; }
    }
}
