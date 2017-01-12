using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Web.Mvc;
using e2rcModel.BusinessLayer;
using e2rc.Models.Repository;

namespace e2rc.Models
{
    public class FranchiseAssignLocationToClientModel
    {

        public long Assign_ID { get; set; }
        public long User_ID { get; set; }
        public string User { get; set; }
        public long? Client_ID { get; set; }
        public long? Reviewer_ID { get; set; }
        public string ReviewerName { get; set; }   
        public string CompanyName { get; set; }
        public long? Location_ID { get; set; }
        public string LocationName { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        public LocationModel location { get; set; }
        public InspectorModel inspector { get; set; }

        public List<long> lstReviewer_ID { get; set; }
        public List<long> lstLocation_ID { get; set; }
        public bool RemoveAccess { get; set; }

       
        public IEnumerable<dynamic> GetReviewerDetails(long User_ID)
        {
            return e2rc.Models.Repository.FranchiseAssignLocationToClientRepository.GetReviewerDetails(User_ID);
        }
        public IEnumerable<dynamic> GetReviewerClients(long User_ID, long Reviewer_ID)
        {
            return e2rc.Models.Repository.FranchiseAssignLocationToClientRepository.GetReviewerClients(User_ID, Reviewer_ID);
        }
        public IEnumerable<dynamic> ReviewerLocations(long User_ID,long Client_ID)
        {
            return e2rc.Models.Repository.FranchiseAssignLocationToClientRepository.GetReviewerClientsLocation(User_ID, Client_ID);
        }
    }
}