using Autofac;
using Nameless.Core.Domain.Security.Interfaces;
using Nameless.Core.Domain.Security.Services;
using Nameless.Security.Application.Auth.Services;
using Nameless.Security.Application.AuthServices.Interfaces;

namespace Nameless.WebApi.Scope.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            AddServices(builder);
        }

        private void AddServices(ContainerBuilder builder)
        {
            //Auth
            builder.RegisterType<SessionService>().As<ISessionService>().SingleInstance();
            builder.RegisterType<SignInService>().As<ISignInService>().SingleInstance();
            builder.RegisterType<TokenService>().As<ITokenService>().SingleInstance();
        }
    }
}
