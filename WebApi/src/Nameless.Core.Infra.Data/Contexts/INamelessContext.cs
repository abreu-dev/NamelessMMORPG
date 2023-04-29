using Microsoft.EntityFrameworkCore;

namespace Nameless.Core.Infra.Data.Contexts
{
    public interface INamelessContext
    {
        void EnsureCreated();

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    }
}
