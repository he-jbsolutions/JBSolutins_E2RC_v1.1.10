
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e2rc.Models.Security;
using e2rc.Models.Repository;
using System.Dynamic;

namespace e2rc.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        public JsonResult IsUserNameAvailable(string UserName, long? User_ID = 0)
        {
            return Json(UserRepository.IsUserNameAvailabe(UserName, User_ID), JsonRequestBehavior.AllowGet);
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.CurrentUser = HttpContext.User as CustomPrincipal;
            //base.OnActionExecuted(filterContext);
        }
    }
}

namespace System
{
    public static class ExpandoHelper
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }

    }
}