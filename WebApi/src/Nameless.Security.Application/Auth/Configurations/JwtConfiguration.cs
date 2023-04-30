namespace Nameless.Security.Application.Auth.Configuration
{
    public record JwtConfiguration(string Secret, int ExpiresInHours);
}
