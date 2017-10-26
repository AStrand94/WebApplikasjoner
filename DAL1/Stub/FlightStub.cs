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
            if (id == 0)
            {
                var f = new Flight
                {
                    Id = 0
                };
                return f;
            }
            else
            {
                return GetFlight(1);
            }
        }

        public bool ExistsFlightWithId(int id)
        {
            if(id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<Flight> GetAllFlightConnections()
        {
            var flightList = new List<Flight>();
            var flight = GetFlight(1);

            flightList.Add(flight);
            flightList.Add(flight);
            flightList.Add(flight);

            return flightList;
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            var flightList = new List<Flight>();
            var flight = GetFlight(1);

            flightList.Add(flight);
            flightList.Add(flight);
            flightList.Add(flight);

            return flightList;
        }

        public Flight GetFlight(int flightId)
        {
            if (flightId == 0)
            {
                var flight = new Flight
                {
                    Id = 0
                };
                return flight;
            }
            else
            {
                var flight = new Flight()
                {
                    Id = 1,
                    Price = 100
                };
                return flight;
            }
        }

        public Flight InsertFlight(Flight flight)
        {
            if (flight.Id == 0)
            {
                var f = new Flight
                {
                    Id = 0
                };
                return f;
            }
            else
            {
                return GetFlight(1);
            }
        }

        public Flight UpdateFlight(Flight flight)
        {
            if (flight.Id == 0)
            {
                var f = new Flight
                {
                    Id = 0
                };
                return f;
            }
            else
            {
                return GetFlight(1);
            }
        }
    }
}
