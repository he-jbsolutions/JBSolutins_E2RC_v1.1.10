using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Web.Mvc;
using e2rcModel.BusinessLayer;
using e2rc.Models.Repository;

namespace e2rc.Models
{
    public class FranchiseAssignLocationToInspectorModel
    {
        public long Assign_ID { get; set; }
        public long User_ID { get; set; }
        public string User { get; set; }
        public long? Inspector_ID { get; set; }
        public List<long> lstInspector_ID { get; set; }
        public string InspectorName { get; set; }
        public long? Location_ID { get; set; }
        public List<long> lstLocation_ID { get; set; }
        public string LocationName { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        public LocationModel location { get; set; }
        public InspectorModel inspector { get; set; }

        public string sInspector_ID { get; set; }
        public string sLocation_ID { get; set; }
        public bool RemoveAccess { get; set; }

        public IEnumerable<dynamic> GetInspectorDetails(long User_ID)
        {
            return e2rc.Models.Repository.FranchiseAssignLocationToInspectorRepository.GetInspectorDetails(User_ID);
        }
        public IEnumerable<dynamic> FranchiseLocations(long User_ID)
        {
            return e2rc.Models.Repository.FranchiseAssignLocationToInspectorRepository.GetFranchiseLocations(User_ID);
        }

        public static long getReviewer_UserID(long User_ID)
        {
            return (long)e2rc.Models.Repository.ReviewerRepository.getReviewer_UserID((long)User_ID);
        }
    }
}