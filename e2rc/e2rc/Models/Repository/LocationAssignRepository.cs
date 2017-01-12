using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public static class LocationAssignRepository
    {

        public static List<string> SearchByName(string search, long User_ID)
        {
            var Names = new LocationAssign().AutoList(search, User_ID);

            if (Names != null)
            {
                return Names.ToList();
            }
            return null;
        }
        public static IEnumerable<LocationAssignModel> inspectors
        {
            get
            {
                List<LocationAssignModel> inspectors = new List<LocationAssignModel>();
                foreach (var inspector in new LocationAssign().Inspectors)
                {
                    inspectors.Add(new LocationAssignModel { Inspector_ID = inspector.Inspector_ID, Name = inspector.Name });
                }
                return inspectors;
            }
        }
        public static IEnumerable<LocationAssignModel> Locations
        {
            get
            {
                List<LocationAssignModel> locations = new List<LocationAssignModel>();
                foreach (var location in new LocationAssign().Locations)
                {
                    locations.Add(new LocationAssignModel { Location_ID = location.Location_ID, Name = location.Name });
                }
                return locations;
            }
        }
        public static IEnumerable<dynamic> PMLocations(long User_ID)
        {
            return new LocationAssign().PMLocations(User_ID);
        }
        public static IEnumerable<LocationAssignModel> List(long User_ID)
        {
            IEnumerable<LocationAssign> AssignList = (IEnumerable<LocationAssign>)new LocationAssign().List((long)User_ID);
            if (AssignList != null)
            {
                List<LocationAssignModel> PMModelList = new List<LocationAssignModel>();

                foreach (LocationAssign LocationAssign in AssignList)
                {
                    PMModelList.Add(GetLocationAssignModel(LocationAssign));
                }
                return PMModelList;
            }
            return null;
        }
        public static IEnumerable<LocationAssignModel> List(string InspectorName, long User_ID)
        {
            IEnumerable<LocationAssign> LocationAssignList = new LocationAssign().List(InspectorName, User_ID);

            if (LocationAssignList != null)
            {
                List<LocationAssignModel> LocationAssignModelList = new List<LocationAssignModel>();

                foreach (LocationAssign LocationAssign in LocationAssignList)
                {
                    LocationAssignModelList.Add(GetLocationAssignModel(LocationAssign));
                }
                return LocationAssignModelList;
            }
            return null;
        }
        private static LocationAssignModel GetLocationAssignModel(LocationAssign LocationAssign)
        {
            return new LocationAssignModel
            {
                Assign_ID = LocationAssign.Assign_ID, 
                Date = LocationAssign.Date,
                User_ID = LocationAssign.User_ID,
                inspector = new InspectorModel
                {
                    Inspector_ID = LocationAssign.Inspector_ID,
                    Name = LocationAssign.InspectorName,

                },
                //Inspector_ID = LocationAssign.Inspector_ID,
               //InspectorName  = LocationAssign.InspectorName,
                location = new LocationModel
                {
                    Location_ID= LocationAssign.Location_ID,
                    Name=LocationAssign.LocationName,
                },
                //Location_ID = LocationAssign.Location_ID,
                //LocationName = LocationAssign.LocationName,
            };
        }
        public static bool Create(LocationAssignModel LocationAssignModel)
        {
            LocationAssign LocationAssign = GetLocationAssign(LocationAssignModel);
            return LocationAssign.Create();
            //return true;
        }
        private static LocationAssign GetLocationAssign(LocationAssignModel LocationAssignModel)
        {
            return new LocationAssign
            {
                Inspector_ID = LocationAssignModel.inspector.Inspector_ID,
                Assign_ID = LocationAssignModel.Assign_ID,
                Location_ID=LocationAssignModel.location.Location_ID,
                LocationName = LocationAssignModel.LocationName,
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID
                
            };
        }
        public static LocationAssignModel Single(long? Assign_ID, long User_ID)
        {
            LocationAssign LocationAssign = new LocationAssign().Single((long)Assign_ID, User_ID);
            return GetLocationAssignModel(LocationAssign);
        }
        public static bool Edit(LocationAssignModel LocationAssignModel)
        {
            LocationAssign LocationAssign = GetLocationAssign(LocationAssignModel);
            return LocationAssign.Edit();
        }
        public static bool Delete(LocationAssignModel LocationAssignModel)
        {
            return new LocationAssign
            {
                Assign_ID = LocationAssignModel.Assign_ID,               
            }.Delete();
        }
        public static IEnumerable<dynamic> GetInspectorDetails(long User_ID)
        {
            return new LocationAssign().GetInspectorDetails(User_ID);
        }

        
    }
}