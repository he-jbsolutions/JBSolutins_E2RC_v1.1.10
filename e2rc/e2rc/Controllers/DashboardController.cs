using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Mvc;
using e2rc.Models;
using e2rcModel.BusinessLayer;
using e2rc.Models.Repository;
using PagedList;
using iTextSharp.text;
using System.Linq;


using Winnovative;
using System.Web.Configuration;
//using System.Web.Mail;
using System.Net.Mail;

namespace e2rc.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        HtmlToPdfConverter htmlToPdfConverter;
        [HttpGet]
        public ActionResult Index(int? Page=1, string sortOrder = "")
        {

          //  bool test = MailSetting.MailSend("donotreply@e2rc.com", "bhaveshjain@bhaveshitconsultant.com", "donotreply@e2rc.com", "Testing from E2RC", "This is testing from e2rc.", null);
          
            e2rc.Models.DashboardModel DashboardModel = new Models.DashboardModel();
            ViewBag.Inspection_IDSortParm = sortOrder == "Inspection_ID" ? "Inspection_ID_desc" : "Inspection_ID";
            ViewBag.Customer_NameSortParm = sortOrder == "CustomerName" ? "CustomerName_desc" : "CustomerName";
            ViewBag.InspectedBySortParm = sortOrder == "Inspector" ? "Inspector_desc" : "Inspector";
            ViewBag.ProjectNameSortParm = sortOrder == "ProjectName" ? "ProjectName_desc" : "ProjectName";           
            ViewBag.InspectionDateSortParm = sortOrder == "InspectionDate" ? "InspectionDate_desc" : "InspectionDate";
            ViewBag.TrackingNoSortParm = sortOrder == "TrackingNo" ? "TrackingNo_desc" : "TrackingNo";
            ViewBag.CompanyNameSortParm = sortOrder == "CompanyName" ? "CompanyName_desc" : "CompanyName";
            ViewBag.ClientNameSortParm = sortOrder == "ClientName" ? "ClientName_desc" : "ClientName";
            ViewBag.LocationSortParm = sortOrder == "Location" ? "Location_desc" : "Location";
            ViewBag.MailingAddressSortParm = sortOrder == "MailingAddress" ? "MailingAddress_desc" : "MailingAddress";
            ViewBag.StateNameSortParm = sortOrder == "StateName" ? "StateName_desc" : "StateName";
            ViewBag.CitySortParm = sortOrder == "City" ? "City_desc" : "City";
            ViewBag.ZipCodeSortParm = sortOrder == "ZipCode" ? "ZipCode_desc" : "ZipCode";
            ViewBag.FranchiseSortParm = sortOrder == "Franchise" ? "Franchise_desc" : "Franchise";
            ViewBag.ModifiedDateSortParm = sortOrder == "ModifiedDate" ? "ModifiedDate_desc" : "ModifiedDate";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.UserNameSortParm = sortOrder == "Username" ? "Username_desc" : "Username";
            ViewBag.OpenWorkOrderSortParm = sortOrder == "OpenWorkOrder" ? "OpenWorkOrder_desc" : "OpenWorkOrder";
            if (User.Role == "Inspector")
            {
                var InspectorList=DashboardRepository.UploadedDataSearchSort(sortOrder, User.Role, (long)User.User_ID);
                if (InspectorList != null)
                {
                    ViewBag.InspecorProjectCount = InspectorList.Count();
                    DashboardModel.ActionMaintenanceList = InspectorList.ToPagedList((int)Page, 25);
                }
                else
                {
                    DashboardModel.ActionMaintenanceList = new List<ActionMaintenanceData>().ToPagedList(1, 1);
                }
            }
            else if (User.Role == "Franchise Admin")
            {
                var franchiseList = DashboardRepository.FranchiseDashboard(sortOrder, User.Role, (long)User.User_ID);
                if (franchiseList != null)
                {
                    ViewBag.FranCompanyCount = franchiseList.Count();
                    DashboardModel.FranchiseDataList = franchiseList.ToPagedList((int)Page, 25);
                }
                else
                {
                    DashboardModel.FranchiseDataList = new List<FranchiseDashboard>().ToPagedList(1, 1);
                }
                
            }
            else if (User.Role == "Project Manager")
            {
                var list = DashboardRepository.ProjectManagerDashboard(sortOrder, User.Role, (long)User.User_ID);
                if (list != null)
                {
                    ViewBag.ProjectCount = list.Count();
                    DashboardModel.ProjectManagerDataList = list.ToPagedList((int)Page, 25);
                }
                else
                {
                    ViewBag.ProjectCount = 0;
                    DashboardModel.ProjectManagerDataList = new List<ProjectManagerDashboard>().ToPagedList(1, 1);
                }
            }
            else if (User.Role == "Company")
            {                
                var clientList = DashboardRepository.ClientMainDashboard(sortOrder, User.Role, (long)User.User_ID);
                if (clientList != null)
                {
                    ViewBag.ClientprojectCount = clientList.Count();
                    DashboardModel.ClientDataList = clientList.ToPagedList((int)Page, 25);                   
                }
                else
                {
                    ViewBag.ClientprojectCount = 0;
                    DashboardModel.ClientDataList = new List<ClientDashboard>().ToPagedList(1,1);
                }                               
            }
            else if (User.Role == "Owner" || User.Role == "Operator")
            {
                var DirectorReportList = DashboardRepository.DirectorDashboard(sortOrder, User.Role, (long)User.User_ID);
                if (DirectorReportList != null)
                {
                    ViewBag.OwnerList = DirectorReportList.Count();
                    DashboardModel.DirectorReportList = DirectorReportList.ToPagedList((int)Page, 25);
                }
                else
                {
                    DashboardModel.DirectorReportList = new List<DirectorReportData>().ToPagedList(1, 1);
                }
            }
            else if (User.Role == "Super Admin")
            {
                var SuperadminList = DashboardRepository.SuperAdminDashboard(sortOrder, User.Role, (long)User.User_ID);
                if (SuperadminList != null)
                {
                    ViewBag.SuperAdminFranchiseCount = SuperadminList.Count();
                    DashboardModel.SuperAdminActiveList = SuperadminList.ToPagedList((int)Page, 25);
                }
                else
                {
                    DashboardModel.SuperAdminActiveList = new List<SuperAdminData>().ToPagedList(1, 1);
                }                
            }
            else if (User.Role == "Reviewer")
            {  
               var  ReviewerList = DashboardRepository.ReviewerDashboard(sortOrder, User.Role, (long)User.User_ID);
               if (ReviewerList != null)
                { 
                    ViewBag.ReviewerCount = ReviewerList.Count();
                    DashboardModel.ReviewerDataList = ReviewerList.ToPagedList((int)Page, 25);
                }
                else
                {
                    DashboardModel.ReviewerDataList = new List<ReviewerDashboard>().ToPagedList(1, 1);
                }
            }

            if (DashboardModel != null)
            {
                return View(DashboardModel);
            }
            return View();
        }
                
        [HttpPost]
        public JsonResult AssignInspector(long Location_ID, long Inspector_ID)
        {
            return Json(DashboardRepository.AssignInspector((long)User.User_ID, Location_ID, Inspector_ID));
        }


        [HttpPost]
        public JsonResult MaintenanceCompleted(long UploadData_ID, DateTime ClosedDate)
        {
            long? lCorrectiveID = CorrectiveActionRepository.Create(UploadData_ID, false, (long)User.User_ID);
            string sUserName = DashboardRepository.MaintenanceCompleted((long)User.User_ID, UploadData_ID, ClosedDate) ? User.Name : User.Name;
            //return Json(DashboardRepository.MaintenanceCompleted((long)User.User_ID, UploadData_ID, ClosedDate) ? User.Name : User.Name);
            var result = new { sUsername = sUserName, lCorrective_ID = lCorrectiveID };

            return Json(result);
        }

        [HttpPost]
        public JsonResult ActionCompleted(long UploadData_ID, DateTime ClosedDate)
        {
            string sUserName = DashboardRepository.ActionCompleted((long)User.User_ID, UploadData_ID, ClosedDate) ? User.Name : User.Name;
            long? lCorrectiveID = CorrectiveActionRepository.Create(UploadData_ID, true, (long)User.User_ID);
            //return Json(DashboardRepository.ActionCompleted((long)User.User_ID, UploadData_ID, ClosedDate) ? User.Name : User.Name);
            var result = new { sUsername = sUserName, lCorrective_ID = lCorrectiveID };
            return Json(result);
        }

        [HttpPost]
        public void checkWorkOrderCompleted(long Inspection_ID)
        {
          Boolean IsCompleted=  DashboardRepository.checkWorkOrderCompleted(Inspection_ID);
          if (IsCompleted)
          {
              string bottomCompanyLogoUrl = "";
              dynamic clientDetails = InspectionRepository.getClientDetailsByInspectionId(Inspection_ID);

              if (clientDetails.GetType().GetProperty("UploadLogoPath").GetValue(clientDetails, null) != "")
              {
                  bottomCompanyLogoUrl = ConfigurationManager.AppSettings["CompanyLogo"] + clientDetails.GetType().GetProperty("UploadLogoPath").GetValue(clientDetails, null);
                  //headerHtmlUrl = Server.MapPath("~/" + clientDetails.GetType().GetProperty("UploadLogoPath").GetValue(clientDetails, null));
              }
              else
              {
                  bottomCompanyLogoUrl = WebConfigurationManager.AppSettings["Imgemail"];
              }

              MailSetting.CompletionWorkOrederMailToContrator(Inspection_ID, bottomCompanyLogoUrl); 
          }
        }

        public void ActionAcknowlegment(long UploadData_ID, int day)
        {
            DashboardRepository.ActionAcknowlegment(UploadData_ID, day);
        }

        public void MaintenanceAcknowlegment(long UploadData_ID, int day)
        {
            DashboardRepository.MaintenanceAcknowlegment(UploadData_ID, day);
        }

        [AllowAnonymous]
        public ViewResult ServiceActionMaintenanceWorkToE2RC()
        {
           try
            {
                string sCompanyLogoUrl = AddCompanyLogoOnMailSend(Convert.ToInt64(RouteData.Values["Inspection_ID"]));
                ViewBag.IsMailSentSuccessfully = MailSetting.ServiceActionMaintenanceMailToE2rc(
                    Convert.ToInt64(RouteData.Values["Inspection_ID"]), Convert.ToInt16(RouteData.Values["ActionDay"]), Convert.ToString(RouteData.Values["Email"]), sCompanyLogoUrl);
                    return View("SendMailMessage");
            }
            catch
            {
            }
            return View("SendMailMessage");
        }

        [AllowAnonymous]
        public ActionResult SendMailMessage()
        {
            return View();
        }

        
        [HttpPost]
        public JsonResult GenrateMaintenanceWorkOrder(long Inspection_ID)
        {           
            return Json(DashboardRepository.GenrateMaintenanceWorkOrder(Inspection_ID));           
        }

        [HttpPost]
        public JsonResult GenerateMaintenanceCompletedList(long Inspection_ID)
        {
            return Json(DashboardRepository.GenerateMaintenanceCompletedList(Inspection_ID));
        }

        [HttpPost]
        public JsonResult GenrateActioneWorkOrder(long Inspection_ID)
        {
            return Json(DashboardRepository.GenrateActioneWorkOrder(Inspection_ID));
        }

        [HttpPost]
        public JsonResult GenerateActionCompletedList(long Inspection_ID)
        {
            return Json(DashboardRepository.GenerateActionCompletedList(Inspection_ID));
        }

        private string AddCompanyLogoOnMailSend(long Inspection_ID)
        {
            string bottomCompanyLogoUrl = "";
            dynamic clientDetails = InspectionRepository.getClientDetailsByInspectionId(Inspection_ID);

            if (clientDetails.GetType().GetProperty("UploadLogoPath").GetValue(clientDetails, null) != "")
            {
                bottomCompanyLogoUrl = ConfigurationManager.AppSettings["CompanyLogo"] + clientDetails.GetType().GetProperty("UploadLogoPath").GetValue(clientDetails, null);
                //headerHtmlUrl = Server.MapPath("~/" + clientDetails.GetType().GetProperty("UploadLogoPath").GetValue(clientDetails, null));
            }
            else
            {
                bottomCompanyLogoUrl = ConfigurationManager.AppSettings["ImgLogo"];
            }
            return bottomCompanyLogoUrl;
        }

        [HttpGet]
        [AllowAnonymous]
        public void GeneratePDF()
        {

            long Inspection_ID = Convert.ToInt64(RouteData.Values["Inspection_ID"]);
            StringBuilder sbActionMaintenanceTemplate = new StringBuilder();
            int? page = 1;

            sbActionMaintenanceTemplate.Append("<html>");
            sbActionMaintenanceTemplate.Append("<body>");
            e2rc.Models.DashboardModel DashboardModel = new Models.DashboardModel();
            var ActionMaintenanceData = DashboardRepository.getGeneratePDFActionMaintenanceList(Inspection_ID).FirstOrDefault(item => item.Inspection_Id == Inspection_ID);
            //style=\"padding:50px
            sbActionMaintenanceTemplate.AppendLine("<div  style=\"padding-left: 50px;font-family:sans-serif\"><h1>Work Order</h1></div>");
            sbActionMaintenanceTemplate.AppendLine("<div>");
            sbActionMaintenanceTemplate.AppendLine("<table border=\"0\"  style=\"padding-left: 50px;Width:60%;font-Size:17px;font-family:sans-serif\">");
            if (ActionMaintenanceData != null)
            {

                sbActionMaintenanceTemplate.Append("<tr>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:18px;\">Customer  </td>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:15x;\">" + ActionMaintenanceData.ClientName + "</td>");
                sbActionMaintenanceTemplate.Append("</tr>");

                sbActionMaintenanceTemplate.Append("<tr>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:18px;\"> Project Name : </td>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:15px;\">" + ActionMaintenanceData.ProjectName + "</td>");
                sbActionMaintenanceTemplate.Append("</tr>");

                sbActionMaintenanceTemplate.Append("<tr>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:18px;\">Site Contact 1  </td>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:15px;\">" + ActionMaintenanceData.InspectionReportEmails + "</td>");
                sbActionMaintenanceTemplate.Append("</tr>");

                sbActionMaintenanceTemplate.Append("<tr>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:18px;\">Site Contact 2  </td>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:15px;\">" + ActionMaintenanceData.WorkOrdersEmails + "</td>");
                sbActionMaintenanceTemplate.Append("</tr>");

                sbActionMaintenanceTemplate.Append("<tr>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:18px;\">Location </td>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:15px;\">" + ActionMaintenanceData.Location + "</td>");
                sbActionMaintenanceTemplate.Append("</tr>");

                sbActionMaintenanceTemplate.Append("<tr>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:18px;\"> Date Created </td>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:15px;\">" + ActionMaintenanceData.CreatedDate + "</td>");
                sbActionMaintenanceTemplate.Append("</tr>");

                sbActionMaintenanceTemplate.Append("<tr>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:18px;\">Date Due  </td>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:15px;\">" + ActionMaintenanceData.DueDate + "</td>");
                sbActionMaintenanceTemplate.Append("</tr>");

                sbActionMaintenanceTemplate.Append("<tr>");
                sbActionMaintenanceTemplate.Append("<td style=\"font-Size:18px;\">Site Classification  </td>");
                // sbActionMaintenanceTemplate.Append("<td style=\"font-Size:17px;\">" + ActionMaintenanceData.DueDate + "</td>");
                sbActionMaintenanceTemplate.Append("</tr>");

            }
            sbActionMaintenanceTemplate.AppendLine("</table></div></br>");
            List<Maintenance> UploadDataActionList = new List<Maintenance>();
            UploadDataActionList = DashboardRepository.GenrateActioneWorkOrder(Inspection_ID);
            int RowCount = 0;
            if (UploadDataActionList != null)
            {
                sbActionMaintenanceTemplate.AppendLine("<div  style=\"padding-left: 50px;Width:60%;font-Size:17px;font-family:sans-serif\"><h3> Action Required </h3></div>");
                int i = 0;
                foreach (var action in UploadDataActionList)
                {

                    i++;
                    RowCount++;
                    sbActionMaintenanceTemplate.Append("<div  style=\"padding-left: 50px;Width:100%;font-Size:20px;font-family:sans-serif\">" + i + " . " + action.SEControls + "</div>");
                    sbActionMaintenanceTemplate.Append("<div style=\"padding-left: 70px;font-Size:20px;font-family:sans-serif\"><b> Corrective Maintenance   :</b> " + action.ActionRequired + "</div>");
                    sbActionMaintenanceTemplate.Append("<div style=\"padding-left: 70px;font-Size:20px;font-family:sans-serif\"><b> Required Item   :</b> " + action.Quantity +
                                                                        "(" + action.UOM + ")" + action.UPLocation + "</div>");
                    sbActionMaintenanceTemplate.Append("<div style=\"padding-left: 70px;Width:100%;font-Size:20px;font-family:sans-serif\">Photo available online - <a href='" + Server.MapPath("~/Inspection" + action.UploadImagePath) + "'>Click here to View</a></div> <br />");

                    if (RowCount % 6 == 0)
                    {
                        sbActionMaintenanceTemplate.Append("</br></br></br></br></br></br></br></br></br></br></br></br>");
                    }
                }
            }
            else
            {
                sbActionMaintenanceTemplate.AppendLine("<div  style=\"padding-left: 50px;Width:60%;font-Size:17px;font-family:sans-serif\"><h3> Action Required : Not </h3></div>");
            }
            List<Maintenance> UploadDataMaintenanceList = new List<Maintenance>();
            UploadDataMaintenanceList = DashboardRepository.GenrateMaintenanceWorkOrder(Inspection_ID);
            if (UploadDataMaintenanceList != null)
            {
                sbActionMaintenanceTemplate.AppendLine("<div  style=\"padding-left: 50px;Width:60%;font-Size:17px;font-family:sans-serif\"><h3> Maintenance Needed </h3></div>");
                int i = 0;
                foreach (var maintenance in UploadDataMaintenanceList)
                {

                    i++;
                    RowCount++;
                    sbActionMaintenanceTemplate.Append("<div  style=\"padding-left: 50px;Width:100%;font-Size:20px;font-family:sans-serif\">" + i + " . " + maintenance.SEControls + "</div>");
                    sbActionMaintenanceTemplate.Append("<div style=\"padding-left: 70px;font-Size:20px;font-family:sans-serif\"><b> Corrective Action   :</b> " + maintenance.MaintenanceNeeded + "</div>");
                    sbActionMaintenanceTemplate.Append("<div style=\"padding-left: 70px;font-Size:20px;font-family:sans-serif\"><b> Required Item   : </b>" + maintenance.Quantity +
                        "(" + maintenance.UOM + ")" + maintenance.UPLocation + "</div>");
                    sbActionMaintenanceTemplate.Append("<div style=\"padding-left: 70px;Width:100%;font-Size:20px;font-family:sans-serif\">Photo available online - <a href='" + Server.MapPath("~/Inspection" + maintenance.UploadImagePath) + "'>Click here to View</a></div> <br />");
                    if (RowCount % 8 == 0 && i < 2)
                    {
                        sbActionMaintenanceTemplate.Append("</br></br></br></br></br></br>");
                    }

                }
            }
            else
            {
                sbActionMaintenanceTemplate.AppendLine("<div  style=\"padding-left: 50px;Width:60%;font-Size:17px;font-family:sans-serif\"><h3> Maintenance Needed : No Needed </h3></div>");
            }
            sbActionMaintenanceTemplate.AppendLine("</body></html>");

            // Get the server IP and port
            String serverIP = "137.116.153.126"; //"111.221.99.23";
            uint serverPort = uint.Parse("45001");

            // Create a HTML to PDF converter object with default settings
            Winnovative.HtmlToPdfClient. HtmlToPdfConverter htmlToPdfConverter = new  Winnovative.HtmlToPdfClient.HtmlToPdfConverter(serverIP, serverPort);

            // Set optional service password
            //if (textBoxServicePassword.Text.Length > 0)
            htmlToPdfConverter.ServicePassword = "R3C@librate";

            // Set license key received after purchase to use the converter in licensed mode
            // Leave it not set to use the converter in demo mode
            htmlToPdfConverter.LicenseKey = "fvDh8eDx4fHg4P/h8eLg/+Dj/+jo6Og=";

            // Set HTML Viewer width in pixels which is the equivalent in converter of the browser window width
            htmlToPdfConverter.HtmlViewerWidth = int.Parse("1024");

            // Set HTML viewer height in pixels to convert the top part of a HTML page 
            // Leave it not set to convert the entire HTML
            //if (htmlViewerHeightTextBox.Text.Length > 0)
            //    htmlToPdfConverter.HtmlViewerHeight = int.Parse(htmlViewerHeightTextBox.Text);

            // Set PDF page size which can be a predefined size like A4 or a custom size in points 
            // Leave it not set to have a default A4 PDF page
            htmlToPdfConverter.PdfDocumentOptions.PdfPageSize = Winnovative.HtmlToPdfClient.PdfPageSize.A4;

            // Set PDF page orientation to Portrait or Landscape
            // Leave it not set to have a default Portrait orientation for PDF page
            htmlToPdfConverter.PdfDocumentOptions.PdfPageOrientation = Winnovative.HtmlToPdfClient.PdfPageOrientation.Portrait;

            // Set the maximum time in seconds to wait for HTML page to be loaded 
            // Leave it not set for a default 60 seconds maximum wait time
            htmlToPdfConverter.NavigationTimeout = int.Parse("60");

            // Set an adddional delay in seconds to wait for JavaScript or AJAX calls after page load completed
            // Set this property to 0 if you don't need to wait for such asynchcronous operations to finish
            //if (conversionDelayTextBox.Text.Length > 0)
            //    htmlToPdfConverter.ConversionDelay = int.Parse(conversionDelayTextBox.Text);

            // The buffer to receive the generated PDF document
            byte[] outPdfBuffer = null;

            //if (convertUrlRadioButton.Checked)
            //{
            //    string url = urlTextBox.Text;

            // Convert the HTML page given by an URL to a PDF document in a memory buffer
            outPdfBuffer = htmlToPdfConverter.ConvertHtml(sbActionMaintenanceTemplate.ToString(), ConfigurationManager.AppSettings["SiteURL"]);
            //http://e2rc.azurewebsites.net/Inspection/Details?Inspection_ID=1508");
            //}

             // Set response content type
            Response.AddHeader("Content-Type", "application/pdf");

            // Instruct the browser to open the PDF file as an attachment or inline
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename=e2rc-{1}.pdf; size={2}",
                 "attachment", DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff"), outPdfBuffer.Length.ToString()));

            // Write the PDF document buffer to HTTP response
            Response.BinaryWrite(outPdfBuffer);

            // End the HTTP response and stop the current page processing
            Response.End();

           // WriteFileToResponse(CreatePdf(Convert.ToString(sbActionMaintenanceTemplate)));
        }



        private string CreatePdf(string strContent)
        {
            Winnovative.Document document = new Winnovative.Document();
            Winnovative.PdfPage pdfPage = document.AddPage();
            htmlToPdfConverter = new HtmlToPdfConverter();
            htmlToPdfConverter.PdfDocumentOptions.ShowHeader = true;          
            PdfFont font = document.Fonts.Add(new System.Drawing.Font(new System.Drawing.FontFamily("Arial"), 12, System.Drawing.GraphicsUnit.Point));           
            htmlToPdfConverter.PrerenderEnabled = true;
            AddHtmlHeader(document);
            //AddHtmlFooter(document, font);
            //for (int iCount = 0; iCount < strContent.Length; iCount++)
            //{
            if (strContent != "")
            {
                string strTempContent;
                strTempContent = strContent;
                //  strTempContent = "<html><head><link href='../../Content/css/bootstrap.css' rel='stylesheet'/><link href='../../Content/css/bootstrap.min.css' rel='stylesheet' /><link href='../../Content/css/signin.css' rel='stylesheet' /><link href='../../Content/css/style.css' rel='stylesheet' /><script src='../../Scripts/bootstrap.min.js' type='text/javascript'></script></head><body width=100%;>" + strContent[iCount] + "</body></html>";
                strTempContent = @"<!DOCTYPE html >
                                                <html lang='en'>
                                                <head>                                              
                                              <link href='../../Content/css/bootstrap.css' rel='stylesheet'/>  
                                              <link href='../../Content/css/bootstrap.min.css' rel='stylesheet' />
                                              <link href='../../Content/css/signin.css' rel='stylesheet' />
                                              <link href='../../Content/css/downloadStyle.css' rel='stylesheet' />
                                              <script src='../../Scripts/bootstrap.min.js' type='text/javascript'></script>
                                                </head>
                                                  <body style='background-image:none !important;'>" + strContent + "</body></html>";

                htmlToPdfConverter.LicenseKey = "fvDh8eDx4fHg4P/h8eLg/+Dj/+jo6Og=";
                document.CompressionLevel = PdfCompressionLevel.Normal;
                document.Margins = new Margins(0, 10, 0, 0);
                document.DocumentInformation.Author = "HTML to PDF Converter";
                document.ViewerPreferences.HideToolbar = false;
                // set if the images are compressed in PDF with JPEG to reduce the PDF document size
                document.JpegCompressionEnabled = true;
                PdfPageSize pg = new PdfPageSize((float)792, (float)610);

                Winnovative.PdfPage page = document.Pages.AddNewPage(pg, new Margins(0, 0, -0, 0), PdfPageOrientation.Portrait);
                document.OpenAction.Action = new PdfActionJavaScript("this.zoom=125;");
                float xLocation = float.Parse("0");
                float yLocation = float.Parse("0");
                float width = float.Parse("0");
                float height = float.Parse("0");
                // convert HTML to PDF
                HtmlToPdfElement htmlToPdfElement;
                string htmlStringToConvert = strTempContent;
                string baseURL = "";
                htmlToPdfElement = new HtmlToPdfElement(xLocation, yLocation, width, height, htmlStringToConvert, baseURL);
                htmlToPdfElement.StretchToFit = true;
                page.AddElement(htmlToPdfElement);
                htmlToPdfElement.LiveUrlsEnabled = true;
                //}
            }
            document.Pages.Remove(0);
            string strFileName;
            try
            {
                strFileName = "e2rc-" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss.fff") + ".pdf";
                string strpath = Server.MapPath("PDFFiles") + "\\" + strFileName;
                document.Save(strpath);
            }
            finally
            {
                document.Close();
            }
            return strFileName;
        }
        private void WriteFileToResponse(string fileName)
        {
            Response.Clear();           
            Response.AppendHeader("content-disposition", "attachment; filename=\"" + fileName + "\""); // name);
            Response.ContentType = "Application/pdf";
            Response.WriteFile(Server.MapPath("PDFFiles") + "\\" + fileName);           
            Response.Flush();
            Response.Close();         
            System.IO.File.Delete(Server.MapPath("PDFFiles") + "\\" + fileName);
            Response.End();
        }
        private void AddHtmlHeader(Winnovative.Document document)
        {
            string headerHtmlUrl = ConfigurationManager.AppSettings["ImgLogo"];
            document.AddHeaderTemplate(87);
            // Create a HTML element to be added in header
            HtmlToPdfElement headerHtmlToPdf = new HtmlToPdfElement(260f, 10f, 2550, 500, headerHtmlUrl);
            // Set the HTML element to fit the container height
            headerHtmlToPdf.FitHeight = true;
            // Add HTML element to header
            headerHtmlToPdf.AvoidImageBreak = true;
            document.Header.DrawOnFirstPage = true;
            document.Header.AddElement(headerHtmlToPdf);

        }

        //GenratePMLocationInspectionWorkOrder

        [HttpPost]
        public JsonResult GenratePMLocationInspectionWorkOrder(long Location_ID)
        {
            return Json(DashboardRepository.GenratePMLocationInspectionWorkOrder(Location_ID));
        }

        

        [HttpPost]
        public JsonResult GenratePMLocationInspectionCompleted(long Location_ID)
        {
            return Json(DashboardRepository.GenratePMLocationInspectionCompleted(Location_ID));
        }
        
        [HttpPost]
        public JsonResult UsersOfActiveLocation(long Location_ID)
        {
            return Json(DashboardRepository.ActiveLocationUsers(Location_ID));
        }
       
    }
}
