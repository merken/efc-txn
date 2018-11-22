using System;

namespace Register.Data.Model
{
    public class BankTransfer
    {
        public enum Recurrency
        {
            Once,
            Monthly,
            Quarterly,
            Yearly
        }

        public int Id { get; private set; }
        public Recurrency TransferRecurrency { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferDateTime { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}