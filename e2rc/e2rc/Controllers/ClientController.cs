using System;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using PagedList;

namespace e2rc.Controllers
{   
    [Authorize]
    public class ClientController : BaseController
    {   
        [HttpGet]
        public ActionResult Index(string search, string view, int? page, Int64 user_id=0, string sortOrder = "")
        {

            ViewBag.CompanyNameSortParm = sortOrder == "CompanyName" ? "CompanyName_desc" : "CompanyName";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.UserNameSortParm = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            
            if (User.Role == "Super Admin")
            {
                var clientList = ClientRepository.sortClientDetails(user_id, search, sortOrder, view);
                 if (clientList != null)
                 {
                     return View(clientList.ToPagedList(page ?? 1, 10));
                 }
            }
            else
            {
               var  clientList = ClientRepository.sortClientDetails((long)User.User_ID, search, sortOrder, view);
                 if (clientList != null)
                 {
                     return View(clientList.ToPagedList(page ?? 1, 10));
                 }
            } 
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ClientModel() { Address = new AddressModel(), Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(ClientModel clientModel)
        {
          //  if (ModelState.IsValid)
           // {
                ClientRepository.Create(clientModel);
                return RedirectToAction("Index");
           // }
            //return View(clientModel);
        }

        [HttpGet]
        public ActionResult Edit(long? Client_ID,long user_id=0)
        {
            if (User.Role == "Super Admin")
            {
                return View(ClientRepository.Single(Client_ID,user_id));
            }
            else
            {
                return View(ClientRepository.Single(Client_ID, (long)User.User_ID));
            }
        }

        [HttpPost]
        public ActionResult Edit(ClientModel clientModel)
        {
           // if (ModelState.IsValid)
            //{
                ViewBag.IsClientEdited = ClientRepository.Edit(clientModel);
            //}
            return View(clientModel);
        }

        [HttpGet]
        public ActionResult Details(long Client_ID,long user_id=0)
        {
            if (User.Role == "Franchise Admin")
            {
                return View(ClientRepository.Single(Client_ID, (long)User.User_ID));
            }
            else if (User.Role == "Company")
            {
                long User_ID = ClientRepository.getClient_UserID((long)User.User_ID);
                return View(ClientRepository.Single(Client_ID, User_ID));
            }

            else if (User.Role == "Super Admin")
            {
                return View(ClientRepository.Single(Client_ID, user_id));
            }
            else
            {
                return View(ClientRepository.Single(Client_ID, (long)User.User_ID));
            }           
        }

        [HttpPost]
        public bool Delete(long? Client_ID)
        {
            return ClientRepository.Delete(
                    new ClientModel
                    {
                        Client_ID = Client_ID,
                        CreatedBy_ID = User.User_ID
                    }
                );
        }

        [HttpGet]
        public ActionResult SubmissionIndex(string search,int? page,string sortOrder = "")
        {
            ViewBag.FormNameSortParm = sortOrder == "FormName" ? "FormName_desc" : "FormName";
            ViewBag.ProjectNameSortParm = sortOrder == "ProjectName" ? "ProjectName_desc" : "ProjectName";
            ViewBag.locationSortParm = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.InspectorNameSortParm = sortOrder == "InspectorName" ? "InspectorName_desc" : "InspectorName";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";               
            var SubmissionList = ClientRepository.sortSubmissionList((long)User.User_ID, search, sortOrder);
            if (SubmissionList != null)
            {
                return View(SubmissionList.ToPagedList(page ?? 1, 10));
            }  
            return View();
        }

        [HttpGet]
        public JsonResult GetClientName(string term ,long user_id=0)
        {
            //if (User.Role == "Super Admin")
            //{
            //    return Json(ClientRepository.SearchByName(term,(long)user_id, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
                return Json(ClientRepository.SearchByName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpGet]
        public JsonResult GetProjectName(string term)
        {
            return Json(ClientRepository.SearchByProjectName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }  
     
        
        [HttpPost]
        public JsonResult UpdateClientStatus(long Client_ID)
        {
            return Json(ClientRepository.UpdateClientStatus(Client_ID));
        }

        [HttpPost]
        public JsonResult UpdateClientStatusList(long[] chkedClient_ID, long[] unchkClient_ID)
        {
            var chkedClient_Ids = chkedClient_ID;
            var unchkClient_IDs = unchkClient_ID;
            bool chkedResult = true, unkchkedResult = true;

            if (chkedClient_Ids != null)
            {
                foreach (var client in chkedClient_Ids)
                {
                    chkedResult = ClientRepository.ActivateClientStatus(Convert.ToInt64(client));
                }
            }
            if (unchkClient_IDs != null)
            {
                foreach (var client in unchkClient_IDs)
                {
                    unkchkedResult = ClientRepository.UpdateClientStatus(Convert.ToInt64(client));
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

        [HttpPost]
        public JsonResult DeActivateFranchiseStatus(long Franchise_ID)
        {
            return Json(FranchiseRepository.DeActivateFranchiseStatus(Franchise_ID));
        }

        [HttpGet]
        public ActionResult DisplayActiveClient(string search, int? page, string sortOrder = "", long user_id = 0)
        {

            ViewBag.UserNameSortParm = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewBag.CompanyNameSortParm = sortOrder == "CompanyName" ? "CompanyName_desc" : "CompanyName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";               
            var ActiveClientList = ClientRepository.sortActiveClientList(user_id,search,sortOrder);
            if (ActiveClientList != null)
            {
                return View(ActiveClientList.ToPagedList(page ?? 1, 10));
            }  
            return View();
        }
    }
}
