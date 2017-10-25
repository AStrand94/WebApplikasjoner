using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication3.Controllers;
using WebApplication3.BLL;
using WebApplication3.DAL;
using WebApplication3.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using Rhino.Mocks.Constraints;

namespace UnitTesting
{
    [TestClass]
    public class AdminControllerTest
    {
        private AdminController controller;

        [TestInitialize]
        public void Initialize()
        {
            this.controller = new AdminController(
                new AirplaneBLL(new AirplaneStub()),
                new AirportBLL(new AirportStub()),
                new CustomerBLL(new CustomerStub()),
                new FlightBLL(new FlightStub()),
                new LoginBLL(new LoginStub()),
                new OrderBLL(new OrderStub()),
                new RouteBLL(new RouteStub()),
                new TicketBLL(new TicketStub()));
        }

        [TestMethod]
        public void Login()
        {
            //Arrange
            var username = "admin";
            var password = "admin";

            //Act
            var result = (RedirectToRouteResult)controller.Login(username, password);

            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["loggedIn"] = true;
            controller.Session["loggedInUser"] = username;


            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values, "Index");


        }

        [TestMethod]
        public void Airplanes()
        {
            //Arrange
            var expectedResult = new List<Airplane>();
            var airplane = new Airplane()
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            expectedResult.Add(airplane);
            expectedResult.Add(airplane);
            expectedResult.Add(airplane);


            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["loggedIn"] = true;

            //Act
            var result = (ViewResult)controller.Airplanes();
            var resultList = (List<Airplane>)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");

            for (var i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].Id, resultList[i].Id);
                Assert.AreEqual(expectedResult[i].Model, resultList[i].Model);
                Assert.AreEqual(expectedResult[i].Seats, resultList[i].Seats);
            }

        }
    }
}
