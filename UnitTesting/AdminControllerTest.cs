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
using System.Linq;

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
                new RouteBLL(new RouteStub(), new AirportStub()),
                new TicketBLL(new TicketStub()));

            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["loggedIn"] = true;
        }

        [TestMethod]
        public void Login()
        {
            //Arrange
            var username = "admin";
            var password = "admin";

            //Act
            var result = (RedirectToRouteResult)controller.Login(username, password);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);        }

        [TestMethod]
        public void Login_Fail()
        {
            //Arrange
            var username = "wrong";
            var password = "wrong";

            //Act
            var result = (RedirectToRouteResult)controller.Login(username, password);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Logout()
        {
            //Arrange
            controller.Session["loggedIn"] = false;

            //Act
            var result = (RedirectToRouteResult)controller.LogOut();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Index()
        {
            //Arrange

            // Act
            var resultat = (ViewResult)controller.Index();

            // Assert
            Assert.AreEqual(resultat.ViewName, "");
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

        [TestMethod]
        public void Airports()
        {
            //Arrange
            var expectedResult = new List<Airport>();
            var airport = new Airport()
            {
                Id = 1,
                Name = "Gardermoen",
                Code = "OSL",
                Country = "Norway",
                City = "Oslo"
            };

            expectedResult.Add(airport);
            expectedResult.Add(airport);
            expectedResult.Add(airport);

            //Act
            var result = (ViewResult)controller.Airports();
            var resultList = (List<Airport>)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");

            for (var i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].Id, resultList[i].Id);
                Assert.AreEqual(expectedResult[i].Name, resultList[i].Name);
                Assert.AreEqual(expectedResult[i].Code, resultList[i].Code);
                Assert.AreEqual(expectedResult[i].Country, resultList[i].Country);
                Assert.AreEqual(expectedResult[i].City, resultList[i].City);
            }
        }

        [TestMethod]
        public void Customers()
        {
            //Arrange
            var expectedResult = new List<Customer>();
            var customer = new Customer()
            {
                Id = 1,
                Firstname = "Ola",
                Lastname = "Normann",
                Email = "olanormann@gmail.com",
                Telephone = "12345678"
            };

            expectedResult.Add(customer);
            expectedResult.Add(customer);
            expectedResult.Add(customer);

            //Act
            var result = (ViewResult)controller.Customers();
            var resultList = (List<Customer>)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");

            for (var i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].Id, resultList[i].Id);
                Assert.AreEqual(expectedResult[i].Firstname, resultList[i].Firstname);
                Assert.AreEqual(expectedResult[i].Lastname, resultList[i].Lastname);
                Assert.AreEqual(expectedResult[i].Email, resultList[i].Email);
                Assert.AreEqual(expectedResult[i].Telephone, resultList[i].Telephone);
            }
        }

        [TestMethod]
        public void Flights()
        {
            //Arrange
            var expectedResult = new List<Flight>();
            var flight = new Flight()
            {
                Id = 1,
                Price = 100
            };

            expectedResult.Add(flight);
            expectedResult.Add(flight);
            expectedResult.Add(flight);

            //Act
            var result = (ViewResult)controller.Flights();
            var resultList = (List<Flight>)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");

            for (var i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].Id, resultList[i].Id);
                Assert.AreEqual(expectedResult[i].Price, resultList[i].Price);
            }
        }

        [TestMethod]
        public void Routes()
        {
            //Arrange
            var expectedResult = new List<Route>();

            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var route = new Route()
            {
                Id = 1,
                FromAirport = airport,
                ToAirport = airport,
                FlightTime = new TimeSpan(10, 0, 0)
            };

            expectedResult.Add(route);
            expectedResult.Add(route);
            expectedResult.Add(route);

            //Act
            var result = (ViewResult)controller.Routes();
            var resultList = (List<Route>)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");

            for (var i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].Id, resultList[i].Id);
                Assert.AreEqual(expectedResult[i].FlightTime, resultList[i].FlightTime);
            }
        }

        [TestMethod]
        public void UpdateRoute()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var route = new Route()
            {
                Id = 1,
                FromAirport = airport,
                ToAirport = airport,
                FlightTime = new TimeSpan(10, 0, 0)
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateRoute(route);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Routes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteRoute()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var route = new Route()
            {
                Id = 1,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteRoute(route.Id);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Routes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Order()
        {
            //Arrange
            var expectedResult = new Order()
            {
                Id = 1,
                Tickets = new List<Ticket>(),
                Customer = new Customer
                {
                    Firstname = "Andreas",
                    Lastname = "Strand",
                    Telephone = "00000000",
                    Email = "a@a.a"
                }
            };

            //Act
            var result = (ViewResult)controller.Orders();
            var resultList = (List<Order>)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");

            
        }


    }
}
