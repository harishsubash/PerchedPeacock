﻿using System;
using System.Threading.Tasks;
using PerchedPeacock.Core;
using System.Linq;
using static PerchedPeacock.Contracts.PerchedPeacockParking.V1;
using PerchedPeacock.Domain.Interfaces.Repositories;
using PerchedPeacock.Domain;

namespace PerchedPeacock.Api
{
    public class ParkingLotApplicationService : IApplicationService
    {
        private readonly IParkingLotRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Guid ParkingLotId { get; private set; }

        public ParkingLotApplicationService(IParkingLotRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ParkingLotsInfo> GetParkingLots()
        {
            var parkingLots = await _repository.Load();
            return new ParkingLotsInfo
            {
                ParkingLots = parkingLots.Select(result => new ParkingLotInfo
                {
                    ParkingLotId = result.ParkingLotId,
                    Name = result.Name,
                    Address = result.Address,
                    AvailableSlots = result.ParkingSlots.Count(item => !item.isOccupied),
                    DailyParkingRate = "60",
                })
            };
        }

        public async Task<ParkingResponse> CreateParking(CreateParking request)
        {
            if (await _repository.Exists(request.Name))
                throw new InvalidOperationException(
                    $"Entity with name {request.Name} already exists"
                );

            var parkingLot = new ParkingLot(Guid.NewGuid(), request.Name, request.Address, request.NoofSlot);
            await _repository.Add(parkingLot);
            await _unitOfWork.Commit();

            return new ParkingResponse { ParkingLotId = parkingLot.ParkingLotId };
        }

        public async Task<ParkingLotInfo> Load(Guid parkingLotId)
        {
            var parkingLot = await _repository.Load(parkingLotId);

            if (parkingLot == null)
                throw new InvalidOperationException(
                    $"Entity with id {parkingLotId} cannot be found");
            else
            {
                var parkingSlotsInfo = 
                    parkingLot.ParkingSlots.Select(parkingSlot => new ParkingSlotInfo
                    {
                        ParkingSlotId = parkingSlot.ParkingSlotId,
                        isOccupied = parkingSlot.isOccupied,
                        SlotNumber = parkingSlot.SlotNumber
                    }).ToList();

                return new ParkingLotInfo
                {
                    ParkingLotId = parkingLot.ParkingLotId,
                    Name = parkingLot.Name,
                    Address = parkingLot.Address,
                    AvailableSlots = parkingLot.ParkingSlots.Count(item => !item.isOccupied),
                    ParkingSlotsInfo = parkingSlotsInfo,
                    DailyParkingRate = "60",
                };
            }
        }

        public Task Handle(object command)
        {
            throw new NotImplementedException();
        }
    }
}
