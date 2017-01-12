using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace e2rc.Models.Repository
{
    public class CorrectiveActionRepository
    {
        public static bool Create(CorrectiveActionModel CorrectiveAction)
        {
            CorrectiveActionReport CorrectiveActionReport = GetCorrectiveAction(CorrectiveAction);
            return CorrectiveActionReport.Create();
        }

        public static long? Create(long UploadData_ID, bool isCorrective, long UserID)
        {
            //CorrectiveActionReport objCorrectiveActionReport = new CorrectiveActionReport();
            CorrectiveActionReport.UploadDataID = UploadData_ID;
            CorrectiveActionReport.is_Corrective = isCorrective;
            CorrectiveActionReport.Created_By = Convert.ToInt32(UserID);
            //return CorrectiveActionReport.Create();
            return CorrectiveActionReport.CreateASDraft();
           // return false;
        }
        public static bool Edit(CorrectiveActionModel CorrectiveAction)
        {
            CorrectiveActionReport CorrectiveActionReport = GetCorrectiveAction(CorrectiveAction);
            return CorrectiveActionReport.Edit();
        }

        private static CorrectiveActionReport GetCorrectiveAction(CorrectiveActionModel CorrectiveAction)
        {
            CorrectiveActionReport objCorrectiveActionReport = new CorrectiveActionReport();
          
            DataTable dtProblem = new DataTable();
            DataTable dtStrom = new DataTable();

            dtProblem = ConvertToDataTable(CorrectiveAction.UploadProblemDataModelList);
            dtStrom = ConvertToDataTable(CorrectiveAction.UploadStromDataModelList);
            dtStrom.Columns.Remove("SWPPPRequireYes");
            dtStrom.Columns.Remove("SWPPPRequireNo");

            objCorrectiveActionReport.CorrectiveActionID = CorrectiveAction.CorrectiveActionID;
            objCorrectiveActionReport.Inspection_ID = CorrectiveAction.Inspection_ID;
            objCorrectiveActionReport.CurrentDate = CorrectiveAction.CurrentDate;
            objCorrectiveActionReport.ProblemDiscoveredDate = CorrectiveAction.ProblemDiscoveredDate;
            //objCorrectiveActionReport.TimeDiscovered = CorrectiveAction.TimeDiscovered;
            //objCorrectiveActionReport.lstTriggerCode = CorrectiveAction.lstTriggerCode;
            objCorrectiveActionReport.UploadData_ID = CorrectiveAction.UploadData_ID;
            objCorrectiveActionReport.DescriptionIssue = CorrectiveAction.DescriptionIssue;
            objCorrectiveActionReport.CompletionDeadline = CorrectiveAction.CompletionDeadline;
            objCorrectiveActionReport.CompletionDeadlineNote = CorrectiveAction.CompletionDeadlineNote;
            objCorrectiveActionReport.dtProblem = dtProblem;
            objCorrectiveActionReport.dtStrom = dtStrom;
            objCorrectiveActionReport.lstTriggerCode = CorrectiveAction.lstTriggerCode != null ? string.Join(",", CorrectiveAction.lstTriggerCode) : string.Empty;
            objCorrectiveActionReport.ISComplete = CorrectiveAction.IsComplete;
            objCorrectiveActionReport.isCorrective = CorrectiveAction.isCorrective;
            objCorrectiveActionReport.CreatedBy = CorrectiveAction.CreatedBy;

           /* if (CorrectiveAction.FileName != string.Empty)
            {
                CorrectiveAction.SaveSign();

                string fileName = System.IO.Path.GetFileNameWithoutExtension(CorrectiveAction.FileName).ToString();
                string fileExt = System.IO.Path.GetExtension(CorrectiveAction.FileName).ToString();

                CorrectiveAction.UploadSignPath = "/Client/Logo/" + fileName + "_" + CorrectiveAction.CompanyName + fileExt;
            }*/

            return objCorrectiveActionReport;
        }

        public static dynamic GetInpectionReportDetails(long Inspection_ID)
        {
            return new CorrectiveActionReport().InpectionReportDetails(Inspection_ID);
        }

        public static CorrectiveActionModel Single(long? Inspection_ID)
        {
            CorrectiveActionReport objCorrectiveActionReport = new CorrectiveActionReport().InpectionReportDetails(Inspection_ID);
            return GetInspectionDetails(objCorrectiveActionReport);
        }

        /* This Method Populate information from Inspectiomn Report */
        private static CorrectiveActionModel GetInspectionDetails(CorrectiveActionReport objCorrectiveActionReport)
        {
            CorrectiveActionModel objCorrectiveActionModel = new CorrectiveActionModel();

            objCorrectiveActionModel.Inspection_ID = objCorrectiveActionReport.Inspection_ID;
            objCorrectiveActionModel.Client_ID = objCorrectiveActionReport.Client_ID;
            objCorrectiveActionModel.CompanyName = objCorrectiveActionReport.CompanyName;
            objCorrectiveActionModel.Location_ID = objCorrectiveActionReport.Location_ID;
            objCorrectiveActionModel.ProjectName = objCorrectiveActionReport.ProjectName;
            objCorrectiveActionModel.ProblemDiscoveredDate = Convert.ToDateTime(objCorrectiveActionReport.CurrentDate);
            objCorrectiveActionModel.CurrentDate = DateTime.Now.ToString("MM/dd/yyyy");
            objCorrectiveActionModel.TimeDiscovered = objCorrectiveActionReport.TimeDiscovered;

            return objCorrectiveActionModel;
        }

        public static CorrectiveActionModel SingleDetails(long? UploadData_ID)
        {
            CorrectiveActionReport objCorrectiveActionReport = new CorrectiveActionReport().CorrectiveActionReportDetails(UploadData_ID);
          
            return CorrectiveActionReportDetails(objCorrectiveActionReport);
        }

        /* This Method Populate information from Inspectiomn Report */
        private static CorrectiveActionModel CorrectiveActionReportDetails(CorrectiveActionReport objCorrectiveActionReport)
        {
            CorrectiveActionModel objCorrectiveActionModel = new CorrectiveActionModel();
            if (objCorrectiveActionReport != null)
            {
                objCorrectiveActionModel.Inspection_ID = objCorrectiveActionReport.Inspection_ID;
                objCorrectiveActionModel.Client_ID = objCorrectiveActionReport.Client_ID;
                objCorrectiveActionModel.CompanyName = objCorrectiveActionReport.CompanyName;
                objCorrectiveActionModel.Location_ID = objCorrectiveActionReport.Location_ID;
                objCorrectiveActionModel.UploadData_ID = objCorrectiveActionReport.UploadData_ID;
                objCorrectiveActionModel.ProjectName = objCorrectiveActionReport.ProjectName;
                objCorrectiveActionModel.DescriptionIssue = objCorrectiveActionReport.DescriptionIssue;
                objCorrectiveActionModel.CompletionDeadline = objCorrectiveActionReport.CompletionDeadline;
                objCorrectiveActionModel.CompletionDeadlineNote = objCorrectiveActionReport.CompletionDeadlineNote;
                objCorrectiveActionModel.IsComplete = objCorrectiveActionReport.ISComplete;
                objCorrectiveActionModel.isCorrective = objCorrectiveActionReport.isCorrective;
                objCorrectiveActionModel.ProblemDiscoveredDate = Convert.ToDateTime(objCorrectiveActionReport.CurrentDate);
                objCorrectiveActionModel.CurrentDate = DateTime.Now.ToString("MM/dd/yyyy");
                objCorrectiveActionModel.TimeDiscovered = objCorrectiveActionReport.TimeDiscovered;
                objCorrectiveActionModel.TriggerCode = objCorrectiveActionReport.lstTriggerCode;
                

                for (int i = 0; i < objCorrectiveActionReport.dtProblem.Rows.Count; i++)
                {
                    DataRow row = objCorrectiveActionReport.dtProblem.Rows[i];
                    objCorrectiveActionModel.UploadProblemDataModelList.Add(new ProblemInfo
                    {
                        ProblemID = Convert.ToInt32(row["ProblemID"]),
                        ProblemCause = Convert.ToString(row["ProblemCause"]),
                        ProblemDetermined = Convert.ToString(row["ProblemDetermined"]),
                        ProblemDate = (Convert.ToDateTime(row["PrombleDate"]))
                    });
                }

                
                for (int i = 0; i < objCorrectiveActionReport.dtStrom.Rows.Count; i++)
                {
                    DataRow StormRow = objCorrectiveActionReport.dtStrom.Rows[i];
                    objCorrectiveActionModel.UploadStromDataModelList.Add(new StromWaterControl
                    {
                        StromID = Convert.ToInt32(StormRow["StromID"]),
                        StromModifiedText = Convert.ToString(StormRow["StromModifiedText"]),
                        CompletedDate = (Convert.ToDateTime(StormRow["CompletedDate"])),
                        SWPPUpdateDate = (Convert.ToDateTime(StormRow["SWPPUpdateRequire"])),
                        Notes = Convert.ToString(StormRow["Notes"]),
                    });
                }
            }
            return objCorrectiveActionModel;
        }
        public static IEnumerable<dynamic> Queryget()
        {
            return new CorrectiveActionReport().Queryget();
        }

        #region "General Function"
        /*Convert List to DataTabel*/
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        #endregion
    }
}