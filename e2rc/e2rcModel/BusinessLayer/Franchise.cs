using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.BusinessLayer.Interface;
using e2rcModel.DataAccessLayer;
using System.ComponentModel;
using System.Data;
using e2rcModel.Common;
using System.Security.Cryptography;
using System.IO;
using System.Web.Http;
using System.Web;
using System.Web.Optimization;




namespace e2rcModel.BusinessLayer
{
    public class Franchise : ICRUD<Franchise, long>
    {
        public long? Franchise_ID { get; set; }

        public string FraCompName { get; set; }

        public Boolean FranchiseStatus { get; set; }

        public DateTime Date { get; set; }

        public User AdminUser { get; set; }

        public Address Address { get; set; }

        public long? CreatedBy_ID { get; set; }

        public bool IsActive { get; set; }

        public string UploadLogoPath { get; set; }



        public bool Create()
        {
            return new DAL().Insert("sp_Franchise_CRUD",
                  new object[] {"@Action", "@FraCompName", "@Date","@Name",
                    "@FirstName","@LastName", "@UserName","@Email","@CompanyName","@Role_ID","@Password","@MobileNumber","@PhoneNumber",
                    "@City","@State_ID","@MailingAddress","@ZipCode","@CreatedBy","@IsActive", "@UploadLogoPath"
                },
                  new object[] {Actions.INSERT.ToString(), FraCompName,Date,AdminUser.Name,
                    AdminUser.FirstName,AdminUser.LastName,AdminUser.UserName,AdminUser.Email,FraCompName,AdminUser.Role.Role_ID,AdminUser.Password,
                    AdminUser.MobileNumber,AdminUser.PhoneNumber,
                    Address.City,Address.State.State_ID,Address.MailingAddress,Address.ZipCode,CreatedBy_ID,IsActive, UploadLogoPath
                
                });
        }

        public bool Edit()
        {
            return new DAL().Update("sp_Franchise_CRUD",
                  new object[] {"@Action", "@FraCompName", "@Date",
                    "@FirstName", "@LastName","@UserName","@Email","@CompanyName","@Role_ID","@Password","@MobileNumber","@PhoneNumber",
                    "@City","@State_ID","@MailingAddress","@ZipCode","@CreatedBy","@FranchiseID","@IsActive","@UploadLogoPath"
                },
                  new object[] {Actions.UPDATE.ToString(), FraCompName,Date,
                    AdminUser.FirstName,AdminUser.LastName,AdminUser.UserName,AdminUser.Email,FraCompName,AdminUser.Role.Role_ID,AdminUser.Password,
                    AdminUser.MobileNumber,AdminUser.PhoneNumber,
                    Address.City,Address.State.State_ID,Address.MailingAddress,Address.ZipCode,CreatedBy_ID,Franchise_ID,IsActive, UploadLogoPath          
                  });
        }

        public bool LogoUpdate()
        {
            return new DAL().Update("sp_Franchise_Logo_Edit",
                            new object[] {"@Action", "@CreatedBy","@FranchiseID", "@UploadLogoPath" },

                            new object[] {Actions.UPDATE.ToString(), CreatedBy_ID, Franchise_ID, UploadLogoPath });
        }
        public int GetUserLoginRoleID(string sUserName)
        {
            int RoleID =0 ;
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Get_User_Login_Role", new object[] { "@UserName" }, new object[] { sUserName });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                 RoleID =Convert.ToInt32(dataset.Tables[0].Rows[0]["Role_ID"].ToString());
            }
            return RoleID;
        }

        public bool Delete()
        {
            return new DAL().Delete("sp_Franchise_CRUD",
                  new object[] {"@Action", "@FranchiseID","@IsActive","@CreatedBy"
                },
                  new object[] {Actions.DELETE.ToString(), Franchise_ID,IsActive, CreatedBy_ID                
                });
        }

        public IEnumerable<Franchise> List(string companyName,string view)
        {

            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_fetchFranchise_List", new object[] { "@Search_By", "@view" }, new object[] { companyName, view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Franchise> franchiseList = new List<Franchise>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    franchiseList.Add(new Franchise
                    {
                        CreatedBy_ID = Convert.ToInt64(row["CreatedBy"]),
                        Franchise_ID = Convert.ToInt64(row["Franchise_ID"]),
                        FraCompName = Convert.ToString(row["FraCompName"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        FranchiseStatus = Convert.ToBoolean(row["status"]),
                        AdminUser = new User
                        {
                            User_ID = Convert.ToInt64(row["User_ID"]),
                            FirstName = Convert.ToString(row["FirstName"]),
                            LastName = Convert.ToString(row["LastName"]),
                            UserName = Convert.ToString(row["UserName"]),
                            CompanyName = Convert.ToString(row["CompanyName"]),
                            Email = Convert.ToString(row["Email"]),                           
                            CreatedBy_ID = Convert.ToInt64(row["CreatedBy"]),
                            MobileNumber = Convert.ToString(row["MobileNumber"]),
                            PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                            Role = new Role
                            {
                                Role_ID = Convert.ToByte(row["Role_ID"]),
                                Description = Convert.ToString(row["Role"])
                            }
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
                return franchiseList;
            }
            return null;
        }

        public IEnumerable<string> AutoList(string search)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Franchise_List", new object[] { "@Search_By" }, new object[] { search });

            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> FranchiseNames = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    FranchiseNames.Add(Convert.ToString(row["FraCompName"]));
                    FranchiseNames.Add(Convert.ToString(row["UserName"]));
                   
                }
                return FranchiseNames;
            }
            return null;
        }

        public IEnumerable<Franchise> List()
        {
            //Franchise/Index?view=Active  
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_Franchise_List");
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Franchise> FranchiseList = new List<Franchise>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    FranchiseList.Add(new Franchise
                    {
                        CreatedBy_ID = Convert.ToInt64(row["CreatedBy"]),
                        Franchise_ID = Convert.ToInt64(row["Franchise_ID"]),
                        FraCompName = Convert.ToString(row["FraCompName"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        FranchiseStatus = Convert.ToBoolean(row["status"]),
                        AdminUser = new User
                        {
                            User_ID = Convert.ToInt64(row["User_ID"]),
                            FirstName = Convert.ToString(row["FirstName"]),
                            LastName = Convert.ToString(row["LastName"]),
                            UserName = Convert.ToString(row["UserName"]),
                            CompanyName = Convert.ToString(row["CompanyName"]),
                            Email = Convert.ToString(row["Email"]),
                            Password = Convert.ToString(row["Password"]),
                            MobileNumber = Convert.ToString(row["MobileNumber"]),
                            PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                            CreatedBy_ID = Convert.ToInt64(row["CreatedBy"]),
                            Role = new Role
                            {
                                Role_ID = Convert.ToByte(row["Role_ID"]),
                                Description = Convert.ToString(row["Role"])
                            }
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
                return FranchiseList;
            }

            return null;
        }

        public IEnumerable<Franchise> List(string view)
        {
            //Franchise/Index?view=Active  
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_fetchFranchise_List", new object[] { "@view" }, new object[] { view });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Franchise> FranchiseList = new List<Franchise>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    FranchiseList.Add(new Franchise
                    {
                        CreatedBy_ID = Convert.ToInt64(row["CreatedBy"]),
                        Franchise_ID = Convert.ToInt64(row["Franchise_ID"]),
                        FraCompName = Convert.ToString(row["FraCompName"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        FranchiseStatus = Convert.ToBoolean(row["status"]),
                        AdminUser = new User
                        {
                            User_ID = Convert.ToInt64(row["User_ID"]),
                            FirstName = Convert.ToString(row["FirstName"]),
                            LastName = Convert.ToString(row["LastName"]),
                            UserName = Convert.ToString(row["UserName"]),
                            CompanyName = Convert.ToString(row["CompanyName"]),
                            Email = Convert.ToString(row["Email"]),                           
                            MobileNumber = Convert.ToString(row["MobileNumber"]),
                            PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                            CreatedBy_ID = Convert.ToInt64(row["CreatedBy"]),
                            Role = new Role
                            {
                                Role_ID = Convert.ToByte(row["Role_ID"]),
                                Description = Convert.ToString(row["Role"])
                            }
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
                return FranchiseList;
            }

            return null;
        }


        public Franchise Single(long value)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Franchise_List", new object[] { "@Franchise_ID" }, new object[] { value });

            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataset.Tables[0].Rows[0];
                return (new Franchise
                {
                    CreatedBy_ID = Convert.ToInt64(row["CreatedBy"]),
                    Franchise_ID = Convert.ToInt64(row["Franchise_ID"]),
                    FraCompName = Convert.ToString(row["FraCompName"]),
                    FranchiseStatus = Convert.ToBoolean(row["status"]),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    UploadLogoPath = row["UploadLogoPath"].ToString(),
                    Date = Convert.ToDateTime(row["Date"]),
                    AdminUser = new User
                    {
                        User_ID = Convert.ToInt64(row["User_ID"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        UserName = Convert.ToString(row["UserName"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        Email = Convert.ToString(row["Email"]),
                        Password = Convert.ToString(row["Password"]),
                        MobileNumber = Convert.ToString(row["MobileNumber"]),
                        PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                        CreatedBy_ID = Convert.ToInt64(row["CreatedBy"]),
                        Role = new Role
                        {
                            Role_ID = Convert.ToByte(row["Role_ID"]),
                            Description = Convert.ToString(row["Role"])
                        }
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
            else
                return null;
        }


        public IEnumerable<string> AutoList(string search,long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Project_FranchiseWise_SearchName", new object[] { "@Search_By", "@User_ID" }, new object[] { search, User_ID });

            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> ProjectNames = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ProjectNames.Add(Convert.ToString(row["Name"]));
                }
                return ProjectNames;
            }
            return null;
        }

        public IEnumerable<Submission> SubmissionList(long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_GetFranchiseSubmission_List", new object[] { "@User_ID" }, new object[] { User_ID });
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
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"])
                    });
                }
                return SubmissionList;
            }
            return null;
        }

        public IEnumerable<Submission> SubmissionList(string Search_By,long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_GetFranchiseSubmission_List", new object[] { "@User_ID", "@Search_By" }, new object[] { User_ID, Search_By });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        ClientName = Convert.ToString(row["ClientName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        path = Convert.ToString(row["path"]),
                    });
                }
                return SubmissionList;
            }
            return null;
        }

        public IEnumerable<Submission> FranchiseSubmissionList(string Search_By, long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_GetFranchiseSubmission_List", new object[] { "@User_ID", "@Search_By" }, new object[] { User_ID, Search_By });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        ClientName = Convert.ToString(row["ClientName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        path = Convert.ToString(row["path"]),
                    });
                }
                return SubmissionList;
            }
            return null;
        }

        public IEnumerable<Submission> FranchiseSubmissionList(long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_GetFranchiseSubmission_List", new object[] { "@User_ID" }, new object[] { User_ID });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        ClientName = Convert.ToString(row["ClientName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        path = Convert.ToString(row["path"]),
                    });
                }
                return SubmissionList;
            }
            return null;
        }

        public static long? FranchiseID(long? User_ID)
        {
         return (long?)new DAL().ExecuteStoredProcedure("sp_getFranchise_ID",
                 new object[] {"@User_ID"                                
                },
                 new object[] {User_ID
                }, "@Franchise_ID", "0", System.Data.SqlDbType.BigInt);
        }

        public bool UpdateFranchiseStatus(long Franchise_ID)
        {
            return new DAL().Update("sp_ActivateFranchiseStatus",
                  new object[] {"@Franchise_ID"
                },
                  new object[] {Franchise_ID               
                });
        }
        public bool DeActivateFranchiseStatus(long Franchise_ID)
        {
            return new DAL().Update("sp_DeActivateFranchiseStatus",
                  new object[] {"@Franchise_ID"
                },
                  new object[] {Franchise_ID               
                });
        }       
        
    }
}
