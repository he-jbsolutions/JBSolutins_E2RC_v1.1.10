using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace e2rc.Models
{
    public class WeatherInspectionModel
    {
        //tblWeatherInspection table
        public bool StromEventYes { get; set; }
        public bool StromEventNo { get; set; }
        public string StromEvent { get; set; }
        public string StromEventYesValue { get; set; }
        public long Inspection_ID { get; set; }
        
        [Required(ErrorMessage = "Weather time is required.")]
        public string Weather_Time { get; set; }
        private List<SelectListItem> WeatherTime = new List<SelectListItem>();

        public StormDetailsModel StormDetailsListOne { get; set; }
        public StormDetailsModel StormDetailsListTwo { get; set; }
        public StormDetailsModel StormDetailsListThree { get; set; }
        public StormDetailsModel StormDetailsListFour { get; set; } 

        public List<SelectListItem> WeatherTimes
        {
            get
            {
                WeatherTime.Add(new SelectListItem { Text = "Clear", Value = "Clear" });
                WeatherTime.Add(new SelectListItem { Text = "Cloudy", Value = "Cloudy" });
                WeatherTime.Add(new SelectListItem { Text = "Rain", Value = "Rain" });
                WeatherTime.Add(new SelectListItem { Text = "Sleet", Value = "Sleet" });
                WeatherTime.Add(new SelectListItem { Text = "Fog", Value = "Fog" });
                WeatherTime.Add(new SelectListItem { Text = "Snowing", Value = "Snowing" });
                WeatherTime.Add(new SelectListItem { Text = "High Winds", Value = "High Winds" });
                return WeatherTime;
            }
        }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "temperature is required.")]
        [RegularExpression(@"^[-+]?[0-9]*\.?[0-9]*([eE][-+]?[0-9]+)?$", ErrorMessage = "Invalid temperature")]
        public float Temperature { get; set; }
        public bool LastInspectionYes { get; set; }
        public bool LastInspectionNo { get; set; }
        public bool LastInspection { get; set; }
        public bool InspectionOccuringYes { get; set; }
        public bool InspectionOccuringNo { get; set; }
        public string InspectionOccuring { get; set; }
        public string InspectionOccuringYesValue { get; set; }
        public bool UnsafeInspectionYes { get; set; }
        public bool UnsafeInspectionNo { get; set; }
        public string UnsafeInspection { get; set; }
        public string UnsafeInspectionValue { get; set; }

        public List<UploadDataModel> UploadDataModelList { get; set; }
        public List<StormDetailsModel> StormDetailsModelList { get; set; }

        public List<UploadDataModel> UploadDataModelEditList { get; set; }
        public List<StormDetailsModel> StormDetailsModelEditList { get; set; }
        public UploadDataModel UploadData1 { get; set; }
       
        
        public WeatherInspectionModel()
        {
            UploadDataModelList = new List<UploadDataModel>();
            UploadDataModelEditList = new List<UploadDataModel>();
            StormDetailsModelList = new List<StormDetailsModel>();
            StormDetailsModelEditList = new List<StormDetailsModel>();
        }
    }
}