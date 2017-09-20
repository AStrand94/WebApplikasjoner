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
        Order order = new Order();

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
                ViewBag.NoData = "No flights on this date. (could also be no flights on this distance..)";
                return View();
            } 

            return View(allFlights);
        }

        public ActionResult RegisterFlight()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Registration()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Registration(int flightId)
        {
            order.Flight = db.Flights.Where(f => f.Id == flightId).First();
            Session.Add("Flight", order.Flight);
            return View();
        }

        [HttpPost]
        public ActionResult Payment(Models.Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                
            }

            order.Customer = customer;
            Session.Add("Customer", order.Customer);
            return View();
        }

    }
}