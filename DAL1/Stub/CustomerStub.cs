using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class CustomerStub : ICustomerDAL
    {
        public void AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void AddCustomers(IEnumerable<Customer> customers)
        {
            throw new NotImplementedException();
        }

        public Order DeleteAssociatedOrder(int customerId, int orderId)
        {
            throw new NotImplementedException();
        }

        public Customer DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomersConnections()
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
