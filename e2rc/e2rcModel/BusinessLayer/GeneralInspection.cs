using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.Common;
using e2rcModel.DataAccessLayer;
using System.Data;



namespace e2rcModel.BusinessLayer
{
    public class GeneralInspection
    {
        //tblGeneralInspection table
        public long? Client_ID { get; set; }
        public long Inspection_ID { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string InspectorName { get; set; }
        public long Location_ID { get; set; }
       // public string LocationName { get; set; }
        public string Tracking_No { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string StartTime { get; set; }
        //string _StartTime = DateTime.Now.ToString("H:mm");
        //public string StartTime
        //{
        //    get
        //    {
        //        return _StartTime;
        //    }

        //    set
        //    {
        //        _StartTime = value;

        //    }
        //}
        public string EndTime { get; set; }
        public long? Inspector_ID { get; set; }
        public string InspectorTitle { get; set; }
        public string InspectorContact { get; set; }
        public string InspectorQualification { get; set; }
        public string PhaseValue { get; set; }
        public string InspectionType { get; set; }
        public int CodeId { get; set; }
        public int CodeTypeId { get; set; }
        public int ID { get; set; }
        public string Description { get; set; }
        public string RainEventValue { get; set; }
        public string RainEventOtherValue { get; set; }
        public long? CreatedBy_ID { get; set; }
        public bool IsComplete { get; set; }     
        public bool PhaseClear { get; set; }
        public bool PhaseExcavations { get; set; }
        public bool PhaseBuilding { get; set; }
        public bool PhaseRoughGrading { get; set; }
        public bool PhaseInfrastructure { get; set; }
        public bool PhaseFinalGrading { get; set; }
        public bool PhaseFinalStabilization { get; set; }   
        public bool PhaseSitework { get; set; }
        public bool PhaseOther { get; set; }     
        public bool RainEventOther { get; set; }
        public bool RainEvent { get; set; }
        public string UploadSignPath { get; set; }
        public string StartTime_AmPm { get; set; }
        public string EndTime_AmPm { get; set; }
        public string FormName { get; set; }
        public long Create()
        {
            return (long)new DAL().ExecuteStoredProcedure("sp_GeneralInspection_CRUD",
                  new object[] {"@Action", "@Client_ID", "@Location_ID","@Tracking_No","@Location","@Date", "@StartTime","@EndTime","@Inspector_ID",
                                "@InspectorTitle","@InspectorContact","@InspectorQualification","@PhaseValue","@InspectionType","@CodeId",
                                "@RainEventValue","@RainEventOtherValue","@CreatedBy","@IsComplete","@FormName"
                },
                  new object[] {Actions.INSERT.ToString(),Client_ID,Location_ID,Tracking_No,Location,Date,StartTime,EndTime,Inspector_ID,
                                InspectorTitle,InspectorContact,InspectorQualification,PhaseValue,InspectionType,CodeId,
                                RainEventValue,RainEventOtherValue,CreatedBy_ID,IsComplete,FormName                
                }, "@Inspection_ID", "0", System.Data.SqlDbType.BigInt);
        }        
        public bool Edit()
        {
            return new DAL().Update("sp_GeneralInspection_CRUD",
                   new object[] {"@Action", "@Client_ID", "@Location_ID","@Tracking_No","@Location","@Date", "@StartTime","@EndTime","@Inspector_ID",
                                "@InspectorTitle","@InspectorContact","@InspectorQualification","@PhaseValue","@InspectionType","@CodeId",
                                "@RainEventValue","@RainEventOtherValue","@CreatedBy","@IsComplete","@Inspection_ID","@FormName" },
                   new object[] {Actions.UPDATE.ToString(),Client_ID,Location_ID,Tracking_No,Location,Date,StartTime,EndTime,Inspector_ID,
                                InspectorTitle,InspectorContact,InspectorQualification,PhaseValue,InspectionType,CodeId,
                                RainEventValue,RainEventOtherValue,CreatedBy_ID,IsComplete,Inspection_ID,FormName }
                   );
        }

        public bool Autoresponder(long Inspection_ID)
        {
            return new DAL().Update("sp_GeneralInspection_CRUD_AUTORESPONDER",
                    new object[] { "@Inspection_ID" },
                    new object[] { Inspection_ID }
                    );
        }


        public bool WorkOrderCompletedAutoResponder(long Inspection_ID)
        {
            return new DAL().Update("sp_WorkOrderCompletedAutoResponder",
                    new object[] { "@Inspection_ID" },
                    new object[] { Inspection_ID }
                    );
        }

        //  from mail
        public bool setFirstReviewerInfo(long Inspection_ID,long Reviewer_ID ,DateTime dt)
        {
            return new DAL().Update("sp_setFirstReviewerDetails",
                    new object[] { "@Inspection_ID", "@Reviewer_ID", "@dt" },
                    new object[] { Inspection_ID,Reviewer_ID,dt }
                    );
        }

        // set for logged in
        public bool setFirstReviewerLoginInfo(long User_ID, long Inspection_ID, long Location_ID)
        {
            return new DAL().Update("sp_setFirstReviewerLoginDetails",
                    new object[] { "@User_ID", "@Inspection_ID", "@Location_ID" },
                    new object[] { User_ID, Inspection_ID, Location_ID }
                    );
        }


        public bool setFirstReviewerDownloadPDFLoginInfo(long Reviewer_ID, long Inspection_ID, long Location_ID)
        {
            return new DAL().Update("sp_setFirstReviewerDownLoadPDFLoginDetails",
                    new object[] { "@Reviewer_ID", "@Inspection_ID", "@Location_ID" },
                    new object[] { Reviewer_ID, Inspection_ID, Location_ID }
                    );
        }
    }
}
