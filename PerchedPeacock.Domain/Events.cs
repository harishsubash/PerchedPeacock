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
            public Guid Id { get; internal set; }
            public Guid ParkingSlotId { get; internal set; }
        }

        public class BookParkingSlot
        {
            public Guid Id { get; internal set; }
        }

        public class ReleaseParkingSlot
        {
            public Guid Id { get; internal set; }
        }
    }
}
