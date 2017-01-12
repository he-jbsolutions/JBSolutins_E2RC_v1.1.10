using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.DataAccessLayer;
using e2rcModel.Common;
using System.Data;

namespace e2rcModel.BusinessLayer
{
    public class Client : User
    {
        public DateTime Date { get; set; }

        public string CompanyName { get; set; }

        public long? Client_ID { get; set; }

        public long? User_ID { get; set; }

        public bool IsActive { get; set; }
        
        public Address Address { get; set; }

        public string LogoPath { get; set; }
        public string EditLogoPath { get; set; }

        public override bool Create()
        {
            return new DAL().Insert("sp_Client_CRUD",
                 new object[] {"@Action", "@Date","@FirstName","@LastName", "@UserName","@Email","@Role_ID","@Password",
                                "@MobileNumber","@PhoneNumber","@City","@State_ID","@MailingAddress","@ZipCode","@CreatedBy","@IsActive","@CompanyName"/*,"@UploadLogoPath"*/
                },
                 new object[] {Actions.INSERT.ToString(),Date,FirstName,LastName,UserName,Email,Role.Role_ID,Password, MobileNumber,PhoneNumber,
                               Address.City,Address.State.State_ID,Address.MailingAddress,Address.ZipCode,CreatedBy_ID,IsActive,CompanyName/*,EditLogoPath*/
                });
        }

        public override bool Edit()
        {
            return new DAL().Update("sp_Client_CRUD",
                new object[] {"@Action", "@Date","@FirstName","@LastName" ,"@UserName","@Email","@Role_ID","@Password",
                                "@MobileNumber","@PhoneNumber","@City","@State_ID","@MailingAddress","@ZipCode","@CreatedBy","@Client_ID","@IsActive","@CompanyName"/*,"@UploadLogoPath"*/
                },
                new object[] {Actions.UPDATE.ToString(),Date,FirstName,LastName,UserName,Email,Role.Role_ID,Password,
                              MobileNumber,PhoneNumber,Address.City,Address.State.State_ID,Address.MailingAddress,Address.ZipCode,CreatedBy_ID,
                              Client_ID,IsActive,CompanyName/*,EditLogoPath*/
                });
        }

        public override bool Delete()
        {
            return new DAL().Delete("sp_Client_CRUD",
                                              new object[] { "@Action", "@Client_ID", "@CreatedBy" },
                                              new object[] { Actions.DELETE.ToString(), Client_ID, CreatedBy_ID });
        }
        public IEnumerable<User> List(long CreatedBy_ID,string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Client_List", new object[] { "@CreatedBy","@view" }, new object[] { CreatedBy_ID,view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Client> clientList = new List<Client>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    clientList.Add(new Client
                    {
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        UserName = Convert.ToString(row["UserName"]),
                        Email = Convert.ToString(row["Email"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        //Password = Convert.ToString(row["Password"]),
                        MobileNumber = Convert.ToString(row["MobileNumber"]),
                        PhoneNumber = Convert.ToString(row["PhoneNumber"]),
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
                return clientList;
            }
            return null;
        }

        public IEnumerable<Client> List(string search, long CreatedBy_ID,string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Client_List",
                                                                new object[] { "@Search_By", "@CreatedBy","@view" },
                                                                new object[] { search, CreatedBy_ID,view});
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Client> ClientList = new List<Client>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ClientList.Add(new Client
                    {
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        UserName = Convert.ToString(row["UserName"]),
                        //Password = Convert.ToString(row["Password"]),
                        Email = Convert.ToString(row["Email"]),
                        MobileNumber = Convert.ToString(row["MobileNumber"]),
                        PhoneNumber = Convert.ToString(row["PhoneNumber"]),
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
                       },
                        User_ID = Convert.ToInt64(row["User_ID"])
                    });
                }
                return ClientList;
            }
            return null;
        }

        public IEnumerable<string> AutoList(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Client_SearchName",
                                                                new object[] { "@Search_By", "@CreatedBy" },
                                                                new object[] { search, CreatedBy_ID });

            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> ClientName = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ClientName.Add(Convert.ToString(row["Name"]));
                }
                return ClientName;
            }
            return null;
        }
        public IEnumerable<string> ProjectAutoList(string search, long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Project_ClientWise_SearchName",
                                                                new object[] { "@Search_By", "@User_ID" },
                                                                new object[] { search, User_ID });

            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> ProjectName = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ProjectName.Add(Convert.ToString(row["Name"]));
                }
                return ProjectName;
            }
            return null;
        }

        public Client Single(long Client_ID, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getClient_List",
                                                               new object[] { "@Client_ID", "@CreatedBy" },
                                                               new object[] { Client_ID, CreatedBy_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                return (new Client
                {
                    Client_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Client_ID"]),
                    CompanyName = Convert.ToString(dataset.Tables[0].Rows[0]["CompanyName"]),
                    FirstName = Convert.ToString(dataset.Tables[0].Rows[0]["FirstName"]),
                    LastName = Convert.ToString(dataset.Tables[0].Rows[0]["LastName"]),
                    Date = Convert.ToDateTime(dataset.Tables[0].Rows[0]["Date"]),
                    UserName = Convert.ToString(dataset.Tables[0].Rows[0]["UserName"]),
                    Password = Convert.ToString(dataset.Tables[0].Rows[0]["Password"]),
                    Email = Convert.ToString(dataset.Tables[0].Rows[0]["Email"]),
                    MobileNumber = Convert.ToString(dataset.Tables[0].Rows[0]["MobileNumber"]),
                    PhoneNumber = Convert.ToString(dataset.Tables[0].Rows[0]["PhoneNumber"]),
                    IsActive = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsActive"]),
                    LogoPath = Convert.ToString(dataset.Tables[0].Rows[0]["LogoPath"]),
                    EditLogoPath = Convert.ToString(dataset.Tables[0].Rows[0]["UploadLogoPath"]),
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

        public IEnumerable<Client> clients
        {
            get
            {
                DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_ClientNames_List");

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<Client> clients = new List<Client>();

                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        clients.Add(new Client
                        {
                            Client_ID = Convert.ToInt64(Row["Client_ID"]),
                            Name = Convert.ToString(Row["Name"])
                        });
                    }
                    return clients;

                }
                return null;
            }
        }

        public IEnumerable<Submission> SubmissionList(long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_getClientSubmisionList", new object[] { "@User_ID" }, new object[] { User_ID });           
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        path = Convert.ToString(row["path"])

                    });
                }
                return SubmissionList;
            }

            return null;
        }
        public IEnumerable<Submission> SubmissionList(string Search_By,long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_getClientSubmisionList", new object[] { "@User_ID", "@Search_By" }, new object[] { User_ID, Search_By });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        path = Convert.ToString(row["path"])

                    });
                }
                return SubmissionList;
            }

            return null;
        }

        public IEnumerable <dynamic> ClientDetails(long Client_ID,long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Client_Details", new object[] { "@Client_ID", "@User_ID" }, new object[] { Client_ID, User_ID });
            List<dynamic> clientLocationList = new List<dynamic>();
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {                
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    clientLocationList.Add(new
                    {
                       // Client_ID = Convert.ToInt64(row["Client_ID"]),
                        Location_ID = Convert.ToInt16(row["Location_ID"]),
                        Name = Convert.ToString(row["ProjectName"]),
                       // ClientEmail = Convert.ToString(row["ClientEmail"])

                    });

                }
                return clientLocationList;

            }
            else
            {
                clientLocationList.Add(new { Client_ID = 0, Location_ID = 0, ProjectName = "", ClientEmail = "" });
                return clientLocationList;
            }
        }

        public static long? ClientID(long? User_ID)
        {
            return (long?)new DAL().ExecuteStoredProcedure("sp_getClient_ID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@Client_ID", "0", System.Data.SqlDbType.BigInt);
        }

        public static long getClient_UserID(long User_ID)
        {
            return (long)new DAL().ExecuteStoredProcedure("sp_getClient_UserID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@ClientUser_ID", "0", System.Data.SqlDbType.BigInt);
        }

        public bool UpdateClientStatus(long Client_ID)
        {
            return new DAL().Update("sp_UpdateClientStatus", new object[] { "@Client_ID" }, new object[] { Client_ID });

        }

        public bool ActivateClientStatus(long Client_ID)
        {
            return new DAL().Update("sp_ActivateClientStatus",
                  new object[] {"@Client_ID"
                },
                  new object[] {Client_ID               
                });
        }


        public IEnumerable<Client> ActiveClientList(long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_ActiveClient_List", new object[] { "@CreatedBy" }, new object[] { CreatedBy_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Client> clientList = new List<Client>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    clientList.Add(new Client
                    {
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        UserName = Convert.ToString(row["UserName"]),
                        Email = Convert.ToString(row["Email"]),
                        Date = Convert.ToDateTime(row["Date"]),                      
                        MobileNumber = Convert.ToString(row["MobileNumber"]),
                        PhoneNumber = Convert.ToString(row["PhoneNumber"]),
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
                                Name = Convert.ToString(row["StateName"])                               
                            }
                        }
                    });
                }
                return clientList;
            }
            return null;
        }

        public IEnumerable<Client> ActiveClientList(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_ActiveClient_List",
                                                                new object[] { "@Search_By", "@CreatedBy",},
                                                                new object[] { search, CreatedBy_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Client> ClientList = new List<Client>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ClientList.Add(new Client
                    {
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        //FirstName = Convert.ToString(row["FirstName"]),
                        //LastName = Convert.ToString(row["LastName"]),
                        //Date = Convert.ToDateTime(row["Date"]),
                        UserName = Convert.ToString(row["UserName"]),
                        //Password = Convert.ToString(row["Password"]),
                        Email = Convert.ToString(row["Email"]),
                        MobileNumber = Convert.ToString(row["MobileNumber"]),
                        PhoneNumber = Convert.ToString(row["PhoneNumber"]),
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
                                Name = Convert.ToString(row["StateName"])
                            }
                        },
                        User_ID = Convert.ToInt64(row["User_ID"])
                    });
                }
                return ClientList;
            }
            return null;
        }

    }
}
