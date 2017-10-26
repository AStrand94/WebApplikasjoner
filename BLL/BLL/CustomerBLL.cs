using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class CustomerBLL : ICustomerBLL
    {
        private ICustomerDAL _customer;

        public CustomerBLL()
        {
            _customer = new CustomerDAL();
        }

        public CustomerBLL(ICustomerDAL stub)
        {
            _customer = stub;
        }

        public Customer AddCustomer(Customer customer)
        {
            return _customer.AddCustomer(customer);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customer.GetAllCustomers();
        }

        public IEnumerable<Customer> GetAllCustomersConnections()
        {
            return _customer.GetAllCustomersConnections();
        }

        public Customer DeleteCustomer(int id)
        {
            return _customer.DeleteCustomer(id);
        }

        public Order DeleteAssociatedOrder(int customerId, int orderId)
        {
            return _customer.DeleteAssociatedOrder(customerId, orderId);
        }

        public bool CanDelete(int id)
        {
            return _customer.HasOrder(id);
        }
        
        public Customer UpdateCustomer(Customer customer)
        {
            return _customer.UpdateCustomer(customer);
        }
    }
}
