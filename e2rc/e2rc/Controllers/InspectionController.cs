using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;


using Winnovative.HtmlToPdfClient;
using System.Drawing;


namespace e2rc.Controllers
{
    [Authorize]
    public class InspectionController : BaseController
    {
        //HtmlToPdfConverter htmlToPdfConverter;
        int i = 0;
        string[] ViewArray;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetInpectorDetails()
        {
            return Json(InspectionRepository.GetInspectorDetails(User.UserName), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (TempData["MsgFromPage"] != null)
            {
                ViewBag.message = TempData["MsgFromPage"];
                TempData["MsgFromPage"] = null;
            }
                return View(new InspectionModel()
                {
                    Date = DateTime.Now,                   
                });           
        }

        [HttpPost]
        public ActionResult Create(InspectionModel inspectionModel)
        {
            inspectionModel.CreatedBy = (long)User.User_ID;
            string UserRole = User.Role;
            inspectionModel.FormName = "F1";
           // inspectionModel.generalInspectionModel.Date=DateTime.Now;
            inspectionModel.weatherInspectionModel.StormDetailsModelList = BindStormDetailsModel();
            inspectionModel.weatherInspectionModel.UploadDataModelList = BindUploadDataModel();
            long inspectionID = InspectionRepository.Create(inspectionModel);
             if (inspectionID > 0) {
                 if (inspectionModel.generalInspectionModel.IsComplete)
                 {
                     string pdfPath = string.Empty;
                     string companyName = inspectionModel.generalInspectionModel.CustomerName;
                     string ProjectName = inspectionModel.generalInspectionModel.location.Name;
                     string trackingNO = inspectionModel.generalInspectionModel.Tracking_No;
                     DateTime date = inspectionModel.generalInspectionModel.Date;
                     string location = inspectionModel.generalInspectionModel.locationName;
                     string InspectorName = inspectionModel.generalInspectionModel.inspector.Name;
                     string sCompanyLogoUrl = AddCompanyLogoOnMailSend(inspectionID);
                     MailSetting.SendMail(inspectionModel.generalInspectionModel.InspectorEmail, companyName, ProjectName, InspectorName, trackingNO, date, location, pdfPath, inspectionID, sCompanyLogoUrl);
                     MailSetting.SendMail(ProjectName, inspectionID, date, companyName, sCompanyLogoUrl);
                 }
            }
             else
             {
                 TempData["MsgFromPage"] = " Inspection report for this project is already submitted today.";

             }
             //return RedirectToAction("Index");
             if (inspectionID > 0)
             {
                 return RedirectToAction("Index");

             }
             else
             {
                 return RedirectToAction("Create");
             }
            
        }

        [HttpGet]
        public ActionResult Edit(long? Inspection_ID)
        {
            return View(InspectionRepository.Single((long)Inspection_ID));
        }

        [HttpPost]
        public ActionResult Edit(InspectionModel inspectionModel)
        {
            inspectionModel.CreatedBy = (long)User.User_ID;
            string UserRole = User.Role;           
            inspectionModel.weatherInspectionModel.StormDetailsModelList = BindStormDetailsModelNew();
            inspectionModel.weatherInspectionModel.StormDetailsModelEditList = BindStormDetailsModelEdit();
            inspectionModel.weatherInspectionModel.UploadDataModelList = BindUploadDataModelNew();
            inspectionModel.weatherInspectionModel.UploadDataModelEditList = BindUploadDataModelEdit();
            foreach (var item in inspectionModel.weatherInspectionModel.UploadDataModelEditList)
            {
                if (item.PostedFile!=null)
                {
                    string path = Server.MapPath("/Inspection/UploadedImage/" + item.ImageName);
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not
                    {
                        file.Delete();
                    }
                }
            }
            ViewBag.IsInspectionEdited = InspectionRepository.Edit(inspectionModel);
            if (inspectionModel.generalInspectionModel.IsComplete)
            {
                //string pdfPath = Server.MapPath("PDFFiles") + "\\" + DownloadAttachedPDF(inspectionModel.Inspection_ID);
                string pdfPath = string.Empty;
                string companyName = inspectionModel.generalInspectionModel.CustomerName;
                string ProjectName = inspectionModel.generalInspectionModel.location.Name;
                string trackingNO = inspectionModel.generalInspectionModel.Tracking_No;
                DateTime date = inspectionModel.generalInspectionModel.Date;
                string location = inspectionModel.generalInspectionModel.locationName;
                string InspectorName = inspectionModel.generalInspectionModel.inspector.Name;
                string sCompanyLogoUrl = AddCompanyLogoOnMailSend(inspectionModel.Inspection_ID);
                MailSetting.SendMail(inspectionModel.generalInspectionModel.InspectorEmail, companyName, ProjectName, InspectorName, trackingNO, date, location, pdfPath, inspectionModel.Inspection_ID, sCompanyLogoUrl);
                MailSetting.SendMail(ProjectName, inspectionModel.Inspection_ID, date, companyName, sCompanyLogoUrl);
                // System.IO.File.Delete(pdfPath);
                return RedirectToAction("CompleteIndex", "Inspector");
            }
            inspectionModel.weatherInspectionModel.UploadDataModelList = inspectionModel.weatherInspectionModel.UploadDataModelEditList;
            inspectionModel.weatherInspectionModel.StormDetailsModelList = inspectionModel.weatherInspectionModel.StormDetailsModelEditList;
            return View(inspectionModel);
        }

        [HttpPost]
        public JsonResult GetLocationDetails(long Location_ID)
        {
            return Json(InspectionRepository.GetLocationDetails(Location_ID));
        }

        [HttpPost]
        public JsonResult GetInpectorDetails(long Inspector_ID)
        {
            return Json(InspectionRepository.GetInspectorDetails(Inspector_ID));
        }

        [HttpGet][AllowAnonymous]
        public ActionResult Details(long? Inspection_ID, long Location_ID =0)
        {            
            bool y = InspectionRepository.setFirstReviewerLoginInfo((long)User.User_ID,(long) Inspection_ID, Location_ID);  
            return View(InspectionRepository.Single((long)Inspection_ID));
        }

        [HttpPost]
        public JsonResult GetClientDetails(long Client_ID)
        {
            return Json(ClientRepository.GetClientDetails(Client_ID,(long)User.User_ID));
        }

        [HttpGet]
        public JsonResult GetInspectorsClientDetails()
        {
            return Json(InspectionRepository.GetInspectorsClientDetails((long)User.User_ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult deleteUploadData(long UploadDataID, string ImageName)
        {
            if (ImageName != string.Empty)
            {
                string path = Server.MapPath("/Inspection/UploadedImage/" + ImageName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not
                {
                    file.Delete();
                }
            }
            return Json(InspectionRepository.deleteUploadData(UploadDataID));
        }
        [HttpPost]
        public JsonResult deleteStormDetails(long Storm_ID)
        {
            return Json(InspectionRepository.deleteStormDetails(Storm_ID));
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

        //[HttpGet,AllowAnonymous]
        //public void DownloadPDF(long Inspection_ID)
        //{
        //    string[] ViewArray = ViewToString((long)Inspection_ID).Split('`');
        //    StringBuilder sbHtmlTemplate = new StringBuilder();
        //    sbHtmlTemplate.AppendLine("<div class=\"panel-body\">");
        //    sbHtmlTemplate.AppendLine(" <div class=\"table-responsive\">");
        //    sbHtmlTemplate.AppendLine("<table id=\"tblAddNewInfo1\" class=\"table table-bordered table-striped\" style=\"background-color: White\">");
        //    sbHtmlTemplate.AppendLine(" <thead><tr> <th>  </th>");
        //    sbHtmlTemplate.AppendLine("<th class=\"col-md-2 active\"> S&E/P2 Controls </th>");
        //    sbHtmlTemplate.AppendLine(" <th class=\"col-md-2 active\" style=\"font-family:ArialMT;font-size:12px;\">Maintenance Needed </th>");
        //    sbHtmlTemplate.AppendLine(" <th class=\"col-md-2 active\" style=\"font-family:ArialMT;font-size:12px;\">Corrective Action?</th>");
        //    sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\">Location </th>");
        //    sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Quantity  </th>");
        //    sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Unit of Measure</th>");
        //    sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Upload Picture</th> </tr> </thead><tbody> ");
        //    bool IsNeedtoAppendTemplate = false;
        //    for (int index = 0; index < ViewArray.Length - 3; index++)
        //    {
        //        ViewArray[index] = ViewArray[index].Trim();

        //        {
        //            string s = ViewArray[index].Substring(ViewArray[index].Length - 5, 5);
        //            string s1 = ViewArray[index].Substring(ViewArray[index].Length - 6, 6);
        //            if (s == "</tr>" || s1 == "</div>")
        //            {
        //                if (index != 0)
        //                {
        //                    if (index != 1)
        //                    {
        //                        ViewArray[index] = sbHtmlTemplate.ToString() + ViewArray[index];
        //                        ViewArray[index] += "</tbody> </table></div></div>";
        //                        IsNeedtoAppendTemplate = true;
        //                        continue;
        //                    }
        //                }
        //            }

        //            if (IsNeedtoAppendTemplate)
        //            {

        //                IsNeedtoAppendTemplate = false;
        //            }
        //        }
        //    }
        //    WriteFileToResponse(CreatePdf(ViewArray));
        //}


        [HttpGet][AllowAnonymous]
        public void DownloadPDF(long Inspection_ID, long Location_ID=0)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["Reviewer_ID"]) && Convert.ToInt64(Request.QueryString["Reviewer_ID"])!=0)
                {
                    try
                    {
                        bool y = InspectionRepository.setFirstReviewerDownloadPDFLoginInfo(Convert.ToInt64(Request.QueryString["Reviewer_ID"]),(long)Inspection_ID, Location_ID);
                    }
                    catch
                    {
                    }
                }           

            // Get the server IP and port
            String serverIP = "137.116.153.126"; //"111.221.99.23";
            uint serverPort = uint.Parse("45001");

            // Create a HTML to PDF converter object with default settings
            Winnovative.HtmlToPdfClient.HtmlToPdfConverter htmlToPdfConverter = new Winnovative.HtmlToPdfClient.HtmlToPdfConverter(serverIP, serverPort);
            
            // Set optional service password
            //if (textBoxServicePassword.Text.Length > 0)
            htmlToPdfConverter.ServicePassword = "R3C@librate";

            // Set license key received after purchase to use the converter in licensed mode
            // Leave it not set to use the converter in demo mode
            htmlToPdfConverter.LicenseKey = "BIqai5qLmZmdnYuamYWbi5iahZqZhZKSkpKLmw==";

            // Set HTML Viewer width in pixels which is the equivalent in converter of the browser window width
            htmlToPdfConverter.HtmlViewerWidth = int.Parse("1024");

            // Set HTML viewer height in pixels to convert the top part of a HTML page 
            // Leave it not set to convert the entire HTML
            //if (htmlViewerHeightTextBox.Text.Length > 0)
            //    htmlToPdfConverter.HtmlViewerHeight = int.Parse(htmlViewerHeightTextBox.Text);

            // Set PDF page size which can be a predefined size like A4 or a custom size in points 
            // Leave it not set to have a default A4 PDF page
            htmlToPdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;

            // set header and footer
            htmlToPdfConverter.PdfDocumentOptions.ShowHeader = true;
            htmlToPdfConverter.PdfDocumentOptions.ShowFooter = true;
            htmlToPdfConverter.PdfHeaderOptions.HeaderHeight = 95;
            //Add header and footer text

            AddHeaderElements(htmlToPdfConverter, Inspection_ID);
            AddFooterElements(htmlToPdfConverter);

            // Set PDF page orientation to Portrait or Landscape
            // Leave it not set to have a default Portrait orientation for PDF page
            htmlToPdfConverter.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;

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
           // outPdfBuffer = htmlToPdfConverter.ConvertUrl(ConfigurationManager.AppSettings["SiteURL"] + "Inspection/Details?Inspection_ID=" + Inspection_ID);
            //http://e2rc.azurewebsites.net/Inspection/Details?Inspection_ID=1508");
            outPdfBuffer = htmlToPdfConverter.ConvertHtml(htmlTemplate(Inspection_ID), ConfigurationManager.AppSettings["SiteURL"]);

            
            //}
            //else
            //{
            //    string htmlString = htmlStringTextBox.Text;
            //    string baseUrl = baseUrlTextBox.Text;

            //    // Convert a HTML string with a base URL to a PDF document in a memory buffer
            //    outPdfBuffer = htmlToPdfConverter.ConvertHtml(htmlString, baseUrl);
            //}

            // Send the PDF as response to browser

            // Set response content type
            Response.AddHeader("Content-Type", "application/pdf");

            // Instruct the browser to open the PDF file as an attachment or inline
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename=e2rc-{1}.pdf; size={2}",
                 "attachment", DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff"), outPdfBuffer.Length.ToString()));

            // Write the PDF document buffer to HTTP response
            Response.BinaryWrite(outPdfBuffer);

            // End the HTTP response and stop the current page processing
            Response.End();
            //string[] ViewArray = ViewToString((long)Inspection_ID).Split('`');
            //StringBuilder sbHtmlTemplate = new StringBuilder();
            //sbHtmlTemplate.AppendLine("<div class=\"panel-body\">");
            //sbHtmlTemplate.AppendLine(" <div class=\"table-responsive\">");
            //sbHtmlTemplate.AppendLine("<table id=\"tblAddNewInfo1\" class=\"table table-bordered table-striped\" style=\"background-color: White\">");
            //sbHtmlTemplate.AppendLine(" <thead><tr> <th>  </th>");
            //sbHtmlTemplate.AppendLine(" <th class=\"col-md-1 active\">Station </th>");
            //sbHtmlTemplate.AppendLine("<th class=\"col-md-2 active\"> S&E/P2 Controls </th>");
            //sbHtmlTemplate.AppendLine(" <th class=\"col-md-2 active\">Maintenance Needed </th>");
            //sbHtmlTemplate.AppendLine(" <th class=\"col-md-2 active\">Corrective Action?</th>");
            //sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\">Location </th>");
            //sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Quantity  </th>");
            //sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Unit of Measure</th>");
            //sbHtmlTemplate.AppendLine(" <th> L/M/R </th>");
            //sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Upload Picture</th></tr> </thead><tbody> ");
            //bool IsNeedtoAppendTemplate = false;
            //for (int index = 0; index < ViewArray.Length - 3; index++)
            //{
            //    ViewArray[index] = ViewArray[index].Trim();

            //    {
            //        string s = ViewArray[index].Substring(ViewArray[index].Length - 5, 5);
            //        string s1 = ViewArray[index].Substring(ViewArray[index].Length - 6, 6);
            //        if (s == "</tr>" || s1 == "</div>")
            //        {
            //            if (index != 0)
            //            {
            //                if (index != 1)
            //                {
            //                    ViewArray[index] = sbHtmlTemplate.ToString() + ViewArray[index];
            //                    ViewArray[index] += "</tbody> </table></div></div>";
            //                    IsNeedtoAppendTemplate = true;
            //                    continue;
            //                }
            //            }
            //        }

            //        if (IsNeedtoAppendTemplate)
            //        {

            //            IsNeedtoAppendTemplate = false;
            //        }
            //    }
            //}
            //WriteFileToResponse(CreatePdf(ViewArray));
        }

        private void AddHeaderElements(HtmlToPdfConverter htmlToPdfConverter, long Inspection_ID)
        {
            string headerHtmlUrl = "";
            dynamic clientDetails = InspectionRepository.getClientDetailsByInspectionId(Inspection_ID);

            if (clientDetails.GetType().GetProperty("UploadLogoPath").GetValue(clientDetails, null) != "") 
            {
                headerHtmlUrl = ConfigurationManager.AppSettings["CompanyLogo"] + clientDetails.GetType().GetProperty("UploadLogoPath").GetValue(clientDetails, null);
                //headerHtmlUrl = Server.MapPath("~/" + clientDetails.GetType().GetProperty("UploadLogoPath").GetValue(clientDetails, null));
            }
            else 
            {
                headerHtmlUrl = ConfigurationManager.AppSettings["ImgLogo"];
            }

            HtmlToImageConverter imageConvertor = new HtmlToImageConverter();
            // htmlToPdfConverter.AddHeaderTemplate(87);
            // Create a HTML element to be added in header
           // HtmlToPdfElement headerHtmlToPdf = new HtmlToPdfElement(260f, 10f, 2550, 500, headerHtmlUrl);
            var webClient = new System.Net.WebClient();
            byte[] imageBytes = webClient.DownloadData(headerHtmlUrl);
            System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(imageBytes));

            int iImageHeight = image.Height;
            int iImageWidth = image.Width;
           // float ImageHeight = iImageHeight <= 250 ? 250 : 95;
            
            PdfPageSize PdfSize = new PdfPageSize();

            float fPdfwidth = PdfSize.Width;
            float fImageWidth = (float)(iImageWidth * 0.75);
            float fImageHeight = (float)(iImageHeight * 0.75);

            float yLocation = (95 - fImageHeight);
            float xLocation = ((Math.Abs(fPdfwidth - fImageWidth))*2)/3;

            xLocation = xLocation > 240 ? 250 : xLocation;
            //yLocation = yLocation > 30 || yLocation < 10 ? 15 : yLocation;

            HtmlToPdfElement headerHtmlToPdf = new HtmlToPdfElement(xLocation, yLocation, 595, 95, headerHtmlUrl);
            //HtmlToPdfElement headerHtmlToPdf = new HtmlToPdfElement(xLocation, yLocation, 2550, 250, headerHtmlUrl);
            // Set the HTML element to fit the container height
            //headerHtmlToPdf.FitHeight = bAutoFitHeight;

            // Add HTML element to header
            headerHtmlToPdf.AvoidImageBreak = true;
            htmlToPdfConverter.PdfHeaderOptions.AddElement(headerHtmlToPdf);
            htmlToPdfConverter.PdfHeaderOptions.ShowInFirstPage = true;
        }

        private void AddFooterElements(HtmlToPdfConverter htmlToPdfConverter)
        {
           PdfFont font = new  PdfFont("Arial",11,true);
            //create a template to be added in the header and footer           
            // document.Footer = document.AddTemplate(document.Pages[0].ClientRectangle.Width + 95, 108);           
            string strPageNum = "&p; / &P;";

            TextElement footerPageNumber = new TextElement(300f, 10f, 50, strPageNum,font);
            htmlToPdfConverter.PdfFooterOptions.AddElement(footerPageNumber);
        }

        private string htmlTemplate(long Inspection_ID)
        {
            string[] ViewArray = ViewToString((long)Inspection_ID).Split('`');
            //StringBuilder sbHtmlTemplate = new StringBuilder();
            //sbHtmlTemplate.AppendLine("<div class=\"panel-body\">");
            //sbHtmlTemplate.AppendLine(" <div class=\"table-responsive\">");
            //sbHtmlTemplate.AppendLine("<table id=\"tblAddNewInfo1\" class=\"table table-bordered table-striped\" style=\"background-color: White\">");
            //sbHtmlTemplate.AppendLine(" <thead><tr> <th>  </th>");
            //sbHtmlTemplate.AppendLine("<th class=\"col-md-2 active\"> S&E/P2 Controls </th>");
            //sbHtmlTemplate.AppendLine(" <th class=\"col-md-2 active\" style=\"font-family:ArialMT;font-size:12px;\">Maintenance Needed </th>");
            //sbHtmlTemplate.AppendLine(" <th class=\"col-md-2 active\" style=\"font-family:ArialMT;font-size:12px;\">Corrective Action?</th>");
            //sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\">Location </th>");
            //sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Quantity  </th>");
            //sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Unit of Measure</th>");
            //sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Upload Picture</th> </tr> </thead><tbody> ");
            //bool IsNeedtoAppendTemplate = false;
            //for (int index = 0; index < ViewArray.Length ; index++)
            //{
            //    ViewArray[index] = ViewArray[index].Trim();

            //    {
            //        string s = ViewArray[index].Substring(ViewArray[index].Length - 5, 5);
            //        string s1 = ViewArray[index].Substring(ViewArray[index].Length - 6, 6);
            //        if (s == "</tr>" || s1 == "</div>")
            //        {
            //            if (index != 0)
            //            {
            //                if (index != 1)
            //                {
            //                    ViewArray[index] = sbHtmlTemplate.ToString() + ViewArray[index];
            //                    ViewArray[index] += "</tbody> </table></div></div>";
            //                    IsNeedtoAppendTemplate = true;
            //                    continue;
            //                }
            //            }
            //        }

            //        if (IsNeedtoAppendTemplate)
            //        {

            //            IsNeedtoAppendTemplate = false;
            //        }
            //    }
            //}
            return  string.Join("",ViewArray);
        }       

        public string DownloadAttachedPDF(long Inspection_ID)
        {
            string[] ViewArray = ViewToString((long)Inspection_ID).Split('`');
            StringBuilder sbHtmlTemplate = new StringBuilder();
            sbHtmlTemplate.AppendLine("<div class=\"panel-body\">");
            sbHtmlTemplate.AppendLine("<div class=\"table-responsive\">");
            sbHtmlTemplate.AppendLine("<table id=\"tblAddNewInfo1\" class=\"table table-bordered table-striped\" style=\"background-color: White\">");
            sbHtmlTemplate.AppendLine(" <thead><tr> <th>  </th>");
            sbHtmlTemplate.AppendLine("<th class=\"col-md-2 active\"> S&E/P2 Controls </th>");
            sbHtmlTemplate.AppendLine(" <th class=\"col-md-2 active\">  Action Needed? </th>");
            sbHtmlTemplate.AppendLine(" <th class=\"col-md-2 active\">   Action Type?</th>");
            sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\">Location </th>");
            sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Quantity  </th>");
            sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Unit of Measure</th>");
            sbHtmlTemplate.AppendLine("<th class=\"col-md-1 active\"> Upload Picture</th> </tr> </thead><tbody> ");

            bool IsNeedtoAppendTemplate = false;
            for (int index = 0; index < ViewArray.Length - 3; index++)
            {
                ViewArray[index] = ViewArray[index].Trim();

                {
                    string s = ViewArray[index].Substring(ViewArray[index].Length - 5, 5);
                    string s1 = ViewArray[index].Substring(ViewArray[index].Length - 6, 6);
                    if (s == "</tr>" || s1 == "</div>")
                    {
                        if (index != 0)
                        {
                            if (index != 1)
                            {
                                ViewArray[index] = sbHtmlTemplate.ToString() + ViewArray[index];
                                ViewArray[index] += "</tbody> </table></div></div>";
                                IsNeedtoAppendTemplate = true;
                                continue;
                            }
                        }
                    }

                    if (IsNeedtoAppendTemplate)
                    {

                        IsNeedtoAppendTemplate = false;
                    }
                }
            }

            string fileName = CreatePdf(ViewArray);
            return fileName;
        }

        private List<StormDetailsModel> BindStormDetailsModel()
        {
            bool IsNext = true;
            int count = 1;
            List<StormDetailsModel> StormDetailsList = new List<StormDetailsModel>();

            while (IsNext)
            {               
                if (!string.IsNullOrEmpty(Request["StormEvent" + count + "_StormDate"]) || !string.IsNullOrEmpty(Request["StormEvent" + count + "_StormDuration"]) ||
                    (!string.IsNullOrEmpty(Request["StormEvent" + count + "_Amount"])))
                    
                {
                    try
                    {
                        StormDetailsList.Add(new StormDetailsModel
                        {
                            StormDate = Convert.ToDateTime(Request["StormEvent" + count + "_StormDate"])== null ?DateTime.Now: (Convert.ToDateTime(Request["StormEvent" + count + "_StormDate"])),
                            StormDuration = Convert.ToString(Request["StormEvent" + count + "_StormDuration"]) == string.Empty ? string.Empty : (Convert.ToString(Request["StormEvent" + count + "_StormDuration"])),
                            Amount = Convert.ToDecimal(Request["StormEvent" + count + "_Amount"]) == 0 ? 0 : (Convert.ToDecimal(Request["StormEvent" + count + "_Amount"])),
                            stID = Request["hfstID" + count] != null ? Convert.ToInt16(Request["hfstID" + count]) : 0,
                            ParentstID = Request["hfstPID" + count] != null ? Convert.ToInt16(Request["hfstPID" + count]) : 0,
                        });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    IsNext = false;
                }
                count++;
            }
            return StormDetailsList;
        }

        private List<StormDetailsModel> BindStormDetailsModelNew()
        {
            bool IsNext = true;
            int count = 1;
            List<StormDetailsModel> StormDetailsList = new List<StormDetailsModel>();

            while (IsNext)
            {
                if (!string.IsNullOrEmpty(Request["StormEvent" + count + "_StormDate"]) || !string.IsNullOrEmpty(Request["StormEvent" + count + "_StormDuration"]) ||
                     (!string.IsNullOrEmpty(Request["StormEvent" + count + "_Amount"])))
               
                {
                    try
                    {
                        StormDetailsList.Add(new StormDetailsModel
                        {
                            StormDate = Convert.ToDateTime(Request["StormEvent" + count + "_StormDate"]) == null ? DateTime.Now : (Convert.ToDateTime(Request["StormEvent" + count + "_StormDate"])),
                            StormDuration = Convert.ToString(Request["StormEvent" + count + "_StormDuration"]) == string.Empty ? string.Empty : (Convert.ToString(Request["StormEvent" + count + "_StormDuration"])),
                            Amount = Convert.ToDecimal(Request["StormEvent" + count + "_Amount"]) == 0 ? 0 : (Convert.ToDecimal(Request["StormEvent" + count + "_Amount"])),
                            ParentStorm_ID = Convert.ToInt64(Request["StormEvent" + count + "_ParentStorm_ID"]),
                            stID = Request["hfstID" + count] != null ? Convert.ToInt16(Request["hfstID" + count]) : 0,
                            ParentstID = Request["hfstPID" + count] != null ? Convert.ToInt16(Request["hfstPID" + count]) : 0
                            
                        });
                        count++;
                    }
                    catch
                    {

                    }
                }

                else
                {
                    IsNext = false;
                }
            }

            return StormDetailsList;
        }

        private List<StormDetailsModel> BindStormDetailsModelEdit()
        {
            bool IsNext = true;
            int count = 1;
            List<StormDetailsModel> StormDetailsList = new List<StormDetailsModel>();
            string[] hfDeletedstRows = Request["hfDeletedstRows"].Split(',');
            bool IsCountInhfDeletedRows = false;

            while (IsNext)
            {
                foreach (string item in hfDeletedstRows)
                {
                    if (item == count.ToString())
                    {
                        IsCountInhfDeletedRows = true;
                        count++;
                        break;
                    }

                }
                if (IsCountInhfDeletedRows)
                {
                    IsCountInhfDeletedRows = false;
                    continue;
                }

                if (!string.IsNullOrEmpty(Request["StormEventE" + count + "_StormDate"]) || !string.IsNullOrEmpty(Request["StormEventE" + count + "_StormDuration"]) ||
                     (!string.IsNullOrEmpty(Request["StormEvent" + count + "_Amount"])))
                {
                    try
                    {

                        StormDetailsList.Add(new StormDetailsModel
                        {
                            StormDate = Convert.ToDateTime(Request["StormEventE" + count + "_StormDate"]) == null ? DateTime.Now : (Convert.ToDateTime(Request["StormEventE" + count + "_StormDate"])),
                            StormDuration = Convert.ToString(Request["StormEventE" + count + "_StormDuration"]) == string.Empty ? string.Empty : (Convert.ToString(Request["StormEventE" + count + "_StormDuration"])),
                            Amount = Convert.ToDecimal(Request["StormEventE" + count + "_Amount"]) == 0 ? 0 : (Convert.ToDecimal(Request["StormEventE" + count + "_Amount"])),
                            Storm_ID = Convert.ToInt64(Request["StormEventE" + count + "_Storm_ID"]),
                            ParentStorm_ID = Convert.ToInt64(Request["StormEventE" + count + "_ParentStorm_ID"]),
                            
                        });
                        count++;
                    }

                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
                else
                {
                    IsNext = false;
                }

            }
            return StormDetailsList;
        }

        private List<UploadDataModel> BindUploadDataModel()
        {
            bool IsNext = true;
            int count = 1;
            List<UploadDataModel> UploadDataList = new List<UploadDataModel>();

            while (IsNext)
            {

                if (!string.IsNullOrEmpty(Request["UploadData" + count + "_Location"]) || !string.IsNullOrEmpty(Request["UploadData" + count + "_PPPLength"]) ||
                     !string.IsNullOrEmpty(Request["UploadData" + count + "_ItemC1"]) || !string.IsNullOrEmpty(Request["UploadData" + count + "_ItemC2"]) ||
                     !string.IsNullOrEmpty(Request["UploadData" + count + "_ItemC3"]) || !string.IsNullOrEmpty(Request["UploadData" + count + "_UOM"]) ||
                     (Request.Files["UploadData" + count + "_fileToUpload"]) != null)
                {
                    try
                    {
                        UploadDataList.Add(new UploadDataModel
                        {

                            itemC1 = new ItemC1Model { Item_ID = (Convert.ToInt32(Request["UploadData" + count + "_ItemC1"])) == 0 ? 0 : (Convert.ToInt32(Request["UploadData" + count + "_ItemC1"])) },
                            itemC2 = new ItemC2Model { Item_ID = Convert.ToInt32(Request["UploadData" + count + "_ItemC2"]) == 0 ? 0 : Convert.ToInt32(Request["UploadData" + count + "_ItemC2"]) },
                            itemC3 = new ItemC3Model { Item_ID = Convert.ToInt32(Request["UploadData" + count + "_ItemC3"]) == 0 ? 0 : Convert.ToInt32(Request["UploadData" + count + "_ItemC3"]) },
                            Location = (Request["UploadData" + count + "_Location"] == null ? string.Empty : Request["UploadData" + count + "_Location"]),
                            PPPLength = (Convert.ToString(Request["UploadData" + count + "_PPPLength"])) == string.Empty ? 0 : (Convert.ToInt32(Request["UploadData" + count + "_PPPLength"])),
                            UOM = new UOMModel { UOM_ID = Convert.ToInt32(Request["UploadData" + count + "_UOM"]) == 0 ? 0 : Convert.ToInt32(Request["UploadData" + count + "_UOM"]) },
                            ParentIndex = (Request["UploadData" + count + "_ParentClickIndex"]) == null ? 0 : (Convert.ToInt32(Request["UploadData" + count + "_ParentClickIndex"])),
                            PostedFile = Request.Files["UploadData" + count + "_fileToUpload"],
                            ID = Request["hfID" + count] != null ? Convert.ToInt16(Request["hfID" + count]) : 0,
                            ParentID = Request["hfPID" + count] != null ? Convert.ToInt16(Request["hfPID" + count]) : 0
                        });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    IsNext = false;
                }
                count++;
            }
            return UploadDataList;
        }

        private List<UploadDataModel> BindUploadDataModelNew()
        {
            bool IsNext = true;
            int count = 1;
            List<UploadDataModel> UploadDataList = new List<UploadDataModel>();

            while (IsNext)
            {

                if (!string.IsNullOrEmpty(Request["UploadData" + count + "_Location"]) ||
                    !string.IsNullOrEmpty(Request["UploadData" + count + "_PPPLength"]) ||
                     !string.IsNullOrEmpty(Request["UploadData" + count + "_ItemC1"]) || !string.IsNullOrEmpty(Request["UploadData" + count + "_ItemC2"]) ||
                     !string.IsNullOrEmpty(Request["UploadData" + count + "_ItemC3"]) || !string.IsNullOrEmpty(Request["UploadData" + count + "_UOM"]) ||
                     (Request.Files["UploadData" + count + "_fileToUpload"]) != null)
                {
                    try
                    {
                        UploadDataList.Add(new UploadDataModel
                        {
                            itemC1 = new ItemC1Model { Item_ID = (Convert.ToInt32(Request["UploadData" + count + "_ItemC1"])) == 0 ? 0 : (Convert.ToInt32(Request["UploadData" + count + "_ItemC1"])) },
                            itemC2 = new ItemC2Model { Item_ID = Convert.ToInt32(Request["UploadData" + count + "_ItemC2"]) == 0 ? 0 : Convert.ToInt32(Request["UploadData" + count + "_ItemC2"]) },
                            itemC3 = new ItemC3Model { Item_ID = Convert.ToInt32(Request["UploadData" + count + "_ItemC3"]) == 0 ? 0 : Convert.ToInt32(Request["UploadData" + count + "_ItemC3"]) },
                            Location = (Request["UploadData" + count + "_Location"] == null ? string.Empty : Request["UploadData" + count + "_Location"]),
                            PPPLength = (Convert.ToString(Request["UploadData" + count + "_PPPLength"])) == string.Empty ? 0 : (Convert.ToInt32(Request["UploadData" + count + "_PPPLength"])),
                            UOM = new UOMModel { UOM_ID = Convert.ToInt32(Request["UploadData" + count + "_UOM"]) == 0 ? 0 : Convert.ToInt32(Request["UploadData" + count + "_UOM"]) },
                            ParentUploadData_ID = Convert.ToInt64(Request["UploadData" + count + "_ParentUploadData_ID"]),
                            PostedFile = Request.Files["UploadData" + count + "_fileToUpload"] == null ? null: (Request.Files["UploadData" + count + "_fileToUpload"]),
                            ID = Request["hfID" + count] != null ? Convert.ToInt16(Request["hfID" + count]) : 0,
                            ParentID = Request["hfPID" + count] != null ? Convert.ToInt16(Request["hfPID" + count]) : 0
                        });
                        count++;
                    }
                    catch
                    {

                    }
                }

                else
                {
                    IsNext = false;
                }
            }

            return UploadDataList;
        }

        private List<UploadDataModel> BindUploadDataModelEdit()
        {
            bool IsNext = true;
            int count = 1;
            List<UploadDataModel> UploadDataList = new List<UploadDataModel>();
            string[] hfDeletedRows = Request["hfDeletedRows"].Split(',');
            bool IsCountInhfDeletedRows = false;

            while (IsNext)
            {
                foreach (string item in hfDeletedRows)
                {
                    if (item == count.ToString())
                    {
                        IsCountInhfDeletedRows = true;
                        count++;
                        break;
                    }

                }
                if (IsCountInhfDeletedRows)
                {
                    IsCountInhfDeletedRows = false;
                    continue;
                }

                if (!string.IsNullOrEmpty(Request["UploadDataE" + count + "_Location"]) ||
                    !string.IsNullOrEmpty(Request["UploadDataE" + count + "_PPPLength"]) ||
                     !string.IsNullOrEmpty(Request["UploadDataE" + count + "_ItemC1"]) || !string.IsNullOrEmpty(Request["UploadDataE" + count + "_ItemC2"]) ||
                     !string.IsNullOrEmpty(Request["UploadDataE" + count + "_ItemC3"]) || !string.IsNullOrEmpty(Request["UploadDataE" + count + "_UOM"]))
                {
                    try
                    {

                        UploadDataList.Add(new UploadDataModel
                        {
                            // Convert.ToBoolean("true"),
                            itemC1 = new ItemC1Model { Item_ID = Convert.ToInt32(Request["UploadDataE" + count + "_ItemC1"]) == 0 ? 0 : Convert.ToInt32(Request["UploadDataE" + count + "_ItemC1"]) },
                            itemC2 = new ItemC2Model { Item_ID = Convert.ToInt32(Request["UploadDataE" + count + "_ItemC2"]) == 0 ? 0 : Convert.ToInt32(Request["UploadDataE" + count + "_ItemC2"]) },
                            itemC3 = new ItemC3Model { Item_ID = Convert.ToInt32(Request["UploadDataE" + count + "_ItemC3"]) == 0 ? 0 : Convert.ToInt32(Request["UploadDataE" + count + "_ItemC3"]) },
                            Location = Request["UploadDataE" + count + "_Location"] == null ? string.Empty : Request["UploadDataE" + count + "_Location"],
                            PPPLength = Convert.ToString(Request["UploadDataE" + count + "_PPPLength"]) == string.Empty ? 0 : Convert.ToInt32(Request["UploadDataE" + count + "_PPPLength"]),
                            UOM = new UOMModel { UOM_ID = Convert.ToInt32(Request["UploadDataE" + count + "_UOM"]) },
                            UploadData_ID = Convert.ToInt64(Request["UploadDataE" + count + "_UploadData_ID"]),
                            ParentUploadData_ID = Convert.ToInt64(Request["UploadDataE" + count + "_ParentUploadData_ID"]),
                            PostedFile = Request.Files["UploadDataE" + count + "_fileToUpload"] == null ? null : (Request.Files["UploadDataE" + count + "_fileToUpload"]),
                            UploadImagePath = (Convert.ToString(Request["UploadDataE" + count + "_UploadImagePath"])) == string.Empty ? string.Empty : (Convert.ToString(Request["UploadDataE" + count + "_UploadImagePath"])),
                            ImageName = (Convert.ToString(Request["UploadDataE" + count + "_ImageName"])) == string.Empty ? string.Empty : (Convert.ToString(Request["UploadDataE" + count + "_ImageName"]))

                            
                        });
                        count++;
                    }

                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
                else
                {
                    IsNext = false;
                }

            }
            return UploadDataList;
        }

        private string ViewToString(long? Inspection_ID)
        {
            return RenderRazorViewToString("~/Views/Inspection/CreatePDF.cshtml", InspectionRepository.Single((long)Inspection_ID));

            //code upload karun check kara ok
            //include in project option distat nahi 
            //tuchya padhatine addd kara ok
            
        }

        private string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        private string CreatePdf(string[] strContent)
        {
//            Winnovative.Document document = new Winnovative.Document();
//            Winnovative.PdfPage pdfPage = document.AddPage();
//            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();
//            htmlToPdfConverter.PdfDocumentOptions.ShowHeader = true;
//            //add a font to the document that can be used for the texts elements 
//            PdfFont font = document.Fonts.Add(new System.Drawing.Font(new System.Drawing.FontFamily("Arial"), 12, System.Drawing.GraphicsUnit.Point));
//            //System.Drawing.Font ttfFont = new System.Drawing.Font(new System.Drawing.FontFamily("Times New Roman"), 23, System.Drawing.GraphicsUnit.Point);
//            htmlToPdfConverter.PrerenderEnabled = true;
//            AddHtmlHeader(document);
//            //AddHtmlFooter(document, font);
//            for (int iCount = 0; iCount < strContent.Length; iCount++)
//            {
//                if (strContent[iCount].ToString() != "")
//                {
//                    string strTempContent;
//                    strTempContent = strContent[iCount];
//                    strTempContent = "<html><head><link href='../../Content/css/bootstrap.css' rel='stylesheet'/><link href='../../Content/css/bootstrap.min.css' rel='stylesheet' /><link href='../../Content/css/signin.css' rel='stylesheet' /><link href='../../Content/css/style.css' rel='stylesheet' /><script src='../../Scripts/bootstrap.min.js' type='text/javascript'></script></head><body width=100%;>" + strContent[iCount] + "</body></html>";
//                    strTempContent = @"<!DOCTYPE html >
//                                                <html lang='en'>
//                                                <head>                                              
//                                              <link href='../../Content/css/bootstrap.css' rel='stylesheet'/>  
//                                              <link href='../../Content/css/bootstrap.min.css' rel='stylesheet' />
//                                              <link href='../../Content/css/signin.css' rel='stylesheet' />
//                                              <link href='../../Content/css/downloadStyle.css' rel='stylesheet' />
//                                              <script src='../../Scripts/bootstrap.min.js' type='text/javascript'></script>
//                                                </head>
//                                                  <body style='background-image:none !important;'>" + strContent[iCount] + "</body></html>";
                    

//                    htmlToPdfConverter.LicenseKey = "fvDh8eDx4fHg4P/h8eLg/+Dj/+jo6Og=";
//                    document.CompressionLevel = PdfCompressionLevel.Normal;
//                    document.Margins = new Margins(0, 10, 0, 0);
//                    document.DocumentInformation.Author = "HTML to PDF Converter";
//                    document.ViewerPreferences.HideToolbar = false;
//                    // set if the images are compressed in PDF with JPEG to reduce the PDF document size
//                    document.JpegCompressionEnabled = true;
//                    PdfPageSize pg = new PdfPageSize((float)792, (float)610);

//                    Winnovative.PdfPage page = document.Pages.AddNewPage(pg, new Margins(0, 0, -0, 0), PdfPageOrientation.Portrait);
//                    document.OpenAction.Action = new PdfActionJavaScript("this.zoom=125;");
//                    float xLocation = float.Parse("0");
//                    float yLocation = float.Parse("0");
//                    float width = float.Parse("0");
//                    float height = float.Parse("0");
//                    // convert HTML to PDF
//                    HtmlToPdfElement htmlToPdfElement;
//                    string htmlStringToConvert = strTempContent;
//                    string baseURL = "http://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath + "Inspection" + "/" + "Details";
//                    htmlToPdfElement = new HtmlToPdfElement(xLocation, yLocation, width, height, htmlStringToConvert, baseURL);
//                    htmlToPdfElement.StretchToFit = true;
//                    htmlToPdfConverter.NavigationTimeout=500;
//                    page.AddElement(htmlToPdfElement);
//                    htmlToPdfElement.LiveUrlsEnabled = true;
//                }
//            }
//            document.Pages.Remove(0);
//            string strFileName;
//            try
//            {
//                strFileName = "e2rc-" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss.fff") + ".pdf";
//                string strpath = Server.MapPath("PDFFiles") + "\\" + strFileName;
//                document.Save(strpath);
//            }
//            finally
//            {
//                document.Close();
//            }
//            return strFileName;
            return string.Empty;
        }

        private void WriteFileToResponse(string fileName)
        {
            Response.Clear();
            // MailSetting.SendMail(inspectionModel, strpath);
            Response.AppendHeader("content-disposition", "attachment; filename=\"" + fileName + "\""); // name);
            Response.ContentType = "Application/pdf";
            Response.WriteFile(Server.MapPath("PDFFiles") + "\\" + fileName);
            //Response.StatusCode = 301;
            Response.Flush();
            Response.Close();
            // System.Threading.Thread.Sleep(5000);
            System.IO.File.Delete(Server.MapPath("PDFFiles") + "\\" + fileName);
            Response.End();
        }

        //private void AddHtmlHeader(Winnovative.Document document)
        //{
        //    string headerHtmlUrl = ConfigurationManager.AppSettings["ImgLogo"];
        //    document.AddHeaderTemplate(87);
        //    // Create a HTML element to be added in header
        //    HtmlToPdfElement headerHtmlToPdf = new HtmlToPdfElement(260f, 10f, 2550, 500, headerHtmlUrl);
        //    // Set the HTML element to fit the container height
        //    headerHtmlToPdf.FitHeight = true;
        //    // Add HTML element to header
        //    headerHtmlToPdf.AvoidImageBreak = true;
        //    document.Header.DrawOnFirstPage = true;
        //    document.Header.AddElement(headerHtmlToPdf);

        //}

        //private void AddHtmlFooter(Winnovative.Document document, PdfFont footerPageNumberFont)
        //{
        //    PdfFont fontDate = document.Fonts.Add(new System.Drawing.Font(new System.Drawing.FontFamily("Georgia"), 8,
        //                System.Drawing.GraphicsUnit.Point));
        //    //create a template to be added in the header and footer
        //    document.AddFooterTemplate(50);
        //    // document.Footer = document.AddTemplate(document.Pages[0].ClientRectangle.Width + 95, 108);           
        //    string strPageNum = "&p; / &P;";

        //    TextElement footerPageNumber = new TextElement(300f, 10f, 50, strPageNum, fontDate);
        //    document.Footer.AddElement(footerPageNumber);


        //}

         [AllowAnonymous]
        public void MailAutoresponder()
        {
            Int64 Inspection_ID = Convert.ToInt64(RouteData.Values["Inspection_ID"]);
            bool y = InspectionRepository.Autoresponder(Inspection_ID);
        }

        [AllowAnonymous]
        public void WorkOrderCompletedAutoResponder()
        {
            Int64 Inspection_ID = Convert.ToInt64(RouteData.Values["Inspection_ID"]);
            bool y = InspectionRepository.WorkOrderCompletedAutoResponder(Inspection_ID);
        } 
 
        [AllowAnonymous]
        public ViewResult ActionMaintenanceWorkToE2RC()
        {    
            try
            {
                ViewBag.IsMailSentSuccessfully = MailSetting.ActionMaintenanceWorktoE2RC(
                    Convert.ToInt64(RouteData.Values["Inspection_ID"]), Convert.ToString(RouteData.Values["InspEmail"]));                   
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

       

         [AllowAnonymous]
         public void setFirstReviewerInfo()
         {            
             bool y = InspectionRepository.setFirstReviewerInfo(Convert.ToInt64(RouteData.Values["Inspection_ID"]), Convert.ToInt64(RouteData.Values["Reviewer_ID"]), DateTime.Now);             
         } 


    }
}
