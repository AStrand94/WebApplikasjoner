using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDTO
    {
        public int FlightId { get; set; }
        public int CustomerId { get; set; }
        public List<PersonDTO> Travellers { get; set; }
    }
}
