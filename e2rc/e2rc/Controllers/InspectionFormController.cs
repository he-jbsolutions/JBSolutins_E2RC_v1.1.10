using System;
using System.Linq;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using PagedList;
namespace e2rc.Controllers
{
    [Authorize]
    public class InspectionFormController : BaseController
    {
        //
        // GET: /InspectionForm/
        public ActionResult Index(string role, string search, int? page, string sortOrder = "")
        {
            ViewBag.role=role;            
            if (!String.IsNullOrEmpty(search))
            {
                var InspectionForms = InspectionFormRepository.List(search);
                if (InspectionForms != null)
                {                   
                    return View(InspectionForms.ToList());
                }
            }
            else
            {
                var InspectionForms = InspectionFormRepository.List();
                if (InspectionForms != null)
                {
                     return View(InspectionForms.ToList());
                }
            } 
            return View();
        }

        //
        // GET: /InspectionForm/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /InspectionForm/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /InspectionForm/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /InspectionForm/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /InspectionForm/Edit/5
        [HttpPost]
        public JsonResult Edit(InspectionFormModel Form)
        {
            try
            {
                Form.CreatedBy_ID = (long)User.User_ID;

                return Json(InspectionFormRepository.Edit(Form) ? "Form updated sucessfull." : "unable to update the form");
            }
            catch
            {
                return Json("unable to update the form");
            }
        }

        //
        // GET: /InspectionForm/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /InspectionForm/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult GetFormName(string term)
        {
            return Json(InspectionFormRepository.SearchByFormName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }
    }
}
