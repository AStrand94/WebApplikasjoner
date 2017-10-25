using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class AirportBLL : IAirportBLL
    {
        private IAirportDAL _airport;

        public AirportBLL()
        {
            _airport = new AirportDAL();
        }

        public AirportBLL(IAirportDAL stub)
        {
            _airport = stub;
        }

        public List<Airport> GetAllAirports()
        {
            return _airport.GetAllAirports();
        }

        public Airport AddAirport(Airport airport)
        {
            return _airport.AddAirport(airport);
        }

        public Airport UpdateAirport(Airport airport)
        {
            return _airport.UpdateAirport(airport);
        }

        public bool AirportIsUsedInRoutes(int id)
        {
            Airport airport = new AirportDAL().GetAirport(id);
            bool routeHasAirport = new RouteBLL().RouteHasAirport(id);

            return airport != null && !routeHasAirport;
        }

        public Airport DeleteAirport(int id)
        {
            return _airport.DeleteAirport(id);
        }
    }
}
