using e2rcModel.Common;
using e2rcModel.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace e2rcModel.BusinessLayer
{
    public class CorrectiveActionReport
    {
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public int CreatedBy { get; set; }
        public string Inspector_Name { get; set; }
        public Int64 Inspection_ID { get; set; }
        public Int64 Inspector_ID { get; set; }
        public string ProjectName { get; set; }
        public string CurrentDate { get; set; }
        public DateTime ProblemDiscoveredDate { get; set; }
        public string TimeDiscovered { get; set; }
        public string DescriptionIssue { get; set; }
        public DateTime CompletionDeadline { get; set; }
        public string CompletionDeadlineNote { get; set; }
        public string lstTriggerCode { get; set; }
        public long? Client_ID { get; set; }
        public long? Location_ID { get; set; }
        public long? UploadData_ID { get; set; }
        public bool isCorrective { get; set; }
        //public string CreatedDate { get; set; }
        //public string TimeDiscovered { get; set; }

        public List<ProblemInfo> UploadProblemDataList { get; set; }
        public List<StromWaterControl> UploadStromDataList { get; set; }
        public string UploadSignPath { get; set; }
        public long CorrectiveActionID { get; set; }

        public static long UploadDataID { get; set; }
        public static int Created_By { get; set; }
        public static bool is_Corrective { get; set; }

        public bool SWPPPRequireYes { get; set; }
        public bool SWPPPRequireNo { get; set; }
        public bool ISComplete { get; set; }
        public DataTable dtProblem { get; set; }
        public DataTable dtStrom { get; set; }
    
        public bool Create()
        {
            return new DAL().Insert("uspCorrectiveActionReportCRUD",
                new object[] {
                                "@Action", "@Inspection_ID", "@User_ID", "@CurrentDate", "@ProblemDiscoveredDate", "@DescriptionIssue", "@CompletionDeadline", "@CompletionDeadlineNote", "@TriggerEventCode", "@IsSaveFinal", "@dtProblemCA", "@dtStromCA", "@UploadData_ID", "@CreatedBy", "@isCorrective"
                              },

                  new object[] {
                                Actions.INSERT.ToString(), Inspection_ID, CreatedBy, CurrentDate, ProblemDiscoveredDate, DescriptionIssue, CompletionDeadline, CompletionDeadlineNote, lstTriggerCode, ISComplete, dtProblem, dtStrom, UploadData_ID, 0, CreatedBy, isCorrective
                            });
        }

        public static long? CreateASDraft()
        {
            return (long?)new DAL().ExecuteStoredProcedure("uspCorrectiveActionDraftReport",
                new object[] {
                                "@Action", "@UploadData_ID", "@User_ID", "@isCorrective","@IsSaveFinal"
                             },

                  new object[] {
                                Actions.INSERT.ToString(), UploadDataID, Created_By, is_Corrective, 0
                            }, "@returnCorrectiveActionID", "0", System.Data.SqlDbType.BigInt);
        }
       
        public bool Edit()
        {
            return new DAL().Update("uspCorrectiveActionReportCRUD",
                new object[] {
                                "@Action", "@CorrectiveActionID", "@Inspection_ID", "@User_ID", "@CurrentDate", "@ProblemDiscoveredDate", "@DescriptionIssue", "@CompletionDeadline", "@CompletionDeadlineNote", "@TriggerEventCode", "@IsSaveFinal", "@dtProblemCA", "@dtStromCA", "@UploadData_ID", "@CreatedBy", "@isCorrective"
                             },

                new object[] {
                                Actions.UPDATE.ToString(), CorrectiveActionID, Inspection_ID, CreatedBy, CurrentDate, ProblemDiscoveredDate, DescriptionIssue, CompletionDeadline, CompletionDeadlineNote, lstTriggerCode, ISComplete, dtProblem, dtStrom, UploadData_ID, CreatedBy, isCorrective
                            });
        }
        public CorrectiveActionReport InpectionReportDetails(long? Inspection_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("uspGetInspectionDetail", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataset.Tables[0].Rows[0];
                return (new CorrectiveActionReport
                {
                    Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                    Client_ID = Convert.ToInt64(row["Client_ID"]),
                    CompanyName = Convert.ToString(row["CompanyName"]),
                    Location_ID = Convert.ToInt64(row["Location_ID"]),
                    ProjectName = Convert.ToString(row["ProjectName"]),
                    CurrentDate = (Convert.ToDateTime(row["CreatedDate"])).ToString("MM/dd/yyyy"),
                    TimeDiscovered = (Convert.ToDateTime(row["CreatedDate"])).ToString("hh:mm:tt")
                });
            }
            else
                return null;
        }

        public CorrectiveActionReport CorrectiveActionReportDetails(long? UploadData_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("uspGetCorrectiveActionDetail", new object[] { "@UploadData_ID" }, new object[] { UploadData_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataset.Tables[0].Rows[0];
                return (new CorrectiveActionReport
                {
                    CreatedBy = Convert.ToInt32(row["User_ID"]),
                    Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                    CompanyName = Convert.ToString(row["CompanyName"]),
                    ProjectName = Convert.ToString(row["ProjectName"]),
                    ProblemDiscoveredDate = (Convert.ToDateTime(row["ProblemDiscoveredDate"])),
                    CompletionDeadline = (Convert.ToDateTime(row["CompletionDeadline"])),
                    CompletionDeadlineNote = Convert.ToString(row["CompletionDeadlineNote"]),
                    isCorrective = (Convert.ToBoolean(row["isCorrective"])),
                    ISComplete = (Convert.ToBoolean(row["IsSaveFinal"])),
                    DescriptionIssue = Convert.ToString(row["DescriptionIssue"]),
                    CurrentDate = (Convert.ToDateTime(row["Created"])).ToString("MM/dd/yyyy"),
                    TimeDiscovered = (Convert.ToDateTime(row["Created"])).ToString("hh:mm:tt"),
                    lstTriggerCode = Convert.ToString(row["TriggerCode"]),
                    dtProblem = dataset.Tables[1],
                    dtStrom = dataset.Tables[2],
                });
            }
            //if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[1].Rows.Count > 0)
            //{
            //    DataRow row = dataset.Tables[1].Rows[0];
            //    for(int i=0; i<dataset.Tables[1].Rows.Count; i++)
            //    { 
            //        UploadProblemDataList.Add(new ProblemInfo {
            //            ProblemCause = Convert.ToString(row["ProblemCause"]),
            //            ProblemDetermined = Convert.ToString(row["ProblemDetermined"]),
            //            ProblemDate = (Convert.ToDateTime(row["PrombleDate"]))
            //        });
            //    }
            //}
            //if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[2].Rows.Count > 0)
            //{
            //    DataRow row = dataset.Tables[2].Rows[0];
            //    for (int i = 0; i < dataset.Tables[2].Rows.Count; i++)
            //    {
            //        UploadStromDataList.Add(new StromWaterControl
            //        {
            //            StromModifiedText = Convert.ToString(row["StromModifiedText"]),
            //            CompletedDate = (Convert.ToDateTime(row["CompletedDate"])),
            //            SWPPUpdateDate = (Convert.ToDateTime(row["SWPPUpdateRequire"])),
            //            Notes = Convert.ToString(row["Notes"]),
            //        });
            //    }
            //}
            else
                return null;
        }

        public IEnumerable<TriggerCode> Queryget()
        {
            List<TriggerCode> TriggerCode = new List<TriggerCode>();
            DataSet dataSet = new DAL().ExecuteStoredProcedure("usp_query_get", new object[] { "@query", "@param1" }, new object[] { "code", 2 });

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Row in dataSet.Tables[0].Rows)
                {
                    TriggerCode.Add(new TriggerCode
                    {
                        Code_ID = Convert.ToInt32(Row["CodeId"]),
                        Description = Convert.ToString(Row["description"])
                    });
                }
                return TriggerCode;
            }
            else
            {
                TriggerCode.Add(new TriggerCode
                {
                    Code_ID = 0,
                    Description = ""
                });
                return TriggerCode;
            }
        }
    }


    public class ProblemInfo
    {
        public string ProblemCause { get; set; }
        public string ProblemDetermined { get; set; }
        public DateTime ProblemDate { get; set; }
    }
    public class StromWaterControl
    {
        public string StromModifiedText { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime SWPPUpdateDate { get; set; }
        public string Notes { get; set; }
    }
    public class TriggerCode
    {
        public int Code_ID { get; set; }
        public string Description { get; set; }
    }
}
