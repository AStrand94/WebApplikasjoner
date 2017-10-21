using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class AirplaneBLL
    {
        private DB db = new DB();

        public IEnumerable<Airplane> GetAllAirplanes()
        {
            return new AirplaneDAL(db).GetAllAirplanes();
        }

        public Airplane InsertAirplane(Airplane airplane)
        {
            return new AirplaneDAL(db).InsertAirplane(airplane);
        }

        public string CanDeleteAirplane(int id)
        {
            AirplaneDAL dal = new AirplaneDAL(db);

            if (!dal.Contains(id))
            {
                return "No flight with id " + id;
            }

            Airplane airplane = dal.GetAirplane(id);

            if (airplane.Flights.Any())
            {
                return "Cannot delete. There are flights connected to airplane " + airplane.Model + ", id " + airplane.Id;
            }

            return "";
        }

        public Airplane DeleteAirplane(int id)
        {
            return new AirplaneDAL(db).DeleteAirplane(id);
        }

        public string CanUpdateAirplane(Airplane airplane)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if(airplane.Model == null || airplane.Model.Length < 1)
            {
                stringBuilder.Append("Model must be filled!");
            }

            if(airplane.Seats == 0)
            {
                stringBuilder.Append("Number of seats cannot be 0!");
            }

            return stringBuilder.ToString();
        }

        public Airplane UpdateAirplane(Airplane airplane)
        {
            return new AirplaneDAL(db).UpdateAirplane(airplane);
        }
    }
}