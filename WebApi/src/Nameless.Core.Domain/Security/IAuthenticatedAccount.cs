namespace Nameless.Core.Domain.Security
{
    public interface IAuthenticatedAccount
    {
        Guid Id { get; }
        string Email { get; }
        string Username { get; }
        string Language { get; }
    }
}
