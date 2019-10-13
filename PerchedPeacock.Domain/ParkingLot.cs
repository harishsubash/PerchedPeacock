using PerchedPeacock.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerchedPeacock.Domain
{
    public partial class ParkingLot : AggregateRoot<ParkingLotId>
    {
        protected ParkingLot() { }

        public Guid ParkingLotId { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public int NumberOfSlots { get; private set; }
        public int HourlyRate { get; private set; }
        public int DailyRate { get; private set; }
        public List<ParkingSlot> ParkingSlots { get; set; }
        public List<ParkingSlip> ParkingSlips { get; set; }

        public ParkingLot(Guid parkingLotId, string name, string address
            , int numberOfSlots, int hourlyRate, int dailyRate)
        {
            ParkingSlots = new List<ParkingSlot>();
            ParkingSlips = new List<ParkingSlip>();
            Apply(new Events.CreateParkingLot
            {
                ParkingLotId = parkingLotId,
                Name = name,
                Address = address,
                NumberOfSlots = numberOfSlots,
                HourlyRate = hourlyRate,
                DailyRate = dailyRate,
            });
        }
        public void BookSlot(Guid parkingSlotId, string vehicleNumber)
        {
            Apply(new Events.BookParkingSlot
            {
                ParkingLotId = ParkingLotId,
                ParkingSlotId = parkingSlotId,
                VehicleNumber = vehicleNumber
            }); ;
        }
        public void ReleaseSlot(Guid parkingSlotId)
        {
            Apply(new Events.ReleaseParkingSlot
            {
                ParkingLotId = ParkingLotId,
                ParkingSlotId = parkingSlotId,
                HourlyRate = HourlyRate,
                DailyRate = DailyRate,
            });
        }

        protected override void EnsureValidState(object @event)
        {
            switch (@event)
            {
                case Events.CreateParkingLot e:
                    if (string.IsNullOrEmpty(Name)) throw new ArgumentOutOfRangeException("Name", "Name must be supplied");
                    if (string.IsNullOrEmpty(Address)) throw new ArgumentOutOfRangeException("Address", "Address must be supplied");
                    if (NumberOfSlots <= 0) throw new ArgumentOutOfRangeException("NumberOfSlots");
                    if (HourlyRate <= 0 ) throw new ArgumentOutOfRangeException("HourlyRate");
                    if (DailyRate <= 0 || HourlyRate > DailyRate) throw new ArgumentOutOfRangeException("DailyRate");
                    break;
            }
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.CreateParkingLot e:
                    ParkingLotId = e.ParkingLotId;
                    Name = e.Name;
                    Address = e.Address;
                    NumberOfSlots = e.NumberOfSlots;
                    Id = new ParkingLotId(e.ParkingLotId);
                    HourlyRate = e.HourlyRate;
                    DailyRate = e.DailyRate;
                    AddParkingSlot();
                    break;
                case Events.BookParkingSlot e:
                    UpdateParkingSlot(e.ParkingSlotId, e);
                    AddParkingSlip(e);
                    break;
                case Events.ReleaseParkingSlot e:
                    UpdateParkingSlot(e.ParkingSlotId, e);
                    UpdateParkingSlip(e);
                    break;
            }
        }

        private void AddParkingSlot()
        {
            for (int slotNumber = 1; slotNumber <= NumberOfSlots; slotNumber++)
            {
                var parkingslot = new ParkingSlot(Apply);
                ApplyToEntity(parkingslot,
                    new Events.CreateParkingSlot
                    {
                        ParkingSlotId = Guid.NewGuid(),
                        SlotNumber = slotNumber,
                        Id = ParkingLotId
                    });
                ParkingSlots.Add(parkingslot);
            }
        }

        private void AddParkingSlip(Events.BookParkingSlot e)
        {
            var createSlip = new ParkingSlip(Apply);
            e.ParkingSlipId = Guid.NewGuid();
            ApplyToEntity(createSlip, e);
            ParkingSlips.Add(createSlip);
        }

        private void UpdateParkingSlip(Events.ReleaseParkingSlot e)
        {
            var updateSlip = FindParkingSlip(e.ParkingLotId, e.ParkingSlotId);
            ApplyToEntity(updateSlip, e);
        }

        private void UpdateParkingSlot(Guid parkingSlotId, object @event)
        {
            ParkingSlot parkingSlot = FindParkingSlot(parkingSlotId);
            if (parkingSlot == null)
                throw new InvalidOperationException("Cannot find slot");

            ApplyToEntity(parkingSlot, @event);
        }

        private ParkingSlot FindParkingSlot(Guid parkingSlotId)
            => ParkingSlots.FirstOrDefault(x => x.ParkingSlotId == parkingSlotId);

        private ParkingSlip FindParkingSlip(Guid parkingLotId, Guid parkingSlotId)
            => ParkingSlips.FirstOrDefault(x => x.ParkingLotId == parkingLotId && x.ParkingSlotId == parkingSlotId);
    }
}
