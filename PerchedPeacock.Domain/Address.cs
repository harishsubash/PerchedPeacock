using PerchedPeacock.Core;

namespace PerchedPeacock.Domain
{
    public class Address : Value<Address>
    {
        public string StreetAddress { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public Address(string streetAddress, string city, string country, string zipCode)
        {
            StreetAddress = streetAddress;
            City = city;
            Country = country;
            ZipCode = zipCode;
        }

        public Address(string city, string country)
        {
            City = city;
            Country = country;
        }

        public override string ToString()
          =>  $"{StreetAddress}, {City}, {Country} {ZipCode}";

    }
}
