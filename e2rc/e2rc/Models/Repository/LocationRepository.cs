using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public static class LocationRepository
    {
        public static IEnumerable<LocationModel> Locations
        {
            get
            {
                List<LocationModel> locations = new List<LocationModel>();
                foreach (var location in new Location().Locations)
                {
                    locations.Add(new LocationModel { Location_ID = location.Location_ID, Name = location.Name });
                }
                return locations;
            }
        }

        public static IEnumerable<ProjectTypeModel> ProjectType
        {
            get
            {
                List<ProjectTypeModel> projecttypeList = new List<ProjectTypeModel>();
                foreach (var Projecttype in new Location().Projecttypes)
                {
                    projecttypeList.Add(new ProjectTypeModel { ProjectType_ID = Projecttype.ProjectType_ID, ProjectType = Projecttype.ProjectType, });
                }
                return projecttypeList;
            }
        }

        public static bool Create(LocationModel locationmodel)
        {
            Location location = GetLocation(locationmodel);
            return location.Create();
        }

        public static IEnumerable<LocationModel> List(long User_ID, string view)
        {
            IEnumerable<Location> LocationList = (IEnumerable<Location>)new Location().List(User_ID, view);
            if (LocationList != null)
            {
                List<LocationModel> LocationModelList = new List<LocationModel>();

                foreach (Location location in LocationList)
                {
                    LocationModelList.Add(GetLocationModel(location));
                }
                return LocationModelList;
            }
            return null;
        }

        public static IEnumerable<LocationModel> List(string search, long User_ID, string view)
        {
            IEnumerable<Location> LocationList = new Location().List(search, User_ID, view);
            if (LocationList != null)
            {
                List<LocationModel> LocationModelList = new List<LocationModel>();
                foreach (Location location in LocationList)
                {
                    LocationModelList.Add(GetLocationModel(location));
                }
                return LocationModelList;
            }
            return null;
        }

        public static List<string> SearchByName(string search, long User_ID)
        {
            var LocationName = new Location().AutoList(search, User_ID);

            if (LocationName != null)
            {
                return LocationName.ToList();
            }
            return null;
        }

        public static LocationModel Single(long Location_ID, long User_ID)
        {
            Location location = new Location().Single((long)Location_ID, User_ID);
            return GetLocationModel(location);
        }

        public static bool Edit(LocationModel Locationmodel)
        {
            Location location = GetLocation(Locationmodel);
            Locationmodel.lInspector_ID = location.sInspector_ID;
            Locationmodel.lReviewer_ID = location.sReviewer_ID;
            return location.Edit();
        }

        public static bool Delete(LocationModel Locationmodel)
        {
            return new Location
            {
                Location_ID = Locationmodel.Location_ID,
                CreatedBy_ID = Locationmodel.CreatedBy_ID
            }.Delete();
        }

        private static Location GetLocation(LocationModel locationmodel)
        {
            
            return new Location
            {
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID,
                Location_ID = locationmodel.Location_ID,
                Client_ID = locationmodel.Client_ID,
                Customer_Name = locationmodel.CustomerName,
                Company_Name = locationmodel.CompanyName,
                Name = locationmodel.Name,
                MailingAddress = locationmodel.MailingAddress,
                F1_ID = locationmodel.F1_ID,
                F2_ID = locationmodel.F2_ID,
                //Email1=locationmodel.Email_1,
                //Email2=locationmodel.Email_2,
                InspectionReportEmails = locationmodel.InspectionReportEmails,
                WorkOrdersEmails = locationmodel.WorkOrdersEmails,
                Threedaynoticeemails = locationmodel.Threedaynoticeemails,
                Fivedaynoticeemails = locationmodel.Fivedaynoticeemails,
                Sevendaynoticeemails = locationmodel.Sevendaynoticeemails,
                MaintainAction = locationmodel.MaintainAction,
                IsRequired = locationmodel.IsRequired,
                IsActive = locationmodel.IsActive,
                User_ID = locationmodel.User_ID,
                State = new State
                {
                    State_ID = locationmodel.State.State_ID,
                    Name = locationmodel.State.Name,
                    Code = locationmodel.State.Code
                },
                City = locationmodel.City,
                ZipCode = locationmodel.ZipCode,
                MailingAddress2 = locationmodel.MailingAddress2,
                ProjectType_ID = locationmodel.ProjectType_ID,
                InspectionFrequency = locationmodel.InspectionFreq,
                TrackingNumber = locationmodel.TrackingNumber,
                sInspector_ID = locationmodel.lstInspector_ID != null ? string.Join(",", locationmodel.lstInspector_ID) : string.Empty,
                sReviewer_ID = locationmodel.lstReviewer_ID != null ? string.Join(",", locationmodel.lstReviewer_ID) : string.Empty
            };
        }

        private static LocationModel GetLocationModel(Location location)
        {
            return new LocationModel
            {
                Location_ID = location.Location_ID,
                Client_ID = location.Client_ID,
                CustomerName = location.Customer_Name,
                CompanyName = location.Company_Name,
                Name = location.Name,
                City = location.City,
                MailingAddress = location.MailingAddress,
                F1_ID = location.F1_ID,
                F2_ID = location.F2_ID,
                InspectionReportEmails = location.InspectionReportEmails,
                WorkOrdersEmails = location.WorkOrdersEmails,
                Threedaynoticeemails = location.Threedaynoticeemails,
                Fivedaynoticeemails = location.Fivedaynoticeemails,
                Sevendaynoticeemails = location.Sevendaynoticeemails,
                MaintainAction = location.MaintainAction,
                IsRequired = location.IsRequired,
                IsActive = location.IsActive,
                //IsAssignInspector = location.IsAssignInspector,
                //IsAssignReviewer = location.IsAssignReviewer,
                //lInspector_ID = (location.sInspector_ID).Split(',').Select(long.Parse).ToList(),
                //lReviewer_ID = (location.sReviewer_ID).Split(',').Select(long.Parse).ToList(),
                //lInspector_ID = location.sInspector_ID != null ? (location.sInspector_ID).Split(',').Select(s => int.Parse(s)).ToArray(): null,
               // lReviewer_ID = location.sReviewer_ID != null ? (location.sReviewer_ID).Split(',').Select(s => int.Parse(s)).ToArray() : null,
                lInspector_ID = location.sInspector_ID != null ? location.sInspector_ID : string.Empty,
                lReviewer_ID = location.sReviewer_ID != null ? location.sReviewer_ID : string.Empty,
                State = new StateModel
                {
                    State_ID = location.State.State_ID,
                    Code = location.State.Code,
                    Name = location.State.Name
                },
                ZipCode = location.ZipCode,
                MailingAddress2 = location.MailingAddress2,
                ProjectType_ID = location.ProjectType_ID,                
                ProjectType=location.ProjectType,
                InspectionFreq = location.InspectionFrequency,
                TrackingNumber = location.TrackingNumber,
                User_ID = location.CreatedBy_ID,


            };
        }
        public static bool IsTrackingNumberAvailable(string TrackingNumber, long? Location_ID = 0)
        {
            return new Location().IsTrackingNumberAvailable(TrackingNumber, Location_ID);
        }

        public static IEnumerable<dynamic> GetClientDetails(long User_ID)
        {
            return new Location().GetClientDetails(User_ID);
        }

        internal static bool UpdateLocationStatus(long Location_ID, string view)
        {
            return new Location().UpdateLocationStatus(Location_ID, view);
        }


        public static IEnumerable<LocationModel> DisplayClientWiseProject(long Client_ID)
        {
            IEnumerable<Location> LocationList = (IEnumerable<Location>)new Location().DisplayClientWiseProject(Client_ID);
            if (LocationList != null)
            {
                List<LocationModel> LocationModelList = new List<LocationModel>();

                foreach (Location location in LocationList)
                {
                    LocationModelList.Add(GetClientWiseLocation(location));
                }
                return LocationModelList;
            }
            return null;
        }

        private static LocationModel GetClientWiseLocation(Location location)
        {
            return new LocationModel
            {
                Location_ID = location.Location_ID,
                ModifiedDate = location.ModifiedDate.HasValue ? location.ModifiedDate : null,
                Client_ID = location.Client_ID,
                CustomerName = location.Customer_Name,
                CompanyName = location.Company_Name,
                Name = location.Name,
                City = location.City,
                MailingAddress = location.MailingAddress,
                IsActive = location.IsActive,
                //DueDate=location.DueDate,
                days = location.days,
                //Date=location.Date,
                State = new StateModel
                {
                    State_ID = location.State.State_ID,
                    Code = location.State.Code,
                    Name = location.State.Name
                },
                ZipCode = location.ZipCode,
                MailingAddress2 = location.MailingAddress2
            };
        }



    }
}