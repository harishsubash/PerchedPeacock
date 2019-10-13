using PerchedPeacock.Core;
using System;

namespace PerchedPeacock.Domain
{
    public class ParkingSlot : Entity<ParkingSlotId>
    {
        protected ParkingSlot() { }
        public Guid ParkingSlotId { get; set; }
        // Entity state
        public Guid ParentId { get; private set; }
        public int SlotNumber { get; private set; }
        public ParkingSlotType ParkingType { get; set; }
        public bool isOccupied { get; set; }

        public ParkingSlot(Action<object> applier) : base(applier)
        {
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.CreateParkingSlot e:
                    Id = new ParkingSlotId(e.ParkingSlotId);
                    ParkingSlotId = e.ParkingSlotId;
                    ParentId = e.Id;
                    SlotNumber = e.SlotNumber;
                    ParkingType = ParkingSlotType.FourWheeler;
                    isOccupied = false;
                    break;
                case Events.BookParkingSlot e:
                    isOccupied = true;
                    break;
                case Events.ReleaseParkingSlot e:
                    isOccupied = false;
                    break;
            }
        }
    }
}
