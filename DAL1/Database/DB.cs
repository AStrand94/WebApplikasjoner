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
            if (modifiedEntities.Any()) LogChangesToEntity(modifiedEntities);

            var addedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();
            if (addedEntities.Any()) LogNewEntities(addedEntities);

            var deletedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted).ToList();
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
                StringBuilder stringBuilder = new StringBuilder();
                var entityName = change.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(change);
                stringBuilder.Append("Entity ").Append(entityName).Append(" has changed:\n");

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.OriginalValues[prop].ToString();
                    var currentValue = change.CurrentValues[prop].ToString();
                    if (originalValue != currentValue)
                    {
                        stringBuilder.Append("Column '").Append(prop).Append("' old val: ").Append(originalValue).Append(" new val: ").Append(currentValue).Append('\n');
                    }
                }
                LogHelper.Log(stringBuilder.ToString());
            }
        }

        public void LogNewEntities(List<DbEntityEntry> addedEntities)
        {
            foreach (var added in addedEntities)
            {
                StringBuilder stringBuilder = new StringBuilder();
                var entityName = added.Entity.GetType().Name;
                stringBuilder.Append("Entity has been added in ").Append(entityName).Append(" with values:\n");

                foreach (var prop in added.CurrentValues.PropertyNames)
                {
                    var currentValue = added.CurrentValues[prop].ToString();
                    stringBuilder.Append("Column '").Append(prop).Append(" val: ").Append(currentValue).Append('\n');
                }
                LogHelper.Log(stringBuilder.ToString());
            }
        }

        public void LogDeletedEntities(List<DbEntityEntry> deletedEntities)
        {
            foreach (var added in deletedEntities)
            {
                StringBuilder stringBuilder = new StringBuilder();
                var entityName = added.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(added);
                stringBuilder.Append("Entity in table ").Append(entityName).Append(" with id ").Append(primaryKey).Append(" has been deleted");
                LogHelper.Log(stringBuilder.ToString());
            }
        }

        private object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
    }
}