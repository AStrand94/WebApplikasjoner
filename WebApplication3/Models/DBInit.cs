using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class DBInit : DropCreateDatabaseAlways<DB>
    {
        protected override void Seed(DB context)
        {
            var kunde1 = new Customer
           {
                Id = 0,
                Firstname = "Andreas",
                Lastname = "Strand"
            };

            context.Customers.Add(kunde1);

            var airport1 = new Airport
            {
                Id = 0,
                Name = "Gardermoen"
            };

            var airport2 = new Airport
            {
                Id = 1,
                Name = "Flesland"
            };

            context.Airports.Add(airport1);
            context.Airports.Add(airport2);

            var route1 = new Route
            {
                Id = 0,
                ToAirport = airport1,
                FromAirport = airport2
            };

            context.Routes.Add(route1);

            base.Seed(context);
        }
    }
}