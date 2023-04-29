using Autofac;
using Autofac.Extensions.DependencyInjection;
using Nameless.Core.Infra.Data.Contexts;
using Nameless.WebApi.Scope.Extensions;
using Nameless.WebApi.Scope.Middlewares;
using Nameless.WebApi.Scope.Modules;
using Serilog;
using System.Diagnostics;

namespace Nameless.WebApi
{
    public partial class Program
    {
        public static void Main()
        {
            Console.Title = "Nameless.MMO WebApi";

            var sw = new Stopwatch();
            sw.Start();

            var builder = WebApplication.CreateBuilder();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddCustomControllers();

            builder.Services.AddCustomSwagger();

            builder.Host.UseSerilog((context, configuration)
                    => configuration.ReadFrom.Configuration(context.Configuration));

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterModule(new MiddlewareModule());
                container.RegisterModule(new ConfigurationModule(builder.Configuration));
                container.RegisterModule(new DatabaseModule(builder.Configuration));
                container.RegisterModule(new ServiceModule());
            });

            var app = builder.Build();

            app.Logger.LogInformation("Starting Nameless.MMO WebApi");

            var container = app.Services.GetAutofacRoot();

            LoadDatabase(container, app.Logger);

            if (app.Environment.IsDevelopment())
            {
                app.UseCustomSwagger();
            }

            app.MapControllers();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            sw.Stop();

            app.Logger.LogInformation("WebApi is {Up}! {Time} ms", "up", sw.ElapsedMilliseconds);

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