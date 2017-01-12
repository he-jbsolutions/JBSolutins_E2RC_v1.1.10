using System;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using PagedList;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using e2rcModel.Common;

namespace e2rc.Controllers
{
    [Authorize]
    public class ReviewerController : BaseController
    {
        //
        // GET: /Reviewer/

        [HttpGet]
        public ActionResult Index(string search, string view, string sortOrder, int? page)
        {
            ViewBag.NameSortParm = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewBag.UserNameSortParm = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            var ReviewerList = ReviewerRepository.sortReviewerDetails((long)User.User_ID, search, sortOrder, view);

            if (ReviewerList != null)
            {
                return View(ReviewerList.ToPagedList(page ?? 1, 10));
            }

            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View(new ReviewerModel() { Address = new AddressModel(), Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(ReviewerModel reviewerModel)
        {
            if (Request.Form["Client_ID"] != null)
            {
                reviewerModel.selectedClientIDs = Request.Form["Client_ID"].Split(',');
            }
            reviewerModel.User_ID = User.User_ID;
            bool result = ReviewerRepository.Create(reviewerModel);

            /* Project Assign to Reviewer Mail Notification
            reviewerModel.slstLocationID = reviewerModel.lstLocation_ID != null ? string.Join(",", reviewerModel.lstLocation_ID) : string.Empty;

            string Reviewername = reviewerModel.FirstName + " " + reviewerModel.LastName;
            if (result)
            {
                MailSetting.SendMail(Actions.INSERT.ToString(), 0, reviewerModel.slstLocationID, "Reviewer", Reviewername, reviewerModel.Email);
            }*/
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(long Reviewer_ID)
        {
            ReviewerModel reviewer;
            if (User.Role == "Franchise Admin")
            {
                reviewer = ReviewerRepository.Single(Reviewer_ID, (long)User.User_ID);
            }
            else if (User.Role == "Reviewer")
            {
                long User_ID = ReviewerRepository.getReviewer_UserID((long)User.User_ID);
                reviewer = ReviewerRepository.Single(Reviewer_ID, User_ID);
            }
            else
            {
                reviewer = ReviewerRepository.Single(Reviewer_ID, (long)User.User_ID);
            }
            if (reviewer.Client_IDs != null)
            {
                foreach (long client_id in reviewer.Client_IDs)
                {
                    try
                    {
                        reviewer.hfSelectedClients += "," + client_id;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (!string.IsNullOrEmpty(reviewer.hfSelectedClients))
            {
                reviewer.hfSelectedClients = reviewer.hfSelectedClients.Remove(0, 1);
            }

            return View(reviewer);
        }

        [HttpPost]
        public ActionResult Edit(ReviewerModel ReviewerModel)
        {
            ReviewerModel.CreatedBy_ID = User.User_ID;
            //ViewBag.IsReviewerEdited = ReviewerRepository.Edit(ReviewerModel);
            //ViewBag.Reviewername = ReviewerModel.FirstName + ' ' + ReviewerModel.LastName;

            /* Send Mail Notification for Assign Project and Reviewer 
            ReviewerModel.slstLocationID = ReviewerModel.lstLocation_ID != null ? string.Join(",", ReviewerModel.lstLocation_ID) : string.Empty;
            TempData["Reviewername"] = ReviewerModel.FirstName + ' ' + ReviewerModel.LastName;
            MailSetting.SendMail(Actions.UPDATE.ToString(),ReviewerModel.Reviewer_ID, ReviewerModel.slstLocationID, "Reviewer", TempData["Reviewername"].ToString(), ReviewerModel.Email);*/

 
            TempData["IsReviewerEdited"] = ReviewerRepository.Edit(ReviewerModel);
            TempData["Reviewername"] = ReviewerModel.FirstName + ' ' + ReviewerModel.LastName;

            ReviewerModel.hfSelectedClients = string.Empty;
            if (ReviewerModel.Client_IDs != null)
            {
                foreach (long ClientIds in ReviewerModel.Client_IDs)
                {
                    try
                    {
                        ReviewerModel.hfSelectedClients += "," + ClientIds;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (!string.IsNullOrEmpty(ReviewerModel.hfSelectedClients))
            {
                ReviewerModel.hfSelectedClients = ReviewerModel.hfSelectedClients.Remove(0, 1);
            }

            //return View(ReviewerModel);
            return RedirectToAction("Edit", new { Reviewer_ID = ReviewerModel.Reviewer_ID });
        }

        [HttpGet]
        public ActionResult Edit(long? Reviewer_ID)
        {
            ReviewerModel re = ReviewerRepository.Single(Reviewer_ID, (long)User.User_ID);
            if (re.Client_IDs != null)
            {
                foreach (long clientids in re.Client_IDs)
                {
                    try
                    {
                        re.hfSelectedClients += "," + clientids;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (!string.IsNullOrEmpty(re.hfSelectedClients))
            {
                re.hfSelectedClients = re.hfSelectedClients.Remove(0, 1);
            }
            return View(re);
        }

        [HttpPost]
        public JsonResult UpdateReviewerStatusList(long[] chkedReviewer_ID, long[] unchkReviewer_ID)
        {
            var chkedPM_Ids = chkedReviewer_ID;
            var unchkPM_IDs = unchkReviewer_ID;
            bool chkedResult = true, unkchkedResult = true;

            if (chkedPM_Ids != null)
            {
                string view = "Active";
                ViewBag.isactive = view;
                foreach (var pm in chkedPM_Ids)
                {
                    chkedResult = ReviewerRepository.UpdateReviewerStatus(Convert.ToInt64(pm), view);
                }
            }
            if (unchkPM_IDs != null)
            {
                string view = "InActive";
                ViewBag.isactive = view;
                foreach (var pm in unchkPM_IDs)
                {
                    unkchkedResult = ReviewerRepository.UpdateReviewerStatus(Convert.ToInt64(pm), view);
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
        //public JsonResult ClientWiseProject(long[] Client_IDs)
        //{
        //    ReviewerModel rw = new ReviewerModel();
        //    string client_ids=string.Empty;
        //    if (Client_IDs != null)
        //    {
               
        //        foreach (long ClientId in Client_IDs)
        //        {
        //            try
        //            {
        //                client_ids += "," + ClientId;
        //            }
        //            catch (Exception)
        //            {
        //            }
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(client_ids))
        //    {
        //        client_ids = client_ids.Remove(0, 1);
        //    }
        //    return Json(ReviewerRepository.GetClientWiseProject(client_ids));
           
        //}

        [HttpGet]
        public JsonResult GetReviewerName(string term, string view)
        {
            return Json(ReviewerRepository.SearchByName(term, (long)User.User_ID, view), JsonRequestBehavior.AllowGet);
           
        }
    }
}
