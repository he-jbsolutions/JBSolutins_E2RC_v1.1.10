using System;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using PagedList;

namespace e2rc.Controllers
{
    [Authorize]
    public class LocationAssignController : BaseController
    {
        //
        // GET: /Assign/

        public ActionResult Index(string search, int? page)
        {
            if (String.IsNullOrEmpty(search))
            {
                var LocationAssignList = LocationAssignRepository.List((long)User.User_ID);

                if (LocationAssignList != null)
                {
                    return View(LocationAssignList.ToPagedList(page ?? 1, 10));
                }
            }
            else
            {
                var LocationAssignList = LocationAssignRepository.List(search, (long)User.User_ID);
                if (LocationAssignList != null)
                {
                    return View(LocationAssignList.ToPagedList(page ?? 1, 10));
                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View(new LocationAssignModel() { Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(LocationAssignModel LocationAssignModel)
        {

            LocationAssignRepository.Create(LocationAssignModel);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Edit(long? Assign_ID)
        {
            return View(LocationAssignRepository.Single(Assign_ID, (long)User.User_ID));
        }

        [HttpGet]
        public JsonResult GetName(string term)
        {
            return Json(LocationAssignRepository.SearchByName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(LocationAssignModel LocationAssignModel)
        {
            //  if (ModelState.IsValid)
            //{
            ViewBag.IsAssignLocationEdited = LocationAssignRepository.Edit(LocationAssignModel);
            // }
            return View(LocationAssignModel);
        }


        [HttpPost]
        public bool Delete(long Assign_ID)
        {
            return LocationAssignRepository.Delete(
                    new LocationAssignModel
                    {
                        Assign_ID = Assign_ID,
                    }
                );
        }

        [HttpGet]
        public JsonResult GetInspectorDetails()
        {
            return Json(LocationAssignRepository.GetInspectorDetails((long)User.User_ID), JsonRequestBehavior.AllowGet);
        }

    }
}
