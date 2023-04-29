namespace Nameless.Core.Domain.Security.Interfaces
{
    public interface ISessionService
    {
        IAuthenticatedAccount? Account { get; }

        bool IsAuthenticated();

        void Authenticate(IAuthenticatedAccount account);
    }
}
