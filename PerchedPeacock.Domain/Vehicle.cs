using PerchedPeacock.Core;

namespace PerchedPeacock.Domain
{
    public class Vehicle: Value<Vehicle>
    {
        public string Number { get; set; }

        public string Model { get; set; }
    }
}
