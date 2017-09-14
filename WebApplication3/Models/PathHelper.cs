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
            this.toAirport = toAirport;
            this.fromAirport = fromAirport;
            FlightList = new List<List<Flight>>();
            this.db = db;
            AllFlights = db.Flights.ToList();
        }
        private DateTime date;
        private int fromAirport;
        private int toAirport;
        private DB db;
        private List<Flight> AllFlights;
        private List<List<Flight>> FlightList { get; }
        private static readonly int MAX_STOPOVERS = 3;

        public List<List<Flight>> GetAllFlights()
        {
            foreach(Flight f in GetDirectFlights())
            {
                List<Flight> tempList = new List<Flight>();
                tempList.Add(f);
                FlightList.Add(tempList);
            }

            /*foreach(List<Flight> fList in GetStopovers())
            {
                FlightList.Add(fList);
            }*/
            
            return FlightList;
        }

        public IEnumerable<Flight> GetDirectFlights()
        {
            List<Flight> routeList = db.Flights.ToList();

            return routeList.Where(r => r.Route.FromAirport.Id == fromAirport && r.Route.ToAirport.Id == toAirport);
        }

        /*private List<List<Flight>> GetStopovers()
        {
            List<List<Flight>> Stopovers = new List<List<Flight>>();

            List<Flight> allFlightsFromA = db.Flights.Where(f => f.Route.FromAirport.Id == fromAirport).ToList();

            foreach(Flight f in allFlightsFromA)
            {
                Stopovers.Add(GetStopovers(f));
            }

            return Stopovers;
        }

        private List<Flight> GetStopovers(Flight f)
        {
            int nStopovers = 1;
            List<Flight> Stopovers = new List<Flight>();
            List<Airport> UsedAirports = new List<Airport>();
            UsedAirports.Add(f.Route.FromAirport);
            Stopovers.Add(f);

            return NextStopOver(Stopovers);
        }

        private List<Flight> NextStopOver(List<Flight> Stopovers)
        {
            Flight PrevFlight = Stopovers.Last();

            if(PrevFlight.Route.ToAirport.Id == toAirport)
            {
                return Stopovers;
            }
            else
            {
                foreach ()
                {

                }

                return NextStopOver(Stopovers);
            }
        }*/
        
    }
}