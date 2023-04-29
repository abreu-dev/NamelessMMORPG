using Autofac;
using Nameless.Security.Application.Auth.Configuration;

namespace Nameless.WebApi.Scope.Modules
{
    public class ConfigurationModule : Module
    {
        private readonly IConfiguration _configuration;

        public ConfigurationModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            JwtConfiguration jwtConfiguration = new(string.Empty, 0);

            _configuration.GetSection("JwtConfiguration").Bind(jwtConfiguration);

            builder.RegisterInstance(jwtConfiguration).SingleInstance();
        }
    }
}
