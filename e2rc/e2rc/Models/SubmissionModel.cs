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
    public class SubmissionModel
    {
        public long? Inspection_ID { get; set; }
        [Display(Name = "Form Name")]
        public string FormName { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name="Location")]
        public string location { get; set; }
        [Display(Name = "Inspector Name")]
        public string InspectorName { get; set; }
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ModifiedDate { get; set; }
        public bool IsComplete { get; set; }
        public bool Auto { get; set; }
        public string path { get; set; }
        public string WorkOrder { get; set; }
        public int Pastduedays { get; set; }
        public Int64 User_ID { get; set; } 
        
    }
}