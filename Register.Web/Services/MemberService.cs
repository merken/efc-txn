using System.Threading.Tasks;
using Register.Data.Model;
using Register.Web.Controllers;

namespace Register.Web.Services
{
    public class MemberService : IRegisterService
    {
        private readonly RegisterDbContext context;

        public MemberService(RegisterDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Register(RegistrationDto registration)
        {
            var newMember = new Member
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName
            };

            await context.Members.AddAsync(newMember);
            await context.SaveChangesAsync();

            return newMember.Id;
        }
    }
}