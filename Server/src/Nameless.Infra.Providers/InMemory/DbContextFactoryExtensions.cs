using Microsoft.EntityFrameworkCore;
using Nameless.Infra.DbContext;
using Nameless.Infra.DbContext.Factory;

namespace Nameless.Infra.Providers.InMemory
{
    public static class DbContextFactoryExtensions
    {
        public static DbContextOptions<NamelessContext> UseInMemory(this DbContextFactory factory, string name)
        {
            var builder = new DbContextOptionsBuilder<NamelessContext>();
            builder.UseInMemoryDatabase(name);

            return builder.Options;
        }
    }
}
