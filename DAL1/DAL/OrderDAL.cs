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
        public Order AddOrder(Order order)
        {
            using (DB db = new DB())
            {
                db.Flights.Attach(order.Tickets[0].Flight);
                foreach (var ticket in order.Tickets) db.Tickets.Add(ticket);
                db.Orders.Add(order);
                db.SaveChanges();
                return order;
            }

        }

        public Order AddOrderWithCustomer(Order order)
        {
            using(DB db = new DB())
            {
                order.Customer = db.Customers.Add(order.Customer);

                HashSet<Flight> flights = new HashSet<Flight>();
                foreach (var t in order.Tickets)
                {
                    if (!flights.Any(fl => fl.Id == t.Flight.Id))
                    {
                        Flight flight = db.Flights.Where(f => f.Id == t.Flight.Id).Single();
                        t.Flight = flight;
                    }
                    else
                    {
                        Flight flight = flights.Where(fl => fl.Id == t.Flight.Id).Single();
                        flights.Add(flight);
                        t.Flight = flight;
                    }
                    
                }

                foreach (var ticket in order.Tickets) db.Tickets.Add(ticket);
                db.Orders.Add(order);
                db.SaveChanges();
                return order;
            }
        }

        public IEnumerable<Order> GetOrder(String ReferenceNumber)
        {
            using (DB db = new DB())
            {
                return db.Orders
                    .Where(o => o.Reference.Equals(ReferenceNumber))
                    .Include("Customer")
                    .Include("Tickets.Flight.Route.ToAirport")
                    .Include("Tickets.Flight.Route.FromAirport")
                    .ToList();
            }
        }

        public Order GetOrder(int id)
        {
            using (DB db = new DB())
            {
                return db.Orders.Where(o => o.Id == id)
                    .Include("Tickets.Flight.Route.ToAirport")
                    .Include("Tickets.Flight.Route.FromAirport")
                    .Include("Customer")
                    .Single();
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
                    .Include("Tickets.Flight.Route.ToAirport")
                    .Include("Tickets.Flight.Route.FromAirport")
                    .ToList();
            }
        }
    }
}
