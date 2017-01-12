using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public class GeneralInspectionRepository
    {
        public static bool Create(GeneralInspectionModel generalInspectionModel)
        {
            GeneralInspection generalInspection = GetGeneralInspection(generalInspectionModel);
            return true;
        }
        private static GeneralInspection GetGeneralInspection(GeneralInspectionModel generalInspectionModel)
        {
            return new GeneralInspection
            {
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID,
                Client_ID = generalInspectionModel.Client.Client_ID,
                Location_ID = generalInspectionModel.location.Location_ID,
                Tracking_No = generalInspectionModel.Tracking_No,
                Location = generalInspectionModel.locationName,
                Date = generalInspectionModel.Date,
                StartTime = generalInspectionModel.StartTime,
                EndTime = generalInspectionModel.EndTime,
                Inspector_ID = generalInspectionModel.inspector.Inspector_ID,
                InspectorTitle = generalInspectionModel.Inspector_Title,
                InspectorContact = generalInspectionModel.Inspector_Contact,
                InspectorQualification = generalInspectionModel.Inspector_Qualification,
                PhaseValue = generalInspectionModel.PhaseValue,
                InspectionType = generalInspectionModel.Inspection_Type,
                RainEventValue = generalInspectionModel.RainEventValue                 
            };
        }
    }
}