using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using e2rc.Models.Common;
using System.IO;
using System.Web.Optimization;

namespace e2rc.Models
{
    public class FranchiseModel
    {     
        [DisplayName("Company Name"), Required(AllowEmptyStrings = false, ErrorMessage = "Company Name is Required.")]
        public string FraCompName { get; set; }
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is Required."), DisplayFormat(DataFormatString = "{0:d}")]
        //[RegularExpression(@"^\d{2}\/\d{2}\/\d{4}$", ErrorMessage = "Date format should be MM/DD/YYYY")]
        //[RegularExpression(@"^(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/]\d{4}$", ErrorMessage = "End Date should be in MM/dd/yyyy format")]
        public DateTime Date { get; set; }

        [Required]
        public UserModel AdminUser { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is Required.")]
        [Display(Name = "User Name"), Remote("IsUserNameAvailable", "Franchise", AdditionalFields = "User_ID", ErrorMessage = "Username Unavailable.")]
        public string UserName { get; set; }

        public Boolean FranchiseStatus { get; set; }

        [Required]
        public AddressModel Address { get; set; }

        public long? CreatedBy_ID { get; set; }

        public long? Franchise_ID { get; set; }

        public long? User_ID { get; set; }

        public bool IsActive { get; set; }

        public FranchiseModel()
        {
            this.Address = new AddressModel();
            this.AdminUser = new UserModel();
        }
        public string UploadLogoUrl { get; set; }
        public string slogoName { get; set; }


        [RegularExpression(@"^.*\.(png|PNG|JPE?G|jpe?g|bmp|BMP)$", ErrorMessage = "Please upload PNG,JPEG,BMP Image only.")]
        //[RegularExpression(@"^.*\.(png)$", ErrorMessage = "Please Upload PNG Image Only.")]
        public HttpPostedFileBase PostedFile { get; set; }

        public string FileName
        {
            get
            {
                if (PostedFile != null)
                    return PostedFile.FileName;
                else
                    return String.Empty;
            }
        }

        public bool SaveFranchiseLogo()
        {
           
            string strMappath = HttpContext.Current.Server.MapPath("~/FranchiseLogo/");

            if (!Directory.Exists(strMappath))
            {
                DirectoryInfo di = Directory.CreateDirectory(strMappath);
            }

            string fileName = System.IO.Path.GetFileNameWithoutExtension(PostedFile.FileName).ToString();

            string fileExt = System.IO.Path.GetExtension(PostedFile.FileName).ToString();

            PostedFile.SaveAs(HttpContext.Current.Server.MapPath("/FranchiseLogo/") + "//" + fileName + "_" + FraCompName + fileExt);

            return true;
        }
    }
}