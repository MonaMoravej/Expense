using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;


using System.Web.Http;
using OData.Models;
using System.Net;
using System.Web.OData.Routing;

namespace OData.Controllers
{
    public class ProductController : ODataController
    {
        ODataDbContext _db = new ODataDbContext();

        // [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_db.Products);
        }

       // [ODataRoute("Product({Id})")]
        public IHttpActionResult Get([FromODataUri]int Id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == Id);
            if (product == null) { return NotFound();   }
            return Ok(product);
        }




        // GET /Products(1)/ProductType
        [EnableQuery]
        public SingleResult<ProductType> GetProductType([FromODataUri] int key)
        {
            var result = _db.Products.Where(m => m.Id == key).Select(m => m.ProductType);
            return SingleResult.Create(result);
        }





        public IHttpActionResult Post(Product product)
        {
            _db.Products.Add(product);
            return Ok(product);
        }

        //update entire entity
        public IHttpActionResult Put(Product product)
        {
            return Ok();
        }

        //update partially
        public IHttpActionResult Patch(int Id, Delta<Product> product)
        {
            return Ok();
        }

        public IHttpActionResult Delete(int Id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == Id);
            if (product == null)
            {
                return NotFound();
                
            }
            else
            {
               _db.Products.Remove(product);
                return Ok();
    }
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);

        }
    }
}