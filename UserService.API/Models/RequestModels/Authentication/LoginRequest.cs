using System.ComponentModel.DataAnnotations;

namespace UserService.API.Models.RequestModels.Authentication
{
    public class LoginRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;


        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
