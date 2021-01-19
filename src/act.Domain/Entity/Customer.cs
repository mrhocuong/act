using System.Collections.Generic;

namespace act.Domain.Entity
{
    public class Customer : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}