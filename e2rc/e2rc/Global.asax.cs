using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using e2rc.Models.Security;
using e2rcModel.DataAccessLayer;
using System.Data;
using System.IO;


namespace e2rc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    // http://e2rc.azurewebsites.net

    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
           
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
          
            if (!(Request.Url.ToString().Contains("azurewebsites.net")) && (!Request.Url.ToString().Contains("e2rc.wsisites.net")) && (!Request.Url.ToString().Contains("localhost")))
            {
                var routeData = new RouteData();
                routeData.Values["controller"] = "Error";
                routeData.Values["action"] = "PageNotFound";


                IController controller = new e2rc.Controllers.ErrorController();
                var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
                controller.Execute(rc);
            }

            if ((Request.Url.ToString().Contains("azurewebsites.net")) || (Request.Url.ToString().Contains("e2rc.wsisites.net")) || (Request.Url.ToString().Contains("localhost")))
            {
               
                if (Request.Url.ToString().Contains("logo.png?Inspection_ID"))
                {
                    UpdateSentMailStatus(Convert.ToInt64(Request["Inspection_ID"]));
                }

                if (Request.Url.ToString().Contains("email.png?Inspection_ID"))
                {
                    WorkOrderCompletedAutoResponder(Convert.ToInt64(Request["Inspection_ID"]));
                }
                if (Request.Url.ToString().Contains("ReviewerInspection/DownloadPDF?Inspection_ID"))
                {
                    DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getFormID", new object[] { "@Inspection_ID" }, new object[] { Convert.ToInt64(Request["Inspection_ID"]) });
                    if (dataset != null || dataset.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt64(dataset.Tables[0].Rows[0]["Form_ID"]) == 1008)
                        {
                            
                            if (Convert.ToInt64(dataset.Tables[0].Rows[0]["Reviewer_ID"]) == 0 || (dataset.Tables[0].Rows[0]["Reviewer_ID"]) == null)
                            {
                                setFirstReviewerInfo(Convert.ToInt64(Request["Inspection_ID"]), Convert.ToInt64(Request["Reviewer_ID"]));
                            }
                            StationInspectionSentMailDownloadPDF(Convert.ToInt64(Request["Inspection_ID"]));
                        }
                        else
                        {
                            if ((Convert.ToInt64(dataset.Tables[0].Rows[0]["Reviewer_ID"]) == 0) || ((dataset.Tables[0].Rows[0]["Reviewer_ID"])== null))
                            {
                                setFirstReviewerInfo(Convert.ToInt64(Request["Inspection_ID"]), Convert.ToInt64(Request["Reviewer_ID"]));
                            }
                            InspectionSentMailDownloadPDF(Convert.ToInt64(Request["Inspection_ID"]));
                           
                        }
                    }
                }

                if (Request.Url.ToString().Contains("DownloadPDF?Inspection_ID"))
                {
                    DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getFormID", new object[] { "@Inspection_ID" }, new object[] { Convert.ToInt64(Request["Inspection_ID"]) });
                    if (dataset != null || dataset.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt64(dataset.Tables[0].Rows[0]["Form_ID"]) == 1008)
                        {
                            StationInspectionSentMailDownloadPDF(Convert.ToInt64(Request["Inspection_ID"]));
                        }
                        else
                        {
                            InspectionSentMailDownloadPDF(Convert.ToInt64(Request["Inspection_ID"]));
                        }
                    }
                }
                if (Request.Url.ToString().Contains("ServiceActionMaintenanceWorkToE2RC?Inspection_ID"))
                {
                    CallActionWorkToE2RC(Convert.ToInt64(Request["Inspection_ID"]), Convert.ToInt16(Request["ActionDay"]), Convert.ToString(Request["Email"]));
                }                

                if (Request.Url.ToString().Contains("ActionMaintenanceWorktoE2RC?Inspection_ID"))
                {
                    ActionMaintenanceWorkToE2RC(Convert.ToInt64(Request["Inspection_ID"]), Convert.ToString(Request["InspEmail"]));
                }

                if (Request.Url.ToString().Contains("ActionMaintenanceWorktoInspector?Inspection_ID"))
                {
                    ActionMaintenanceWorktoInspector(Convert.ToInt64(Request["Inspection_ID"]));
                }

                if (Request.Url.ToString().Contains("GeneratePDF?Inspection_ID")) //Dashboard/GeneratePDF?Inspection_ID=1512
                {
                    DashboardGeneratePDF(Convert.ToInt64(Request["Inspection_ID"]));
                }

                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                    CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                    newUser.User_ID = serializeModel.User_ID;
                    newUser.Name = serializeModel.Name;
                    newUser.UserName = serializeModel.UserName;
                    newUser.Role = serializeModel.Role;
                    newUser.Email = serializeModel.Email;
                    newUser.LogoPath = serializeModel.LogoPath;
                    HttpContext.Current.User = newUser;
                }
            }

        }

        private void InspectionSentMailDownloadPDF(long Inspection_ID)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "Inspection";
            routeData.Values["action"] = "DownloadPDF";
            routeData.Values["Inspection_ID"] = Inspection_ID;
            IController controller = new e2rc.Controllers.InspectionController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
        }
        private void StationInspectionSentMailDownloadPDF(long Inspection_ID)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "StationInspection";
            routeData.Values["action"] = "DownloadPDF";
            routeData.Values["Inspection_ID"] = Inspection_ID;
            IController controller = new e2rc.Controllers.StationInspectionController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
        }

        private void UpdateSentMailStatus(long Inspection_ID)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "Inspection";
            routeData.Values["action"] = "MailAutoresponder";
            routeData.Values["Inspection_ID"] = Inspection_ID;
            IController controller = new e2rc.Controllers.InspectionController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
        }


        private void WorkOrderCompletedAutoResponder(long Inspection_ID)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "Inspection";
            routeData.Values["action"] = "WorkOrderCompletedAutoResponder";
            routeData.Values["Inspection_ID"] = Inspection_ID;
            IController controller = new e2rc.Controllers.InspectionController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            //if ((Request.Url.ToString().Contains("e2rc.azurewebsites.net")) || (Request.Url.ToString().Contains("e2rc.wsisites.net")))
            //{
            //    HttpContext.Current.Response.AddHeader(
            //                "Access-Control-Allow-Origin", "*");
            //}
            //else
            //{
            //}
        }
        private void CallActionWorkToE2RC(long Inspection_ID, Int16 days, string Email)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "Dashboard";
            routeData.Values["action"] = "ServiceActionMaintenanceWorkToE2RC";
            routeData.Values["Inspection_ID"] = Inspection_ID;
            routeData.Values["ActionDay"] = days;
            routeData.Values["Email"] = Email;  
            IController controller = new e2rc.Controllers.DashboardController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
        }       
        private void DashboardGeneratePDF(long Inspection_ID)
        {
            var exception = Server.GetLastError();
            // TODO: Log the exception or something
            Response.Clear();
            Server.ClearError();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Dashboard";
            routeData.Values["action"] = "GeneratePDF";
            routeData.Values["Inspection_ID"] = Inspection_ID;

            IController controller = new e2rc.Controllers.DashboardController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
        }

        private void ActionMaintenanceWorkToE2RC(long Inspection_ID, string InspectionReportEmailID)
        {
            var exception = Server.GetLastError();
            // TODO: Log the exception or something
            Response.Clear();
            Server.ClearError();

            var routeData = new RouteData();
            routeData.Values["controller"] = "Inspection";
            routeData.Values["action"] = "ActionMaintenanceWorktoE2RC";
            routeData.Values["Inspection_ID"] = Inspection_ID;
            routeData.Values["InspEmail"] = InspectionReportEmailID;
           // routeData.Values["Date"] = date;
            IController controller = new e2rc.Controllers.InspectionController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
        }


        private void ActionMaintenanceWorktoInspector(long Inspection_ID)
        {
            var exception = Server.GetLastError();
            // TODO: Log the exception or something
            Response.Clear();
            Server.ClearError();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Inspection";
            routeData.Values["action"] = "ActionMaintenanceWorktoInspector";
            routeData.Values["Inspection_ID"] = Inspection_ID;
           // routeData.Values["Date"] = date;
            IController controller = new e2rc.Controllers.InspectionController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
        }


        private void setFirstReviewerInfo(long Inspection_ID, long Reviewer_ID)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "Inspection";
            routeData.Values["action"] = "setFirstReviewerInfo";
            routeData.Values["Inspection_ID"] = Inspection_ID;
            routeData.Values["Reviewer_ID"] = Reviewer_ID;
            IController controller = new e2rc.Controllers.InspectionController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
        }

    }
}