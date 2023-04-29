using Nameless.Infra.DbEntities;
using Nameless.Security.Contracts;

namespace Nameless.Security.Application.AuthServices.Interfaces
{
    public interface ISignInService
    {
        AccountModel SignIn(SignInDto signInDto);
    }
}
