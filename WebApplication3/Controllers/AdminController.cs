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
        // GET: Admin
        public ActionResult Login(string username, string password)
        {
            LoginBLL loginBLL = new LoginBLL();

            if(loginBLL.checkLogin(username, password))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home", new { area = "" });

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Airplanes()
        {
            return View(new AirplaneBLL().GetAllAirplanes());
        }

        public ActionResult Airports()
        {
            return View(new AirportBLL().getAllAirports());
        }

        public ActionResult Customers()
        {
            return View(new CustomerBLL().GetAllCustomers());
        }

        public ActionResult Flights()
        {
            return View(new FlightBLL().GetAllFlights());
        }

        public ActionResult Routes()
        {
            return View(new RouteBLL().GetAllRoutes());
        }

    }
}