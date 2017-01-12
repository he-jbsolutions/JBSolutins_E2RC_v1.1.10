using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;
using System.Text;
using System.Configuration;
using System.IO;

namespace e2rc.Models.Repository
{
    public class InspectionRepository
    {
        public static IEnumerable<dynamic> Locations(long User_ID)
        {
            return new Inspection().Locations(User_ID);    
        }
        public static long Create(InspectionModel inspectionModel)
        {
            StringBuilder sb = new StringBuilder();
            if (inspectionModel.generalInspectionModel.PhaseClear)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Clear and Grub";
                sb.Append("Clear and Grub,");
            }
            if (inspectionModel.generalInspectionModel.PhaseExcavations)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Excavation";
                sb.Append("Excavation,");
            }
            if (inspectionModel.generalInspectionModel.PhaseBuilding)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Building (vertical)";
                sb.Append("Building (vertical),");
            }
            if (inspectionModel.generalInspectionModel.PhaseRoughGrading)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Rough Grading";
                sb.Append("Rough Grading,");
            }
            if (inspectionModel.generalInspectionModel.PhaseInfrastructure)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Infrastructure";
                sb.Append("Infrastructure,");
            }
            if (inspectionModel.generalInspectionModel.PhaseFinalGrading)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Final Grading";
                sb.Append("Final Grading,");
            }
            if (inspectionModel.generalInspectionModel.PhaseFinalStabilization)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Final Stabilization";
                sb.Append("Final Stabilization");
            }
            if (sb.ToString() != string.Empty)
            {
                inspectionModel.generalInspectionModel.PhaseValue = sb.ToString();
            }
            else
            {
                inspectionModel.generalInspectionModel.PhaseValue = string.Empty;
            }

            if (inspectionModel.generalInspectionModel.RainEvent)
            {
                inspectionModel.generalInspectionModel.RainEventValue = inspectionModel.generalInspectionModel.RainEventOthervalue;
            }
            else
            {
                inspectionModel.generalInspectionModel.RainEventValue = null;
            }
            if (inspectionModel.generalInspectionModel.RainEventOther)
            {
                inspectionModel.generalInspectionModel.RainEventOthervalue = inspectionModel.generalInspectionModel.RainEventOthervalue;
            }
            else
            {
                inspectionModel.generalInspectionModel.RainEventOthervalue = null;
            }

            if (inspectionModel.weatherInspectionModel.StromEventYes)
            {
                inspectionModel.weatherInspectionModel.StromEvent = inspectionModel.weatherInspectionModel.StromEventYesValue;
            }
            else
            {
                //inspectionModel.weatherInspectionModel.StromEvent = "No";
                inspectionModel.weatherInspectionModel.StromEvent = "";
            }
            if (inspectionModel.weatherInspectionModel.InspectionOccuringYes)
            {
                inspectionModel.weatherInspectionModel.InspectionOccuring = inspectionModel.weatherInspectionModel.InspectionOccuringYesValue;
            }
            else
            {
                inspectionModel.weatherInspectionModel.InspectionOccuring = "No";
            }
            if (inspectionModel.weatherInspectionModel.UnsafeInspectionYes)
            {
                inspectionModel.weatherInspectionModel.UnsafeInspection = inspectionModel.weatherInspectionModel.UnsafeInspectionValue;
            }
            else
            {
                inspectionModel.weatherInspectionModel.UnsafeInspection = "No";
            }
            string controllerName = "Inspection";
            foreach (var item in inspectionModel.weatherInspectionModel.UploadDataModelList)
            {
                if (item.FileName.Trim() != string.Empty)
                {
                    item.SaveFile(controllerName);
                    string FName = item.FileName.Replace(' ','_');
                    FName = string.Concat(Path.GetFileNameWithoutExtension(FName), DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), Path.GetExtension(FName));
                    item.UploadImagePath = "/UploadedImage/" + FName;
                    // item.UploadImagePath = item.FileName;
                }
            }
            Inspection inspection = GetInspection(inspectionModel);
            return inspection.Create();
        }

        public static bool Edit(InspectionModel inspectionModel)
        {
            StringBuilder sb = new StringBuilder();
            if (inspectionModel.generalInspectionModel.PhaseClear)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Clear and Grub";
                sb.Append("Clear and Grub,");
            }
            if (inspectionModel.generalInspectionModel.PhaseExcavations)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Excavation";
                sb.Append("Excavation,");
            }
            if (inspectionModel.generalInspectionModel.PhaseBuilding)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Building (vertical)";
                sb.Append("Building (vertical),");
            }
            if (inspectionModel.generalInspectionModel.PhaseRoughGrading)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Rough Grading";
                sb.Append("Rough Grading,");
            }
            if (inspectionModel.generalInspectionModel.PhaseInfrastructure)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Infrastructure";
                sb.Append("Infrastructure,");
            }
            if (inspectionModel.generalInspectionModel.PhaseFinalGrading)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Final Grading";
                sb.Append("Final Grading,");
            }
            if (inspectionModel.generalInspectionModel.PhaseFinalStabilization)
            {
                inspectionModel.generalInspectionModel.PhaseValue = "Final Stabilization";
                sb.Append("Final Stabilization");
            }
            if (sb.ToString() != string.Empty)
            {
                inspectionModel.generalInspectionModel.PhaseValue = sb.ToString();
            }
            else
            {
                inspectionModel.generalInspectionModel.PhaseValue = string.Empty;
            }

            if (inspectionModel.generalInspectionModel.RainEvent)
            {
                inspectionModel.generalInspectionModel.RainEventValue = inspectionModel.generalInspectionModel.RainEventOthervalue;
            }
            else
            {
                inspectionModel.generalInspectionModel.RainEventValue = null;
            }
            if (inspectionModel.generalInspectionModel.RainEventOther)
            {
                inspectionModel.generalInspectionModel.RainEventOthervalue = inspectionModel.generalInspectionModel.RainEventOthervalue;
            }
            else
            {
                inspectionModel.generalInspectionModel.RainEventOthervalue = null;
            }

            if (inspectionModel.weatherInspectionModel.StromEventYes)
            {
                inspectionModel.weatherInspectionModel.StromEvent = inspectionModel.weatherInspectionModel.StromEventYesValue;
            }
            else
            {
                inspectionModel.weatherInspectionModel.StromEvent = "";
            }
            if (inspectionModel.weatherInspectionModel.InspectionOccuringYes)
            {
                inspectionModel.weatherInspectionModel.InspectionOccuring = inspectionModel.weatherInspectionModel.InspectionOccuringYesValue;
            }
            else
            {
                inspectionModel.weatherInspectionModel.InspectionOccuring = "No";
            }
            if (inspectionModel.weatherInspectionModel.UnsafeInspectionYes)
            {
                inspectionModel.weatherInspectionModel.UnsafeInspection = inspectionModel.weatherInspectionModel.UnsafeInspectionValue;
            }
            else
            {
                inspectionModel.weatherInspectionModel.UnsafeInspection = "No";
            }

            string controllerName = "Inspection";
            foreach (var item in inspectionModel.weatherInspectionModel.UploadDataModelList)
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
            foreach (var item in inspectionModel.weatherInspectionModel.UploadDataModelEditList)
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

            Inspection inspection = GetInspection(inspectionModel);
            return inspection.Edit(inspectionModel.Inspection_ID);
        }

        public static dynamic GetLocationDetails(long Location_ID)
        {
            return new Inspection().LocationDetails(Location_ID);
        }

        public static dynamic GetInspectorDetails(long Inspector_ID)
        {
            return new Inspection().InspectorDetails(Inspector_ID);
        }

        public static dynamic GetInspectorDetails(string InspectorName)
        {
            return new Inspection().InspectorDetails(InspectorName);
        }

        public static IEnumerable<dynamic> GetInspectorsClientDetails(long Inspector_ID)
        {
            return new Inspection().GetInspectorsClientDetails(Inspector_ID);
        }

        public static InspectionModel Single(long? Inspection_ID)
        {
            Inspection Inspection = new Inspection().Single(Inspection_ID);
            return GetSubmissionsModel(Inspection);
        }

        public static dynamic getClientDetailsByInspectionId(long Inspection_ID)
        {
            return new Inspection().getClientDetailsByInspectionId(Inspection_ID);
        }

        public static InspectionModel GetSubmissionsModel(Inspection Inspection)
        {
            InspectionModel inspectionModel = new InspectionModel();

            inspectionModel.ReviewerName = Inspection.ReviewerName;
            inspectionModel.ReviewerOpenDateTime = Inspection.ReviewerOpenDateTime;
            inspectionModel.ReviewerTitle = Inspection.ReviewerTitle;

            inspectionModel.generalInspectionModel = new GeneralInspectionModel
            {
                CustomerName = Inspection.generalInspection.CustomerName,

                Client = new ClientModel
                {
                    Client_ID = Inspection.generalInspection.Client_ID,
                    Name=Inspection.generalInspection.CustomerName,
                    CompanyName=Inspection.generalInspection.CompanyName,

                },
                location = new GeneralInspectionModel
                {
                    Location_ID = Inspection.generalInspection.Location_ID,
                    Name = Inspection.generalInspection.ProjectName,
                },
                Date = Inspection.generalInspection.Date,
                ModifiedDate = Inspection.generalInspection.ModifiedDate,
                inspector = new InspectorModel
                {
                    Inspector_ID = Inspection.generalInspection.Inspector_ID,
                    Name = Inspection.generalInspection.InspectorName,
                },
                locationName = Inspection.generalInspection.Location,
                Tracking_No = Inspection.generalInspection.Tracking_No,
                StartTime = Inspection.generalInspection.StartTime.Trim(),
                EndTime = Inspection.generalInspection.EndTime.Trim(),
                StartTime_AmPm = Inspection.generalInspection.StartTime_AmPm.Trim(),
                EndTime_AmPm = Inspection.generalInspection.EndTime_AmPm.Trim(),
                Inspection_Type = Inspection.generalInspection.InspectionType,
                CodeId = Inspection.generalInspection.CodeId,
                Description = Inspection.generalInspection.Description,
                Inspector_Contact = Inspection.generalInspection.InspectorContact,
                Inspector_Qualification = Inspection.generalInspection.InspectorQualification,
                Inspector_Title = Inspection.generalInspection.InspectorTitle,
                PhaseValue = Inspection.generalInspection.PhaseValue,
                PhaseBuilding = Inspection.generalInspection.PhaseBuilding,
                PhaseClear = Inspection.generalInspection.PhaseClear,
                PhaseExcavations = Inspection.generalInspection.PhaseExcavations,
                PhaseRoughGrading = Inspection.generalInspection.PhaseRoughGrading,
                PhaseInfrastructure = Inspection.generalInspection.PhaseInfrastructure,
                PhaseFinalGrading = Inspection.generalInspection.PhaseFinalGrading,
                PhaseFinalStabilization = Inspection.generalInspection.PhaseFinalStabilization,
                PhaseOther = Inspection.generalInspection.PhaseOther,
                PhaseSitework = Inspection.generalInspection.PhaseSitework,
                RainEventOthervalue = Inspection.generalInspection.RainEventValue == "" ? Inspection.generalInspection.RainEventOtherValue : Inspection.generalInspection.RainEventValue,
                //RainEventOthervalue = Inspection.generalInspection.RainEventOtherValue,
                RainEvent = Inspection.generalInspection.RainEvent,
                RainEventOther = Inspection.generalInspection.RainEventOther,
                UploadSignPath = Inspection.generalInspection.UploadSignPath == "" ? "" : Inspection.generalInspection.UploadSignPath
            };
            inspectionModel.weatherInspectionModel = new WeatherInspectionModel
            {
                StromEvent = Inspection.weatherInspection.StromEvent,
                StromEventYes = Inspection.weatherInspection.StromEventYes,
                StromEventNo = Inspection.weatherInspection.StromEventNo,
                StromEventYesValue = Inspection.weatherInspection.StromEvent,
                Weather_Time = Inspection.weatherInspection.WeatherTime,
                Temperature = Inspection.weatherInspection.Temperature,
                LastInspectionYes = Inspection.weatherInspection.LastInspectionYes,
                LastInspectionNo = Inspection.weatherInspection.LastInspectionNo,
                InspectionOccuringYes = Inspection.weatherInspection.InspectionOccuringYes,
                InspectionOccuringNo = Inspection.weatherInspection.InspectionOccuringNo,
                InspectionOccuring = Convert.ToString(Inspection.weatherInspection.InspectionOccuringYes),
                InspectionOccuringYesValue = Inspection.weatherInspection.InspectionOccuringYesValue,
                UnsafeInspection = Convert.ToString(Inspection.weatherInspection.UnsafeInspection),
                UnsafeInspectionYes = Inspection.weatherInspection.UnsafeInspectionYes,
                UnsafeInspectionNo = Inspection.weatherInspection.UnsafeInspectionNo,
                UnsafeInspectionValue = Inspection.weatherInspection.UnsafeInspectionValue,
            };
         
            inspectionModel.SiteInspection1 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection1.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection1.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection1.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection1.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection1.ActionRequiredYes,               
                ActionRequiredNo = Inspection.SiteInspection1.ActionRequiredNo,
                Notes = Inspection.SiteInspection1.Notes,
                Question = Inspection.SiteInspection1.Question,
                SiteInspection_ID = Inspection.SiteInspection1.SiteInspection_ID
            };
            inspectionModel.SiteInspection2 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection2.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection2.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection2.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection2.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection2.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection2.ActionRequiredNo,
                Notes = Inspection.SiteInspection2.Notes,
                Question = Inspection.SiteInspection2.Question,
                SiteInspection_ID = Inspection.SiteInspection2.SiteInspection_ID
            };
            inspectionModel.SiteInspection3 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection3.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection3.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection3.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection3.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection3.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection3.ActionRequiredNo,
                Notes = Inspection.SiteInspection3.Notes,
                Question = Inspection.SiteInspection3.Question,
                SiteInspection_ID = Inspection.SiteInspection3.SiteInspection_ID
            };
            inspectionModel.SiteInspection4 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection4.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection4.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection4.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection4.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection4.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection4.ActionRequiredNo,
                Notes = Inspection.SiteInspection4.Notes,
                Question = Inspection.SiteInspection4.Question,
                SiteInspection_ID = Inspection.SiteInspection4.SiteInspection_ID
            };
            inspectionModel.SiteInspection5 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection5.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection5.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection5.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection5.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection5.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection5.ActionRequiredNo,
                Notes = Inspection.SiteInspection5.Notes,
                Question = Inspection.SiteInspection5.Question,
                SiteInspection_ID = Inspection.SiteInspection5.SiteInspection_ID
            };
            inspectionModel.SiteInspection6 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection6.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection6.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection6.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection6.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection6.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection6.ActionRequiredNo,
                Notes = Inspection.SiteInspection6.Notes,
                Question = Inspection.SiteInspection6.Question,
                SiteInspection_ID = Inspection.SiteInspection6.SiteInspection_ID
            };
            inspectionModel.SiteInspection7 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection7.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection7.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection7.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection7.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection7.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection7.ActionRequiredNo,
                Notes = Inspection.SiteInspection7.Notes,
                Question = Inspection.SiteInspection7.Question,
                SiteInspection_ID = Inspection.SiteInspection7.SiteInspection_ID
            };
            inspectionModel.SiteInspection8 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection8.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection8.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection8.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection8.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection8.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection8.ActionRequiredNo,
                Notes = Inspection.SiteInspection8.Notes,
                Question = Inspection.SiteInspection8.Question,
                SiteInspection_ID = Inspection.SiteInspection8.SiteInspection_ID
            };
            inspectionModel.SiteInspection9 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection9.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection9.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection9.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection9.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection9.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection9.ActionRequiredNo,
                Notes = Inspection.SiteInspection9.Notes,
                Question = Inspection.SiteInspection9.Question,
                SiteInspection_ID = Inspection.SiteInspection9.SiteInspection_ID
            };
            inspectionModel.SiteInspection10 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection10.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection10.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection10.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection10.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection10.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection10.ActionRequiredNo,
                Notes = Inspection.SiteInspection10.Notes,
                Question = Inspection.SiteInspection10.Question,
                SiteInspection_ID = Inspection.SiteInspection10.SiteInspection_ID
            };
            inspectionModel.SiteInspection11 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection11.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection11.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection11.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection11.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection11.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection11.ActionRequiredNo,
                Notes = Inspection.SiteInspection11.Notes,
                Question = Inspection.SiteInspection11.Question,
                SiteInspection_ID = Inspection.SiteInspection11.SiteInspection_ID
            };
            inspectionModel.SiteInspection12 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection12.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection12.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection12.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection12.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection12.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection12.ActionRequiredNo,
                Notes = Inspection.SiteInspection12.Notes,
                Question = Inspection.SiteInspection12.Question,
                SiteInspection_ID = Inspection.SiteInspection12.SiteInspection_ID
            };
            inspectionModel.SiteInspection13 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection13.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection13.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection13.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection13.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection13.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection13.ActionRequiredNo,
                Notes = Inspection.SiteInspection13.Notes,
                Question = Inspection.SiteInspection13.Question,
                SiteInspection_ID = Inspection.SiteInspection13.SiteInspection_ID
            };
            inspectionModel.SiteInspection14 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection14.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection14.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection14.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection14.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection14.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection14.ActionRequiredNo,
                Notes = Inspection.SiteInspection14.Notes,
                Question = Inspection.SiteInspection14.Question,
                SiteInspection_ID = Inspection.SiteInspection14.SiteInspection_ID
            };
            inspectionModel.SiteInspection15 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection15.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection15.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection15.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection15.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection15.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection15.ActionRequiredNo,
                Notes = Inspection.SiteInspection15.Notes,
                Question = Inspection.SiteInspection15.Question,
                SiteInspection_ID = Inspection.SiteInspection15.SiteInspection_ID
            };
            inspectionModel.SiteInspection16 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection16.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection16.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection16.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection16.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection16.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection16.ActionRequiredNo,
                Notes = Inspection.SiteInspection16.Notes,
                Question = Inspection.SiteInspection16.Question,
                SiteInspection_ID = Inspection.SiteInspection16.SiteInspection_ID
            };
            inspectionModel.SiteInspection17 = new SiteInspectionModel
            {
                InspectionQuestion_ID = Inspection.SiteInspection17.InspectionQuestion_ID,
                AreaInspectedYes = Inspection.SiteInspection17.AreaInspectedYes,
                AreaInspectedNo = Inspection.SiteInspection17.AreaInspectedNo,
                AreaInspectedNA = Inspection.SiteInspection17.AreaInspectedNA,
                ActionRequiredYes = Inspection.SiteInspection17.ActionRequiredYes,
                ActionRequiredNo = Inspection.SiteInspection17.ActionRequiredNo,
                Notes = Inspection.SiteInspection17.Notes,
                Question = Inspection.SiteInspection17.Question,
                SiteInspection_ID = Inspection.SiteInspection17.SiteInspection_ID
            };
            

            foreach (var item in Inspection.weatherInspection.StormDetailslList)
            {
                inspectionModel.weatherInspectionModel.StormDetailsModelList.Add(new StormDetailsModel
                {
                    Amount=item.Amount,
                    StormDuration=item.StormDuration,
                    StormDate=item.StormDateTime,                    
                    ParentStorm_ID=item.ParentStorm_ID,
                    Storm_ID=item.Storm_ID,    
                });             

            }
             foreach (var item in Inspection.weatherInspection.UploadDataList)
            {
                inspectionModel.weatherInspectionModel.UploadDataModelList.Add(new UploadDataModel
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


                    Location = item.Location,
                    PPPLength = item.LengthPPP,

                    UOM = new UOMModel
                    {
                        UOM_ID = item.UOM_ID,
                        UOM = item.UOM,
                    },
                    UploadData_ID = item.UploadData_ID,
                    ParentUploadData_ID = item.ParentUploadData_ID,
                    UploadImagePath = item.UploadImagePath,
                    ImageName = item.ImageName,
                    Status=item.Status

                });
            }
            return inspectionModel;
        }

        private static Inspection GetInspection(InspectionModel inspectionModel)
        {
            Inspection inspection = new Inspection();
            inspection.Date = inspectionModel.Date;
            inspection.generalInspection = new GeneralInspection
            {
                CreatedBy_ID = inspectionModel.CreatedBy,
                Inspection_ID = inspectionModel.Inspection_ID,
                FormName = (inspectionModel.FormName == "" ? "F2" : "F1"),
                Client_ID = inspectionModel.generalInspectionModel.Client.Client_ID,
                Location_ID = inspectionModel.generalInspectionModel.location.Location_ID,
                ProjectName = inspectionModel.generalInspectionModel.location.Name,
                Tracking_No = inspectionModel.generalInspectionModel.Tracking_No,
                Location = inspectionModel.generalInspectionModel.locationName,
                Date = inspectionModel.generalInspectionModel.Date,
                StartTime = inspectionModel.generalInspectionModel.StartTime + inspectionModel.generalInspectionModel.StartTime_AmPm,
                EndTime = inspectionModel.generalInspectionModel.EndTime + inspectionModel.generalInspectionModel.EndTime_AmPm,
                Inspector_ID = inspectionModel.generalInspectionModel.inspector.Inspector_ID,
                InspectorName = inspectionModel.generalInspectionModel.inspector.Name,
                InspectorTitle = inspectionModel.generalInspectionModel.Inspector_Title,
                InspectorContact = inspectionModel.generalInspectionModel.Inspector_Contact,
                InspectorQualification = inspectionModel.generalInspectionModel.Inspector_Qualification,
                PhaseValue = inspectionModel.generalInspectionModel.PhaseValue,
                InspectionType = inspectionModel.generalInspectionModel.Inspection_Type,
                CodeId = inspectionModel.generalInspectionModel.CodeId,
                Description = inspectionModel.generalInspectionModel.Description,
                RainEventValue = inspectionModel.generalInspectionModel.RainEventValue,
                RainEventOtherValue = inspectionModel.generalInspectionModel.RainEventOthervalue,
                IsComplete = inspectionModel.generalInspectionModel.IsComplete,


            };
            inspection.weatherInspection = new WeatherInspection
            {
                Inspection_ID = inspectionModel.Inspection_ID,
                StromEventYes = inspectionModel.weatherInspectionModel.StromEventYes,
                StromEvent = inspectionModel.weatherInspectionModel.StromEvent,
                //StormDetailsListOne = getStromDetails(inspectionModel.weatherInspectionModel.StormDetailsListOne),
                //StormDetailsListTwo = getStromDetails(inspectionModel.weatherInspectionModel.StormDetailsListTwo),
                //StormDetailsListThree = getStromDetails(inspectionModel.weatherInspectionModel.StormDetailsListThree),
                //StormDetailsListFour = getStromDetails(inspectionModel.weatherInspectionModel.StormDetailsListFour),
                WeatherTime = inspectionModel.weatherInspectionModel.Weather_Time,
                Temperature = inspectionModel.weatherInspectionModel.Temperature,
                LastInspection = inspectionModel.weatherInspectionModel.LastInspectionYes,
                InspectionOccuring = inspectionModel.weatherInspectionModel.InspectionOccuringYes,
                InspectionOccuringYes = inspectionModel.weatherInspectionModel.InspectionOccuringYes,
                InspectionOccuringYesValue = inspectionModel.weatherInspectionModel.InspectionOccuring,
                UnsafeInspection = inspectionModel.weatherInspectionModel.UnsafeInspectionYes,
                UnsafeInspectionValue = inspectionModel.weatherInspectionModel.UnsafeInspection
            };


            inspection.SiteInspection1 = getSiteInspection(inspectionModel.SiteInspection1);
            inspection.SiteInspection2 = getSiteInspection(inspectionModel.SiteInspection2);
            inspection.SiteInspection3 = getSiteInspection(inspectionModel.SiteInspection3);
            inspection.SiteInspection4 = getSiteInspection(inspectionModel.SiteInspection4);
            inspection.SiteInspection5 = getSiteInspection(inspectionModel.SiteInspection5);
            inspection.SiteInspection6 = getSiteInspection(inspectionModel.SiteInspection6);
            inspection.SiteInspection7 = getSiteInspection(inspectionModel.SiteInspection7);
            inspection.SiteInspection8 = getSiteInspection(inspectionModel.SiteInspection8);
            inspection.SiteInspection9 = getSiteInspection(inspectionModel.SiteInspection9);
            inspection.SiteInspection10 = getSiteInspection(inspectionModel.SiteInspection10);
            inspection.SiteInspection11 = getSiteInspection(inspectionModel.SiteInspection11);
            inspection.SiteInspection12 = getSiteInspection(inspectionModel.SiteInspection12);
            inspection.SiteInspection13 = getSiteInspection(inspectionModel.SiteInspection13);
            inspection.SiteInspection14 = getSiteInspection(inspectionModel.SiteInspection14);
            inspection.SiteInspection15 = getSiteInspection(inspectionModel.SiteInspection15);
            inspection.SiteInspection16 = getSiteInspection(inspectionModel.SiteInspection16);
            inspection.SiteInspection17 = getSiteInspection(inspectionModel.SiteInspection17);
            
            inspection.weatherInspection.StormDetailslList = new List<StormDetails>();
            foreach (StormDetailsModel item in inspectionModel.weatherInspectionModel.StormDetailsModelList)
            {

                inspection.weatherInspection.StormDetailslList.Add(getStormEventData(item));

            }

            inspection.weatherInspection.UploadDataList = new List<UploadData>();
            foreach (UploadDataModel item in inspectionModel.weatherInspectionModel.UploadDataModelList)
            {
                inspection.weatherInspection.UploadDataList.Add(getUploadData(item));
            }

            inspection.weatherInspection.StormDetailsEditList= new List<StormDetails>();
            foreach (StormDetailsModel item in inspectionModel.weatherInspectionModel.StormDetailsModelEditList)
            {
                inspection.weatherInspection.StormDetailsEditList.Add(getStormEventData(item));
            }


            inspection.weatherInspection.UploadDataEditList = new List<UploadData>();
            foreach (UploadDataModel item in inspectionModel.weatherInspectionModel.UploadDataModelEditList)
            {
                inspection.weatherInspection.UploadDataEditList.Add(getUploadData(item));
            }


            return inspection;
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
            jUploadData.ParentID = uploadDataModel.ParentID;
            jUploadData.ID = uploadDataModel.ID;
            jUploadData.UploadData_ID = uploadDataModel.UploadData_ID == null ? 0 : uploadDataModel.UploadData_ID;
            jUploadData.ParentUploadData_ID = uploadDataModel.ParentUploadData_ID == null ? 0 : uploadDataModel.ParentUploadData_ID;
            return jUploadData;
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
        private static SiteInspection getSiteInspection(SiteInspectionModel siteInspectionModel)
        {
          
            SiteInspection siteinspection=new SiteInspection();
            
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

        public static bool Autoresponder(long Inspection_ID)
        {
            GeneralInspection GeneralInspection = new GeneralInspection();
            return GeneralInspection.Autoresponder(Inspection_ID);
        }

        public static bool WorkOrderCompletedAutoResponder(long Inspection_ID)
        {
            GeneralInspection GeneralInspection = new GeneralInspection();
            return GeneralInspection.WorkOrderCompletedAutoResponder(Inspection_ID);
        }

        public static Boolean deleteUploadData(long UploadData_ID)
        {

            return new Inspection().deleteUploadData(UploadData_ID);
        }

        public static Boolean deleteStormDetails(long Storm_ID)
        {
            return new Inspection().deleteStormDetails(Storm_ID);
        }

        public static bool setFirstReviewerInfo(long Inspection_ID,long Reviewer_ID,DateTime dt)
        {
            GeneralInspection GeneralInspection = new GeneralInspection();
            return GeneralInspection.setFirstReviewerInfo(Inspection_ID, Reviewer_ID, dt);
        }       

        public static bool setFirstReviewerLoginInfo(long User_ID, long Inspection_ID, long Location_ID)
        {
            return new GeneralInspection().setFirstReviewerLoginInfo(User_ID, Inspection_ID, Location_ID);
        }

        public static bool setFirstReviewerDownloadPDFLoginInfo(long Reviewer_ID, long Inspection_ID, long Location_ID)
        {
            return new GeneralInspection().setFirstReviewerDownloadPDFLoginInfo(Reviewer_ID, Inspection_ID, Location_ID);
        }
    }
}