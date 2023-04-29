using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nameless.Core.Domain.Security.Interfaces;
using Nameless.Security.Application.AuthServices.Interfaces;
using System.Globalization;

namespace Nameless.WebApi.Scope.Handlers
{
    public class AuthenticationTokenFilterAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private const string DefaultLanguage = "EN";

        private readonly ITokenService _tokenService;
        private readonly ISessionService _sessionService;

        public AuthenticationTokenFilterAttribute(ITokenService tokenService, 
                                                  ISessionService sessionService)
        {
            _tokenService = tokenService;
            _sessionService = sessionService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (HasFilter(context, typeof(IgnoreAuthenticationTokenFilterAttribute)))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(DefaultLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(DefaultLanguage);
                return;
            };

            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var tokenData = _tokenService.ValidateToken(token);

            if (tokenData.IsValid)
            {
                _sessionService.Authenticate(tokenData.Account);

                Thread.CurrentThread.CurrentCulture = new CultureInfo(tokenData.Account.Language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(tokenData.Account.Language);

                return;
            }

            context.Result = new UnauthorizedObjectResult(null);
        }

        private static bool HasFilter(AuthorizationFilterContext context, Type tokenFilter)
        {
            return context.ActionDescriptor.FilterDescriptors.Any(x => x.Filter.GetType() == tokenFilter);
        }
    }
}
