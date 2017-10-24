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
        public void AddRoute(Route route)
        {
            throw new NotImplementedException();
        }

        public Route DeleteRoute(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsRouteWithId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Route> GetAllRoutes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Route> GetAllRoutesConnections()
        {
            throw new NotImplementedException();
        }

        public Route GetRoute(int id)
        {
            throw new NotImplementedException();
        }

        public bool RouteHasAirport(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoute(Route route)
        {
            throw new NotImplementedException();
        }
    }
}
