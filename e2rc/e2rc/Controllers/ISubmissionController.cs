using System;
namespace e2rc.Controllers
{
    interface ISubmissionController
    {
        System.Web.Mvc.ActionResult Details(long? Inspection_ID);
        System.Web.Mvc.JsonResult GetProjectName(string term);
        System.Web.Mvc.ActionResult Index(string search, int? page, string sortOrder = "");
    }
}
