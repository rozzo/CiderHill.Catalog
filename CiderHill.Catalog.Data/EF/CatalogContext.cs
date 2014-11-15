using CiderHill.Catalog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace CiderHill.Catalog.Data
{
    public class CatalogContext : DbContext
    {
        static CatalogContext()
        {
            //Database.SetInitializer<CatalogContext>(null);
        }

        public CatalogContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }
            else
            {
                modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            }
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Seed> Seeds { get; set; }
        public DbSet<SeedInventory> SeedInventories { get; set; }
        public DbSet<SeedNote> SeedNotes { get; set; }
        public DbSet<SeedPlanting> SeedPlantings { get; set; }
        public DbSet<Tree> Trees { get; set; }
        public DbSet<TreeNote> TreeNotes { get; set; }
        public DbSet<TreePlanting> TreePlantings { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        public int Save(string author)
        {
            SetCreatedModified(author);
            return this.SaveChanges();
        }

        /// <summary>
        /// Set the modified and created by field for all added/modified entities.
        /// </summary>
        private void SetCreatedModified(string author)
        {
            // Find all entities that are modified or added
            var entries = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added
                || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                var propNames = new List<string> { "CREATED", "CREATEDBY", "MODIFIED", "MODIFIEDBY" };
                var props = entity.GetType().GetProperties().Where(x => propNames.Contains(x.Name.ToUpper()));

                foreach (var prop in props)
                {
                    // For all adds set the created properties
                    if (entry.State == EntityState.Added)
                    {
                        if (prop.Name.ToUpper() == "CREATED")
                            prop.SetValue(entity, System.DateTime.Now, null);
                        else if (prop.Name.ToUpper() == "CREATEDBY")
                            prop.SetValue(entity, author, null);
                    }

                    // Always set modified properties
                    if (prop.Name.ToUpper() == "MODIFIED")
                        prop.SetValue(entity, System.DateTime.Now, null);
                    else if (prop.Name.ToUpper() == "MODIFIEDBY")
                        prop.SetValue(entity, author, null);
                }
            }
        }
    }
}
