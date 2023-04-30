using Microsoft.EntityFrameworkCore;
using Nameless.Infra.Common.Contexts;
using Nameless.Infra.Common.Interfaces;

namespace Nameless.Infra.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly INamelessContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(INamelessContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.GetDbSet<TEntity>();
        }
    }
}
