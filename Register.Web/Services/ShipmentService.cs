using System;
using System.Threading.Tasks;
using Register.Data.Model;
using Register.Web.Controllers;

namespace Register.Web.Services
{
    public class ShipmentService : IRegisterService
    {
        private readonly RegisterDbContext context;

        public ShipmentService(RegisterDbContext context)
        {
            this.context = context;
        }

        public async Task AddWelcomePackage(RegistrationDto registration, int memberId)
        {
            await AddPackage(registration, memberId, Shipment.ShipmentType.WelcomePackage, DateTime.Today);
        }

        public async Task AddOneYearPackage(RegistrationDto registration, int memberId)
        {
            await AddPackage(registration, memberId, Shipment.ShipmentType.OneYearAnniversary, DateTime.Today.AddYears(1));
        }

        public async Task AddFiveYearPackage(RegistrationDto registration, int memberId)
        {
            await AddPackage(registration, memberId, Shipment.ShipmentType.FiveYearAnniversary, DateTime.Today.AddYears(5));
        }

        private async Task AddPackage(RegistrationDto registration, int memberId, Shipment.ShipmentType type, DateTime date)
        {
            var member = await context.Members.FindAsync(memberId);
            if (member == null)
                throw new ArgumentException($"Member with id {memberId} does not exist");

            await context.Shipments.AddAsync(new Shipment
            {
                Address = new Address
                {
                    Street = registration.Street,
                    StreetNumber = registration.StreetNumber,
                    City = registration.City,
                    State = registration.State,
                    Country = registration.Country,
                },
                MemberId = member.Id,
                Type = type,
                Date = date
            });
            await context.SaveChangesAsync();
        }
    }
}