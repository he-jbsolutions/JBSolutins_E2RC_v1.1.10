using System;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using PagedList;

namespace e2rc.Controllers
{
    public class ExecutorController : BaseController
    {
        //
        // GET: /Executive/

        public ActionResult Index(string search, string view, int? page, string sortOrder = "")
        {
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.UserNameSortParm = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            var ExecutorList = ExecutorRepository.sortExecutorDetails((long)User.User_ID, search, sortOrder, view);
            if (ExecutorList != null)
            {
                return View(ExecutorList.ToPagedList(page ?? 1, 10));
            }

            return View();
        }

        [HttpGet]
        public ActionResult Details(long Executor_ID)
        {
            if (User.Role == "Franchise Admin")
            {
                return View(ExecutorRepository.Single(Executor_ID, (long)User.User_ID));
            }
            else if (User.Role == "Operator")
            {
                long User_ID = ExecutorRepository.getExecutor_UserID((long)User.User_ID);
                return View(ExecutorRepository.Single(Executor_ID,User_ID));
            }
            else
            {
              return View(ExecutorRepository.Single(Executor_ID, (long)User.User_ID));
            }
        }
        //
        // GET: /Executive/Create

        // GET: /Director/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ExecutorModel() { Address = new AddressModel(), Date = DateTime.Now });
        }

        //
        // POST: /Executive/Create

        [HttpPost]
        public ActionResult Create(ExecutorModel ExecutorModel)
        {
            try
            {
                // TODO: Add insert logic here
                ExecutorRepository.Create(ExecutorModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Executive/Edit/5

        [HttpGet]
        public ActionResult Edit(long? Executor_ID)
        {
            return View(ExecutorRepository.Single(Executor_ID, (long)User.User_ID));
        }

        [HttpPost]
        public ActionResult Edit(ExecutorModel ExecutorModel)
        {
            // if (ModelState.IsValid)
            //{
            ViewBag.IsexecutorEdited = ExecutorRepository.Edit(ExecutorModel);
            ViewBag.ExecutorName = ExecutorModel.FirstName + ' ' + ExecutorModel.LastName;
            //}
            return View(ExecutorModel);
        }

        //
        // GET: /Executive/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Executive/Delete/5

        [HttpPost]
        public bool Delete(long? Executor_ID)
        {
            return ExecutorRepository.Delete(
                    new ExecutorModel
                    {
                        Executor_ID = Executor_ID,
                        CreatedBy_ID = User.User_ID
                    }
                );
        }


        [HttpPost]
        public JsonResult UpdateExecutorStatusList(long[] chkedExecutor_ID, long[] unchkExecutor_ID)
        {
            var chkedExecutor_Ids = chkedExecutor_ID;
            var unchkExecutor_IDs = unchkExecutor_ID;
            bool chkedResult = true, unkchkedResult = true;

            if (chkedExecutor_Ids != null)
            {
                string view = "Active";
                foreach (var executor in chkedExecutor_Ids)
                {
                    chkedResult = ExecutorRepository.UpdateExecutorStatus(Convert.ToInt64(executor), view);
                }
            }
            if (unchkExecutor_IDs != null)
            {
                string view = "InActive";
                foreach (var executor in unchkExecutor_IDs)
                {
                    unkchkedResult = ExecutorRepository.UpdateExecutorStatus(Convert.ToInt64(executor), view);
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
        }

        [HttpGet]
        public JsonResult GetOperatorName(string term)
        {
            return Json(ExecutorRepository.SearchByName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }
    }
}
