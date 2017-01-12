using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;
using System.Data;
using System;

namespace e2rc.Models.Repository
{
    public class ReviewerRepository
    {

        public static IEnumerable<ReviewerModel> List(string ReviewerName, long User_ID, string view)
        {
            IEnumerable<Reviewer> ReviewerList = new Reviewer().List(ReviewerName, User_ID, view);

            if (ReviewerList != null)
            {
                List<ReviewerModel> ReviewerModelList = new List<ReviewerModel>();

                foreach (Reviewer Reviewer in ReviewerList)
                {
                    ReviewerModelList.Add(GetReviewerModel(Reviewer));
                }
                return ReviewerModelList;
            }
            return null;
        }


        public static IEnumerable<ReviewerModel> List(long User_ID, string view)
        {
            IEnumerable<Reviewer> ProjectManagerList = (IEnumerable<Reviewer>)new Reviewer().List((long)User_ID, view);
            if (ProjectManagerList != null)
            {
                List<ReviewerModel> reviewerModelList = new List<ReviewerModel>();

                foreach (Reviewer Reviewer in ProjectManagerList)
                {
                    reviewerModelList.Add(GetReviewerModel(Reviewer));
                }
                return reviewerModelList;
            }
            return null;
        }

        private static ReviewerModel GetReviewerModel(Reviewer Reviewer)
        {
            ReviewerModel reviewermodel = null;
            reviewermodel = new ReviewerModel
            {
                Reviewer_ID = Reviewer.Reviewer_ID,
                ReviewerTitle=Reviewer.ReviewerTitle,
                IsActive = Reviewer.IsActive,
                IsAllowToCloseWorkOrder=Reviewer.IsAllowToCloseWorkOrder,
                //Inspector_ID = ProjectManager.Inspector_ID,  
                slstLocationID = Reviewer.sLocationID,
                FirstName = Reviewer.FirstName,
                LastName = Reviewer.LastName,
                Date = Reviewer.Date,
                UserName = Reviewer.UserName,
                Password = Reviewer.Password,
                ConfirmPassword = Reviewer.Password,
                MobileNumber = Reviewer.MobileNumber,
                PhoneNumber = Reviewer.PhoneNumber,
                Qualification = Reviewer.Qualification,
                Email = Reviewer.Email,
                User_ID = Reviewer.User_ID,
                Role = new RoleModel
                {
                    Role_ID = Reviewer.Role.Role_ID,
                    Description = Reviewer.Role.Description
                },
                Address = new AddressModel
                {
                    City = Reviewer.Address.City,
                    MailingAddress = Reviewer.Address.MailingAddress,
                    State = new StateModel
                    {
                        State_ID = Reviewer.Address.State.State_ID,
                        Code = Reviewer.Address.State.Code,
                        Name = Reviewer.Address.State.Name
                    },
                    ZipCode = Reviewer.Address.ZipCode
                },
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID,
                Client_IDs=Reviewer.ClientIds
                //Location_ID = ProjectManager.Location_ID
            };

            return reviewermodel;
        }
        public static bool Create(ReviewerModel reviewerModel)
        {
            Reviewer Reviewer = GetReviewer(reviewerModel);
            //return ProjectManager.Create(); 
            long Reviewer_ID = reviewerModel.Reviewer_ID;
            bool result = Reviewer.Create();
            bool success = false;
            if (result == true)
            {
                if (reviewerModel.selectedClientIDs != null)
                {
                    foreach (string Client_ID in reviewerModel.selectedClientIDs)
                    {
                        success = Reviewer.CreateReviewerClients(Reviewer_ID, Convert.ToInt64(Client_ID));
                    }
                }
                
            }
            

            //if (result == true)
            //{
            //    DataTable dataTable = new DataTable();
            //    dataTable.Columns.Add(new DataColumn("ProjectManager_ID", System.Type.GetType("System.Int64")));
            //    dataTable.Columns.Add(new DataColumn("Location_ID", System.Type.GetType("System.Int64")));

            //    foreach (string Location_ID in  ProjectManagerModel.selectedLocationIDs)
            //    {
            //        DataRow dataRow = dataTable.NewRow();
            //        dataRow["ProjectManager_ID"] = ProjectManager_ID;
            //        dataRow["Location_ID"] =Convert.ToInt64(Location_ID);
            //        dataTable.Rows.Add(dataRow);                 
            //    }
            //    try
            //    {
            //       success= new e2rcModel.DataAccessLayer.DAL().Insert("sp_ProjectManagerLocation_CRUD",
            //            new object[] { "@Action", "@ProjectManager_ID", "@Locations" },
            //            new object[] { "INSERT", ProjectManager_ID, dataTable });
            //    }
            //    catch (System.Exception ex)
            //    {
            //        throw ex;
            //    }
            //}

            if (success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool Edit(ReviewerModel ReviewerModel)
        {
            Reviewer Reviewer = GetReviewer(ReviewerModel);
            Reviewer.selectedClientIDs = ReviewerModel.Client_IDs != null ? string.Join(", ", ReviewerModel.Client_IDs) : string.Empty;
            long? Reviewer_ID = ReviewerModel.Reviewer_ID;
            bool result = Reviewer.Edit();
            bool success = false;
            if (result == true)
            {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("Reviewer_ID", System.Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("Client_ID", System.Type.GetType("System.Int64")));

                    if (ReviewerModel.Client_IDs != null)
                    {
                        foreach (long Client_ID in ReviewerModel.Client_IDs)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["Reviewer_ID"] = Reviewer_ID;
                            dataRow["Client_ID"] = Client_ID;
                            dataTable.Rows.Add(dataRow);
                            // success = ProjectManager.EditPMLocation(ProjectManager_ID, Location_ID);
                        }
                    }
                    try
                    {
                        success = new e2rcModel.DataAccessLayer.DAL().Insert("sp_ReviewerClient_CRUD",
                              new object[] { "@Action", "@Reviewer_ID", "@Clients"},
                              new object[] { "UPDATE", Reviewer_ID, dataTable });
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
            }
            if (success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private static Reviewer GetReviewer(ReviewerModel ReviewerModel)
        {
            return new Reviewer
            {
                //  Inspector_ID = ProjectManagerModel.inspector.Inspector_ID,

                Reviewer_ID = ReviewerModel.Reviewer_ID,
                ReviewerTitle=ReviewerModel.ReviewerTitle,
                IsActive = ReviewerModel.IsActive,
                selectedClientIDs = ReviewerModel.selectedClientIDs !=null ? string.Join(", ", ReviewerModel.selectedClientIDs) : string.Empty,
                sLocationID = ReviewerModel.lstLocation_ID != null ? string.Join(",", ReviewerModel.lstLocation_ID) : string.Empty,
                IsAllowToCloseWorkOrder=ReviewerModel.IsAllowToCloseWorkOrder,
                FirstName = ReviewerModel.FirstName,
                LastName = ReviewerModel.LastName,
                Date = ReviewerModel.Date,
                UserName = ReviewerModel.UserName,
                Password = ReviewerModel.Password,
                MobileNumber = ReviewerModel.MobileNumber,
                PhoneNumber = ReviewerModel.PhoneNumber,
                Qualification = ReviewerModel.Qualification,
                Email = ReviewerModel.Email,
                Role = new Role
                {
                    Role_ID = ReviewerModel.Role.Role_ID,
                    Description = ReviewerModel.Role.Description
                },
                Address = new Address
                {
                    City = ReviewerModel.Address.City,
                    MailingAddress = ReviewerModel.Address.MailingAddress,
                    State = new State
                    {
                        State_ID = ReviewerModel.Address.State.State_ID,
                        Code = ReviewerModel.Address.State.Code,
                        Name = ReviewerModel.Address.State.Name
                    },
                    ZipCode = ReviewerModel.Address.ZipCode
                },
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID
            };
        }

        //public static IEnumerable<dynamic> GetClientWiseProject(string Client_ID)
        //{ 
        //    return new Reviewer().GetClientDetails(Client_ID);
        //}

        public static IEnumerable<dynamic> GetReviewerClientDetails(long User_ID)
        {
            return new Reviewer().GetReviewerClientDetails(User_ID);
        }

        public static IEnumerable<ReviewerModel> sortReviewerDetails(long User_ID, string Search, string sortOrder, string view)
        {
            IEnumerable<ReviewerModel> ReviewerList;
            if (!string.IsNullOrEmpty(Search))
            {
                ReviewerList = List(Search, User_ID, view);
            }
            else
            {
                ReviewerList = List(User_ID, view);
            }
            if (ReviewerList != null)
            {
                switch (sortOrder)
                {

                    case "Name_desc":
                        ReviewerList = ReviewerList.OrderByDescending(m => m.Name);
                        break;
                    case "Name":
                        ReviewerList = ReviewerList.OrderBy(m => m.Name);
                        break;
                    case "UserName_desc":
                        ReviewerList = ReviewerList.OrderByDescending(m => m.UserName);
                        break;
                    case "UserName":
                        ReviewerList = ReviewerList.OrderBy(m => m.UserName);
                        break;
                    case "Email":
                        ReviewerList = ReviewerList.OrderBy(m => m.Email);
                        break;
                    case "Email_desc":
                        ReviewerList = ReviewerList.OrderByDescending(m => m.Email);
                        break;
                    case "Date":
                        ReviewerList = ReviewerList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        ReviewerList = ReviewerList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        ReviewerList = ReviewerList.OrderByDescending(m => m.Date);
                        break;
                }
                return ReviewerList;
            }
            else
            {
                return null;
            }
        }

        public static ReviewerModel Single(long? Reviewer_ID, long User_ID)
        {
            Reviewer Reviewer = new Reviewer().Single((long)Reviewer_ID, User_ID);
            ReviewerModel pm = GetReviewerModel(Reviewer);
            pm.slstLocationID = Reviewer.sLocationID;
            Reviewer.ClientIds = Reviewer.ClientIds;
            //foreach (var item in ProjectManager.locations)
            //{
            //    //pm.location.location_id = item["location_id"];
            //    //pm.location.name = item["name"];

            //}
            return pm;
        }


        public static long? get_Reviewer_ID(long? User_ID)
        {
            return Reviewer.get_Reviewer_ID(User_ID);
        }
        public static long getReviewer_UserID(long User_ID)
        {
            return (long)Reviewer.getReviewer_UserID(User_ID);
        }

        internal static bool UpdateReviewerStatus(long Reviewer_ID, string view)
        {
            return new Reviewer().UpdateReviewerStatus(Reviewer_ID, view);
        }


        public static List<string> SearchByName(string search, long User_ID, string view)
        {
            var reviewerNames = new Reviewer().AutoList(search, User_ID,view);

            if (reviewerNames != null)
            {
                return reviewerNames.ToList();
            }
            return null;
        }
    }
}