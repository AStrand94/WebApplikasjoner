using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;
using System.Data.Entity;

namespace WebApplication3.DAL
{
    public class CustomerDAL : ICustomerDAL
    {
        public void AddCustomers(IEnumerable<Customer> customers) 
        {
            using (DB db = new DB())
            {
                foreach (var customer in customers)
                {
                    db.Customers.Add(customer);
                }
                db.SaveChanges();
            }
        }

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

        public void AddCustomer(Customer customer)
        {
            using (DB db = new DB())
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (DB db = new DB())
            {
                Customer customerInDb = db.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Firstname = customer.Firstname;
                customerInDb.Lastname = customer.Lastname;
                customerInDb.Telephone = customer.Telephone;
                customerInDb.Email = customer.Email;

                db.SaveChanges();
            }
        }

        public bool ExistsCustomerWithId(int customerId)
        {
            using(DB db = new DB())
            {
                return db.Customers.Any(c => c.Id == customerId);
            }
        }
    }
}
