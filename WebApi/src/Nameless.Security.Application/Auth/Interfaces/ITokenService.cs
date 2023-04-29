using Nameless.Infra.DbEntities;
using Nameless.Security.Application.AuthServices.Models;

namespace Nameless.Security.Application.AuthServices.Interfaces
{
    public interface ITokenService
    {
        string GenerateAuthenticationToken(AccountModel account);
        ValidatedToken ValidateToken(string? token);
    }
}
