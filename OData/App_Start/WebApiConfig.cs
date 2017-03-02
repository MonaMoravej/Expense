
using Microsoft.OData.Edm;
using OData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace OData
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            
            config.MapODataServiceRoute("ODataRoute","odata",GetEdmModel());
            config.EnsureInitialized();
        }

        private static IEdmModel GetEdmModel()
        {
            var buider = new ODataConventionModelBuilder();
            buider.Namespace = "OData";
            buider.ContainerName = "ODataContainer";

        
            buider.EntitySet<Product>("Product");
            buider.EntitySet<ProductType>("ProductType");

           
            return buider.GetEdmModel();

            //buider.EntitySet<Payee>("Payee");
            //buider.EntitySet<Transaction>("Transaction");
            // buider.EntitySet<Account>("Account");
            //buider.EntitySet<Budget>("Budget");
            //buider.EntitySet<Category>("Category");


        }
    }
}
