using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class TicketStub : ITicketDAL
    {
        public void addTickets(IEnumerable<Ticket> tickets)
        {
            
        }

        public Ticket DeleteTicket(int id)
        {
            Ticket ticket = new Ticket();
            ticket.Id = 0;
            ticket.FirstName = "Andreas";
            ticket.LastName = "Strand";

            ticket.Order = new Order
            {
                Tickets = new List<Ticket>(),
                Customer = new Customer
                {
                    Firstname = "Andreas",
                    Lastname = "",
                    Telephone = "",
                    Email = ""
                }
            };

            ticket.Order.Tickets.Add(ticket);

            return ticket;
        }
    }
}
