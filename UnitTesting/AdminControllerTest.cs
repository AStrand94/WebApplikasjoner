﻿using System;
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
using DTO;

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
                new AirportBLL(new AirportStub(), new RouteStub()),
                new CustomerBLL(new CustomerStub()),
                new FlightBLL(new FlightStub(), new RouteStub(), new AirplaneStub(), new AirportStub()),
                new LoginBLL(new LoginStub()),
                new OrderBLL(new OrderStub(), new CustomerStub(), new FlightStub(), new TicketStub(), new ReferenceGeneratorStub()),
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
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

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
            var result = (ViewResult)controller.Index();

            // Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void Index_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.Index();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
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
        public void Airplanes_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.Airplanes();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
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
        public void Airports_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.Airports();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
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
        public void Customers_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.Customers();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
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
        public void Flights_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.Flights();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
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
        public void Routes_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.Routes();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
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

            var airport1 = new Airport
            {
                Id = 2,
                Name = "Gardermoen"
            };

            var route = new Route()
            {
                Id = 1,
                FromAirport = airport,
                ToAirport = airport1,
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
        public void UpdateRoute_With_Id_0()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airport1 = new Airport
            {
                Id = 2,
                Name = "Gardermoen"
            };

            var route = new Route()
            {
                Id = 0,
                FromAirport = airport,
                ToAirport = airport1,
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
        public void UpdateRoute_NoFlightTime()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airport1 = new Airport
            {
                Id = 2,
                Name = "Gardermoen"
            };

            var route = new Route()
            {
                Id = 1,
                FromAirport = airport,
                ToAirport = airport1
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateRoute(route);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Routes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateRoute_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.UpdateRoute(new Route());

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateRoute_Fail()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var route = new Route()
            {
                Id = 0,
                FromAirport = airport,
                ToAirport = airport,
                FlightTime = new TimeSpan(10, 0, 0)
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateRoute(route);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Routes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.IsTrue(errorMessage.ToString().Length > 0);
        }

        [TestMethod]
        public void UpdateRoute_Fail_ModelState()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var route = new Route()
            {
                Id = 0,
                FromAirport = airport,
                ToAirport = airport,
            };

            //Act
            controller.ModelState.AddModelError("FlightTime", "FlightTime need to be set!");
            var result = (RedirectToRouteResult)controller.UpdateRoute(route);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Routes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.IsTrue(controller.ModelState.Count() == 1);
        }

        [TestMethod]
        public void UpdateRoute_Success ()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airport1 = new Airport
            {
                Id = 2,
                Name = "London"
            };

            var route = new Route()
            {
                Id = 1,
                FromAirport = airport,
                ToAirport = airport1,
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
        public void DeleteRoute_Success()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var route = new Route()
            {
                Id = 1000,
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
        public void DeleteRoute_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.DeleteRoute(1);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteRoute_Fail()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var route = new Route()
            {
                Id = 0,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteRoute(route.Id);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Routes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Route does not exist");
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

			var t = new Ticket
			{
				FirstName = "",
				LastName = "",
				Order = expectedResult
			};
			expectedResult.Tickets.Add(t);

			//Act
			var result = (ViewResult)controller.Order(expectedResult.Id);
            var resultOrder = (Order)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(expectedResult.Id, resultOrder.Id);
            Assert.AreEqual(expectedResult.Customer.Firstname, resultOrder.Customer.Firstname);
            Assert.AreEqual(expectedResult.Customer.Lastname, resultOrder.Customer.Lastname);
            Assert.AreEqual(expectedResult.Customer.Telephone, resultOrder.Customer.Telephone);
            Assert.AreEqual(expectedResult.Customer.Email, resultOrder.Customer.Email);
            Assert.AreEqual(expectedResult.Tickets.Count(), resultOrder.Tickets.Count());
        }

        [TestMethod]
        public void Order_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.Order(1);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Order_Fail()
        {
            //Arrange
            var expectedResult = new Order()
            {
                Id = 0,
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
            var result = (RedirectToRouteResult)controller.Order(expectedResult.Id);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteCustomer()
        {
            //Arrange
            var expectedResult = new Customer()
            {
                Id = 1,
                Firstname = "Ola",
                Lastname = "Normann",
                Email = "olanormann@gmail.com",
                Telephone = "12345678"
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteCustomer(expectedResult.Id);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Customers", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteCustomer_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.DeleteCustomer(1);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteCustomer_Fail_Has_Order()
        {
            //Arrange
            var expectedResult = new Customer()
            {
                Id = 100,
                Firstname = "Ola",
                Lastname = "Normann",
                Email = "olanormann@gmail.com",
                Telephone = "12345678"
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteCustomer(expectedResult.Id);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Customers", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "This customer has orders connected, and so cannot be deleted!");
        }

        [TestMethod]
        public void DeleteCustomer_Fail_Null()
        {
            //Arrange
            var expectedResult = new Customer()
            {
                Id = 0,
                Firstname = "Ola",
                Lastname = "Normann",
                Email = "olanormann@gmail.com",
                Telephone = "12345678"
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteCustomer(expectedResult.Id);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Customers", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "An error occured");
        }

        [TestMethod]
        public void DeleteAirport()
        {
            //Arrange
            var expectedResult = new Airport()
            {
                Id = 1,
                Name = "Gardermoen",
                Code = "OSL",
                Country = "Norway",
                City = "Oslo"
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteAirport(expectedResult.Id);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airports", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteAirport_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.DeleteAirport(1);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteAirport_Fail_Route_Has_Airport()
        {
            //Arrange
            var expectedResult = new Airport()
            {
                Id = 100,
                Name = "Gardermoen",
                Code = "OSL",
                Country = "Norway",
                City = "Oslo"
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteAirport(expectedResult.Id);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airports", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "This airport has routes connected and cannot be deleted!");
        }

        [TestMethod]
        public void DeleteAirport_Fail_Null()
        {
            //Arrange
            var expectedResult = new Airport()
            {
                Id = 0,
                Name = "Gardermoen",
                Code = "OSL",
                Country = "Norway",
                City = "Oslo"
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteAirport(expectedResult.Id);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airports", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "An error occured");
        }

        [TestMethod]
        public void DeleteOrder()
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
            var result = (RedirectToRouteResult)controller.DeleteOrder(expectedResult.Id);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Orders", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteOrder_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.DeleteOrder(1);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteOrder_Fail_Null()
        {
            //Arrange
            var expectedResult = new Order()
            {
                Id = 0,
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
            var result = (RedirectToRouteResult)controller.DeleteOrder(expectedResult.Id);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Orders", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "An error occured");
        }

        [TestMethod]
        public void CreateOrder()
        {
            //Arrange

            // Act
            var result = (ViewResult)controller.CreateOrder();

            // Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void CreateOrder_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateOrder();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateOrderInput()
        {
            var person = new PersonDTO
            {
                firstName = "Ola",
                lastName = "Normann"
            };

            var persons = new List<PersonDTO>();
            persons.Add(person);
            persons.Add(person);

            //Arrange
            var expectedResult = new OrderDTO
            {
                CustomerId = 100,
                FlightId = 100,
                Travellers = persons
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateOrder(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Orders", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateOrderInput_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateOrder(new OrderDTO());

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateOrderInput_Fail_Null_Id()
        {
            var person = new PersonDTO
            {
                firstName = "Ola",
                lastName = "Normann"
            };

            var persons = new List<PersonDTO>();
            persons.Add(person);
            persons.Add(person);

            //Arrange
            var expectedResult = new OrderDTO
            {
                CustomerId = 0,
                FlightId = 1,
                Travellers = persons
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteOrder(expectedResult.CustomerId);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Orders", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateOrderInput_Fail_Null()
        {
            var person = new PersonDTO
            {
                lastName = "Normann"
            };

            var persons = new List<PersonDTO>();
            persons.Add(person);
            persons.Add(person);

            //Arrange
            var expectedResult = new OrderDTO
            {
                CustomerId = 100,
                FlightId = 100,
                Travellers = persons
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateOrder(expectedResult);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("CreateOrder", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Must fill out firstname and lastname!");
        }

        [TestMethod]
        public void CreateOrderInput_Fail_CustomerDoesNotExist()
        {
            var person = new PersonDTO
            {
                firstName = "Ola",
                lastName = "Normann"
            };

            var persons = new List<PersonDTO>();
            persons.Add(person);
            persons.Add(person);

            //Arrange
            var expectedResult = new OrderDTO
            {
                CustomerId = 1,
                FlightId = 100,
                Travellers = persons
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateOrder(expectedResult);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("CreateOrder", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Customer does not exist");
        }

        [TestMethod]
        public void CreateOrderInput_Fail_FlightDoesNotExist()
        {
            var person = new PersonDTO
            {
                firstName = "Ola",
                lastName = "Normann"
            };

            var persons = new List<PersonDTO>();
            persons.Add(person);
            persons.Add(person);

            //Arrange
            var expectedResult = new OrderDTO
            {
                CustomerId = 100,
                FlightId = 1,
                Travellers = persons
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateOrder(expectedResult);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("CreateOrder", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Flight does not exist");
        }

        [TestMethod]
        public void CreateOrder_Success()
        {
            OrderDTO dto = new OrderDTO();
            dto.Travellers = new List<PersonDTO>();
            dto.CustomerId = 100;
            dto.FlightId = 100;

            PersonDTO p1 = new PersonDTO();
            p1.firstName = "Andreas";
            p1.lastName = "Strand";

            PersonDTO p2 = new PersonDTO();
            p2.firstName = "Ola";
            p2.lastName = "Nordmann";

            dto.Travellers.Add(p1); dto.Travellers.Add(p2);

            var result = (RedirectToRouteResult)controller.CreateOrder(dto);

            var errorMessage = controller.TempData["errorMessage"];

            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Orders", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateCustomer()
        {
            //Arrange

            // Act
            var result = (ViewResult)controller.CreateCustomer();

            // Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void CreateCustomer_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateCustomer();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateCustomerInput()
        {
            //Arrange
            var expectedResult = new Customer
            {
                Id = 1,
                Firstname = "Ola",
                Lastname = "Normann",
                Email = "olanormann@gmail.com",
                Telephone = "12345678"
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateCustomer(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Customers", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateCustomerInput_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateCustomer(new Customer());

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateCustomerInput_Fail_Null_Id()
        {
            //Arrange
            var expectedResult = new Customer
            {
                Id = 0,
                Firstname = "Ola",
                Lastname = "Normann",
                Email = "olanormann@gmail.com",
                Telephone = "12345678"
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateCustomer(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Customers", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateCustomerInput_Fail_Null()
        {
            //Arrange
            var expectedResult = new Customer
            {
                Id = 1,
            };

            //Act
            var result = (ViewResult)controller.CreateCustomer(expectedResult);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(errorMessage, "All fields must be filled out!");
        }

        [TestMethod]
        public void CreateCustomerInput_Fail_ModelState()
        {
            //Arrange
            var expectedResult = new Customer
            {
                Id = 1,
            };

            //Act
            controller.ModelState.AddModelError("firstName", "First name need to be set");
            var result = (ViewResult)controller.CreateCustomer(expectedResult);

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsTrue(controller.ModelState.Count() == 1);
        }

        [TestMethod]
        public void CreateAirport()
        {
            //Arrange

            // Act
            var result = (ViewResult)controller.CreateAirport();

            // Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void CreateAirport_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateAirport();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateAirportInput()
        {
            //Arrange
            var expectedResult = new Airport
            {
                Id = 1,
                Name = "Gardermoen",
                Code = "OSL",
                Country = "Norway",
                City = "Oslo"
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateAirport(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airports", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateAirportInput_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateAirport(new Airport());

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateAirportInput_Fail_Null()
        {
            //Arrange
            var expectedResult = new Airport
            {
                Id = 1,
                Name = "admin",
                Code = "OSL",
                Country = "Norway",
                City = "Oslo"
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateAirport(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airports", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateAirportInput_Fail_ModelState()
        {
            //Arrange
            var expectedResult = new Airport
            {
                Id = 1,
            };

            //Act
            controller.ViewData.ModelState.AddModelError("Name", "Invalid name");
            var result = (RedirectToRouteResult)controller.CreateAirport(expectedResult);


            //Assert
            Assert.AreEqual("Airports", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.IsTrue(controller.ModelState.Count() == 1);
        }

        [TestMethod]
        public void CreateAirportInput_Fail_Null_Id()
        {
            //Arrange
            var expectedResult = new Airport
            {
                Id = 0,
            };

            //Act
            controller.ViewData.ModelState.AddModelError("Name", "Invalid name");
            var result = (RedirectToRouteResult)controller.CreateAirport(expectedResult);


            //Assert
            Assert.AreEqual("Airports", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.IsTrue(controller.ModelState.Count() == 1);
        }

        [TestMethod]
        public void CreateFlight()
        {
            //Arrange

            // Act
            var result = (ViewResult)controller.CreateFlight();

            // Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void CreateFlight_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateFlight();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateFlightInput()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            var expectedResult = new Flight
            {
                Id = 1,
                Price = 100,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1)
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateFlight(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Flights", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateFlightInput_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateFlight(new Flight());

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateFlightInput_Fail_Null_Id()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            var expectedResult = new Flight
            {
                Id = 1,
                Price = 1000,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1)
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateFlight(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Flights", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateFlightInput_Fail_Price()
        {
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            //Arrange
            var expectedResult = new Flight
            {
                Id = 1,
                Price = 0,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1)
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateFlight(expectedResult);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("CreateFlight", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Price cannot be 0!\n\r");
        }

        [TestMethod]
        public void CreateFlightInput_Fail_RouteDoesNotExist()
        {
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 0,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            //Arrange
            var expectedResult = new Flight
            {
                Id = 1,
                Price = 110,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1)
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateFlight(expectedResult);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("CreateFlight", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Route does not exist");
        }

        [TestMethod]
        public void CreateFlightInput_Fail_ModelState()
        {
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 0,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            //Arrange
            var expectedResult = new Flight
            {
                Id = 1,
                Price = 110,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1)
            };

            //Act
            controller.ModelState.AddModelError("Flights", "Route does not exist");
            var result = (RedirectToRouteResult)controller.CreateFlight(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("CreateFlight", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.IsTrue(controller.ModelState.Count() == 1);
        }

        [TestMethod]
        public void CreateFlightInput_Fail_AirplaneDoesNotExist()
        {
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 0,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            //Arrange
            var expectedResult = new Flight
            {
                Id = 1,
                Price = 110,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1)
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateFlight(expectedResult);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("CreateFlight", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Airplane does not exist");
        }

        [TestMethod]
        public void CreateFlightInput_Fail_FlightBeforeNow()
        {
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            //Arrange
            var expectedResult = new Flight
            {
                Id = 1,
                Price = 110,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2014, 1, 1)
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateFlight(expectedResult);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("CreateFlight", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Flight cannot be before now!");
        }

        [TestMethod]
        public void CreateAirplane()
        {
            //Arrange

            // Act
            var result = (ViewResult)controller.CreateAirplane();

            // Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void CreateAirplane_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateAirplane();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateAirplaneInput()
        {
            //Arrange
            var expectedResult = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateAirplane(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airplanes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateAirplaneInput_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateAirplane(new Airplane());

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateAirplaneInput_Fail_Null()
        {
            //Arrange
            var expectedResult = new Airplane
            {
                Id = 1,
            };

            //Act
            var modelError = "Model not given";
            controller.ViewData.ModelState.AddModelError("Firstname", modelError);
            var result = (ViewResult)controller.CreateAirplane(expectedResult);

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsTrue(controller.ViewData.ModelState.Count() == 1, modelError);
        }

        [TestMethod]
        public void CreateAirplaneInput_Fail_Null_Id()
        {
            //Arrange
            var expectedResult = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 0
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateAirplane(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airplanes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateRoute()
        {
            //Arrange

            // Act
            var result = (ViewResult)controller.CreateRoute();

            // Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void CreateRoue_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateRoute();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateRouteInput()
        {
            //Arrange
            var airport1 = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airport2 = new Airport
            {
                Id = 2,
                Name = "Stockholm"
            };

            var route = new Route()
            {
                Id = 1,
                FromAirport = airport1,
                ToAirport = airport2,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            //Act
            var result = (RedirectToRouteResult)controller.CreateRoute(route);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Routes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateRouteInput_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.CreateRoute(new Route());

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CreateRouteInput_Fail_Null()
        {
            //Arrange
            var route = new Route()
            {
                Id = 1,
                Flights = new List<Flight>(),
            };

            //Act
            var result = (ViewResult)controller.CreateRoute(route);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(errorMessage, "All fields must be filled out!");
        }

        [TestMethod]
        public void CreateRouteInput_Fail_ModelState()
        {
            //Arrange
            var route = new Route()
            {
                Id = 1,
                Flights = new List<Flight>(),
            };

            //Act
            controller.ModelState.AddModelError("Route", "All fields must be filled out");
            var result = (ViewResult)controller.CreateRoute(route);

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsTrue(controller.ModelState.Count() == 1);
        }

        [TestMethod]
        public void CreateRouteInput_Fail_SameAirport()
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
            var result = (ViewResult)controller.CreateRoute(route);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(errorMessage, "Airports can not be the same!");
        }

		[TestMethod]
		public void CreateRoute_With_Id_0()
		{
			var airport1 = new Airport
			{
				Id = 1,
				Name = "Gardermoen"
			};

			var airport2 = new Airport
			{
				Id = 2,
				Name = "Stockholm"
			};

			var route = new Route()
			{
				Id = 0,
				FromAirport = airport1,
				ToAirport = airport2,
				Flights = new List<Flight>(),
				FlightTime = new TimeSpan(10, 0, 0)
			};

			//Act
			var result = (RedirectToRouteResult)controller.CreateRoute(route);

			//Assert
			Assert.AreEqual(result.RouteName, "");
			Assert.AreEqual("Routes", result.RouteValues["action"]);
			Assert.AreEqual("Admin", result.RouteValues["controller"]);
		}

        [TestMethod]
        public void Orders()
        {
            //Arrange
            var expectedResult = new List<Order>();
            var order = new Order()
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

			var t = new Ticket
			{
				FirstName = "",
				LastName = "",
				Order = order
			};
			order.Tickets.Add(t);

			expectedResult.Add(order);
            expectedResult.Add(order);
            expectedResult.Add(order);

            //Act
            var result = (ViewResult)controller.Orders();
            var resultList = (List<Order>)result.Model;

            //Assert
            Assert.AreEqual(result.ViewName, "");

            for (var i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].Id, resultList[i].Id);
                Assert.AreEqual(expectedResult[i].Tickets.Count(), resultList[i].Tickets.Count());
                Assert.AreEqual(expectedResult[i].Customer.Firstname, resultList[i].Customer.Firstname);
                Assert.AreEqual(expectedResult[i].Customer.Lastname, resultList[i].Customer.Lastname);
                Assert.AreEqual(expectedResult[i].Customer.Telephone, resultList[i].Customer.Telephone);
                Assert.AreEqual(expectedResult[i].Customer.Email, resultList[i].Customer.Email);
            }
        }

        [TestMethod]
        public void Orders_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.Orders();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateCustomer()
        {
            //Arrange
            var customer = new Customer
            {
                Id = 1,
                Firstname = "Ola",
                Lastname = "Normann",
                Email = "olanormann@gmail.com",
                Telephone = "12345678"
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateCustomer(customer);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Customers", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateCustomer_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.UpdateCustomer(new Customer());

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateCustomer_Fail_Null()
        {
            //Arrange
            var customer = new Customer
            {
                Id = 0,
                Firstname = "Ola",
                Lastname = "Normann",
                Email = "olanormann@gmail.com",
                Telephone = "12345678"
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateCustomer(customer);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Customers", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateCustomer_Fail_ModelState()
        {
            //Arrange
            var customer = new Customer
            {
                Id = 1,
                Firstname = "Ola",
                Lastname = "Normann",
                Email = "olanormann@gmail.com",
                Telephone = "12345678"
            };

            var modelError = "Firstname not given";
            controller.ViewData.ModelState.AddModelError("Firstname", modelError);

            //Act
            var result = (RedirectToRouteResult)controller.UpdateCustomer(customer);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Customers", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.IsTrue(controller.ViewData.ModelState.Count == 1, modelError);
        }

        [TestMethod]
        public void UpdateAirport()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen",
                Code = "OSL",
                Country = "Norway",
                City = "Oslo"
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateAirport(airport);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airports", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateAirport_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.UpdateAirport(new Airport());

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateAirport_Fail_Null()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 0,
                Name = "Gardermoen",
                Code = "OSL",
                Country = "Norway",
                City = "Oslo"
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateAirport(airport);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airports", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateAirports_Fail_ModelState()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen",
                Code = "OSL",
                Country = "Norway",
                City = "Oslo"
            };

            var modelError = "Name not given";
            controller.ViewData.ModelState.AddModelError("Name", modelError);

            //Act
            var result = (RedirectToRouteResult)controller.UpdateAirport(airport);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airports", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.IsTrue(controller.ViewData.ModelState.Count == 1, modelError);
        }

        [TestMethod]
        public void UpdateAirplane()
        {
            //Arrange
            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateAirplane(airplane);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airplanes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateAirplane_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.UpdateAirplane(new Airplane());

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateAirplane_Fail_ModelNull()
        {
            //Arrange
            var airplane = new Airplane
            {
                Id = 1,
                Seats = 148
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateAirplane(airplane);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airplanes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Model must be filled!");
        }

        [TestMethod]
        public void UpdateAirplane_Fail_Model_Null()
        {
            //Arrange
            var airplane = new Airplane
            {
                Id = 0,
                Seats = 148
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateAirplane(airplane);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airplanes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Model must be filled!");
        }

        [TestMethod]
        public void UpdateAirplane_Fail_ModelState()
        {
            //Arrange
            var airplane = new Airplane
            {
                Id = 1,
                Seats = 148
            };

            //Act
            controller.ModelState.AddModelError("Airplane", "Airplane can not be null");
            var result = (RedirectToRouteResult)controller.UpdateAirplane(airplane);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airplanes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.IsTrue(controller.ModelState.Count() == 1);
        }

        [TestMethod]
        public void UpdateAirplane_Fail_SeatsNull()
        {
            //Arrange
            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateAirplane(airplane);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airplanes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Number of seats cannot be 0!");
        }

        [TestMethod]
        public void UpdateFlight()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            var expectedResult = new Flight
            {
                Id = 1,
                Price = 100,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1)
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateFlight(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Flights", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateFlight_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.UpdateFlight(new Flight());

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateFlight_Fail_Time()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            var expectedResult = new Flight
            {
                Id = 1,
                Price = 100,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2015, 1, 1)
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateFlight(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Flights", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateFlight_Fail_Null_Id()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            var expectedResult = new Flight
            {
                Id = 1,
                Price = 1000,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1)
            };

            //Act
            var result = (RedirectToRouteResult)controller.UpdateFlight(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Flights", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void UpdateFlight_Fail_ModelState()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            var expectedResult = new Flight
            {
                Id = 1,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1)
            };

            //Act
            controller.ModelState.AddModelError("Price", "Price can not be 0");
            var result = (RedirectToRouteResult)controller.UpdateFlight(expectedResult);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Flights", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.IsTrue(controller.ModelState.Count() == 1);
        }

        [TestMethod]
        public void DeleteFlight()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            var ticket = new Ticket()
            {
                Id = 1,
                FirstName = "Ola",
                LastName = "Normann",
            };

            var tickets = new List<Ticket>();
            tickets.Add(ticket);
            tickets.Add(ticket);

            //Arrange
            var expectedResult = new Flight
            {
                Id = 1,
                Price = 100,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1),
                Tickets = tickets
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteFlight(expectedResult.Id);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Flights", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteFlight_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.DeleteFlight(1);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteFlight_Good()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            var tickets = new List<Ticket>();;

            //Arrange
            var expectedResult = new Flight
            {
                Id = 100,
                Price = 100,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1),
                Tickets = tickets
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteFlight(expectedResult.Id);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Flights", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteFlight_Fail_Null_Id()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            var tickets = new List<Ticket>(); ;

            //Arrange
            var expectedResult = new Flight
            {
                Id = 500,
                Price = 100,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1),
                Tickets = tickets
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteFlight(expectedResult.Id);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Flights", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteFlight_Fail_Null()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            var ticket = new Ticket()
            {
                Id = 1,
                FirstName = "Ola",
                LastName = "Normann",
            };

            var tickets = new List<Ticket>();
            tickets.Add(ticket);
            tickets.Add(ticket);

            var expectedResult = new Flight
            {
                Id = 0,
                Price = 100,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1),
                Tickets = tickets
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteFlight(expectedResult.Id);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Flights", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage, "Flight does not exist");
        }

        [TestMethod]
        public void DeleteFlight_Fail_HasOrders()
        {
            //Arrange
            var airport = new Airport
            {
                Id = 1,
                Name = "Gardermoen"
            };

            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            var route = new Route()
            {
                Id = 100,
                FromAirport = airport,
                ToAirport = airport,
                Flights = new List<Flight>(),
                FlightTime = new TimeSpan(10, 0, 0)
            };

            var ticket = new Ticket()
            {
                Id = 1,
                FirstName = "Ola",
                LastName = "Normann",
            };

            var tickets = new List<Ticket>();
            tickets.Add(ticket);
            tickets.Add(ticket);

            var expectedResult = new Flight
            {
                Id = 1,
                Price = 100,
                Route = route,
                Airplane = airplane,
                Time = new DateTime(2018, 1, 1),
                Tickets = tickets
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteFlight(expectedResult.Id);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Flights", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage.ToString().Substring(0,10), "Cannot delete flight, must delete orders: ".Substring(0, 10));
        }

        [TestMethod]
        public void DeleteAirplane()
        {
            //Arrange
            var airplane = new Airplane
            {
                Id = 1,
                Model = "Boeing 737",
                Seats = 148
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteAirplane(airplane.Id);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airplanes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteAirplane_NotLoggedIn()
        {
            //Arrange
            controller.Session["loggedIn"] = false;
            var result = (RedirectToRouteResult)controller.DeleteAirplane(1);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeleteAirplane_Fail_NoFlight()
        {
            //Arrange
            var airplane = new Airplane
            {
                Id = 0,
                Model = "Boeing 737",
                Seats = 148
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteAirplane(airplane.Id);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airplanes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage.ToString().Substring(0,5), "No flight with id".Substring(0,5));
        }

        [TestMethod]
        public void DeleteAirplane_Fail_HasFlights()
        {
            //Arrange
            var airplane = new Airplane
            {
                Id = 100,
                Model = "Boeing 737",
                Seats = 148
            };

            //Act
            var result = (RedirectToRouteResult)controller.DeleteAirplane(airplane.Id);
            var errorMessage = controller.TempData["errorMessage"];

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual("Airplanes", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
            Assert.AreEqual(errorMessage.ToString().Substring(0, 5), "Cannot delete. There are flights connected to airplane".Substring(0, 5));
        }

        [TestMethod]
        public void UserIsLoggedIn()
        {
            //Arrange

            //Act
            var result = controller.UserIsLoggedIn();

            //Assert
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void UserIsLoggedIn_Fail()
        {
            //Arrange
            controller.Session["loggedIn"] = false;

            //Act
            var result = controller.UserIsLoggedIn();

            //Assert
            Assert.AreEqual(result, false);
        }
    }
}
