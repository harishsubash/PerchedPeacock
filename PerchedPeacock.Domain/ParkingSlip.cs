using System;

namespace PerchedPeacock.Domain
{
    public class ParkingSlip
    {
        public long ReferenceId { get; set; }
        public long ParkingSlotId { get; set; }
        public long ParkingLotId { get; set; }
        public ParkingSlotType ParkingType { get; set; }
        public double ParkingCharges { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
