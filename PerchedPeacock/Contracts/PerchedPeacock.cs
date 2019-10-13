using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerchedPeacock.Contracts
{
    public static class PerchedPeacockParking
    {
        public static class V1
        {
            public class CreateParking
            {
                public string Name { get; set; }
                public string Address { get; set; }
                public int DailyRate { get; set; }
                public int NoofSlot { get; set; }
            }

            public class UpdateSlot
            {
                public Guid ParkingLotId { get; set; }
                public Guid ParkingSlotId { get; set; }
                public string VehicleNumber { get; set; }
            }

            public class RequestInfo
            {
                public Guid Id { get; set; }
            }

            public class ParkingLotInfo
            {
                public Guid ParkingLotId { get; set; }
                public string Name { get; set; }
                public string Address { get; set; }
                public string DailyParkingRate { get; set; }
                public int AvailableSlots { get; set; }
                public List<ParkingSlotInfo> ParkingSlotsInfo { get; set; }
            }

            public class ParkingSlotInfo
            {
                public Guid ParkingSlotId { get; set; }
                public int SlotNumber { get; set; }
                public bool isOccupied { get; set; }
            }
            
            public class ParkingLotsInfo
            {
                public IEnumerable<ParkingLotInfo> ParkingLots { get; set; }
            }

            public enum HttpReturnAction
            {
                HttpOk,
                HttpCreate,
            }

            public class ParkingResponse
            {
                public Guid ParkingLotId { get; set; }
            }
        }
    }
}
