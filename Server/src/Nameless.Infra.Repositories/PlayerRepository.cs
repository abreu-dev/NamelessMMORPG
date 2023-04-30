using Microsoft.EntityFrameworkCore;
using Nameless.Infra.Common.Contexts;
using Nameless.Infra.Common.Interfaces;
using Nameless.Infra.Common.Models;

namespace Nameless.Infra.Repositories
{
    public class PlayerRepository : BaseRepository<PlayerModel>, IPlayerRepository
    {
        public PlayerRepository(INamelessContext context) : base(context)
        {
        }

        public List<PlayerModel> GetPlayersByAccount(Guid accountId)
        {
            return _dbSet
                .AsNoTracking()
                .Where(x => x.AccountId == accountId).ToList();
        }
    }
}
