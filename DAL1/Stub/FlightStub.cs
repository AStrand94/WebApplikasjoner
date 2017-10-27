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
            if (id == 500)
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
            if(id == 100)
            {
                return true;
            }
            else
            {
                return false;
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
                return null;
            }
            else if(flightId == 100)
            {
                var order = new Order()
                {
                    Id = 1,
                    Reference = "DAKLF"
                };

                var tickets = new List<Ticket>();

                var flight = new Flight()
                {
                    Id = 1,
                    Price = 100,
                    Tickets = tickets
                };
                return flight;
            }
            else if (flightId == 500)
            {
                var order = new Order()
                {
                    Id = 1,
                    Reference = "DAKLF"
                };

                var tickets = new List<Ticket>();

                var flight = new Flight()
                {
                    Id = 1,
                    Price = 100,
                    Tickets = tickets
                };
                return flight;
            }
            else
            {
                var order = new Order()
                {
                    Id = 1,
                    Reference = "DAKLF"
                };

                var ticket = new Ticket()
                {
                    Id = 1,
                    FirstName = "Ola",
                    LastName = "Normann",
                    Order = order
                };

                var tickets = new List<Ticket>();
                tickets.Add(ticket);
                tickets.Add(ticket);

                var flight = new Flight()
                {
                    Id = 1,
                    Price = 100,
                    Tickets = tickets
                };
                return flight;
            }
        }

        public Flight InsertFlight(Flight flight)
        {
            if (flight.Price == 1000)
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
            if (flight.Price == 1000)
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

        public IEnumerable<Flight> GetAllFlightsWithFullRoute()
        {
            return GetAllFlights();
        }
    }
}
