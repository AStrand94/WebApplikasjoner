using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class TicketBLL : ITicketBLL
    {
        private ITicketDAL dal;

        [ExcludeFromCodeCoverage]
        public TicketBLL()
        {
            dal = new TicketDAL();
        }

        public TicketBLL(ITicketDAL stub)
        {
            dal = stub;
        }

        public Ticket DeleteTicket(int id)
        {
            return dal.DeleteTicket(id);
        }

    }
}
