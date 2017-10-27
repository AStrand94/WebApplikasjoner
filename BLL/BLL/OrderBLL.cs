using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;
using DTO;
using WebApplication3.DAL;

namespace WebApplication3.BLL
{
    public class OrderBLL : IOrderBLL
    {
        private IOrderDAL _orderDAL;
        private ICustomerDAL _customerDAL;
        private IFlightDAL _flightDAL;
        private ITicketDAL _ticketDAL;
        private IReferenceGenerator _referenceGenerator;

        public OrderBLL()
        {
            _orderDAL = new OrderDAL();
            _customerDAL = new CustomerDAL();
            _flightDAL = new FlightDAL();
            _ticketDAL = new TicketDAL();
            _referenceGenerator = new ReferenceGenerator();
        }

        public OrderBLL(IOrderDAL stub, ICustomerDAL customerStub, IFlightDAL flightStub, ITicketDAL ticketStub, IReferenceGenerator referenceGenerator)
        {
            _orderDAL = stub;
            _customerDAL = customerStub;
            _flightDAL = flightStub;
            _ticketDAL = ticketStub;
            _referenceGenerator = referenceGenerator;
        }

        //Called from Home controller, so will not be tested
        public Order CreateOrder(OrderSession orderSession)
        {
            String referenceNumber = _referenceGenerator.getReferenceNumber();

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
                    Flight flight = _flightDAL.GetFlight(fId);

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

            //new TicketDAL().addTickets(tickets);
            o.TotalPrice = orderSession.TotalPrice;
            _orderDAL.AddOrderWithCustomer(o);

            return _orderDAL.GetOrder(o.Id);
        }

        //Used from Home controller, will not be tested
        public IEnumerable<Order> GetOrder(string ReferenceNumber)
        {
            return new OrderDAL().GetOrder(ReferenceNumber);
        }

        public Order GetOrder(int id)
        {
            return _orderDAL.GetOrder(id);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderDAL.GetAllOrders();
        }

        public IEnumerable<Order> GetAllOrdersConnections()
        {
            return _orderDAL.GetAllOrdersConnections();
        }

        public string CanCreateOrder(OrderDTO dto)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach(var t in dto.Travellers)
            {
                if(t.firstName == null || t.firstName.Length == 0 || t.lastName == null || t.lastName.Length == 0)
                {
                    stringBuilder.Append("Must fill out firstname and lastname!");
                    break;
                }
            }

            if(!_flightDAL.ExistsFlightWithId(dto.FlightId))
            {
                stringBuilder.Append("Flight does not exist");
            }

            if (!_customerDAL.ExistsCustomerWithId(dto.CustomerId))
            {
                stringBuilder.Append("Customer does not exist");
            }

            return stringBuilder.ToString();
        }

        public Order CreateOrder(OrderDTO dto)
        {
            Order order = new Order();
            
            order.Customer = _customerDAL.GetCustomer(dto.CustomerId);
            Flight flight = _flightDAL.GetFlight(dto.FlightId);
            order.Reference = _referenceGenerator.getReferenceNumber();

            List<Ticket> tickets = new List<Ticket>();

            foreach(var t in dto.Travellers)
            {
                Ticket ticket = new Ticket
                {
                    FirstName = t.firstName,
                    LastName = t.lastName,
                    Order = order,
                    Flight = flight
                };
                tickets.Add(ticket);
            }

            order.TotalPrice = flight.Price * dto.Travellers.Count;
            order.Tickets = tickets;
            _orderDAL.AddOrder(order);
            //new TicketDAL().addTickets(tickets);

            return order;
        }
    }
}
