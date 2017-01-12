using System;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using PagedList;

namespace e2rc.Controllers
{
    public class DirectorController : BaseController
    {
        //
        // GET: /Director/

        [HttpGet]
        public ActionResult Index(string search, string view, int? page, string sortOrder = "")
        {
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.UserNameSortParm = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            var DirectorList = DirectorRepository.sortDirectorDetails((long)User.User_ID, search, sortOrder, view);
                if (DirectorList != null)
                {
                    return View(DirectorList.ToPagedList(page ?? 1, 10));
                }
            
            return View();
        }

        //
        // GET: /Director/Details/5

        public ActionResult Details(long Director_ID)
        {
            if (User.Role == "Franchise Admin")
            {
                return View(DirectorRepository.Single(Director_ID, (long)User.User_ID));
            }
            else if (User.Role == "Owner")
            {
                long User_ID = DirectorRepository.getDirector_UserID((long)User.User_ID);
                return View(DirectorRepository.Single(Director_ID, User_ID));
            }
            else
            {
                return View(DirectorRepository.Single(Director_ID, (long)User.User_ID));
            }
        }

        //
        // GET: /Director/Create
         [HttpGet]
        public ActionResult Create()
        {
            return View(new DirectorModel() { Address = new AddressModel(), Date = DateTime.Now });
        }

        //
        // POST: /Director/Create

        [HttpPost]
        public ActionResult Create(DirectorModel directorModel)
        {
            try
            {
                // TODO: Add insert logic here
                DirectorRepository.Create(directorModel);
                return RedirectToAction("Index");

               
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Director/Edit/5

        [HttpGet]
        public ActionResult Edit(long? Director_ID)
        {
            return View(DirectorRepository.Single(Director_ID, (long)User.User_ID));
        }

        [HttpPost]
        public ActionResult Edit(DirectorModel DirectorModel)
        {
            // if (ModelState.IsValid)
            //{
            ViewBag.IsDirectorEdited = DirectorRepository.Edit(DirectorModel);
            ViewBag.directorname=DirectorModel.FirstName+' '+DirectorModel.LastName;
            //}
            return View(DirectorModel);
        }

        //
        // GET: /Director/Delete/5

        [HttpPost]
        public bool Delete(long? Director_ID)
        {
            return DirectorRepository.Delete(
                    new DirectorModel
                    {
                        Director_ID = Director_ID,
                        CreatedBy_ID = User.User_ID
                    }
                );
        }

        [HttpGet]
        public ActionResult Report(string search, int? page, string sortOrder = "")
        {

            ViewBag.FormNameSortParm = sortOrder == "FormName" ? "FormName_desc" : "FormName";
            ViewBag.ClientNameSortParm = sortOrder == "ClientName" ? "ClientName_desc" : "ClientName";
            ViewBag.ProjectNameSortParm = sortOrder == "ProjectName" ? "ProjectName_desc" : "ProjectName";
            ViewBag.locationSortParm = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.InspectorNameSortParm = sortOrder == "InspectorName" ? "InspectorName_desc" : "InspectorName";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

           // var SubmissionList = DirectorRepository.sortReportList((long)User.User_ID, search, sortOrder);
            // var SubmissionList = InspectorRepository.CompleteList((long)User.User_ID);
            //if (SubmissionList != null)
            //{
            //    return View(SubmissionList.ToPagedList(page ?? 1, 10));
            //}

            return View();
        }

        [HttpPost]
        public JsonResult UpdateDirectorStatusList(long[] chkedDirector_ID, long[] unchkDirector_ID)
        {
            var chkedDirector_Ids = chkedDirector_ID;
            var unchkDirector_IDs = unchkDirector_ID;
            bool chkedResult = true, unkchkedResult = true;

            if (chkedDirector_Ids != null)
            {
                string view = "Active";
                foreach (var director in chkedDirector_Ids)
                {
                    chkedResult = DirectorRepository.UpdateDirectorStatus(Convert.ToInt64(director), view);
                }
            }
            if (unchkDirector_IDs != null)
            {
                string view = "InActive";
                foreach (var director in unchkDirector_IDs)
                {
                    unkchkedResult = DirectorRepository.UpdateDirectorStatus(Convert.ToInt64(director), view);
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

        [HttpGet]
        public JsonResult GetDirectorName(string term)
        {
            return Json(DirectorRepository.SearchByName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }
        
    }
}
