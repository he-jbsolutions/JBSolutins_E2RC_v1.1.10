using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace e2rc.Models
{
    public class RoleModel       
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is Required.")]
        [Display(Name = "Role")]
        public byte Role_ID { get; set; }

        public string Description { get; set; }
    }
}