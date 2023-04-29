using Microsoft.EntityFrameworkCore;
using Nameless.Core.Domain.Data;
using Nameless.Core.Infra.Data.Contexts;

namespace Nameless.Core.Infra.Data.Repositories
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
