using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class CustomerDAL
    {
        private DB db;

        public CustomerDAL(DB db)
        {
            this.db = db;
        }

        public void AddCustomers(IEnumerable<Customer> customers) 
        {
            foreach (var customer in customers)
            {
                db.Customers.Add(customer);
            }
            db.SaveChanges();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        public Customer DeleteCustomer(int id)
        {
            //Må slette avhengigheter først. Ikke slett for kunder som har ordre.
            Customer customer = new Customer()
            {
                Id = id
            };

            db.Customers.Attach(customer)
            customer = db.Customers.Remove(customer);
            db.SaveChanges();

            return customer;
        }

    }
}
