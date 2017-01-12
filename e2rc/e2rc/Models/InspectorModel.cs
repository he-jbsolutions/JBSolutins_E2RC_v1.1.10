using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.IO;

namespace e2rc.Models
{
    public class InspectorModel:UserModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is Required.")]
        public RoleModel Role { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is Required."), DisplayFormat(DataFormatString = "{0:d}")]
        //[RegularExpression(@"^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$", ErrorMessage = "Date format should be mm/dd/yyyy")]  
         //[RegularExpression(@"^(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/]\d{4}$", ErrorMessage = "End Date should be in MM/dd/yyyy format")]
        //[RegularExpression(@"^\d{2}\/\d{2}\/\d{4}$", ErrorMessage = "Date format should be mm/dd/yyyy")]  
        
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Name is Required.")]
        public long? Inspector_ID { get; set; }

        public long? Client_ID { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Inspector Title is Required.")]
        public string InspectorTitle { get; set; }

        public bool IsActive { get; set; }

        public string slstLocationID { get; set; }

        public long? User_ID { get; set; }

        [Required]
        public AddressModel Address { get; set; }    

        public IEnumerable<RoleModel> Roles
        {
            get
            {
                e2rc.Models.Repository.RoleRepository.Role_Type = "Inspector";
                return e2rc.Models.Repository.RoleRepository.Roles;
            }
        }
        public string UploadSignPath { get; set; }
        public string SignPath { get; set; }
        public string EditSignPath { get; set; }
        //[Required(ErrorMessage = "Signature Image Required.")]
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

        public InspectorModel()
        {
        }

        public bool SaveSign()
        {

            EditSignPath = PostedFile.FileName;
            PostedFile.SaveAs(HttpContext.Current.Server.MapPath("/Inspection/Signature/") + "//" + PostedFile.FileName);
            return true;
        }

        public bool SaveClientLogo()
        {

            EditSignPath = PostedFile.FileName;

            string strMappath = HttpContext.Current.Server.MapPath("~/Client/Logo/");
            
            if (!Directory.Exists(strMappath))
            {
                DirectoryInfo di = Directory.CreateDirectory(strMappath);
            }
     
            string fileName = System.IO.Path.GetFileNameWithoutExtension(PostedFile.FileName).ToString();

            string fileExt = System.IO.Path.GetExtension(PostedFile.FileName).ToString();

            PostedFile.SaveAs(HttpContext.Current.Server.MapPath("/Client/Logo/") + "//" + fileName + "_" + CompanyName + fileExt);
            return true;
        }
        public bool SaveFile()
        {          
            UploadSignPath = PostedFile.FileName;
            PostedFile.SaveAs(HttpContext.Current.Server.MapPath("/Inspection/Signature/") + "//" + PostedFile.FileName);
            return true;
        }
    }
}