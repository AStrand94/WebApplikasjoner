using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public interface IAirportDAL
    {
        void AddAirport(Airport airport);
        Airport DeleteAirport(int id);
        Airport GetAirport(int id);
        List<Airport> GetAllAirports();
        Airport GetById(int Id);
        void UpdateAirport(Airport airport);
    }
}