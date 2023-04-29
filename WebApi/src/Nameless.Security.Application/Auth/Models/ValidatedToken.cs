namespace Nameless.Security.Application.AuthServices.Models
{
    public class ValidatedToken
    {
        public bool IsValid { get; set; }
        public AuthenticatedAccount Account { get; set; }
    }
}
