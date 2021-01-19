using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using act.Domain;
using act.Domain.Entity;
using act.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace act.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PaymentController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = _appDbContext.Transactions.AsNoTracking()
                .Include(x => x.Customer)
                .OrderByDescending(x => x.Id)
                .Select(x => new TransactionModel()
                {
                    Id = x.Id,
                    Customer = new CustomerModel() {Name = x.Customer.Name, Email = x.Customer.Email},
                    Amount = x.Amount,
                    Currency = x.Currency,
                    PaymentAt = x.CreateAt
                });

            var result = await query.ToListAsync();
            return Ok(new ApiResponse<List<TransactionModel>>(result));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePayment createPayment)
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(x => x.Email == createPayment.Email);
            if (customer == null)
            {
                customer = new Customer()
                {
                    Name = createPayment.Name,
                    Email = createPayment.Email
                };
                await _appDbContext.Customers.AddAsync(customer);
                await _appDbContext.SaveChangesAsync();
            }

            var createTransaction = new Transaction()
            {
                CustomerId = customer.Id,
                Amount = createPayment.Amount,
                Currency = "USd"
            };
            await _appDbContext.Transactions.AddAsync(createTransaction);
            await _appDbContext.SaveChangesAsync();

            return Ok(new ApiResponse<int>(createTransaction.Id));
        }
    }
}