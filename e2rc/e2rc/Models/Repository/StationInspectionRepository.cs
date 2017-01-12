using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;
using System.Text;
using System.IO;
using System.Data;

namespace e2rc.Models.Repository
{
    public class StationInspectionRepository
    {

        public static IEnumerable<dynamic> Locations(long User_ID)
        {
            return new StationInspection().Locations(User_ID);
        }
        public static long Create(StationInspectionModel StationInspectionModel)
        {
            StringBuilder sb = new StringBuilder();
            if (StationInspectionModel.generalInspectionModel.PhaseClear)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Clear and Grub";
                sb.Append("Clear and Grub,");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseExcavations)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Excavation";
                sb.Append("Excavation,");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseBuilding)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Building (vertical)";
                sb.Append("Building (vertical),");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseRoughGrading)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Rough Grading";
                sb.Append("Rough Grading,");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseInfrastructure)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Infrastructure";
                sb.Append("Infrastructure,");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseFinalGrading)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Final Grading";
                sb.Append("Final Grading,");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseFinalStabilization)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Final Stabilization";
                sb.Append("Final Stabilization");
            }
            if (sb.ToString() != string.Empty)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = sb.ToString();
            }
            else
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = string.Empty;
            }

            if (StationInspectionModel.generalInspectionModel.RainEvent)
            {
                StationInspectionModel.generalInspectionModel.RainEventValue = StationInspectionModel.generalInspectionModel.RainEventOthervalue;
            }
            else
            {
                StationInspectionModel.generalInspectionModel.RainEventValue = null;
            }
            if (StationInspectionModel.generalInspectionModel.RainEventOther)
            {
                StationInspectionModel.generalInspectionModel.RainEventOthervalue = StationInspectionModel.generalInspectionModel.RainEventOthervalue;
            }
            else
            {
                StationInspectionModel.generalInspectionModel.RainEventOthervalue = null;
            }

            if (StationInspectionModel.weatherInspectionModel.StromEventYes)
            {
                StationInspectionModel.weatherInspectionModel.StromEvent = StationInspectionModel.weatherInspectionModel.StromEventYesValue;
            }
            else
            {
                //inspectionModel.weatherInspectionModel.StromEvent = "No";
                StationInspectionModel.weatherInspectionModel.StromEvent = "";
            }
            if (StationInspectionModel.weatherInspectionModel.InspectionOccuringYes)
            {
                StationInspectionModel.weatherInspectionModel.InspectionOccuring = StationInspectionModel.weatherInspectionModel.InspectionOccuringYesValue;
            }
            else
            {
                StationInspectionModel.weatherInspectionModel.InspectionOccuring = "No";
            }
            if (StationInspectionModel.weatherInspectionModel.UnsafeInspectionYes)
            {
                StationInspectionModel.weatherInspectionModel.UnsafeInspection = StationInspectionModel.weatherInspectionModel.UnsafeInspectionValue;
            }
            else
            {
                StationInspectionModel.weatherInspectionModel.UnsafeInspection = "No";
            }
            string controllerName = "StationInspection";
            foreach (var item in StationInspectionModel.weatherInspectionModel.UploadDataModelList)
            {
                if (item.FileName.Trim() != string.Empty)
                {
                    item.SaveFile(controllerName);
                    string FName = item.FileName.Replace(' ', '_');
                    FName = string.Concat(Path.GetFileNameWithoutExtension(FName), DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), Path.GetExtension(FName));
                    item.UploadImagePath = "/UploadedImage/" + FName;
                    // item.UploadImagePath = item.FileName;
                }
            }
            StationInspection StationInspection = GetInspection(StationInspectionModel);
            return StationInspection.Create();
        }
       
        
        public static dynamic GetInspectorDetails(string InspectorName)
        {
            return new StationInspection().InspectorDetails(InspectorName);
        }
        private static StationInspection GetInspection(StationInspectionModel StationInspectionModel)
        {
            StationInspection StationInspection = new StationInspection();
            StationInspection.Date = StationInspectionModel.Date;
            StationInspection.generalInspection = new GeneralInspection
            {               
                CreatedBy_ID = StationInspectionModel.CreatedBy,
                Inspection_ID = StationInspectionModel.Inspection_ID,
                Client_ID = StationInspectionModel.generalInspectionModel.Client.Client_ID,
                Location_ID = StationInspectionModel.generalInspectionModel.location.Location_ID,
                ProjectName = StationInspectionModel.generalInspectionModel.location.Name,
                Tracking_No = StationInspectionModel.generalInspectionModel.Tracking_No,
                Location = StationInspectionModel.generalInspectionModel.locationName,
                Date = StationInspectionModel.generalInspectionModel.Date,
                StartTime = StationInspectionModel.generalInspectionModel.StartTime + StationInspectionModel.generalInspectionModel.StartTime_AmPm,
                EndTime = StationInspectionModel.generalInspectionModel.EndTime + StationInspectionModel.generalInspectionModel.EndTime_AmPm,
                Inspector_ID = StationInspectionModel.generalInspectionModel.inspector.Inspector_ID,
                InspectorName = StationInspectionModel.generalInspectionModel.inspector.Name,
                InspectorTitle = StationInspectionModel.generalInspectionModel.Inspector_Title,
                InspectorContact = StationInspectionModel.generalInspectionModel.Inspector_Contact,
                InspectorQualification = StationInspectionModel.generalInspectionModel.Inspector_Qualification,
                PhaseValue = StationInspectionModel.generalInspectionModel.PhaseValue,
                InspectionType = StationInspectionModel.generalInspectionModel.Inspection_Type,
                CodeId = StationInspectionModel.generalInspectionModel.CodeId,
                RainEventValue = StationInspectionModel.generalInspectionModel.RainEventValue,
                RainEventOtherValue = StationInspectionModel.generalInspectionModel.RainEventOthervalue,
                IsComplete = StationInspectionModel.generalInspectionModel.IsComplete,
                FormName=StationInspectionModel.FormName
            };
            StationInspection.weatherInspection = new WeatherInspection
            {
                Inspection_ID = StationInspectionModel.Inspection_ID,
                StromEventYes = StationInspectionModel.weatherInspectionModel.StromEventYes,
                StromEvent = StationInspectionModel.weatherInspectionModel.StromEvent,
                //StormDetailsListOne = getStromDetails(StationInspectionModel.weatherInspectionModel.StormDetailsListOne),
                //StormDetailsListTwo = getStromDetails(StationInspectionModel.weatherInspectionModel.StormDetailsListTwo),
                //StormDetailsListThree = getStromDetails(StationInspectionModel.weatherInspectionModel.StormDetailsListThree),
                //StormDetailsListFour = getStromDetails(StationInspectionModel.weatherInspectionModel.StormDetailsListFour),
                WeatherTime = StationInspectionModel.weatherInspectionModel.Weather_Time,
                Temperature = StationInspectionModel.weatherInspectionModel.Temperature,
                LastInspection = StationInspectionModel.weatherInspectionModel.LastInspectionYes,
                InspectionOccuring = StationInspectionModel.weatherInspectionModel.InspectionOccuringYes,
                InspectionOccuringYes = StationInspectionModel.weatherInspectionModel.InspectionOccuringYes,
                InspectionOccuringYesValue = StationInspectionModel.weatherInspectionModel.InspectionOccuring,
                UnsafeInspection = StationInspectionModel.weatherInspectionModel.UnsafeInspectionYes,
                UnsafeInspectionValue = StationInspectionModel.weatherInspectionModel.UnsafeInspection
            };

            StationInspection.SiteInspection1 = getSiteInspection(StationInspectionModel.SiteInspection1);
            StationInspection.SiteInspection2 = getSiteInspection(StationInspectionModel.SiteInspection2);
            StationInspection.SiteInspection3 = getSiteInspection(StationInspectionModel.SiteInspection3);
            StationInspection.SiteInspection4 = getSiteInspection(StationInspectionModel.SiteInspection4);
            StationInspection.SiteInspection5 = getSiteInspection(StationInspectionModel.SiteInspection5);
            StationInspection.SiteInspection6 = getSiteInspection(StationInspectionModel.SiteInspection6);
            StationInspection.SiteInspection7 = getSiteInspection(StationInspectionModel.SiteInspection7);
            StationInspection.SiteInspection8 = getSiteInspection(StationInspectionModel.SiteInspection8);
            StationInspection.SiteInspection9 = getSiteInspection(StationInspectionModel.SiteInspection9);
            StationInspection.SiteInspection10 = getSiteInspection(StationInspectionModel.SiteInspection10);
            StationInspection.SiteInspection11 = getSiteInspection(StationInspectionModel.SiteInspection11);
            StationInspection.SiteInspection12 = getSiteInspection(StationInspectionModel.SiteInspection12);
            StationInspection.SiteInspection13 = getSiteInspection(StationInspectionModel.SiteInspection13);
            StationInspection.SiteInspection14 = getSiteInspection(StationInspectionModel.SiteInspection14);
            StationInspection.SiteInspection15 = getSiteInspection(StationInspectionModel.SiteInspection15);
            StationInspection.SiteInspection16 = getSiteInspection(StationInspectionModel.SiteInspection16);
            StationInspection.SiteInspection17 = getSiteInspection(StationInspectionModel.SiteInspection17);
            
            StationInspection.weatherInspection.StormDetailslList = new List<StormDetails>();
            foreach (StormDetailsModel item in StationInspectionModel.weatherInspectionModel.StormDetailsModelList)
            {

                StationInspection.weatherInspection.StormDetailslList.Add(getStormEventData(item));

            }

            StationInspection.weatherInspection.UploadDataList = new List<UploadData>();
            foreach (UploadDataModel item in StationInspectionModel.weatherInspectionModel.UploadDataModelList)
            {
                StationInspection.weatherInspection.UploadDataList.Add(getUploadData(item));
            }
            StationInspection.weatherInspection.StormDetailsEditList = new List<StormDetails>();
            foreach (StormDetailsModel item in StationInspectionModel.weatherInspectionModel.StormDetailsModelEditList)
            {
                StationInspection.weatherInspection.StormDetailsEditList.Add(getStormEventData(item));
            }


            StationInspection.weatherInspection.UploadDataEditList = new List<UploadData>();
            foreach (UploadDataModel item in StationInspectionModel.weatherInspectionModel.UploadDataModelEditList)
            {
                StationInspection.weatherInspection.UploadDataEditList.Add(getUploadData(item));
            }
            return StationInspection;
        }

        public static dynamic GetLocationDetails(long Location_ID)
        {
            return new StationInspection().LocationDetails(Location_ID);
        }

        
        public static dynamic GetInspectorDetails(long Inspector_ID)
        {
            return new StationInspection().InspectorDetails(Inspector_ID);
        }
        private static StormDetails getStromDetails(StormDetailsModel stormDetailsModel)
        {
            return new StormDetails
            {
                StormDateTime = stormDetailsModel.StormDate,
                StormDuration = stormDetailsModel.StormDuration,
                Amount = stormDetailsModel.Amount,
                Storm_ID = stormDetailsModel.Storm_ID
            };
        }

        private static StormDetails getStormEventData(StormDetailsModel stormdetailmodel)
        {
            StormDetails stormevtdetails = new StormDetails();
            stormevtdetails.StormDateTime = stormdetailmodel.StormDate;
            stormevtdetails.StormDuration = stormdetailmodel.StormDuration;
            stormevtdetails.Amount = stormdetailmodel.Amount;
            stormevtdetails.ParentstID = stormdetailmodel.ParentstID;
            stormevtdetails.stID = stormdetailmodel.stID;
            stormevtdetails.ParentStorm_ID = stormdetailmodel.ParentStorm_ID;
            stormevtdetails.Storm_ID = stormdetailmodel.Storm_ID;
            return stormevtdetails;
        }
        private static UploadData getUploadData(UploadDataModel uploadDataModel)
        {
            
            UploadData jUploadData = new UploadData();
            jUploadData.ItemC1_ID = uploadDataModel.itemC1.Item_ID;
            jUploadData.ItemC2_ID = uploadDataModel.itemC2.Item_ID;
            jUploadData.ItemC3_ID = uploadDataModel.itemC3.Item_ID;
            jUploadData.Location = (uploadDataModel.Location == null ? "" : uploadDataModel.Location);
            jUploadData.LengthPPP = uploadDataModel.PPPLength;
            jUploadData.UOM_ID = uploadDataModel.UOM.UOM_ID;
            jUploadData.UploadImagePath = (uploadDataModel.UploadImagePath == null ? "" : uploadDataModel.UploadImagePath);
            jUploadData.FileName = ((uploadDataModel.PostedFile) == null ? "" : uploadDataModel.PostedFile.FileName);
            jUploadData.LtRt = (uploadDataModel.LtRt == null ? "" : uploadDataModel.LtRt);
            jUploadData.Station = (uploadDataModel.Station == null ? "" : uploadDataModel.Station);
            jUploadData.ParentID = uploadDataModel.ParentID;
            jUploadData.ID = uploadDataModel.ID;
            jUploadData.UploadData_ID = uploadDataModel.UploadData_ID == null ? 0 : uploadDataModel.UploadData_ID;
            jUploadData.ParentUploadData_ID = uploadDataModel.ParentUploadData_ID == null ? 0 : uploadDataModel.ParentUploadData_ID;
           
            return jUploadData;
        }

        private static SiteInspection getSiteInspection(SiteInspectionModel siteInspectionModel)
        {

            SiteInspection siteinspection = new SiteInspection();

            siteinspection.InspectionQuestion_ID = siteInspectionModel.InspectionQuestion_ID;
            //siteinspection.AreaInspected = siteInspectionModel.ActionRequiredYes;
            siteinspection.ActionRequired = siteInspectionModel.ActionRequiredYes;
            siteinspection.Notes = siteInspectionModel.Notes;
            siteinspection.SiteInspection_ID = siteInspectionModel.SiteInspection_ID;
            if (siteInspectionModel.AreaInspectedNA)
            {
                siteinspection.AreaInspected = null;
            }
            else if (siteInspectionModel.AreaInspectedYes)
            {
                siteinspection.AreaInspected = true;
            }
            else
            {
                siteinspection.AreaInspected = false;
            }

            return siteinspection;
        }

        public static StationInspectionModel Single(long? Inspection_ID)
        {
            StationInspection StationInspection = new StationInspection().Single(Inspection_ID);
            return GetSubmissionsModel(StationInspection);
        }

        public static dynamic getClientDetailsByInspectionId(long? Inspection_ID)
        {
            dynamic clientDetails = new StationInspection().getClientDetailsByInspectionId(Inspection_ID);

            return clientDetails;
        }

        public static StationInspectionModel GetSubmissionsModel(StationInspection StationInspection)
        {
            StationInspectionModel StationInspectionModel = new StationInspectionModel();
            StationInspectionModel.ReviewerName = StationInspection.ReviewerName;
            StationInspectionModel.ReviewerTitle = StationInspection.ReviewerTitle;
            StationInspectionModel.ReviewerOpenDateTime = StationInspection.ReviewerOpenDateTime;

            StationInspectionModel.generalInspectionModel = new GeneralInspectionModel
            {
                CustomerName = StationInspection.generalInspection.CustomerName,

                Client = new ClientModel
                {
                    Client_ID = StationInspection.generalInspection.Client_ID,
                    Name = StationInspection.generalInspection.CustomerName,
                    CompanyName = StationInspection.generalInspection.CompanyName,
                },
                location = new GeneralInspectionModel
                {
                    Location_ID = StationInspection.generalInspection.Location_ID,
                    Name = StationInspection.generalInspection.ProjectName,
                },
                Date = StationInspection.generalInspection.Date,
                ModifiedDate=StationInspection.generalInspection.ModifiedDate,
                inspector = new InspectorModel
                {
                    Inspector_ID = StationInspection.generalInspection.Inspector_ID,
                    Name = StationInspection.generalInspection.InspectorName,
                },
                locationName = StationInspection.generalInspection.Location,
                Tracking_No = StationInspection.generalInspection.Tracking_No,
                StartTime = StationInspection.generalInspection.StartTime.Trim(),
                EndTime = StationInspection.generalInspection.EndTime.Trim(),
                StartTime_AmPm = StationInspection.generalInspection.StartTime_AmPm.Trim(),
                EndTime_AmPm = StationInspection.generalInspection.EndTime_AmPm.Trim(),
                Inspection_Type = StationInspection.generalInspection.InspectionType,
                CodeId = StationInspection.generalInspection.CodeId,
                Description = StationInspection.generalInspection.Description,
                Inspector_Contact = StationInspection.generalInspection.InspectorContact,
                Inspector_Qualification = StationInspection.generalInspection.InspectorQualification,
                Inspector_Title = StationInspection.generalInspection.InspectorTitle,
                PhaseValue = StationInspection.generalInspection.PhaseValue,
                PhaseBuilding = StationInspection.generalInspection.PhaseBuilding,
                PhaseClear = StationInspection.generalInspection.PhaseClear,
                PhaseExcavations = StationInspection.generalInspection.PhaseExcavations,
                PhaseRoughGrading = StationInspection.generalInspection.PhaseRoughGrading,
                PhaseInfrastructure = StationInspection.generalInspection.PhaseInfrastructure,
                PhaseFinalGrading = StationInspection.generalInspection.PhaseFinalGrading,
                PhaseFinalStabilization = StationInspection.generalInspection.PhaseFinalStabilization,
                PhaseOther = StationInspection.generalInspection.PhaseOther,
                PhaseSitework = StationInspection.generalInspection.PhaseSitework,
                RainEventOthervalue = StationInspection.generalInspection.RainEventValue == "" ? StationInspection.generalInspection.RainEventOtherValue : StationInspection.generalInspection.RainEventValue,
                //RainEventOthervalue = Inspection.generalInspection.RainEventOtherValue,
                RainEvent = StationInspection.generalInspection.RainEvent,
                RainEventOther = StationInspection.generalInspection.RainEventOther,
                UploadSignPath = StationInspection.generalInspection.UploadSignPath
            };
            StationInspectionModel.weatherInspectionModel = new WeatherInspectionModel
            {
                StromEvent = StationInspection.weatherInspection.StromEvent,
                StromEventYes = StationInspection.weatherInspection.StromEventYes,
                StromEventNo = StationInspection.weatherInspection.StromEventNo,
                StromEventYesValue = StationInspection.weatherInspection.StromEvent,
                Weather_Time = StationInspection.weatherInspection.WeatherTime,
                Temperature = StationInspection.weatherInspection.Temperature,
                LastInspectionYes = StationInspection.weatherInspection.LastInspectionYes,
                LastInspectionNo = StationInspection.weatherInspection.LastInspectionNo,
                InspectionOccuringYes = StationInspection.weatherInspection.InspectionOccuringYes,
                InspectionOccuringNo = StationInspection.weatherInspection.InspectionOccuringNo,
                InspectionOccuring = Convert.ToString(StationInspection.weatherInspection.InspectionOccuringYes),
                InspectionOccuringYesValue = StationInspection.weatherInspection.InspectionOccuringYesValue,
                UnsafeInspection = Convert.ToString(StationInspection.weatherInspection.UnsafeInspection),
                UnsafeInspectionYes = StationInspection.weatherInspection.UnsafeInspectionYes,
                UnsafeInspectionNo = StationInspection.weatherInspection.UnsafeInspectionNo,
                UnsafeInspectionValue = StationInspection.weatherInspection.UnsafeInspectionValue,
            };
           
            StationInspectionModel.SiteInspection1 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection1.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection1.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection1.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection1.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection1.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection1.ActionRequiredNo,
                Notes = StationInspection.SiteInspection1.Notes,
                Question = StationInspection.SiteInspection1.Question,
                SiteInspection_ID = StationInspection.SiteInspection1.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection2 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection2.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection2.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection2.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection2.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection2.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection2.ActionRequiredNo,
                Notes = StationInspection.SiteInspection2.Notes,
                Question = StationInspection.SiteInspection2.Question,
                SiteInspection_ID = StationInspection.SiteInspection2.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection3 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection3.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection3.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection3.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection3.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection3.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection3.ActionRequiredNo,
                Notes = StationInspection.SiteInspection3.Notes,
                Question = StationInspection.SiteInspection3.Question,
                SiteInspection_ID = StationInspection.SiteInspection3.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection4 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection4.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection4.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection4.AreaInspectedNo,
                ActionRequiredYes = StationInspection.SiteInspection4.ActionRequiredYes,
                AreaInspectedNA = StationInspection.SiteInspection4.AreaInspectedNA,
                ActionRequiredNo = StationInspection.SiteInspection4.ActionRequiredNo,
                Notes = StationInspection.SiteInspection4.Notes,
                Question = StationInspection.SiteInspection4.Question,
                SiteInspection_ID = StationInspection.SiteInspection4.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection5 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection5.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection5.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection5.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection5.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection5.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection5.ActionRequiredNo,
                Notes = StationInspection.SiteInspection5.Notes,
                Question = StationInspection.SiteInspection5.Question,
                SiteInspection_ID = StationInspection.SiteInspection5.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection6 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection6.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection6.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection6.AreaInspectedNo,
                ActionRequiredYes = StationInspection.SiteInspection6.ActionRequiredYes,
                AreaInspectedNA = StationInspection.SiteInspection6.AreaInspectedNA,
                ActionRequiredNo = StationInspection.SiteInspection6.ActionRequiredNo,
                Notes = StationInspection.SiteInspection6.Notes,
                Question = StationInspection.SiteInspection6.Question,
                SiteInspection_ID = StationInspection.SiteInspection6.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection7 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection7.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection7.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection7.AreaInspectedNo,
                ActionRequiredYes = StationInspection.SiteInspection7.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection7.ActionRequiredNo,
                AreaInspectedNA = StationInspection.SiteInspection7.AreaInspectedNA,
                Notes = StationInspection.SiteInspection7.Notes,
                Question = StationInspection.SiteInspection7.Question,
                SiteInspection_ID = StationInspection.SiteInspection7.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection8 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection8.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection8.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection8.AreaInspectedNo,
                ActionRequiredYes = StationInspection.SiteInspection8.ActionRequiredYes,
                AreaInspectedNA = StationInspection.SiteInspection8.AreaInspectedNA,
                ActionRequiredNo = StationInspection.SiteInspection8.ActionRequiredNo,
                Notes = StationInspection.SiteInspection8.Notes,
                Question = StationInspection.SiteInspection8.Question,
                SiteInspection_ID = StationInspection.SiteInspection8.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection9 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection9.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection9.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection9.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection9.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection9.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection9.ActionRequiredNo,
                Notes = StationInspection.SiteInspection9.Notes,
                Question = StationInspection.SiteInspection9.Question,
                SiteInspection_ID = StationInspection.SiteInspection9.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection10 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection10.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection10.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection10.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection10.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection10.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection10.ActionRequiredNo,
                Notes = StationInspection.SiteInspection10.Notes,
                Question = StationInspection.SiteInspection10.Question,
                SiteInspection_ID = StationInspection.SiteInspection10.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection11 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection11.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection11.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection11.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection11.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection11.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection11.ActionRequiredNo,
                Notes = StationInspection.SiteInspection11.Notes,
                Question = StationInspection.SiteInspection11.Question,
                SiteInspection_ID = StationInspection.SiteInspection11.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection12 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection12.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection12.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection12.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection12.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection12.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection12.ActionRequiredNo,
                Notes = StationInspection.SiteInspection12.Notes,
                Question = StationInspection.SiteInspection12.Question,
                SiteInspection_ID = StationInspection.SiteInspection12.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection13 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection13.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection13.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection13.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection13.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection13.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection13.ActionRequiredNo,
                Notes = StationInspection.SiteInspection13.Notes,
                Question = StationInspection.SiteInspection13.Question,
                SiteInspection_ID = StationInspection.SiteInspection13.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection14 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection14.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection14.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection14.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection14.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection14.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection14.ActionRequiredNo,
                Notes = StationInspection.SiteInspection14.Notes,
                Question = StationInspection.SiteInspection14.Question,
                SiteInspection_ID = StationInspection.SiteInspection14.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection15 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection15.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection15.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection15.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection15.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection15.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection15.ActionRequiredNo,
                Notes = StationInspection.SiteInspection15.Notes,
                Question = StationInspection.SiteInspection15.Question,
                SiteInspection_ID = StationInspection.SiteInspection15.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection16 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection16.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection16.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection16.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection16.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection16.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection16.ActionRequiredNo,
                Notes = StationInspection.SiteInspection16.Notes,
                Question = StationInspection.SiteInspection16.Question,
                SiteInspection_ID = StationInspection.SiteInspection16.SiteInspection_ID
            };
            StationInspectionModel.SiteInspection17 = new SiteInspectionModel
            {
                InspectionQuestion_ID = StationInspection.SiteInspection17.InspectionQuestion_ID,
                AreaInspectedYes = StationInspection.SiteInspection17.AreaInspectedYes,
                AreaInspectedNo = StationInspection.SiteInspection17.AreaInspectedNo,
                AreaInspectedNA = StationInspection.SiteInspection17.AreaInspectedNA,
                ActionRequiredYes = StationInspection.SiteInspection17.ActionRequiredYes,
                ActionRequiredNo = StationInspection.SiteInspection17.ActionRequiredNo,
                Notes = StationInspection.SiteInspection17.Notes,
                Question = StationInspection.SiteInspection17.Question,
                SiteInspection_ID = StationInspection.SiteInspection17.SiteInspection_ID
            };
            


            foreach (var item in StationInspection.weatherInspection.StormDetailslList)
            {
                StationInspectionModel.weatherInspectionModel.StormDetailsModelList.Add(new StormDetailsModel
                {
                    Amount = item.Amount,
                    StormDuration = item.StormDuration,
                    StormDate = item.StormDateTime,
                    ParentStorm_ID = item.ParentStorm_ID,
                    Storm_ID = item.Storm_ID,
                });

            }
            foreach (var item in StationInspection.weatherInspection.UploadDataList)
            {
                StationInspectionModel.weatherInspectionModel.UploadDataModelList.Add(new UploadDataModel
                {
                    itemC1 = new ItemC1Model
                    {
                        Item_ID = Convert.ToInt32(item.ItemC1_ID),
                        Name = item.ItemC1_Name,
                    },
                    itemC2 = new ItemC2Model
                    {
                        Item_ID = Convert.ToInt16(item.ItemC2_ID),
                        Name = item.ItemC2_Name,
                    },
                    itemC3 = new ItemC3Model
                    {
                        Item_ID = Convert.ToInt16(item.ItemC3_ID),
                        Name = item.ItemC3_Name,
                    },                   
                    Station = item.Station,
                    Location = item.Location,
                    PPPLength = item.LengthPPP,
                    LtRt = item.LtRt,
                    UOM = new UOMModel
                    {
                        UOM_ID = item.UOM_ID,
                        UOM = item.UOM,
                    },
                    UploadData_ID = item.UploadData_ID,
                    ParentUploadData_ID = item.ParentUploadData_ID,
                    UploadImagePath = item.UploadImagePath,
                    ImageName = item.ImageName,
                    Status = item.Status
                });
            }
            return StationInspectionModel;
        }

        public static bool Edit(StationInspectionModel StationInspectionModel)
        {
            StringBuilder sb = new StringBuilder();
            if (StationInspectionModel.generalInspectionModel.PhaseClear)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Clear and Grub";
                sb.Append("Clear and Grub,");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseExcavations)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Excavation";
                sb.Append("Excavation,");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseBuilding)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Building (vertical)";
                sb.Append("Building (vertical),");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseRoughGrading)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Rough Grading";
                sb.Append("Rough Grading,");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseInfrastructure)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Infrastructure";
                sb.Append("Infrastructure,");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseFinalGrading)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Final Grading";
                sb.Append("Final Grading,");
            }
            if (StationInspectionModel.generalInspectionModel.PhaseFinalStabilization)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = "Final Stabilization";
                sb.Append("Final Stabilization");
            }
            if (sb.ToString() != string.Empty)
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = sb.ToString();
            }
            else
            {
                StationInspectionModel.generalInspectionModel.PhaseValue = string.Empty;
            }

            if (StationInspectionModel.generalInspectionModel.RainEvent)
            {
                StationInspectionModel.generalInspectionModel.RainEventValue = StationInspectionModel.generalInspectionModel.RainEventOthervalue;
            }
            else
            {
                StationInspectionModel.generalInspectionModel.RainEventValue = null;
            }
            if (StationInspectionModel.generalInspectionModel.RainEventOther)
            {
                StationInspectionModel.generalInspectionModel.RainEventOthervalue = StationInspectionModel.generalInspectionModel.RainEventOthervalue;
            }
            else
            {
                StationInspectionModel.generalInspectionModel.RainEventOthervalue = null;
            }

            if (StationInspectionModel.weatherInspectionModel.StromEventYes)
            {
                StationInspectionModel.weatherInspectionModel.StromEvent = StationInspectionModel.weatherInspectionModel.StromEventYesValue;
            }
            else
            {
                StationInspectionModel.weatherInspectionModel.StromEvent = "";
            }
            if (StationInspectionModel.weatherInspectionModel.InspectionOccuringYes)
            {
                StationInspectionModel.weatherInspectionModel.InspectionOccuring = StationInspectionModel.weatherInspectionModel.InspectionOccuringYesValue;
            }
            else
            {
                StationInspectionModel.weatherInspectionModel.InspectionOccuring = "No";
            }
            if (StationInspectionModel.weatherInspectionModel.UnsafeInspectionYes)
            {
                StationInspectionModel.weatherInspectionModel.UnsafeInspection = StationInspectionModel.weatherInspectionModel.UnsafeInspectionValue;
            }
            else
            {
                StationInspectionModel.weatherInspectionModel.UnsafeInspection = "No";
            }

            string controllerName = "StationInspection";             
          
            foreach (var item in StationInspectionModel.weatherInspectionModel.UploadDataModelList)
            {
                if (item.FileName.Trim() != string.Empty)
                {
                    item.SaveFile(controllerName);
                    string FName = item.FileName.Replace(' ', '_');
                    FName = string.Concat(Path.GetFileNameWithoutExtension(FName), DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), Path.GetExtension(FName));
                    item.UploadImagePath = "/UploadedImage/" + FName;
                    item.ImageName = FName;
                }
            }

            foreach (var item in StationInspectionModel.weatherInspectionModel.UploadDataModelEditList)
            {
                if (item.FileName.Trim() != string.Empty)
                {
                    item.SaveFile(controllerName);
                    string FName = item.FileName.Replace(' ', '_');
                    FName = string.Concat(Path.GetFileNameWithoutExtension(FName), DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), Path.GetExtension(FName));
                    item.UploadImagePath = "/UploadedImage/" + FName;
                    item.ImageName = FName;
                }
            }
            StationInspection StationInspection = GetInspection(StationInspectionModel);
            return StationInspection.Edit(StationInspectionModel.Inspection_ID);
        }

        public static bool Autoresponder(long Inspection_ID)
        {
            GeneralInspection GeneralInspection = new GeneralInspection();
            return GeneralInspection.Autoresponder(Inspection_ID);
        }

        public static Boolean deleteUploadData(long UploadData_ID)
        {
            return new StationInspection().deleteUploadData(UploadData_ID);
        }

        public static int checkInspectionReportAlreadyExist(Int64 Location_ID, DateTime date, string FormName)
        {
            var Result= new StationInspection().checkInspectionReportAlreadyExist(Location_ID, date, FormName);
            return Result;
        }
    }
}