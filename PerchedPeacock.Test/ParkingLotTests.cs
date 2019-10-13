using NUnit.Framework;
using PerchedPeacock.Domain;
using System;

namespace PerchedPeacock.Test
{
    public class ParkingLotTests
    {
        private ParkingLot _parkingLot;

        [Test]
        public void create_parkinglot_contains_allocated_lots()
        {
            _parkingLot = new ParkingLot(Guid.NewGuid(), "ParkingLot1", "ITPL, Bangalore", 5);
            Assert.AreEqual(_parkingLot.NumberOfSlots, _parkingLot.ParkingSlots.Count);
        }
    }
}