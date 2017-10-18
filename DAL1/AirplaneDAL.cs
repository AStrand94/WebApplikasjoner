using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class AirplaneDAL
    {
        private DB db;

        public AirplaneDAL(DB db)
        {
            this.db = db;
        }

        public IEnumerable<Airplane> GetAllAirplanes()
        {
            return db.Airplanes.ToList();
        }
    }
}
