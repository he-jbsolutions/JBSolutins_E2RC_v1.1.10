using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;
using e2rcModel;
namespace e2rc.Models.Repository
{
    public static class DashboardRepository
    {
        
       public static IEnumerable<ActionMaintenanceData> getActionMaintenanceList(string role, long User_ID)
        {
            IEnumerable<Maintenance> ActionMaintenanceList = new Maintenance().getActionMaintenanceList(role,User_ID);
            if (ActionMaintenanceList != null)
            {
                List<ActionMaintenanceData> MaintenanceModelList = new List<ActionMaintenanceData>();
                foreach (var Maintenance in ActionMaintenanceList)
                {
                    MaintenanceModelList.Add(GetMaintenance(Maintenance));
                }
                return MaintenanceModelList;
            }
            return null;

        }

        private static ActionMaintenanceData GetMaintenance(Maintenance maintain)
        {
            //use this in generate pdf 
            return new ActionMaintenanceData
            {
                Inspection_Id=maintain.Inspection_ID,
                duedays=maintain.days,
                Location_ID=maintain.Location_ID,
                Client_ID=maintain.Client_ID,
                ClientName=maintain.ClientName,
                CompanyName=maintain.CompanyName,
                Location = maintain.Location,   
                ProjectName=maintain.ProjectName,
                Tracking_No=maintain.Tracking_No,
                InspectorName = maintain.InspectorName,
                InspectionDate = maintain.InspectionDate,
                DueDate=maintain.DueDate,
                CreatedDate=maintain.CreatedDate,
                InspectionReportEmails=maintain.InspectionReportEmails,
                WorkOrdersEmails = maintain.WorkOrdersEmails,
                InspectorEmail = maintain.InspectorEmail,
                OpenWorkOrder = maintain.OpenWorkOrder
            };
        }

        public static IEnumerable<ActionMaintenanceData> UploadedDataSearchSort(string sortOrder, string role, long User_ID)
        {
            IEnumerable<ActionMaintenanceData> UploadDataList;
            UploadDataList = getActionMaintenanceList(role, User_ID);
            if (UploadDataList != null)
            {
                switch (sortOrder)
                {
                    // TrackingNo
                    case "Inspection_ID_desc":
                        UploadDataList = UploadDataList.OrderByDescending(m => m.Inspection_Id);
                        break;
                    case "Inspection_ID":
                        UploadDataList = UploadDataList.OrderBy(m => m.Inspection_Id);
                        break;
                    case "TrackingNo_desc":
                        UploadDataList = UploadDataList.OrderByDescending(m => m.Tracking_No);
                        break;
                    case "TrackingNo":
                        UploadDataList = UploadDataList.OrderBy(m => m.Tracking_No);
                        break;
                    case "Inspector_desc":
                        UploadDataList = UploadDataList.OrderByDescending(m => m.InspectorName);
                        break;
                    case "Inspector":
                        UploadDataList = UploadDataList.OrderBy(m => m.InspectorName);
                        break;
                    case "CompanyName_desc":
                        UploadDataList = UploadDataList.OrderByDescending(m => m.CompanyName);
                        break;
                    case "CompanyName":
                        UploadDataList = UploadDataList.OrderBy(m => m.CompanyName);
                        break;
                    case "ProjectName":
                        UploadDataList = UploadDataList.OrderBy(m => m.ProjectName);
                        break;
                    case "ProjectName_desc":
                        UploadDataList = UploadDataList.OrderByDescending(m => m.ProjectName);
                        break;
                    case "OpenWorkOrder":
                        UploadDataList = UploadDataList.OrderBy(m => m.OpenWorkOrder);
                        break;
                    case "OpenWorkOrder_desc":
                        UploadDataList = UploadDataList.OrderByDescending(m => m.OpenWorkOrder);
                        break;

                    case "InspectionDate":
                        UploadDataList = UploadDataList.OrderBy(m => m.InspectionDate);
                        break;
                    case "InspectionDate_desc":
                        UploadDataList = UploadDataList.OrderByDescending(m => m.InspectionDate);
                        break;
                    default:
                        UploadDataList = UploadDataList.OrderBy(m => m.InspectionDate);
                        break;
                }
                return UploadDataList;
            }
            else
            {
                return null;
            }
           
        }

        public static IEnumerable<DirectorReportData> DirectorDashboard(string sortOrder, string role, long User_ID)
        {
            IEnumerable<DirectorReportData> DirectorList;
            DirectorList = getDirectorList(role, User_ID);
            if (DirectorList != null)
            {
                switch (sortOrder)
                {
                    // TrackingNo
                    //case "Inspection_ID_desc":
                    //    DirectorList = DirectorList.OrderByDescending(m => m.Inspection_Id);
                    //    break;
                    //case "Inspection_ID":
                    //    DirectorList = DirectorList.OrderBy(m => m.Inspection_Id);
                    //    break;
                    case "TrackingNo_desc":
                        DirectorList = DirectorList.OrderByDescending(m => m.Tracking_No);
                        break;
                    case "TrackingNo":
                        DirectorList = DirectorList.OrderBy(m => m.Tracking_No);
                        break;
                    //case "Inspector_desc":
                    //    DirectorList = DirectorList.OrderByDescending(m => m.InspectorName);
                    //    break;
                    //case "Inspector":
                    //    DirectorList = DirectorList.OrderBy(m => m.InspectorName);
                    //    break;
                    //case "CustomerName_desc":
                    //    DirectorList = DirectorList.OrderByDescending(m => m.ClientName);
                    //    break;
                    //case "CustomerName":
                    //    DirectorList = DirectorList.OrderBy(m => m.ClientName);
                    //    break;
                    case "ProjectName":
                        DirectorList = DirectorList.OrderBy(m => m.ProjectName);
                        break;
                    case "ProjectName_desc":
                        DirectorList = DirectorList.OrderByDescending(m => m.ProjectName);
                        break;
                    //case "InspectionDate":
                    //    DirectorList = DirectorList.OrderBy(m => m.InspectionDate);
                    //    break;
                    //case "InspectionDate_desc":
                    //    DirectorList = DirectorList.OrderByDescending(m => m.InspectionDate);
                    //    break;
                    default:
                        DirectorList = DirectorList.OrderBy(m => m.Tracking_No);
                        break;
                }
                return DirectorList;
            }
            else
            {
                return null;
            }

        }

        public static IEnumerable<DirectorReportData> getDirectorList(string role, long User_ID)
        {
            IEnumerable<Maintenance> directorList = new Maintenance().getDirectorReportList(role, User_ID);
            if (directorList != null)
            {
                List<DirectorReportData> MaintenanceModelList = new List<DirectorReportData>();
                foreach (var Maintenance in directorList)
                {
                    MaintenanceModelList.Add(GetReport(Maintenance));
                }
                return MaintenanceModelList;
            }
            return null;

        }

        private static DirectorReportData GetReport(Maintenance maintain)
        {
            return new DirectorReportData
            {
                Client_ID = maintain.Client_ID,
                Location_ID = maintain.Location_ID,
                OpenWorkOrder = maintain.OpenWorkOrder,
                //ClientName = maintain.ClientName,
                //Location = maintain.Location,
                ProjectName = maintain.ProjectName,
                Tracking_No = maintain.Tracking_No,
                //InspectorName = maintain.InspectorName,
                //InspectionDate = maintain.InspectionDate,
                //DueDate = maintain.DueDate,
                //CreatedDate = maintain.CreatedDate,
                //Email_1 = maintain.Email_1,
                //Email_2 = maintain.Email_2,
                //InspectorEmail = maintain.InspectorEmail
            };
        }

        public static IEnumerable<FranchiseDashboard> FranchiseDashboard(string sortOrder, string role, long User_ID)
        {
            IEnumerable<FranchiseDashboard> FranchiseDashboardList;
            FranchiseDashboardList = getFranchiseList(role, User_ID);
            //return FranchiseDashboardList;
            if (FranchiseDashboardList != null)
            {
                switch (sortOrder)
                {
                    case "CompanyName_desc":
                        FranchiseDashboardList = FranchiseDashboardList.OrderByDescending(m => m.CompanyName);
                        break;
                    case "CompanyName":
                        FranchiseDashboardList = FranchiseDashboardList.OrderBy(m => m.CompanyName);
                        break;
                    case "ClientName_desc":
                        FranchiseDashboardList = FranchiseDashboardList.OrderByDescending(m => m.ClientName);
                        break;
                    case "ClientName":
                        FranchiseDashboardList = FranchiseDashboardList.OrderBy(m => m.ClientName);
                        break;
                    case "OpenWorkOrder":
                        FranchiseDashboardList = FranchiseDashboardList.OrderBy(m => m.OpenWorkOrder);
                        break;
                    case "OpenWorkOrder_desc":
                        FranchiseDashboardList = FranchiseDashboardList.OrderByDescending(m => m.OpenWorkOrder);
                        break;
                    //case "ProjectName_desc":
                    //    FranchiseDashboardList = FranchiseDashboardList.OrderByDescending(m => m.ProjectName);
                    //    break;
                    //case "ProjectName":
                    //    FranchiseDashboardList = FranchiseDashboardList.OrderBy(m => m.ProjectName);
                    //    break;
                    case "StateName_desc":
                        FranchiseDashboardList = FranchiseDashboardList.OrderByDescending(m => m.StateName);
                        break;
                    case "StateName":
                        FranchiseDashboardList = FranchiseDashboardList.OrderBy(m => m.StateName);
                        break;
                    case "MailingAddress":
                        FranchiseDashboardList = FranchiseDashboardList.OrderBy(m => m.MailingAddress);
                        break;
                    case "MailingAddress_desc":
                        FranchiseDashboardList = FranchiseDashboardList.OrderByDescending(m => m.MailingAddress);
                        break;
                    case "City":
                        FranchiseDashboardList = FranchiseDashboardList.OrderBy(m => m.City);
                        break;
                    case "City_desc":
                        FranchiseDashboardList = FranchiseDashboardList.OrderByDescending(m => m.City);
                        break;
                    case "ModifiedDate":
                        FranchiseDashboardList = FranchiseDashboardList.OrderBy(m => m.ModifiedDate);
                        break;
                    case "ModifiedDate_desc":
                        FranchiseDashboardList = FranchiseDashboardList.OrderByDescending(m => m.ModifiedDate);
                        break;
                    default:
                        FranchiseDashboardList = FranchiseDashboardList.OrderBy(m => m.CompanyName);
                        break;

                }
                return FranchiseDashboardList;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<ProjectManagerDashboard> ProjectManagerDashboard(string sortOrder, string role, long User_ID)
        {
            IEnumerable<ProjectManagerDashboard> ProjectManagerDashboardList;
            ProjectManagerDashboardList = getProjectManagerDashboardList(role, User_ID);

            if (ProjectManagerDashboardList != null)
            {
                switch (sortOrder)
                {

                    case "ProjectName":
                        ProjectManagerDashboardList = ProjectManagerDashboardList.OrderBy(m => m.ProjectName);
                        break;
                    case "ProjectName_desc":
                        ProjectManagerDashboardList = ProjectManagerDashboardList.OrderByDescending(m => m.ProjectName);
                        break;
                    case "TrackingNo_desc":
                        ProjectManagerDashboardList = ProjectManagerDashboardList.OrderByDescending(m => m.Tracking_No);
                        break;
                    case "TrackingNo":
                        ProjectManagerDashboardList = ProjectManagerDashboardList.OrderBy(m => m.Tracking_No);
                        break;
                    //case "InspectionDate":
                    //    ProjectManagerDashboardList = ProjectManagerDashboardList.OrderBy(m => m.CreatedDate);
                    //    break;
                    //case "InspectionDate_desc":
                    //    ProjectManagerDashboardList = ProjectManagerDashboardList.OrderByDescending(m => m.CreatedDate);
                    //    break;
                    default:
                        ProjectManagerDashboardList = ProjectManagerDashboardList.OrderBy(m => m.ProjectName);
                        break;
                }
                return ProjectManagerDashboardList;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<ClientDashboard> ClientMainDashboard(string sortOrder, string role, long User_ID)
        {
            IEnumerable<ClientDashboard> ClientDashboardList;
            ClientDashboardList = getClientDashboardList(role, User_ID);
            //return ClientDashboardList;
            if (ClientDashboardList != null)
            {
                switch (sortOrder)
                {
                    case "CompanyName_desc":
                        ClientDashboardList = ClientDashboardList.OrderByDescending(m => m.CompanyName);
                        break;
                    case "CompanyName":
                        ClientDashboardList = ClientDashboardList.OrderBy(m => m.CompanyName);
                        break;

                    case "TrackingNo_desc":
                        ClientDashboardList = ClientDashboardList.OrderByDescending(m => m.Tracking_No);
                        break;
                    case "TrackingNo":
                        ClientDashboardList = ClientDashboardList.OrderBy(m => m.Tracking_No);
                        break;
                    case "ProjectName":
                        ClientDashboardList = ClientDashboardList.OrderBy(m => m.ProjectName);
                        break;
                    case "ProjectName_desc":
                        ClientDashboardList = ClientDashboardList.OrderByDescending(m => m.ProjectName);
                        break;
                    case "OpenWorkOrder":
                        ClientDashboardList = ClientDashboardList.OrderBy(m => m.OpenWorkOrder);
                        break;
                    case "OpenWorkOrder_desc":
                        ClientDashboardList = ClientDashboardList.OrderByDescending(m => m.OpenWorkOrder);
                        break;

                    case "InspectionDate":
                        ClientDashboardList = ClientDashboardList.OrderBy(m => m.CreatedDate);
                        break;
                    case "InspectionDate_desc":
                        ClientDashboardList = ClientDashboardList.OrderByDescending(m => m.CreatedDate);
                        break;
                    default:
                        ClientDashboardList = ClientDashboardList.OrderBy(m => m.CreatedDate);
                        break;
                }
                return ClientDashboardList;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<FranchiseDashboard> getFranchiseList(string role, long User_ID)
        {
            IEnumerable<Maintenance> FranchiseDashboardList = new Maintenance().getFranchiseList(role, User_ID);
            if (FranchiseDashboardList != null)
            {
                List<FranchiseDashboard> FranchiseList = new List<FranchiseDashboard>();
                foreach (var franchise in FranchiseDashboardList)
                {
                    FranchiseList.Add(GetFranchise(franchise));
                }
                return FranchiseList;
            }
            return null;
        }

        private static FranchiseDashboard GetFranchise(Maintenance maintain)
        {
            return new FranchiseDashboard
            {
                //Inspection_Id = maintain.Inspection_ID,
                ClientName = maintain.ClientName,
                OpenWorkOrder = maintain.OpenWorkOrder,
                //Location_ID=maintain.Location_ID,
                Client_ID=maintain.Client_ID,
                //ProjectName = maintain.ProjectName, 
                //PhoneNumber=maintain.PhoneNumber,
                City=maintain.City,
                MailingAddress =maintain.MailingAddress,
                StateName=maintain.StateName,
                ZipCode=maintain.ZipCode,  
                CompanyName=maintain.CompanyName               
            };
        }

        public static IEnumerable<ProjectManagerDashboard> getProjectManagerDashboardList(string role, long User_ID)
        {
            IEnumerable<Maintenance> ActionMaintenanceList = new Maintenance().getProjectManagerDashboardList(role, User_ID);
            if (ActionMaintenanceList != null)
            {
                List<ProjectManagerDashboard> MaintenanceModelList = new List<ProjectManagerDashboard>();
                foreach (var Maintenance in ActionMaintenanceList)
                {
                    MaintenanceModelList.Add(GetProjectManager(Maintenance));
                }
                return MaintenanceModelList;
            }
            return null;
        }

        private static ProjectManagerDashboard GetProjectManager(Maintenance maintain)
        {
            return new ProjectManagerDashboard
            {
                ProjectName = maintain.ProjectName,
                Tracking_No = maintain.Tracking_No,
                Location_ID = maintain.Location_ID,
                IsAssignLocation = maintain.IsAssignLocation,
                Client_ID=maintain.Client_ID,
                OpenWorkOrder = maintain.OpenWorkOrder
                //ClientName = maintain.ClientName,
                //Location = maintain.Location,                
                //InspectorName = maintain.InspectorName,
                //InspectionDate = maintain.InspectionDate,
                //DueDate = maintain.DueDate,
                //CreatedDate = maintain.CreatedDate,
                //Email_1 = maintain.Email_1,
                //Email_2 = maintain.Email_2,
                //InspectorEmail = maintain.InspectorEmail,
                //Inspection_Id = maintain.Inspection_ID,
                //ProjectManager_ID=maintain.ProjectManager_ID,
                //Assign_ID=maintain.Assign_ID, 
            };
        }

        //getClientDashboardList

        public static IEnumerable<ClientDashboard> getClientDashboardList(string role,long User_ID)
        {
            IEnumerable<Maintenance> ClientDashboardList = new Maintenance().getClientDashboardList(role, User_ID);
            if (ClientDashboardList != null)
            {
                List<ClientDashboard> ClientModelList = new List<ClientDashboard>();
                foreach (var Maintenance in ClientDashboardList)
                {
                    ClientModelList.Add(GetClient(Maintenance));
                }
                return ClientModelList;
            }
            return null;

        }

        private static ClientDashboard GetClient(Maintenance maintain)
        {
            return new ClientDashboard
            {               
                Location_ID=maintain.Location_ID,
                ProjectName = maintain.ProjectName,
                Tracking_No = maintain.Tracking_No,
                Client_ID=maintain.Client_ID,
                OpenWorkOrder = maintain.OpenWorkOrder
           };
        }


        public static IEnumerable<ActionMaintenanceCompleted> getActionMaintenanceCompletedList()
        {

            IEnumerable<ActionCompleted> ActionMaintenanceList = new ActionCompleted().getActionMaintenanceCompleteList();
            if (ActionMaintenanceList != null)
            {
                List<ActionMaintenanceCompleted> MaintenanceModelList = new List<ActionMaintenanceCompleted>();
                foreach (var ActionMaintenance in ActionMaintenanceList)
                {
                    MaintenanceModelList.Add(GetActionMaintenance(ActionMaintenance));
                }
                return MaintenanceModelList;
            }
            return null;
        }

        private static ActionMaintenanceCompleted GetActionMaintenance(ActionCompleted ActionCompleted)
        {
            return new ActionMaintenanceCompleted
            {
                Name = ActionCompleted.name,  
            };
        }

        public static dynamic GenrateMaintenanceWorkOrder(long Inspection_ID)
        {
            return new Maintenance().GenrateMaintenanceWorkOrder(Inspection_ID);
        }
        public static dynamic GenerateMaintenanceCompletedList(long Inspection_ID)
        {
            return new Maintenance().GenerateMaintenanceCompletedList(Inspection_ID);
        }
        public static dynamic GenrateActioneWorkOrder(long Inspection_ID)
        {
            return new Maintenance().GenrateActionWorkOrder(Inspection_ID);
        }      
        public static dynamic GenerateActionCompletedList(long Inspection_ID)
        {
            return new Maintenance().GenerateActionCompletedList(Inspection_ID);
        }

        public static Boolean ActionCompleted(long User_ID,  long UploadData_ID, DateTime ActionCompletedDate)
        {
            return new Maintenance().ActionCompleted(User_ID,UploadData_ID, ActionCompletedDate);
        }

        public static Boolean MaintenanceCompleted(long User_ID, long UploadData_ID, DateTime MaintenanceCompletedDate)
        {
            return new Maintenance().MaintenanceCompleted(User_ID, UploadData_ID, MaintenanceCompletedDate);
        }

        public static Boolean ActionAcknowlegment(long UploadData_ID, int day)
        {
            return new Maintenance().ActionAcknowlegment(UploadData_ID, day);
        }

        public static Boolean MaintenanceAcknowlegment(long UploadData_ID, int day)
        {
            return new Maintenance().MaintenanceAcknowlegment(UploadData_ID, day);
        }

        public static Boolean ActionWorkToE2RC(Int64 Location_ID, DateTime Date, int ActionDay)
        {
            return new Maintenance().ActionWorkToE2RC(Location_ID,Date, ActionDay);
        }

        public static Boolean MaintenanceWorktoE2RC(string Location_ID, DateTime Date, int MaintenanceDay)
        {
            return new Maintenance().MaintenanceWorktoE2RC(Location_ID, Date, MaintenanceDay);
        }

        public static Boolean AssignInspector(long User_ID,long Location_ID,long Inspector_ID)
        {
            return new Maintenance().AssignInspector(User_ID, Location_ID, Inspector_ID);
        }

        public static dynamic GenratePMLocationInspectionWorkOrder(long Location_ID)
        {
            return new Maintenance().GenratePMLocationInspectionWorkOrder(Location_ID);
        }
        public static dynamic GenratePMLocationInspectionCompleted(long Location_ID)
        {
            return new Maintenance().GenratePMLocationInspectionCompleted(Location_ID);
        }       

        public static IEnumerable<ActionMaintenanceData> InspectionDetailsforShare(long Inspection_ID)
        {
            IEnumerable<Maintenance> ActionMaintenanceList = new Maintenance().InspectionDetailsforShare(Inspection_ID);
            if (ActionMaintenanceList != null)
            {
                List<ActionMaintenanceData> MaintenanceModelList = new List<ActionMaintenanceData>();
                foreach (var Maintenance in ActionMaintenanceList)
                {
                    MaintenanceModelList.Add(GetInspectionDetailsforShare(Maintenance));
                }
                return MaintenanceModelList;
            }
            return null;

        }

        private static ActionMaintenanceData GetInspectionDetailsforShare(Maintenance maintain)
        {
            return new ActionMaintenanceData
            {
                Inspection_Id = maintain.Inspection_ID,
                ClientName = maintain.ClientName,
                Location = maintain.Location,
                ProjectName = maintain.ProjectName,
                Tracking_No = maintain.Tracking_No,
                InspectorName = maintain.InspectorName,
                InspectionDate = maintain.InspectionDate,
                DueDate = maintain.DueDate,
                CreatedDate = maintain.CreatedDate,
                InspectionReportEmails = maintain.InspectionReportEmails,
                WorkOrdersEmails = maintain.WorkOrdersEmails,
                InspectorEmail = maintain.InspectorEmail
            };
        }

        public static IEnumerable<SuperAdminData> SuperAdminDashboard(string sortOrder, string role, long User_ID)
        {
            IEnumerable<SuperAdminData> UploadDataList;
            UploadDataList = getSuperAdminActiveClientList(role, User_ID);
            if (UploadDataList != null)
            {
                switch (sortOrder)
                {

                    case "CompanyName_desc":
                        UploadDataList = UploadDataList.OrderByDescending(m => m.FranchiseCompany);
                        break;
                    case "CompanyName":
                        UploadDataList = UploadDataList.OrderBy(m => m.FranchiseCompany);
                        break;
                    case "Franchise_desc":
                        UploadDataList = UploadDataList.OrderByDescending(m => m.franchiseName);
                        break;
                    case "Franchise":
                        UploadDataList = UploadDataList.OrderBy(m => m.franchiseName);
                        break;
                    case "Username_desc":
                        UploadDataList = UploadDataList.OrderByDescending(m => m.Username);
                        break;
                    case "Username":
                        UploadDataList = UploadDataList.OrderBy(m => m.Username);
                        break;
                    case "Date_desc":
                        UploadDataList = UploadDataList.OrderByDescending(m => m.date);
                        break;
                    case "Date":
                        UploadDataList = UploadDataList.OrderBy(m => m.date);
                        break;
                    default :
                        UploadDataList = UploadDataList.OrderBy(m => m.FranchiseCompany);
                        break;                   
                   
                }
                return UploadDataList;
            }
            else
            {
                return null;
            }

        }

        public static IEnumerable<SuperAdminData> getSuperAdminActiveClientList(string role, long User_ID)
        {
            IEnumerable<Maintenance> SuperAdminActiveClientList = new Maintenance().getSuperAdminActiveClientList(role, User_ID);
            if (SuperAdminActiveClientList != null)
            {
                List<SuperAdminData> SuperAdminList = new List<SuperAdminData>();
                foreach (var Maintenance in SuperAdminActiveClientList)
                {
                    SuperAdminList.Add(GetSuperAdmin(Maintenance));
                }
                return SuperAdminList;
            }
            return null;

        }//Days Past Due     

        private static SuperAdminData GetSuperAdmin(Maintenance maintain)
        {
            return new SuperAdminData
            {
                FranchiseCompany=maintain.FranchiseCompany,
                Username=maintain.Username,
                franchiseName=maintain.franchiseName,
                Franchise_ID=maintain.Franchise_ID,
                date = maintain.date,
                User_ID=maintain.User_ID,
                status=maintain.status
            };
        }

        public static dynamic ActiveLocationUsers(long Location_ID)
        {
            return new Maintenance().ActiveLocationUsers(Location_ID);
        }


        public static IEnumerable<ActionMaintenanceData> getGeneratePDFActionMaintenanceList(long Inspection_ID)
        {
            IEnumerable<Maintenance> ActionMaintenanceList = new Maintenance().getGeneratePDFActionMaintenanceList(Inspection_ID);
            if (ActionMaintenanceList != null)
            {
                List<ActionMaintenanceData> MaintenanceModelList = new List<ActionMaintenanceData>();
                foreach (var Maintenance in ActionMaintenanceList)
                {
                    MaintenanceModelList.Add(GetMaintenance(Maintenance));
                }
                return MaintenanceModelList;
            }
            return null;

        }




        public static IEnumerable<ReviewerDashboard> ReviewerDashboard(string sortOrder, string role, long User_ID)
        {
            IEnumerable<ReviewerDashboard> ReviewerDashboardList;
            ReviewerDashboardList = getReviewerDashboardList(role, User_ID);
            //return ClientDashboardList;
            if (ReviewerDashboardList != null)
            {
                switch (sortOrder)
                {
                   case "TrackingNo_desc":
                        ReviewerDashboardList = ReviewerDashboardList.OrderByDescending(m => m.Tracking_No);
                        break;
                    case "TrackingNo":
                        ReviewerDashboardList = ReviewerDashboardList.OrderBy(m => m.Tracking_No);
                        break;
                    case "ProjectName":
                        ReviewerDashboardList = ReviewerDashboardList.OrderBy(m => m.ProjectName);
                        break;
                    case "ProjectName_desc":
                        ReviewerDashboardList = ReviewerDashboardList.OrderByDescending(m => m.ProjectName);
                        break;                   
                    default:
                        ReviewerDashboardList = ReviewerDashboardList.OrderBy(m => m.ProjectName);
                        break;
                }
                return ReviewerDashboardList;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<ReviewerDashboard> getReviewerDashboardList(string role, long User_ID)
        {
            IEnumerable<Maintenance> ReviewerDashboardList = new Maintenance().getReviewerList(role, User_ID);
            if (ReviewerDashboardList != null)
            {
                List<ReviewerDashboard> ReviewerList = new List<ReviewerDashboard>();
                foreach (var reviewer in ReviewerDashboardList)
                {
                    ReviewerList.Add(GetReviewer(reviewer));
                }
                return ReviewerList;
            }
            return null;
        }
        private static ReviewerDashboard GetReviewer(Maintenance maintain)
        {
            return new ReviewerDashboard
            {
                Reviewer_ID=maintain.Reviewer_ID,
                ProjectName = maintain.ProjectName,
                Tracking_No = maintain.Tracking_No,
                Location_ID = maintain.Location_ID,               
                Client_ID = maintain.Client_ID,
                OpenWorkOrder = maintain.OpenWorkOrder
            };
        }

        public static Boolean checkWorkOrderCompleted(long Inspection_ID)
        {
            return new Maintenance().checkWorkOrderCompleted(Inspection_ID);
        }


        public static bool setFirstReviewerLoginInfo(long Reviewer_ID, long Location_ID)
        {
            return new Maintenance().setFirstReviewerLoginInfo(Reviewer_ID,Location_ID);
        }
    }
}