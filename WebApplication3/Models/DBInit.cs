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

            base.Seed(context);
        }
    }
}