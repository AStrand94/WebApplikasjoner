using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;
using WebApplication3.Model;

namespace WebApplication3.BLL
{
    public class FlightBLL : IFlightBLL
    {
        private int fromAirportId;
        private int toAirportId;
        private DateTime date;
        private DateTime? returnDate;
        private int numberOfTravellers;

        private IFlightDAL _flightDAL;
        private IRouteDAL _routeDAL;
        private IAirplaneDAL _airplaneDAL;
        private IAirportDAL _airportDAL;

        public FlightBLL(int fromAirportId, int toAirportId, DateTime date, DateTime? returnDate, int numberOfTravellers)
        {
            this.fromAirportId = fromAirportId;
            this.toAirportId = toAirportId;
            this.date = date;
            this.returnDate = returnDate;
            this.numberOfTravellers = numberOfTravellers;
            _flightDAL = new FlightDAL();
            _routeDAL = new RouteDAL();
            _airplaneDAL = new AirplaneDAL();
            _airportDAL = new AirportDAL();
        }

        public FlightBLL()
        {
            _flightDAL = new FlightDAL();
            _routeDAL = new RouteDAL();
            _airplaneDAL = new AirplaneDAL();
            _airportDAL = new AirportDAL();
        }
        
        public FlightBLL(IFlightDAL stub, IRouteDAL routeStub, IAirplaneDAL airplaneStub, IAirportDAL airportStub)
        {
            _flightDAL = stub;
            _routeDAL = routeStub;
            _airplaneDAL = airplaneStub;
            _airportDAL = airportStub;
        }

        private List<Travel> GetFlightsTo()
        {
            PathHelper pathHelper = new PathHelper(fromAirportId, toAirportId, date, new DB());
            return pathHelper.GetAllFlights();
        }

        private List<Travel> GetFlightsFrom()
        {
            PathHelper pathHelper = new PathHelper(toAirportId, fromAirportId, returnDate.GetValueOrDefault(), new DB());
            List<Travel> ReturnFlights = pathHelper.GetAllFlights();
            ReturnFlights.ForEach(f => f.isReturnFlight = true);
            return ReturnFlights;
        }

        private List<Travel> GetFligthts()
        {
            List<Travel> Travels = GetFlightsTo();
            if (returnDate != null)
            {
                List<Travel> RetTravels = GetFlightsFrom();
                RetTravels.ForEach(t => t.isReturnFlight = true);
                Travels.AddRange(RetTravels);
            }

            return Travels;
        }

        public TravelModel GetTravelModel()
        {
            if(returnDate == null)
            {
                return new TravelModel(GetFlightsTo(), _airportDAL.GetAirport(fromAirportId), _airportDAL.GetAirport(toAirportId));
            }
            else
            {
                return new TravelModel(GetFlightsTo(), GetFlightsFrom(), _airportDAL.GetAirport(fromAirportId), _airportDAL.GetAirport(toAirportId));
            }
        }

        public List<Flight> GetFlights(List<int> flightIds)
        {
            List<Flight> flights = new List<Flight>();

            foreach (int id in flightIds)
            {
                flights.Add(new FlightDAL().GetFlightWithInclude(id));
            }

            return flights;
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return _flightDAL.GetAllFlights();
        }

        public IEnumerable<Flight> GetAllFlightsWithFullRoute()
        {
            return _flightDAL.GetAllFlightsWithFullRoute();
        }

        public IEnumerable<Flight> GetAllFlightConnections()
        {
            return _flightDAL.GetAllFlightConnections();
        }

        public String CanUpdateFlight(Flight flight)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (flight.Price == 0 || flight.Route == null || flight.Time == null || flight.Id < 0)
            {
                stringBuilder.Append("All field must be filled!\n\r");
            }

            if (flight.Time != null && flight.Time < DateTime.Now)
            {
                stringBuilder.Append("Time cannot be before now!\n\r");
            }

            return stringBuilder.ToString();
        }

        public void UpdateFlight(Flight flight)
        {
            Flight dbFlight = _flightDAL.GetFlight(flight.Id);

            if (dbFlight == null)
                throw new NullReferenceException("flight with id " + flight.Id + " does not exist in current context");
            
            flight.Route = _routeDAL.GetRoute(flight.Route.Id);
            flight.Airplane = _airplaneDAL.GetAirplane(flight.Airplane.Id);
            _flightDAL.UpdateFlight(flight);
        }

        public string CanDeleteFlight(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();

            Flight flight = _flightDAL.GetFlight(id);

            if(flight == null)
            {
                stringBuilder.Append("Flight does not exist");
            }
            else
            {
                if(flight.Tickets.Count > 0)
                {
                    HashSet<string> orders = new HashSet<string>();
                    foreach(var ticket in flight.Tickets)
                    {
                        orders.Add(ticket.Order.Reference);
                    }

                    stringBuilder.Append("Cannot delete flight, must delete orders: ");

                    foreach(var str in orders)
                    {
                        stringBuilder.Append(str + ',');
                    }
                    stringBuilder.Remove(stringBuilder.Length - 1,1);
                }
            }

            return stringBuilder.ToString();
        }

       
        public Flight DeleteFlight(int id)
        {
            return _flightDAL.DeleteFlight(id);
        }

        public string CanInsertFlight(Flight flight)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if(flight.Price == 0)
            {
                stringBuilder.Append("Price cannot be 0!\n\r");
            }

            if (!_routeDAL.ExistsRouteWithId(flight.Route.Id))
            {
                stringBuilder.Append("Route does not exist");
            }

            if (!_airplaneDAL.ExistsAirplaneWithId(flight.Airplane.Id))
            {
                stringBuilder.Append("Airplane does not exist");
            }

            if(flight.Time == null || flight.Time < DateTime.Now)
            {
                stringBuilder.Append("Flight cannot be before now!");
            }

            return stringBuilder.ToString();
        }

        public Flight InsertFlight(Flight flight)
        {
            flight.Route = _routeDAL.GetRoute(flight.Route.Id);
            flight.Airplane = _airplaneDAL.GetAirplane(flight.Airplane.Id);
            return _flightDAL.InsertFlight(flight);
        }
    }
}
