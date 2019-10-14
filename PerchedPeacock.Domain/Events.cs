using System;

namespace PerchedPeacock.Domain
{
    public static class Events
    {
        public class CreateParkingLot
        {
            public Guid ParkingLotId { get; internal set; }
            public string Name { get; internal set; }
            public Address Address { get; internal set; }
            public int NumberOfSlots { get; internal set; }
            public int HourlyRate { get; internal set; }
            public int DailyRate { get; internal set; }
        }
        public class CreateParkingSlot
        {
            public int SlotNumber { get; internal set; }
            public Guid Id { get; internal set; }
            public Guid ParkingSlotId { get; internal set; }
        }

        public class BookParkingSlot
        {
            public Guid ParkingSlipId { get; set; }
            public Guid ParkingSlotId { get; set; }
            public Guid ParkingLotId { get; set; }
            public string VehicleNumber { get; set; }
        }

        public class ReleaseParkingSlot
        {
            public Guid ParkingSlipId { get; set; }
            public Guid ParkingSlotId { get; set; }
            public Guid ParkingLotId { get; set; }
            public int HourlyRate { get; internal set; }
            public int DailyRate { get; internal set; }
            public string VehicleNumber { get; internal set; }
        }
    }
}
