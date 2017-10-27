using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using WebApplication3.Logging;
using WebApplication3.Model;

namespace WebApplication3.DAL
{
    public class DB : DbContext
    {

        public virtual DbSet<Airplane> Airplanes { get; set; }
        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

        public DB() : base("name=DB")
        {
            Database.CreateIfNotExists();
            Database.SetInitializer(new DBInit());
        }

        public override int SaveChanges()
        {
            CheckForChanges();
            return base.SaveChanges();
        }

        public void CheckForChanges()
        {
            var modifiedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
            var addedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();
            var deletedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted).ToList();

            if (modifiedEntities.Any()) LogChangesToEntity(modifiedEntities);
            if (addedEntities.Any()) LogNewEntities(addedEntities);
            if (deletedEntities.Any()) LogDeletedEntities(deletedEntities);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void LogChangesToEntity(List<DbEntityEntry> modifiedEntities)
        {
            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(change);
                //stringBuilder.Append("Entity ").Append(entityName).Append(" has changed:\n");
                Log log = new Log();
                log.Entity = entityName;
                log.Type = "Change";
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.OriginalValues[prop].ToString();
                    var currentValue = change.CurrentValues[prop].ToString();
                    if (originalValue != currentValue)
                    {
                        stringBuilder.Append(prop).Append(", old val: ").Append(originalValue).Append(", new val: ").Append(currentValue).Append("   ");
                    }
                }
                log.Description = stringBuilder.ToString();
                Logs.Add(log);
            }
        }

        public void LogNewEntities(List<DbEntityEntry> addedEntities)
        {
            foreach (var added in addedEntities)
            {
                Log log = new Log();
                var entityName = added.Entity.GetType().Name;
                
                log.Entity = entityName;
                log.Type = "Add";

                StringBuilder stringBuilder = new StringBuilder();
                
                foreach (var prop in added.CurrentValues.PropertyNames)
                {
                    var currentValue = added.CurrentValues[prop].ToString();
                    stringBuilder.Append(prop).Append(" val: ").Append(currentValue).Append("   ");
                }
                log.Description = stringBuilder.ToString();
                Logs.Add(log);
            }
        }

        public void LogDeletedEntities(List<DbEntityEntry> deletedEntities)
        {
            foreach (var added in deletedEntities)
            {
                Log log = new Log();
                var entityName = added.Entity.GetType().Name;
                log.Entity = entityName;
                var primaryKey = GetPrimaryKeyValue(added);

                log.Type = "Delete";
                log.Description = "Id: " + primaryKey;

                Logs.Add(log);
            }
        }

        private object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
    }
}