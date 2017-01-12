using System;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using PagedList;


namespace e2rc.Controllers
{
    public class FranchiseAssignLocationToInspectorController : BaseController
    {
        //
        // GET: /FranchiseAssignLocationToInspector/

        public ActionResult Index(string search, int? page)
        {
            if (String.IsNullOrEmpty(search))
            {
                var LocationAssignList = FranchiseAssignLocationToInspectorRepository.List((long)User.User_ID);

                if (LocationAssignList != null)
                {
                    return View(LocationAssignList.ToPagedList(page ?? 1, 10));
                }
            }
            else
            {
                var LocationAssignList = FranchiseAssignLocationToInspectorRepository.List(search, (long)User.User_ID);
                if (LocationAssignList != null)
                {
                    return View(LocationAssignList.ToPagedList(page ?? 1, 10));
                }
            }
            return View(); 
        }

        //
        // GET: /FranchiseAssignLocationToInspector/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FranchiseAssignLocationToInspector/Create
         [HttpGet]
        public ActionResult Create()
        {
            return View(new FranchiseAssignLocationToInspectorModel() { Date = DateTime.Now });
        }

        //
        // POST: /FranchiseAssignLocationToInspector/Create

        [HttpPost]
        public ActionResult Create(FranchiseAssignLocationToInspectorModel franchiseAssignLocationToInspectormodel, bool RemoveAccess)
        {
            if (RemoveAccess != true)
            {
                // TODO: Add insert logic here
                FranchiseAssignLocationToInspectorRepository.Create(franchiseAssignLocationToInspectormodel);
                return RedirectToAction("Index");
            }
            // Remove Access on project Selected Inspector
            else
            {
                FranchiseAssignLocationToInspectorRepository.RemoveProjectAccess(franchiseAssignLocationToInspectormodel);
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /FranchiseAssignLocationToInspector/Edit/5
          [HttpGet]
        public ActionResult Edit(long? Assign_ID)
        {
            return View(FranchiseAssignLocationToInspectorRepository.Single(Assign_ID, (long)User.User_ID));
        }

        //
        // POST: /FranchiseAssignLocationToInspector/Edit/5

        [HttpPost]
        public ActionResult Edit(FranchiseAssignLocationToInspectorModel franchiseAssignLocationToInspectormodel)
        {           
                ViewBag.IsFranchiseAssignLocationEdited = FranchiseAssignLocationToInspectorRepository.Edit(franchiseAssignLocationToInspectormodel);              
                return View(franchiseAssignLocationToInspectormodel);            
        }

        //
        // GET: /FranchiseAssignLocationToInspector/Delete/5

        public bool Delete(long Assign_ID)
        {
            return FranchiseAssignLocationToInspectorRepository.Delete(
                     new FranchiseAssignLocationToInspectorModel
                     {
                         Assign_ID = Assign_ID,
                     }
                 );
        }

        [HttpGet]
        public JsonResult GetName(string term)
        {
            return Json(FranchiseAssignLocationToInspectorRepository.SearchByName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }

        
    }
}
