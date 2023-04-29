using Nameless.Infra.DbEntities;
using Nameless.Security.Application.AuthServices.Interfaces;
using Nameless.Security.Contracts;
using Nameless.Security.Domain.Exceptions;
using Nameless.Security.Domain.Repositories;

namespace Nameless.Security.Application.Auth.Services
{
    public class SignInService : ISignInService
    {
        private readonly IAccountRepository _accountRepository;

        public SignInService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountModel SignIn(SignInDto signInDto)
        {
            var user = _accountRepository.Authenticate(signInDto.Username, signInDto.Password);

            if (user == null)
                throw new SignInFailedException();

            return user;
        }
    }
}
