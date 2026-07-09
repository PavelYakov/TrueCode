using System.ComponentModel.DataAnnotations;

namespace UserService.API.Models.RequestModels.Authentication
{
    public class RegisterRequest
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;


        [Required]
        [MinLength(4)]
        public string Password { get; set; } = string.Empty;
    }
}
