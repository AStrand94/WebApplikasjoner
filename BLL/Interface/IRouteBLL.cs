﻿using System.Collections.Generic;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public interface IRouteBLL
    {
        Route AddRoute(Route route);
        string CanDeleteRoute(int id);
        Route DeleteRoute(int id);
        IEnumerable<Route> GetAllRoutes();
        bool RouteHasAirport(int id);
        Route UpdateRoute(Route route);
        IEnumerable<Route> GetAllRoutesConnections();
    }
}