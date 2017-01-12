using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using e2rcModel.BusinessLayer;
using e2rc.Models.Repository;

namespace e2rc.Models
{
    public class ReviewerModel : UserModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is required.")]
        public RoleModel Role { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is require."), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public long Reviewer_ID { get; set; }
        public long Client_ID { get; set; }
        public long Location_ID { get; set; }
        public string ReviewerTitle { get; set; }
        public List<long> Client_IDs { get; set; }
        public long? User_ID { get; set; }
        [Required]
        public AddressModel Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsAllowToCloseWorkOrder { get; set; }
        public IEnumerable<RoleModel> Roles
        {
            get
            {
                e2rc.Models.Repository.RoleRepository.Role_Type = "Reviewer";
                return e2rc.Models.Repository.RoleRepository.Roles;
            }
        }
        public string slstLocationID { get; set; }
        public string hfSelectedClients { get; set; }
        public string[] selectedClientIDs { get; set; }

        public ReviewerModel()
        {

        }

        public IEnumerable<dynamic> ReviewerClientDetails(long User_ID)
        {
            return ReviewerRepository.GetReviewerClientDetails(User_ID);
        }

    }
}