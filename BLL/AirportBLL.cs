using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class AirportBLL
    {
        private DB db = new DB();
        
        public List<Airport> getAllAirports()
        {
            return new AirportDAL(db).getAllAirports();
        }

        public Airport GetById(int Id)
        {
            return new AirportDAL(db).GetById(Id);
        }
    }
}
