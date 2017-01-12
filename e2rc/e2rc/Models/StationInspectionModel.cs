using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Web.Mvc;
using e2rcModel.BusinessLayer;
using e2rc.Models.Repository;

namespace e2rc.Models
{
    public class StationInspectionModel
    {

        public long CreatedBy { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerTitle { get; set; }
        //[DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ReviewerOpenDateTime { get; set; }
        public string FormName { get; set; }
        public long Inspection_ID { get; set; }
        public GeneralInspectionModel generalInspectionModel { get; set; }
        public WeatherInspectionModel weatherInspectionModel { get; set; }

        public SiteInspectionModel SiteInspection1 { get; set; }
        public SiteInspectionModel SiteInspection2 { get; set; }
        public SiteInspectionModel SiteInspection3 { get; set; }
        public SiteInspectionModel SiteInspection4 { get; set; }
        public SiteInspectionModel SiteInspection5 { get; set; }
        public SiteInspectionModel SiteInspection6 { get; set; }
        public SiteInspectionModel SiteInspection7 { get; set; }
        public SiteInspectionModel SiteInspection8 { get; set; }
        public SiteInspectionModel SiteInspection9 { get; set; }
        public SiteInspectionModel SiteInspection10 { get; set; }
        public SiteInspectionModel SiteInspection11 { get; set; }
        public SiteInspectionModel SiteInspection12 { get; set; }
        public SiteInspectionModel SiteInspection13 { get; set; }
        public SiteInspectionModel SiteInspection14 { get; set; }
        public SiteInspectionModel SiteInspection15 { get; set; }
        public SiteInspectionModel SiteInspection16 { get; set; }
        public SiteInspectionModel SiteInspection17 { get; set; }
       


        public StationInspectionModel()
        {
            
            this.weatherInspectionModel = new WeatherInspectionModel();        

            int count = 1;
            foreach (var item in SiteInspectionRepository.List())
            {
                switch (count)
                {
                    case 1: this.SiteInspection1 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 2: this.SiteInspection2 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 3: this.SiteInspection3 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 4: this.SiteInspection4 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 5: this.SiteInspection5 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 6: this.SiteInspection6 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 7: this.SiteInspection7 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 8: this.SiteInspection8 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 9: this.SiteInspection9 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 10: this.SiteInspection10 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedNo = true,
                        Notes = string.Empty
                    };
                        break;
                    case 11: this.SiteInspection11 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedNo = true,
                        Notes = string.Empty
                    };
                        break;
                    case 12: this.SiteInspection12 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 13: this.SiteInspection13 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 14: this.SiteInspection14 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 15: this.SiteInspection15 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedNo = true,
                        Notes = string.Empty
                    };
                        break;
                    case 16: this.SiteInspection16 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;
                    case 17: this.SiteInspection17 = new SiteInspectionModel
                    {
                        InspectionQuestion_ID = item.InspectionQuestion_ID,
                        Question = item.Question,
                        ActionRequiredNo = true,
                        AreaInspectedYes = true,
                        Notes = string.Empty
                    };
                        break;                    
                }
                count++;
            }
        }
        public DateTime Date { get; set; }


        public IEnumerable<dynamic> Locations(long User_ID)
        {
            return StationInspectionRepository.Locations(User_ID);
        }
        public IEnumerable<dynamic> GetInspectorsClientDetails(long Inspector_ID)
        {
            return new Inspection().GetInspectorsClientDetails(Inspector_ID);
        }
    }
}