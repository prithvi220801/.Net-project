using ElectricityBillingSystemDemo.Models;

namespace ElectricityBillingSystemDemo.Interfaces
{
    public interface IPayment
    {
        Task<List<Payment>> GetPayments();
        Task<List<Payment>> GetPaymentById(int id);
        Task AddPayment(Payment payment);
        Task UpdatePayment(int id,Payment payment);
        Task DeletePayment(int id);

    }
}
