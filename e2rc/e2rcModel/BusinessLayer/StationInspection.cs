using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.DataAccessLayer;
using e2rcModel.Common;
using System.Data;


namespace e2rcModel.BusinessLayer
{
   public class StationInspection
    {
        public long Inspection_ID { get; set; }
        public Int64 Location_ID { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerTitle { get; set; }
       
        public DateTime ReviewerOpenDateTime { get; set; }
        public string Name { get; set; }
        public GeneralInspection generalInspection { get; set; }
        public WeatherInspection weatherInspection { get; set; }
        public SiteInspection SiteInspection1 { get; set; }
        public SiteInspection SiteInspection2 { get; set; }
        public SiteInspection SiteInspection3 { get; set; }
        public SiteInspection SiteInspection4 { get; set; }
        public SiteInspection SiteInspection5 { get; set; }
        public SiteInspection SiteInspection6 { get; set; }
        public SiteInspection SiteInspection7 { get; set; }
        public SiteInspection SiteInspection8 { get; set; }
        public SiteInspection SiteInspection9 { get; set; }
        public SiteInspection SiteInspection10 { get; set; }
        public SiteInspection SiteInspection11 { get; set; }
        public SiteInspection SiteInspection12 { get; set; }
        public SiteInspection SiteInspection13 { get; set; }
        public SiteInspection SiteInspection14 { get; set; }
        public SiteInspection SiteInspection15 { get; set; }
        public SiteInspection SiteInspection16 { get; set; }
        public SiteInspection SiteInspection17 { get; set; }
        

        public DateTime Date { get; set; }

        public long Create()
        {
            Inspection_ID = generalInspection.Create();
            if (Inspection_ID > 0) { 
            weatherInspection.Create(Inspection_ID);
            SiteInspection1.Create(Inspection_ID);
            SiteInspection2.Create(Inspection_ID);
            SiteInspection3.Create(Inspection_ID);
            SiteInspection4.Create(Inspection_ID);
            SiteInspection5.Create(Inspection_ID);
            SiteInspection6.Create(Inspection_ID);
            SiteInspection7.Create(Inspection_ID);
            SiteInspection8.Create(Inspection_ID);
            SiteInspection9.Create(Inspection_ID);
            SiteInspection10.Create(Inspection_ID);
            SiteInspection11.Create(Inspection_ID);
            SiteInspection12.Create(Inspection_ID);
            SiteInspection13.Create(Inspection_ID);
            SiteInspection14.Create(Inspection_ID);
            SiteInspection15.Create(Inspection_ID);
            SiteInspection16.Create(Inspection_ID);
            SiteInspection17.Create(Inspection_ID);
            }
            return Inspection_ID;
        }

        public bool Edit(long Inspection_ID)
        {
            generalInspection.Edit();
            weatherInspection.Edit(Inspection_ID);
            SiteInspection1.Edit();
            SiteInspection2.Edit();
            SiteInspection3.Edit();
            SiteInspection4.Edit();
            SiteInspection5.Edit();
            SiteInspection6.Edit();
            SiteInspection7.Edit();
            SiteInspection8.Edit();
            SiteInspection9.Edit();
            SiteInspection10.Edit();
            SiteInspection11.Edit();
            SiteInspection12.Edit();
            SiteInspection13.Edit();
            SiteInspection14.Edit();
            SiteInspection15.Edit();
            SiteInspection16.Edit();
            SiteInspection17.Edit();
            
            return true;
        }

        public IEnumerable<dynamic> Locations(long User_ID)
        {
            List<dynamic> locations = new List<dynamic>();
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_InspectorLocation_List", new object[] { "@User_ID" }, new object[] { User_ID });

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Row in dataSet.Tables[0].Rows)
                {
                    locations.Add(new
                    {
                        Location_ID = Convert.ToInt64(Row["Location_ID"]),
                        Name = Convert.ToString(Row["ProjectName"])

                    });
                }
                return locations;

            }
            else
            {
                locations.Add(new
                {
                    Location_ID = 0,
                    Name = ""
                });
                return locations;
            }

        }

        public dynamic LocationDetails(long Location_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Location_Details", new object[] { "@Location_ID" }, new object[] { Location_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataset.Tables[0].Rows[0];
                return new
                {
                    locationName = Convert.ToString(row["Location"]),
                    LocationID = Convert.ToInt64(row["Location_ID"]),
                    F2_ID = Convert.ToString(row["F2_ID"]) == null ? "" : Convert.ToString(row["F2_ID"]),
                    TrackingNumber=Convert.ToString(row["TrackingNumber"])
                };
            }
            return new { locationName = "", LocationID = 0, F2_ID = 0 };
        }

        public dynamic InspectorDetails(long Inspector_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Inspector_Details", new object[] { "@Inspector_ID" }, new object[] { Inspector_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataset.Tables[0].Rows[0];
                return new
                {
                    InspectorName = Convert.ToString(row["InspectorName"]),
                    InspectorTitle = Convert.ToString(row["InspectorTitle"]),
                    ContactNumber = Convert.ToString(row["MobileNumber"]),
                    Qualification = Convert.ToString(row["Qualification"]),
                    InspectorEmail = Convert.ToString(row["Email"])
                };
            }
            return new { InspectorTitle = "", ContactNumber = 0 };
        }

        public dynamic InspectorDetails(string InspectorName)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_InspectorDetails", new object[] { "@Name" }, new object[] { InspectorName });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataset.Tables[0].Rows[0];
                return new
                {
                    Inspector_ID = Convert.ToInt64(row["Inspector_ID"]),
                    InspectorName = Convert.ToString(row["InspectorName"]),
                    InspectorTitle = Convert.ToString(row["InspectorTitle"]),
                    ContactNumber = Convert.ToString(row["MobileNumber"]),
                    Qualification = Convert.ToString(row["Qualification"]),
                    InspectorEmail = Convert.ToString(row["Email"]),
                    UploadSignPath = Convert.ToString(row["UploadSignPath"])
                };
            }
            return new { InspectorTitle = "", ContactNumber = 0 };
        }

        public StationInspection Single(long? Inspection_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_GeneralInspection_List", new object[] { "@Inspection_ID" }, new object[] { @Inspection_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                StationInspection inspection = new StationInspection();
                inspection.generalInspection = new GeneralInspection
                {
                    Inspection_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Inspection_ID"]),
                    Client_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Client_ID"]),
                    CustomerName = Convert.ToString(dataset.Tables[0].Rows[0]["CustomerName"]),
                    CompanyName = Convert.ToString(dataset.Tables[0].Rows[0]["CompanyName"]),
                    ProjectName = Convert.ToString(dataset.Tables[0].Rows[0]["ProjectName"]),
                    Tracking_No = Convert.ToString(dataset.Tables[0].Rows[0]["Tracking_No"]),
                    Location_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Location_ID"]),
                    Location = Convert.ToString(dataset.Tables[0].Rows[0]["Location"]),
                    Date = Convert.ToDateTime(dataset.Tables[0].Rows[0]["Date"]),
                    ModifiedDate = Convert.ToDateTime(dataset.Tables[0].Rows[0]["ModifiedDate"]),
                    StartTime = Convert.ToString(dataset.Tables[0].Rows[0]["StartTime"]).Substring(0, Convert.ToString(dataset.Tables[0].Rows[0]["StartTime"]).Length - 2),
                    StartTime_AmPm = Convert.ToString(dataset.Tables[0].Rows[0]["StartTime"]).Substring(Convert.ToString(dataset.Tables[0].Rows[0]["StartTime"]).Length - 2),
                    EndTime = Convert.ToString(dataset.Tables[0].Rows[0]["EndTime"]).Substring(0, Convert.ToString(dataset.Tables[0].Rows[0]["EndTime"]).Length - 2),
                    EndTime_AmPm = Convert.ToString(dataset.Tables[0].Rows[0]["EndTime"]).Substring(Convert.ToString(dataset.Tables[0].Rows[0]["EndTime"]).Length - 2),
                    Inspector_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Inspector_ID"]),
                    InspectorName = Convert.ToString(dataset.Tables[0].Rows[0]["InspectorName"]),
                    InspectorTitle = Convert.ToString(dataset.Tables[0].Rows[0]["InspectorTitle"]),
                    InspectorContact = Convert.ToString(dataset.Tables[0].Rows[0]["MobileNumber"]),
                    InspectorQualification = Convert.ToString(dataset.Tables[0].Rows[0]["Qualification"]),
                    PhaseValue = Convert.ToString(dataset.Tables[0].Rows[0]["PhaseValue"]),
                    PhaseClear = Convert.ToBoolean(Convert.ToString(dataset.Tables[0].Rows[0]["PhaseValue"]).Contains("Clear and Grub") ? true : false),
                    PhaseExcavations = Convert.ToBoolean(Convert.ToString(dataset.Tables[0].Rows[0]["PhaseValue"]).Contains("Excavation") ? true : false),
                    PhaseBuilding = Convert.ToBoolean(Convert.ToString(dataset.Tables[0].Rows[0]["PhaseValue"]).Contains("Building (vertical)") ? true : false),
                    PhaseRoughGrading = Convert.ToBoolean(Convert.ToString(dataset.Tables[0].Rows[0]["PhaseValue"]).Contains("Rough Grading") ? true : false),
                    PhaseInfrastructure = Convert.ToBoolean(Convert.ToString(dataset.Tables[0].Rows[0]["PhaseValue"]).Contains("Infrastructure") ? true : false),
                    PhaseFinalGrading = Convert.ToBoolean(Convert.ToString(dataset.Tables[0].Rows[0]["PhaseValue"]).Contains("Final Grading") ? true : false),
                    PhaseFinalStabilization = Convert.ToBoolean(Convert.ToString(dataset.Tables[0].Rows[0]["PhaseValue"]).Contains("Final Stabilization") ? true : false),
                    PhaseSitework = (dataset.Tables[0].Rows[0]["PhaseValue"]).ToString().Trim() == "Sitework" ? true : false,
                    PhaseOther = (dataset.Tables[0].Rows[0]["PhaseValue"]).ToString().Trim() == "Other" ? true : false,
                    InspectionType = Convert.ToString(dataset.Tables[0].Rows[0]["InspectionType"]),
                    CodeId = Convert.ToInt16(dataset.Tables[0].Rows[0]["Site_Classification"]),
                    Description = Convert.ToString(dataset.Tables[0].Rows[0]["Site_Des"]),
                    RainEventValue = (dataset.Tables[0].Rows[0]["RainEvent"]).ToString().Trim() != "" ? (dataset.Tables[0].Rows[0]["RainEvent"]).ToString().Trim() : "",
                    RainEventOtherValue = (dataset.Tables[0].Rows[0]["RainEventOther"]).ToString().Trim() != "" ? (dataset.Tables[0].Rows[0]["RainEventOther"]).ToString().Trim() : "",
                    RainEvent = (dataset.Tables[0].Rows[0]["RainEvent"]).ToString().Trim() == "" ? false : true,
                    RainEventOther = (dataset.Tables[0].Rows[0]["RainEventOther"]).ToString().Trim() == "" ? false : true,
                    UploadSignPath = (dataset.Tables[0].Rows[0]["UploadSignPath"]).ToString() == "" ? string.Empty : (dataset.Tables[0].Rows[0]["UploadSignPath"]).ToString()
                };
                WeatherInspection wetherInspection = new WeatherInspection();

                if (dataset != null && dataset.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[3].Rows)
                    {
                        wetherInspection.UploadDataList.Add(new UploadData
                        {
                            ItemC1_ID = Convert.ToInt32(row["c1_ItemID"]),
                            ItemC1_Name = Convert.ToString(row["C1_Name"]),

                            ItemC2_ID = Convert.ToInt32(row["C2_ItemID"]),
                            ItemC2_Name = Convert.ToString(row["C2_Name"]),

                            ItemC3_ID = Convert.ToInt32(row["C3_ItemID"]),
                            ItemC3_Name = Convert.ToString(row["C3_Name"]),
                            Station = Convert.ToString(row["Station"]),
                            LtRt = Convert.ToString(row["LtRt"]),
                            Location = Convert.ToString(row["ud_Location"]),
                            LengthPPP = Convert.ToInt32(row["LengthPPP"]),

                            UOM_ID = Convert.ToInt32(row["UOM_ID"]),
                            UOM = Convert.ToString(row["UOM"]),
                            UploadData_ID = Convert.ToInt64(row["UploadData_ID"]),
                            ParentUploadData_ID = Convert.ToInt64(row["ParentUploadData_ID"]),
                            UploadImagePath = Convert.ToString(row["UploadImagePath"]),
                            ImageName = Convert.ToString(row["ImageName"]),
                            Status = Convert.ToString(row["Status"])
                        });
                    }
                }
                if (dataset != null && dataset.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[2].Rows)
                    {
                        wetherInspection.StormDetailslList.Add(new StormDetails
                        {
                            StormDateTime = Convert.ToDateTime(row["StormDateTime"]),
                            StormDuration = Convert.ToString(row["StormDuration"]),
                            Amount = Convert.ToDecimal(row["Amount"]),
                            Storm_ID = Convert.ToInt64(row["Storm_ID"]),
                            ParentStorm_ID = Convert.ToInt64(row["ParentStorm_ID"]),
                        });
                    }
                }  
                
               
                if (dataset.Tables[1].Rows.Count > 0)
                {
                    //if (Convert.ToString(dataset.Tables[1].Rows[0]["StromEvent"]) != null)
                     if (Convert.ToString(dataset.Tables[1].Rows[0]["StromEvent"]) != "")
                    {
                        wetherInspection.StromEventYes = true;
                        wetherInspection.StromEvent = Convert.ToString(dataset.Tables[1].Rows[0]["StromEvent"]);
                    }
                    else
                    {
                        wetherInspection.StromEventNo = true;
                        wetherInspection.StromEvent = string.Empty;
                    }
                    wetherInspection.WeatherTime = Convert.ToString(dataset.Tables[1].Rows[0]["WeatherTime"]);
                    wetherInspection.Temperature = float.Parse(Convert.ToString(dataset.Tables[1].Rows[0]["Temperature"]));
                    wetherInspection.LastInspection = Convert.ToBoolean(dataset.Tables[1].Rows[0]["LastInspection"]);
                    if (Convert.ToBoolean(dataset.Tables[1].Rows[0]["LastInspection"]))
                    {
                        wetherInspection.LastInspectionYes = true;
                    }
                    else
                    {
                        wetherInspection.LastInspectionNo = true;
                    }
                    if (Convert.ToBoolean(dataset.Tables[1].Rows[0]["InspectionOccuring"]))
                    {
                        wetherInspection.InspectionOccuringYes = true;
                        wetherInspection.InspectionOccuringYesValue = Convert.ToString(dataset.Tables[1].Rows[0]["InspectionOccuringYes"]);
                    }
                    else
                    {
                        wetherInspection.InspectionOccuringNo = true;
                        wetherInspection.InspectionOccuringYesValue = string.Empty;
                    }
                    wetherInspection.UnsafeInspection = Convert.ToBoolean(dataset.Tables[1].Rows[0]["UnsafeInspection"]);

                    if (Convert.ToBoolean(dataset.Tables[1].Rows[0]["UnsafeInspection"]))
                    {
                        wetherInspection.UnsafeInspectionYes = true;
                        wetherInspection.UnsafeInspectionValue = Convert.ToString(dataset.Tables[1].Rows[0]["UnsafeInspectionYes"]);
                    }
                    else
                    {
                        wetherInspection.UnsafeInspectionNo = true;
                        wetherInspection.UnsafeInspectionValue = string.Empty;
                    }
                }
                else
                {
                    inspection.weatherInspection = new WeatherInspection();
                }
                inspection.weatherInspection = wetherInspection;
                if (dataset.Tables[4].Rows.Count > 0)
                {
                    inspection.SiteInspection1 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[0]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[0]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[0]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[0]["ActionRequired"]) == true,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[0]["ActionRequired"]) == false,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[0]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[0]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[0]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[0]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection1.AreaInspectedNA = true;
                        inspection.SiteInspection1.AreaInspectedNo = false;
                        inspection.SiteInspection1.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[0]["AreaInspected"]))
                    {
                        inspection.SiteInspection1.AreaInspectedNA = false;
                        inspection.SiteInspection1.AreaInspectedNo = false;
                        inspection.SiteInspection1.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection1.AreaInspectedNA = false;
                        inspection.SiteInspection1.AreaInspectedNo = true;
                        inspection.SiteInspection1.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection1 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 1)
                {
                    inspection.SiteInspection2 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[1]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[1]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[1]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[1]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[1]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[1]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[1]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[1]["SiteInspection_ID"])
                    };

                    if (dataset.Tables[4].Rows[1]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection2.AreaInspectedNA = true;
                        inspection.SiteInspection2.AreaInspectedNo = false;
                        inspection.SiteInspection2.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[1]["AreaInspected"]))
                    {
                        inspection.SiteInspection2.AreaInspectedNA = false;
                        inspection.SiteInspection2.AreaInspectedNo = false;
                        inspection.SiteInspection2.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection2.AreaInspectedNA = false;
                        inspection.SiteInspection2.AreaInspectedNo = true;
                        inspection.SiteInspection2.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection2 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 2)
                {
                    inspection.SiteInspection3 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[2]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[2]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[2]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[2]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[2]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[2]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[2]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[2]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[2]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection3.AreaInspectedNA = true;
                        inspection.SiteInspection3.AreaInspectedNo = false;
                        inspection.SiteInspection3.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[2]["AreaInspected"]))
                    {
                        inspection.SiteInspection3.AreaInspectedNA = false;
                        inspection.SiteInspection3.AreaInspectedNo = false;
                        inspection.SiteInspection3.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection3.AreaInspectedNA = false;
                        inspection.SiteInspection3.AreaInspectedNo = true;
                        inspection.SiteInspection3.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection3 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 3)
                {
                    inspection.SiteInspection4 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[3]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[3]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[3]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[3]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[3]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[3]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[3]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[3]["SiteInspection_ID"])
                    };

                    if (dataset.Tables[4].Rows[3]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection4.AreaInspectedNA = true;
                        inspection.SiteInspection4.AreaInspectedNo = false;
                        inspection.SiteInspection4.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[3]["AreaInspected"]))
                    {
                        inspection.SiteInspection4.AreaInspectedNA = false;
                        inspection.SiteInspection4.AreaInspectedNo = false;
                        inspection.SiteInspection4.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection4.AreaInspectedNA = false;
                        inspection.SiteInspection4.AreaInspectedNo = true;
                        inspection.SiteInspection4.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection4 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 4)
                {
                    inspection.SiteInspection5 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[4]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[4]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[4]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[4]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[4]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[4]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[4]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[4]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[4]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection5.AreaInspectedNA = true;
                        inspection.SiteInspection5.AreaInspectedNo = false;
                        inspection.SiteInspection5.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[4]["AreaInspected"]))
                    {
                        inspection.SiteInspection5.AreaInspectedNA = false;
                        inspection.SiteInspection5.AreaInspectedNo = false;
                        inspection.SiteInspection5.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection5.AreaInspectedNA = false;
                        inspection.SiteInspection5.AreaInspectedNo = true;
                        inspection.SiteInspection5.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection5 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 5)
                {
                    inspection.SiteInspection6 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[5]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[5]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[5]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[5]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[5]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[5]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[5]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[5]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[5]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection6.AreaInspectedNA = true;
                        inspection.SiteInspection6.AreaInspectedNo = false;
                        inspection.SiteInspection6.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[5]["AreaInspected"]))
                    {
                        inspection.SiteInspection6.AreaInspectedNA = false;
                        inspection.SiteInspection6.AreaInspectedNo = false;
                        inspection.SiteInspection6.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection6.AreaInspectedNA = false;
                        inspection.SiteInspection6.AreaInspectedNo = true;
                        inspection.SiteInspection6.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection6 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 6)
                {
                    inspection.SiteInspection7 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[6]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[6]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[6]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[6]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[6]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[6]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[6]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[6]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[6]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection7.AreaInspectedNA = true;
                        inspection.SiteInspection7.AreaInspectedNo = false;
                        inspection.SiteInspection7.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[6]["AreaInspected"]))
                    {
                        inspection.SiteInspection7.AreaInspectedNA = false;
                        inspection.SiteInspection7.AreaInspectedNo = false;
                        inspection.SiteInspection7.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection7.AreaInspectedNA = false;
                        inspection.SiteInspection7.AreaInspectedNo = true;
                        inspection.SiteInspection7.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection7 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 7)
                {
                    inspection.SiteInspection8 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[7]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[7]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[7]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[7]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[7]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[7]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[7]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[7]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[7]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection8.AreaInspectedNA = true;
                        inspection.SiteInspection8.AreaInspectedNo = false;
                        inspection.SiteInspection8.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[7]["AreaInspected"]))
                    {
                        inspection.SiteInspection8.AreaInspectedNA = false;
                        inspection.SiteInspection8.AreaInspectedNo = false;
                        inspection.SiteInspection8.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection8.AreaInspectedNA = false;
                        inspection.SiteInspection8.AreaInspectedNo = true;
                        inspection.SiteInspection8.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection8 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 8)
                {
                    inspection.SiteInspection9 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[8]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[8]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[8]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[8]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[8]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[8]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[8]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[8]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[8]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection9.AreaInspectedNA = true;
                        inspection.SiteInspection9.AreaInspectedNo = false;
                        inspection.SiteInspection9.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[8]["AreaInspected"]))
                    {
                        inspection.SiteInspection9.AreaInspectedNA = false;
                        inspection.SiteInspection9.AreaInspectedNo = false;
                        inspection.SiteInspection9.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection9.AreaInspectedNA = false;
                        inspection.SiteInspection9.AreaInspectedNo = true;
                        inspection.SiteInspection9.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection9 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 9)
                {
                    inspection.SiteInspection10 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[9]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[9]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[9]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[9]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[9]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[9]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[9]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[9]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[9]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection10.AreaInspectedNA = true;
                        inspection.SiteInspection10.AreaInspectedNo = false;
                        inspection.SiteInspection10.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[9]["AreaInspected"]))
                    {
                        inspection.SiteInspection10.AreaInspectedNA = false;
                        inspection.SiteInspection10.AreaInspectedNo = false;
                        inspection.SiteInspection10.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection10.AreaInspectedNA = false;
                        inspection.SiteInspection10.AreaInspectedNo = true;
                        inspection.SiteInspection10.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection10 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 10)
                {
                    inspection.SiteInspection11 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[10]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[10]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[10]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[10]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[10]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[10]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[10]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[10]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[10]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection11.AreaInspectedNA = true;
                        inspection.SiteInspection11.AreaInspectedNo = false;
                        inspection.SiteInspection11.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[10]["AreaInspected"]))
                    {
                        inspection.SiteInspection11.AreaInspectedNA = false;
                        inspection.SiteInspection11.AreaInspectedNo = false;
                        inspection.SiteInspection11.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection11.AreaInspectedNA = false;
                        inspection.SiteInspection11.AreaInspectedNo = true;
                        inspection.SiteInspection11.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection11 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 11)
                {
                    inspection.SiteInspection12 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[11]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[11]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[11]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[11]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[11]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[11]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[11]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[11]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[11]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection12.AreaInspectedNA = true;
                        inspection.SiteInspection12.AreaInspectedNo = false;
                        inspection.SiteInspection12.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[11]["AreaInspected"]))
                    {
                        inspection.SiteInspection12.AreaInspectedNA = false;
                        inspection.SiteInspection12.AreaInspectedNo = false;
                        inspection.SiteInspection12.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection12.AreaInspectedNA = false;
                        inspection.SiteInspection12.AreaInspectedNo = true;
                        inspection.SiteInspection12.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection12 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 12)
                {
                    inspection.SiteInspection13 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[12]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[12]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[12]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[12]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[12]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[12]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[12]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[12]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[12]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection13.AreaInspectedNA = true;
                        inspection.SiteInspection13.AreaInspectedNo = false;
                        inspection.SiteInspection13.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[12]["AreaInspected"]))
                    {
                        inspection.SiteInspection13.AreaInspectedNA = false;
                        inspection.SiteInspection13.AreaInspectedNo = false;
                        inspection.SiteInspection13.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection13.AreaInspectedNA = false;
                        inspection.SiteInspection13.AreaInspectedNo = true;
                        inspection.SiteInspection13.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection13 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 13)
                {
                    inspection.SiteInspection14 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[13]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[13]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[13]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[13]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[13]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[13]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[13]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[13]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[13]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection14.AreaInspectedNA = true;
                        inspection.SiteInspection14.AreaInspectedNo = false;
                        inspection.SiteInspection14.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[13]["AreaInspected"]))
                    {
                        inspection.SiteInspection14.AreaInspectedNA = false;
                        inspection.SiteInspection14.AreaInspectedNo = false;
                        inspection.SiteInspection14.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection14.AreaInspectedNA = false;
                        inspection.SiteInspection14.AreaInspectedNo = true;
                        inspection.SiteInspection14.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection14 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 14)
                {
                    inspection.SiteInspection15 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[14]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[14]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[14]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[14]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[14]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[14]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[14]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[14]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[14]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection15.AreaInspectedNA = true;
                        inspection.SiteInspection15.AreaInspectedNo = false;
                        inspection.SiteInspection15.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[14]["AreaInspected"]))
                    {
                        inspection.SiteInspection15.AreaInspectedNA = false;
                        inspection.SiteInspection15.AreaInspectedNo = false;
                        inspection.SiteInspection15.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection15.AreaInspectedNA = false;
                        inspection.SiteInspection15.AreaInspectedNo = true;
                        inspection.SiteInspection15.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection15 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 15)
                {
                    inspection.SiteInspection16 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[15]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[15]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[15]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[15]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[15]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[15]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[15]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[15]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[15]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection16.AreaInspectedNA = true;
                        inspection.SiteInspection16.AreaInspectedNo = false;
                        inspection.SiteInspection16.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[15]["AreaInspected"]))
                    {
                        inspection.SiteInspection16.AreaInspectedNA = false;
                        inspection.SiteInspection16.AreaInspectedNo = false;
                        inspection.SiteInspection16.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection16.AreaInspectedNA = false;
                        inspection.SiteInspection16.AreaInspectedNo = true;
                        inspection.SiteInspection16.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection16 = new SiteInspection();
                }
                if (dataset.Tables[4].Rows.Count > 16)
                {
                    inspection.SiteInspection17 = new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(dataset.Tables[4].Rows[16]["InspectionQuestion_ID"]),
                        //AreaInspected = Convert.ToBoolean(dataset.Tables[4].Rows[16]["AreaInspected"]),
                        ActionRequired = Convert.ToBoolean(dataset.Tables[4].Rows[16]["ActionRequired"]),
                        ActionRequiredYes = Convert.ToBoolean(dataset.Tables[4].Rows[16]["ActionRequired"]) == true ? true : false,
                        ActionRequiredNo = Convert.ToBoolean(dataset.Tables[4].Rows[16]["ActionRequired"]) == true ? false : true,
                        Notes = Convert.ToString(dataset.Tables[4].Rows[16]["Notes"]),
                        Question = Convert.ToString(dataset.Tables[4].Rows[16]["Question"]),
                        SiteInspection_ID = Convert.ToInt64(dataset.Tables[4].Rows[16]["SiteInspection_ID"])
                    };
                    if (dataset.Tables[4].Rows[16]["AreaInspected"] is DBNull)
                    {
                        inspection.SiteInspection17.AreaInspectedNA = true;
                        inspection.SiteInspection17.AreaInspectedNo = false;
                        inspection.SiteInspection17.AreaInspectedYes = false;
                    }
                    else if (Convert.ToBoolean(dataset.Tables[4].Rows[16]["AreaInspected"]))
                    {
                        inspection.SiteInspection17.AreaInspectedNA = false;
                        inspection.SiteInspection17.AreaInspectedNo = false;
                        inspection.SiteInspection17.AreaInspectedYes = true;
                    }
                    else
                    {
                        inspection.SiteInspection17.AreaInspectedNA = false;
                        inspection.SiteInspection17.AreaInspectedNo = true;
                        inspection.SiteInspection17.AreaInspectedYes = false;
                    }
                }
                else
                {
                    inspection.SiteInspection17 = new SiteInspection();
                }
              
                if (dataset.Tables[5].Rows.Count > 0)
                {
                    inspection.ReviewerName = dataset.Tables[5].Rows[0]["ReviewerName"].ToString();
                    inspection.ReviewerOpenDateTime = Convert.ToDateTime(dataset.Tables[5].Rows[0]["ReviewerOpenDateTime"]);
                    inspection.ReviewerTitle = Convert.ToString(dataset.Tables[5].Rows[0]["ReviewerTitle"]);
                }
                return inspection;
            }
            else return null;
        }

        public bool deleteUploadData(long UploadData_ID)
        {
            return new DAL().Update("sp_deleteUploadDataID_CRUD",
                    new object[] { "@UploadData_ID" },
                    new object[] { UploadData_ID }
                    );
        }
        public int checkInspectionReportAlreadyExist(Int64 Location_ID, DateTime date, string FormName)
        {
            return new DAL().Update("sp_InspectionReportExist",
                    new object[] { "@Location_ID", "@date", "@FormName" },
                    new object[] { Location_ID, date, FormName }, "@ProjectExist",0 );
                   
        }

        public dynamic getClientDetailsByInspectionId(long? Inspection_ID) 
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_client_details_get_by_inspID", new object[] { "@Inspection_ID" }, new object[] { @Inspection_ID });

            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataset.Tables[0].Rows[0];
                return new
                {
                    ClientID = Convert.ToString(row["Client_ID"]),
                    CompanyName = Convert.ToString(row["CompanyName"]),
                    UploadLogoPath = Convert.ToString(row["Upload_Logo_Path"]),                    
                };
            }
            return new { Upload_Logo_Path = "", CompanyName = "" }; ;            
        }
       
    
    }
}
