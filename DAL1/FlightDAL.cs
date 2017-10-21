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

    }
}
