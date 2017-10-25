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
        private IAirplaneDAL _airplane;

        public AirplaneBLL()
        {
            _airplane = new AirplaneDAL();
        }

        public AirplaneBLL(IAirplaneDAL stub)
        {
            _airplane = stub;
        }

        public IEnumerable<Airplane> GetAllAirplanes() {
            return _airplane.GetAllAirplanes();
        }

        public Airplane InsertAirplane(Airplane airplane)
        {
            return _airplane.InsertAirplane(airplane);
        }

        public string CanDeleteAirplane(int id)
        {
            if (!_airplane.Contains(id))
            {
                return "No flight with id " + id;
            }

            Airplane airplane = _airplane.GetAirplane(id);

            if(_airplane.HasFlights(id))
            {
                return "Cannot delete. There are flights connected to airplane " + airplane.Model + ", id " + airplane.Id;
            }

            return "";
        }

        public Airplane DeleteAirplane(int id)
        {
            return _airplane.DeleteAirplane(id);
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
            return _airplane.UpdateAirplane(airplane);
        }
    }
}