using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using e2rc.Models.Repository;

namespace e2rc.Models
{
    public class DirectorModel : UserModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is Required.")]
        public RoleModel Role { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is Required."), DisplayFormat(DataFormatString = "{0:d}")]
        //[RegularExpression(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", ErrorMessage = "Enter date in mm/dd/yyyy format")]
        //[RegularExpression(@"^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$", ErrorMessage = "Enter date in mm/dd/yyyy format")]     

        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public AddressModel Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer Name is Required.")]
        public long? Director_ID { get; set; }

        public long? User_ID { get; set; }

        public IEnumerable<RoleModel> Roles
        {
            get
            {
                e2rc.Models.Repository.RoleRepository.Role_Type = "Owner";
                return e2rc.Models.Repository.RoleRepository.Roles;
            }
        }
    }
}