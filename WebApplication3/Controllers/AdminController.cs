using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.BLL;
using WebApplication3.Model;

namespace WebApplication3.Controllers
{
    public class AdminController : Controller
    {
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
            ViewBag.Airplanes = new AirplaneBLL().GetAllAirplanes().Count();
            ViewBag.Airports = new AirportBLL().GetAllAirports().Count();

            return View();
        }

        public ActionResult Airplanes()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(new AirplaneBLL().GetAllAirplanes().OrderBy(a => a.Model));
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

            return View(new CustomerBLL().GetAllCustomers().OrderBy(c => c.Firstname));
        }

        public ActionResult Flights()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData["allAirplanes"] = new AirplaneBLL().GetAllAirplanes();
            TempData["allRoutes"] = new RouteBLL().GetAllRoutes();
            return View(new FlightBLL().GetAllFlights());
        }

        public ActionResult Routes()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(new RouteBLL().GetAllRoutes().OrderBy(r => r.FromAirport.Name));
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

            if (!airportBLL.CanDelete(id))
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

        public ActionResult Orders()
        {
            IEnumerable<Order> orders = new OrderBLL().GetAllOrders();
            return View(orders.OrderBy(o => o.Reference));
        }

        [HttpPost]
        public ActionResult UpdateCustomer(Customer customer)
        {
            new CustomerBLL().UpdateCustomer(customer);
            SetMessage("Customer with id " + customer.Id + " was successfully updated");
            return RedirectToAction("Customers");
        }

        [HttpPost]
        public ActionResult UpdateAirport(Airport airport)
        {
            new AirportBLL().UpdateAirport(airport);
            SetMessage("Airport " + airport.Name + " was successfully updated");
            return RedirectToAction("Airports");
        }

        [HttpPost]
        public ActionResult UpdateFlight(Flight flight)
        {
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