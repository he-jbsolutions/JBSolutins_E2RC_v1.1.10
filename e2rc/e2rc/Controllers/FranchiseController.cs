using System;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using e2rcModel.BusinessLayer;
using PagedList;
using System.Configuration;
using System.Drawing;

namespace e2rc.Controllers
{
    [Authorize]
    public class FranchiseController : BaseController
    {   
        [HttpGet]
        public ActionResult Index(string search, string view, int? page, string sortOrder = "")
        {
            ViewBag.CompanyNameSortParm = sortOrder == "CompanyName" ? "CompanyName_desc" : "CompanyName";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.UserNameSortParm = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            var franchiseList = FranchiseRepository.sortFranchiseDetails(search, sortOrder, view);              
            if (franchiseList != null)
            {
                return View(franchiseList.ToPagedList(page ?? 1, 10));
            }            
            return View();
        }

        [HttpGet]
        public ActionResult Create(string view)
        {

            ViewBag.View = view;
            return View(new FranchiseModel() { Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(FranchiseModel FranchiseModel)
        {
            if (FranchiseRepository.checkIsImageValid(FranchiseModel))
            {
                FranchiseRepository.Create(FranchiseModel);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMsg = "Please upload logo with maximum height 600 * 100 PX";
                return View(FranchiseModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(long? Franchise_ID)
        {
            FranchiseModel Franchisemodel;
            if (User.Role == "Super Admin")
            {
                Franchisemodel = FranchiseRepository.Single((long)Franchise_ID);
                Franchisemodel.UploadLogoUrl = ConfigurationManager.AppSettings["CompanyLogo"] + Franchisemodel.UploadLogoUrl;
                return View(Franchisemodel);
            }
            else
            {
                Franchisemodel = FranchiseRepository.Single((long)FranchiseRepository.FranchiseID(User.User_ID));
                Franchisemodel.UploadLogoUrl = ConfigurationManager.AppSettings["CompanyLogo"] + Franchisemodel.UploadLogoUrl;
                return View(Franchisemodel);
            }
        }

        [HttpPost]
        public ActionResult Edit(FranchiseModel Franchisemodel)
        {
            if (FranchiseRepository.checkIsImageValid(Franchisemodel))
            {
                ViewBag.IsFranchiseEdited = FranchiseRepository.Edit(Franchisemodel);
                ViewBag.FranchiseName = Franchisemodel.AdminUser.FirstName + ' ' + Franchisemodel.AdminUser.LastName;

                Franchisemodel.UploadLogoUrl = ConfigurationManager.AppSettings["CompanyLogo"] + Franchisemodel.UploadLogoUrl;
                if (!String.IsNullOrEmpty(Franchisemodel.UploadLogoUrl) && User.Role != "Super Admin")
                {
                    Session["CompanyLogoPath"] = ConfigurationManager.AppSettings["CompanyLogo"] + Franchisemodel.UploadLogoUrl;
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Please upload logo with maximum height 600 * 100 PX";
            }
            return View(Franchisemodel);
            //return RedirectToAction("Edit", new { Franchise_ID = Franchisemodel.Franchise_ID});
        }

        public ActionResult Account(long? Franchise_ID)
        {
            FranchiseModel franchiseModel = new FranchiseModel();
            franchiseModel.Franchise_ID = Franchise_ID;
            return View(franchiseModel);
        }

        public ActionResult LogoUpdate(long? Franchise_ID)
        {
            FranchiseModel franchiseModel = new FranchiseModel();
            if (Session["CompanyLogoPath"] != null   )
            {
                string[] sLogoImage = (Session["CompanyLogoPath"].ToString()).Split('/');
                franchiseModel.slogoName = (Session["CompanyLogoPath"].ToString()).Split('/')[(sLogoImage.Length) - 1];
            }
            franchiseModel.Franchise_ID = Franchise_ID;
            return View(franchiseModel);
        }

        [HttpPost]
        public ActionResult LogoUpdate(FranchiseModel Franchisemodel)
        {
            if (FranchiseRepository.checkIsImageValid(Franchisemodel))
            {
                Franchisemodel.FraCompName = Session["Name"].ToString();
                ViewBag.IsFranchiseEdited = FranchiseRepository.LogoUpdate(Franchisemodel);

                if (!String.IsNullOrEmpty(Franchisemodel.UploadLogoUrl))
                {
                    Session["CompanyLogoPath"] = ConfigurationManager.AppSettings["CompanyLogo"] + Franchisemodel.UploadLogoUrl;
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Please upload logo with maximum height 600 * 100 PX";

                if (Session["CompanyLogoPath"] != null)
                {
                    string[] sLogoImage = (Session["CompanyLogoPath"].ToString()).Split('/');
                    Franchisemodel.slogoName = (Session["CompanyLogoPath"].ToString()).Split('/')[(sLogoImage.Length) - 1];
                }
            }
            return View(Franchisemodel);
        }

        [HttpGet]
        public ActionResult Details(long? Franchise_ID)
        {
            //return View(FranchiseRepository.Single((long)Franchise_ID));
            if (User.Role == "Super Admin")
            {
                return View(FranchiseRepository.Single((long)Franchise_ID));
            }
            else
            {
                return View(FranchiseRepository.Single((long)FranchiseRepository.FranchiseID(User.User_ID)));
            }
        }

        [HttpPost]
        public bool Delete(long? Franchise_ID)
        {
            return FranchiseRepository.Delete(
                new FranchiseModel
                {
                    Franchise_ID = Franchise_ID,
                    IsActive = false,
                    CreatedBy_ID = User.User_ID
                });
        }

        [HttpGet]
        public ActionResult SubmissionIndex(string search, int? page,Int64 user_id=0, string sortOrder = "")
        {
            ViewBag.FormNameSortParm = sortOrder == "FormName" ? "FormName_desc" : "FormName";
            ViewBag.ClientNameSortParm = sortOrder == "ClientName" ? "ClientName_desc" : "ClientName";
            ViewBag.ProjectNameSortParm = sortOrder == "ProjectName" ? "ProjectName_desc" : "ProjectName";
            ViewBag.locationSortParm = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.InspectorNameSortParm = sortOrder == "InspectorName" ? "InspectorName_desc" : "InspectorName";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            if (User.Role == "Super Admin")
            {
                if (String.IsNullOrEmpty(search))
                {
                    var SubmissionList = FranchiseRepository.sortSubmissionList(user_id, search, sortOrder);
                    if (SubmissionList != null)
                    {
                        return View(SubmissionList.ToPagedList(page ?? 1, 10));
                    }
                }
                else
                {
                    var SubmissionList = FranchiseRepository.sortSubmissionList(user_id, search, sortOrder);
                    if (SubmissionList != null)
                    {
                        return View(SubmissionList.ToPagedList(page ?? 1, 10));
                    }
                }
            }
            else
            {
                if (String.IsNullOrEmpty(search))
                {
                    var SubmissionList = FranchiseRepository.sortSubmissionList((long)User.User_ID, search, sortOrder);
                    if (SubmissionList != null)
                    {
                        return View(SubmissionList.ToPagedList(page ?? 1, 10));
                    }
                }
                else
                {
                    var SubmissionList = FranchiseRepository.sortSubmissionList((long)User.User_ID, search, sortOrder);
                    if (SubmissionList != null)
                    {
                        return View(SubmissionList.ToPagedList(page ?? 1, 10));
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult FranchiseSubmissionIndex(string search, int? page, Int64 user_id = 0, string sortOrder = "")
        {
            ViewBag.FormNameSortParm = sortOrder == "FormName" ? "FormName_desc" : "FormName";
            ViewBag.ClientNameSortParm = sortOrder == "ClientName" ? "ClientName_desc" : "ClientName";
            ViewBag.ProjectNameSortParm = sortOrder == "ProjectName" ? "ProjectName_desc" : "ProjectName";
            ViewBag.locationSortParm = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.InspectorNameSortParm = sortOrder == "InspectorName" ? "InspectorName_desc" : "InspectorName";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            if (User.Role == "Super Admin")
            {
                if (String.IsNullOrEmpty(search))
                {
                    var SubmissionList = FranchiseRepository.sortFranchiseWiseSubmissionList(user_id, search, sortOrder);
                    if (SubmissionList != null)
                    {
                        return View(SubmissionList.ToPagedList(page ?? 1, 10));
                    }
                }
                else
                {
                    var SubmissionList = FranchiseRepository.sortFranchiseWiseSubmissionList(user_id, search, sortOrder);
                    if (SubmissionList != null)
                    {
                        return View(SubmissionList.ToPagedList(page ?? 1, 10));
                    }
                }
            }
            else
            {
                if (String.IsNullOrEmpty(search))
                {
                    var SubmissionList = FranchiseRepository.sortFranchiseWiseSubmissionList((long)User.User_ID, search, sortOrder);
                    if (SubmissionList != null)
                    {
                        return View(SubmissionList.ToPagedList(page ?? 1, 10));
                    }
                }
                else
                {
                    var SubmissionList = FranchiseRepository.sortFranchiseWiseSubmissionList((long)User.User_ID, search, sortOrder);
                    if (SubmissionList != null)
                    {
                        return View(SubmissionList.ToPagedList(page ?? 1, 10));
                    }
                }
            }
            return View();
        }
               

        [HttpGet]
        public JsonResult GetFranchiseName(string term)
        {
            return Json(FranchiseRepository.SearchByName(term), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProjectName(string term)
        {
            return Json(FranchiseRepository.SearchByName(term,(long)User.User_ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateFranchiseStatus(long[] chkedFranchise_ID, long[] unchkFranchise_ID)
        {
            var chkedfranchise_Ids = chkedFranchise_ID;
            var unchkFranchise_IDs = unchkFranchise_ID;
            bool chkedResult = true, unkchkedResult = true;

            if (chkedfranchise_Ids != null)
            {
                foreach (var franchise in chkedfranchise_Ids)
                {
                    chkedResult = FranchiseRepository.UpdateFranchiseStatus(Convert.ToInt64(franchise));
                }
            }
            if (unchkFranchise_IDs != null)
            {
                foreach (var franchise in unchkFranchise_IDs)
                {
                    unkchkedResult = FranchiseRepository.DeActivateFranchiseStatus(Convert.ToInt64(franchise));
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

        //[HttpPost]
        //public JsonResult DeActivateFranchiseStatus(long Franchise_ID)
        //{
        //    return Json(FranchiseRepository.DeActivateFranchiseStatus(Franchise_ID));
        //}

        
    }
}
