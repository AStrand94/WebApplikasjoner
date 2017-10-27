using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class RouteStub : IRouteDAL
    {
        public Route AddRoute(Route route)
        {
            if (route.Id == 0)
            {
                return null;
            }
            else
            {
				var airport = new Airport
				{
					Id = 1,
					Name = "Gardermoen"
				};

				return new Route
				{
					Id = 1,
					FromAirport = airport,
					ToAirport = airport,
					Flights = new List<Flight>(),
					FlightTime = new TimeSpan(10, 0, 0)
				};
			}
        }

        public Route DeleteRoute(int id)
        {
            if (id == 0)
            {
                var r = new Route
                {
                    Id = 0
                };
                return r;
            }
            else
            {
                return GetRoute(1);
            }
        }

        public bool ExistsRouteWithId(int id)
        {
            if (id == 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Route> GetAllRoutes()
        {
            var routeList = new List<Route>();
            var route = GetRoute(1);

            routeList.Add(route);
            routeList.Add(route);
            routeList.Add(route);

            return routeList;
        }

        public IEnumerable<Route> GetAllRoutesConnections()
        {
            var routeList = new List<Route>();
            var route = GetRoute(1);

            routeList.Add(route);
            routeList.Add(route);
            routeList.Add(route);

            return routeList;
        }

        public Route GetRoute(int id)
        {
            if(id == 0)
            {
                return null;
            }
            else
            {
                var airport = new Airport
                {
                    Id = 1,
                    Name = "Gardermoen"
                };

                return new Route
                {
                    Id = 1,
                    FromAirport = airport,
                    ToAirport = airport,
                    Flights = new List<Flight>(),
                    FlightTime = new TimeSpan(10, 0, 0)
                };
            }
        }

        public bool RouteHasAirport(int id)
        {
            if (id == 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Route UpdateRoute(Route route)
        {
            if (route.Id == 0)
            {
                var r = new Route
                {
                    Id = 0
                };
                return r;
            }
            else
            {
                return GetRoute(1);
            }
        }
    }
}
