using System;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using PagedList;
namespace e2rc.Controllers
{
    public class FranchiseAssignLocationToClientController : BaseController
    {
        //
        // GET: /FranchiseAssignLocationToInspector/

        public ActionResult Index(string search, int? page)
        {
            if (String.IsNullOrEmpty(search))
            {
                var LocationAssignList = FranchiseAssignLocationToClientRepository.List((long)User.User_ID);

                if (LocationAssignList != null)
                {
                    return View(LocationAssignList.ToPagedList(page ?? 1, 10));
                }
            }
            else
            {
                var LocationAssignList = FranchiseAssignLocationToClientRepository.List(search, (long)User.User_ID);
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
            return View(new FranchiseAssignLocationToClientModel() { Date = DateTime.Now });
        }

        //
        // POST: /FranchiseAssignLocationToInspector/Create

        [HttpPost]
        public ActionResult Create(FranchiseAssignLocationToClientModel FranchiseAssignLocationToClientModel, bool RemoveAccess)
        {
            if (RemoveAccess != true)
            {
                // TODO: Add insert logic here
                FranchiseAssignLocationToClientRepository.Create(FranchiseAssignLocationToClientModel);
                return RedirectToAction("Index");
            }
            else
            {
                FranchiseAssignLocationToClientRepository.RemoveProjectAccess(FranchiseAssignLocationToClientModel);
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /FranchiseAssignLocationToInspector/Edit/5
        [HttpGet]
        public ActionResult Edit(long? Assign_ID)
        {
            return View(FranchiseAssignLocationToClientRepository.Single(Assign_ID, (long)User.User_ID));
        }

        //
        // POST: /FranchiseAssignLocationToInspector/Edit/5

        [HttpPost]
        public ActionResult Edit(FranchiseAssignLocationToClientModel FranchiseAssignLocationToClientModel)
        {
            ViewBag.IsFranchiseAssignLocationEdited = FranchiseAssignLocationToClientRepository.Edit(FranchiseAssignLocationToClientModel);
            return View(FranchiseAssignLocationToClientModel);
        }

        //
        // GET: /FranchiseAssignLocationToInspector/Delete/5

        public bool Delete(long Assign_ID)
        {
            return FranchiseAssignLocationToClientRepository.Delete(
                     new FranchiseAssignLocationToClientModel
                     {
                         Assign_ID = Assign_ID,
                     }
                 );
        }

        [HttpGet]
        public JsonResult GetName(string term)
        {
            return Json(FranchiseAssignLocationToClientRepository.SearchByName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetReviewerClientsLocation(long Client_ID)
        {
            return Json(FranchiseAssignLocationToClientRepository.GetReviewerClientsLocation((long)User.User_ID,Client_ID));
        }

        [HttpPost]
        public JsonResult GetReviewerClients(long Reviewer_ID)
        {
            return Json(FranchiseAssignLocationToClientRepository.GetReviewerClients((long)User.User_ID, Reviewer_ID));
        }
    }
}
