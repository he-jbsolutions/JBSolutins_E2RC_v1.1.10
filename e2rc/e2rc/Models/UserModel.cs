using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace e2rc.Models
{
    public class UserModel
    {
        [Display(Name = "Name"), Required(AllowEmptyStrings = false, ErrorMessage = "Name is Required.")]
        [RegularExpression(@"^[0-9a-zA-Z ]*$", ErrorMessage = "Invalid name.")]
        public string Name { get; set; }

        [Display(Name = "First Name"), Required(AllowEmptyStrings = false, ErrorMessage = "First Name is Required.")]
        //[RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name"), Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is Required.")]
        //[RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid name.")]
        public string LastName { get; set; }

        [Display(Name = "Username"), Remote("IsUserNameAvailable", "Base",AdditionalFields = "User_ID", ErrorMessage = "Username Unavailable."),
        Required(AllowEmptyStrings = false, ErrorMessage = "Username is Required.")]
        public string UserName { get; set; }

        [Display(Name = "Password"), Required(AllowEmptyStrings = false, ErrorMessage = "Password is Required."),
        DataType(DataType.Password), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[^\w])).+$",ErrorMessage="password must be 8 characters long, have one uppercase, one lowercase and one special character and one number")]
        [MinLength(8, ErrorMessage = "Password must be 8 character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required.")]
        [DataType(DataType.Password),Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Company Name"),
        Required(AllowEmptyStrings = false, ErrorMessage = "Company Name is Required.")]
        public string CompanyName { get; set; }

        [Display(Name = "Email"),
        Required(AllowEmptyStrings = false, ErrorMessage = "Enter Email Address."),
        DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email"),
        MaxLength(150)]        
        public string Email { get; set; }

        [Display(Name = "Mobile Phone"), Required(AllowEmptyStrings = false, ErrorMessage = "Mobile No is Required."),
        RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$", ErrorMessage = "Invalid Mobile Number"),MaxLength(12)]

        public string MobileNumber { get; set; }

        [Display(Name = "Office Phone"), Required(AllowEmptyStrings = false, ErrorMessage = "Phone No is Required."),
      //  DisplayFormat(DataFormatString = "{0:###-###-####}"),
        RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$",ErrorMessage="Invalid Phone Number"),MaxLength(12)]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is Required.")]
        public RoleModel Role { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Qualification is Required.")]
        //[RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid name.")]
        public string Qualification { get; set; }

       
        public long? CreatedBy_ID { get; set; }

        public long? User_ID { get; set; }

        public List<long> lstLocation_ID { get; set; }
       

        public IEnumerable<RoleModel> Roles
        {
            get
            {
                e2rc.Models.Repository.RoleRepository.Role_Type = "Franchise Admin";
                return e2rc.Models.Repository.RoleRepository.Roles;
            }
        }
    }
}