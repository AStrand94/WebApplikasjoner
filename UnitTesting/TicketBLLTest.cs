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

        private static ITicketDAL dal = new TicketStub();
        private static TicketBLL bll = new TicketBLL(dal);


        [TestMethod]
        public void TestDeleteTicket()
        {
            Ticket ticket = bll.DeleteTicket(0);

            Assert.AreEqual(ticket.FirstName, "Andreas");
        }
    }
}
