using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class AirplaneBLL : IAirplaneBLL
    {
        private IAirplaneDAL airplane;

        public AirplaneBLL()
        {
            airplane = new AirplaneDAL();
        }

        public AirplaneBLL(IAirplaneDAL stub)
        {
            airplane = stub;
        }

        public IEnumerable<Airplane> AllAirplanes => airplane.GetAllAirplanes();

        public Airplane InsertAirplane(Airplane airplane)
        {
            return new AirplaneDAL().InsertAirplane(airplane);
        }

        public string CanDeleteAirplane(int id)
        {
            AirplaneDAL dal = new AirplaneDAL();

            if (!dal.Contains(id))
            {
                return "No flight with id " + id;
            }

            Airplane airplane = dal.GetAirplane(id);

            if(dal.HasFlights(id))
            {
                return "Cannot delete. There are flights connected to airplane " + airplane.Model + ", id " + airplane.Id;
            }

            return "";
        }

        public Airplane DeleteAirplane(int id)
        {
            return new AirplaneDAL().DeleteAirplane(id);
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
            return new AirplaneDAL().UpdateAirplane(airplane);
        }
    }
}