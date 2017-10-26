using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class TicketDAL : ITicketDAL
    {
        public void addTickets(IEnumerable<Ticket> tickets)
        {
            using (DB db = new DB())
            {
                db.Orders.Add(((List<Ticket>)tickets)[0].Order);
                db.Tickets.AddRange(tickets);
                db.SaveChanges();
            }
        }

        public Ticket DeleteTicket(int id)
        {
            using (DB db = new DB())
            {
                Ticket ticket = db.Tickets.Where(t => t.Id == id).Single();

                if (ticket != null)
                {
                    db.Tickets.Attach(ticket);
                    ticket = db.Tickets.Remove(ticket);
                    db.SaveChanges();
                }

                return ticket;
            }
        }
    }
}
