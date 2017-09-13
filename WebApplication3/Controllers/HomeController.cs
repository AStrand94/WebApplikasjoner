using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        DB db = new DB();
        public ActionResult Index()
        {
            var airports = db.Airports.ToList();
            return View(airports);
        }

        public ActionResult RegisterFlight()
        {
            return View();
        }
    }
}