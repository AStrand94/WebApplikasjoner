using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public interface ICustomerDAL
    {
        void AddCustomer(Customer customer);
        void AddCustomers(IEnumerable<Customer> customers);
        Order DeleteAssociatedOrder(int customerId, int orderId);
        Customer DeleteCustomer(int id);
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
        void UpdateCustomer(Customer customer);
        IEnumerable<Customer> GetAllCustomersConnections();
        bool HasOrder(int id);
    }
}