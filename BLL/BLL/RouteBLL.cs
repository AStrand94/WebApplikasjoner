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
        private IRouteDAL dal;

        public RouteBLL()
        {
            dal = new RouteDAL();
        }

        public RouteBLL(IRouteDAL stub)
        {
            dal = stub;
        }

        public IEnumerable<Route> GetAllRoutes()
        {
            return dal.GetAllRoutes();
        }

        public IEnumerable<Route> GetAllRoutesConnections()
        {
            return dal.GetAllRoutesConnections();
        }

        public bool RouteHasAirport(int id)
        {
            return dal.RouteHasAirport(id);
        }

        public void UpdateRoute(Route route)
        {
            AirportDAL airportDAL = new AirportDAL();

            Route dbRoute = dal.GetRoute(route.Id);

            if (dbRoute == null)
                throw new NullReferenceException("Route with id " + route.Id + " does not exist in current context");

            route.FromAirport = airportDAL.GetAirport(route.FromAirport.Id);
            route.ToAirport = airportDAL.GetAirport(route.ToAirport.Id);
            dal.UpdateRoute(route);
        }

        public Route DeleteRoute(int id)
        {
            return dal.DeleteRoute(id);
        }

        public string CanDeleteRoute(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();

            Route route = dal.GetRoute(id);

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
            return dal.AddRoute(route);
        }
    }
}
