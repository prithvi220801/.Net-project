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
    public class BillingController : Controller
    {
        private readonly IBill _ibill;
        private readonly ElectricityDbContext _context;

        public BillingController(IBill ibill, ElectricityDbContext context)
        {
            _ibill = ibill;
            _context = context;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult> GetBills()
        {
            

           /* var result=await _context.Bills.Include(b => b.Customer).ToListAsync();//Including customer Information using Eager Loading*/
           var result=await _ibill.GetBills();
            return Ok(result);
            
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBillsById(int id)
        {
           // var result = await _context.Bills.Include(b => b.Customer).Where(b => b.CustomerId == id).ToListAsync();
           var result=await _ibill.GetBillsById(id);
            if(result==null)
            {
                return NotFound("Bill Not Found");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddBill(Bill bill)
        {
            
            _ibill.AddBill(bill);
            return CreatedAtAction(nameof(GetBillsById), new { id = bill.BillId }, bill);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBill(int id, [FromBody]Bill bill)
        {
            if(id!=bill.CustomerId)
            {
                return BadRequest();    
            }

            await _ibill.UpdateBill(id, bill);
            return Ok(new { message = "Bill Updated" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBill(int id)
        {
            await _ibill.DeleteBill(id);
            return Ok("Deleted Bill!");
        }
    }
}
