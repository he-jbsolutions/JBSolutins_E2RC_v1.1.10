using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models
{
    public class SiteInspectionModel
    {
        public long InspectionQuestion_ID { get; set; }
        public string Question { get; set; }     
        public bool AreaInspectedYes { get; set; }
        public bool AreaInspectedNo { get; set; }
        public bool AreaInspectedNA { get; set; }        
        public bool ActionRequiredYes { get; set; }
        public bool ActionRequiredNo { get; set; }
        public string Notes { get; set; }
        public long Inpection_ID { get; set; }
        public long SiteInspection_ID { get; set; }
             
        public SiteInspectionModel()
        {           
        }          
    }
}