using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        private IRouteDAL _route;

        [ExcludeFromCodeCoverage]
        public AirportBLL()
        {
            _airport = new AirportDAL();
            _route = new RouteDAL();
        }

        public AirportBLL(IAirportDAL stub, IRouteDAL routeStub)
        {
            _airport = stub;
            _route = routeStub;
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
            Airport airport = _airport.GetAirport(id);
            bool routeHasAirport = _route.RouteHasAirport(id);

            return !routeHasAirport;
        }

        public Airport DeleteAirport(int id)
        {
            return _airport.DeleteAirport(id);
        }

        public Airport GetAirport(int id)
        {
            return _airport.GetAirport(id);
        }
    }
}
