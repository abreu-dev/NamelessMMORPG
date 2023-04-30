using Microsoft.EntityFrameworkCore;
using Nameless.Data.Contexts;
using Nameless.Data.Factory;

namespace Nameless.Data.Providers.InMemory
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
