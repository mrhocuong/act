using System.ComponentModel.DataAnnotations.Schema;

namespace act.Domain.Entity
{
    public class Transaction : BaseModel
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}