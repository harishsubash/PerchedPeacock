using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PerchedPeacock.Domain;
using PerchedPeacock.Domain.Interfaces.Repositories;
using PerchedPeacock.Infra.Persistanace.EF;

namespace PerchedPeacock.Infra.Persistance.Repositories
{
    public class ParkingLotRepository : IParkingLotRepository, IDisposable
    {
        private readonly ParkingLotContext _dbContext;

        public ParkingLotRepository(ParkingLotContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Add(ParkingLot entity)
        => _dbContext.ParkingLot.AddAsync(entity);

        public async Task<bool> Exists(long id)
            => await _dbContext.ParkingLot.FindAsync(id) != null;
        public async Task<bool> Exists(string name)
            => await _dbContext.ParkingLot.AnyAsync(a => a.Name.ToLower().Equals(name.ToLower()));

        public async Task<IEnumerable<ParkingLot>> Load()
        => await _dbContext.ParkingLot.Include(x => x.ParkingSlots).ToListAsync();

        public async Task<ParkingLot> Load(Guid id)
            => await _dbContext.ParkingLot.Include(x => x.ParkingSlots).FirstOrDefaultAsync(a=> a.ParkingLotId == id);
        public void Dispose() => _dbContext.Dispose();
    }
}
