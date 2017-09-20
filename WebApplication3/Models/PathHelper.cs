using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class PathHelper
    {
        public PathHelper(int fromAirport, int toAirport, DateTime date, DB db)
        {
            this.date = date;
            this.toAirportId = toAirport;
            this.fromAirportId = fromAirport;
            FlightList = new List<List<Flight>>();
            this.db = db;
            AllFlights = db.Flights.ToList();
            FromAirport = db.Airports.Where(air => air.Id == fromAirport).First();
            ToAirport = db.Airports.Where(air => air.Id == toAirport).First();
        }
        private DateTime date;
        private int fromAirportId;
        private int toAirportId;
        private DB db;
        private List<Flight> AllFlights;
        private List<List<Flight>> FlightList { get; }
        private Airport FromAirport;
        private Airport ToAirport;

        public List<Travel> GetAllFlights()
        {
            List<Travel> FlightList = GetDirectFlights();

            foreach(var t in getStopovers())
            {
                FlightList.Add(t);
            }

            return FlightList;
        }



        public List<Travel> GetDirectFlights()
        {
            List<Flight> routeList = db.Flights.ToList();
            List<Travel> DirectTravels = new List<Travel>();

            foreach (var f in db.Flights.Where(r => r.Route.FromAirport.Id == fromAirportId && r.Route.ToAirport.Id == toAirportId))
            {
                DirectTravels.Add(new Travel(f));
            }
            return DirectTravels;
        }

        private List<Travel> getStopovers()
        {
          
            Travel WholeDistance = new Travel(FromAirport,ToAirport);

            List<Flight> FromFlights = db.Flights.Where(f => f.Route.FromAirport.Id == FromAirport.Id).ToList();
            List<Flight> ToFlights = db.Flights.Where(f => f.Route.ToAirport.Id == ToAirport.Id).ToList();

            List<Travel> Stopovers = new List<Travel>();

            foreach (var f in FromFlights)
            {
                if (toAirportIn(f,ToFlights))
                {
                    foreach (var Travel in GetTravel(f,ToFlights)){
                        Stopovers.Add(Travel);
                    }
                }
            }
            return Stopovers;
        }

        private List<Travel> GetTravel(Flight f, List<Flight> ToFlights)
        {
            List<Travel> allTravels = new List<Travel>();

            foreach(var ToFlight in ToFlights)
            {
                if(f.Route.ToAirport.Id == ToFlight.Route.FromAirport.Id)
                {
                    Travel t = new Travel(f,ToFlight);
                    allTravels.Add(t);
                }
            }
            return allTravels;
        }

        private bool toAirportIn(Flight f, List<Flight> ToFlightList)
        {
            foreach (var ToFlight in ToFlightList)
            {
                if (f.Route.ToAirport.Id == ToFlight.Route.FromAirport.Id)
                {
                    return true;
                }
            }
            return false;

        }
    }
}