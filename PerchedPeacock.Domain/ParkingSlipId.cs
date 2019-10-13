using PerchedPeacock.Core;
using System;

namespace PerchedPeacock.Domain
{
    public class ParkingSlipId : Value<ParkingSlipId>
    {
        public ParkingSlipId(Guid value) => Value = value;

        public Guid Value { get; }

        protected ParkingSlipId() { }
    }
}
