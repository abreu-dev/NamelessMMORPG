using Microsoft.EntityFrameworkCore;
using Nameless.Core.Infra.Data.Contexts;
using Nameless.Core.Infra.Data.Repositories;
using Nameless.Infra.DbEntities;
using Nameless.Security.Domain.Repositories;

namespace Nameless.Security.Repository.Repositories
{
    public class AccountRepository : BaseRepository<AccountModel>, IAccountRepository
    {
        public AccountRepository(INamelessContext dbContext)
            : base(dbContext)
        {
        }

        public AccountModel? Authenticate(string username, string password)
        {
            return _dbSet
                .AsNoTracking()
                .SingleOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}
