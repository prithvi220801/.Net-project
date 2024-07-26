using ElectricityBillingSystemDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectricityBillingSystemDemo.Interfaces
{
    public interface ICustomer
    {
        Task<List<Customer>> GetCustomer();
        Task<List<Customer>> GetCustomerById(int id);
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(int id, Customer customer);
        Task DeleteCustomer(int id);
    }
}
