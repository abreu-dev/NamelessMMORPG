using Microsoft.EntityFrameworkCore;
using Nameless.Core.Infra.Data.Contexts;
using System.Reflection;
using EntityFrameworkCoreContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Nameless.Infra.DbContext.Contexts
{
    public class NamelessContext : EntityFrameworkCoreContext, INamelessContext
    {
        public NamelessContext(DbContextOptions<NamelessContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
