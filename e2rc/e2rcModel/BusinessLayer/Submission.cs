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

namespace e2rcModel.BusinessLayer
{
    //public class Submission : ICRUD<Submission, long>
      public class Submission 
    {
        public long? Inspection_ID { get; set; }
        public string FormName { get; set; }
        public string path { get; set; }
        public string WorkOrder { get; set; }
        public string ProjectName { get; set; }
        public string location { get; set; }
        public string InspectorName { get; set; }
        public int Pastdays { get; set; }        
        public string ClientName { get; set; }
        public string CompanyName { get; set; }
        public DateTime Date { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsComplete { get; set; }
        public bool IsAutoresponder { get; set; }

        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Edit()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public Submission Single(long value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Submission> List(long User_ID)       
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_getSubmisionList", new object[] { "@User_ID"}, new object[] { User_ID}); 
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        ClientName = Convert.ToString(row["Client"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location=Convert.ToString(row["Location"]),
                        Date=Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        IsComplete = Convert.ToBoolean(row["IsComplete"]) ,
                        path = Convert.ToString(row["path"])
                    });
                }
                return SubmissionList;
            }
            
            return null;
        }

        public IEnumerable<Submission> List(string Search_By, long User_ID)      
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_getSubmisionList", new object[] { "@User_ID", "@Search_By" }, new object[] { User_ID, Search_By });          
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        ClientName = Convert.ToString(row["Client"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
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

        public IEnumerable<string> AutoList(string search)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Project_SearchName", new object[] { "@Search_By" }, new object[] { search });

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

        public IEnumerable<Submission> DisplayProjectWiseInspection(long Location_ID, long Client_ID,long User_ID,string Role)
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_DisplayProjectWiseInspection", new object[] { "@Location_ID", "@Client_ID", "@User_ID", "@Role" }, new object[] { Location_ID, Client_ID, User_ID, Role });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<Submission> SubmissionList = new List<Submission>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SubmissionList.Add(new Submission
                    {
                        FormName = Convert.ToString(row["FormName"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        location = Convert.ToString(row["Location"]),
                        Date = Convert.ToDateTime(row["dt"]),
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        IsComplete = Convert.ToBoolean(row["IsComplete"]),
                        path = Convert.ToString(row["path"]),
                        ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]),
                        WorkOrder = Convert.ToString(row["workorder"]),
                        Pastdays =Convert.ToString(row["workorder"])=="Complete" ? 0 : Convert.ToInt16(row["DaysPastDue"]) < 0 ? 0 : Convert.ToInt16(row["DaysPastDue"])
                    });
                }
                return SubmissionList;
            }

            return null;
        }


        public bool getReviewerAllowWorkOrder(long? User_ID)
        {
            object IsAvailable = new DataAccessLayer.DAL().ExecuteScalar("sp_CheckReviewerAllowToCloseWO",
                new object[] { "@User_ID" }, new object[] {User_ID });
            return (int)IsAvailable == 1 ? true : false;
        }

    }
}

