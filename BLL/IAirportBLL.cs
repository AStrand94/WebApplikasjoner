using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public interface IAirportBLL
    {
        void AddAirport(Airport airport);
        bool AirportIsUsedInRoutes(int id);
        Airport DeleteAirport(int id);
        List<Airport> GetAllAirports();
        Airport GetById(int Id);
        void UpdateAirport(Airport airport);
    }
}