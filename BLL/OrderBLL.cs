using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class OrderBLL : IOrderBLL
    {
        private IOrderDAL order;

        public OrderBLL()
        {
            order = new OrderDAL();
        }

        public OrderBLL(IOrderDAL stub)
        {
            order = stub;
        }

        public Order CreateOrder(OrderSession orderSession)
        {
            String referenceNumber = new ReferenceGenerator().getReferenceNumber();

            FlightDAL flightDAL = new FlightDAL();
            Order o = new Order
            {
                Reference = referenceNumber,
                Customer = orderSession.Customer
            };

            List<Ticket> tickets = new List<Ticket>();

            foreach (Customer tr in orderSession.Travelers)
            {
                foreach (int fId in orderSession.Flights)
                {
                    Flight flight = flightDAL.GetFlight(fId);

                    Ticket ticket = new Ticket
                    {
                        Order = o,
                        Flight = flight,
                        FirstName = tr.Firstname,
                        LastName = tr.Lastname
                    };
                    tickets.Add(ticket);
                }
            }

            o.Tickets = tickets;

            o.TotalPrice = orderSession.TotalPrice;
            new OrderDAL().AddOrder(o);
            new TicketDAL().addTickets(tickets);

            return o;
        }

        public IEnumerable<Order> GetOrder(string ReferenceNumber)
        {
            return new OrderDAL().GetOrder(ReferenceNumber);
        }

        public Order GetOrder(int id)
        {
            return new OrderDAL().GetOrder(id);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return new OrderDAL().GetAllOrders();
        }

        public void AddOrder(Order order)
        {
            CustomerDAL customerDAL = new CustomerDAL();
            OrderDAL orderDAL = new OrderDAL();

            order.Customer = customerDAL.GetCustomer(order.Customer.Id);
            

            orderDAL.AddOrder(order);
        }

        public IEnumerable<Order> GetAllOrdersConnections()
        {
            return new OrderDAL().GetAllOrdersConnections();
        }
    }
}
