using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public class FranchiseAssignLocationToClientRepository
    {

        public static List<string> SearchByName(string search, long User_ID)
        {
            var Names = new FranchiseAssignLocationToClient().AutoList(search, User_ID);

            if (Names != null)
            {
                return Names.ToList();
            }
            return null;
        }
        public static IEnumerable<FranchiseAssignLocationToClientModel> List(long User_ID)
        {
            IEnumerable<FranchiseAssignLocationToClient> AssignList = (IEnumerable<FranchiseAssignLocationToClient>)new FranchiseAssignLocationToClient().List((long)User_ID);
            if (AssignList != null)
            {
                List<FranchiseAssignLocationToClientModel> PMModelList = new List<FranchiseAssignLocationToClientModel>();

                foreach (FranchiseAssignLocationToClient FranchiseAssignLocationToClient in AssignList)
                {
                    PMModelList.Add(GetFranchiseAssignLocationToClientModel(FranchiseAssignLocationToClient));
                }
                return PMModelList;
            }
            return null;
        }
        public static IEnumerable<FranchiseAssignLocationToClientModel> List(string InspectorName, long User_ID)
        {
            IEnumerable<FranchiseAssignLocationToClient> FranchiseAssignLocationToClientList = new FranchiseAssignLocationToClient().List(InspectorName, User_ID);

            if (FranchiseAssignLocationToClientList != null)
            {
                List<FranchiseAssignLocationToClientModel> FranchiseAssignLocationToClientModelList = new List<FranchiseAssignLocationToClientModel>();

                foreach (FranchiseAssignLocationToClient FranchiseAssignLocationToClient in FranchiseAssignLocationToClientList)
                {
                    FranchiseAssignLocationToClientModelList.Add(GetFranchiseAssignLocationToClientModel(FranchiseAssignLocationToClient));
                }
                return FranchiseAssignLocationToClientModelList;
            }
            return null;
        }
        private static FranchiseAssignLocationToClientModel GetFranchiseAssignLocationToClientModel(FranchiseAssignLocationToClient FranchiseAssignLocationToClient)
        {
            return new FranchiseAssignLocationToClientModel
            {
                Assign_ID = FranchiseAssignLocationToClient.Assign_ID,
                Reviewer_ID = FranchiseAssignLocationToClient.Reviewer_ID,
                ReviewerName = FranchiseAssignLocationToClient.ReviewerName,
                Date = FranchiseAssignLocationToClient.Date,
                User_ID = FranchiseAssignLocationToClient.User_ID,
                Client_ID = FranchiseAssignLocationToClient.Client_ID,
                CompanyName = FranchiseAssignLocationToClient.CompanyName,
                Location_ID = FranchiseAssignLocationToClient.Location_ID,
                LocationName = FranchiseAssignLocationToClient.LocationName,
            };
        }
        public static bool Create(FranchiseAssignLocationToClientModel FranchiseAssignLocationToClientModel)
        {
            FranchiseAssignLocationToClient FranchiseAssignLocationToClient = GetFranchiseAssignLocationToClient(FranchiseAssignLocationToClientModel);
            return FranchiseAssignLocationToClient.Create();
            //return true; 
        }

        public static bool RemoveProjectAccess(FranchiseAssignLocationToClientModel FranchiseAssignLocationToClientModel)
        {
            FranchiseAssignLocationToClient FranchiseAssignLocationToClient = GetFranchiseAssignLocationToClient(FranchiseAssignLocationToClientModel);
            return FranchiseAssignLocationToClient.RemoveProjectAccess();
            //return true;
        }
        private static FranchiseAssignLocationToClient GetFranchiseAssignLocationToClient(FranchiseAssignLocationToClientModel FranchiseAssignLocationToClientModel)
        {
            return new FranchiseAssignLocationToClient
            {
                Reviewer_ID=FranchiseAssignLocationToClientModel.Reviewer_ID,
                sReviewer_ID = FranchiseAssignLocationToClientModel.lstReviewer_ID != null ? string.Join(",", FranchiseAssignLocationToClientModel.lstReviewer_ID) : string.Empty,
                Client_ID = FranchiseAssignLocationToClientModel.Client_ID,
                Assign_ID = FranchiseAssignLocationToClientModel.Assign_ID,
                Location_ID = FranchiseAssignLocationToClientModel.Location_ID,
                sLocation_ID = FranchiseAssignLocationToClientModel.lstLocation_ID != null ? string.Join(",", FranchiseAssignLocationToClientModel.lstLocation_ID) : string.Empty,
                LocationName = FranchiseAssignLocationToClientModel.LocationName,
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID

            };
        }
        public static FranchiseAssignLocationToClientModel Single(long? Assign_ID, long User_ID)
        {
            FranchiseAssignLocationToClient FranchiseAssignLocationToClient = new FranchiseAssignLocationToClient().Single((long)Assign_ID, User_ID);
            return GetFranchiseAssignLocationToClientModel(FranchiseAssignLocationToClient);
        }
        public static bool Edit(FranchiseAssignLocationToClientModel FranchiseAssignLocationToClientModel)
        {
            FranchiseAssignLocationToClient FranchiseAssignLocationToClient = GetFranchiseAssignLocationToClient(FranchiseAssignLocationToClientModel);
            return FranchiseAssignLocationToClient.Edit();
        }
        public static bool Delete(FranchiseAssignLocationToClientModel FranchiseAssignLocationToClientModel)
        {
            return new FranchiseAssignLocationToClient
            {
                Assign_ID = FranchiseAssignLocationToClientModel.Assign_ID,
            }.Delete();
        }




        public static IEnumerable<dynamic> GetReviewerDetails(long User_ID)
        {
            return new FranchiseAssignLocationToClient().GetReviewerDetails(User_ID);
        }

        public static IEnumerable<dynamic> GetReviewerClients(long User_ID, long Reviewer_ID)
        {
            return new FranchiseAssignLocationToClient().GetReviewerClients(User_ID, Reviewer_ID);
        }

        public static IEnumerable<dynamic> GetReviewerClientsLocation(long User_ID,long Client_ID)
        {
            return new FranchiseAssignLocationToClient().GetReviewerClientsLocation(User_ID, Client_ID);
        }

    }
}