using PerchedPeacock.Core;
using PerchedPeacock.Infra.Persistanace.EF;
using System.Threading.Tasks;

namespace PerchedPeacock.Infra.Transaction
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly ParkingLotContext _dbContext;

        public EfCoreUnitOfWork(ParkingLotContext dbContext)
            => _dbContext = dbContext;

        public Task Commit() => _dbContext.SaveChangesAsync();
    }
}
