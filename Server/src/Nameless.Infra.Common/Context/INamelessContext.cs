using Microsoft.EntityFrameworkCore;

namespace Nameless.Infra.Common.Contexts
{
    public interface INamelessContext
    {
        void EnsureCreated();

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    }
}
