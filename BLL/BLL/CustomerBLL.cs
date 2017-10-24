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
        private DB db = new DB();

        private ICustomerDAL customer;

        public CustomerBLL()
        {
            customer = new CustomerDAL();
        }

        public CustomerBLL(ICustomerDAL stub)
        {
            customer = stub;
        }

        public void AddCustomers(IEnumerable<Customer> customers)
        {
            new CustomerDAL().AddCustomers(customers);
        }

        public void AddCustomer(Customer customer)
        {
            new CustomerDAL().AddCustomer(customer);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return new CustomerDAL().GetAllCustomers();
        }

        public IEnumerable<Customer> GetAllCustomersConnections()
        {
            return new CustomerDAL().GetAllCustomersConnections();
        }

        public Customer DeleteCustomer(int id)
        {
            return new CustomerDAL().DeleteCustomer(id);
        }

        public Order DeleteAssociatedOrder(int customerId, int orderId)
        {
            return new CustomerDAL().DeleteAssociatedOrder(customerId, orderId);
        }

        public bool CanDelete(int id)
        {
            Customer customer = new CustomerDAL().GetCustomer(id);

            return customer != null  && !customer.Order.Any();
        }
        
        public void UpdateCustomer(Customer customer)
        {
            new CustomerDAL().UpdateCustomer(customer);
        }
    }
}
