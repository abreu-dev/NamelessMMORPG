using Autofac;
using Nameless.Core.Infra.Data.Contexts;
using Nameless.Infra.DbContext.Contexts;
using Nameless.Infra.DbContext.Factory;
using Nameless.Infra.Providers.InMemory;
using Nameless.Security.Domain.Repositories;
using Nameless.Security.Repository.Repositories;

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
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().SingleInstance();
        }
    }
}
