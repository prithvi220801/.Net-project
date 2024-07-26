using ElectricityBillingSystemDemo.Data;
using ElectricityBillingSystemDemo.Interfaces;
using ElectricityBillingSystemDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricityBillingSystemDemo.Repositories
{
    public class BillingRepository:IBill
    {
        private readonly ElectricityDbContext _context;

        public BillingRepository(ElectricityDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bill>> GetBills()
        {
            return await _context.Bills.ToListAsync();
        }

        public async Task<List<Bill>> GetBillsById(int id)
        {
            return await _context.Bills.Where(c=>c.CustomerId==id).ToListAsync();

        }

        
        public void AddBill(Bill bill)
        {
            try
            {

                _context.Bills.Add(bill);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while adding the bill: {ex.Message}");
                throw; 
            }
        }

        public async Task UpdateBill(int id,Bill bill)
        {
            if(id!=bill.CustomerId)
            {
                throw new ArgumentException("id mismatch");
            }
            _context.Entry(bill).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBill(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if(bill!=null)
            {
                _context.Bills.Remove(bill);
                await _context.SaveChangesAsync();
            }
        }
    }
}
