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

    }
}
