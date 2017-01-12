using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace e2rc.Models
{
    public class GeneralInspectionModel
    {       
        [Required(ErrorMessage = "Customer Name is Required.")]
        public ClientModel Client { get; set; }
        public IEnumerable<ClientModel> clients
        {
            get
            {
                return e2rc.Models.Repository.ClientRepository.Clients;
            }
        }
        public string ClientEmail { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }

        public string Inspector_Name { get; set; }

        public Int64 Inspector_ID { get; set; }       
        //[Required(ErrorMessage = "Not Assign any Location to this Inspector.")]  
        [Required(AllowEmptyStrings=false, ErrorMessage = "Select Project Name.")] 
        public Int64 Location_ID { get; set; }       
        public string Name { get; set; }

        [Required(ErrorMessage = "Project name is Required.")]
        public GeneralInspectionModel location { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Tracking No is Required.")]
        public string Tracking_No { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Location is Required.")]
        public string locationName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is Required."), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ModifiedDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Time")]
        [RegularExpression(@"^(0[0-9]|1[0-2]):[0-5][0-9]$", ErrorMessage = "Invalid Time.")]
        public String StartTime { get; set; }

       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Time")] 
        //[DisplayFormat(DataFormatString = "{##:##}")]       
        [RegularExpression(@"^(0[0-9]|1[0-2]):[0-5][0-9]$", ErrorMessage = "Invalid Time.")]
        public String EndTime { get; set; }

        [Required]
        public InspectorModel inspector { get; set; }
        public IEnumerable<InspectorModel> inspectors
        {
            get
            {
                return e2rc.Models.Repository.InspectorRepository.inspectors;
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Inspector's Title is Required.")]
        public string Inspector_Title { get; set; }

        [Required(ErrorMessage = "Contact Information is Required.")]
        public string Inspector_Contact { get; set; }

        [Required(ErrorMessage = "Qualification is Required.")]
        public string Inspector_Qualification { get; set; }
        public string InspectorEmail { get; set; }

        public bool PhaseClear { get; set; }
        public bool PhaseExcavations { get; set; }
        public bool PhaseBuilding { get; set; }
        public bool PhaseSitework { get; set; }
        public bool PhaseOther { get; set; }

        public bool PhaseRoughGrading { get; set; }
        public bool PhaseInfrastructure { get; set; }
        public bool PhaseFinalGrading { get; set; }
        public bool PhaseFinalStabilization { get; set; }
        public string PhaseValue { get; set; }

        public bool RainEvent { get; set; }
        public bool RainEventOther { get; set; }
        public string RainEventValue { get; set; }
        public string RainEventOthervalue { get; set; }

        [Required(ErrorMessage = "Inspection Type is Required.")]
        public string Inspection_Type { get; set; }
        private List<SelectListItem> InspectionType = new List<SelectListItem>();
        public List<SelectListItem> Inspections
        {
            get
            {
                InspectionType.Add(new SelectListItem { Text = "7 Day", Value = "7Day" });
                InspectionType.Add(new SelectListItem { Text = "14 Day", Value = "14Day" });
                InspectionType.Add(new SelectListItem { Text = "+ .25\" Rain Event", Value = "Rain Event" });
                return InspectionType;
            }
        }
         [Required(ErrorMessage = "Site Classifications is Required.")]
        public int CodeId { get; set; }
        public string Description { get; set; }
        public IEnumerable<SiteClassificationModel> siteClassifications
        {
            get
            {
                return e2rc.Models.Repository.SiteClassificationRepository.items;
            }
        }
        [Required(ErrorMessage = "Select Value.")]
        public string StartTime_AmPm { get; set; }

        [Required(ErrorMessage = "Select Value.")]
        public string EndTime_AmPm { get; set; }
        private List<SelectListItem> TimeAmPmList = new List<SelectListItem>();
        public List<SelectListItem> times
        {
            get
            {
                TimeAmPmList.Add(new SelectListItem { Text = "AM", Value = "AM" });
                TimeAmPmList.Add(new SelectListItem { Text = "PM", Value = "PM" });
                return TimeAmPmList;
            }
        }
        public bool IsComplete { get; set; }
        public string UploadSignPath { get; set; }

       
    }
}