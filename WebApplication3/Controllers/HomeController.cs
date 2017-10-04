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
        public ActionResult RegisterFlight(int fromAirportId, int toAirportId, DateTime date,DateTime? returnDate, int? numberOfTravellers) 
        {
            var orderObject = GetOrderObject();
            orderObject.NumberTravellers = numberOfTravellers.GetValueOrDefault();
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

            double totalPrice = 0;
            foreach(var tr in model.Travels)
            {
                totalPrice += tr.Price * (double)numberOfTravellers;
            }
            orderObject.TotalPrice = totalPrice;

            ViewBag.NumberTravellers = numberOfTravellers;
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
        public ActionResult Payment(IEnumerable<Customer> customers)
        {
            Customer mainCustomer = customers.ElementAt(0);
            try
            {
                foreach(var customer in customers)
                {
                    db.Customers.Add(customer);
                }
                
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                RedirectToAction("Index");
            }

            int numberTravellers = GetOrderObject().NumberTravellers;
            OrderSession order = GetOrderObject();
            order.Customer = mainCustomer;
            order.Travelers = new List<Customer>();
            order.Travelers.AddRange(customers);

            ViewBag.FlightList = GetFlightsFromId(order.Flights);
            ViewBag.Customers = customers;
            ViewBag.NumberTravellers = order.NumberTravellers;
            ViewBag.TotalPrice = order.TotalPrice;

            return View();
        }

        [HttpPost]
        public ActionResult Registration(int flightId1, int? flightId2, int? flightId3, int? flightId4)
        {
            OrderSession order = GetOrderObject();
            order.Flights.Clear();
            
            order.Flights.Add(flightId1);
            List<int> flightIds = new List<int>();
            flightIds.Add(flightId1);

            if(flightId2 != null)
            {
                flightIds.Add(flightId2.GetValueOrDefault());
                order.Flights.Add(flightId2.GetValueOrDefault());
            }

            if (flightId3 != null)
            {
                flightIds.Add(flightId3.GetValueOrDefault());
                order.Flights.Add(flightId3.GetValueOrDefault());
            }

            if (flightId4 != null)
            {
                flightIds.Add(flightId4.GetValueOrDefault());
                order.Flights.Add(flightId4.GetValueOrDefault());
            }

            ViewBag.NumberTravellers = order.NumberTravellers;

            ViewBag.FlightList = GetFlightsFromId(flightIds);
            return View();
        }

        [HttpPost]
        public ActionResult Overview(String cardNumber, String expDate, String cvc)
        {
            if (Session["Order"] == null) return View(Session["FinishedOrder"]); //If user tries to refresh page

            String referenceNumber = new ReferenceGenerator().getReferenceNumber(db);
            OrderSession order = GetOrderObject();
            
            Order o = new Order
            {
                Reference = referenceNumber,
                Customer = order.Customer
            };
            
            List<Ticket> tickets = new List<Ticket>();

            foreach (Customer tr in order.Travelers) {
                foreach (int fId in GetOrderObject().Flights) {
                    Flight flight = db.Flights.Where(f => f.Id == fId).First();

                    Ticket ticket = new Ticket
                    {
                        Order = o,
                        Flight = flight,
                        Traveler = tr
                    };
                    tickets.Add(ticket);
                }
            }

            o.Tickets = tickets;

            o.TotalPrice = order.TotalPrice;
            db.Orders.Add(o);
            db.Tickets.AddRange(tickets);
            db.SaveChanges();

            Session.Clear();
            Session["FinishedOrder"] = o;

            return View(o);
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

        private List<Flight> GetFlightsFromId(List<int> flightIds)
        {
            List<Flight> flights = new List<Flight>();

            foreach(int id in flightIds)
            {
                flights.Add(
                    db.Flights.Where(f => f.Id == id).First()
                    );
            }
            return flights;
        }
    }
}