using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.OData;

namespace OData.Helpers
{
    public static class ODataHelpers
    {

        public static bool HasProperty(this object instance,string propertyName)
        {
            var propertyInfo = instance.GetType().GetProperty(propertyName);
            return (propertyInfo != null);
        }

        public static object GetValue(this object instance, string propertyName)
        {
            var propertyInfo = instance.GetType().GetProperty(propertyName);
            if (propertyInfo==null)
            {
                throw new HttpException("Can not find a property with name : " + propertyName); 
            }
            var result =  propertyInfo.GetValue(instance, new object[] { });
            return result;//type:object
        }

        public static IHttpActionResult  CraeteOkActionResult(this ODataController controller, object propertyValue)
        {
            var okMethod = default(MethodInfo);

            var methodList = controller.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach(var method in methodList)
            {
                //we want ok with one parameter as result value
                if(method.Name=="Ok" && method.GetParameters().Length == 1)
                {
                    okMethod = method;
                    break;
                }

            }
           okMethod = okMethod.MakeGenericMethod(propertyValue.GetType());
           var returnValue =  okMethod.Invoke(controller, new object[] { propertyValue });
            return (IHttpActionResult) returnValue;
        }
    }
}