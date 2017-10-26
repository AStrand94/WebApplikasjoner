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
        public Customer AddCustomer(Customer customer)
        {
            if (customer.Id == 0)
            {
                var c = new Customer
                {
                    Id = 0
                };
                return c;
            }
            else
            {
                return GetCustomer(1);
            }
        }

        public Order DeleteAssociatedOrder(int customerId, int orderId)
        {
            if (customerId == 0 && orderId == 0)
            {
                var o = new Order
                {
                    Id = 0
                };
                return o;
            }
            else
            {
                var o = new Order
                {
                    Id = 1
                };
                return o;
            }
        }

        public Customer DeleteCustomer(int id)
        {
            if (id == 0)
            {
                return null;
            }
            else
            {
                return GetCustomer(1);
            }
        }

        public bool ExistsCustomerWithId(int customerId)
        {
            if(customerId == 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customerList = new List<Customer>();
            var customer = GetCustomer(1);

            customerList.Add(customer);
            customerList.Add(customer);
            customerList.Add(customer);

            return customerList;
        }

        public IEnumerable<Customer> GetAllCustomersConnections()
        {
            var customerList = new List<Customer>();
            var customer = GetCustomer(1);

            customerList.Add(customer);
            customerList.Add(customer);
            customerList.Add(customer);

            return customerList;
        }

        public Customer GetCustomer(int id)
        {
            if (id == 0)
            {
                var customer = new Customer
                {
                    Id = 0
                };
                return customer;
            }
            else
            {
                var customer = new Customer()
                {
                    Id = 1,
                    Firstname = "Ola",
                    Lastname = "Normann",
                    Email = "olanormann@gmail.com",
                    Telephone = "12345678"
                };
                return customer;
            }
        }

        public bool HasOrder(int id)
        {
            if (id == 100)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Customer UpdateCustomer(Customer customer)
        {
            if (customer.Id == 0)
            {
                var c = new Customer
                {
                    Id = 0
                };
                return c;
            }
            else
            {
                return GetCustomer(1);
            }
        }
    }
}
