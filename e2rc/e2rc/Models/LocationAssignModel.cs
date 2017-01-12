using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Web.Mvc;
using e2rcModel.BusinessLayer;
using e2rc.Models.Repository;

namespace e2rc.Models
{
    public class LocationAssignModel : UserModel
    {
        public long Assign_ID { get; set; }
        public long User_ID { get; set; }
        public string User { get; set; }
        public long? Inspector_ID { get; set; }
        public string InspectorName { get; set; }
        public long? Location_ID { get; set; }
        public string LocationName { get; set; }
         [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        public LocationModel location { get; set; }
        public IEnumerable<LocationAssignModel> Locations
        {
            get
            {
                return e2rc.Models.Repository.LocationAssignRepository.Locations;
            }
        }
        public IEnumerable<dynamic> PMLocations(long User_ID)
        {
            return e2rc.Models.Repository.LocationAssignRepository.PMLocations(User_ID);
        }
        public InspectorModel inspector { get; set; }
        public IEnumerable<LocationAssignModel> inspectors
        {
            get
            {
                return e2rc.Models.Repository.LocationAssignRepository.inspectors;
            }
        }

        public IEnumerable<dynamic> GetInspectorDetails(long User_ID)
        {
            return e2rc.Models.Repository.LocationAssignRepository.GetInspectorDetails(User_ID);
        }

    }
}