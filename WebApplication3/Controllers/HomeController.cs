using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication3.DAL;
using WebApplication3.BLL;
using WebApplication3.Model;
using WebApplication3.Models;
using DTO.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {

        private DB db = new DB();

        public ActionResult Index()
        {
            return View(new AirportBLL().getAllAirports());
        }

        [HttpGet]
        public ActionResult RegisterFlight(int fromAirportId, int toAirportId, DateTime date,DateTime? returnDate, int numberOfTravellers) 
        {
            FlightBLL bll = new FlightBLL(fromAirportId, toAirportId, date, returnDate, numberOfTravellers);
            GetOrderObject().NumberTravellers = numberOfTravellers;
            TravelModel model = bll.GetTravelModel();
            
            if (!model.IsValidRoute())
            {
                ViewBag.NoData = "No flights on this date.";
                return PartialView();
            }

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

        public ActionResult Payment()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Overview()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Payment(IEnumerable<Customer> customers)
        {
            if (Session["Order"] == null) return RedirectToAction("Index");

            Customer mainCustomer = customers.ElementAt(0);
            new CustomerBLL().AddCustomers(customers);

            int numberTravellers = GetOrderObject().NumberTravellers;
            OrderSession order = GetOrderObject();
            order.Customer = mainCustomer;
            order.Travelers = new List<Customer>();
            order.Travelers.AddRange(customers);
            order.TotalPrice = getTotalPrice(numberTravellers, GetFlightsFromId(order.Flights));

            ViewBag.FlightList = GetFlightsFromId(order.Flights);
            ViewBag.Customers = customers;
            ViewBag.NumberTravellers = order.NumberTravellers;
            ViewBag.TotalPrice = order.TotalPrice;

            return View();
        }

        private double getTotalPrice(int numberTravellers, IEnumerable<Flight> flights)
        {
            double totalPrice = 0;
            foreach (var tr in flights)
            {
                totalPrice += tr.Price * (double)numberTravellers;
            }
            return totalPrice;
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

            Order o = new OrderBLL().CreateOrder(GetOrderObject());

            Session.Clear();
            Session["FinishedOrder"] = o;

            return View(o);
        }

        [HttpPost]
        public ActionResult _OrderView(string Reference)
        {
            var order = new OrderBLL().GetOrder(Reference);

            if (!order.Any()) return PartialView();
            else return PartialView(order.First());
            
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
            return new FlightBLL().GetFlights(flightIds);
        }
    }
}