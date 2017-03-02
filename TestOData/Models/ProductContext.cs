using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestOData.Models
{
    public class ProductContext: DbContext
    {
        public ProductContext() 
                : base("ProductConnectionString")
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}