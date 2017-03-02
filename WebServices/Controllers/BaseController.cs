using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebServices.Controllers
{
    public abstract class BaseController:Controller
    {
        public const string URLHELPER = "URLHELPER";
       // public const string USERID = "USERID";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            context.HttpContext.Items[URLHELPER] = Url;
           // context.HttpContext.Items[USERID] = "userId";
        }
    }
}
