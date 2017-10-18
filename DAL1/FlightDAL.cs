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

    }
}
