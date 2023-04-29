using Autofac;
using Nameless.WebApi.Scope.Middlewares;

namespace Nameless.WebApi.Scope.Modules
{
    public class MiddlewareModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExceptionHandlingMiddleware>().SingleInstance();
        }
    }
}
