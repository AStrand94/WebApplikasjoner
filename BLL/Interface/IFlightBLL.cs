using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public interface IFlightBLL
    {
        string CanDeleteFlight(int id);
        string CanInsertFlight(Flight flight);
        string CanUpdateFlight(Flight flight);
        Flight DeleteFlight(int id);
        IEnumerable<Flight> GetAllFlights();
        IEnumerable<Flight> GetAllFlightsWithFullRoute();
        List<Flight> GetFlights(List<int> flightIds);
        TravelModel GetTravelModel();
        Flight InsertFlight(Flight flight);
        void UpdateFlight(Flight flight);
        IEnumerable<Flight> GetAllFlightConnections();
    }
}