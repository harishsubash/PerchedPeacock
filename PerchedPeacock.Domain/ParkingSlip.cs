using PerchedPeacock.Core;
using System;

namespace PerchedPeacock.Domain
{
    public class ParkingSlip : Entity<ParkingSlipId>
    {
        protected ParkingSlip() { }
        public Guid ParkingSlipId { get; set; }

        //EntityState
        public Guid ParkingSlotId { get; private set; }
        public Guid ParkingLotId { get; private set; }
        public ParkingSlotType ParkingType { get; private set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double ParkingCharges { get; set; }

        public ParkingSlip(Action<object> applier) : base(applier)
        {
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.BookParkingSlot e:
                    break;
                case Events.ReleaseParkingSlot e:
                    break;
            }
        }

    }
}
