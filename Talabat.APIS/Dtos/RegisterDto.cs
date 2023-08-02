using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DispalyName { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        
        public string Password { get; set; }
    }
}
