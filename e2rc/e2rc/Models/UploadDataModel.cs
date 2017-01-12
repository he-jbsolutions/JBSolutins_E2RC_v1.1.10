using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace e2rc.Models
{
    public class UploadDataModel
    {
        public int ParentID { get; set; }

        public int ID { get; set; }
        private static int count = 0;
        public long Inspection_ID { get; set; }
        public ItemC1Model itemC1 { get; set; }
        public IEnumerable<ItemC1Model> C1items
        {
            get
            {
                return e2rc.Models.Repository.ItemC1Repository.items;
            }
        }

        public long UploadData_ID { get; set; }
        
        public bool ActionNA { get; set; }

        public string UploadImagePath { get; set; }
        public string ImageName { get; set; }
        public string Status { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }

        public string FileNameNew { get; set; }

        [Required(ErrorMessage = "image is required")]
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
        public ItemC2Model itemC2 { get; set; }
        public IEnumerable<ItemC2Model> C2items
        {
            get
            {
                return e2rc.Models.Repository.ItemC2Repository.items;
            }
        }
        public ItemC3Model itemC3 { get; set; }
        public IEnumerable<ItemC3Model> C3items
        {
            get
            {
                return e2rc.Models.Repository.ItemC3Repository.items;
            }
        }

        public string Station { get; set; }
        public string Location { get; set; }
        public int PPPLength { get; set; }

        public UOMModel UOM { get; set; }
        public IEnumerable<UOMModel> uoms
        {
            get
            {
                return e2rc.Models.Repository.UOMRepository.UOMs;
            }
        }

     
        public string LtRt { get; set; }
        public int ParentIndex { get; set; }
        public long ParentUploadData_ID { get; set; }

        public UploadDataModel()
        {
        }
        public bool SaveFile(string ControllerName)
        {
            if (ControllerName == "Inspection")
            {               
                UploadImagePath = PostedFile.FileName.ToString().Replace(' ','_');
                UploadImagePath = string.Concat(Path.GetFileNameWithoutExtension(UploadImagePath), DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), Path.GetExtension(UploadImagePath));
                PostedFile.SaveAs(HttpContext.Current.Server.MapPath("/Inspection/UploadedImage/" + UploadImagePath));               
                return true;
            }
            else if (ControllerName == "StationInspection")
            {
                UploadImagePath = PostedFile.FileName.ToString().Replace(' ', '_');
                UploadImagePath = string.Concat(Path.GetFileNameWithoutExtension(UploadImagePath), DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), Path.GetExtension(UploadImagePath));
                PostedFile.SaveAs(HttpContext.Current.Server.MapPath("/StationInspection/UploadedImage/" + UploadImagePath));
                return true;
            }
            else

                return false;
        }
    }
}