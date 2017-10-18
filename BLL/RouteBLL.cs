using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class RouteBLL
    {
        private DB db = new DB();

        public IEnumerable<Route> GetAllRoutes()
        {
            return new RouteDAL(db).GetAllRoutes();
        }
    }
}
