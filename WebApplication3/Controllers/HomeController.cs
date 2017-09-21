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

        private DB db = new DB();

        public ActionResult Index()
        {
            var airports = db.Airports.ToList();
            return View(airports);
        }

        [HttpGet]
        public ActionResult RegisterFlight(int fromAirportId, int toAirportId, DateTime date,DateTime? returnDate) 
        {
            PathHelper pathHelper = new PathHelper(fromAirportId, toAirportId,date, db);
            List<Travel> allFlights = pathHelper.GetAllFlights();

            PathHelper returnHelper;
            List<Travel> returnFlights = new List<Travel>(); //ønsker egentlig ikke å assigne denne før if'en under..

            if(returnDate != null)
            {
                returnHelper = new PathHelper(toAirportId, fromAirportId, returnDate.GetValueOrDefault(), db);
                returnFlights = returnHelper.GetAllFlights();
                returnFlights.ForEach(f => f.isReturnFlight = true);
            }

            if (returnDate == null && !allFlights.Any() || (!allFlights.Any() || returnDate != null && !returnFlights.Any()))
            {
                ViewBag.NoData = "No flights on this date. (could also be no flights on this distance..)";
                return View();
            }
            else
            {
                allFlights.AddRange(returnFlights);
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
            Order order = GetOrderObject();
            order.Customer = customer;

            return View();
        }

        [HttpPost]
        public ActionResult Registration(int flightId1, int? flightId2, int? flightId3, int? flightId4)
        {

            Order order = GetOrderObject();
            order.Flights.Clear();
            
            order.Flights.Add(db.Flights.Where(f => f.Id == flightId1).First());

            if(flightId2 != null)
            {
                order.Flights.Add(db.Flights.Where(f => f.Id == flightId2).First());
            }

            if (flightId3 != null)
            {
                order.Flights.Add(db.Flights.Where(f => f.Id == flightId3).First());
            }

            if (flightId4 != null)
            {
                order.Flights.Add(db.Flights.Where(f => f.Id == flightId4).First());
            }

            return View();
        }

        private Order GetOrderObject()
        {
            Order order;

            if (Session["Order"] != null)
            {
                order = (Order)Session["Order"];
            }
            else
            {
                order = new Order();
                Session["Order"] = order;
            }

            return order;
        }

    }
}