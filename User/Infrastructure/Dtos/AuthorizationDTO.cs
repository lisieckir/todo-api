using System.ComponentModel.DataAnnotations;

namespace TutorialRestApi.User.Infrastructure.Dtos
{
    public class AuthorizationDTO
    {
        [Required(AllowEmptyStrings = false)]

        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; } 
    }
}
