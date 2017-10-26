using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public interface IAirportDAL
    {
        Airport AddAirport(Airport airport);
        Airport DeleteAirport(int id);
        Airport GetAirport(int id);
        List<Airport> GetAllAirports();
        Airport UpdateAirport(Airport airport);
        Airport GetById(int fromAirportId);
    }
}