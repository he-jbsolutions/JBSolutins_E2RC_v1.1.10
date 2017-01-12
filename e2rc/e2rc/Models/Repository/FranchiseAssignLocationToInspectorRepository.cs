using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public class FranchiseAssignLocationToInspectorRepository
    {
        public static List<string> SearchByName(string search, long User_ID)
        {
            var Names = new FranchiseAssignLocationToInspector().AutoList(search, User_ID);

            if (Names != null)
            {
                return Names.ToList();
            }
            return null;
        }
        public static IEnumerable<FranchiseAssignLocationToInspectorModel> List(long User_ID)
        {
            IEnumerable<FranchiseAssignLocationToInspector> AssignList = (IEnumerable<FranchiseAssignLocationToInspector>)new FranchiseAssignLocationToInspector().List((long)User_ID);
            if (AssignList != null)
            {
                List<FranchiseAssignLocationToInspectorModel> PMModelList = new List<FranchiseAssignLocationToInspectorModel>();

                foreach (FranchiseAssignLocationToInspector FranchiseAssignLocationToInspector in AssignList)
                {
                    PMModelList.Add(GetFranchiseAssignLocationToInspectorModel(FranchiseAssignLocationToInspector));
                }
                return PMModelList;
            }
            return null;
        }
        public static IEnumerable<FranchiseAssignLocationToInspectorModel> List(string InspectorName, long User_ID)
        {
            IEnumerable<FranchiseAssignLocationToInspector> FranchiseAssignLocationToInspectorList = new FranchiseAssignLocationToInspector().List(InspectorName, User_ID);

            if (FranchiseAssignLocationToInspectorList != null)
            {
                List<FranchiseAssignLocationToInspectorModel> FranchiseAssignLocationToInspectorModelList = new List<FranchiseAssignLocationToInspectorModel>();

                foreach (FranchiseAssignLocationToInspector FranchiseAssignLocationToInspector in FranchiseAssignLocationToInspectorList)
                {
                    FranchiseAssignLocationToInspectorModelList.Add(GetFranchiseAssignLocationToInspectorModel(FranchiseAssignLocationToInspector));
                }
                return FranchiseAssignLocationToInspectorModelList;
            }
            return null;
        }
        private static FranchiseAssignLocationToInspectorModel GetFranchiseAssignLocationToInspectorModel(FranchiseAssignLocationToInspector FranchiseAssignLocationToInspector)
        {
            return new FranchiseAssignLocationToInspectorModel
            {
                Assign_ID = FranchiseAssignLocationToInspector.Assign_ID,
                Date = FranchiseAssignLocationToInspector.Date,
                User_ID = FranchiseAssignLocationToInspector.User_ID,
                Inspector_ID = FranchiseAssignLocationToInspector.Inspector_ID,
                sInspector_ID =  FranchiseAssignLocationToInspector.lstInspector_ID !=null ? string.Join(",", FranchiseAssignLocationToInspector.lstInspector_ID) : string.Empty,
                InspectorName = FranchiseAssignLocationToInspector.InspectorName,
                sLocation_ID = FranchiseAssignLocationToInspector.lstInspector_ID != null ? string.Join(",", FranchiseAssignLocationToInspector.lstInspector_ID) : string.Empty,
                LocationName = FranchiseAssignLocationToInspector.LocationName,
            };
        }
        public static bool Create(FranchiseAssignLocationToInspectorModel FranchiseAssignLocationToInspectorModel)
        {
            FranchiseAssignLocationToInspector FranchiseAssignLocationToInspector = GetFranchiseAssignLocationToInspector(FranchiseAssignLocationToInspectorModel);
            return FranchiseAssignLocationToInspector.Create();
            //return true;
        }

        public static bool RemoveProjectAccess(FranchiseAssignLocationToInspectorModel FranchiseAssignLocationToInspectorModel)
        {
            FranchiseAssignLocationToInspector FranchiseAssignLocationToInspector = GetFranchiseAssignLocationToInspector(FranchiseAssignLocationToInspectorModel);
            return FranchiseAssignLocationToInspector.RemoveProjectAccess();
            //return true;
        }

        private static FranchiseAssignLocationToInspector GetFranchiseAssignLocationToInspector(FranchiseAssignLocationToInspectorModel FranchiseAssignLocationToInspectorModel)
        {
            return new FranchiseAssignLocationToInspector
            {
                Inspector_ID = FranchiseAssignLocationToInspectorModel.Inspector_ID,
                sInspector_ID = FranchiseAssignLocationToInspectorModel.lstInspector_ID != null ? string.Join(",", FranchiseAssignLocationToInspectorModel.lstInspector_ID) : string.Empty,
                Assign_ID = FranchiseAssignLocationToInspectorModel.Assign_ID,
                Location_ID = FranchiseAssignLocationToInspectorModel.Location_ID,
                sLocation_ID = FranchiseAssignLocationToInspectorModel.lstLocation_ID != null ? string.Join(",", FranchiseAssignLocationToInspectorModel.lstLocation_ID) : string.Empty,
                LocationName = FranchiseAssignLocationToInspectorModel.LocationName,
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID

            };
        }
        public static FranchiseAssignLocationToInspectorModel Single(long? Assign_ID, long User_ID)
        {
            FranchiseAssignLocationToInspector FranchiseAssignLocationToInspector = new FranchiseAssignLocationToInspector().Single((long)Assign_ID, User_ID);
            return GetFranchiseAssignLocationToInspectorModel(FranchiseAssignLocationToInspector);
        }
        public static bool Edit(FranchiseAssignLocationToInspectorModel FranchiseAssignLocationToInspectorModel)
        {
            FranchiseAssignLocationToInspector FranchiseAssignLocationToInspector = GetFranchiseAssignLocationToInspector(FranchiseAssignLocationToInspectorModel);
            return FranchiseAssignLocationToInspector.Edit();
        }
        public static bool Delete(FranchiseAssignLocationToInspectorModel FranchiseAssignLocationToInspectorModel)
        {
            return new FranchiseAssignLocationToInspector
            {
                Assign_ID = FranchiseAssignLocationToInspectorModel.Assign_ID,
            }.Delete();
        }
        public static IEnumerable<dynamic> GetInspectorDetails(long User_ID)
        {
            return new FranchiseAssignLocationToInspector().GetInspectorDetails(User_ID);
        }
        public static IEnumerable<dynamic> GetFranchiseLocations(long User_ID)
        {
            return new FranchiseAssignLocationToInspector().GetFranchiseLocations(User_ID);
        }
    }
}