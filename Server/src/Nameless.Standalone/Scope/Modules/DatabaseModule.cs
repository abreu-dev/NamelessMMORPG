using Autofac;
using Nameless.Infra.Common.Contexts;
using Nameless.Infra.Common.Interfaces;
using Nameless.Infra.DbContext;
using Nameless.Infra.DbContext.Factory;
using Nameless.Infra.Providers.InMemory;
using Nameless.Infra.Repositories;

namespace Nameless.Standalone.Scope.Modules
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
            AddRepositories(builder);
        }

        private void AddDatabases(ContainerBuilder builder)
        {
            var options = DbContextFactory.GetInstance()
                .UseInMemory(_configuration.GetConnectionString("DefaultConnection") ?? string.Empty);

            builder.RegisterType<NamelessContext>()
                .WithParameter("Options", options)
                .As<INamelessContext>()
                .InstancePerLifetimeScope();

            builder.RegisterInstance(options).SingleInstance();
        }

        private void AddRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>().SingleInstance();
        }
    }
}
