using Autofac;
using Nameless.Infra.DbContext.Contexts;
using Nameless.Infra.DbContext.Factory;
using Nameless.Infra.Providers.InMemory;

namespace Nameless.WebApi.Scope.Modules
{
    public class DatabaseModule : Module
    {
        private readonly IConfiguration _configuration;

        public DatabaseModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            AddDatabases(builder);
        }

        private void AddDatabases(ContainerBuilder builder)
        {
            var options = DbContextFactory.GetInstance()
                .UseInMemory(_configuration.GetConnectionString("DefaultConnection") ?? string.Empty);

            builder.RegisterType<NamelessContext>()
                .WithParameter("Options", options)
                .InstancePerLifetimeScope();

            builder.RegisterInstance(options).SingleInstance();
        }
    }
}
