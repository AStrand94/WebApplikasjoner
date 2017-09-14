using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [HttpGet]
        public ActionResult RegisterFlight(int fromAirportId, int toAirportId, DateTime date) //Correct implementation
        {
            PathHelper pathHelper = new PathHelper(fromAirportId, toAirportId,date, db);

            var allFlights = pathHelper.GetDirectFlights().ToList();

            if (!allFlights.Any())
            {
                return RedirectToAction("Index");
            }

            return View(allFlights);
        }

        public ActionResult RegisterFlight()
        {
            //return View();
            return RedirectToAction("Index");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Models.Customer customerIn)
        {

            try
            {
                db.Customers.Add(customerIn);
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Index");
        } 
     
    }
}