using System;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using PagedList;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
namespace e2rc.Controllers
{
    [Authorize]
    public class ProjectManagerController : BaseController
    {
        [HttpGet]
        public ActionResult Index(string search,string view ,string sortOrder,int? page)
        {
            ViewBag.NameSortParm = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewBag.UserNameSortParm = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";  
           
                var ProjectManagerList = ProjectManagerRepository.sortPMDetails((long)User.User_ID, search, sortOrder, view);
                //var ProjectManagerList = ProjectManagerRepository.List((long)User.User_ID);
                if (ProjectManagerList != null)
                {
                    return View(ProjectManagerList.ToPagedList(page ?? 1, 10));
                }
           
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProjectManagerModel() { Address = new AddressModel(), Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(ProjectManagerModel ProjectManagerModel)
        {
            if (Request.Form["location.Location_ID"] != null)
            {
                ProjectManagerModel.selectedLocationIDs = Request.Form["location.Location_ID"].Split(',');
            }
            ProjectManagerRepository.Create(ProjectManagerModel);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Edit(long? ProjectManager_ID)
        {

            ProjectManagerModel pm = ProjectManagerRepository.Single(ProjectManager_ID, (long)User.User_ID);
            if (pm.LocationIDs != null)
            {
                foreach (long locationId in pm.LocationIDs)
                {
                    try
                    {
                        pm.hfSelectedLocations += "," + locationId;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (!string.IsNullOrEmpty(pm.hfSelectedLocations))
            {
                pm.hfSelectedLocations = pm.hfSelectedLocations.Remove(0, 1);
            }            
            return View(pm);
        }

        [HttpPost]
        public ActionResult Edit(ProjectManagerModel ProjectManagerModel)
        {           
            ViewBag.IsProjectManagerEdited = ProjectManagerRepository.Edit(ProjectManagerModel);
            ViewBag.PMname = ProjectManagerModel.FirstName + ' ' + ProjectManagerModel.LastName;
            ProjectManagerModel.hfSelectedLocations = string.Empty;
            if (ProjectManagerModel.LocationIDs != null)
            {
                foreach (long locationId in ProjectManagerModel.LocationIDs)
                {
                    try
                    {
                        ProjectManagerModel.hfSelectedLocations += "," + locationId;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (!string.IsNullOrEmpty(ProjectManagerModel.hfSelectedLocations))
            {
                ProjectManagerModel.hfSelectedLocations = ProjectManagerModel.hfSelectedLocations.Remove(0, 1);
            }             
            return View(ProjectManagerModel);
        }

        [HttpGet]
        public ActionResult Details(long ProjectManager_ID)
        {
            ProjectManagerModel pm;
            if (User.Role == "Franchise Admin")
            {
                 pm = ProjectManagerRepository.Single(ProjectManager_ID, (long)User.User_ID);
            }
            else if (User.Role == "Project Manager")
            {
                long User_ID = ProjectManagerRepository.getProjectManager_UserID((long)User.User_ID);
                pm = ProjectManagerRepository.Single(ProjectManager_ID, User_ID);
            }
            else
            {
                pm = ProjectManagerRepository.Single(ProjectManager_ID, (long)User.User_ID);
            }
            if (pm.LocationIDs != null)
            {
                foreach (long locationId in pm.LocationIDs)
                {
                    try
                    {
                        pm.hfSelectedLocations += "," + locationId;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (!string.IsNullOrEmpty(pm.hfSelectedLocations))
            {
                pm.hfSelectedLocations = pm.hfSelectedLocations.Remove(0, 1);
            }

            return View(pm);
        }

        [HttpPost]
        public bool Delete(long ProjectManager_ID)
        {
            return ProjectManagerRepository.Delete(
                    new ProjectManagerModel
                    {
                        ProjectManager_ID = ProjectManager_ID,
                        CreatedBy_ID = User.User_ID
                    }
                );
        }

        [HttpGet]
        public JsonResult GetName(string term)
        {
            return Json(ProjectManagerRepository.SearchByName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateProjectManagerStatusList(long[] chkedProjectManager_ID, long[] unchkProjectManager_ID)
        {
            var chkedPM_Ids = chkedProjectManager_ID;
            var unchkPM_IDs = unchkProjectManager_ID;
            bool chkedResult = true, unkchkedResult = true;

            if (chkedPM_Ids != null)
            {
                string view = "Active";
                foreach (var pm in chkedPM_Ids)
                {
                    chkedResult = ProjectManagerRepository.UpdateProjectManagerStatus(Convert.ToInt64(pm), view);
                }
            }
            if (unchkPM_IDs != null)
            {
                string view = "InActive";
                foreach (var pm in unchkPM_IDs)
                {
                    unkchkedResult = ProjectManagerRepository.UpdateProjectManagerStatus(Convert.ToInt64(pm), view);
                }
            }
            if ((chkedResult) && (unkchkedResult))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }


            //return Json(FranchiseRepository.UpdateFranchiseStatus());
        }




        //[HttpGet]
        //public ActionResult SubmissionIndex(string search, int? page)
        //{
        //    if (String.IsNullOrEmpty(search))
        //    {
        //        var SubmissionList = InspectorRepository.SubmissionList((long)User.User_ID);
        //        if (SubmissionList != null)
        //        {
        //            return View(SubmissionList.ToPagedList(page ?? 1, 10));
        //        }
        //    }
        //    else
        //    {
        //        var SubmissionList = InspectorRepository.SubmissionList(search, (long)User.User_ID);
        //        if (SubmissionList != null)
        //        {
        //            return View(SubmissionList.ToPagedList(page ?? 1, 10));
        //        }
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult CompleteIndex(string search, int? page)
        //{
        //    if (String.IsNullOrEmpty(search))
        //    {
        //        var SubmissionList = InspectorRepository.CompleteList((long)User.User_ID);
        //        if (SubmissionList != null)
        //        {
        //            return View(SubmissionList.ToPagedList(page ?? 1, 10));
        //        }
        //    }
        //    else
        //    {
        //        var SubmissionList = InspectorRepository.CompleteList(search, (long)User.User_ID);
        //        if (SubmissionList != null)
        //        {
        //            return View(SubmissionList.ToPagedList(page ?? 1, 10));
        //        }
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public JsonResult GetInspectorName(string term)
        //{
        //    return Json(InspectorRepository.SearchByName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public JsonResult GetProjectName(string term)
        //{
        //    return Json(InspectorRepository.SearchByProjectName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public JsonResult GetProjectNameEdit(string term)
        //{
        //    return Json(InspectorRepository.SearchByProjectNameEdit(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        //}
       
    }
}