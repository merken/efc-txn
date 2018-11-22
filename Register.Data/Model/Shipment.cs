using System;

namespace Register.Data.Model
{
    public class Address
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    public class Shipment
    {
        public enum ShipmentType
        {
            WelcomePackage,
            OneYearAnniversary,
            FiveYearAnniversary,
            TenYearAnniversary
        }

        public int Id { get; private set; }
        public Address Address { get; set; }
        public ShipmentType Type { get; set; }
        public DateTime Date { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}