using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class TicketDAL
    {
        private DB db;

        public TicketDAL(DB db)
        {
            this.db = db;
        }

        public void addTickets(IEnumerable<Ticket> tickets)
        {
            db.Tickets.AddRange(tickets);
            db.SaveChanges();
        }

        public Ticket DeleteTicket(int id)
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
