using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class RouteDAL
    {
        private DB db;

        public RouteDAL(DB db)
        {
            this.db = db;
        }

        public IEnumerable<Route> GetAllRoutes()
        {
            return db.Routes.ToList();
        }

        public Route GetRoute(int id)
        {
            return db.Routes.Where(r => r.Id == id).Single();
        }
    }
}
