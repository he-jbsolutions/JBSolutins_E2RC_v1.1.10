using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e2rc.Models;
using e2rc.Models.Repository;
using System.IO;
using Winnovative.HtmlToPdfClient;
using System.Configuration;

namespace e2rc.Controllers
{
    [Authorize]
    public class CorrectiveActionController : BaseController
    {
        //
        // GET: /CorrectiveAction/

        public ActionResult Index()
        {
            return View();
        }
        /*
        [HttpGet]
        public ActionResult Create(long? Inspection_ID, long? UploadData_ID, bool isCorrective)
        {
            CorrectiveActionModel CorrectiveActionModel;
            CorrectiveActionModel = CorrectiveActionRepository.Single(Inspection_ID);
            CorrectiveActionModel.isCorrective = isCorrective;
            CorrectiveActionModel.UploadData_ID = UploadData_ID;
            ViewBag.ReportTitle = isCorrective ? "CORRECTIVE ACTION REPORT" : "MAINTENANCE ACTION REPORT";
            return View(CorrectiveActionModel);
        }*/

         [HttpGet]
        public ActionResult Create(long? Inspection_ID, long? UploadData_ID, bool isCorrective, long Corrective_ID)
        {
            CorrectiveActionModel CorrectiveActionModel;
            CorrectiveActionModel = CorrectiveActionRepository.Single(Inspection_ID);
            CorrectiveActionModel.isCorrective = isCorrective;
            CorrectiveActionModel.UploadData_ID = UploadData_ID;
            CorrectiveActionModel.CorrectiveActionID = Corrective_ID;
            ViewBag.ReportTitle = isCorrective ? "CORRECTIVE ACTION REPORT" : "MAINTENANCE ACTION REPORT";
            return View(CorrectiveActionModel);
        }

        [HttpPost]
        public ActionResult Create(CorrectiveActionModel CorrectiveActionModel)
        {
            CorrectiveActionModel.UploadProblemDataModelList = BindProblemDataModelList();
            CorrectiveActionModel.UploadStromDataModelList = BindStromDataModelList();

            //bool result = CorrectiveActionRepository.Create(CorrectiveActionModel);
            bool result = CorrectiveActionRepository.Edit(CorrectiveActionModel);

            return RedirectToAction("ProjectWiseInpectionSubmission", "Submission", new { Location_ID = CorrectiveActionModel.Location_ID ,Client_ID = CorrectiveActionModel.Client_ID ,display = "disableBack" });
            //return View();
        }

        public ActionResult Edit(long? UploadData_ID)
        {
            CorrectiveActionModel CorrectiveActionModel;
            CorrectiveActionModel = CorrectiveActionRepository.SingleDetails(UploadData_ID);
            CorrectiveActionModel.UploadData_ID = UploadData_ID;
            return View(CorrectiveActionModel);
        }

        [HttpPost]
        public ActionResult Edit(CorrectiveActionModel CorrectiveActionModel)
        {
            CorrectiveActionModel.UploadProblemDataModelList = BindProblemDataModelList();
            CorrectiveActionModel.UploadStromDataModelList = BindStromDataModelList();

            bool result = CorrectiveActionRepository.Edit(CorrectiveActionModel);
            //return View(CorrectiveActionModel);

            return RedirectToAction("Edit", "CorrectiveAction", new { UploadData_ID = CorrectiveActionModel.UploadData_ID, isCorrective = CorrectiveActionModel.isCorrective });
        }

        public ActionResult Details(long? UploadData_ID, bool IsAllow)
        {
            CorrectiveActionModel CorrectiveActionModel;
            CorrectiveActionModel = CorrectiveActionRepository.SingleDetails(UploadData_ID);
            CorrectiveActionModel.UploadData_ID = UploadData_ID;
            ViewBag.IsAllow = IsAllow;
            return View(CorrectiveActionModel);
        }

        //[HttpGet]
        //public JsonResult GetInpectionReportDetails(long Inspection_ID)
        //{
        //    return Json(CorrectiveActionRepository.GetInpectionReportDetails(Inspection_ID), JsonRequestBehavior.AllowGet);
        //}

        #region Bind List

        /* Bind Detials Of Section B.1 For Problem Information*/
        private List<ProblemInfo> BindProblemDataModelList()
        {
            bool IsNext = true;
            int count = 1;
            List<ProblemInfo> ProblemDataList = new List<ProblemInfo>();

            while (IsNext)
            {
                if (!string.IsNullOrEmpty(Request["Problem" + count + "_ProblemCause"]) || !string.IsNullOrEmpty(Request["Problem" + count + "_ProblemDetermined"]) ||
                     (!string.IsNullOrEmpty(Request["Problem" + count + "_ProblemDate"])))
                {
                    try
                    {
                        ProblemDataList.Add(new ProblemInfo
                        {
                            ProblemID = string.IsNullOrEmpty(Request["Problem" + count + "_ProblemID"]) ? 0 : Convert.ToInt32((Request["Problem" + count + "_ProblemID"])),
                            ProblemCause = string.IsNullOrEmpty(Request["Problem" + count + "_ProblemCause"]) ? string.Empty : (Convert.ToString(Request["Problem" + count + "_ProblemCause"])),
                            ProblemDetermined = string.IsNullOrEmpty(Request["Problem" + count + "_ProblemDetermined"]) ? string.Empty : (Convert.ToString(Request["Problem" + count + "_ProblemDetermined"])),
                            ProblemDate = string.IsNullOrEmpty(Request["Problem" + count + "_ProblemDate"]) ? DateTime.Now : (Convert.ToDateTime(Request["Problem" + count + "_ProblemDate"])),
                        
                            //PrombleDate = Convert.ToDecimal(Request["StormEvent" + count + "_Amount"]) == 0 ? 0 : (Convert.ToDecimal(Request["StormEvent" + count + "_Amount"])),
                            //ParentStorm_ID = Convert.ToInt64(Request["StormEvent" + count + "_ParentStorm_ID"]),
                            //stID = Request["hfstID" + count] != null ? Convert.ToInt16(Request["hfstID" + count]) : 0,
                            //ParentstID = Request["hfstPID" + count] != null ? Convert.ToInt16(Request["hfstPID" + count]) : 0

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

            return ProblemDataList;
        }

        /* Bind Detials Of Section B.2 For StromWater Control Information*/
        private List<StromWaterControl> BindStromDataModelList()
        {
            bool IsNext = true;
            int count = 1;
            List<StromWaterControl> StromControlDataList = new List<StromWaterControl>();

            while (IsNext)
            {
                //if (!string.IsNullOrEmpty(Request["StromControl" + count + "_StromModifiedText"]) || !string.IsNullOrEmpty(Request["StromControl" + count + "_CompletedDate"]) ||
                //     (!string.IsNullOrEmpty(Request["StromControl" + count + "_SWPPUpdateDate"]) || (!string.IsNullOrEmpty(Request["StromControl" + count + "_Notes"])) || (!string.IsNullOrEmpty(Request["StromControl" + count + "_SWPPPRequireYesNo"]))))
                if (!string.IsNullOrEmpty(Request["StromControl" + count + "_StromModifiedText"]) || !string.IsNullOrEmpty(Request["StromControl" + count + "_CompletedDate"]) ||
                                     (!string.IsNullOrEmpty(Request["StromControl" + count + "_SWPPUpdateDate"]) || (!string.IsNullOrEmpty(Request["StromControl" + count + "_Notes"]))))

                {
                    try
                    {
                        StromControlDataList.Add(new StromWaterControl
                        {
                            StromID = string.IsNullOrEmpty(Request["StromControl" + count + "_StromID"]) ? 0 : Convert.ToInt32((Request["StromControl" + count + "_StromID"])),
                            StromModifiedText = string.IsNullOrEmpty(Request["StromControl" + count + "_StromModifiedText"]) ? string.Empty : (Convert.ToString(Request["StromControl" + count + "_StromModifiedText"])),
                            CompletedDate = string.IsNullOrEmpty(Request["StromControl" + count + "_CompletedDate"]) ? DateTime.Now : (Convert.ToDateTime(Request["StromControl" + count + "_CompletedDate"])),
                            SWPPUpdateDate = Convert.ToBoolean(Request["StromControl" + count + "_SWPPPRequireYesNo"]) == true ? string.IsNullOrEmpty(Request["StromControl" + count + "_SWPPUpdateDate"]) ? DateTime.Now : (Convert.ToDateTime(Request["StromControl" + count + "_SWPPUpdateDate"])) : DateTime.Now,
                            Notes = string.IsNullOrEmpty(Request["StromControl" + count + "_Notes"]) ? string.Empty : (Convert.ToString(Request["StromControl" + count + "_Notes"])),

                            //    PrombleDate = Convert.ToDecimal(Request["StormEvent" + count + "_Amount"]) == 0 ? 0 : (Convert.ToDecimal(Request["StormEvent" + count + "_Amount"])),
                            //ParentStorm_ID = Convert.ToInt64(Request["StormEvent" + count + "_ParentStorm_ID"]),
                            //stID = Request["hfstID" + count] != null ? Convert.ToInt16(Request["hfstID" + count]) : 0,
                            //ParentstID = Request["hfstPID" + count] != null ? Convert.ToInt16(Request["hfstPID" + count]) : 0
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
            return StromControlDataList;
        }

        #endregion

        #region Corrective/Maintainance Action Report PDF
        private string ViewToString(long? UploadData_ID)
        {
            return RenderRazorViewToString("~/Views/CorrectiveAction/CreatePDF.cshtml",  CorrectiveActionRepository.SingleDetails(UploadData_ID));

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

        [HttpGet]
        [AllowAnonymous]
        public void DownloadPDF(long UploadData_ID, long Inspection_ID)
        {

            //if (!string.IsNullOrEmpty(Request.QueryString["Reviewer_ID"]) && Convert.ToInt64(Request.QueryString["Reviewer_ID"]) != 0)
            //{
            //    try
            //    {
            //        bool y = InspectionRepository.setFirstReviewerDownloadPDFLoginInfo(Convert.ToInt64(Request.QueryString["Reviewer_ID"]), (long)UploadData_ID, Location_ID);
            //    }
            //    catch
            //    {
            //    }
            //}

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
            outPdfBuffer = htmlToPdfConverter.ConvertHtml(htmlTemplate(UploadData_ID), ConfigurationManager.AppSettings["SiteURL"]);


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
            float xLocation = ((Math.Abs(fPdfwidth - fImageWidth)) * 2) / 3;

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
            PdfFont font = new PdfFont("Arial", 11, true);
            //create a template to be added in the header and footer           
            // document.Footer = document.AddTemplate(document.Pages[0].ClientRectangle.Width + 95, 108);           
            string strPageNum = "&p; / &P;";

            TextElement footerPageNumber = new TextElement(300f, 10f, 50, strPageNum, font);
            htmlToPdfConverter.PdfFooterOptions.AddElement(footerPageNumber);

        }



        private string htmlTemplate(long UploadData_ID)
        {

            string[] ViewArray = ViewToString((long)UploadData_ID).Split('`');
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

            return string.Join("", ViewArray);
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
        #endregion
    }
}
