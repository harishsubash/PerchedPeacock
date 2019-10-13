using NUnit.Framework;
using PerchedPeacock.Domain;
using System;
using System.Linq;

namespace PerchedPeacock.Tests
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

        [Test]
        public void book_parkinglot_slot()
        {
            _parkingLot = new ParkingLot(Guid.NewGuid(), "ParkingLot1", "ITPL, Bangalore", 7);
            int slotNumber = 7;
            var parkingslot = _parkingLot.ParkingSlots.First(x => x.SlotNumber == slotNumber);
            _parkingLot.BookParkingSlot(parkingslot.Id.Value);
            Assert.IsTrue(parkingslot.isOccupied);
        }

        [Test]
        public void release_parkinglot_slot()
        {
            _parkingLot = new ParkingLot(Guid.NewGuid(), "ParkingLot1", "ITPL, Bangalore", 7);
            int slotNumber = 7;
            var parkingslot = _parkingLot.ParkingSlots.First(x => x.SlotNumber == slotNumber);
            _parkingLot.ReleaseParkingSlot(parkingslot.Id.Value);
            Assert.IsTrue(!parkingslot.isOccupied);
        }
    }
}