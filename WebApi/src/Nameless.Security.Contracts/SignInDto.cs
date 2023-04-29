using System.ComponentModel.DataAnnotations;

namespace Nameless.Security.Contracts
{
    public class SignInDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
