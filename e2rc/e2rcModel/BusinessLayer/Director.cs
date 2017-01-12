using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.DataAccessLayer;
using e2rcModel.Common;
using System.Data;

namespace e2rcModel.BusinessLayer
{
    public class Director : User
    {
        public DateTime Date { get; set; }

        public long? Director_ID { get; set; }

        public long? User_ID { get; set; }

        public Address Address { get; set; }
        public bool IsActive { get; set; }

        public override bool Create()
        {
            return new DAL().Insert("sp_Director_CRUD",
                 new object[] {"@Action", "@Date","@FirstName","@LastName", "@UserName","@Email","@Role_ID","@Password",
                                "@MobileNumber","@PhoneNumber","@City","@State_ID","@MailingAddress","@ZipCode","@CreatedBy","@IsActive"
                },
                 new object[] {Actions.INSERT.ToString(),Date,FirstName,LastName,UserName,Email,Role.Role_ID,Password, MobileNumber,PhoneNumber,
                               Address.City,Address.State.State_ID,Address.MailingAddress,Address.ZipCode,CreatedBy_ID,IsActive                    
                });
        }

        public override bool Edit()
        {
            return new DAL().Update("sp_Director_CRUD",
                new object[] {"@Action", "@Date","@FirstName","@LastName" ,"@UserName","@Email","@Role_ID","@Password",
                                "@MobileNumber","@PhoneNumber","@City","@State_ID","@MailingAddress","@ZipCode","@CreatedBy","@Director_ID","@IsActive"
                },
                new object[] {Actions.UPDATE.ToString(),Date,FirstName,LastName,UserName,Email,Role.Role_ID,Password,
                              MobileNumber,PhoneNumber,Address.City,Address.State.State_ID,Address.MailingAddress,Address.ZipCode,CreatedBy_ID,
                              Director_ID,IsActive                    
                });
        }

        public override bool Delete()
        {
            return new DAL().Delete("sp_Director_CRUD",
                                              new object[] { "@Action", "@Director_ID", "@CreatedBy" },
                                              new object[] { Actions.DELETE.ToString(), Director_ID, CreatedBy_ID });
        }

        public IEnumerable<User> List(long CreatedBy_ID,string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Director_List", new object[] { "@CreatedBy", "@view" }, new object[] { CreatedBy_ID,view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Director> DirectorList = new List<Director>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    DirectorList.Add(new Director
                    {
                        Director_ID = Convert.ToInt64(row["Director_ID"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        UserName = Convert.ToString(row["UserName"]),
                        Email = Convert.ToString(row["Email"]),
                        Date = Convert.ToDateTime(row["Date"]),                       
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
                return DirectorList;
            }
            return null;
        }

        public IEnumerable<Director> List(string search, long CreatedBy_ID,string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Director_List",
                                                                new object[] { "@Search_By", "@CreatedBy", "@view" },
                                                                new object[] { search, CreatedBy_ID,view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Director> DirectorList = new List<Director>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    DirectorList.Add(new Director
                    {
                        Director_ID = Convert.ToInt64(row["Director_ID"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        UserName = Convert.ToString(row["UserName"]),                        
                        Email = Convert.ToString(row["Email"]),
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
                        },
                        User_ID = Convert.ToInt64(row["User_ID"])
                    });
                }
                return DirectorList;
            }
            return null;
        }

        public Director Single(long Director_ID, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getDirector_List",
                                                               new object[] { "@Director_ID", "@CreatedBy" },
                                                               new object[] { Director_ID, CreatedBy_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                return (new Director
                {
                    Director_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Director_ID"]),
                    FirstName = Convert.ToString(dataset.Tables[0].Rows[0]["FirstName"]),
                    LastName = Convert.ToString(dataset.Tables[0].Rows[0]["LastName"]),
                    Date = Convert.ToDateTime(dataset.Tables[0].Rows[0]["Date"]),
                    UserName = Convert.ToString(dataset.Tables[0].Rows[0]["UserName"]),
                    Password = Convert.ToString(dataset.Tables[0].Rows[0]["Password"]),
                    Email = Convert.ToString(dataset.Tables[0].Rows[0]["Email"]),
                    MobileNumber = Convert.ToString(dataset.Tables[0].Rows[0]["MobileNumber"]),
                    PhoneNumber = Convert.ToString(dataset.Tables[0].Rows[0]["PhoneNumber"]),
                    IsActive = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsActive"]),
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

        
        public IEnumerable<Submission> SubmissionList(string Search_By, long User_ID)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_getDirectorSubmisionList", new object[] { "@User_ID", "@Search_By" }, new object[] { User_ID, Search_By });
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


        public bool UpdateDirectorStatus(long Director_ID,string view)
        {
            return new DAL().Update("sp_UpdateDirectorStatus",
                  new object[] {"@Director_ID","@view"
                },
                  new object[] {Director_ID ,view              
                });
        }

        public IEnumerable<string> AutoList(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Director_SearchName",
                                                                new object[] { "@Search_By", "@CreatedBy" },
                                                                new object[] { search, CreatedBy_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> DirectorName = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    DirectorName.Add(Convert.ToString(row["FirstName"]));
                }
                return DirectorName;
            }
            return null;
        }

        public static long? DirectorID(long? User_ID)
        {
            return (long?)new DAL().ExecuteStoredProcedure("sp_getDirector_ID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@Director_ID", "0", System.Data.SqlDbType.BigInt);
        }

        public static long getDirector_UserID(long User_ID)
        {
            return (long)new DAL().ExecuteStoredProcedure("sp_getDirector_UserID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@DirectorUser_ID", "0", System.Data.SqlDbType.BigInt);
        }
    }
}
