using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.DataAccessLayer;
using e2rcModel.Common;
using System.Data;

namespace e2rcModel.BusinessLayer
{
    public class Executor : User
    {
        public DateTime Date { get; set; }

        public long? Executor_ID { get; set; }

        public long? User_ID { get; set; }
        public bool IsActive { get; set; }

        public Address Address { get; set; }

        public override bool Create()
        {
            return new DAL().Insert("sp_Executor_CRUD",
                 new object[] {"@Action", "@Date","@FirstName","@LastName", "@UserName","@Email","@Role_ID","@Password",
                                "@MobileNumber","@PhoneNumber","@City","@State_ID","@MailingAddress","@ZipCode","@CreatedBy","@IsActive"
                },
                 new object[] {Actions.INSERT.ToString(),Date,FirstName,LastName,UserName,Email,Role.Role_ID,Password, MobileNumber,PhoneNumber,
                               Address.City,Address.State.State_ID,Address.MailingAddress,Address.ZipCode,CreatedBy_ID ,IsActive                   
                });
        }

        public override bool Edit()
        {
            return new DAL().Update("sp_Executor_CRUD",
                new object[] {"@Action", "@Date","@FirstName","@LastName" ,"@UserName","@Email","@Role_ID","@Password",
                                "@MobileNumber","@PhoneNumber","@City","@State_ID","@MailingAddress","@ZipCode","@CreatedBy","@Executor_ID","@IsActive"
                },
                new object[] {Actions.UPDATE.ToString(),Date,FirstName,LastName,UserName,Email,Role.Role_ID,Password,
                              MobileNumber,PhoneNumber,Address.City,Address.State.State_ID,Address.MailingAddress,Address.ZipCode,CreatedBy_ID,
                              Executor_ID,IsActive                  
                });
        }

        public override bool Delete()
        {
            return new DAL().Delete("sp_Executor_CRUD",
                                              new object[] { "@Action", "@Executor_ID", "@CreatedBy" },
                                              new object[] { Actions.DELETE.ToString(), Executor_ID, CreatedBy_ID });
        }
        public IEnumerable<User> List(long CreatedBy_ID,string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Executor_List", new object[] { "@CreatedBy", "@view" }, new object[] { CreatedBy_ID, view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Executor> ExecutiveList = new List<Executor>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ExecutiveList.Add(new Executor
                    {
                        Executor_ID = Convert.ToInt64(row["Executor_ID"]),
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
                return ExecutiveList;
            }
            return null;
        }

        public IEnumerable<Executor> List(string search, long CreatedBy_ID,string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Executor_List",
                                                                new object[] { "@Search_By", "@CreatedBy","@view" },
                                                                new object[] { search, CreatedBy_ID,view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Executor> ExecutiveList = new List<Executor>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ExecutiveList.Add(new Executor
                    {
                        Executor_ID = Convert.ToInt64(row["Executor_ID"]),
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
                return ExecutiveList;
            }
            return null;
        }

        public Executor Single(long Executor_ID, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getExecutor_List",
                                                               new object[] { "@Executor_ID", "@CreatedBy" },
                                                               new object[] { Executor_ID, CreatedBy_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                return (new Executor
                {
                    Executor_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Executor_ID"]),
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

        public bool UpdateExecutorStatus(long Executor_ID, string view)
        {
            return new DAL().Update("sp_UpdateExecutorStatus",
                  new object[] {"@Executor_ID","@view"
                },
                  new object[] {Executor_ID ,view              
                });
        }

        public IEnumerable<string> AutoList(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Executor_SearchName",
                                                                new object[] { "@Search_By", "@CreatedBy" },
                                                                new object[] { search, CreatedBy_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> ExecutorName = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ExecutorName.Add(Convert.ToString(row["FirstName"]));
                }
                return ExecutorName;
            }
            return null;
        }

        public static long? ExecutorID(long? User_ID)
        {
            return (long?)new DAL().ExecuteStoredProcedure("sp_getExecutor_ID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@Executor_ID", "0", System.Data.SqlDbType.BigInt);
        }

        public static long getExecutor_UserID(long User_ID)
        {
            return (long)new DAL().ExecuteStoredProcedure("sp_getExecutor_UserID",
                    new object[] {"@User_ID"                                
                },
                    new object[] {User_ID
                }, "@ExecutorUser_ID", "0", System.Data.SqlDbType.BigInt);
        }
    }
}
