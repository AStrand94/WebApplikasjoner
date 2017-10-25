using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication3.DAL;
using WebApplication3.BLL;
using WebApplication3.Model;

namespace UnitTesting
{
    [TestClass]
    public class TicketBLLTest
    {
        const ITicketDAL dal = new TicketStub();
        const TicketBLL bll = new TicketBLL(dal);


        [TestMethod]
        public void TestDeleteTicket()
        {
            Ticket ticket = bll.DeleteTicket(0);

            Assert.Equals(ticket.FirstName, "Andreas");
        }
    }
}
