using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using e2rcModel.DataAccessLayer;
using System.Security.Cryptography;
using e2rcModel.Common;

namespace e2rc.Models
{
    public class MailSetting
    {

        public static void SendMail(string InspectorMail, string companyName, string ProjectName, string InspectorName, string TrackingNo, DateTime date, string Location, string pdfPath, long Inspection_ID)
        {
            Attachment att = null;
            if (!string.IsNullOrEmpty(pdfPath))
            {
                att = new Attachment(pdfPath);
            }
            try
            {         
                StringBuilder Sb = new StringBuilder(); 
                DataSet dataset = new DAL().ExecuteStoredProcedure("sp_InspectionCompletedReport", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });  
                if (dataset.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(Convert.ToString(dataset.Tables[0].Rows[0]["InspectionReportEmails"])))
                    {
                        char[] delimiterChars = { ';', ','};
                        string[] InspectionReportEmailIds = dataset.Tables[0].Rows[0]["InspectionReportEmails"].ToString().Split(delimiterChars);
                        foreach (string InspectionReportEmail in InspectionReportEmailIds)
                        {
                            Sb.Clear();
                            Sb.Append("<html>");
                            Sb.Append("<body>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p> A stormwater inspection was completed at <b> " + ProjectName + "</b> on <b> " + date.ToShortDateString() + ". </b></p></div>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To download and acknowledge receipt of the inspection report click here: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection/DownloadPDF?Inspection_ID=" + Inspection_ID + ">Download PDF</a></p></div>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To view all inspection reports or open items for this project log in to your account: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + ">http://e2rc.azurewebsites.net/ </a></p></div>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">The inspection identified maintenance or corrective actions.</div><br/>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">The regulation requires modification to or complete repair no later than seven days following discovery to remain compliant.</div><br/>");
                            Sb.Append("<table cellpadding=\"5\" border=\"1\" style=\"Width:70%;font-Size:12px;font-family:Times New Roman;\">");
                            Sb.Append("<tr>");
                            if (Convert.ToString(dataset.Tables[0].Rows[0]["Form_ID"]) == "1000")
                            {
                                Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Location</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                            }
                            else
                            {
                                Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Station</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                            }
                            Sb.Append("</tr>");
                            foreach (DataRow dr in dataset.Tables[0].Rows)
                            {
                                try
                                {
                                    Sb.Append("<tr>");
                                    Sb.Append("<td>" + dr["SandEControls"] + " </td>");
                                    Sb.Append("<td>" + dr["MaintenanceNeeded"] + " </td>");
                                    Sb.Append("<td>" + dr["ActionRequired"] + " </td>");
                                    if ((Convert.ToString(dr["Form_ID"])) == "1000")
                                    {
                                        Sb.Append("<td>" + dr["u_Location"] + " </td>");
                                    }
                                    else
                                    {
                                        Sb.Append("<td>" + dr["Station"] + " </td>");
                                    }                                    
                                    Sb.Append("<td>" + dr["Quantity"] + " </td>");
                                    Sb.Append("<td>" + dr["UOM"] + " </td>");
                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["UploadImagePath"])))
                                    {
                                        if ((Convert.ToString(dr["Form_ID"])) == "1000")
                                        {
                                            Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                                        }
                                        else
                                        {
                                            Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "StationInspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                                        }
                                    }
                                    else
                                    {
                                        Sb.Append("<td>No</td>");
                                    }
                                    Sb.Append("<td>" + dr["Status"] + " </td>");
                                    Sb.Append("</tr>");
                                }
                                catch
                                {
                                }
                            }
                            Sb.Append("</table><br/>");
                            string[] splitInspectionReportEmail = InspectionReportEmail.Split('@');
                            string EncryptedInspectionReportEmail = Encrypt(splitInspectionReportEmail[0], true);
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection/ActionMaintenanceWorktoE2RC?Inspection_ID=" + Inspection_ID + "&InspEmail=" + EncryptedInspectionReportEmail + "&Date=" + Convert.ToDateTime(dataset.Tables[0].Rows[0]["ModifiedDate"]) + "\"> Click here for E2RC to fix it! </a></div><br/>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br/>E2RC SWPPP Compliance Team<br/><img src='" + WebConfigurationManager.AppSettings["ImgLogo"] + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                            Sb.Append("</body>");
                            Sb.Append("</html>");

                            MailSend(WebConfigurationManager.AppSettings["Mailfrom"], InspectionReportEmail,ProjectName + " " + " Inspection Report", Sb.ToString(), att);
                        }               
               
            }
                else if ( dataset.Tables[0].Rows.Count ==0 && dataset.Tables[5].Rows.Count > 0 && !String.IsNullOrEmpty(Convert.ToString(dataset.Tables[5].Rows[0]["InspectionReportEmails"])))
                {
                    char[] delimiterChars = { ';', ',' };
                    string[] InspectionReportEmailIds = dataset.Tables[5].Rows[0]["InspectionReportEmails"].ToString().Split(delimiterChars);
                    foreach (string InspectionReportEmail in InspectionReportEmailIds)
                    {
                        Sb.Clear();
                        Sb.Append("<html>");
                        Sb.Append("<body>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p> A stormwater inspection was completed at <b> " + ProjectName + "</b> on <b> " + date.ToShortDateString() + ". </b></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To download and acknowledge receipt of the inspection report click here: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection/DownloadPDF?Inspection_ID=" + Inspection_ID + ">Download PDF</a></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To view all inspection reports or open items for this project log in to your account: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "> http://e2rc.azurewebsites.net/ </a></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>The inspection did not identify any maintenance or corrective actions</p></div><br/>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + WebConfigurationManager.AppSettings["ImgLogo"] + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                        Sb.Append("</body>");
                        Sb.Append("</html>");
                        MailSend(WebConfigurationManager.AppSettings["Mailfrom"], InspectionReportEmail, ProjectName + " " + " Inspection Report", Sb.ToString(), att);
                    }
                }

            }



            catch (Exception EX)
            {
                throw;
            }
            finally
            {
                //att.Dispose();
            }
        }

        public static void SendMail(string ProjectName, long Inspection_ID, DateTime date, string companyName)
        {
            Attachment att = null;
            try
            {
                DataSet dsReviewer = new DAL().ExecuteStoredProcedure("sp_getReviewerDetailsForMail", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
                StringBuilder Sb = new StringBuilder();
                
                if (dsReviewer != null && dsReviewer.Tables.Count > 0 && dsReviewer.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drReviewer in dsReviewer.Tables[0].Rows)
                    {
                        Sb.Clear();
                        Sb.Append("<html>");
                        Sb.Append("<body>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>A stormwater inspection was completed at<b> " + ProjectName + "</b> on <b> " + date.ToShortDateString() + ". </b></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To download and acknowledge receipt of the inspection report click here: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "ReviewerInspection/DownloadPDF?Inspection_ID=" + Inspection_ID + "&Reviewer_ID=" + drReviewer["Reviewer_ID"] + ">Download PDF</a></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To view all inspection reports or open items for this project log in to your account: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "> http://e2rc.azurewebsites.net/ </a></p></div>");
                        
                        DataSet dataset = new DAL().ExecuteStoredProcedure("sp_InspectionCompletedReport", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });

                        if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                        {
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">The inspection identified maintenance or corrective actions.</div><br/>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">The regulation requires modification to or complete repair no later than seven days following discovery to remain compliant.</div><br/>");
                            Sb.Append("<table cellpadding=\"5\" border=\"1\" style=\"Width:60%;font-Size:12px;font-family:Times New Roman;\">");
                            Sb.Append("<tr>");                           
                            if (Convert.ToString(dataset.Tables[0].Rows[0]["Form_ID"]) == "1000")
                            {

                                Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Location</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                            }
                            else
                            {
                                Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Station</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                            }
                            Sb.Append("</tr>");
                            foreach (DataRow dr in dataset.Tables[0].Rows)
                            {
                                try
                                {
                                    Sb.Append("<tr>");
                                    Sb.Append("<td>" + dr["SandEControls"] + " </td>");
                                    Sb.Append("<td>" + dr["MaintenanceNeeded"] + " </td>");
                                    Sb.Append("<td>" + dr["ActionRequired"] + " </td>");
                                    if ((Convert.ToString(dr["Form_ID"])) == "1000")
                                    {
                                        Sb.Append("<td>" + dr["u_Location"] + " </td>");
                                    }
                                    else
                                    {
                                        Sb.Append("<td>" + dr["Station"] + " </td>");
                                    }  
                                    Sb.Append("<td>" + dr["Quantity"] + " </td>");
                                    Sb.Append("<td>" + dr["UOM"] + " </td>");
                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["UploadImagePath"])))
                                    {
                                        if ((Convert.ToString(dr["Form_ID"])) == "1000")
                                        {
                                            Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                                        }
                                        else
                                        {
                                            Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "StationInspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                                        }
                                    }
                                    else
                                    {
                                        Sb.Append("<td>No</td>");
                                    }
                                    Sb.Append("<td>" + dr["Status"] + " </td>");
                                    Sb.Append("</tr>");
                                }
                                catch
                                {
                                }
                            }
                            Sb.Append("</table><br/>");
                            string[] splitReviewerEmail = Convert.ToString(drReviewer["Email"]).Split('@');
                            string EncryptedReviewerEmail = Encrypt(splitReviewerEmail[0], true);
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection/ActionMaintenanceWorktoE2RC?Inspection_ID=" + Inspection_ID + "&InspEmail=" + EncryptedReviewerEmail + "&Date=" + Convert.ToDateTime(dataset.Tables[0].Rows[0]["ModifiedDate"]) + "\"> Click here for E2RC to fix it! </a></div><br/>");
                           // Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><a  href=" + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection/ActionMaintenanceWorktoInspector?Inspection_ID=" + Inspection_ID + "&Date=" + Convert.ToDateTime(dataset.Tables[0].Rows[0]["ModifiedDate"]) + "\"><input type=\"button\" value=\" Fix It\"/></a></div></br>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + WebConfigurationManager.AppSettings["ImgLogo"] + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                            Sb.Append("</body>");
                            Sb.Append("</html>");
                        }
                        else
                        {
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>The inspection did not identify any maintenance or corrective actions</p></div><br/>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + WebConfigurationManager.AppSettings["ImgLogo"] + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                            Sb.Append("</body>");
                            Sb.Append("</html>");
                        }
                        MailSend(WebConfigurationManager.AppSettings["Mailfrom"], Convert.ToString(drReviewer["Email"]),ProjectName + " " + " Inspection Report", Sb.ToString(), att);
                    }
                }
                else
                {
                    Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>The inspection did not identify any maintenance or corrective actions</p></div>");
                    Sb.Append("</body>");
                    Sb.Append("</html>");
                }
            }
            catch (Exception EX)
            {
                throw;
            }
            finally
            {
                //att.Dispose();
            }
        }


        /* Inspection Report For Maintenance No maintenance */
        public static void SendMail(string ProjectName, long Inspection_ID, DateTime date, string companyName, string sCompanyLogoUrl)
        {
            Attachment att = null;
            try
            {
                DataSet dsReviewer = new DAL().ExecuteStoredProcedure("sp_getReviewerDetailsForMail", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
                StringBuilder Sb = new StringBuilder();


                if (dsReviewer != null && dsReviewer.Tables.Count > 0 && dsReviewer.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drReviewer in dsReviewer.Tables[0].Rows)
                    {
                        Sb.Clear();
                        Sb.Append("<html>");
                        Sb.Append("<body>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>A stormwater inspection was completed at<b> " + ProjectName + "</b> on <b> " + date.ToShortDateString() + ". </b></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To download and acknowledge receipt of the inspection report click here: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "ReviewerInspection/DownloadPDF?Inspection_ID=" + Inspection_ID + "&Reviewer_ID=" + drReviewer["Reviewer_ID"] + ">Download PDF</a></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To view all inspection reports or open items for this project log in to your account: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "> http://e2rc.azurewebsites.net/ </a></p></div>");

                        DataSet dataset = new DAL().ExecuteStoredProcedure("sp_InspectionCompletedReport", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });

                        if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                        {
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">The inspection identified maintenance or corrective actions.</div><br/>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">The regulation requires modification to or complete repair no later than seven days following discovery to remain compliant.</div><br/>");
                            Sb.Append("<table cellpadding=\"5\" border=\"1\" style=\"Width:60%;font-Size:12px;font-family:Times New Roman;\">");
                            Sb.Append("<tr>");
                            if (Convert.ToString(dataset.Tables[0].Rows[0]["Form_ID"]) == "1000")
                            {

                                Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Location</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                            }
                            else
                            {
                                Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Station</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                            }
                            Sb.Append("</tr>");
                            foreach (DataRow dr in dataset.Tables[0].Rows)
                            {
                                try
                                {
                                    Sb.Append("<tr>");
                                    Sb.Append("<td>" + dr["SandEControls"] + " </td>");
                                    Sb.Append("<td>" + dr["MaintenanceNeeded"] + " </td>");
                                    Sb.Append("<td>" + dr["ActionRequired"] + " </td>");
                                    if ((Convert.ToString(dr["Form_ID"])) == "1000")
                                    {
                                        Sb.Append("<td>" + dr["u_Location"] + " </td>");
                                    }
                                    else
                                    {
                                        Sb.Append("<td>" + dr["Station"] + " </td>");
                                    }
                                    Sb.Append("<td>" + dr["Quantity"] + " </td>");
                                    Sb.Append("<td>" + dr["UOM"] + " </td>");
                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["UploadImagePath"])))
                                    {
                                        if ((Convert.ToString(dr["Form_ID"])) == "1000")
                                        {
                                            Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                                        }
                                        else
                                        {
                                            Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "StationInspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                                        }
                                    }
                                    else
                                    {
                                        Sb.Append("<td>No</td>");
                                    }
                                    Sb.Append("<td>" + dr["Status"] + " </td>");
                                    Sb.Append("</tr>");
                                }
                                catch
                                {
                                }
                            }
                            Sb.Append("</table><br/>");
                            string[] splitReviewerEmail = Convert.ToString(drReviewer["Email"]).Split('@');
                            string EncryptedReviewerEmail = Encrypt(splitReviewerEmail[0], true);
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection/ActionMaintenanceWorktoE2RC?Inspection_ID=" + Inspection_ID + "&InspEmail=" + EncryptedReviewerEmail + "&Date=" + Convert.ToDateTime(dataset.Tables[0].Rows[0]["ModifiedDate"]) + "\"> Click here for E2RC to fix it! </a></div><br/>");
                            // Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><a  href=" + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection/ActionMaintenanceWorktoInspector?Inspection_ID=" + Inspection_ID + "&Date=" + Convert.ToDateTime(dataset.Tables[0].Rows[0]["ModifiedDate"]) + "\"><input type=\"button\" value=\" Fix It\"/></a></div></br>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + sCompanyLogoUrl + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                            Sb.Append("</body>");
                            Sb.Append("</html>");
                        }
                        else
                        {
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>The inspection did not identify any maintenance or corrective actions</p></div><br/>");
                            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + sCompanyLogoUrl + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                            Sb.Append("</body>");
                            Sb.Append("</html>");
                        }
                        MailSend(WebConfigurationManager.AppSettings["Mailfrom"], Convert.ToString(drReviewer["Email"]), ProjectName + " " + " Inspection Report", Sb.ToString(), att);
                    }
                }
                else
                {
                    Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>The inspection did not identify any maintenance or corrective actions</p></div>");
                    Sb.Append("</body>");
                    Sb.Append("</html>");
                }


            }
            catch (Exception EX)
            {
                throw;
            }
            finally
            {
                //att.Dispose();
            }
        }

        //For Company Logo Image Sending with Details (Inspection Report Mail)
        public static void SendMail(string InspectorMail, string companyName, string ProjectName, string InspectorName, string TrackingNo, DateTime date, string Location, string pdfPath, long Inspection_ID, string sCompanyLogoUrl)
        {
            Attachment att = null;
            if (!string.IsNullOrEmpty(pdfPath))
            {
                att = new Attachment(pdfPath);
            }
            try
            {
                StringBuilder Sb = new StringBuilder();
                DataSet dataset = new DAL().ExecuteStoredProcedure("sp_InspectionCompletedReport", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
                if (dataset.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(Convert.ToString(dataset.Tables[0].Rows[0]["InspectionReportEmails"])))
                {
                    char[] delimiterChars = { ';', ',' };
                    string[] InspectionReportEmailIds = dataset.Tables[0].Rows[0]["InspectionReportEmails"].ToString().Split(delimiterChars);
                    foreach (string InspectionReportEmail in InspectionReportEmailIds)
                    {
                        Sb.Clear();
                        Sb.Append("<html>");
                        Sb.Append("<body>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p> A stormwater inspection was completed at <b> " + ProjectName + "</b> on <b> " + date.ToShortDateString() + ". </b></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To download and acknowledge receipt of the inspection report click here: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection/DownloadPDF?Inspection_ID=" + Inspection_ID + ">Download PDF</a></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To view all inspection reports or open items for this project log in to your account: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + ">http://e2rc.azurewebsites.net/ </a></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">The inspection identified maintenance or corrective actions.</div><br/>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">The regulation requires modification to or complete repair no later than seven days following discovery to remain compliant.</div><br/>");
                        Sb.Append("<table cellpadding=\"5\" border=\"1\" style=\"Width:70%;font-Size:12px;font-family:Times New Roman;\">");
                        Sb.Append("<tr>");
                        if (Convert.ToString(dataset.Tables[0].Rows[0]["Form_ID"]) == "1000")
                        {
                            Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Location</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                        }
                        else
                        {
                            Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Station</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                        }
                        Sb.Append("</tr>");
                        foreach (DataRow dr in dataset.Tables[0].Rows)
                        {
                            try
                            {
                                Sb.Append("<tr>");
                                Sb.Append("<td>" + dr["SandEControls"] + " </td>");
                                Sb.Append("<td>" + dr["MaintenanceNeeded"] + " </td>");
                                Sb.Append("<td>" + dr["ActionRequired"] + " </td>");
                                if ((Convert.ToString(dr["Form_ID"])) == "1000")
                                {
                                    Sb.Append("<td>" + dr["u_Location"] + " </td>");
                                }
                                else
                                {
                                    Sb.Append("<td>" + dr["Station"] + " </td>");
                                }
                                Sb.Append("<td>" + dr["Quantity"] + " </td>");
                                Sb.Append("<td>" + dr["UOM"] + " </td>");
                                if (!string.IsNullOrEmpty(Convert.ToString(dr["UploadImagePath"])))
                                {
                                    if ((Convert.ToString(dr["Form_ID"])) == "1000")
                                    {
                                        Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                                    }
                                    else
                                    {
                                        Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "StationInspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                                    }
                                }
                                else
                                {
                                    Sb.Append("<td>No</td>");
                                }
                                Sb.Append("<td>" + dr["Status"] + " </td>");
                                Sb.Append("</tr>");
                            }
                            catch
                            {
                            }
                        }
                        Sb.Append("</table><br/>");
                        string[] splitInspectionReportEmail = InspectionReportEmail.Split('@');
                        string EncryptedInspectionReportEmail = Encrypt(splitInspectionReportEmail[0], true);
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection/ActionMaintenanceWorktoE2RC?Inspection_ID=" + Inspection_ID + "&InspEmail=" + EncryptedInspectionReportEmail + "&Date=" + Convert.ToDateTime(dataset.Tables[0].Rows[0]["ModifiedDate"]) + "\"> Click here for E2RC to fix it! </a></div><br/>");
                        //Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br/>E2RC SWPPP Compliance Team<br/><img src='" + WebConfigurationManager.AppSettings["ImgLogo"] + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br/>E2RC SWPPP Compliance Team<br/><img src='" +  sCompanyLogoUrl + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                        Sb.Append("</body>");
                        Sb.Append("</html>");

                        MailSend(WebConfigurationManager.AppSettings["Mailfrom"], InspectionReportEmail, ProjectName + " " + " Inspection Report", Sb.ToString(), att);
                    }
                }
                else if (dataset.Tables[0].Rows.Count == 0 && dataset.Tables[5].Rows.Count > 0 && !String.IsNullOrEmpty(Convert.ToString(dataset.Tables[5].Rows[0]["InspectionReportEmails"])))
                {
                    char[] delimiterChars = { ';', ',' };
                    string[] InspectionReportEmailIds = dataset.Tables[5].Rows[0]["InspectionReportEmails"].ToString().Split(delimiterChars);
                    foreach (string InspectionReportEmail in InspectionReportEmailIds)
                    {
                        Sb.Clear();
                        Sb.Append("<html>");
                        Sb.Append("<body>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p> A stormwater inspection was completed at <b> " + ProjectName + "</b> on <b> " + date.ToShortDateString() + ". </b></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To download and acknowledge receipt of the inspection report click here: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection/DownloadPDF?Inspection_ID=" + Inspection_ID + ">Download PDF</a></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>To view all inspection reports or open items for this project log in to your account: <a href=" + WebConfigurationManager.AppSettings["SiteURL"] + "> http://e2rc.azurewebsites.net/ </a></p></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>The inspection did not identify any maintenance or corrective actions</p></div><br/>");
                        //Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + WebConfigurationManager.AppSettings["ImgLogo"] + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                        Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + sCompanyLogoUrl + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                        Sb.Append("</body>");
                        Sb.Append("</html>");
                        MailSend(WebConfigurationManager.AppSettings["Mailfrom"], InspectionReportEmail, ProjectName + " " + " Inspection Report", Sb.ToString(), att);
                    }
                }

            }
            catch (Exception EX)
            {
                throw;
            }
            finally
            {
                //att.Dispose();
            }
        }

        /*Mail Notification For Assign and Unassign Porject On Reviewer */
        public static bool SendMail(string action, long ID, string slstLocationID, string Role, string Name, string Email) 
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_AssignProjectMailNotification", new object[] { "@Action", "@ID", "@Role", "@lstLocationID" }, new object[] { action, ID, Role, slstLocationID });

            bool IsmailSent = false;
            StringBuilder Sb = new StringBuilder();
            Sb.Clear();
            Sb.Append("<html>");
            Sb.Append("<body>");
            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p><b>Mr. " + Name + ".</b> </p> </div>");
  
            /*Assign Project List*/
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"> <p>Below projects are Assigned to you.</p></div>");
                Sb.Append("<ul style =\"list-style-type: circle;\">");

                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    Sb.Append("<li>" + dr["LocationName"] + " </li>");
                }
                Sb.Append("</ul>");
            }

            /* Unassign Project List */
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[1].Rows.Count > 0)
            {
                Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"> <p>Below projects are removed from your project list. </p> </div>");
                Sb.Append("<ul style =\"list-style-type: circle;\">");

                foreach (DataRow dr in dataset.Tables[1].Rows)
                {
                    Sb.Append("<li>" + dr["LocationName"] + " </li>");
                }
                Sb.Append("</ul>");
            }

            Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>Thank You.</p></div>");  

            // Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + WebConfigurationManager.AppSettings["ImgLogo"] + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
            Sb.Append("</body>");
            Sb.Append("</html>");

            try
            {
                if (dataset != null && dataset.Tables.Count > 0)
                {
                    //MailSend(Convert.ToString(dataset.Tables[0].Rows[0]["Email_1"]), "e2rc@e2rc.com", "", "Maintenance Needed/Action Required", ActionSb.ToString(), null);
                    IsmailSent = MailSend(WebConfigurationManager.AppSettings["Mailfrom"], WebConfigurationManager.AppSettings["Maile2rc"], "Assign/Unassign Project", Sb.ToString(), null);
                }
            }
            catch
            {
            }

            if (IsmailSent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // made it for Service
        public static bool ServiceActionMaintenanceMailToE2rc(long Inspection_ID, int ActionDay,string Email)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_fetchActionMaintenanceDetailsforE2RC", new object[] { "@Inspection_ID", "@Day" }, new object[] { Inspection_ID, ActionDay });
            bool IsmailSent = false;
            StringBuilder Sb = new StringBuilder();
            string UserName = string.Empty;
            if (!string.IsNullOrEmpty(Email))
            {
                UserName = Decrypt(Email, true);
            }
            
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                Sb.Clear();
                Sb.Append("<html>");
                Sb.Append("<body>");
                Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
                Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>An inspection was completed at<b> " + dataset.Tables[0].Rows[0]["Name"] + "</b> on <b> " + Convert.ToString(dataset.Tables[0].Rows[0]["InspectionDate"]) + ".</b> The inspection identified maintenance or corrective actions.</p></div>");  
                Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">" + UserName + " with " + dataset.Tables[0].Rows[0]["CompanyName"] + " would like E2RC to perform the maintenance or corrective action activities listed below.</div><br/>");               
                Sb.Append("<table cellpadding=\"5\" border=\"1\" style=\"Width:70%;font-Size:12px;\">");
                Sb.Append("<tr>"); 
                   if (Convert.ToString(dataset.Tables[0].Rows[0]["Form_ID"]) == "1000")
                   {
                       Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Location</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                   }
                   else
                   {
                       Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Station</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                   }
                Sb.Append("</tr>");
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    try
                    {
                        Sb.Append("<tr>");
                        Sb.Append("<td>" + dr["SandEControls"] + " </td>");
                        Sb.Append("<td>" + dr["MaintenanceNeeded"] + " </td>");
                        Sb.Append("<td>" + dr["ActionRequired"] + " </td>");
                        if ((Convert.ToString(dr["Form_ID"])) == "1000")
                        {
                            Sb.Append("<td>" + dr["u_Location"] + " </td>");
                        }
                        else
                        {
                            Sb.Append("<td>" + dr["Station"] + " </td>");
                        }
                        Sb.Append("<td>" + dr["Quantity"] + " </td>");
                        Sb.Append("<td>" + dr["UOM"] + " </td>");
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UploadImagePath"])))
                        {
                            if ((Convert.ToString(dr["Form_ID"])) == "1000")
                            {
                                Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                            }
                            else
                            {
                                Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "StationInspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                            }
                        }
                        else
                        {
                            Sb.Append("<td>No</td>");
                        }
                        Sb.Append("<td>" + dr["Status"] + " </td>");
                        Sb.Append("</tr>");
                    }
                    catch
                    {
                    }
                }

                Sb.Append("</table><br/>");
               // Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + WebConfigurationManager.AppSettings["ImgLogo"] + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                Sb.Append("</body>");
                Sb.Append("</html>");

                try
                {
                    //MailSend(Convert.ToString(dataset.Tables[0].Rows[0]["Email_1"]), "e2rc@e2rc.com", "", "Maintenance Needed/Action Required", ActionSb.ToString(), null);
                    IsmailSent = MailSend(WebConfigurationManager.AppSettings["Mailfrom"], WebConfigurationManager.AppSettings["Maile2rc"],"Maintenance Needed/Action Required", Sb.ToString(), null);
                }
                catch
                {
                }
            }

            if (IsmailSent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // made it for Service with Franchise Comapny Logo If Exists(Maintenance Request)
        public static bool ServiceActionMaintenanceMailToE2rc(long Inspection_ID, int ActionDay, string Email, string sCompanyLogoUrl)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_fetchActionMaintenanceDetailsforE2RC", new object[] { "@Inspection_ID", "@Day" }, new object[] { Inspection_ID, ActionDay });
            bool IsmailSent = false;
            StringBuilder Sb = new StringBuilder();
            string UserName = string.Empty;
            if (!string.IsNullOrEmpty(Email))
            {
                UserName = Decrypt(Email, true);
            }

            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                Sb.Clear();
                Sb.Append("<html>");
                Sb.Append("<body>");
                Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
                Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>An inspection was completed at<b> " + dataset.Tables[0].Rows[0]["Name"] + "</b> on <b> " + Convert.ToString(dataset.Tables[0].Rows[0]["InspectionDate"]) + ".</b> The inspection identified maintenance or corrective actions.</p></div>");
                Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">" + UserName + " with " + dataset.Tables[0].Rows[0]["CompanyName"] + " would like E2RC to perform the maintenance or corrective action activities listed below.</div><br/>");
                Sb.Append("<table cellpadding=\"5\" border=\"1\" style=\"Width:70%;font-Size:12px;\">");
                Sb.Append("<tr>");
                if (Convert.ToString(dataset.Tables[0].Rows[0]["Form_ID"]) == "1000")
                {
                    Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Location</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                }
                else
                {
                    Sb.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Station</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                }
                Sb.Append("</tr>");
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    try
                    {
                        Sb.Append("<tr>");
                        Sb.Append("<td>" + dr["SandEControls"] + " </td>");
                        Sb.Append("<td>" + dr["MaintenanceNeeded"] + " </td>");
                        Sb.Append("<td>" + dr["ActionRequired"] + " </td>");
                        if ((Convert.ToString(dr["Form_ID"])) == "1000")
                        {
                            Sb.Append("<td>" + dr["u_Location"] + " </td>");
                        }
                        else
                        {
                            Sb.Append("<td>" + dr["Station"] + " </td>");
                        }
                        Sb.Append("<td>" + dr["Quantity"] + " </td>");
                        Sb.Append("<td>" + dr["UOM"] + " </td>");
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UploadImagePath"])))
                        {
                            if ((Convert.ToString(dr["Form_ID"])) == "1000")
                            {
                                Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                            }
                            else
                            {
                                Sb.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "StationInspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                            }
                        }
                        else
                        {
                            Sb.Append("<td>No</td>");
                        }
                        Sb.Append("<td>" + dr["Status"] + " </td>");
                        Sb.Append("</tr>");
                    }
                    catch
                    {
                    }
                }

                Sb.Append("</table><br/>");
                // Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + WebConfigurationManager.AppSettings["ImgLogo"] + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                Sb.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + sCompanyLogoUrl + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                Sb.Append("</body>");
                Sb.Append("</html>");

                try
                {
                    //MailSend(Convert.ToString(dataset.Tables[0].Rows[0]["Email_1"]), "e2rc@e2rc.com", "", "Maintenance Needed/Action Required", ActionSb.ToString(), null);
                    IsmailSent = MailSend(WebConfigurationManager.AppSettings["Mailfrom"], WebConfigurationManager.AppSettings["Maile2rc"], "Maintenance Needed/Action Required", Sb.ToString(), null);
                }
                catch
                {
                }
            }

            if (IsmailSent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ActionMaintenanceWorktoE2RC(long Inspection_ID,string InspectionReportEmailID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_ActionMaintenanceWorktoE2RC", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
            bool IsmailSent = false;
            StringBuilder SbWorktoE2RC = new StringBuilder();
            string UserName = string.Empty;
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                SbWorktoE2RC.Clear();
                SbWorktoE2RC.Append("<html>");
                SbWorktoE2RC.Append("<body>");
                if (!String.IsNullOrEmpty(InspectionReportEmailID))
                {
                    UserName = Decrypt(InspectionReportEmailID, true);
                }

                SbWorktoE2RC.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
                SbWorktoE2RC.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>An inspection was completed at<b> " + dataset.Tables[0].Rows[0]["Name"] + "</b> on <b> " + Convert.ToString(dataset.Tables[0].Rows[0]["InspectionDate"]) + ".</b> The inspection identified maintenance or corrective actions.</p></div>");                  
                SbWorktoE2RC.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">" + UserName + " with " + dataset.Tables[0].Rows[0]["CompanyName"] + " would like E2RC to perform the maintenance or corrective action activities listed below.</div><br/>");               
                SbWorktoE2RC.Append("<table cellpadding=\"5\" border=\"1\" style=\"Width:60%;font-Size:12px;font-family:Times New Roman;\">");
                SbWorktoE2RC.Append("<tr>");
                if (Convert.ToString(dataset.Tables[0].Rows[0]["Form_ID"]) == "1000")
                {
                    SbWorktoE2RC.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Location</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                }
                else
                {
                    SbWorktoE2RC.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Station</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Status</th>");
                }
                SbWorktoE2RC.Append("</tr>");
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    try
                    {
                        SbWorktoE2RC.Append("<tr>");
                        SbWorktoE2RC.Append("<td>" + dr["SandEControls"] + " </td>");
                        SbWorktoE2RC.Append("<td>" + dr["MaintenanceNeeded"] + " </td>");
                        SbWorktoE2RC.Append("<td>" + dr["ActionRequired"] + " </td>");
                        if ((Convert.ToString(dr["Form_ID"])) == "1000")
                        {
                            SbWorktoE2RC.Append("<td>" + dr["u_Location"] + " </td>");
                        }
                        else
                        {
                            SbWorktoE2RC.Append("<td>" + dr["Station"] + " </td>");
                        }
                        SbWorktoE2RC.Append("<td>" + dr["Quantity"] + " </td>");
                        SbWorktoE2RC.Append("<td>" + dr["UOM"] + " </td>");
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UploadImagePath"])))
                        {
                            if ((Convert.ToString(dr["Form_ID"])) == "1000")
                            {
                                SbWorktoE2RC.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                            }
                            else
                            {
                                SbWorktoE2RC.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "StationInspection" + dr["UploadImagePath"] + ">Yes</a></td>");
                            }
                        }
                        else
                        {
                            SbWorktoE2RC.Append("<td>No</td>");
                        }
                        SbWorktoE2RC.Append("<td>" + dr["Status"] + " </td>");
                        SbWorktoE2RC.Append("</tr>");
                    }
                    catch
                    {
                    }
                }

                SbWorktoE2RC.Append("</table><br/><br/>");
               // SbWorktoE2RC.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\">Thank you,<br />E2RC SWPPP Compliance Team <br/><br/>E2RC :<br/><br/> <img src='" + WebConfigurationManager.AppSettings["ImgLogo"] + "?Inspection_ID=" + Inspection_ID + "' alt='Logo' /></div>");
                SbWorktoE2RC.Append("</body>");
                SbWorktoE2RC.Append("</html>");

                try
                {
                    IsmailSent = MailSend(WebConfigurationManager.AppSettings["Mailfrom"], WebConfigurationManager.AppSettings["Maile2rc"],"Request for Action Required/Maintenance Needed", SbWorktoE2RC.ToString(), null);
                }
                catch
                {
                }
            }
            if (IsmailSent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
              
        public static void CompletionWorkOrederMailToContrator(long Inspection_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_CompletionWorkOrederMailToContrator", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                StringBuilder SbMailToContrator = new StringBuilder();
                SbMailToContrator.Append("<html>");
                SbMailToContrator.Append("<body>");
                SbMailToContrator.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
                SbMailToContrator.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>The following maintenance or corrective actions were completed for the <b> " + dataset.Tables[0].Rows[0]["Name"] + "</b> project on <b> " + dataset.Tables[0].Rows[0]["workorderCompletedDate"] + ". </b></p></div>");
                SbMailToContrator.Append("<table cellpadding=\"5\" border=\"1\" style=\"Width:60%;font-Size:12px;font-family:Times New Roman;\">");
                SbMailToContrator.Append("<tr>");
                if (Convert.ToString(dataset.Tables[0].Rows[0]["Form_ID"]) == "1000")
                {
                    SbMailToContrator.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Location</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Maintenance Completed Date</th><th>Action Completed Date</th>");
                }
                else
                {
                    SbMailToContrator.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Station</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Maintenance Completed Date</th><th>Action Completed Date</th>");
                }
                SbMailToContrator.Append("</tr>");
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        SbMailToContrator.Append("<tr>");
                        SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["SandEControls"] + " </td>");
                        SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["MaintenanceNeeded"] + " </td>");
                        SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["ActionRequired"] + " </td>");

                        if (Convert.ToString(dataset.Tables[0].Rows[i]["Form_ID"]) == "1000")
                        {
                            SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["u_Location"] + " </td>");
                        }
                        else
                        {
                            SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["Station"] + " </td>");                      
                        }
                        
                        SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["Quantity"] + " </td>");
                        SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["UOM"] + " </td>");
                        if (!string.IsNullOrEmpty(Convert.ToString(dataset.Tables[0].Rows[i]["UploadImagePath"])))
                        {
                            if (Convert.ToString(dataset.Tables[0].Rows[i]["Form_ID"]) == "1000")
                            {
                                SbMailToContrator.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection" + dataset.Tables[0].Rows[i]["UploadImagePath"] + ">Yes</a></td>");
                            }
                            else
                            {
                                SbMailToContrator.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "StationInspection" + dataset.Tables[0].Rows[i]["UploadImagePath"] + ">Yes</a></td>");
                            }
                        }
                        else
                        {
                            SbMailToContrator.Append("<td>No</td>");
                        }
                        if (Convert.ToString(dataset.Tables[0].Rows[i]["MaintenanceNeeded"]).Trim() !="None")
                        {
                            SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["MaintenanceCompletedDate"] + " </td>");
                        }
                        else
                        {
                            SbMailToContrator.Append("<td>Not Applicable</td>");
                        }

                        if (Convert.ToString(dataset.Tables[0].Rows[i]["ActionRequired"]).Trim()!="None")
                        {
                            SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["ActionCompletedDate"] + " </td>");
                        }
                        else
                        {
                            SbMailToContrator.Append("<td>Not Applicable</td>");
                        }
                       
                        SbMailToContrator.Append("</tr>");
                    }
                    catch
                    {
                    }
                }
                SbMailToContrator.Append("</table><br/>");
                SbMailToContrator.Append("<div style=\"font-Size:12px;\">Thank you,</div> <br/>");
                SbMailToContrator.Append("<div style=\"font-Size:12px;\"> E2RC SWPPP Compliance Team <br/><img src='" + WebConfigurationManager.AppSettings["Imgemail"] + "?Inspection_ID=" + Inspection_ID + "' alt='Imgemail' /> </div>");
                SbMailToContrator.Append("</body>");
                SbMailToContrator.Append("</html>");
                
                try
                {
                    MailSend(WebConfigurationManager.AppSettings["Mailfrom"], Convert.ToString(dataset.Tables[0].Rows[0]["WorkOrdersEmails"]).Replace(';', ','),"SWPPP Work Order Completed", SbMailToContrator.ToString(), null);
                   // MailSend(WebConfigurationManager.AppSettings["Mailfrom"], "team.dotnet@jbsolutions.in", "SWPPP Work Order Completed", SbMailToContrator.ToString(), null);
                }
                catch
                {
                }
            }
        }

        // Completion Work Oreder Mail with Franchise Comapny Logo If Exists (Work Order Completed)
        public static void CompletionWorkOrederMailToContrator(long Inspection_ID, string sCompanyLogoUrl)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_CompletionWorkOrederMailToContrator", new object[] { "@Inspection_ID" }, new object[] { Inspection_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                StringBuilder SbMailToContrator = new StringBuilder();
                SbMailToContrator.Append("<html>");
                SbMailToContrator.Append("<body>");
                SbMailToContrator.Append("<div style=\"font-Size:12px;font-family:Times New Roman;align:center\">Hello,</div>");
                SbMailToContrator.Append("<div style=\"font-Size:12px;font-family:Times New Roman;\"><p>The following maintenance or corrective actions were completed for the <b> " + dataset.Tables[0].Rows[0]["Name"] + "</b> project on <b> " + dataset.Tables[0].Rows[0]["workorderCompletedDate"] + ". </b></p></div>");
                SbMailToContrator.Append("<table cellpadding=\"5\" border=\"1\" style=\"Width:60%;font-Size:12px;font-family:Times New Roman;\">");
                SbMailToContrator.Append("<tr>");
                if (Convert.ToString(dataset.Tables[0].Rows[0]["Form_ID"]) == "1000")
                {
                    SbMailToContrator.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Location</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Maintenance Completed Date</th><th>Action Completed Date</th>");
                }
                else
                {
                    SbMailToContrator.Append("<th>S&E/P2 Control</th><th>Maintenance</th><th>Corrective Action</th><th>Station</th><th>Quantity</th><th>Unit of Measure</th><th>Photo</th><th>Maintenance Completed Date</th><th>Action Completed Date</th>");
                }
                SbMailToContrator.Append("</tr>");
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        SbMailToContrator.Append("<tr>");
                        SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["SandEControls"] + " </td>");
                        SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["MaintenanceNeeded"] + " </td>");
                        SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["ActionRequired"] + " </td>");

                        if (Convert.ToString(dataset.Tables[0].Rows[i]["Form_ID"]) == "1000")
                        {
                            SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["u_Location"] + " </td>");
                        }
                        else
                        {
                            SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["Station"] + " </td>");
                        }

                        SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["Quantity"] + " </td>");
                        SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["UOM"] + " </td>");
                        if (!string.IsNullOrEmpty(Convert.ToString(dataset.Tables[0].Rows[i]["UploadImagePath"])))
                        {
                            if (Convert.ToString(dataset.Tables[0].Rows[i]["Form_ID"]) == "1000")
                            {
                                SbMailToContrator.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "Inspection" + dataset.Tables[0].Rows[i]["UploadImagePath"] + ">Yes</a></td>");
                            }
                            else
                            {
                                SbMailToContrator.Append("<td> <a href= " + WebConfigurationManager.AppSettings["SiteURL"] + "StationInspection" + dataset.Tables[0].Rows[i]["UploadImagePath"] + ">Yes</a></td>");
                            }
                        }
                        else
                        {
                            SbMailToContrator.Append("<td>No</td>");
                        }
                        if (Convert.ToString(dataset.Tables[0].Rows[i]["MaintenanceNeeded"]).Trim() != "None")
                        {
                            SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["MaintenanceCompletedDate"] + " </td>");
                        }
                        else
                        {
                            SbMailToContrator.Append("<td>Not Applicable</td>");
                        }

                        if (Convert.ToString(dataset.Tables[0].Rows[i]["ActionRequired"]).Trim() != "None")
                        {
                            SbMailToContrator.Append("<td>" + dataset.Tables[0].Rows[i]["ActionCompletedDate"] + " </td>");
                        }
                        else
                        {
                            SbMailToContrator.Append("<td>Not Applicable</td>");
                        }

                        SbMailToContrator.Append("</tr>");
                    }
                    catch
                    {
                    }
                }
                SbMailToContrator.Append("</table><br/>");
                SbMailToContrator.Append("<div style=\"font-Size:12px;\">Thank you,</div> <br/>");
                //SbMailToContrator.Append("<div style=\"font-Size:12px;\"> E2RC SWPPP Compliance Team <br/><img src='" + WebConfigurationManager.AppSettings["Imgemail"] + "?Inspection_ID=" + Inspection_ID + "' alt='Imgemail' /> </div>");
                SbMailToContrator.Append("<div style=\"font-Size:12px;\"> E2RC SWPPP Compliance Team <br/><img src='" + sCompanyLogoUrl + "?Inspection_ID=" + Inspection_ID + "' alt='Imgemail' /> </div>");
                SbMailToContrator.Append("</body>");
                SbMailToContrator.Append("</html>");

                try
                {
                    MailSend(WebConfigurationManager.AppSettings["Mailfrom"], Convert.ToString(dataset.Tables[0].Rows[0]["WorkOrdersEmails"]).Replace(';', ','), "SWPPP Work Order Completed", SbMailToContrator.ToString(), null);
                    // MailSend(WebConfigurationManager.AppSettings["Mailfrom"], "team.dotnet@jbsolutions.in", "SWPPP Work Order Completed", SbMailToContrator.ToString(), null);
                }
                catch
                {
                }
            }
        }

        public static bool MailSend(string From, string To, string CC, string Subject, string Body, Attachment att)
        {
            MailMessage mail = new MailMessage();
            //mail.To.Add("team.dotnet@jbsolutions.in");
            mail.To.Add(To);
            mail.From = new MailAddress(From);
            mail.CC.Add(new MailAddress(CC));
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            try
            {
                if (att != null)
                {
                    mail.Attachments.Add(att);
                }
            }
            catch
            {
            }
            SmtpClient smtp = new SmtpClient();
            smtp.Host = WebConfigurationManager.AppSettings["MailServer"];
            smtp.Port = Convert.ToInt16(WebConfigurationManager.AppSettings["Port"]);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["IsSsl"]);
            smtp.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["Username"], WebConfigurationManager.AppSettings["Password"]);
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
            return true;
        }

        private static bool MailSend(string From, string To, string Subject, string Body, Attachment att)
        {
            MailMessage mail = new MailMessage();
            //mail.To.Add("team.dotnet@jbsolutions.in");
            mail.To.Add(To);
            mail.From = new MailAddress(From);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            try
            {
                if (att != null)
                {
                    mail.Attachments.Add(att);
                }
            }
            catch
            {
            }
            SmtpClient smtp = new SmtpClient();
            smtp.Host = WebConfigurationManager.AppSettings["MailServer"]; ;
            smtp.Port = Convert.ToInt16(WebConfigurationManager.AppSettings["Port"]);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["IsSsl"]);
            smtp.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["Username"], WebConfigurationManager.AppSettings["Password"]);
            try
            {
                smtp.Send(mail);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static string MailSend2(string From, string To, string CC, string Subject, string Body)
        {
            string rtn = "";
            MailMessage mail = new MailMessage();
            mail.To.Add(To);
            mail.From = new MailAddress(From);
            mail.CC.Add(new MailAddress(CC));
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;
           
            SmtpClient smtp = new SmtpClient();
            smtp.Host = WebConfigurationManager.AppSettings["MailServer"];
            smtp.Port = Convert.ToInt16(WebConfigurationManager.AppSettings["Port"]);
            
            smtp.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["Username"], WebConfigurationManager.AppSettings["Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["IsSsl"]);
            try
            {
                smtp.Send(mail);
                rtn = "success!";
            }
            catch (Exception ex)
            {
                rtn = ex.Message.ToString();
            }
            return rtn;
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                //of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString.Replace(" ","+"));

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}