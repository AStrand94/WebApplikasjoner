using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.BLL;
using WebApplication3.Model;
using DTO;

namespace WebApplication3.Controllers
{
    public class AdminController : Controller
    {
        private IAirplaneBLL airplaneBLL;
        private IAirportBLL airportBLL;

        public AdminController()
        {
            airplaneBLL = new AirplaneBLL();
            airportBLL = new AirportBLL();
        }

        //If unit testing
        public AdminController(IAirplaneBLL airplaneStub, IAirportBLL airportStub)
        {
            airplaneBLL = airplaneStub;
            airportBLL = airportStub;
        }

        public ActionResult Login(string username, string password)
        {
            LoginBLL loginBLL = new LoginBLL();

            if(loginBLL.checkLogin(username, password))
            {
                Session["loggedIn"] = true;
                Session["loggedInUser"] = username;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }
        
        public ActionResult LogOut()
        {
            Session["loggedIn"] = false;
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult Index()
        {
            if(!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            ViewBag.loggedInUser = Session["loggedInUser"];

            ViewBag.Customers = new CustomerBLL().GetAllCustomers().Count();
            ViewBag.Orders = new OrderBLL().GetAllOrders().Count();
            ViewBag.Flights = new FlightBLL().GetAllFlights().Count();
            ViewBag.Routes = new RouteBLL().GetAllRoutes().Count();
            ViewBag.Airplanes = new AirplaneBLL().AllAirplanes.Count();
            ViewBag.Airports = new AirportBLL().GetAllAirports().Count();

            return View();
        }

        public ActionResult Airplanes()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(airplaneBLL.AllAirplanes.OrderBy(a => a.Model));
        }

        public ActionResult Airports()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(new AirportBLL().GetAllAirports().OrderBy(a => a.Name));

        }

        public ActionResult Customers()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(new CustomerBLL().GetAllCustomersConnections().OrderBy(c => c.Firstname));
        }

        public ActionResult Flights()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData["allAirplanes"] = new AirplaneBLL().AllAirplanes;
            TempData["allRoutes"] = new RouteBLL().GetAllRoutesConnections();
            return View(new FlightBLL().GetAllFlightConnections());
        }

        public ActionResult Routes()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData["allAirports"] = new AirportBLL().GetAllAirports();
            return View(new RouteBLL().GetAllRoutes().OrderBy(r => r.FromAirport.Name));
        }

        [HttpPost]
        public ActionResult UpdateRoute(Route route)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            new RouteBLL().UpdateRoute(route);
            SetMessage("Route with id " + route.Id + " was successfully updated");
            return RedirectToAction("Routes");
        }

        [HttpGet]
        public ActionResult DeleteRoute(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            RouteBLL bll = new RouteBLL();
            string message = bll.CanDeleteRoute(id);

            if (message.Length == 0)
            {
                Route route = bll.DeleteRoute(id);
                SetMessage("Route with id " + route.Id + " has been deleted.");
            }
            else
            {
                SetErrorMessage(message);
            }

            return RedirectToAction("Routes");
        }

        [HttpGet]
        public ActionResult Order(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            Order o = new OrderBLL().GetOrder(id);
            if (o != null)
            {
                return View(o);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult DeleteCustomer(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            //delete customer
            CustomerBLL customerBLL = new CustomerBLL();

            if (!customerBLL.CanDelete(id))
            {
                SetErrorMessage("This customer has orders connected, and so cannot be deleted!");
                return RedirectToAction("Customers");
            }

            Customer customer = customerBLL.DeleteCustomer(id);
            if (customer != null) {
                SetMessage(customer.Firstname + " " + customer.Lastname + " successfully deleted");
            }
            else
            {
                SetErrorMessage("Ann error occured");
            }
               return RedirectToAction("Customers");
        }

        [HttpGet]
        public ActionResult DeleteAirport(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            AirportBLL airportBLL = new AirportBLL();

            if (!airportBLL.AirportIsUsedInRoutes(id))
            {
                SetErrorMessage("This airport has routes connected and cannot be deleted!");
                return RedirectToAction("Airports");
            }

            Airport airport = airportBLL.DeleteAirport(id);
            if (airport != null)
            {
                SetMessage(airport.Name + " successfully deleted");
            }
            else
            {
                SetErrorMessage("Ann error occured");
            }
            return RedirectToAction("Airports");
        }

        [HttpGet]
        public ActionResult DeleteOrder(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            OrderBLL orderBLL = new OrderBLL();

            Order order = orderBLL.GetOrder(id);

            if (order != null)
            {
                foreach (var ticket in order.Tickets)
                {
                    if (ticket != null)
                    {
                        new TicketBLL().DeleteTicket(ticket.Id);
                    }
                }
                if(order.Customer != null)
                {
                    CustomerBLL customerBLL = new CustomerBLL();
                    customerBLL.DeleteAssociatedOrder(order.Customer.Id, order.Id);
                }
                
                SetMessage(order.Reference + " successfully deleted");
            }
            else
            {
                SetErrorMessage("Ann error occured");
            }
            return RedirectToAction("Orders");
        }

        public ActionResult CreateOrder()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData["AllFlights"] = new FlightBLL().GetAllFlights();
            TempData["AllCustomers"] = new CustomerBLL().GetAllCustomers();

            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(OrderDTO dto)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            OrderBLL bll = new OrderBLL();

            String result = bll.CanCreateOrder(dto);

            if(result.Length > 0)
            {
                SetErrorMessage(result);
                return RedirectToAction("CreateOrder");
            }

            Order order = bll.CreateOrder(dto);
            SetMessage("Order with reference " + order.Reference + " was successfully created");

            return RedirectToAction("Orders");
        }

        public ActionResult CreateCustomer()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer customer)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            //Validate in server!!
            if (customer == null || customer.Firstname == null || customer.Lastname == null || customer.Telephone == null || customer.Email == null)
            {
                SetErrorMessage("All fields must be filled out!");
                return View();
            }
            new CustomerBLL().AddCustomer(customer);
            SetMessage(customer.Firstname + " " + customer.Lastname + " successfully created");
            return RedirectToAction("Customers");
        }

        public ActionResult CreateAirport()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateAirport(Airport airport)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (airport == null || airport.Name == null || airport.Code == null || airport.Country == null || airport.City == null)
            {
                SetErrorMessage("All fields must be filled out!");
                return View();
            }
            new AirportBLL().AddAirport(airport);
            SetMessage(airport.Name + " successfully created");
            return RedirectToAction("Airports");
        }
        
        public ActionResult CreateFlight()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData["allAirplanes"] = new AirplaneBLL().AllAirplanes;
            TempData["allRoutes"] = new RouteBLL().GetAllRoutes();

            return View();
        }

        [HttpPost]
        public ActionResult CreateFlight(Flight flight)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            FlightBLL bll = new FlightBLL();
            string result = bll.CanInsertFlight(flight);

            if(result.Length > 0)
            {
                SetErrorMessage(result);
                return RedirectToAction("CreateFlight");
            }

            flight = bll.InsertFlight(flight);

            SetMessage("Flight created with id: " + flight.Id);

            return RedirectToAction("Flights");
        }

        public ActionResult CreateAirplane()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateAirplane(Airplane airplane)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            AirplaneBLL bll = new AirplaneBLL();
            bll.InsertAirplane(airplane);

            return RedirectToAction("Airplanes");
        }

        public ActionResult CreateRoute()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData["allAirports"] = new AirportBLL().GetAllAirports();
            return View();
        }

        [HttpPost]
        public ActionResult CreateRoute(Route route)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (route.FromAirport == null || route.ToAirport == null || route.FlightTime == null)
            {
                SetErrorMessage("All fields must be filled out!");
                return View();
            }

            if(route.FromAirport.Id == route.ToAirport.Id)
            {
                SetErrorMessage("Airports can not be the same!");
                return View();
            }

            new RouteBLL().AddRoute(route);
            SetMessage(route.FromAirport.Name + " " + route.ToAirport.Name + " on " + route.FlightTime.ToString() + " successfully created");
            return RedirectToAction("Routes");
        }

        public ActionResult Orders()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(new OrderBLL().GetAllOrdersConnections().OrderBy(o => o.Reference));
        }

        [HttpPost]
        public ActionResult UpdateCustomer(Customer customer)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            new CustomerBLL().UpdateCustomer(customer);
            SetMessage("Customer with id " + customer.Id + " was successfully updated");
            return RedirectToAction("Customers");
        }

        [HttpPost]
        public ActionResult UpdateAirport(Airport airport)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            new AirportBLL().UpdateAirport(airport);
            SetMessage("Airport " + airport.Name + " was successfully updated");
            return RedirectToAction("Airports");
        }

        [HttpPost]
        public ActionResult UpdateFlight(Flight flight)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            FlightBLL bll = new FlightBLL();
            string result = bll.CanUpdateFlight(flight);
            if (result.Length == 0)
            {
                bll.UpdateFlight(flight);
            }
            else
            {
                SetErrorMessage(result);
            }

            return RedirectToAction("Flights");
        }

        [HttpPost]
        public ActionResult UpdateAirplane(Airplane airplane)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            AirplaneBLL bll = new AirplaneBLL();
            string result = bll.CanUpdateAirplane(airplane);

            if (result.Length > 0)
            {
                SetErrorMessage(result);
                return RedirectToAction("Airplanes");
            }

            airplane = bll.UpdateAirplane(airplane);
            return RedirectToAction("Airplanes");
        }

        [HttpGet]
        public ActionResult DeleteFlight(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            FlightBLL bll = new FlightBLL();
            string message = bll.CanDeleteFlight(id);

            if(message.Length == 0)
            {
                Flight flight = bll.DeleteFlight(id);
                SetMessage("Flight with id " + flight.Id + " with flight at " + flight.Time.ToShortDateString() + " has been deleted.");
            }
            else
            {
                SetErrorMessage(message);
            }

            return RedirectToAction("Flights");
        }

        [HttpGet]
        public ActionResult DeleteAirplane(int id)
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            AirplaneBLL bll = new AirplaneBLL();
            string result = bll.CanDeleteAirplane(id);

            if(result.Length > 0)
            {
                SetErrorMessage(result);
                return RedirectToAction("Airplanes");
            }

            Airplane airplane = bll.DeleteAirplane(id);
            SetMessage("Airplane " + airplane.Model + ", with id: " + airplane.Id + " was successfully deleted");
            return RedirectToAction("Airplanes");
        }

        public bool UserIsLoggedIn()
        {
            if (Session["loggedIn"] == null || Session["loggedIn"].Equals(false))
            {
                return false;
            }
            return true;
        }

        private void SetMessage(string message)
        {
            TempData["message"] = message;
        }

        private void SetErrorMessage(string message)
        {
            TempData["errorMessage"] = message;
        }

    }
}