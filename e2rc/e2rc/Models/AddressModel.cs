using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;
using System.ComponentModel.DataAnnotations;
namespace e2rc.Models
{
    public class AddressModel
    {
        //[Display(Name = "City"), Required(AllowEmptyStrings = false, ErrorMessage = "City is required.",RegularExpression(@"^[0-9a-zA-Z ]+$]")
        [Display(Name = "City"), Required(AllowEmptyStrings = false, ErrorMessage = "City is Required.")]
        [RegularExpression(@"^([a-zA-Z]+\s)*[a-zA-Z]+$", ErrorMessage = "Invalid City")]
        public string City { get; set; }

        [Required]
        public StateModel State { get; set; }

        [Display(Name = "Mailing Address"), Required(AllowEmptyStrings = false, ErrorMessage = "Address is Required.")]
        public string MailingAddress { get; set; }

        public string MailingAddress2 { get; set; }

        [Display(Name = "Zip Code"), Required(AllowEmptyStrings = false, ErrorMessage = "Zip Code is Required.")]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Invalid Zip Code")]
        //[RegularExpression(@"^[\s\S]{0,5}$", ErrorMessage = "Zip code must be 5 digits.")]
        [Range(00001, 99999, ErrorMessage = "Invalid Zip Code")]
        public string ZipCode { get; set; }

        public IEnumerable<StateModel> States
         {
             get
             {
                 return e2rc.Models.Repository.StateRepository.States;
             }
         }
    }
}