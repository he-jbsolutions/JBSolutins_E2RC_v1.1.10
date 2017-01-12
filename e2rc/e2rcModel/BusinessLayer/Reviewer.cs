using System;
using System.Collections.Generic;
using System.Data;
using e2rcModel.Common;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
    public class Reviewer : User
    {

        public DateTime Date { get; set; }
        public long? Client_ID { get; set; }
        public long Reviewer_ID { get; set; }
        public string ReviewerTitle { get; set; }
        public long Location_ID { get; set; }
        public string InspectorName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAllowToCloseWorkOrder { get; set; }
        public Address Address { get; set; }
        public List<long> ClientIds { get; set; }
        //public List<Int64,string> location_ID = new List<Int64,string>();
        public string[] selectedLocationIDs { get; set; }
        public string selectedClientIDs { get; set; }

        public Reviewer()
        {
            Client_ID = 0;
        }

        public IEnumerable<User> List(long CreatedBy_ID, string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Reviewer_List", new object[] { "@CreatedBy", "@view" }, new object[] { CreatedBy_ID, view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Reviewer> ReviewerList = new List<Reviewer>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ReviewerList.Add(new Reviewer
                    {
                        Reviewer_ID = Convert.ToInt64(row["Reviewer_ID"]),
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
                return ReviewerList;
            }
            return null;
        }

        public IEnumerable<Reviewer> List(string search, long CreatedBy_ID, string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Reviewer_List",
                                                                new object[] { "@Search_By", "@CreatedBy", "@view" },
                                                                new object[] { search, CreatedBy_ID, view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Reviewer> ReviewerList = new List<Reviewer>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ReviewerList.Add(new Reviewer
                    {
                        Reviewer_ID = Convert.ToInt64(row["Reviewer_ID"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        IsAllowToCloseWorkOrder = Convert.ToBoolean(row["IsAllowToCloseWorkOrder"]),
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
                return ReviewerList;
            }
            return null;
        }

        public Reviewer Single(long ProjectManager_ID, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getReviewer_List",
                                                               new object[] { "@Reviewer_ID", "@CreatedBy" },
                                                               new object[] { ProjectManager_ID, CreatedBy_ID });
            Reviewer reviewer = null;
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                reviewer = new Reviewer
                {
                    Reviewer_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Reviewer_ID"]),
                    IsActive = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsActive"]),
                    IsAllowToCloseWorkOrder = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsAllowToCloseWorkOrder"]),
                    //IsAssignAllProject = Convert.ToBoolean(dataset.Tables[0].Rows[0]["ISAssignAllProject"]),
                    //Inspector_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Inspector_ID"]),
                    //InspectorName = Convert.ToString(dataset.Tables[0].Rows[0]["InspectorName"]),
                    sLocationID = Convert.ToString(dataset.Tables[0].Rows[0]["slLocationId"]),
                    ReviewerTitle = Convert.ToString(dataset.Tables[0].Rows[0]["ReviewerTitle"]),
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
                    List<long> tempClientList = new List<long>();
                    foreach (DataRow Row in dataset.Tables[1].Rows)
                    {
                        tempClientList.Add(Convert.ToInt64(Row["Client_ID"]));
                    }
                    reviewer.ClientIds = tempClientList;
                }
            }

            return reviewer;
        }
        public override bool Create()
        {
            return new DAL().Insert("sp_Reviewer_CRUD",
                 new object[] {"@Action", "@Date","@FirstName","@LastName","@UserName","@Email","@Role_ID","@Password","@Qualification",
                                "@MobileNumber","@PhoneNumber","@City","@State_ID","@MailingAddress","@ZipCode","@CreatedBy","@IsActive","@IsAllowToCloseWorkOrder","@ReviewerTitle","@lstLocationID", "@CLientIDSelected"
                },
                 new object[] {Actions.INSERT.ToString(),Date,FirstName,LastName,UserName,Email,Role.Role_ID,Password,
                                Qualification,MobileNumber,PhoneNumber, Address.City,Address.State.State_ID,Address.MailingAddress,Address.ZipCode,
                                CreatedBy_ID ,IsActive,IsAllowToCloseWorkOrder,ReviewerTitle, sLocationID, selectedClientIDs
                });
        }

        public override bool Edit()
        {
            return new DAL().Update("sp_Reviewer_CRUD",
                 new object[] {"@Action", "@Date", "@FirstName", "@LastName", "@UserName", "@Email", "@Role_ID", "@Password", "@MobileNumber", "@PhoneNumber", "@Qualification", "@City", "@State_ID",                  "@MailingAddress", "@ZipCode", "@CreatedBy", "@Reviewer_ID", "@IsActive", "@IsAllowToCloseWorkOrder", "@ReviewerTitle", "@lstLocationID", "@CLientIDSelected"
                },
                 new object[] {Actions.UPDATE.ToString(), Date, FirstName, LastName, UserName, Email, Role.Role_ID, Password, MobileNumber, PhoneNumber, Qualification, Address.City, Address.State.State_ID, Address.MailingAddress, Address. ZipCode, CreatedBy_ID, Reviewer_ID, IsActive, IsAllowToCloseWorkOrder, ReviewerTitle, sLocationID, selectedClientIDs
                });
        }

        public override bool Delete()
        {
            return new DAL().Delete("sp_Reviewer_CRUD",
                                               new object[] { "@Action", "@Reviewer_ID", "@CreatedBy" },
                                               new object[] { Actions.DELETE.ToString(), Reviewer_ID, CreatedBy_ID });
        }


        //public IEnumerable<dynamic> GetClientDetails(string Client_ID)
        //{

           
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("Client_ID", typeof(System.Int64));
        //        if (!string.IsNullOrEmpty(Client_ID))
        //        {
        //            foreach (var item in Client_ID.Split(','))
        //            {
        //                DataRow dr = dt.NewRow();
        //                dr[0] = Convert.ToInt64(item);
        //                dt.Rows.Add(dr);
        //            }
        //        }
        //        DataSet dataset = new DAL().ExecuteStoredProcedure("get_Reviewer_ClientwiseProject1", new object[] { "@Client_ID" }, new object[] { dt });
        //        List<dynamic> LocationList = new List<dynamic>();
        //        if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dataset.Tables[0].Rows)
        //            {
        //                LocationList.Add(new
        //                {
        //                    Location_ID = Convert.ToInt64(row["Location_ID"]),
        //                    Name = Convert.ToString(row["Name"]),
        //                });
        //            }
        //            return LocationList;
        //        }
        //        else
        //        {
        //            LocationList.Add(new { Location_ID = 0, Name = "" });
        //            return LocationList;
        //        }
        //    }

        public bool CreateReviewerClients(long Reviewer_ID, long Client_ID)
        {
            return new DAL().Insert("sp_ReviewerClient_CRUD",
                 new object[] { "@Action", "@Reviewer_ID", "@Client_ID" },
                 new object[] {Actions.INSERT.ToString(),Convert.ToInt64(Reviewer_ID),Convert.ToInt64(Client_ID)
            });
        }

        public bool EditReviewerClientsLocation(long? Reviewer_ID,long Client_ID)
        {
            return new DAL().Insert("sp_ReviewerClient_CRUD",
                 new object[] { "@Action", "@Reviewer_ID", "Client_ID" },
                 new object[] {Actions.UPDATE.ToString(),Convert.ToInt64(Reviewer_ID), Convert.ToInt64(Client_ID)
            });
        }


        public IEnumerable<dynamic> GetReviewerClientDetails(long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_ReviewerClient_List", new object[] { "@User_ID" }, new object[] { User_ID });
            List<dynamic> ReviewerList = new List<dynamic>();
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ReviewerList.Add(new
                    {
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                    });
                }

                return ReviewerList;
            }
            else
            {
                ReviewerList.Add(new { Client_ID = 0, CompanyName = "" });
                return ReviewerList;
            }
        }


        public static long? get_Reviewer_ID(long? User_ID)
        {
            return (long?)new DAL().ExecuteStoredProcedure("get_Reviewer_ID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@Reviewer_ID", "0", System.Data.SqlDbType.BigInt);
        }

        public static long getReviewer_UserID(long User_ID)
        {
            return (long)new DAL().ExecuteStoredProcedure("sp_Reviewer_UserID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@ReviewerUser_ID", "0", System.Data.SqlDbType.BigInt);
        }

        public bool UpdateReviewerStatus(long Reviewer_ID, string view)
        {
            return new DAL().Update("sp_UpdateReviewerStatus",
                  new object[] {"@Reviewer_ID","@view"
                },
                  new object[] {Reviewer_ID ,view              
                });
        }


        public IEnumerable<string> AutoList(string search, long User_ID, string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Reviewer_SearchName", new object[] { "@Search_By", "@CreatedBy", "@view" }, new object[] { search, User_ID, view });

            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> FranchiseNames = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    FranchiseNames.Add(Convert.ToString(row["FirstName"])); 
                }
                return FranchiseNames;
            }
            return null;
        }
    }
}
