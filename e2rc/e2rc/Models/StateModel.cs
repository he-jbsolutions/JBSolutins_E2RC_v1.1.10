using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace e2rc.Models
{
    public class StateModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "State is Required.")]
        [Display(Name = "State")]
        public byte State_ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}