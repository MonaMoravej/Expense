using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OData.Models
{
    public class ODataDbContext: DbContext
    {
        public ODataDbContext() : base("ODataConnection") { }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("OData");
            base.OnModelCreating(modelBuilder);

            //configuration for decimal type or maybe dateTime as well

        }

    }
}