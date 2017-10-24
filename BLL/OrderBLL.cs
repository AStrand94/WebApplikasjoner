﻿using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;
using DTO;

namespace WebApplication3.BLL
{
    public class OrderBLL
    {
        private DB db = new DB();

        public Order CreateOrder(OrderSession orderSession)
        {
            String referenceNumber = new ReferenceGenerator().getReferenceNumber(db);

            FlightDAL flightDAL = new FlightDAL(db);
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
            new OrderDAL(db).AddOrder(o);
            new TicketDAL(db).addTickets(tickets);

            return o;
        }

        public IEnumerable<Order> GetOrder(string ReferenceNumber)
        {
            return new OrderDAL(db).GetOrder(ReferenceNumber);
        }

        public Order GetOrder(int id)
        {
            return new OrderDAL(db).GetOrder(id);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return new OrderDAL(db).GetAllOrders();
        }

        public void AddOrder(Order order)
        {
            CustomerDAL customerDAL = new CustomerDAL(db);
            OrderDAL orderDAL = new OrderDAL(db);

            order.Customer = customerDAL.GetCustomer(order.Customer.Id);
            

            orderDAL.AddOrder(order);
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

            FlightDAL flightDAL = new FlightDAL(db);
            CustomerDAL customerDAL = new CustomerDAL(db);

            if(!flightDAL.ExistsFlightWithId(dto.FlightId))
            {
                stringBuilder.Append("Flight does not exist");
            }

            if (!customerDAL.ExistsCustomerWithId(dto.CustomerId))
            {
                stringBuilder.Append("Customer does not exist");
            }

            return stringBuilder.ToString();
        }

        public Order CreateOrder(OrderDTO dto)
        {
            Order order = new Order();
            
            order.Customer = new CustomerDAL(db).GetCustomer(dto.CustomerId);
            Flight flight = new FlightDAL(db).GetFlight(dto.FlightId);
            order.Reference = new ReferenceGenerator().getReferenceNumber(db);

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

            new OrderDAL(db).AddOrder(order);
            new TicketDAL(db).addTickets(tickets);

            return order;
        }
    }
}
