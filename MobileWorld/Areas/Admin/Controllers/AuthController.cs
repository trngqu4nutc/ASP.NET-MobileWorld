using MobileWorld.Common;
using MobileWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MobileWorld.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var session = (UserLogin)Session[Constants.USER_SESSION];
            if(session == null || session.role == 1)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Error", action = "Index", area = "" }));
            }
            base.OnActionExecuted(filterContext);
        }
    }
}