using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class CustomerBLL
    {
        private DB db = new DB();

        public void AddCustomers(IEnumerable<Customer> customers)
        {
            new CustomerDAL(db).AddCustomers(customers);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return new CustomerDAL(db).GetAllCustomers();
        }
    }
}
