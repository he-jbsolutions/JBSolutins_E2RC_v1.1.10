using System.Web;
using e2rcModel.BusinessLayer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using e2rc.Models.Common;
using System;

namespace e2rc.Models
{
    public class StationSubmissionModel
    {

        public long? Inspection_ID { get; set; }
        [Display(Name = "Form Name")]
        public string FormName { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Location")]
        public string location { get; set; }
        [Display(Name = "Inspector Name")]
        public string InspectorName { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        public bool IsComplete { get; set; }
        public bool Auto { get; set; }
    }
}