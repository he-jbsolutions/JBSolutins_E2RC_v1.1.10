using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e2rc.Models
{
    public class CorrectiveActionModel
    {
        public CorrectiveActionModel()
        {
            UploadProblemDataModelList = new List<ProblemInfo>();
            UploadStromDataModelList = new List<StromWaterControl>();
        }

        public Int64 Code_ID { get; set; }
        public string Description { get; set; }
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
        public string[] lstTriggerCode { get; set; }
        public string TriggerCode { get; set; }
        public bool IsComplete { get; set; }
        public ProblemInfo Problem1 { get; set; }
        public StromWaterControl StromControl1 { get; set; }
        public long? Client_ID { get; set; }
        public long? Location_ID { get; set; }
        public long? UploadData_ID { get; set; }
        public bool isCorrective { get; set; }
        public long CorrectiveActionID { get; set; }

        public List<ProblemInfo> UploadProblemDataModelList { get; set; }
        public List<StromWaterControl> UploadStromDataModelList { get; set; }
        

        public string UploadSignPath { get; set; }
       /* //[Required(ErrorMessage = "Signature Image Required.")]
        [RegularExpression(@"^.*\.(png|PNG|JPE?G|jpe?g|bmp|BMP)$", ErrorMessage = "Please upload PNG,JPEG,BMP Image only.")]
        //[RegularExpression(@"^.*\.(png)$", ErrorMessage = "Please Upload PNG Image Only.")]
        public HttpPostedFileBase PostedFile { get; set; }

        public string FileName
        {
            get
            {
                if (PostedFile != null)
                    return PostedFile.FileName;
                else
                    return String.Empty;
            }
        }
        public bool SaveSign()
        {

            UploadSignPath = PostedFile.FileName;
            PostedFile.SaveAs(HttpContext.Current.Server.MapPath("/Inspection/Signature/") + "//" + PostedFile.FileName);
            return true;
        }*/
    }

    public class ProblemInfo
    {
        public int ProblemID { get; set; }
        public string ProblemCause { get; set; }
        public string ProblemDetermined { get; set; }
        public DateTime ProblemDate { get; set; }
    }

    public class StromWaterControl
    {
        public int StromID { get; set; }
        public string StromModifiedText { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime SWPPUpdateDate { get; set; }
        public string Notes { get; set; }
        public bool SWPPPRequireYes { get; set; }
        public bool SWPPPRequireNo { get; set; }
    }

    public class TriggerCode
    {
        public int Code_ID { get; set; }
        public string Description { get; set; }
    }

}