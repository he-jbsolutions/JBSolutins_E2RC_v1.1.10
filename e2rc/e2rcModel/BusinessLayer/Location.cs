using System;
using System.Collections.Generic;
using System.Data;
using e2rcModel.Common;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
    public class Location : Address
    {
        public string Name { get; set; }
        public string workorder { get; set; }
        public Int64 F1_ID { get; set; }

        public string F2_ID { get; set; }

        public Boolean IsRequired { get; set; }

        public long? Location_ID { get; set; }

        public string TrackingNumber { get; set; }

        public long ProjectType_ID { get; set; }
        public string ProjectType { get; set; }
        public long? Client_ID { get; set; }

        public string Customer_Name { get; set; }
        public string Company_Name { get; set; }

        public long? CreatedBy_ID { get; set; }

        public bool IsActive { get; set; }

        public long? User_ID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int days { get; set; }
        //public string Email1 { get; set; }
        //public string Email2 { get; set; }
        public string InspectionReportEmails { get; set; }
        public string WorkOrdersEmails { get; set; }
        public string Threedaynoticeemails { get; set; }
        public string Fivedaynoticeemails { get; set; }
        public string Sevendaynoticeemails { get; set; }
        public string MaintainAction { get; set; }
        public string InspectionFrequency { get; set; }

        public string sInspector_ID { get; set; }
        public string sReviewer_ID { get; set; }

        public override bool Create()
        {
            return new DAL().Insert("sp_Location_CRUD",
                                     new object[] { "@Action", "@Name", "@MailingAddress", "@State_ID", "@City",
                                                    "@ZipCode", "@CreatedBy", "@F1_ID", "@F2_ID", "@InspectionReportEmails", "@WorkOrdersEmails", "@Threedaynoticeemails", "@Fivedaynoticeemails", "@Sevendaynoticeemails", "@Maintenance_Action_Needed_by", "@IsActive","@Client_ID", "@MailingAddress2", "@ProjectType_ID", "@InspectionFrequency", "@TrackingNumber", "@User_ID", "@lstInspectorID", "@lstReviewerID"},

                                     new object[] { Actions.INSERT.ToString(), Name, MailingAddress, State.State_ID, City,
                                                   ZipCode, CreatedBy_ID, F1_ID, F2_ID, InspectionReportEmails, WorkOrdersEmails, Threedaynoticeemails, Fivedaynoticeemails, Sevendaynoticeemails, MaintainAction, IsActive, Client_ID, MailingAddress2, ProjectType_ID, InspectionFrequency, TrackingNumber, User_ID, sInspector_ID, sReviewer_ID});
        }

        public override bool Edit()
        {
            return new DAL().Insert("sp_Location_CRUD",
                                    new object[] { "@Action", "@Name", "@MailingAddress", "@State_ID", "@City",
                                                    "@ZipCode", "@CreatedBy", "@Location_ID", "@F1_ID", "@F2_ID", "@InspectionReportEmails", "@WorkOrdersEmails", "@Threedaynoticeemails", "@Fivedaynoticeemails", "@Sevendaynoticeemails", "@Maintenance_Action_Needed_by", "@IsActive", "@Client_ID", "@MailingAddress2", "@ProjectType_ID", "@InspectionFrequency", "@TrackingNumber", "@User_ID", "@lstInspectorID", "@lstReviewerID"},

                                    new object[] {Actions.UPDATE.ToString(), Name, MailingAddress, State.State_ID, City,
                                                     ZipCode, CreatedBy_ID, Location_ID, F1_ID, F2_ID, InspectionReportEmails, WorkOrdersEmails, Threedaynoticeemails, Fivedaynoticeemails, Sevendaynoticeemails, MaintainAction, IsActive, Client_ID, MailingAddress2, ProjectType_ID, InspectionFrequency, TrackingNumber, User_ID, sInspector_ID, sReviewer_ID });
        }

        public override bool Delete()
        {
            return new DAL().Delete("sp_Location_CRUD",
                                               new object[] { "@Action", "@Location_ID", "@CreatedBy" },
                                               new object[] { Actions.DELETE.ToString(), Location_ID, CreatedBy_ID });
        }

        public IEnumerable<Location> List(long CreatedBy_ID, string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Location_List", new object[] { "@CreatedBy", "@view" }, new object[] { CreatedBy_ID, view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Location> locationList = new List<Location>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    locationList.Add(new Location
                    {
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        Name = Convert.ToString(row["Name"]),
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        Customer_Name = Convert.ToString(row["CustomerName"]),
                        Company_Name = Convert.ToString(row["CompanyName"]),
                        Address_ID = Convert.ToInt64(row["Address_ID"]),
                        City = Convert.ToString(row["City"]),
                        MailingAddress = Convert.ToString(row["MailingAddress"]),
                        ZipCode = Convert.ToString(row["ZipCode"]),
                        F1_ID = (Convert.ToInt64(row["F1_ID"])) == null ? 0 : Convert.ToInt64(row["F1_ID"]),
                        F2_ID = (Convert.ToString(row["F2_ID"])) == string.Empty ? "" : Convert.ToString(row["F2_ID"]),
                        IsRequired = Convert.ToBoolean(row["IsRequired"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        State = new State
                        {
                            State_ID = Convert.ToByte(row["State_ID"]),
                            Name = Convert.ToString(row["StateName"]),
                            Code = Convert.ToString(row["Code"])
                        }
                    });
                }
                return locationList;
            }
            return null;
        }

        public IEnumerable<Location> List(string search, long CreatedBy_ID, string view)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Location_List",
                                                                new object[] { "@Search_By", "@CreatedBy", "@view" },
                                                                new object[] { search, CreatedBy_ID, view });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Location> locationList = new List<Location>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    locationList.Add(new Location
                    {
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        Name = Convert.ToString(row["Name"]),
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        Customer_Name = Convert.ToString(row["CustomerName"]),
                        Company_Name = Convert.ToString(row["CompanyName"]),
                        Address_ID = Convert.ToInt64(row["Address_ID"]),
                        City = Convert.ToString(row["City"]),
                        MailingAddress = Convert.ToString(row["MailingAddress"]),
                        ZipCode = Convert.ToString(row["ZipCode"]),
                        State = new State
                        {
                            State_ID = Convert.ToByte(row["State_ID"]),
                            Name = Convert.ToString(row["StateName"]),
                            Code = Convert.ToString(row["Code"])
                        }
                    });
                }
                return locationList;
            }
            return null;
        }

        public IEnumerable<string> AutoList(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Location_SearchName",
                                                                new object[] { "@Search_By", "@CreatedBy" },
                                                                new object[] { search, CreatedBy_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> LocationName = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    LocationName.Add(Convert.ToString(row["Name"]));
                }
                return LocationName;
            }
            return null;
        }

        public Location Single(long Location_ID, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getLocation_List",
                                                               new object[] { "@Location_ID", "@CreatedBy" },
                                                               new object[] { Location_ID, CreatedBy_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                return (new Location
                {
                    Location_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Location_ID"]),
                    Name = Convert.ToString(dataset.Tables[0].Rows[0]["Name"]),
                    Client_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Client_ID"]),
                    Customer_Name = Convert.ToString(dataset.Tables[0].Rows[0]["CustomerName"]),
                    Company_Name = Convert.ToString(dataset.Tables[0].Rows[0]["CompanyName"]),
                    MailingAddress = Convert.ToString(dataset.Tables[0].Rows[0]["MailingAddress"]),
                    F1_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["F1_ID"]),
                    F2_ID = Convert.ToString(dataset.Tables[0].Rows[0]["F2_ID"]),
                    InspectionReportEmails = Convert.ToString(dataset.Tables[0].Rows[0]["InspectionReportEmails"]),
                    WorkOrdersEmails = Convert.ToString(dataset.Tables[0].Rows[0]["WorkOrdersEmails"]),
                    Threedaynoticeemails = Convert.ToString(dataset.Tables[0].Rows[0]["Threedaynoticeemails"]),
                    Fivedaynoticeemails = Convert.ToString(dataset.Tables[0].Rows[0]["Fivedaynoticeemails"]),
                    Sevendaynoticeemails = Convert.ToString(dataset.Tables[0].Rows[0]["Sevendaynoticeemails"]),
                    MaintainAction = Convert.ToString(dataset.Tables[0].Rows[0]["Maintenance_Action_Needed_by"]),
                    IsRequired = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsRequired"]),
                    IsActive = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsActive"]),
                   // IsAssignInspector = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsAssignInspector"]),
                    //IsAssignReviewer = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsAssignReviewer"]),
                    sInspector_ID = (Convert.ToString(dataset.Tables[0].Rows[0]["lInspector_ID"])),
                    sReviewer_ID = Convert.ToString(dataset.Tables[0].Rows[0]["lReviewer_ID"]),
                    State = new State
                    {
                        State_ID = Convert.ToByte(dataset.Tables[0].Rows[0]["State_ID"]),
                        Name = Convert.ToString(dataset.Tables[0].Rows[0]["StateName"]),
                        Code = Convert.ToString(dataset.Tables[0].Rows[0]["Code"])
                    },
                    City = Convert.ToString(dataset.Tables[0].Rows[0]["City"]),
                    ZipCode = Convert.ToString(dataset.Tables[0].Rows[0]["ZipCode"]),
                    MailingAddress2 = Convert.ToString(dataset.Tables[0].Rows[0]["MailingAddress2"]),
                    ProjectType_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["ProjectType_ID"]),
                    ProjectType = Convert.ToString(dataset.Tables[0].Rows[0]["ProjectType"]),
                    InspectionFrequency = Convert.ToString(dataset.Tables[0].Rows[0]["InspectionFrequency"]),
                    TrackingNumber = Convert.ToString(dataset.Tables[0].Rows[0]["TrackingNumber"]),
                    CreatedBy_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["CreatedBy"]),
                    

                });
            }
            return null;
        }


        public IEnumerable<Location> Locations
        {
            get
            {
                DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_LocationName_List");

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<Location> locations = new List<Location>();

                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        locations.Add(new Location
                        {
                            Location_ID = Convert.ToInt64(Row["Location_ID"]),
                            Name = Convert.ToString(Row["Name"])
                        });
                    }
                    return locations;

                }
                return null;
            }
        }



        public IEnumerable<Location> Projecttypes
        {
            get
            {
                DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_ProjectType_List");

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<Location> locations = new List<Location>();

                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        locations.Add(new Location
                        {
                            ProjectType_ID = Convert.ToInt64(Row["ProjectType_ID"]),
                            ProjectType = Convert.ToString(Row["ProjectType"])
                        });
                    }
                    return locations;

                }
                return null;
            }
        }

        public bool IsTrackingNumberAvailable(string TrackingNumber, long? Location_ID)
        {
            object IsAvailable = new DataAccessLayer.DAL().ExecuteScalar("sp_IsTrackingNumberAvailable",
                new object[] { "@TrackingNumber", "@Location_ID" }, new object[] { TrackingNumber, Location_ID });
            return (int)IsAvailable == 1 ? false : true;
        }


        public IEnumerable<dynamic> GetClientDetails(long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_LocationwiseClient_List", new object[] { "@User_ID" }, new object[] { User_ID });
            List<dynamic> ClientList = new List<dynamic>();
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ClientList.Add(new
                    {
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        Name = Convert.ToString(row["CompanyName"]),
                    });
                }
                return ClientList;
            }
            else
            {
                ClientList.Add(new { Client_ID = 0, Name = "" });
                return ClientList;
            }
        }


        public bool UpdateLocationStatus(long Location_ID, string view)
        {
            return new DAL().Update("sp_UpdateLocationStatus",
                  new object[] {"@Location_ID","@view"
                },
                  new object[] {Location_ID ,view              
                });
        }

        public IEnumerable<Location> DisplayClientWiseProject(long Client_ID)
        {
            //change where condition
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_DisplayClientWiseLocation_List", new object[] { "@Client_ID" }, new object[] { Client_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<Location> locationList = new List<Location>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    locationList.Add(new Location
                    {
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        ModifiedDate = (row["ModifiedDate"] is DBNull) ?null : (DateTime ?)row["ModifiedDate"],
                        Name = Convert.ToString(row["Name"]),
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        Company_Name = Convert.ToString(row["CompanyName"]),
                        workorder = Convert.ToString(row["workorder"]),
                        Address_ID = Convert.ToInt64(row["Address_ID"]),
                        City = Convert.ToString(row["City"]),                        
                        days =Convert.ToString(row["workorder"])=="Complete"  ? 0 : Convert.ToInt16(row["DaysPastDue"]) < 0 ? 0 : Convert.ToInt16(row["DaysPastDue"]),
                        MailingAddress = Convert.ToString(row["MailingAddress"]),
                        ZipCode = Convert.ToString(row["ZipCode"]),
                        State = new State
                        {
                            State_ID = Convert.ToByte(row["State_ID"]),
                            Name = Convert.ToString(row["StateName"]),
                            Code = Convert.ToString(row["Code"])
                        }
                    });
                }
                return locationList;
            }
            return null;
        }
    }
}
