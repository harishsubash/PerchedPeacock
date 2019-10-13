using PerchedPeacock.Core;
using System;

namespace PerchedPeacock.Domain
{
    public class ParkingSlot : Entity<ParkingSlotId>
    {
        protected ParkingSlot() { }
        public Guid ParkingSlotId { get; private set; }
        // Entity state
        public ParkingLotId ParentId { get; private set; }
        public int SlotNumber { get; private set; }
        public ParkingSlotType ParkingType { get; set; }
        public bool isOccupied { get; set; }

        public ParkingSlot(Action<object> applier) : base(applier)
        {
        }
        //public User User { get; set; }
        //public ParkingSlip ParkingSlip { get; set; }

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
            }
        }
    }
}
