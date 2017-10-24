using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public interface ITicketDAL
    {
        void addTickets(IEnumerable<Ticket> tickets);
        Ticket DeleteTicket(int id);
    }
}