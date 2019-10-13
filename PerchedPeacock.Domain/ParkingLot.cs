using PerchedPeacock.Core;
using System;
using System.Collections.Generic;

namespace PerchedPeacock.Domain
{
    public partial class ParkingLot : AggregateRoot<ParkingLotId>
    {
        protected ParkingLot() { }

        public Guid ParkingLotId { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public int NumberOfSlots { get; private set; }
        public List<ParkingSlot> ParkingSlots { get; set; }

        public ParkingLot(Guid parkingLotId, string name, string address, int numberOfSlots)
        {
            ParkingSlots = new List<ParkingSlot>();
            Apply(new Events.CreateParkingLot
            {
                ParkingLotId = parkingLotId,
                Name = name,
                Address = address,
                NumberOfSlots = numberOfSlots,
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
                    AddParkingSlot();
                    break;
            }
        }

        public void AddParkingSlot()
        {
            for (int slotNumber = 1; slotNumber <= NumberOfSlots; slotNumber++)
            {
                var parkingslot = new ParkingSlot(Apply);
                ApplyToEntity(parkingslot,
                    new Events.CreateParkingSlot
                    {
                        ParkingSlotId = Guid.NewGuid(),
                        SlotNumber = slotNumber,
                        Id = Id
                    });
                ParkingSlots.Add(parkingslot);
            }
        }
    }
}
