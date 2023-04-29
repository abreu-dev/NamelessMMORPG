using Nameless.Core.Domain.Security;

namespace Nameless.Security.Application.AuthServices.Models
{
    public class AuthenticatedAccount : IAuthenticatedAccount
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Language { get; set; }
    }
}
