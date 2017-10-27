using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class RouteBLL : IRouteBLL
    {
        private IRouteDAL _routeDAL;
        private IAirportDAL _airportDAL;

        public RouteBLL()
        {
            _routeDAL = new RouteDAL();
            _airportDAL = new AirportDAL();
        }

        public RouteBLL(IRouteDAL stub, IAirportDAL airportStub)
        {
            _routeDAL = stub;
            _airportDAL = airportStub;
        }

        public IEnumerable<Route> GetAllRoutes()
        {
            return _routeDAL.GetAllRoutes();
        }

        public IEnumerable<Route> GetAllRoutesConnections()
        {
            return _routeDAL.GetAllRoutesConnections();
        }
        

        public Route UpdateRoute(Route route)
        {
            Route dbRoute = _routeDAL.GetRoute(route.Id);

            route.FromAirport = _airportDAL.GetAirport(route.FromAirport.Id);
            route.ToAirport = _airportDAL.GetAirport(route.ToAirport.Id);
            return _routeDAL.UpdateRoute(route);
        }

        public Route DeleteRoute(int id)
        {
            return _routeDAL.DeleteRoute(id);
        }

        public string CanDeleteRoute(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();

            Route route = _routeDAL.GetRoute(id);

            if (route == null)
            {
                stringBuilder.Append("Route does not exist");
            }
            else
            {
                if (route.Flights.Count > 0)
                {
                    HashSet<string> routes = new HashSet<string>();
                    foreach (var flight in route.Flights)
                    {
                        routes.Add(flight.Time.ToShortDateString());
                    }

                    stringBuilder.Append("Cannot delete route, must delete flights from ").
                        Append(route.FromAirport.Name).Append(" to ").Append(route.ToAirport.Name).Append(" on dates: ");

                    foreach (var str in routes)
                    {
                        stringBuilder.Append(str + ',');
                    }
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }

            return stringBuilder.ToString();
        }

        public Route AddRoute(Route route)
        {
            return _routeDAL.AddRoute(route);
        }

        public string CanUpdateRoute(Route route)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            if(route.FromAirport.Id == route.ToAirport.Id)
            {
                stringBuilder.Append("From- and To- airport must be different!\n");
            }

            return stringBuilder.ToString();
        }
    }
}
