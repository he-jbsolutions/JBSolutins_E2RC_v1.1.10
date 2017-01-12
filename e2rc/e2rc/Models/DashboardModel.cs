using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PagedList;
using PagedList.Mvc;

namespace e2rc.Models
{

    public class DashboardModel
    { 
        public IPagedList<ActionMaintenanceCompleted> ActionCompletedList { get; set; }
        public IPagedList<ActionMaintenanceData> ActionMaintenanceList { get; set; }
        public IPagedList<FranchiseDashboard> FranchiseDataList { get; set; }
        public IPagedList<ProjectManagerDashboard> ProjectManagerDataList { get; set; }
        public IPagedList<ClientDashboard> ClientDataList { get; set; }
        public IPagedList<DirectorReportData> DirectorReportList { get; set; }
        public IPagedList<ReviewerDashboard> ReviewerDataList { get; set; }
        public IPagedList<SuperAdminData> SuperAdminActiveList { get; set; }

        public Int64 assignInspector;
        public string Inspector;
    }
    public class ActionMaintenanceCompleted
    {
        public String Name { get; set; }

    }
    public class SuperAdminData
    {   
        public Int64 Franchise_ID ;
        public String franchiseName ;
        public String Username  ;    
        public String FranchiseCompany ;
        [DisplayFormat(DataFormatString = "{0:d}")]
        public String date ;
        public Int64 User_ID ;
        public bool status;
    }

    public class FranchiseDashboard
    {
        //public Int64 Inspection_Id { get; set; }
        //public Int64 Location_ID;
        public Int64 Client_ID { get; set; }
        public String ClientName { get; set; }      
        //public String ProjectName{ get; set; }
        //public String PhoneNumber { get; set; }         
        public String City{ get; set; } 
        public String MailingAddress{ get; set; } 
        public String  StateName{ get; set; }
        public String ZipCode { get; set; }
        public String CompanyName { get; set; }
         [DisplayFormat(DataFormatString = "{0:d}")]
        public String ModifiedDate { get; set; }
         public Int16 OpenWorkOrder;

    }

    public class ProjectManagerDashboard
    { 
        public String ProjectName;
        public string Tracking_No;
        public Int64 Client_ID;
        public string AssignInspector_ID;
        public string IsAssignLocation;
        public Int64 Location_ID;
        public Int64 OpenWorkOrder;
    }

    public class ReviewerDashboard
    {
        public Int64 Reviewer_ID;
        public String ProjectName;
        public string Tracking_No;
        public Int64 Client_ID; 
        public Int64 Location_ID;
        public Int64 OpenWorkOrder;
    }


    public class ClientDashboard
    {
        public Int64 Inspection_Id { get; set; }
        public Int64 Location_ID { get; set; }
        public Int64 Client_ID { get; set; }
        public String CompanyName { get; set; }       
        public String Location;
        public Int16 OpenWorkOrder;
        public String ProjectName;
        public String InspectorName { get; set; }        
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime InspectionDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DueDate { get; set; }
        public String InspectorEmail { get; set; }
        public String Email_1 { get; set; }
        public String Email_2 { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CreatedDate { get; set; }
        public string Tracking_No;
    }


    public class ActionMaintenanceData
    {
        public Int64 Inspection_Id { get; set; }
        public Int64 Location_ID { get; set; }
        public Int64 Client_ID { get; set; }
        public String ClientName { get; set; }
        public String CompanyName { get; set; } 
        public String Location;
        public String ProjectName;       
        public String InspectorName { get; set; }       
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime InspectionDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DueDate { get; set; }        
        public String InspectorEmail { get; set; }
        public String InspectionReportEmails { get; set; }
        public String WorkOrdersEmails { get; set; }
         public String Email_2 { get; set; }
         [DisplayFormat(DataFormatString = "{0:d}")]
         public DateTime CreatedDate { get; set; }
         public string Tracking_No;
         public int duedays { get; set; }
         public Int16 OpenWorkOrder;
        
    }

    public class DirectorReportData
    {
        public Int64 Location_ID { get; set; }
        public Int64 Client_ID { get; set; }
        public String Location;
        public String ProjectName;
        public Int16 OpenWorkOrder;
        public String InspectorName { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime InspectionDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DueDate { get; set; }
        public String InspectorEmail { get; set; }
        public String Email_1 { get; set; }
        public String Email_2 { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CreatedDate { get; set; }
        public string Tracking_No;
    }
   
}