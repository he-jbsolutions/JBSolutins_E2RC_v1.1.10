using System;
using System.Data;
using e2rcModel.BusinessLayer.Interface;
using System.Collections.Generic;
using System.Configuration;

namespace e2rcModel.BusinessLayer
{
    public class User : ICRUD<User, long>
    {
        public User()
        { }
        public User(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }

        /// <summary>
        /// Get the intger Users ID.
        /// </summary>
        public long? User_ID { get; set; }

        public string Name { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string LogoPath { get; set; }

        /// <summary>
        /// Get or Set String Name of the user.
        /// </summary>
        /// 

        public string UserName { get; set; }

        /// <summary>
        /// Get or Set  Password of the user in  encripted format specified in enum PasswordFormat.
        /// </summary>
        public string Password { get; set; }


        public string CompanyName { get; set; }

        public string Email { get; set; }

        //public string Role { get; set; }

        public string MobileNumber { get; set; }


        public string PhoneNumber { get; set; }

        public string Qualification { get; set; }

        public string sLocationID { get; set; }

        public Role Role { get; set; }

        public long? CreatedBy_ID { get; set; }

        /// <summary>
        /// Check user is existing or not. 
        /// if existing then sets it ID and return true else return false.
        /// </summary>
        /// <returns>bool</returns>
        public bool Authenticate()
        {
            DataSet dataset = new DataAccessLayer.DAL().ExecuteStoredProcedure("sp_Login_UserExist_S", new object[] { "@UserName", "@Password" },
                new object[] { UserName, Password });

            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                this.User_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["User_ID"]);
                this.Name = Convert.ToString(dataset.Tables[0].Rows[0]["Name"]);
                this.UserName = Convert.ToString(dataset.Tables[0].Rows[0]["UserName"]);
                this.Email = Convert.ToString(dataset.Tables[0].Rows[0]["Email"]);
                this.CompanyName = Convert.ToString(dataset.Tables[0].Rows[0]["CompanyName"]);
                this.Role = new Role() { Description = Convert.ToString(dataset.Tables[0].Rows[0]["Role"]) };
           
                return true;
            }
            return false;
        }

        public virtual bool Create()
        {
            throw new NotImplementedException();
        }

        public virtual bool RemoveProjectAccess()
        {
            throw new NotImplementedException();
        }

        public virtual bool Edit()
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete()
        {
            throw new NotImplementedException();
        }

        public virtual void Single()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<User> List()
        {
            throw new NotImplementedException();
        }

        public virtual User Single(long value)
        {
            throw new NotImplementedException();
        }

        public string GetCompanyLogoInfo(long UserID)
        {
             DataSet dataset = new DataAccessLayer.DAL().ExecuteStoredProcedure("sp_Company_Logo_Image", new object[] { "@UserId" },
                new object[] { UserID });

            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                this.LogoPath = Convert.ToString(dataset.Tables[0].Rows[0]["LogoPath"]);
            }
            return (!String.IsNullOrEmpty(LogoPath) ? this.LogoPath :  string.Empty);
        }

        public bool IsUserNameAvailable(string UserName, long? User_ID)
        {
            object IsAvailable = new DataAccessLayer.DAL().ExecuteScalar("sp_User_IsUserNameAvailable",
                new object[] { "@UserName", "@User_ID" }, new object[] { UserName, User_ID });
            return (int)IsAvailable == 1 ? false : true;
        }
    }
}
