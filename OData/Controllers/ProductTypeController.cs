using OData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using OData.Helpers;

namespace OData.Controllers
{
    public class ProductTypeController:ODataController

    {
        ODataDbContext _db = new ODataDbContext();



        public IHttpActionResult Get()
        {
            return Ok(_db.ProductTypes);

        }

        public IHttpActionResult Get(int Id)
        {
            var pt = _db.ProductTypes.FirstOrDefault(p => p.Id == Id);
            if (pt == null) { return NotFound(); }
            return Ok(pt);
        }

        [HttpGet]
        [ODataRoute("ProductType({Id})/Name")]
        [ODataRoute("ProductType({Id})/Id")]
        public IHttpActionResult GetProductTypeProperty([FromODataUri] int Id)
        {
            var pt = _db.ProductTypes.FirstOrDefault(p => p.Id == Id);
            if (pt == null) { return NotFound(); }

            var propertyTypeName = Url.Request.RequestUri.Segments.Last();

            var hasProperty = pt.HasProperty(propertyTypeName);
            if (!hasProperty)
            {
                return NotFound();
            }

            //create Product[] by reflector
            var result = pt.GetValue(propertyTypeName);
            if (result == null)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);

            }

            return this.CraeteOkActionResult(result);


            //1. Has product property or not? notFound
            //2. Has property but not value, in this case we found a type but not has a list of product into int , so NoContent
            //3. has property and has a list of product into it ==> return the list 


        }


        //[HttpGet]
        //[ODataRoute("ProductType({Id})/Product")]
        //public IHttpActionResult GetProductTypeCollectionProperty([FromODataUri] int Id)
        //{
        //    var propertyTypeName = Url.Request.RequestUri.Segments.Last();

        //    var pt = _db.ProductTypes.Include(propertyTypeName).FirstOrDefault(p=>p.Id==Id);

        //    if (pt == null) { return NotFound(); }

        //    var collectionPropertyValue = pt.GetValue(propertyTypeName);

        //    return this.CraeteOkActionResult(collectionPropertyValue);


        //}

        // GET /ProductType(1)/Products
        [EnableQuery]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            return _db.ProductTypes.Where(m => m.Id.Equals(key)).SelectMany(m => m.Products);
        }




        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}