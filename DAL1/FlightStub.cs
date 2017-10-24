using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class FlightStub : IFlightDAL
    {
        public Flight DeleteFlight(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsFlightWithId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Flight> GetAllFlightConnections()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            throw new NotImplementedException();
        }

        public Flight GetFlight(int flightId)
        {
            throw new NotImplementedException();
        }

        public Flight InsertFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public void UpdateFlight(Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}
