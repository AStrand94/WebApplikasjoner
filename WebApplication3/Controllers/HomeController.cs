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

            List<Travel> allFlights = pathHelper.GetAllFlights();

            if (!allFlights.Any())
            {
                ViewBag.NoData = "No flights on this date. (could also be no flights on this distance..)";
                return View();
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

        /*[HttpPost] -> kan legge til [ActionName("RegisterCustomer")]. Da kalles /home/RegisterCustomer. Evt bytt bare navn på metoden(er nok best practice å ha metode-navn = view-navn)
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
        }*/

        [HttpPost]
        public ActionResult Registration(int flightId1,int? flightId2, int? flightId3, int? flightId4)
        {
            Console.Write(flightId1);

            /*
             foreach flight in flightIds -> hent ut faktiske flight objekter fra db -> legg inn i bestillingsobjekt -> lagre bestillingsobjekt i SESSION
             */

            return View();
        }

    }
}