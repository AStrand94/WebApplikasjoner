using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class FlightDAL
    {
        private DB db;

        public FlightDAL(DB db)
        {
            this.db = db;
        }

        public Flight GetFlight(int flightId)
        {
            return db.Flights.Where(f => f.Id == flightId).First();
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return db.Flights.ToList();
        }

        public void UpdateFlight(Flight flight)
        {
            Flight dbFlight = GetFlight(flight.Id);
            dbFlight.Route = flight.Route;
            dbFlight.Price = flight.Price;
            dbFlight.Time = flight.Time;
            dbFlight.Airplane = flight.Airplane;
            //DO NOT UPDATE TICKETS.

            db.SaveChanges();
        }

        public Flight DeleteFlight(int id)
        {
            Flight flight = db.Flights.Where(f => f.Id == id).Single();

            if(flight != null)
            {
                db.Flights.Attach(flight);
                flight = db.Flights.Remove(flight);
                db.SaveChanges();
            }

            return flight;
        }

        public bool ExistsFlightWithId(int id)
        {
            return db.Flights.Any(f => f.Id == id);
        }

        public Flight InsertFlight(Flight flight)
        {
            if (flight == null) return null;

            flight = db.Flights.Add(flight);
            db.SaveChanges();

            return flight;
        }
    }
}
