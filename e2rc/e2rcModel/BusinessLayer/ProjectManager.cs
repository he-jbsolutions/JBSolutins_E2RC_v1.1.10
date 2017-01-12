using System;
using System.Collections.Generic;
using System.Data;
using e2rcModel.Common;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
    public class ProjectManager : User
    {
        public DateTime Date { get; set; }
        public long? Client_ID { get; set; }
        public long ProjectManager_ID { get; set; }
        public long? Inspector_ID { get; set; }
        public long Location_ID { get; set; }
        public string InspectorName { get; set; }
        public bool IsActive { get; set; }
        public Address Address { get; set; }
        public List<long> LocationIDs { get; set; }
        //public List<Int64,string> location_ID = new List<Int64,string>();
        public string[] selectedLocationIDs { get; set; }
        public ProjectManager()
        {
            Inspector_ID = 0;
        }
         
        public IEnumerable<string> AutoList(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_ProjectManager_SearchName",
                                                                new object[] { "@Search_By", "@CreatedBy" },
                                                                new object[] { search, CreatedBy_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> Name = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    Name.Add(Convert.ToString(row["Name"]));
                }
                return Name;
            }
            return null;
        }

        public IEnumerable<User> List(long CreatedBy_ID,string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_ProjectManager_List", new object[] { "@CreatedBy","@view" }, new object[] { CreatedBy_ID,view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<ProjectManager> ProjectManagerList = new List<ProjectManager>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ProjectManagerList.Add(new ProjectManager
                    {
                        ProjectManager_ID = Convert.ToInt64(row["ProjectManager_ID"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        //Inspector_ID = Convert.ToInt64(row["Inspector_ID"]),
                        //  InspectorName = Convert.ToString(row["InspectorName"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        UserName = Convert.ToString(row["UserName"]),
                        Email = Convert.ToString(row["Email"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        //Password = Convert.ToString(row["Password"]),
                        MobileNumber = Convert.ToString(row["MobileNumber"]),
                        PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                        Qualification = Convert.ToString(row["Qualification"]),

                        Role = new Role
                        {
                            Role_ID = Convert.ToByte(row["Role_ID"]),
                            Description = Convert.ToString(row["Role"])
                        },
                        Address = new Address
                        {
                            Address_ID = Convert.ToInt64(row["Address_ID"]),
                            City = Convert.ToString(row["City"]),
                            MailingAddress = Convert.ToString(row["MailingAddress"]),
                            ZipCode = Convert.ToString(row["ZipCode"]),
                            State = new State
                            {
                                State_ID = Convert.ToByte(row["State_ID"]),
                                Name = Convert.ToString(row["StateName"]),
                                Code = Convert.ToString(row["Code"])
                            }
                        }
                    });
                }
                return ProjectManagerList;
            }
            return null;
        }

        public IEnumerable<ProjectManager> List(string search, long CreatedBy_ID,string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_ProjectManager_List",
                                                                new object[] { "@Search_By", "@CreatedBy","@view" },
                                                                new object[] { search, CreatedBy_ID,view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<ProjectManager> ProjectManagerList = new List<ProjectManager>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ProjectManagerList.Add(new ProjectManager
                    {
                        ProjectManager_ID = Convert.ToInt64(row["ProjectManager_ID"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                       // Location_ID = Convert.ToInt64(row["Location_ID"]),
                        //Inspector_ID = Convert.ToInt64(row["Inspector_ID"]),
                        //InspectorName = Convert.ToString(row["InspectorName"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        UserName = Convert.ToString(row["UserName"]),
                        //Password = Convert.ToString(row["Password"]),
                        Email = Convert.ToString(row["Email"]),
                        MobileNumber = Convert.ToString(row["MobileNumber"]),
                        PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                        Qualification = Convert.ToString(row["Qualification"]),
                        Role = new Role
                        {
                            Role_ID = Convert.ToByte(row["Role_ID"]),
                            Description = Convert.ToString(row["Role"])
                        },
                        Address = new Address
                        {
                            Address_ID = Convert.ToInt64(row["Address_ID"]),
                            City = Convert.ToString(row["City"]),
                            MailingAddress = Convert.ToString(row["MailingAddress"]),
                            ZipCode = Convert.ToString(row["ZipCode"]),
                            State = new State
                            {
                                State_ID = Convert.ToByte(row["State_ID"]),
                                Name = Convert.ToString(row["StateName"]),
                                Code = Convert.ToString(row["Code"])
                            }
                        },
                        User_ID = Convert.ToInt64(row["User_ID"])
                    });
                }
                return ProjectManagerList;
            }
            return null;
        }

        public ProjectManager Single(long ProjectManager_ID, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getProjectManager_List",
                                                               new object[] { "@ProjectManager_ID", "@CreatedBy" },
                                                               new object[] { ProjectManager_ID, CreatedBy_ID });
            ProjectManager projectManager = null;
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                projectManager = new ProjectManager
                {
                    ProjectManager_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["ProjectManager_ID"]),
                    IsActive = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsActive"]),
                    //Inspector_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Inspector_ID"]),
                    //InspectorName = Convert.ToString(dataset.Tables[0].Rows[0]["InspectorName"]),
                    FirstName = Convert.ToString(dataset.Tables[0].Rows[0]["FirstName"]),
                    LastName = Convert.ToString(dataset.Tables[0].Rows[0]["LastName"]),
                    Date = Convert.ToDateTime(dataset.Tables[0].Rows[0]["Date"]),
                    UserName = Convert.ToString(dataset.Tables[0].Rows[0]["UserName"]),
                    Password = Convert.ToString(dataset.Tables[0].Rows[0]["Password"]),
                    Email = Convert.ToString(dataset.Tables[0].Rows[0]["Email"]),
                    MobileNumber = Convert.ToString(dataset.Tables[0].Rows[0]["MobileNumber"]),
                    PhoneNumber = Convert.ToString(dataset.Tables[0].Rows[0]["PhoneNumber"]),
                    Qualification = Convert.ToString(dataset.Tables[0].Rows[0]["Qualification"]),
                  //  Location_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Location_ID"]),
                    Role = new Role
                    {
                        Role_ID = Convert.ToByte(dataset.Tables[0].Rows[0]["Role_ID"]),
                        Description = Convert.ToString(dataset.Tables[0].Rows[0]["Role"])
                    },
                    Address = new Address
                    {
                        Address_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Address_ID"]),
                        City = Convert.ToString(dataset.Tables[0].Rows[0]["City"]),
                        MailingAddress = Convert.ToString(dataset.Tables[0].Rows[0]["MailingAddress"]),
                        ZipCode = Convert.ToString(dataset.Tables[0].Rows[0]["ZipCode"]),
                        State = new State
                        {
                            State_ID = Convert.ToByte(dataset.Tables[0].Rows[0]["State_ID"]),
                            Name = Convert.ToString(dataset.Tables[0].Rows[0]["StateName"]),
                            Code = Convert.ToString(dataset.Tables[0].Rows[0]["Code"])
                        }
                    },
                    User_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["User_ID"])
                };

                if (dataset.Tables[1].Rows.Count != 0)
                {
                    List<long> tempLocationList = new List<long>();
                    foreach (DataRow Row in dataset.Tables[1].Rows)
                    {
                        tempLocationList.Add(Convert.ToInt64(Row["Location_ID"]));
                    }
                    projectManager.LocationIDs = tempLocationList;
                }
            }

            return projectManager;
        }
        public override bool Create()
        {
            return new DAL().Insert("sp_ProjectManager_CRUD",
                 new object[] {"@Action", "@Date","@FirstName","@LastName","@UserName","@Email","@Role_ID","@Password","@Qualification",
                                "@MobileNumber","@PhoneNumber","@City","@State_ID","@MailingAddress","@ZipCode","@CreatedBy","@IsActive"
                },
                 new object[] {Actions.INSERT.ToString(),Date,FirstName,LastName,UserName,Email,Role.Role_ID,Password,
                                Qualification,MobileNumber,PhoneNumber, Address.City,Address.State.State_ID,Address.MailingAddress,Address.ZipCode,
                                CreatedBy_ID ,IsActive              
                });
        }

        public override bool Edit()
        {
            return new DAL().Update("sp_ProjectManager_CRUD",
                 new object[] {"@Action", "@Date","@FirstName","@LastName", "@UserName","@Email","@Role_ID","@Password",
                                "@MobileNumber","@PhoneNumber","@Qualification","@City","@State_ID","@MailingAddress","@ZipCode","@CreatedBy","@ProjectManager_ID","@IsActive"
                },
                 new object[] {Actions.UPDATE.ToString(),Date,FirstName,LastName,UserName,Email,Role.Role_ID,Password,
                                MobileNumber,PhoneNumber,Qualification, Address.City,Address.State.State_ID,Address.MailingAddress,Address.ZipCode,
                                CreatedBy_ID,ProjectManager_ID,IsActive     
                });
        }

        public override bool Delete()
        {
            return new DAL().Delete("sp_ProjectManager_CRUD",
                                               new object[] { "@Action", "@ProjectManager_ID", "@CreatedBy" },
                                               new object[] { Actions.DELETE.ToString(), ProjectManager_ID, CreatedBy_ID });
        }

        public bool CreatePMLocation(long ProjectManager_ID, long Location_ID)
        {
            return new DAL().Insert("sp_ProjectManagerLocation_CRUD",
                 new object[] { "@Action", "@ProjectManager_ID", "@Location_ID" },
                 new object[] {Actions.INSERT.ToString(),Convert.ToInt64(ProjectManager_ID) ,Convert.ToInt64(Location_ID)
                });
        }

        public bool EditPMLocation(long? ProjectManager_ID, long Location_ID)
        {
            return new DAL().Insert("sp_ProjectManagerLocation_CRUD",
                 new object[] { "@Action", "@ProjectManager_ID", "@Location_ID" },
                 new object[] {Actions.UPDATE.ToString(),Convert.ToInt64(ProjectManager_ID) ,Convert.ToInt64(Location_ID)
                });
        }

        public static long? get_ProjectManager_ID(long? User_ID)
        {
            return (long?)new DAL().ExecuteStoredProcedure("get_ProjectManager_ID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@ProjectManager_ID", "0", System.Data.SqlDbType.BigInt);
        }

        public static long getProjectManager_UserID(long User_ID)
        {
            return (long)new DAL().ExecuteStoredProcedure("sp_ProjectManager_UserID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@PMUser_ID", "0", System.Data.SqlDbType.BigInt);
        }


        public IEnumerable<dynamic> GetPMLocationDetails(long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_PMLocation_List", new object[] { "@User_ID" }, new object[] { User_ID });
            List<dynamic> PMLocationList = new List<dynamic>();
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    PMLocationList.Add(new
                    {
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        Name = Convert.ToString(row["Name"]),
                    });
                }
                return PMLocationList;
            }
            else
            {
                PMLocationList.Add(new { Location_ID = 0, Name = "" });
                return PMLocationList;
            }
        }

        public bool UpdateProjectManagerStatus(long ProjectManager_ID, string view)
        {
            return new DAL().Update("sp_UpdateProjectManagerStatus",
                  new object[] {"@ProjectManager_ID","@view"
                },
                  new object[] {ProjectManager_ID ,view              
                });
        }
    }
}
