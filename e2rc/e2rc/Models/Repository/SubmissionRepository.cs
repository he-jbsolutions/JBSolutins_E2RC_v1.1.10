using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public class SubmissionRepository
    {
        public static IEnumerable<SubmissionModel> List(long User_ID)
        {
            IEnumerable<Submission> SubmissionList = new Submission().List(User_ID);
            if (SubmissionList != null)
            {
                List<SubmissionModel> SubmissionModelList = new List<SubmissionModel>();

                foreach (Submission submission in SubmissionList)
                {
                    SubmissionModelList.Add(GetSubmissionModel(submission));
                }
                return SubmissionModelList;
            }
            return null;
        }

        public static IEnumerable<SubmissionModel> List(string search, long User_ID)
        {
            IEnumerable<Submission> SubmissionList = new Submission().List(search, User_ID);
            if (SubmissionList != null)
            {
                List<SubmissionModel> SubmissionModelList = new List<SubmissionModel>();

                foreach (Submission submission in SubmissionList)
                {
                    SubmissionModelList.Add(GetSubmissionModel(submission));
                }
                return SubmissionModelList;
            }
            return null;
        }

        private static SubmissionModel GetSubmissionModel(Submission submission)
        {
            return new SubmissionModel
              {
                  FormName=submission.FormName,
                  ClientName=submission.ClientName,
                  CompanyName=submission.CompanyName,
                  InspectorName=submission.InspectorName,
                  ProjectName=submission.ProjectName,
                  location=submission.location,
                  Date=submission.Date,
                  Inspection_ID = submission.Inspection_ID,
                  IsComplete=submission.IsComplete,
                  path=submission.path 
              };
        }      

        public static List<string> SearchByName(string search)
        {
            var ProjectNames = new Submission().AutoList(search);

            if (ProjectNames != null)
            {
                return ProjectNames.ToList();
            }
            return null;
        }

        public static IEnumerable<SubmissionModel> sortSubmissionList(long User_ID, string Search, string sortOrder)
        {
            IEnumerable<SubmissionModel> SubmissionmodelList;
            if (!string.IsNullOrEmpty(Search ))
            {
                SubmissionmodelList = List(Search, User_ID);
            }
            else
            {
                SubmissionmodelList = List(User_ID);
            }
            if (SubmissionmodelList != null)
            {
                switch (sortOrder)
                {                            
                    case "FormName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.FormName);
                        break;
                    case "FormName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.FormName);
                        break;
                    case "CompanyName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.CompanyName);
                        break;
                    case "CompanyName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.CompanyName);
                        break;
                    case "ProjectName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.ProjectName);
                        break;
                    case "ProjectName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.ProjectName);
                        break;
                    case "location_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.location);
                        break;
                    case "location":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.location);
                        break;
                    case "InspectorName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.InspectorName);
                        break;
                    case "InspectorName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.InspectorName);
                        break;
                    case "Date":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.Date);
                        break;
                }
                return SubmissionmodelList;
            }

            else
            {
                return null;
            }
        }


        public static IEnumerable<SubmissionModel> DisplayProjectWiseInspection(long Location_ID,long Client_ID,long User_ID,string Role)
        {
            IEnumerable<Submission> SubmissionList = new Submission().DisplayProjectWiseInspection(Location_ID, Client_ID,User_ID,Role);
            if (SubmissionList != null)
            {
                List<SubmissionModel> SubmissionModelList = new List<SubmissionModel>();

                foreach (Submission submission in SubmissionList)
                {
                    SubmissionModelList.Add(GetDisplayProjectWiseInpection(submission));
                }
                return SubmissionModelList;
            }
            return null;
        }

        private static SubmissionModel GetDisplayProjectWiseInpection(Submission submission)
        {
            return new SubmissionModel
            {
                FormName = submission.FormName,
                CompanyName = submission.CompanyName,
                InspectorName = submission.InspectorName,
                ProjectName = submission.ProjectName,
                location = submission.location,
                Date = submission.Date,
                Inspection_ID = submission.Inspection_ID,
                IsComplete = submission.IsComplete,
                path = submission.path,
                WorkOrder=submission.WorkOrder,
                Pastduedays = submission.Pastdays,
                ModifiedDate=submission.ModifiedDate
            };
        }

        //public static IEnumerable<SubmissionModel> sortDisplayProjectWiseInspectionList(long Location_ID,string sortOrder)
        //{
        //    IEnumerable<SubmissionModel> DisplayProjectWiseInspectionlList;
        //    DisplayProjectWiseInspectionlList = DisplayProjectWiseInspection(Location_ID);

        //    if (DisplayProjectWiseInspectionlList != null)
        //    {
        //        switch (sortOrder)
        //        {
        //            case "FormName_desc":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderByDescending(m => m.FormName);
        //                break;
        //            case "FormName":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderBy(m => m.FormName);
        //                break;
        //            case "ClientName_desc":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderByDescending(m => m.ClientName);
        //                break;
        //            case "ClientName":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderBy(m => m.ClientName);
        //                break;
        //            case "ProjectName_desc":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderByDescending(m => m.ProjectName);
        //                break;
        //            case "ProjectName":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderBy(m => m.ProjectName);
        //                break;
        //            case "location_desc":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderByDescending(m => m.location);
        //                break;
        //            case "location":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderBy(m => m.location);
        //                break;
        //            case "InspectorName_desc":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderByDescending(m => m.InspectorName);
        //                break;
        //            case "InspectorName":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderBy(m => m.InspectorName);
        //                break;
        //            case "Date":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderBy(m => m.Date);
        //                break;
        //            case "Date_desc":
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderByDescending(m => m.Date);
        //                break;
        //            default:
        //                DisplayProjectWiseInspectionlList = DisplayProjectWiseInspectionlList.OrderBy(m => m.Date);
        //                break;
        //        }
        //        return DisplayProjectWiseInspectionlList;
        //    }

        //    else
        //    {
        //        return null;
        //    }
        //}



        public static bool getReviewerAllowWorkOrder(long User_ID)
        {
            return new Submission().getReviewerAllowWorkOrder(User_ID);
        }

    }
}