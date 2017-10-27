using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public interface IFlightDAL
    {
        Flight DeleteFlight(int id);
        bool ExistsFlightWithId(int id);
        IEnumerable<Flight> GetAllFlights();
        Flight GetFlight(int flightId);
        Flight InsertFlight(Flight flight);
        Flight UpdateFlight(Flight flight);
        IEnumerable<Flight> GetAllFlightConnections();
        IEnumerable<Flight> GetAllFlightsWithFullRoute();
    }
}