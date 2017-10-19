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

            return View();
        }

        public ActionResult Airplanes()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(new AirplaneBLL().GetAllAirplanes());
        }

        public ActionResult Airports()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(new AirportBLL().getAllAirports());
        }

        public ActionResult Customers()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(new CustomerBLL().GetAllCustomers());
        }

        public ActionResult Flights()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(new FlightBLL().GetAllFlights());
        }

        public ActionResult Routes()
        {
            if (!UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(new RouteBLL().GetAllRoutes());
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

        public ActionResult Orders()
        {
            IEnumerable<Order> orders = new OrderBLL().GetAllOrders();
            return View(orders);
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