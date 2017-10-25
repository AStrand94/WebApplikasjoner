using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public interface IAirportBLL
    {
        Airport AddAirport(Airport airport);
        bool AirportIsUsedInRoutes(int id);
        Airport DeleteAirport(int id);
        List<Airport> GetAllAirports();
        Airport UpdateAirport(Airport airport);
    }
}