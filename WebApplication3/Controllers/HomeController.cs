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
            TravelModel model;

            if(returnDate != null)
            {
                returnHelper = new PathHelper(toAirportId, fromAirportId, returnDate.GetValueOrDefault(), db);
                returnFlights = returnHelper.GetAllFlights();
                returnFlights.ForEach(f => f.isReturnFlight = true);
                model = new TravelModel(allFlights, returnFlights, pathHelper.FromAirport, pathHelper.ToAirport);
            }
            else
            {
                model = new TravelModel(allFlights, pathHelper.FromAirport, pathHelper.ToAirport);
            }

            if (returnDate == null && !allFlights.Any() || (!allFlights.Any() || returnDate != null && !returnFlights.Any()))
            {
                ViewBag.NoData = "No flights on this date. (could also be no flights on this distance..)";
                return PartialView();
            }
            
            return PartialView(model);
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
            OrderSession order = GetOrderObject();
            order.Customer = customer;

            return View();
        }

        [HttpPost]
        public ActionResult Registration(int flightId1, int? flightId2, int? flightId3, int? flightId4)
        {

            OrderSession order = GetOrderObject();
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

        [HttpPost]
        public ActionResult _OrderView(string Reference)
        {
            var model = db.Orders.Where(o => o.Reference.Equals(Reference));

            if (!model.Any()) return PartialView();
            else return PartialView(model.First());
            
        }

        private OrderSession GetOrderObject()
        {
            OrderSession order;

            if (Session["Order"] != null)
            {
                order = (OrderSession)Session["Order"];
            }
            else
            {
                order = new OrderSession();
                Session["Order"] = order;
            }

            return order;
        }



    }
}