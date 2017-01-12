using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;
using System.Data;
using System;

namespace e2rc.Models.Repository
{
    public static class ProjectManagerRepository
    {

        public static List<string> SearchByName(string search, long User_ID)
        {
            var Names = new ProjectManager().AutoList(search, User_ID);

            if (Names != null)
            {
                return Names.ToList();
            }
            return null;
        }

        public static IEnumerable<ProjectManagerModel> List(long User_ID,string view)
        {
            IEnumerable<ProjectManager> ProjectManagerList = (IEnumerable<ProjectManager>)new ProjectManager().List((long)User_ID,view);
            if (ProjectManagerList != null)
            {
                List<ProjectManagerModel> PMModelList = new List<ProjectManagerModel>();

                foreach (ProjectManager ProjectManager in ProjectManagerList)
                {
                    PMModelList.Add(GetProjectManagerModel(ProjectManager));
                }
                return PMModelList;
            }
            return null;
        }

        public static IEnumerable<ProjectManagerModel> List(string ProjectManagerName, long User_ID,string view)
        {
            IEnumerable<ProjectManager> ProjectManagerList = new ProjectManager().List(ProjectManagerName, User_ID,view);

            if (ProjectManagerList != null)
            {
                List<ProjectManagerModel> ProjectManagerModelList = new List<ProjectManagerModel>();

                foreach (ProjectManager ProjectManager in ProjectManagerList)
                {
                    ProjectManagerModelList.Add(GetProjectManagerModel(ProjectManager));
                }
                return ProjectManagerModelList;
            }
            return null;
        }

        private static ProjectManagerModel GetProjectManagerModel(ProjectManager ProjectManager)
        {
            ProjectManagerModel pmmodel = null;
            pmmodel = new ProjectManagerModel
            {
                ProjectManager_ID = ProjectManager.ProjectManager_ID,
                IsActive=ProjectManager.IsActive,
                //Inspector_ID = ProjectManager.Inspector_ID,  
                FirstName = ProjectManager.FirstName,
                LastName = ProjectManager.LastName,
                Date = ProjectManager.Date,
                UserName = ProjectManager.UserName,
                Password = ProjectManager.Password,
                ConfirmPassword = ProjectManager.Password,
                MobileNumber = ProjectManager.MobileNumber,
                PhoneNumber = ProjectManager.PhoneNumber,
                Qualification = ProjectManager.Qualification,
                Email = ProjectManager.Email,
                User_ID = ProjectManager.User_ID,
                Role = new RoleModel
                {
                    Role_ID = ProjectManager.Role.Role_ID,
                    Description = ProjectManager.Role.Description
                },
                Address = new AddressModel
                {
                    City = ProjectManager.Address.City,
                    MailingAddress = ProjectManager.Address.MailingAddress,
                    State = new StateModel
                    {
                        State_ID = ProjectManager.Address.State.State_ID,
                        Code = ProjectManager.Address.State.Code,
                        Name = ProjectManager.Address.State.Name
                    },
                    ZipCode = ProjectManager.Address.ZipCode
                },
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID,
                //Location_ID = ProjectManager.Location_ID
            };

            return pmmodel;
        }

        public static bool Create(ProjectManagerModel ProjectManagerModel)
        {
            ProjectManager ProjectManager = GetProjectManager(ProjectManagerModel);
            //return ProjectManager.Create(); 
            long ProjectManager_ID = ProjectManagerModel.ProjectManager_ID;
            bool result = ProjectManager.Create();
            bool success = false;
            if (result == true)
            {
                if (ProjectManagerModel.selectedLocationIDs != null)
                {
                    foreach (string Location_ID in ProjectManagerModel.selectedLocationIDs)
                    {
                        success = ProjectManager.CreatePMLocation(ProjectManager_ID, Convert.ToInt64(Location_ID));
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

        private static ProjectManager GetProjectManager(ProjectManagerModel ProjectManagerModel)
        {
            return new ProjectManager
            {
                //  Inspector_ID = ProjectManagerModel.inspector.Inspector_ID,

                ProjectManager_ID = ProjectManagerModel.ProjectManager_ID,
                IsActive=ProjectManagerModel.IsActive,
                FirstName = ProjectManagerModel.FirstName,
                LastName = ProjectManagerModel.LastName,
                Date = ProjectManagerModel.Date,
                UserName = ProjectManagerModel.UserName,
                Password = ProjectManagerModel.Password,
                MobileNumber = ProjectManagerModel.MobileNumber,
                PhoneNumber = ProjectManagerModel.PhoneNumber,
                Qualification = ProjectManagerModel.Qualification,
                Email = ProjectManagerModel.Email,
                Role = new Role
                {
                    Role_ID = ProjectManagerModel.Role.Role_ID,
                    Description = ProjectManagerModel.Role.Description
                },
                Address = new Address
                {
                    City = ProjectManagerModel.Address.City,
                    MailingAddress = ProjectManagerModel.Address.MailingAddress,
                    State = new State
                    {
                        State_ID = ProjectManagerModel.Address.State.State_ID,
                        Code = ProjectManagerModel.Address.State.Code,
                        Name = ProjectManagerModel.Address.State.Name
                    },
                    ZipCode = ProjectManagerModel.Address.ZipCode
                },
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID
            };
        }

        public static ProjectManagerModel Single(long? ProjectManager_ID, long User_ID)
        {
            ProjectManager ProjectManager = new ProjectManager().Single((long)ProjectManager_ID, User_ID);
            ProjectManagerModel pm = GetProjectManagerModel(ProjectManager);
            pm.LocationIDs = ProjectManager.LocationIDs;
            //foreach (var item in ProjectManager.locations)
            //{
            //    //pm.location.location_id = item["location_id"];
            //    //pm.location.name = item["name"];

            //}
            return pm;
        }

        public static bool Edit(ProjectManagerModel ProjectManagerModel)
        {
            ProjectManager ProjectManager = GetProjectManager(ProjectManagerModel);
            long? ProjectManager_ID = ProjectManagerModel.ProjectManager_ID;
            bool result = ProjectManager.Edit();
            bool success = false;
            if (result == true)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ProjectManager_ID", System.Type.GetType("System.Int64")));
                dataTable.Columns.Add(new DataColumn("Location_ID", System.Type.GetType("System.Int64")));

                if (ProjectManagerModel.LocationIDs != null)
                {
                    foreach (long Location_ID in ProjectManagerModel.LocationIDs)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ProjectManager_ID"] = ProjectManager_ID;
                        dataRow["Location_ID"] = Location_ID;
                        dataTable.Rows.Add(dataRow);
                        // success = ProjectManager.EditPMLocation(ProjectManager_ID, Location_ID);
                    }

                }
                try
                {
                    success = new e2rcModel.DataAccessLayer.DAL().Insert("sp_ProjectManagerLocation_CRUD",
                          new object[] { "@Action", "@ProjectManager_ID", "@Locations" },
                          new object[] { "UPDATE", ProjectManager_ID, dataTable });
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

        public static bool Delete(ProjectManagerModel ProjectManagerModel)
        {
            return new ProjectManager
            {
                ProjectManager_ID = ProjectManagerModel.ProjectManager_ID,
                CreatedBy_ID = ProjectManagerModel.CreatedBy_ID
            }.Delete();
        }

        public static long? ProjectManager_ID(long? User_ID)
        {
            return ProjectManager.get_ProjectManager_ID(User_ID);
        }
        public static long getProjectManager_UserID(long User_ID)
        {
            return (long)ProjectManager.getProjectManager_UserID(User_ID);
        }

        public static IEnumerable<dynamic> GetPMLocationDetails(long User_ID)
        {
            return new ProjectManager().GetPMLocationDetails(User_ID);
        }


        public static IEnumerable<ProjectManagerModel> sortPMDetails(long User_ID, string Search, string sortOrder, string view)
        {
            IEnumerable<ProjectManagerModel> PMList;
            if (!string.IsNullOrEmpty(Search))
            {
                PMList = List(Search, User_ID, view);
            }
            else
            {
                PMList = List(User_ID, view);
            }
            if (PMList != null)
            {
                switch (sortOrder)
                {

                    case "Name_desc":
                        PMList = PMList.OrderByDescending(m => m.Name);
                        break;
                    case "Name":
                        PMList = PMList.OrderBy(m => m.Name);
                        break;                   
                    case "UserName_desc":
                        PMList = PMList.OrderByDescending(m => m.UserName);
                        break;
                    case "UserName":
                        PMList = PMList.OrderBy(m => m.UserName);
                        break;
                    case "Email":
                        PMList = PMList.OrderBy(m => m.Email);
                        break;
                    case "Email_desc":
                        PMList = PMList.OrderByDescending(m => m.Email);
                        break;
                    case "Date":
                        PMList = PMList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        PMList = PMList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        PMList = PMList.OrderByDescending(m => m.Date);
                        break;
                }
                return PMList;
            }
            else
            {
                return null;
            }
        }

        internal static bool UpdateProjectManagerStatus(long ProjectManager_ID, string view)
        {
            return new ProjectManager().UpdateProjectManagerStatus(ProjectManager_ID, view);
        }
    }
}