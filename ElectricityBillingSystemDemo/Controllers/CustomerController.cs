using ElectricityBillingSystemDemo.Data;
using ElectricityBillingSystemDemo.Interfaces;
using ElectricityBillingSystemDemo.Models;
using ElectricityBillingSystemDemo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectricityBillingSystemDemo.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _icus;
        //private readonly ElectricityDbContext _context;

        public CustomerController(ICustomer icus)
        {
            _icus = icus;
            //_context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetCustomer()
        {
            var customers=await _icus.GetCustomer();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult> GetCustomerById(int id)
        {
            var customer = await _icus.GetCustomerById(id);
            if(customer==null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPost]
        public async Task<ActionResult> AddCustomer(Customer customer)
        {
            if(customer==null)
            {
                return BadRequest();
            }
            await _icus.AddCustomer(customer);
            return Ok(new { message = "Customer Added Successfully" });
        }


        [Authorize(Roles = "Admin,Customer")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id,Customer customer)
        {
            if(id!=customer.CustomerId)
            {
                return BadRequest(new { message = "Customer id Mismatch" });
            }
            await _icus.UpdateCustomer(id,customer);
            return Ok(new { message = "Customer details Updated Successfully!" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _icus.DeleteCustomer(id);
            return Ok(new { message = $"Customer with id {id} Deleted Successfully" });
        }




    }
}
