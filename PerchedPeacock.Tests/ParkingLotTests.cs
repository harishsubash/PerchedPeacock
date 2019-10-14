using NUnit.Framework;
using PerchedPeacock.Domain;
using System;
using System.Globalization;
using System.Linq;

namespace PerchedPeacock.Tests
{
    public class ParkingLotTests
    {
        private ParkingLot _parkingLot;

        [Test]
        public void create_parkinglot_contains_allocated_lots()
        {
            var address = new Address("ITPL", "Bangalore", "India", "560048");
            _parkingLot = new ParkingLot(Guid.NewGuid(), "ParkingLot1", address, 5, 15, 120);
            Assert.AreEqual(_parkingLot.NumberOfSlots, _parkingLot.ParkingSlots.Count);
        }

        [Test]
        public void book_parkinglot_slot_and_generate_payslip()
        {
            int slotNumber = 7;
            string vehicleNumer = "BED5334";
            var address = new Address("ITPL", "Bangalore", "India", "560048");
            _parkingLot = new ParkingLot(Guid.NewGuid(), "ParkingLot1", address, 7, 10, 100);

            var parkingSlot = _parkingLot.ParkingSlots.First(x => x.SlotNumber == slotNumber);
            _parkingLot.BookSlot(parkingSlot.Id.Value, vehicleNumer);

            Assert.IsTrue(parkingSlot.isOccupied);
            Assert.IsTrue(_parkingLot.ParkingSlips.Count == 1);
        }

        [Test]
        public void release_parkinglot_slot_and_generate_amount()
        {
            int slotNumber = 7;
            string vehicleNumer = "BED5334";

            var address = new Address("ITPL", "Bangalore", "India", "560048");
            _parkingLot = new ParkingLot(Guid.NewGuid(), "ParkingLot1", address, 7, 10, 100);

            var parkingSlot = _parkingLot.ParkingSlots.First(x => x.SlotNumber == slotNumber);
            _parkingLot.BookSlot(parkingSlot.Id.Value, vehicleNumer);

            var parkingSlip = _parkingLot.ParkingSlips.FirstOrDefault();
            parkingSlip.StartDateTime = DateTime.Now.Subtract(TimeSpan.FromHours(3));
            _parkingLot.ReleaseSlot(parkingSlot.Id.Value, vehicleNumer);

            Assert.IsTrue(!parkingSlot.isOccupied);
            Assert.AreEqual(parkingSlip.ParkingCharge, 40);
        }
    }
}