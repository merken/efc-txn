using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Register.Data.Model;
using Register.Web.Services;

namespace Register.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly MemberService memberService;
        private readonly ShipmentService shipmentService;
        private readonly BankTransferService bankTransferService;

        public RegistrationController(MemberService memberService, ShipmentService shipmentService, BankTransferService bankTransferService)
        {
            this.memberService = memberService;
            this.shipmentService = shipmentService;
            this.bankTransferService = bankTransferService;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistrationDto registration)
        {
            var memberId = await memberService.Register(registration);

            await shipmentService.AddWelcomePackage(registration, memberId);

            await shipmentService.AddOneYearPackage(registration, memberId);

            await shipmentService.AddFiveYearPackage(registration, memberId);

            await bankTransferService.GenerateBankTransfers(registration, memberId);

            return Ok();
        }
    }
}
