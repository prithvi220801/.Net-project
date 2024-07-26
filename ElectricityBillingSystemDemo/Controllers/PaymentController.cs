using ElectricityBillingSystemDemo.Data;
using ElectricityBillingSystemDemo.Interfaces;
using ElectricityBillingSystemDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectricityBillingSystemDemo.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _ipay;
        private readonly ElectricityDbContext _context;

        public PaymentController(IPayment ipay,ElectricityDbContext context)
        {
            _ipay = ipay;
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetPayments()
        {
            var payments= await _ipay.GetPayments();
            return Ok(payments);
        }

        [Authorize(Roles ="Admin,Customer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Payment>>> GetPaymentById(int id)
        {
            var payment=await _ipay.GetPaymentById(id);
            if (payment==null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddPayment(Payment payment)
        {
            await _ipay.AddPayment(payment);    
            return Ok(new { message = "Payment Details Added Successfully" });
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePayment(int id,Payment payment)
        {
            if(id!=payment.CustomerId)
            {
                return BadRequest("id Mismatch");
            }
            await _ipay.UpdatePayment(id, payment);
            return Ok(new {message="Payment Updated"});
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePayment(int id)
        {
            await _ipay.DeletePayment(id);
            return Ok("Deleted Successfully");
        }
    }
}
