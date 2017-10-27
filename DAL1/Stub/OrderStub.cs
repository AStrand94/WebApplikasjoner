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
        public IEnumerable<Order> GetAllOrders()
        {
            var orderList = new List<Order>();
            var order = GetOrder(1);

            orderList.Add(order);
            orderList.Add(order);
            orderList.Add(order);

            return orderList;
        }

        public IEnumerable<Order> GetAllOrdersConnections()
        {
            var orderList = new List<Order>();
            var order = GetOrder(1);

            orderList.Add(order);
            orderList.Add(order);
            orderList.Add(order);

            return orderList;
        }

        public Order GetOrder(int id)
        {
            if(id == 0)
            {
                return null;
            }
            else
            {
                var o =  new Order
                {
                    Id = 1,
                    Tickets = new List<Ticket>(),
                    Customer = new Customer
                    {
                        Firstname = "Andreas",
                        Lastname = "Strand",
                        Telephone = "00000000",
                        Email = "a@a.a"
                    }
                };
				var t = new Ticket
				{
					FirstName ="",
					LastName = "",
					Order = o
				};

				o.Tickets.Add(t);
				return o;
            }
            
        }
    }
}
