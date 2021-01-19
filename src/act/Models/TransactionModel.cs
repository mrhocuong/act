using System;

namespace act.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public CustomerModel Customer { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } 
        public DateTimeOffset PaymentAt { get; set; }

    }
}