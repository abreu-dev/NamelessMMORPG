using Microsoft.AspNetCore.Mvc.Filters;

namespace Nameless.WebApi.Scope.Handlers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IgnoreAuthenticationTokenFilterAttribute : ActionFilterAttribute { }
}
