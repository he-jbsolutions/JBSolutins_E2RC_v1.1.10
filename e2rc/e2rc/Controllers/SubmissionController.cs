using System;
using System.Web.Mvc;
using System.Collections.Generic;
using e2rc.Models.Repository;
using e2rcModel;

using PagedList;

namespace e2rc.Controllers
{
    [Authorize]
    public class SubmissionController : BaseController
    {
        [HttpGet]
        public ActionResult Index(string search, int? page, string sortOrder = "")
        {
            ViewBag.FormNameSortParm = sortOrder == "FormName" ? "FormName_desc" : "FormName";
            ViewBag.CompanyNameSortParm = sortOrder == "CompanyName" ? "CompanyName_desc" : "CompanyName";
            ViewBag.ProjectNameSortParm = sortOrder == "ProjectName" ? "ProjectName_desc" : "ProjectName";
            ViewBag.locationSortParm = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.InspectorNameSortParm = sortOrder == "InspectorName" ? "InspectorName_desc" : "InspectorName";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";            
                var SubmissionList = SubmissionRepository.sortSubmissionList((long)User.User_ID, search, sortOrder);
                //var SubmissionList = SubmissionRepository.List();
                if (SubmissionList != null)
                {
                    return View(SubmissionList.ToPagedList(page ?? 1, 10));
                }
            
            return View();       
        }

        //[HttpGet]
        //public ActionResult Details(long? Inspection_ID)
        //{
        //    return View(SubmissionRepository.getInspectionDetails(Inspection_ID));
        //}

        [HttpGet]
        public JsonResult GetProjectName(string term)
         {
             return Json(SubmissionRepository.SearchByName(term), JsonRequestBehavior.AllowGet);
         }

        [HttpGet]
        public ActionResult ProjectWiseInpectionSubmission(long Location_ID,long Client_ID,long user_id=0 ,string display="",int? page = 1, string sortOrder = "")
        {
            ViewBag.User_ID = User.Role;
            bool isAllow = false;

            if (User.Role == "Reviewer")
            {
                isAllow = SubmissionRepository.getReviewerAllowWorkOrder((long)User.User_ID);
            }

            ViewBag.isAllow = isAllow.ToString();

            ViewBag.Display = display;
            //ViewBag.ClientNameSortParm = sortOrder == "ClientName" ? "ClientName_desc" : "ClientName";
            //ViewBag.ProjectNameSortParm = sortOrder == "ProjectName" ? "ProjectName_desc" : "ProjectName";
            //ViewBag.locationSortParm = sortOrder == "location" ? "location_desc" : "location";
            //ViewBag.InspectorNameSortParm = sortOrder == "InspectorName" ? "InspectorName_desc" : "InspectorName";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            //var ProjectWiseInpectionlist = SubmissionRepository.sortDisplayProjectWiseInspectionList(Location_ID, sortOrder);
           
            if (user_id == 0)
            {
                var ProjectWiseInpectionlist = SubmissionRepository.DisplayProjectWiseInspection(Location_ID, Client_ID,(long) User.User_ID,User.Role);
                if (ProjectWiseInpectionlist != null)
                {
                    return View(ProjectWiseInpectionlist.ToPagedList(page ?? 1, 10));
                }
            }
            else
            {
                var ProjectWiseInpectionlist = SubmissionRepository.DisplayProjectWiseInspection(Location_ID, Client_ID, user_id,User.Role);
               if (ProjectWiseInpectionlist != null)
               {
                   return View(ProjectWiseInpectionlist.ToPagedList(page ?? 1, 10));
               }
            }
            return View();
        }
    }
}
