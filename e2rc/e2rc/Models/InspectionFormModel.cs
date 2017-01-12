using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace e2rc.Models
{
    public class InspectionFormModel
    {
        public long Form_ID { get; set; }

        public long CreatedBy_ID { get; set; }

        [Display(Name = "Form Name")]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }

        public string Path { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
    }
}