using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;
using System.Data.Entity;

namespace WebApplication3.DAL
{
    public class OrderDAL : IOrderDAL
    {
        public void AddOrder(Order order)
        {
            using (DB db = new DB())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }

        }

        public IEnumerable<Order> GetOrder(String ReferenceNumber)
        {
            using (DB db = new DB())
            {
                return db.Orders.Where(o => o.Reference.Equals(ReferenceNumber));
            }
        }

        public Order GetOrder(int id)
        {
            using (DB db = new DB())
            {
                return db.Orders.Where(o => o.Id == id).Single();
            }
        }

        public bool OrdersHasReferenceNumber(string s)
        {
            using (DB db = new DB())
            {
                return db.Orders.Where(o => o.Reference.Equals(s)).Any();
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            using (DB db = new DB())
            {
                return db.Orders.ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersConnections()
        {
            using (DB db = new DB())
            {
                return db.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Tickets)
                    .ToList();
            }
        }
    }
}
