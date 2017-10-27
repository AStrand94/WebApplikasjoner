using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.Model
{
    [ExcludeFromCodeCoverage]
    public class PathHelper
    {
        private static readonly int HOUR_IN_MILLIS = 3_600_000;

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
        public Airport FromAirport { get; set; }
        public Airport ToAirport { get; set; }

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

            IEnumerable<Flight> flights = db.Flights.Where(f => f.Route.FromAirport.Id == fromAirportId
                                                            && f.Route.ToAirport.Id == toAirportId
                                                            && DbFunctions.TruncateTime(f.Time) == DbFunctions.TruncateTime(date));

            foreach (var f in flights)
            {
                DirectTravels.Add(new Travel(f));
            }
            return DirectTravels;
        }

        private List<Travel> getStopovers()
        {
          
            List<Flight> FromFlights = db.Flights.Where(f => f.Route.FromAirport.Id == FromAirport.Id
                                                            && f.Route.ToAirport.Id != toAirportId
                                                            && DbFunctions.TruncateTime(f.Time) == DbFunctions.TruncateTime(date)).ToList();
            List<Flight> ToFlights = db.Flights.Where(f => f.Route.ToAirport.Id == ToAirport.Id
                                                            && f.Route.FromAirport.Id != fromAirportId
                                                            && DbFunctions.TruncateTime(f.Time) == DbFunctions.TruncateTime(date)).ToList();

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
                if(f.Route.ToAirport.Id == ToFlight.Route.FromAirport.Id && f.Time.AddMilliseconds(f.Route.FlightTime.Milliseconds + HOUR_IN_MILLIS).CompareTo(ToFlight.Time) < 0)
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