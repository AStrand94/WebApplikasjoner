using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public interface ITicketDAL
    {
        Ticket DeleteTicket(int id);
    }
}