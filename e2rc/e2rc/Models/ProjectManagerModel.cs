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
    public class ProjectManagerModel : UserModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is required.")]
        public RoleModel Role { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is require."), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public long ProjectManager_ID { get; set; }
        public long Location_ID { get; set; }
        public LocationModel location { get; set; }
        public List<long> LocationIDs { get; set; }
        public long? User_ID { get; set; }
        [Required]
        public AddressModel Address { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<RoleModel> Roles
        {
            get
            {
                e2rc.Models.Repository.RoleRepository.Role_Type = "Project Manager";
                return e2rc.Models.Repository.RoleRepository.Roles;
            }
        }
        public string hfSelectedLocations { get; set; }
        public string[] selectedLocationIDs { get; set; }
     
        public ProjectManagerModel()
        {

        }

        public IEnumerable<dynamic> PMLocations(long User_ID)
        {
            return ProjectManagerRepository.GetPMLocationDetails(User_ID);
        }


    }
}