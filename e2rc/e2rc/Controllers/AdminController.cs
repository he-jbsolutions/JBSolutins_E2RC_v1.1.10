using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e2rc.Models.Security;

namespace e2rc.Controllers
{
    [CustomAuthorizeAttribute(Roles = "Super Admin")]
    public class AdminController : BaseController
    {        
        public ActionResult Index()
        {
            return View();
        }
    }
}
