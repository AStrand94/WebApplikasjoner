using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public interface IAirplaneBLL
    {
        string CanDeleteAirplane(int id);
        string CanUpdateAirplane(Airplane airplane);
        Airplane DeleteAirplane(int id);
        IEnumerable<Airplane> AllAirplanes { get; }

        Airplane InsertAirplane(Airplane airplane);
        Airplane UpdateAirplane(Airplane airplane);
    }
}