using PerchedPeacock.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerchedPeacock.Domain.Interfaces.Repositories
{
    public interface IParkingLotRepository
    {
        Task<IEnumerable<ParkingLot>> Load();
        Task<ParkingLot> Load(Guid id);
        Task Add(ParkingLot entity);
        Task<bool> Exists(long id);
        Task<bool> Exists(string name);
    }
}
