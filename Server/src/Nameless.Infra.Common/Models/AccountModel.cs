using Nameless.Infra.Resources;

namespace Nameless.Infra.Common.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Language Language { get; set; }

        public virtual ICollection<PlayerModel> Players { get; set; }

        public AccountModel()
        {
            Players = new HashSet<PlayerModel>();
        }
    }
}
