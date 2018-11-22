using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Register.Data.Model;
using Register.Web.Controllers;

namespace Register.Web.Services
{
    class MonthlyBankTransferStrategy : BankTransferStrategy
    {
        private static decimal amount = 5.55m;
        internal override List<BankTransfer> GetBankTransfers(DateTime today, int years, string account, int memberId)
        {
            var bankTransfers = new List<BankTransfer>();
            var occurrences = years * 12;
            var date = new DateTime(today.Year, today.Month, 1);
            for (var i = 0; i < occurrences; i++)
            {
                date = date.AddMonths(1);
                bankTransfers.Add(new BankTransfer
                {
                    MemberId = memberId,
                    Account = account,
                    Amount = amount,
                    TransferDateTime = date
                });
            }

            return bankTransfers;
        }
    }

    class QuarterlyBankTransferStrategy : BankTransferStrategy
    {
        private static decimal amount = 15.55m;
        internal override List<BankTransfer> GetBankTransfers(DateTime today, int years, string account, int memberId)
        {
            var bankTransfers = new List<BankTransfer>();
            var occurrences = (years * 12) / 4;
            var date = new DateTime(today.Year, today.Month, 1);
            for (var i = 0; i < occurrences; i++)
            {
                date = date.AddMonths(3);
                bankTransfers.Add(new BankTransfer
                {
                    MemberId = memberId,
                    Account = account,
                    Amount = amount,
                    TransferDateTime = date
                });
            }

            return bankTransfers;
        }
    }

    class YearlyBankTransferStrategy : BankTransferStrategy
    {
        private static decimal amount = 50.55m;
        internal override List<BankTransfer> GetBankTransfers(DateTime today, int years, string account, int memberId)
        {
            var bankTransfers = new List<BankTransfer>();
            var date = new DateTime(today.Year, today.Month, 1);
            for (var i = 0; i < years; i++)
            {
                date = date.AddYears(1);
                bankTransfers.Add(new BankTransfer
                {
                    MemberId = memberId,
                    Account = account,
                    Amount = amount,
                    TransferDateTime = date
                });
            }

            return bankTransfers;
        }
    }
    abstract class BankTransferStrategy
    {
        internal abstract List<BankTransfer> GetBankTransfers(DateTime today, int years, string account, int memberId);
    }

    static class BankTransferStrategyFactory
    {
        internal static BankTransferStrategy GetStrategy(BankTransfer.Recurrency recurrency)
        {
            switch (recurrency)
            {
                case BankTransfer.Recurrency.Monthly:
                    return new MonthlyBankTransferStrategy();
                case BankTransfer.Recurrency.Quarterly:
                    return new QuarterlyBankTransferStrategy();
                case BankTransfer.Recurrency.Yearly:
                    return new YearlyBankTransferStrategy();
            }
            return null;
        }
    }
    public class BankTransferService : IRegisterService
    {
        private readonly RegisterDbContext context;

        public BankTransferService(RegisterDbContext context)
        {
            this.context = context;
        }

        public async Task GenerateBankTransfers(RegistrationDto registration, int memberId)
        {
            var member = await context.Members.FindAsync(memberId);
            if (member == null)
                throw new ArgumentException($"Member with id {memberId} does not exist");

            var recurrencyType = (BankTransfer.Recurrency)Enum.Parse(typeof(BankTransfer.Recurrency), registration.BankTransferRecurrency);
            var strategy = BankTransferStrategyFactory.GetStrategy(recurrencyType);
            var bankTransfers = strategy.GetBankTransfers(DateTime.Today, 2, registration.Account, member.Id);

            await context.BankTransfer.AddRangeAsync(bankTransfers);
            await context.SaveChangesAsync();
        }
    }
}