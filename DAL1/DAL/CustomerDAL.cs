using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication3.DAL
{
    [ExcludeFromCodeCoverage]
    public class CustomerDAL : ICustomerDAL
    {
        public IEnumerable<Customer> GetAllCustomers()
        {
            using (DB db = new DB())
            {
                return db.Customers.ToList();
            }
        }

        public IEnumerable<Customer> GetAllCustomersConnections()
        {
            using (DB db = new DB())
            {
                return db.Customers
                    .Include(c => c.Order)
                    .ToList();
            }
        }

        public Customer DeleteCustomer(int id)
        {
            using (DB db = new DB())
            {
                //Må slette avhengigheter først. Ikke slett for kunder som har ordre.
                Customer customer = db.Customers.Where(c => c.Id == id).Single();

                if (customer != null)
                {
                    db.Customers.Attach(customer);
                    customer = db.Customers.Remove(customer);
                    db.SaveChanges();
                }

                return customer;
            }
        }

        public bool HasOrder(int id)
        {
            using (DB db = new DB())
            {
                Customer customer = db.Customers.Where(c => c.Id == id).Single();
                return customer != null && !customer.Order.Any();
            }
        }

        public Order DeleteAssociatedOrder(int customerId, int orderId)
        {
            using (DB db = new DB())
            {
                Customer customer = db.Customers.Where(c => c.Id == customerId).Single();
                Order customerOrder = customer.Order.Where(o => o.Id == orderId).Single();

                if (customer != null)
                {
                    db.Orders.Attach(customerOrder);
                    customerOrder = db.Orders.Remove(customerOrder);
                    db.SaveChanges();
                }

                return customerOrder;
            }
        }

        public Customer GetCustomer(int id)
        {
            using (DB db = new DB())
            {
                return db.Customers.Where(c => c.Id == id).Single();
            }
        }

        public Customer AddCustomer(Customer customer)
        {
            using (DB db = new DB())
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return customer;
            }
        }

        public Customer UpdateCustomer(Customer customer)
        {
            using (DB db = new DB())
            {
                Customer customerInDb = db.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Firstname = customer.Firstname;
                customerInDb.Lastname = customer.Lastname;
                customerInDb.Telephone = customer.Telephone;
                customerInDb.Email = customer.Email;

                db.SaveChanges();
                return customerInDb;
            }
        }

        public bool ExistsCustomerWithId(int customerId)
        {
            using (DB db = new DB())
            {
                return db.Customers.Any(c => c.Id == customerId);
            }
        }
    }
}
