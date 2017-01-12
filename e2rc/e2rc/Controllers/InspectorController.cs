using System;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using PagedList;
using e2rcModel.Common;

namespace e2rc.Controllers
{
    [Authorize]  
    public class InspectorController : BaseController
    {
        [HttpGet]
        public ActionResult Index(string search,string view, int? page, string sortOrder = "")
        {          
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.UserNameSortParm = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            var InspectorList = InspectorRepository.sortInspetorDetails((long)User.User_ID, search, sortOrder, view);
            if (InspectorList != null)
            {
                return View(InspectorList.ToPagedList(page ?? 1, 10));
            }            
           return View();
        }

        [HttpGet]
        public ActionResult Create(string view)
        {
            ViewBag.view = view;
            return View(new InspectorModel() { Address = new AddressModel(), Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(InspectorModel inspectorModel)
        {
            if (inspectorModel.Role.Role_ID == 3)
            {
                InspectorRepository.Create(inspectorModel);
                return RedirectToAction("Index", "Client");
            }
            else
            {
                // if (ModelState.IsValid)
                // {
                if (inspectorModel.PostedFile == null)
                {
                    ModelState.AddModelError("PostedFile", "Signature Image Required.");
                    return View(inspectorModel);
                }
                InspectorRepository.Create(inspectorModel);

                /* Send Mail Notification for Assign Project and Inspector
                 bool result = InspectorRepository.Create(inspectorModel);
                string inspectorName = inspectorModel.FirstName + " " + inspectorModel.LastName;
                inspectorModel.slstLocationID = inspectorModel.lstLocation_ID != null ? string.Join(",", inspectorModel.lstLocation_ID) : string.Empty;
                if (result)
                {
                    MailSetting.SendMail(Actions.INSERT.ToString(), 0, inspectorModel.slstLocationID, "Inspector", inspectorName, inspectorModel.Email);
                }*/

                return RedirectToAction("Index");
                // }
                //return View(inspectorModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(long? ID,string view)
        {
            //ViewData["Client_ID"] = ID;
            ViewBag.view = view;
            if (view == "Client")
            {
                var client = ClientRepository.Single(ID, (long)User.User_ID);
                InspectorModel inspector = new InspectorModel();
                inspector.Client_ID = client.Client_ID;
                inspector.CompanyName = client.CompanyName;
                inspector.FirstName=client.FirstName;
                inspector.LastName=client.LastName;
                inspector.Date=client.Date;
                inspector.UserName=client.UserName;
                inspector.IsActive = client.IsActive;
                inspector.Password = client.Password;
                inspector.ConfirmPassword = client.Password;
                inspector.MobileNumber = client.MobileNumber;
                inspector.PhoneNumber = client.PhoneNumber;
                inspector.Email = client.Email;
                inspector.User_ID = client.User_ID;
                if (client.EditLogoPath == "")
                {
                    inspector.SignPath = "Not available";
                }
                else
                {
                    inspector.SignPath = client.EditLogoPath.Substring(client.EditLogoPath.LastIndexOf("/") + 1);
                }
            
                inspector.UploadSignPath = client.EditLogoPath;
                inspector.Role = new RoleModel
                {
                    Role_ID = client.Role.Role_ID,
                    Description = client.Role.Description
                };
                inspector.Address = new AddressModel
                {
                    City = client.Address.City,
                    MailingAddress = client.Address.MailingAddress,
                    State = new StateModel
                    {
                        State_ID = client.Address.State.State_ID,
                        Code = client.Address.State.Code,
                        Name = client.Address.State.Name
                    },
                    ZipCode = client.Address.ZipCode
                };
               // inspector.CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID;
                return View(inspector);
            }
            else
            {
                return View(InspectorRepository.Single(ID, (long)User.User_ID));
            }

           
        }

        [HttpPost]
        public ActionResult Edit(InspectorModel inspectorModel)
        {
            //if (inspectorModel.FileName != string.Empty)
            //{
            //    int fileSize = inspectorModel.PostedFile.ContentLength;

            //    if(fileSize > 10000)
            //    {

            //    }

                
            //}

            if (inspectorModel.Role.Role_ID == 3)
            {
                ViewBag.IsClientEdited = InspectorRepository.Edit(inspectorModel);
                ViewBag.ClientName = inspectorModel.CompanyName;
                return RedirectToAction("Index","Client");
            }
            else
            {
                //  if (ModelState.IsValid)
                //{
                ViewBag.InspectorName = inspectorModel.FirstName + " " + inspectorModel.LastName;

                 /* Send Mail Notification for Assign Project and Inspector
                inspectorModel.slstLocationID = inspectorModel.lstLocation_ID != null ? string.Join(",", inspectorModel.lstLocation_ID) : string.Empty;
                MailSetting.SendMail(Actions.UPDATE.ToString(), Convert.ToInt64(inspectorModel.Inspector_ID), inspectorModel.slstLocationID, "Inspector", ViewBag.InspectorName, inspectorModel.Email);*/

                ViewBag.IsInspectorEdited = InspectorRepository.Edit(inspectorModel);
              
                return RedirectToAction("Index");
                // }
                //return View(inspectorModel);
            }
        }

        [HttpGet]
        public ActionResult Details(long Inspector_ID)
        {
            if (User.Role == "Franchise Admin")
            {
                return View(InspectorRepository.Single(Inspector_ID, (long)User.User_ID));
            }
            else if (User.Role == "Inspector")
            {
                long User_ID=InspectorRepository.getInspector_UserID((long)User.User_ID);
                return View(InspectorRepository.Single(Inspector_ID, User_ID));
            }
            else
            {
                return View(InspectorRepository.Single(Inspector_ID, (long)User.User_ID));
            }
            
        }

        [HttpPost]
        public bool Delete(long? Inspector_ID)
        {
            return InspectorRepository.Delete(
                    new InspectorModel
                    {
                        Inspector_ID = Inspector_ID,
                        CreatedBy_ID = User.User_ID
                    }
                );
        }

        [HttpGet]
        public ActionResult SubmissionIndex(string search, int? page,string sortOrder = "")
        {
            ViewBag.FormNameSortParm = sortOrder == "FormName" ? "FormName_desc" : "FormName";
            ViewBag.CompanyNameSortParm = sortOrder == "CompanyName" ? "CompanyName_desc" : "CompanyName";
            ViewBag.ProjectNameSortParm = sortOrder == "ProjectName" ? "ProjectName_desc" : "ProjectName";
            ViewBag.locationSortParm = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.InspectorNameSortParm = sortOrder == "InspectorName" ? "InspectorName_desc" : "InspectorName";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";  
            var SubmissionList = InspectorRepository.sortSubmissionList((long)User.User_ID, search, sortOrder);          
            if (SubmissionList != null)
            {
                return View(SubmissionList.ToPagedList(page ?? 1, 10));
            }           
            return View();
        }

        [HttpGet]
        public ActionResult StationSubmissionIndex(string search, int? page,string sortOrder = "")
        {
            ViewBag.FormNameSortParm = sortOrder == "FormName" ? "FormName_desc" : "FormName";
            ViewBag.ProjectNameSortParm = sortOrder == "ProjectName" ? "ProjectName_desc" : "ProjectName";
            ViewBag.locationSortParm = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.InspectorNameSortParm = sortOrder == "InspectorName" ? "InspectorName_desc" : "InspectorName";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";   
            if (String.IsNullOrEmpty(search))
            {
                var StationSubmissionList = InspectorRepository.sortStationSubmissionList((long)User.User_ID, search, sortOrder);
                //var StationSubmissionList = InspectorRepository.StationSubmissionList((long)User.User_ID);
                if (StationSubmissionList != null)
                {
                    return View(StationSubmissionList.ToPagedList(page ?? 1, 10));
                }
            }
            else
            {
                var StationSubmissionList = InspectorRepository.sortStationSubmissionList((long)User.User_ID, search, sortOrder);
                //var StationSubmissionList = InspectorRepository.StationSubmissionList(search, (long)User.User_ID);
                if (StationSubmissionList != null)
                {
                    return View(StationSubmissionList.ToPagedList(page ?? 1, 10));
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult CompleteIndex(string search, int? page, string sortOrder = "")
        {

            ViewBag.FormNameSortParm = sortOrder == "FormName" ? "FormName_desc" : "FormName";
            ViewBag.CompanyNameSortParm = sortOrder == "CompanyName" ? "CompanyName_desc" : "CompanyName";
            ViewBag.ProjectNameSortParm = sortOrder == "ProjectName" ? "ProjectName_desc" : "ProjectName";
            ViewBag.locationSortParm = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.InspectorNameSortParm = sortOrder == "InspectorName" ? "InspectorName_desc" : "InspectorName";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";           
                         
            var SubmissionList = InspectorRepository.sortCompleteList((long)User.User_ID, search, sortOrder);
            // var SubmissionList = InspectorRepository.CompleteList((long)User.User_ID);
            if (SubmissionList != null)
            {
                return View(SubmissionList.ToPagedList(page ?? 1, 10));
            }

            return View();
        }

        //FOR STATION COMPLETE INDEX
        [HttpGet]
        public ActionResult StationCompleteIndex(string search, int? page, string sortOrder = "")
        {
            if (String.IsNullOrEmpty(search))
            {
                ViewBag.FormNameSortParm = sortOrder == "FormName" ? "FormName_desc" : "FormName";
                ViewBag.ProjectNameSortParm = sortOrder == "ProjectName" ? "ProjectName_desc" : "ProjectName";
                ViewBag.locationSortParm = sortOrder == "location" ? "location_desc" : "location";
                ViewBag.InspectorNameSortParm = sortOrder == "InspectorName" ? "InspectorName_desc" : "InspectorName";
                ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
                var StaionSubmissionList = InspectorRepository.sortStationCompleteList((long)User.User_ID, search, sortOrder);
             //  var StaionSubmissionList = InspectorRepository.StationCompleteList((long)User.User_ID);
                if (StaionSubmissionList != null)
                {
                    return View(StaionSubmissionList.ToPagedList(page ?? 1, 10));
                }
            }
            else
            {
                var StaionSubmissionList = InspectorRepository.sortStationCompleteList((long)User.User_ID, search, sortOrder);               
                if (StaionSubmissionList != null)
                {
                    return View(StaionSubmissionList.ToPagedList(page ?? 1, 10));
                }
            }
            return View();
        }

        [HttpGet]
        public JsonResult GetInspectorName(string term)
        {
            return Json(InspectorRepository.SearchByName(term,(long)User.User_ID), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProjectName(string term)
        {
            return Json(InspectorRepository.SearchByProjectName(term,(long)User.User_ID), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProjectNameEdit(string term)
        {
            return Json(InspectorRepository.SearchByProjectNameEdit(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }

        
        [HttpGet]
        public JsonResult GetProjectNameForStationBased(string term)
        {
            return Json(InspectorRepository.SearchByProjectNameForStationBased(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }      

        [HttpGet]
        public JsonResult GetProjectNameStationBased(string term)
        {
            return Json(InspectorRepository.SearchByProjectNameStationBased(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateInspectorStatusList(long[] chkInspector_ID, long[] unchkInspector_ID)
        {
            var chkedInspector_Ids = chkInspector_ID;
            var unchkInspector_IDs = unchkInspector_ID;
            bool chkedResult = true, unkchkedResult = true;

            if (chkedInspector_Ids != null)
            {
                foreach (var inspector in chkedInspector_Ids)
                {
                    chkedResult = InspectorRepository.UpdateInspectorStatus(Convert.ToInt64(inspector));
                }
            }
            if (unchkInspector_IDs != null)
            {
                foreach (var inspector in unchkInspector_IDs)
                {
                    unkchkedResult = InspectorRepository.DeActivateInspectorStatus(Convert.ToInt64(inspector));
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
    }
}
