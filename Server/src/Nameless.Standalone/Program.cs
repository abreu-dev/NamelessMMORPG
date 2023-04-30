using Autofac;
using Autofac.Extensions.DependencyInjection;
using Nameless.Infra.Common.Contexts;
using Nameless.Networking.Hubs;
using Nameless.Standalone.Scope.Extensions;
using Nameless.Standalone.Scope.Modules;
using Serilog;
using System.Diagnostics;

namespace Nameless.Standalone
{
    public partial class Program
    {
        public static void Main()
        {
            Console.Title = "Nameless.MMORPG Server";

            var sw = new Stopwatch();
            sw.Start();

            var builder = WebApplication.CreateBuilder();

            builder.Host.UseSerilog((context, configuration)
                => configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddSignalR();

            builder.Services.AddCustomAuthentication(builder.Configuration);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterModule(new DatabaseModule(builder.Configuration));
            });

            var app = builder.Build();

            app.Logger.LogInformation("Welcome to Nameless.MMORPG Server!");

            var container = app.Services.GetAutofacRoot();

            LoadDatabase(container, app.Logger);

            app.UseCustomAuthentication();

            app.MapHub<GameHub>("/GameHub");

            sw.Stop();

            app.Logger.LogInformation("Server is {Up}! {Time} ms", "up", sw.ElapsedMilliseconds);

            app.Run();
        }

        private static void LoadDatabase(IComponentContext container,
                                         Microsoft.Extensions.Logging.ILogger logger)
        {
            var context = container.Resolve<INamelessContext>();

            logger.LogInformation("Loading database");

            try
            {
                context.EnsureCreated();
            }
            catch
            {
                logger.LogError("Unable to connect to database");
            }

            logger.LogInformation("Database loaded");
        }
    }
}