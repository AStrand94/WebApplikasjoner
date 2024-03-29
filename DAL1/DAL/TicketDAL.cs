﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    [ExcludeFromCodeCoverage]
    public class TicketDAL : ITicketDAL
    {
        

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
