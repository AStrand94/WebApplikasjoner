using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class TicketBLL : ITicketBLL
    {
        private ITicketDAL ticket;

        public TicketBLL()
        {
            ticket = new TicketDAL();
        }

        public TicketBLL(ITicketDAL stub)
        {
            ticket = stub;
        }

        public Ticket DeleteTicket(int id)
        {
            return new TicketDAL().DeleteTicket(id);
        }

    }
}
