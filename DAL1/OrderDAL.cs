using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class OrderDAL
    {
        private DB db;

        public OrderDAL(DB db)
        {
            this.db = db;
        }

        public void AddOrder(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public IEnumerable<Order> GetOrder(String ReferenceNumber)
        {
            return db.Orders.Where(o => o.Reference.Equals(ReferenceNumber));
        }

        public Order GetOrder(int id)
        {
            return db.Orders.Where(o => o.Id == id).Single();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return db.Orders.ToList();
        }

    }
}
