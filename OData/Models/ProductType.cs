using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace OData.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        public string Name { get; set; }

       
        public ICollection<Product> Products { get; set; }
    }
}