using System;
using System.Collections.Generic;
using System.Data;
using e2rcModel.Common;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
    public class Inspector : User
    {
        public DateTime Date { get; set; }
        public long? Client_ID { get; set; }
        public long? Inspector_ID { get; set; }
        public bool IsActive { get; set; }
        public Address Address { get; set; }
        public string UploadSignPath { get; set; }
        public string SignPath { get; set; }
        public string InspectorTitle { get; set; }
        public string sLocationID { get; set; }
        public Inspector()
        {
            UploadSignPath = string.Empty;
        }

        public IEnumerable<Inspector> Inspectors
        {
            get
            {
                DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_InspectorName_List");

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<Inspector> inspectors = new List<Inspector>();

                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        inspectors.Add(new Inspector
                        {
                            Inspector_ID = Convert.ToInt64(Row["Inspector_ID"]),
                            Name = Convert.ToString(Row["Name"])
                        });
                    }
                    return inspectors;

                }
                return null;
            }
        }

        public override bool Create()
        {
            return new DAL().Insert("sp_Inspector_CRUD",
                 new object[] {"@Action", "@Date", "@FirstName", "@LastName", "@UserName", "@Email", "@Role_ID", "@Password", "@Qualification", "@MobileNumber", "@PhoneNumber", "@City", "@State_ID", "@MailingAddress", "@ZipCode", "@CreatedBy", "@UploadSignPath", "@InspectorTitle", "@IsActive", "@lstLocationID"
                },
                 new object[] {Actions.INSERT.ToString(), Date, FirstName, LastName, UserName, Email, Role.Role_ID, Password, Qualification, MobileNumber, PhoneNumber, Address.City, Address.State.State_ID, Address.MailingAddress, Address.ZipCode, CreatedBy_ID, UploadSignPath, InspectorTitle, IsActive, sLocationID                 
                 });
        }

        public override bool Edit()
        {
            return new DAL().Update("sp_Inspector_CRUD",
                 new object[] {"@Action", "@Date", "@FirstName", "@LastName", "@UserName", "@Email", "@Role_ID", "@Password", "@MobileNumber", "@PhoneNumber", "@Qualification", "@City", "@State_ID", "@MailingAddress", "@ZipCode", "@CreatedBy", "@Inspector_ID", "@UploadSignPath", "@InspectorTitle", "@IsActive", "@lstLocationID"
                },
                 new object[] {Actions.UPDATE.ToString(),Date,FirstName,LastName,UserName,Email,Role.Role_ID,Password, MobileNumber,PhoneNumber,Qualification, Address.City, Address.State.State_ID, Address.MailingAddress, Address.ZipCode, CreatedBy_ID, Inspector_ID, UploadSignPath, InspectorTitle, IsActive, sLocationID                   
                });
        }

        public override bool Delete()
        {
            return new DAL().Delete("sp_Inspector_CRUD",
                                               new object[] { "@Action", "@Inspector_ID", "@CreatedBy" },
                                               new object[] { Actions.DELETE.ToString(), Inspector_ID, CreatedBy_ID });
        }

        public IEnumerable<User> List(long CreatedBy_ID,string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Inspector_List", new object[] { "@CreatedBy", "@view" }, new object[] { CreatedBy_ID, view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Inspector> InspectorList = new List<Inspector>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    InspectorList.Add(new Inspector
                    {
                        Inspector_ID = Convert.ToInt64(row["Inspector_ID"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName=Convert.ToString(row["LastName"]),
                        UserName = Convert.ToString(row["UserName"]),
                        Email = Convert.ToString(row["Email"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        //Password = Convert.ToString(row["Password"]),
                        MobileNumber = Convert.ToString(row["MobileNumber"]),
                        PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                        Qualification = Convert.ToString(row["Qualification"]),
                        UploadSignPath = Convert.ToString(row["UploadSignPath"]),
                        InspectorTitle = Convert.ToString(row["InspectorTitle"]),
                        SignPath = Convert.ToString(row["SignPath"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
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
                return InspectorList;
            }
            return null;
        }

        public IEnumerable<Inspector> List(string search, long CreatedBy_ID,string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Inspector_List",
                                                                new object[] { "@Search_By", "@CreatedBy", "@view" },
                                                                new object[] { search, CreatedBy_ID, view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Inspector> InspectorList = new List<Inspector>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    InspectorList.Add(new Inspector
                    {
                        Inspector_ID = Convert.ToInt64(row["Inspector_ID"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        UserName = Convert.ToString(row["UserName"]),
                        //Password = Convert.ToString(row["Password"]),
                        Email = Convert.ToString(row["Email"]),
                        MobileNumber = Convert.ToString(row["MobileNumber"]),
                        PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                        Qualification = Convert.ToString(row["Qualification"]),
                        UploadSignPath = Convert.ToString(row["UploadSignPath"]),
                        SignPath = Convert.ToString(row["SignPath"]),
                        IsActive=Convert.ToBoolean(row["IsActive"]),
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
                return InspectorList;
            }
            return null;
        }

        public IEnumerable<string> AutoList(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Inspector_SearchName",
                                                                new object[] { "@Search_By", "@CreatedBy" },
                                                                new object[] { search, CreatedBy_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> InspectorName = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    InspectorName.Add(Convert.ToString(row["LastName"]));
                }
                return InspectorName;
            }
            return null;
        }
        public IEnumerable<string> LocationAutoList(string search, long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Project_Inspector_SearchName",
                                                                new object[] { "@Search_By", "@User_ID" },
                                                                new object[] { search, User_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> LocationName = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    LocationName.Add(Convert.ToString(row["Name"]));
                }
                return LocationName;
            }
            return null;
        }
        public IEnumerable<string> LocationAutoListStationBased(string search, long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Project_Inspector_SearchName_StationBased",
                                                                new object[] { "@Search_By", "@User_ID" },
                                                                new object[] { search, User_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> LocationName = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    LocationName.Add(Convert.ToString(row["Name"]));
                }
                return LocationName;
            }
            return null;
        }

        public IEnumerable<string> LocationAutoCompleteList(string search, long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Project_Inspectorwise_SearchName",
                                                                new object[] { "@Search_By", "@User_ID" },
                                                                new object[] { search, User_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> LocationName = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    LocationName.Add(Convert.ToString(row["Name"]));
                }
                return LocationName;
            }
            return null;
        }

        public IEnumerable<string> LocationAutoCompleteListStationBased(string search, long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Project_Inspectorwise_SearchName_forStationBased",
                                                                new object[] { "@Search_By", "@User_ID" },
                                                                new object[] { search, User_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> LocationName = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    LocationName.Add(Convert.ToString(row["Name"]));
                }
                return LocationName;
            }
            return null;
        }

        public Inspector Single(long Inspector_ID, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getInspector_List",
                                                               new object[] { "@Inspector_ID", "@CreatedBy" },
                                                               new object[] { Inspector_ID, CreatedBy_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                return (new Inspector
                {
                    Inspector_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Inspector_ID"]),
                    FirstName = Convert.ToString(dataset.Tables[0].Rows[0]["FirstName"]),
                    LastName = Convert.ToString(dataset.Tables[0].Rows[0]["LastName"]),
                    Date = Convert.ToDateTime(dataset.Tables[0].Rows[0]["Date"]),
                    UserName = Convert.ToString(dataset.Tables[0].Rows[0]["UserName"]),
                    Password = Convert.ToString(dataset.Tables[0].Rows[0]["Password"]),
                    Email = Convert.ToString(dataset.Tables[0].Rows[0]["Email"]),
                    MobileNumber = Convert.ToString(dataset.Tables[0].Rows[0]["MobileNumber"]),
                    PhoneNumber = Convert.ToString(dataset.Tables[0].Rows[0]["PhoneNumber"]),
                    Qualification = Convert.ToString(dataset.Tables[0].Rows[0]["Qualification"]),
                    UploadSignPath = Convert.ToString(dataset.Tables[0].Rows[0]["UploadSignPath"]),
                    InspectorTitle = Convert.ToString(dataset.Tables[0].Rows[0]["InspectorTitle"]),
                    SignPath = Convert.ToString(dataset.Tables[0].Rows[0]["SignPath"]),
                    IsActive = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsActive"]),
                    sLocationID = Convert.ToString(dataset.Tables[0].Rows[0]["slLocationId"]),
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
                });
            }
            return null;
        }


        /// //////////////////////// FOR INSPECTION SUBMISSION INDEX /////////////////////////////////
       

        public IEnumerable<Submission> SubmissionList(long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_InspectorSubmission_List", new object[] { "@User_ID" }, new object[] { User_ID });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        ClientName = Convert.ToString(row["Client"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        IsComplete = Convert.ToBoolean(row["IsComplete"]),
                        path = Convert.ToString(row["path"])
                    });
                }
                return SubmissionList;
            }

            return null;
        }

        public IEnumerable<Submission> SubmissionList(string Search_By, long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_InspectorSubmission_List", new object[] { "@User_ID", "@Search_By" }, new object[] { User_ID, Search_By });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        ClientName = Convert.ToString(row["Client"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        IsComplete = Convert.ToBoolean(row["IsComplete"]),
                        path = Convert.ToString(row["path"])
                    });
                }
                return SubmissionList;
            }

            return null;
        }

        public IEnumerable<Submission> CompleteList(long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_CompleteSubmission_List", new object[] { "@User_ID" }, new object[] { User_ID });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        ClientName = Convert.ToString(row["Client"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        IsComplete = Convert.ToBoolean(row["IsComplete"]),
                        IsAutoresponder = Convert.ToBoolean(row["IsAutoresponder"]),
                        path = Convert.ToString(row["path"])
                    });
                }
                return SubmissionList;
            }
            return null;
        }

        public IEnumerable<Submission> CompleteList(string Search_By, long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_CompleteSubmission_List", new object[] { "@User_ID", "@Search_By" }, new object[] { User_ID, Search_By });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ClientName = Convert.ToString(row["Client"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        IsComplete = Convert.ToBoolean(row["IsComplete"]),
                        IsAutoresponder = Convert.ToBoolean(row["IsAutoresponder"]),
                        path = Convert.ToString(row["path"])
                    });
                }
                return SubmissionList;
            }
            return null;
        }


        ////////////////////////// FOR STATION INSPECTION SUBMISSION INDEX  /////////////////////////////

        public IEnumerable<Submission> StationSubmissionList(long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_InspectorStationSubmission_List", new object[] { "@User_ID" }, new object[] { User_ID });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> StationSubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    StationSubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        IsComplete = Convert.ToBoolean(row["IsComplete"])
                    });
                }
                return StationSubmissionList;
            }

            return null;
        }

        public IEnumerable<Submission> StationSubmissionList(string Search_By, long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_InspectorStationSubmission_List", new object[] { "@User_ID", "@Search_By" }, new object[] { User_ID, Search_By });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> StationSubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    StationSubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        IsComplete = Convert.ToBoolean(row["IsComplete"])
                    });
                }
                return StationSubmissionList;
            }

            return null;
        }

        public IEnumerable<Submission> StationCompleteList(long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_CompleteStationSubmission_List", new object[] { "@User_ID" }, new object[] { User_ID });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> StationSubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    StationSubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        IsComplete = Convert.ToBoolean(row["IsComplete"]),
                        IsAutoresponder = Convert.ToBoolean(row["IsAutoresponder"])
                    });
                }
                return StationSubmissionList;
            }
            return null;
        }

        public IEnumerable<Submission> StationCompleteList(string Search_By, long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_CompleteStationSubmission_List", new object[] { "@User_ID", "@Search_By" }, new object[] { User_ID, Search_By });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> StationSubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    StationSubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        IsComplete = Convert.ToBoolean(row["IsComplete"]),
                        IsAutoresponder = Convert.ToBoolean(row["IsAutoresponder"])
                    });
                }
                return StationSubmissionList;
            }
            return null;
        }


        public static long? InspectorID(long? User_ID)
        {
            return (long?)new DAL().ExecuteStoredProcedure("sp_getInspector_ID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@Inspector_ID", "0", System.Data.SqlDbType.BigInt);
        }



        public static long getInspector_UserID(long User_ID)
        {
            return (long)new DAL().ExecuteStoredProcedure("sp_getInspector_UserID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@InspUser_ID", "0", System.Data.SqlDbType.BigInt);
        }

        public bool UpdateInspectorStatus(long Inspector_ID)
        {
            return new DAL().Update("sp_ActivateInspectorStatus",
                  new object[] {"@Inspector_ID"
                },
                  new object[] {Inspector_ID               
                });
        }
        public bool DeActivateInspectorStatus(long Inspector_ID)
        {
            return new DAL().Update("sp_DeActivateInspectorStatus",
                  new object[] {"@Inspector_ID"
                },
                  new object[] {Inspector_ID               
                });
        }
    }
}
