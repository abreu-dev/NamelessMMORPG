using Nameless.Infra.Resources;

namespace Nameless.Infra.DbEntities
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Language Language { get; set; }
    }
}
