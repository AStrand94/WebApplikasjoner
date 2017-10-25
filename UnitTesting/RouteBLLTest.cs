using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication3.DAL;
using WebApplication3.Model;
using System.Collections.Generic;
using WebApplication3.BLL;

namespace UnitTesting
{
    [TestClass]
    public class RouteBLLTest
    {
        private static IRouteDAL dal = new RouteStub();
        private static RouteBLL bll = new RouteBLL(dal);
        
        [TestMethod]
        public void TestGetAllRoutes()
        {
            List<Route> routes = (List<Route>)bll.GetAllRoutes();
            
            Assert.AreEqual(routes[0].FromAirport.Name,"Paris airport");
            Assert.AreEqual(routes[1].FromAirport.Name, "Gardermoen");
        }

        [TestMethod]
        public void TestGetAllRoutesConnections()
        {
            List<Route> routes = (List<Route>)bll.GetAllRoutesConnections();

            Assert.AreEqual(routes[0].FromAirport.Name, "Paris airport");
            Assert.AreEqual(routes[1].FromAirport.Name, "Gardermoen");
        }

        [TestMethod]
        public void TestRouteHasAirport()
        {
            Assert.IsFalse(bll.RouteHasAirport(1));
        }

        [TestMethod]
        public void TestDeleteRoute()
        {
            Route route = bll.DeleteRoute(0);

            Assert.AreEqual(route.ToAirport.Name, "Gardermoen");
        }

        [TestMethod]
        public void TestCanDeleteRoute()
        {
            String result = bll.CanDeleteRoute(0);

            Assert.AreEqual(result,"");
        }

        [TestMethod]
        public void TestAddRoute()
        {

        }


    }
}
