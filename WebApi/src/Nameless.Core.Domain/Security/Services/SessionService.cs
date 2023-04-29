using Nameless.Core.Domain.Security.Interfaces;

namespace Nameless.Core.Domain.Security.Services
{
    public class SessionService : ISessionService
    {
        public IAuthenticatedAccount? Account { get; private set; }

        public bool IsAuthenticated()
        {
            return Account != null;
        }

        public void Authenticate(IAuthenticatedAccount account)
        {
            Account = account;
        }
    }
}
