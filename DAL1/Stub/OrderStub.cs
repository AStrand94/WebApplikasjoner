using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class OrderStub : IOrderDAL
    {
        public void AddOrder(Order order)
        {
        }

        public IEnumerable<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            Order order = new Order
            {
                Tickets = new List<Ticket>(),
                Customer = new Customer
                {
                    Firstname = "Andreas",
                    Lastname = "Strand",
                    Telephone = "00000000",
                    Email = "a@a.a"
                }
            };

            orders.Add(order);
        }

        public IEnumerable<Order> GetAllOrdersConnections()
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrder(string ReferenceNumber)
        {
            throw new NotImplementedException();
        }
    }
}
