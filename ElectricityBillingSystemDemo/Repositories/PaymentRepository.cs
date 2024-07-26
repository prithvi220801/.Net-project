using ElectricityBillingSystemDemo.Data;
using ElectricityBillingSystemDemo.Interfaces;
using ElectricityBillingSystemDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectricityBillingSystemDemo.Repositories
{
    public class PaymentRepository:IPayment
    {
        private readonly ElectricityDbContext _context;

        public PaymentRepository(ElectricityDbContext context)
        {
            _context = context;
        }

        public async Task<List<Payment>> GetPayments()
        {
            //return await _context.Payments.Include(b=>b.Bill).ThenInclude(c=>c.Customer).ToListAsync();
            return await _context.Payments.ToListAsync();
        }

        public async  Task<List<Payment>> GetPaymentById(int id)
        {
            //return await _context.Payments.Include(b => b.Bill).ThenInclude(c => c.Customer).Where(i=>i.CustomerId==id).ToListAsync();
            return await _context.Payments.Where(i=>i.CustomerId == id).ToListAsync();
        }

        public async Task AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

        }

        public async Task UpdatePayment(int id,Payment payment)
        {
            if(id!=payment.CustomerId)
            {
                throw new ArgumentException("Id mismatch");
            }
            var result = await _context.Payments.FindAsync(id);
             _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePayment(int id)
        {
            var result = await _context.Payments.FindAsync(id);
            if (result==null)
            {
                throw new KeyNotFoundException($"Payment with id {id} not found");
            }
            _context.Payments.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
