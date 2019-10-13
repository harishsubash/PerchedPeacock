using PerchedPeacock.Core;
using System;

namespace PerchedPeacock.Domain
{
    public class ParkingLotId : Value<ParkingLotId>
    {
        public Guid Id { get; internal set; }

        protected ParkingLotId() { }

        public ParkingLotId(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id), "Parking lot id cannot be empty");

            Id = id;
        }

        public static implicit operator Guid(ParkingLotId self) => self.Id;

        public static implicit operator ParkingLotId(string value)
            => new ParkingLotId(Guid.Parse(value));

        public override string ToString() => Id.ToString();
    }
}
