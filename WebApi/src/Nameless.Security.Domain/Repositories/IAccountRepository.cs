using Nameless.Core.Domain.Data;
using Nameless.Infra.DbEntities;

namespace Nameless.Security.Domain.Repositories
{
    public interface IAccountRepository : IBaseRepository<AccountModel>
    {
        AccountModel? Authenticate(string username, string password);
    }
}
