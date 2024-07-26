using ElectricityBillingSystemDemo.Data;
using ElectricityBillingSystemDemo.Interfaces;
using ElectricityBillingSystemDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectricityBillingSystemDemo.Repositories
{
    public class CustomerRepository : ICustomer
    {

        private readonly ElectricityDbContext _context;
       

        public CustomerRepository(ElectricityDbContext context)
        {
            _context = context;
            
        }

        /*[Authorize(Roles ="Admin")]*/
        public async Task<List<Customer>> GetCustomer()
        {
            return await _context.Customers.ToListAsync();
        }

        /*[Authorize(Roles = "Admin")]*/
        public async Task<List<Customer>> GetCustomerById(int id)
        {
            return await _context.Customers.Where(c=>c.CustomerId==id).ToListAsync();
            
        }

        /*[Authorize(Roles = "Admin")]*/
        public async Task AddCustomer(Customer customer)
        {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
        }

       /*[Authorize(Roles = "Admin")]*/
        public async Task UpdateCustomer(int id, Customer customer)
        {
            

            // var existingCustomer = await _context.Customers.FindAsync(id);
            // if(existingCustomer != null)
            // {
            //     existingCustomer.CustomerId = id;
            //     existingCustomer.CustomerName = customer.CustomerName;
            //     existingCustomer.Address = customer.Address;
            //     existingCustomer.ContactNumber = customer.ContactNumber;
            //     existingCustomer.Email = customer.Email;
            //     _context.Customer.Update(existingCustomer);
            //     await _context.SaveChangesAsync();
            // }
            if(id!=customer.CustomerId)
            {
                throw new ArgumentException("id mismatch");
            }
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        
        public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            
        }


    }
}
