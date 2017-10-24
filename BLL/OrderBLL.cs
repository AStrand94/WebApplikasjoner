﻿using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

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
    }
}
