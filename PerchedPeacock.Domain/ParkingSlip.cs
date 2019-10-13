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
        public DateTime? EndDateTime { get; set; }
        public int ParkingCharge { get; set; }
        public Vehicle Vehicle { get; set; }

        public ParkingSlip(Action<object> applier) : base(applier)
        {
        }


        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.BookParkingSlot e:
                    ParkingSlipId = e.ParkingSlipId;
                    ParkingSlotId = e.ParkingSlotId;
                    ParkingLotId = e.ParkingLotId;
                    Id = new ParkingSlipId(e.ParkingSlipId);
                    ParkingType = ParkingSlotType.FourWheeler;
                    StartDateTime = DateTime.Now;
                    EndDateTime = null;
                    Vehicle = new Vehicle(e.VehicleNumber);
                    break;
                case Events.ReleaseParkingSlot e:
                    EndDateTime = DateTime.Now;
                    ParkingCharge parkingcharge = new ParkingCharge(e.DailyRate, e.HourlyRate);
                    ParkingCharge = parkingcharge.CalculateCharge(StartDateTime, EndDateTime.Value);
                    break;
            }
        }

    }
}
