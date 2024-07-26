using ElectricityBillingSystemDemo.Models;

namespace ElectricityBillingSystemDemo.Interfaces
{
    public interface IBill
    {
        Task<List<Bill>> GetBills();
        Task<List<Bill>> GetBillsById(int id);
        void AddBill(Bill bill);
        Task UpdateBill(int id,Bill bill);
        Task DeleteBill(int id);


    }
}
