using System;
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
                ParkingLots = parkingLots.Select(parkingLot => new ParkingLotInfo
                {
                    ParkingLotId = parkingLot.ParkingLotId,
                    Name = parkingLot.Name,
                    Address = parkingLot.Address.ToString(),
                    AvailableSlots = parkingLot.ParkingSlots.Count(item => !item.isOccupied),
                    DailyParkingRate = parkingLot.DailyRate,
                    HourlyParkingRate = parkingLot.HourlyRate
                })
            };
        }

        public async Task<ParkingBookingsInfo> GetParkingBookings()
        {
            var parkingLots = await _repository.Load();

            return new ParkingBookingsInfo
            {
                ParkingBookings = (from parkinglot in parkingLots
                                   from parkingSlip in parkinglot.ParkingSlips
                                   from parkingSlot in parkinglot.ParkingSlots.Where(x => x.ParkingSlotId == parkingSlip.ParkingSlotId)
                                   select new ParkingBookingInfo
                                   {
                                       ParkingLotName = parkinglot.Name,
                                       StartDateTime = parkingSlip.StartDateTime,
                                       EndDateTime = parkingSlip.EndDateTime,
                                       ParkingCharge = parkingSlip.ParkingCharge,
                                       VehicleNumber = parkingSlip.Vehicle.Number,
                                       isActive = !parkingSlip.EndDateTime.HasValue,
                                       ParkingSlotNumber = parkingSlot.SlotNumber

                                   }).ToList()
            };
        }

        public async Task<ParkingLotsInfo> Load(RequestParkingInfo request)
        {
            var parkingLots = await _repository.Load();
            return new ParkingLotsInfo
            {
                ParkingLots = parkingLots.Where(parkingaddress => 
                    parkingaddress.Address.City.Equals(request.City)
                    && parkingaddress.Address.Country.Equals(request.Country))
                .Select(parkingLot => new ParkingLotInfo
                {
                    ParkingLotId = parkingLot.ParkingLotId,
                    Name = parkingLot.Name,
                    Address = parkingLot.Address.ToString(),
                    AvailableSlots = parkingLot.ParkingSlots.Count(item => !item.isOccupied),
                    DailyParkingRate = parkingLot.DailyRate,
                    HourlyParkingRate = parkingLot.HourlyRate
                })
            };
        }

        public async Task<BookingSlotInfo> BookParkingSlot(UpdateSlot request)
        {
            ParkingLot parkingLot = await _repository.Load(request.ParkingLotId);

            if (parkingLot == null)
                throw new InvalidOperationException(
                    $"Entity with id {request.ParkingLotId} cannot be found");

            if (request.ParkingSlotId == Guid.Empty)
                request.ParkingSlotId = parkingLot.GetNextAvailaleParkingSlot()?.ParkingSlotId ?? new Guid();

            parkingLot.BookSlot(request.ParkingSlotId, request.VehicleNumber);
            await _unitOfWork.Commit();

            var parkingSlot = parkingLot.FindParkingSlot(request.ParkingSlotId);
            var parkingSlip = parkingLot.FindParkingSlip(parkingSlot.ParkingSlipId);

            return new BookingSlotInfo
            {
                VehicleNumber = parkingSlip.Vehicle.Number,
                StartDateTime = parkingSlip.StartDateTime,
                SlotNumber = parkingSlot.SlotNumber
            };

        }

        public async Task<ReleaseSlotInfo> ReleaseParkingSlot(UpdateSlot request)
        {
            var parkingLot = await _repository.Load(request.ParkingLotId);

            if (parkingLot == null)
                throw new InvalidOperationException(
                    $"Entity with id {request.ParkingLotId} cannot be found");

            if (request.ParkingSlotId == Guid.Empty)
                request.ParkingSlotId = parkingLot.FindParkingSlipbyVehicleNumber
                    (request.VehicleNumber, request.ParkingLotId)?.ParkingSlotId ?? new Guid();

            var parkingSlipId = parkingLot.FindParkingSlot(request.ParkingSlotId).ParkingSlipId;
            parkingLot.ReleaseSlot(request.ParkingSlotId, request.VehicleNumber);
            await _unitOfWork.Commit();

            var parkingSlip = parkingLot.FindParkingSlip(parkingSlipId);

            return new ReleaseSlotInfo
            {
                VehicleNumber = parkingSlip.Vehicle.Number,
                StartDateTime = parkingSlip.StartDateTime,
                EndDateTime = parkingSlip.EndDateTime,
                ParkingCharge = parkingSlip.ParkingCharge
            };
        }

        public async Task<ParkingResponse> CreateParking(CreateParking request)
        {
            if (await _repository.Exists(request.Name))
                throw new InvalidOperationException(
                    $"Entity with name {request.Name} already exists"
                );
            
            var parkingLot = new ParkingLot(Guid.NewGuid(), request.Name
                , new Address(request.Address, request.City, request.Country, request.ZipCode)
                , request.NoofSlot, request.HourlyRate, request.DailyRate);
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
                    parkingLot.ParkingSlots.Select(parkingSlot =>new ParkingSlotInfo
                    {
                        ParkingSlotId = parkingSlot.ParkingSlotId,
                        isOccupied = parkingSlot.isOccupied,
                        SlotNumber = parkingSlot.SlotNumber,
                        VehicleNumber = parkingLot.FindParkingSlip(parkingSlot.ParkingSlipId)?.Vehicle?.Number
                    }).ToList();


                return new ParkingLotInfo
                {
                    ParkingLotId = parkingLot.ParkingLotId,
                    Name = parkingLot.Name,
                    Address = parkingLot.Address.ToString(),
                    AvailableSlots = parkingLot.ParkingSlots.Count(item => !item.isOccupied),
                    ParkingSlotsInfo = parkingSlotsInfo,
                    DailyParkingRate = parkingLot.DailyRate,
                    HourlyParkingRate = parkingLot.HourlyRate
                };
            }
        }

        public Task Handle(object command)
        {
            throw new NotImplementedException();
        }
    }
}
