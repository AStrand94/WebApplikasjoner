using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public interface IAirplaneDAL
    {
        bool Contains(int id);
        Airplane DeleteAirplane(int id);
        bool ExistsAirplaneWithId(int id);
        Airplane GetAirplane(int id);
        IEnumerable<Airplane> GetAllAirplanes();
        Airplane InsertAirplane(Airplane airplane);
        Airplane UpdateAirplane(Airplane airplane);
        bool HasFlights(int id);
    }
}