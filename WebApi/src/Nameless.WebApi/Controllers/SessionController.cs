using Microsoft.AspNetCore.Mvc;
using Nameless.Core.Domain.Extensions;
using Nameless.Security.Application.AuthServices.Interfaces;
using Nameless.Security.Contracts;
using Nameless.WebApi.Scope.Handlers;

namespace Nameless.WebApi.Controllers
{
    [ApiController]
    [Route("api/session")]
    [IgnoreAuthenticationTokenFilter]
    public class SessionController : ControllerBase
    {
        private readonly ISignInService _signInService;
        private readonly ITokenService _tokenService;

        public SessionController(ISignInService signInService,
                                 ITokenService tokenService)
        {
            _signInService = signInService;
            _tokenService = tokenService;
        }

        [HttpPost("sign-in")]
        public IActionResult SignIn([FromBody] SignInDto signInDto)
        {
            var result = _signInService.SignIn(signInDto);

            return Ok(new SignInResultDto()
            {
                Token = _tokenService.GenerateAuthenticationToken(result),
                Account = new AccountDto()
                {
                    Id = result.Id,
                    Username = result.Username,
                    Email = result.Email,
                    Language = result.Language.GetEnumDisplayDescription()
                }
            });
        }
    }
}
