using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace e2rc.Models.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {         
                if (!String.IsNullOrEmpty(Roles))
                {
                    if (!CurrentUser.IsInRole(Roles))
                    {
                        filterContext.Result = new RedirectToRouteResult(new
                     RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                       // base.OnAuthorization(filterContext); //returns to login url
                    }
                }
                if (!String.IsNullOrEmpty(Users))
                {
                    if (!Users.Contains(CurrentUser.User_ID.ToString()))
                    {
                        filterContext.Result = new RedirectToRouteResult(new
                     RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                       // base.OnAuthorization(filterContext); //returns to login url
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }           
        }
    }
}