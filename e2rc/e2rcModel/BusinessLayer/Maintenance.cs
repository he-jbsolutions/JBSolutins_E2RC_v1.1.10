using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.DataAccessLayer;
using e2rcModel.Common;
using System.Data;
using System.Dynamic;

namespace e2rcModel.BusinessLayer
{
    public class Maintenance
    {
        public Int64 User_ID;
        public String Location;
        public String ProjectName;
        public Int64 Inspection_ID;
        public string ClientName;
        public String InspectorName;
        public Int64 UploadData_ID;
        public String MaintenanceNeeded;
        public String ActionRequired;
        public DateTime InspectionDate;
        public DateTime DueDate;
        public DateTime CreatedDate;
        public string date;
        public string DisplayDueDate;
        public string DisplayInspectionDate;
        public string ActionStatus;
        public string MaintenanceStatus;
        public string ActionCompletedDate;
        public string MaintenanceCompletedDate;
         public string ActionModifiedBy;
         public string ModifiedByMaintenance;
         public string InspectionReportEmails;
         public string WorkOrdersEmails;
         public Int16 OpenWorkOrder;
         //public string Threedaynoticeemails;
         //public string Fivedaynoticeemails;
         //public string Sevendaynoticeemails;
         public string SEControls;
         public int Quantity;
         public string UPLocation;
         public string UOM;
         public string UploadImagePath;
        public int days;
        public string Tracking_No;
        public string FormName;
        public string InspectorEmail;
        public Int64 Inspection_Id ;       
        public String City;
        public String MailingAddress;
        public String StateName;
        public String ZipCode;
        public String CompanyName;
        public Int64 ProjectManager_ID;
        public Int64 Assign_ID;
        public Int64 Location_ID;
        public String IsAssignLocation;
        public string ModifiedDate;
        public string PhoneNumber;
        public string franchiseName;
        public string Username;
        public bool status;
        public string FranchiseCompany;
        public Int64 Client_ID;
        public Int64 Franchise_ID;
        public Int64 Reviewer_ID;
        public bool ISCorrectiveReport { get; set; } 
        
        public Int16 NoOfActiveFranchise {get;set;}
       

        public IEnumerable<Maintenance> getActionMaintenanceList(string role, long User_ID)
        {
            List<Maintenance> ActionMaintenanceList = new List<Maintenance>();
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getActionMaintenanceDetails", new object[] { "@User_ID", "@Role" }, new object[] { User_ID, role });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ActionMaintenanceList.Add(new Maintenance
                   {
                       //Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                       Location_ID = Convert.ToInt64(row["Location_ID"]),
                       Client_ID = Convert.ToInt64(row["Client_ID"]),
                       ProjectName = Convert.ToString(row["ProjectName"]),
                       //days = Convert.ToInt16(row["DaysPastDue"]) < 0 ? 0 : Convert.ToInt16(row["DaysPastDue"]),
                       //InspectorEmail = Convert.ToString(row["InspectorEmail"]),
                       //Location = Convert.ToString(row["Location"]),
                       //ClientName = Convert.ToString(row["ClientName"]),
                       //CompanyName = Convert.ToString(row["CompanyName"]),
                       //InspectorName = Convert.ToString(row["InspectorName"]),
                       //InspectionDate = Convert.ToDateTime(row["Date"]),
                       //DueDate = Convert.ToDateTime(row["DueDate"]),
                       //Email_1 = Convert.ToString(row["Email_1"]),
                       //Email_2 = Convert.ToString(row["Email_2"]),
                       //CreatedDate = Convert.ToDateTime(row["createdDate"]),
                       Tracking_No = Convert.ToString(row["Tracking_No"]),
                       OpenWorkOrder = Convert.ToInt16(row["OpenWorkOrder"]), 
                   });
                }
                return ActionMaintenanceList;
            }            
            return null;
        }

        public dynamic GenrateMaintenanceWorkOrder(long Inspection_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getInspectionDetailsForMaintenanceWorkOrder", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                var Uplaoddatalist = from row in dataset.Tables[0].AsEnumerable()
                                     select new Maintenance
                                     {
                                         UploadData_ID = row.Field<Int64>("UploadData_ID"),
                                         Inspection_ID=row.Field<Int64>("Inspection_ID"),
                                         FormName = row.Field<string>("FormName"),
                                         MaintenanceNeeded = row.Field<string>("MaintenanceNeeded"),                                                                              
                                         DisplayDueDate = Convert.ToDateTime(row.Field<DateTime>("DueDate")).ToShortDateString(),                                       
                                         MaintenanceStatus = row.Field<string>("MaintenanceStatus"),
                                         DisplayInspectionDate = Convert.ToDateTime(row.Field<DateTime>("InspectionDate")).ToShortDateString(),
                                         MaintenanceCompletedDate = Convert.ToDateTime(row.Field<DateTime>("MaintenanceCompletedDate")).ToShortDateString(),
                                         days = Convert.ToInt32(row.Field<Int32>("PastDuedays")) < 0 ? 0 : Convert.ToInt16(row["PastDuedays"]),
                                         ModifiedByMaintenance = row.Field<string>("ModifiedByMaintenance"),
                                         SEControls = row.Field<string>("SEControls"),
                                         Quantity = row.Field<int>("Quantity"),
                                         UOM = row.Field<string>("UOM"),
                                         UPLocation = row.Field<string>("Location"),
                                         UploadImagePath = row.Field<string>("UploadImagePath")
                                     };
                return Uplaoddatalist.ToList();              
            }
            return null;
        }

        public dynamic GenrateActionWorkOrder(long Inspection_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getInspectionDetailsForActionWorkOrder", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                var Uplaoddatalist = from row in dataset.Tables[0].AsEnumerable()
                                     select new Maintenance
                                     {
                                         UploadData_ID = row.Field<Int64>("UploadData_ID"),
                                         Inspection_ID = row.Field<Int64>("Inspection_ID"),
                                         FormName = row.Field<string>("FormName"),
                                         ActionRequired = row.Field<string>("ActionRequired"),
                                         DisplayDueDate = Convert.ToDateTime(row.Field<DateTime>("DueDate")).ToShortDateString(),
                                         ActionStatus = row.Field<string>("ActionStatus"),                                         
                                         DisplayInspectionDate = Convert.ToDateTime(row.Field<DateTime>("InspectionDate")).ToShortDateString(),
                                         ActionCompletedDate = Convert.ToDateTime(row.Field<DateTime>("ActionCompletedDate")).ToShortDateString(),
                                         days = Convert.ToInt32(row.Field<Int32>("PastDuedays")) < 0 ? 0 : Convert.ToInt16(row["PastDuedays"]),
                                         ActionModifiedBy = row.Field<string>("ActionModifiedBy"),
                                         SEControls = row.Field<string>("SEControls"),
                                         Quantity =Convert.ToInt32(row.Field<int>("Quantity")),
                                         UOM = row.Field<string>("UOM"),
                                         UPLocation = row.Field<string>("Location"),
                                         UploadImagePath = row.Field<string>("UploadImagePath")

                                     };
                return Uplaoddatalist.ToList();
            }
            return null;
        }

        public bool MaintenanceCompleted(long User_ID, long UploadData_ID, DateTime MaintenanceCompletedDate)
        {
            return new DAL().Update("sp_UploadDataMaintenanceComplete",
                    new object[] { "@User_ID", "@UploadData_ID", "@MaintenanceCompletedDate" },
                    new object[] { User_ID, UploadData_ID, MaintenanceCompletedDate }
                    );
        }

        public bool ActionCompleted( long User_ID, long UploadData_ID, DateTime ActionCompletedDate)
        {
            return new DAL().Update("sp_UploadDataActionComplete",
                    new object[] { "@User_ID" ,"@UploadData_ID", "@ActionCompletedDate" },
                    new object[] { User_ID,UploadData_ID, ActionCompletedDate }
                    );
        }

        public bool MaintenanceAcknowlegment(long UploadData_ID, int day)
        {
            return new DAL().Update("sp_MaintenanceAcknowlegment",
                    new object[] { "@UploadData_ID", "@day" },
                    new object[] { UploadData_ID, day }
                    );
        }

        public bool ActionAcknowlegment(long UploadData_ID, int day)
        {
            return new DAL().Update("sp_ActionAcknowlegment",
                    new object[] { "@UploadData_ID", "@day" },
                    new object[] { UploadData_ID, day }
                    );
        }

        public bool ActionWorkToE2RC(Int64 Location_ID, DateTime Date, int ActionDay)
        {
            return new DAL().Update("sp_ActionWorkToE2RC",
                   new object[] { "@Location_ID", "@Date", "@ActionDay" },
                   new object[] { Location_ID,Date, ActionDay }
                   );
        }

        public bool MaintenanceWorktoE2RC(string Location_ID, DateTime Date, int MaintenanceDay)
        {
            return new DAL().Update("sp_MaintenanceWorktoE2RC",
                   new object[] { "@Location_ID", "@Date", "@MaintenanceDay" },
                   new object[] { Location_ID,Date, MaintenanceDay }
                   );
        }

        public dynamic GenerateMaintenanceCompletedList(long Inspection_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getInspection_MaintenanceCompleted", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                var Uplaoddatalist = from row in dataset.Tables[0].AsEnumerable()
                                     select new Maintenance
                                     {
                                         UploadData_ID = row.Field<Int64>("UploadData_ID"),
                                         Inspection_ID = row.Field<Int64>("Inspection_ID"),
                                         FormName = row.Field<string>("FormName"),
                                         MaintenanceNeeded = row.Field<string>("MaintenanceNeeded"),
                                         DisplayDueDate = Convert.ToDateTime(row.Field<DateTime>("DueDate")).ToShortDateString(),
                                         MaintenanceStatus = row.Field<string>("MaintenanceStatus"),
                                         DisplayInspectionDate = Convert.ToDateTime(row.Field<DateTime>("InspectionDate")).ToShortDateString(),
                                         MaintenanceCompletedDate = Convert.ToDateTime(row.Field<DateTime>("MaintenanceCompletedDate")).ToShortDateString(),
                                         days = Convert.ToInt32(row.Field<Int32>("PastDuedays")),
                                         ModifiedByMaintenance = row.Field<string>("ModifiedByMaintenance"),
                                         SEControls = row.Field<string>("SEControls"),
                                         Quantity = row.Field<int>("Quantity"),
                                         UOM = row.Field<string>("UOM"),
                                         UPLocation = row.Field<string>("Location"),
                                         UploadImagePath = row.Field<string>("UploadImagePath"),
                                         ISCorrectiveReport = Convert.ToBoolean(row.Field<int>("ISCorrectiveReport"))
                                     };
                return Uplaoddatalist.ToList();
            }
            return null;
        }

        public dynamic GenerateActionCompletedList(long Inspection_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getInspection_ActionCompleted", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                var Uplaoddatalist = from row in dataset.Tables[0].AsEnumerable()
                                     select new Maintenance
                                     {
                                         UploadData_ID = row.Field<Int64>("UploadData_ID"),
                                         Inspection_ID = row.Field<Int64>("Inspection_ID"),
                                         FormName = row.Field<string>("FormName"),
                                         ActionRequired = row.Field<string>("ActionRequired"),
                                         DisplayDueDate = Convert.ToDateTime(row.Field<DateTime>("DueDate")).ToShortDateString(),
                                         ActionStatus = row.Field<string>("ActionStatus"),
                                         DisplayInspectionDate = Convert.ToDateTime(row.Field<DateTime>("InspectionDate")).ToShortDateString(),
                                         ActionCompletedDate = Convert.ToDateTime(row.Field<DateTime>("ActionCompletedDate")).ToShortDateString(),
                                         days = Convert.ToInt32(row.Field<Int32>("PastDuedays")),
                                         ActionModifiedBy = row.Field<string>("ActionModifiedBy"),
                                         SEControls = row.Field<string>("SEControls"),
                                         Quantity = Convert.ToInt32(row.Field<int>("Quantity")),
                                         UOM = row.Field<string>("UOM"),
                                         UPLocation = row.Field<string>("Location"),
                                         UploadImagePath = row.Field<string>("UploadImagePath"),
                                         ISCorrectiveReport = Convert.ToBoolean(row.Field<int>("ISCorrectiveReport"))

                                     };
                return Uplaoddatalist.ToList();
            }
            return null;
        }

        public IEnumerable<Maintenance> getFranchiseList(string role, long User_ID)
        {
            List<Maintenance> franchiseList = new List<Maintenance>();
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getFranchiseDashboard_List", new object[] { "@User_ID" }, new object[] { User_ID});
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    franchiseList.Add(new Maintenance
                    {
                        ClientName = Convert.ToString(row["ClientName"]),
                        //PhoneNumber = Convert.ToString(row["PhoneNumber"]),                                             
                        MailingAddress = Convert.ToString(row["MailingAddress"]),
                        City = Convert.ToString(row["City"]),
                        StateName = Convert.ToString(row["StateName"]),
                        ZipCode = Convert.ToString(row["ZipCode"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        OpenWorkOrder = Convert.ToInt16(row["OpenWorkOrder"]),
                        //ModifiedDate = Convert.ToString(row["ModifiedDate"]),
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        //Location_ID = Convert.ToInt64(row["Location_ID"])
                    });
                }
                return franchiseList;
            }            
            return null;
        }

        public IEnumerable<Maintenance> getProjectManagerDashboardList(string role, long User_ID)
        {
            List<Maintenance> ProjectManagerList = new List<Maintenance>();
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getProjectManagerDashboard_List", new object[] { "@User_ID", "@Role" }, new object[] { User_ID, role });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ProjectManagerList.Add(new Maintenance
                    {
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        Tracking_No = Convert.ToString(row["Tracking_No"]),
                        IsAssignLocation = Convert.ToString(row["IsAssignLocation"]),
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        OpenWorkOrder = Convert.ToInt16(row["OpenWorkOrder"])
                    });
                }
                return ProjectManagerList;
            }            
            return null;
        }

        public IEnumerable<Maintenance> getClientDashboardList(string role, long User_ID)
        {
            List<Maintenance> ClientList = new List<Maintenance>();
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getClientDashboard_List", new object[] { "@User_ID", "@Role" }, new object[] { User_ID, role });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ClientList.Add(new Maintenance
                    {                       
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        Location_ID = Convert.ToInt64(row["Location_ID"]), 
                        Tracking_No = Convert.ToString(row["Tracking_No"]),
                        Client_ID=Convert.ToInt64(row["Client_ID"]),
                        OpenWorkOrder = Convert.ToInt16(row["OpenWorkOrder"]),
                    });
                }
                return ClientList;
            }            
            return null;
        }
        
        public bool AssignInspector(long User_ID, long Location_ID,long Inspector_ID)
        {
            return new DAL().Insert("sp_LocationAssign_CRUD",
                  new object[] {"@Action", "@Date","@Inspector_ID","@User_ID","@Location_ID"
                },
                  new object[] {Actions.INSERT.ToString(), DateTime.Now,Inspector_ID,User_ID,Location_ID               
                });
        }


        public dynamic GenratePMLocationInspectionWorkOrder(long Location_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_GenratePMLocationInspectionWorkOrder", new object[] { "@Location_ID" }, new object[] { Location_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                var Uplaoddatalist = from row in dataset.Tables[0].AsEnumerable()
                                     select new Maintenance
                                     {
                                         Inspection_ID = row.Field<Int64>("Inspection_ID"),
                                         
                                         ProjectName = row.Field<string>("ProjectName"),
                                         ClientName = row.Field<string>("ClientName"),
                                         InspectorName = row.Field<string>("InspectorName"),
                                         ModifiedDate = Convert.ToDateTime(row.Field<DateTime>("ModifiedDate")).ToShortDateString(),                                        
                                         DueDate = Convert.ToDateTime(row["DueDate"]),
                                         InspectionReportEmails = row.Field<string>("InspectionReportEmails"),
                                         WorkOrdersEmails = row.Field<string>("WorkOrdersEmails"),                                      
                                         Tracking_No = row.Field<string>("Tracking_No"),
                                         InspectorEmail = row.Field<string>("InspectorEmail"),                                        
                                     };
                return Uplaoddatalist.ToList();
            }
            return null;
        }

        

        public dynamic GenratePMLocationInspectionCompleted(long Location_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_GenratePMLocationInspectionCompleted", new object[] { "@Location_ID" }, new object[] { Location_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                var Uplaoddatalist = from row in dataset.Tables[0].AsEnumerable()
                                     select new Maintenance
                                     {
                                         Inspection_ID = row.Field<Int64>("Inspection_ID"),

                                         ProjectName = row.Field<string>("ProjectName"),
                                         ClientName = row.Field<string>("ClientName"),
                                         InspectorName = row.Field<string>("InspectorName"),
                                         ModifiedDate = Convert.ToDateTime(row.Field<DateTime>("ModifiedDate")).ToShortDateString(),
                                         DueDate = Convert.ToDateTime(row["DueDate"]),
                                         InspectionReportEmails = row.Field<string>("InspectionReportEmails"),
                                         WorkOrdersEmails = row.Field<string>("WorkOrdersEmails"),
                                         Tracking_No = row.Field<string>("Tracking_No"),
                                         InspectorEmail = row.Field<string>("InspectorEmail"),
                                     };
                return Uplaoddatalist.ToList();
            }
            return null;
        }

        public IEnumerable<Maintenance> InspectionDetailsforShare(long Inspection_ID)
        {
            List<Maintenance> InspectionDetailsforShare_List = new List<Maintenance>();
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getInspectionDetailsforShare", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    InspectionDetailsforShare_List.Add(new Maintenance
                    {
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        Location = Convert.ToString(row["Location"]),
                        ClientName = Convert.ToString(row["ClientName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        InspectionDate = Convert.ToDateTime(row["Date"]),
                        DueDate = Convert.ToDateTime(row["DueDate"]),
                        InspectionReportEmails = Convert.ToString(row["InspectionReportEmails"]),
                        WorkOrdersEmails = Convert.ToString(row["WorkOrdersEmails"]),
                        CreatedDate = Convert.ToDateTime(row["createdDate"]),
                        Tracking_No = Convert.ToString(row["Tracking_No"]),
                        InspectorEmail = Convert.ToString(row["InspectorEmail"]),

                    });
                }
                return InspectionDetailsforShare_List;
            }
            else
            {
                InspectionDetailsforShare_List.Add(new Maintenance
                {
                    Inspection_ID = 0,
                    Location = "",
                    ClientName = "",
                    InspectorName = "",
                    DueDate = DateTime.Now,
                    InspectionDate = DateTime.Now,
                    // UploadData_ID = 0,

                });
            }
            return InspectionDetailsforShare_List;
        }


        public IEnumerable<Maintenance> getDirectorReportList(string role, long User_ID)
        {
            List<Maintenance> DirectorList = new List<Maintenance>();
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_DisplayReportToDirector", new object[] { "@User_ID", "@Role" }, new object[] { User_ID, role });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    DirectorList.Add(new Maintenance
                    {
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        OpenWorkOrder = Convert.ToInt16(row["OpenWorkOrder"]),
                        //Location = Convert.ToString(row["Location"]),
                        //ClientName = Convert.ToString(row["ClientName"]),
                        //InspectorName = Convert.ToString(row["InspectorName"]),
                        //InspectionDate = Convert.ToDateTime(row["Date"]),
                        //DueDate = Convert.ToDateTime(row["DueDate"]),
                        //Email_1 = Convert.ToString(row["Email_1"]),
                        //Email_2 = Convert.ToString(row["Email_2"]),
                        //CreatedDate = Convert.ToDateTime(row["createdDate"]),
                        Tracking_No = Convert.ToString(row["Tracking_No"]),
                        //InspectorEmail = Convert.ToString(row["InspectorEmail"]),

                    });
                }
                return DirectorList;
            }
            else
            {
                DirectorList.Add(new Maintenance
                {
                    Inspection_ID = 0,
                    Location = "",
                    ClientName = "",
                    InspectorName = "",
                    DueDate = DateTime.Now,
                    InspectionDate = DateTime.Now,
                    // UploadData_ID = 0,

                });
            }
            return DirectorList;
        }

        public IEnumerable<Maintenance> getSuperAdminActiveClientList(string role, long User_ID)
        {
            List<Maintenance> SuperAdminList = new List<Maintenance>();
           
            
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getSuperAdminActiveUserDetails", new object[] { "@User_ID", "@Role" }, new object[] { User_ID, role });           
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    SuperAdminList.Add(new Maintenance
                    {
                        FranchiseCompany = Convert.ToString(row["FraCompName"]),
                        franchiseName = Convert.ToString(row["FranchiseName"]),
                        Username = Convert.ToString(row["Username"]),
                        Franchise_ID = Convert.ToInt64(row["Franchise_ID"]),
                        date = Convert.ToDateTime(row.Field<DateTime>("Date")).ToShortDateString(),
                        User_ID = Convert.ToInt64(row["User_ID"]),
                        status = Convert.ToBoolean(row["Status"]),
                        
                    });
                }
                
                return SuperAdminList;
            }
            else
            {
                SuperAdminList.Add(new Maintenance
                {
                    FranchiseCompany = "",
                    franchiseName = "",                  
                    CreatedDate = DateTime.Now,
                    // UploadData_ID = 0,

                });
            }
            return SuperAdminList;
        }

        public dynamic ActiveLocationUsers(long Location_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_ListActiveLocationUsers", new object[] { "@Location_ID" }, new object[] { Location_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                var Uplaoddatalist = from row in dataset.Tables[0].AsEnumerable()
                                     select new Maintenance
                                     {
                                         franchiseName = row.Field<string>("franchise"),
                                         FranchiseCompany = row.Field<string>("FranCompanyName"),
                                         CompanyName = row.Field<string>("CompanyName"),
                                         InspectorName = row.Field<string>("Inspector"),
                                         
                                     };
                return Uplaoddatalist.ToList();
            }
            return null;
        }

        public IEnumerable<Maintenance> getGeneratePDFActionMaintenanceList(long Inspection_ID)
        {
            List<Maintenance> ActionMaintenanceList = new List<Maintenance>();
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getGeneratePDFActionMaintenanceList", new object[] { "Inspection_ID" }, new object[] { Inspection_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ActionMaintenanceList.Add(new Maintenance
                    {
                        Inspection_ID = Convert.ToInt64(row["Inspection_ID"]),
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        days = Convert.ToInt16(row["DaysPastDue"]) < 0 ? 0 : Convert.ToInt16(row["DaysPastDue"]),
                        InspectorEmail = Convert.ToString(row["InspectorEmail"]),
                        Location = Convert.ToString(row["Location"]),
                        ClientName = Convert.ToString(row["ClientName"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        InspectionDate = Convert.ToDateTime(row["Date"]),
                        DueDate = Convert.ToDateTime(row["DueDate"]),
                        InspectionReportEmails = Convert.ToString(row["InspectionReportEmails"]),
                        WorkOrdersEmails = Convert.ToString(row["WorkOrdersEmails"]),
                        CreatedDate = Convert.ToDateTime(row["createdDate"]),
                        Tracking_No = Convert.ToString(row["Tracking_No"]),
                    });
                }
                return ActionMaintenanceList;
            }
            else
            {
                ActionMaintenanceList.Add(new Maintenance
                {
                    Inspection_ID = 0,
                    Location = "",
                    ClientName = "",
                    InspectorName = "",
                    DueDate = DateTime.Now,
                    InspectionDate = DateTime.Now,
                    // UploadData_ID = 0,

                });
            }
            return ActionMaintenanceList;
        }



        public IEnumerable<Maintenance> getReviewerList(string role, long User_ID)
        {
            List<Maintenance> ReviewerList = new List<Maintenance>();
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getReviewerDashboard_List", new object[] { "@User_ID", "@Role" }, new object[] { User_ID, role });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ReviewerList.Add(new Maintenance
                    {
                        Reviewer_ID = Convert.ToInt64(row["Reviewer_ID"]),
                        ProjectName = Convert.ToString(row["ProjectName"]),
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        Tracking_No = Convert.ToString(row["Tracking_No"]),
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        OpenWorkOrder = Convert.ToInt16(row["OpenWorkOrder"]),
                    });
                }
                return ReviewerList;
            }
            return null;
        }

        public bool checkWorkOrderCompleted(long Inspection_ID)
        {
            object IsCompleted = new DataAccessLayer.DAL().ExecuteScalar("sp_IsWorkOrderCompleted",
                new object[] { "@Inspection_ID" }, new object[] {Inspection_ID});
            return (int)IsCompleted == 1 ? false : true;
        }

        public bool setFirstReviewerLoginInfo(long Reviewer_ID, long Location_ID)
        {
            return new DAL().Update("sp_setFirstReviewerLoginDetails",
                    new object[] { "@Reviewer_ID", "@Location_ID"},
                    new object[] { Reviewer_ID, Location_ID}
                    );
        }

    }
}
