using Nameless.Infra.Common.Models;

namespace Nameless.Infra.Common.Interfaces
{
    public interface IPlayerRepository : IBaseRepository<PlayerModel>
    {
        List<PlayerModel> GetPlayersByAccount(Guid accountId);
    }
}
