using System;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using e2rc.Models.Security;
using PagedList;

namespace e2rc.Controllers
{
    [CustomAuthorizeAttribute(Roles = "Franchise Admin,Super Admin")]
    public class LocationController : BaseController
    {
        [HttpGet]
        public ActionResult Index(string search, string view, int? page, Int64 user_id=0)
        {
           
            if (User.Role == "Super Admin")
            {
                if (String.IsNullOrEmpty(search))
                {
                    var locationlist = LocationRepository.List(user_id, view);
                    if (locationlist != null)
                    {
                        return View(locationlist.ToPagedList(page ?? 1, 10));
                    }
                }
                else
                {
                    var locationlist = LocationRepository.List(search,user_id, view);
                    if (locationlist != null)
                    {
                        return View(locationlist.ToPagedList(page ?? 1, 10));
                    }
                }
            }
            else
            {
                if (String.IsNullOrEmpty(search))
                {
                    var locationlist = LocationRepository.List((long)User.User_ID, view);
                    if (locationlist != null)
                    {
                        return View(locationlist.ToPagedList(page ?? 1, 10));
                    }
                }
                else
                {
                    var locationlist = LocationRepository.List(search, (long)User.User_ID, view);
                    if (locationlist != null)
                    {
                        return View(locationlist.ToPagedList(page ?? 1, 10));
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create(string str)
        {
            ViewBag.view = str;
            return View();
        }

        [HttpPost]
        public ActionResult Create(LocationModel locationmodel)
        {
            //  if (ModelState.IsValid)
            // {
            if (Convert.ToString(Request["hfStationIDs"]) != "")
            {

                locationmodel.F2_ID = Convert.ToString(Request["hfStationIDs"]);
            }
            else
            {
                locationmodel.F2_ID = "";
            }
            locationmodel.User_ID = User.User_ID;
            LocationRepository.Create(locationmodel);
            return RedirectToAction("Index");
            // }
            // return View(locationmodel);
        }

        [HttpGet]
        public ActionResult Edit(long? Location_ID)
        {
            return View(LocationRepository.Single((long)Location_ID, (long)User.User_ID));
        }

        [HttpPost]
        public ActionResult Edit(LocationModel locationModel)
        {          
            // if (ModelState.IsValid)
            //{ 
            //ViewBag.F1ID = locationModel.F1_ID;
            //ViewBag.F2ID = locationModel.F2_ID;
            locationModel.User_ID = User.User_ID;
            ViewBag.IsLocationEdited = LocationRepository.Edit(locationModel);
            ViewBag.LocationName = locationModel.Name; 
            // }
            return View(locationModel);
        }

        [HttpGet]
        public ActionResult Details(long? Location_ID,long user_id=0)
        {
            if (User.Role == "Super Admin")
            {
                return View(LocationRepository.Single((long)Location_ID,user_id));
            }
            else
            {
                return View(LocationRepository.Single((long)Location_ID, (long)User.User_ID));
            }
        }

        [HttpPost]
        public bool Delete(long? Location_ID)
        {
            return LocationRepository.Delete(
                new LocationModel
                {
                    Location_ID = Location_ID,
                    CreatedBy_ID = User.User_ID
                });
        }

        [HttpGet]
        public JsonResult GetLocationName(string term)
        {
            return Json(LocationRepository.SearchByName(term, (long)User.User_ID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsTrackingNumberAvailable(string TrackingNumber, long? Location_ID = 0)
        {
            return Json(LocationRepository.IsTrackingNumberAvailable(TrackingNumber, Location_ID), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetClientDetails()
        {
            return Json(LocationRepository.GetClientDetails((long)User.User_ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateLocationStatusList(long[] chkedLocation_ID, long[] unchkLocation_ID)
        {
            var chkedLocation_IDs = chkedLocation_ID;
            var unchkLocation_IDs = unchkLocation_ID;
            bool chkedResult = true, unkchkedResult = true;

            if (chkedLocation_IDs != null)
            {
                string view = "Active";
                foreach (var location in chkedLocation_IDs)
                {
                    chkedResult = LocationRepository.UpdateLocationStatus(Convert.ToInt64(location), view);
                }
            }
            if (unchkLocation_IDs != null)
            {
                string view = "InActive";
                foreach (var location in unchkLocation_IDs)
                {
                    unkchkedResult = LocationRepository.UpdateLocationStatus(Convert.ToInt64(location), view);
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
        public ActionResult DisplayClientwiseProject(long Client_ID, int? page,long user_id=0)
        {
            var locationlist = LocationRepository.DisplayClientWiseProject(Client_ID);
            if (locationlist != null)
            {
                return View(locationlist.ToPagedList(page ?? 1, 10));
            }
            return View();
        }

    }
}
