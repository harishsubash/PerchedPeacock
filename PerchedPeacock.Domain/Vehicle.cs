using PerchedPeacock.Core;

namespace PerchedPeacock.Domain
{
    public class Vehicle: Value<Vehicle>
    {
        public string Number { get; private set; }

        public Vehicle(string number)
        {
            Number = number;
        }
    }
}
