using System;

namespace PerchedPeacock.Domain
{
    public static class Events
    {
        public class CreateParkingLot
        {
            public Guid ParkingLotId { get; internal set; }
            public string Name { get; internal set; }
            public string Address { get; internal set; }
            public int NumberOfSlots { get; internal set; }
        }
        public class CreateParkingSlot
        {
            public int SlotNumber { get; internal set; }
            public ParkingLotId Id { get; internal set; }
            public Guid ParkingSlotId { get; internal set; }
        }
    }
}
