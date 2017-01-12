using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using e2rc.Models.Common;
namespace e2rc.Models
{
    public class LocationModel:AddressModel 
    {
        [Required(ErrorMessage = "Project Name is Required."), StringLength(150,ErrorMessage="Name must be less than 150 characters.")]        
        public string Name { get; set; }

        [Required(ErrorMessage = "Project Name is Required.")]
        public long? Location_ID { get; set; }

         [Required(ErrorMessage = "Project Type is Required.")]
        public long ProjectType_ID { get; set; }
        
        public long? CreatedBy_ID { get; set; }

        public long? User_ID { get; set; }
        public bool IsActive { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public string Date { get; set; }

         [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ? ModifiedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DueDate { get; set; }
        public int days { get; set; }

        [Required(ErrorMessage = "Company Name is Required.")]
        public long? Client_ID { get; set; }

        [Display(Name = "Company Name"),
       Required(AllowEmptyStrings = false, ErrorMessage = "Select Custome Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Customer Name"),
        Required(AllowEmptyStrings = false, ErrorMessage = "Select Custome Name")]
        public string CustomerName { get; set; }


       
        [Required]
        [Display(Name = "Tracking Number"), Remote("IsTrackingNumberAvailable", "Location", AdditionalFields = "Location_ID", ErrorMessage = "NPDES Tracking Number unavailable.")]
        public string TrackingNumber { get; set; }

        public List<long> lstInspector_ID { get; set; }
        public List<long> lstReviewer_ID { get; set; }

        /*this Property for view seleted id on drop down */
        //public int[] lInspector_ID { get; set; }
       // public int[] lReviewer_ID { get; set; }
        public string lInspector_ID { get; set; }
        public string lReviewer_ID { get; set; }

       
        public Int64 F1_ID { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "F2(Station Based) is required.")]
        public string F2_ID { get; set; }

        public string ProjectType { get; set; }


        public Int64 Item_ID { get; set; }

        public char LMR { get; set; }

        [Display(Name = "Email"),
        Required(AllowEmptyStrings = false, ErrorMessage = "Enter Email 1 Address."),       
        MaxLength(150)]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Invalid Email Format.")]
        public string Email_1 { get; set; }

        [Display(Name = "Email"),
        Required(AllowEmptyStrings = false, ErrorMessage = "Enter Email 2 Address."),        
        MaxLength(150)]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Invalid Email Format.")]
        public string Email_2 { get; set; }

        [Display(Name = "Email"),
        Required(AllowEmptyStrings = false, ErrorMessage = "Enter Inspection Report Emails."),
        MaxLength(150)]
        //[RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Invalid Email Format.")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$", ErrorMessage = "Invalid Email Format.")]
        public string InspectionReportEmails { get; set; }

        [Display(Name = "Email"),
        Required(AllowEmptyStrings = false, ErrorMessage = "Enter Work Orders Emails."),
        MaxLength(150)]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$", ErrorMessage = "Invalid Email Format.")]
        public string WorkOrdersEmails { get; set; }

        [Display(Name = "Email"),
        Required(AllowEmptyStrings = false, ErrorMessage = "Enter Three Day Notice Emails."),
        MaxLength(150)]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$", ErrorMessage = "Invalid Email Format.")]
        public string Threedaynoticeemails { get; set; }

        [Display(Name = "Email"),
        Required(AllowEmptyStrings = false, ErrorMessage = "Enter Five Day Notice Emails."),
        MaxLength(150)]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$", ErrorMessage = "Invalid Email Format.")]
        public string Fivedaynoticeemails { get; set; }

        [Display(Name = "Email"),
       Required(AllowEmptyStrings = false, ErrorMessage = "Enter Seven Day Notice Emails."),
       MaxLength(150)]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$", ErrorMessage = "Invalid Email Format.")]
        public string Sevendaynoticeemails { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Maintainance/Corrective Action is Required.")]
        public string MaintainAction { get; set; }
        [Required(ErrorMessage = "Inspection Frequency is Required.")]
        public string InspectionFreq { get; set; }

        public Boolean IsRequired { get; set; }

        private List<SelectListItem> MaintenanceList = new List<SelectListItem>();
        public List<SelectListItem> Maintenance
        {
            get
            {
                MaintenanceList.Add(new SelectListItem { Text = "E2RC", Value = "E2RC" });
                MaintenanceList.Add(new SelectListItem { Text = "Third Party Vendor", Value = "Third Party Vendor" });
                return MaintenanceList;
            }
        }
        private List<SelectListItem> InspectionFrequencyList = new List<SelectListItem>();
        public List<SelectListItem> InspectionFrequency
        {
            get
            {
                InspectionFrequencyList.Add(new SelectListItem { Text = "7 Day Inspection", Value = "7 Day Inspection" });
                InspectionFrequencyList.Add(new SelectListItem { Text = "14 Day Inspection", Value = "14 Day Inspection" });
                return InspectionFrequencyList;
            }
        }

        public IEnumerable<ProjectTypeModel> ProjectTypes
        {
            get
            {
                return e2rc.Models.Repository.LocationRepository.ProjectType;
            }
        }

        private List<SelectListItem> LMRList = new List<SelectListItem>();
        public List<SelectListItem> LMRs
        {
            get
            {
                LMRList.Add(new SelectListItem { Text = "L", Value = "L" });
                LMRList.Add(new SelectListItem { Text = "M", Value = "M" });
                LMRList.Add(new SelectListItem { Text = "R", Value = "R" });                
                return LMRList;
            }
        }
       
    }
}