using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.BLL;

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

        public bool UserIsLoggedIn()
        {
            if (Session["loggedIn"] == null || Session["loggedIn"].Equals(false))
            {
                return false;
            }
            return true;
        }

    }
}