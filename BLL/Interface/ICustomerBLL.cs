using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public interface ICustomerBLL
    {
        Customer AddCustomer(Customer customer);
        bool CanDelete(int id);
        Order DeleteAssociatedOrder(int customerId, int orderId);
        Customer DeleteCustomer(int id);
        IEnumerable<Customer> GetAllCustomers();
        Customer UpdateCustomer(Customer customer);
        IEnumerable<Customer> GetAllCustomersConnections();
    }
}