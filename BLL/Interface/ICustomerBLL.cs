using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public interface ICustomerBLL
    {
        void AddCustomer(Customer customer);
        void AddCustomers(IEnumerable<Customer> customers);
        bool CanDelete(int id);
        Order DeleteAssociatedOrder(int customerId, int orderId);
        Customer DeleteCustomer(int id);
        IEnumerable<Customer> GetAllCustomers();
        void UpdateCustomer(Customer customer);
        IEnumerable<Customer> GetAllCustomersConnections();
    }
}