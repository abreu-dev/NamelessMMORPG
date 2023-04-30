using Microsoft.EntityFrameworkCore;

namespace Nameless.Data.Contexts
{
    public interface INamelessContext
    {
        void EnsureCreated();

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    }
}
