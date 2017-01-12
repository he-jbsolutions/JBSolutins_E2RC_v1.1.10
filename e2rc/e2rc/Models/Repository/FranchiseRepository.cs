using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;
using e2rcModel.DataAccessLayer;
using System.Drawing;

namespace e2rc.Models.Repository
{
    public static class FranchiseRepository
    {
        public static bool Create(FranchiseModel franchiseModel)
        {
            Franchise franchise = GetFranchise(franchiseModel);
            return franchise.Create();
        }

        public static bool LogoUpdate(FranchiseModel franchiseModel)
        {
            Franchise franchise = GetFranchiseLogoDetials(franchiseModel);
            return franchise.LogoUpdate();
        }

        public static bool checkIsImageValid(FranchiseModel franchiseModel)
        {
            if (franchiseModel.PostedFile != null)
            {
                Image img = Image.FromStream(franchiseModel.PostedFile.InputStream);
                if (img.Width > 600 || img.Height > 100)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public static IEnumerable<FranchiseModel> List(string view)
        {

            IEnumerable<Franchise> FranchiseList = new Franchise().List(view);

            if (FranchiseList != null)
            {
                List<FranchiseModel> FranchiseModelList = new List<FranchiseModel>();

                foreach (Franchise franchise in FranchiseList)
                {
                    FranchiseModelList.Add(GetFranchiseModel(franchise));
                }
                return FranchiseModelList;
            }
            return null;
        }
        public static int getRoleByLoginUser(string sUserName)
        {
            Franchise franchise = new Franchise();

            return (franchise.GetUserLoginRoleID(sUserName));
        }

        public static IEnumerable<FranchiseModel> List(string companyName,string view)
        {
            IEnumerable<Franchise> FranchiseList = new Franchise().List(companyName,view);

            if (FranchiseList != null)
            {
                List<FranchiseModel> FranchiseModelList = new List<FranchiseModel>();

                foreach (Franchise franchise in FranchiseList)
                {
                    FranchiseModelList.Add(GetFranchiseModel(franchise));
                }
                return FranchiseModelList;
            }
            return null;
        }

        public static List<string> SearchByName(string search)
        {
           var franchiseNames = new Franchise().AutoList(search);

           if (franchiseNames != null)
           {
               return franchiseNames.ToList();
           }
            return null;
        }
        private static Franchise GetFranchiseLogoDetials(FranchiseModel franchiseModel)
        {
            string sEditLogoPath = string.Empty;
            if (franchiseModel.FileName != string.Empty)
            {
                franchiseModel.SaveFranchiseLogo();

                string fileName = System.IO.Path.GetFileNameWithoutExtension(franchiseModel.FileName).ToString();

                string fileExt = System.IO.Path.GetExtension(franchiseModel.FileName).ToString();

                sEditLogoPath = "/FranchiseLogo/" + fileName + "_" + franchiseModel.FraCompName + fileExt;
                franchiseModel.slogoName = fileName + "_" + franchiseModel.FraCompName + fileExt;
            }
            franchiseModel.UploadLogoUrl = sEditLogoPath;

            return new Franchise
            {
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID,
                FraCompName = franchiseModel.FraCompName,
                IsActive = franchiseModel.IsActive,
                Date = franchiseModel.Date,
                Franchise_ID = franchiseModel.Franchise_ID,
                UploadLogoPath = sEditLogoPath
            };
        }

        private static Franchise GetFranchise(FranchiseModel franchiseModel)
        {
            string sEditLogoPath = string.Empty;
            if (franchiseModel.FileName != string.Empty)
            {
                franchiseModel.SaveFranchiseLogo();

                string fileName = System.IO.Path.GetFileNameWithoutExtension(franchiseModel.FileName).ToString();

                string fileExt = System.IO.Path.GetExtension(franchiseModel.FileName).ToString();

                sEditLogoPath = "/FranchiseLogo/" + fileName + "_" + franchiseModel.FraCompName + fileExt;
            }
            franchiseModel.UploadLogoUrl = sEditLogoPath;

            return new Franchise
            {
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID,
                FraCompName = franchiseModel.FraCompName,
                IsActive= franchiseModel.IsActive,
                Date = franchiseModel.Date,
                Franchise_ID = franchiseModel.Franchise_ID,
                UploadLogoPath = sEditLogoPath,
               
                Address = new Address
                {
                    City = franchiseModel.Address.City,
                    MailingAddress = franchiseModel.Address.MailingAddress,
                    State = new State
                    {
                        State_ID = franchiseModel.Address.State.State_ID,
                        Code = franchiseModel.Address.State.Code,
                        Name = franchiseModel.Address.State.Name
                    },
                    ZipCode = franchiseModel.Address.ZipCode
                },
                AdminUser = new User
                {
                    CompanyName = franchiseModel.FraCompName,
                    Email = franchiseModel.AdminUser.Email,

                    //name
                    FirstName = franchiseModel.AdminUser.FirstName,
                    LastName=franchiseModel.AdminUser.LastName,
                    Password = franchiseModel.AdminUser.Password,
                    MobileNumber = franchiseModel.AdminUser.MobileNumber,
                    PhoneNumber = franchiseModel.AdminUser.PhoneNumber,
                    UserName = franchiseModel.UserName,
                    Role = new Role
                    {
                        Role_ID = franchiseModel.AdminUser.Role.Role_ID,
                        Description = franchiseModel.AdminUser.Role.Description
                    },
                    CreatedBy_ID = franchiseModel.CreatedBy_ID,
                    User_ID = franchiseModel.AdminUser.User_ID ?? franchiseModel.User_ID
                }
            };
        }

        private static FranchiseModel GetFranchiseModel(Franchise franchise)
        {
            return new FranchiseModel
            {
                CreatedBy_ID = franchise.CreatedBy_ID,
                FraCompName = franchise.FraCompName,
                Date = franchise.Date,
                FranchiseStatus=franchise.FranchiseStatus,
                IsActive=franchise.IsActive,
                UploadLogoUrl = franchise.UploadLogoPath,
                Address = new AddressModel
                {
                    City = franchise.Address.City,
                    MailingAddress = franchise.Address.MailingAddress,
                    State = new StateModel
                    {
                        State_ID = franchise.Address.State.State_ID,
                        Code = franchise.Address.State.Code,
                        Name = franchise.Address.State.Name
                    },
                    ZipCode = franchise.Address.ZipCode
                },
                AdminUser = new UserModel
                {
                    CompanyName = franchise.FraCompName,
                    Email = franchise.AdminUser.Email,//name
                    FirstName = franchise.AdminUser.FirstName,
                    LastName=franchise.AdminUser.LastName,
                    Password = franchise.AdminUser.Password,
                    MobileNumber = franchise.AdminUser.MobileNumber,
                    PhoneNumber = franchise.AdminUser.PhoneNumber,
                    UserName = franchise.AdminUser.UserName,
                    Role = new RoleModel
                    {
                        Role_ID = franchise.AdminUser.Role.Role_ID,
                        Description = franchise.AdminUser.Role.Description
                    },
                    CreatedBy_ID = franchise.CreatedBy_ID,
                    User_ID = franchise.AdminUser.User_ID
                },
                UserName = franchise.AdminUser.UserName,
                Franchise_ID = franchise.Franchise_ID,
                User_ID = franchise.AdminUser.User_ID
            };
        }

        public static FranchiseModel Single(long Franchise_ID)
        {           
            Franchise franchise = new Franchise().Single(Franchise_ID);
            return GetFranchiseModel(franchise);
        }
       
        public static bool Edit(FranchiseModel franchiseModel)
        {
            Franchise franchise = GetFranchise(franchiseModel);
            return franchise.Edit();     
        }

        public static bool Delete(FranchiseModel franchsieModel)
        {
            return new Franchise
            {
                CreatedBy_ID = franchsieModel.CreatedBy_ID,
                IsActive = franchsieModel.IsActive,
                Franchise_ID = franchsieModel.Franchise_ID
            }.Delete();
        }

        public static IEnumerable<SubmissionModel> SubmissionList(long User_ID)
        {
            IEnumerable<Submission> SubmissionList = new Franchise().SubmissionList(User_ID);

            if (SubmissionList != null)
            {
                List<SubmissionModel> SubmissionModelList = new List<SubmissionModel>();

                foreach (Submission submission in SubmissionList)
                {
                    SubmissionModelList.Add(GetSubmissionModel(submission));
                }
                return SubmissionModelList;
            }
            return null;
        }
        public static IEnumerable<SubmissionModel> SubmissionList(string search,long User_ID)
        {
            IEnumerable<Submission> SubmissionList = new Franchise().SubmissionList(search,User_ID);

            if (SubmissionList != null)
            {
                List<SubmissionModel> SubmissionModelList = new List<SubmissionModel>();

                foreach (Submission submission in SubmissionList)
                {
                    SubmissionModelList.Add(GetSubmissionModel(submission));
                }
                return SubmissionModelList;
            }
            return null;
        }

        public static List<string> SearchByName(string search,long User_ID)
        {
            var ProjectNames = new Franchise().AutoList(search, User_ID);

            if (ProjectNames != null)
            {
                return ProjectNames.ToList();
            }
            return null;
        }

        private static SubmissionModel GetSubmissionModel(Submission submission)
        {
            return new SubmissionModel
            {
                FormName = submission.FormName,
                ClientName=submission.ClientName,
                InspectorName = submission.InspectorName,
                ProjectName = submission.ProjectName,
                location = submission.location,
                Date = submission.Date,
                Inspection_ID = submission.Inspection_ID,
                path=submission.path
            };

        }

        public static IEnumerable<FranchiseModel> sortFranchiseDetails(string Search,string sortOrder,string view)
        {
            IEnumerable<FranchiseModel> FranchiseList;
            if (!string.IsNullOrEmpty(Search))
            {
                FranchiseList = List(Search, view);
            }
            else
            {
                FranchiseList = List(view);
            }
            if (FranchiseList != null)
            {
                switch (sortOrder)
                {
                    // TrackingNo
                    case "CompanyName_desc":
                        FranchiseList = FranchiseList.OrderByDescending(m => m.FraCompName);
                        break;
                    case "CompanyName":
                        FranchiseList = FranchiseList.OrderBy(m => m.FraCompName);
                        break;
                    case "FirstName_desc":
                        FranchiseList = FranchiseList.OrderByDescending(m => m.AdminUser.FirstName);
                        break;
                    case "FirstName":
                        FranchiseList = FranchiseList.OrderBy(m => m.AdminUser.FirstName);
                        break;
                    case "LastName_desc":
                        FranchiseList = FranchiseList.OrderByDescending(m => m.AdminUser.LastName);
                        break;
                    case "LastName":
                        FranchiseList = FranchiseList.OrderBy(m => m.AdminUser.LastName);
                        break;
                    case "UserName_desc":
                        FranchiseList = FranchiseList.OrderByDescending(m => m.UserName);
                        break;
                    case "UserName":
                        FranchiseList = FranchiseList.OrderBy(m => m.UserName);
                        break;
                    case "Date":
                        FranchiseList = FranchiseList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        FranchiseList = FranchiseList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        FranchiseList = FranchiseList.OrderByDescending(m => m.Date);
                        break;
                }
                return FranchiseList;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<SubmissionModel> sortSubmissionList(long User_ID, string Search, string sortOrder)
        {
            IEnumerable<SubmissionModel> SubmissionmodelList;
            if (!string.IsNullOrEmpty(Search))
            {
                 SubmissionmodelList = SubmissionList(Search, User_ID);
            }
            else
            {
                SubmissionmodelList = SubmissionList(User_ID);
            }
            if (SubmissionmodelList != null)
            {
                switch (sortOrder)
                {
                    // TrackingNo               
                    case "FormName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.FormName);
                        break;
                    case "FormName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.FormName);
                        break;
                    case "ClientName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.ClientName);
                        break;
                    case "ClientName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.ClientName);
                        break;
                    case "ProjectName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.ProjectName);
                        break;
                    case "ProjectName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.ProjectName);
                        break;
                    case "location_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.location);
                        break;
                    case "location":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.location);
                        break;
                    case "InspectorName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.InspectorName);
                        break;
                    case "InspectorName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.InspectorName);
                        break;
                    case "Date":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.Date);
                        break;
                }
                return SubmissionmodelList;
            }

            else
            {
                return null;
            }
        }


        public static IEnumerable<SubmissionModel> sortFranchiseWiseSubmissionList(long User_ID, string Search, string sortOrder)
        {
            IEnumerable<SubmissionModel> SubmissionmodelList;
            if (!string.IsNullOrEmpty(Search))
            {
                SubmissionmodelList = FranchiseSubmissionList(Search,User_ID);
            }
            else
            {
                SubmissionmodelList = FranchiseSubmissionList(User_ID);
            }
            if (SubmissionmodelList != null)
            {
                switch (sortOrder)
                {
                    // TrackingNo               
                    case "FormName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.FormName);
                        break;
                    case "FormName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.FormName);
                        break;
                    case "ClientName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.ClientName);
                        break;
                    case "ClientName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.ClientName);
                        break;
                    case "ProjectName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.ProjectName);
                        break;
                    case "ProjectName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.ProjectName);
                        break;
                    case "location_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.location);
                        break;
                    case "location":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.location);
                        break;
                    case "InspectorName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.InspectorName);
                        break;
                    case "InspectorName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.InspectorName);
                        break;
                    case "Date":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.Date);
                        break;
                }
                return SubmissionmodelList;
            }

            else
            {
                return null;
            }
        }


        public static IEnumerable<SubmissionModel> FranchiseSubmissionList(long User_ID)
        {
            IEnumerable<Submission> SubmissionList = new Franchise().FranchiseSubmissionList(User_ID);

            if (SubmissionList != null)
            {
                List<SubmissionModel> SubmissionModelList = new List<SubmissionModel>();

                foreach (Submission submission in SubmissionList)
                {
                    SubmissionModelList.Add(GetFranchiseSubmissionModel(submission));
                }
                return SubmissionModelList;
            }
            return null;
        }
        public static IEnumerable<SubmissionModel> FranchiseSubmissionList(string search, long User_ID)
        {
            IEnumerable<Submission> SubmissionList = new Franchise().FranchiseSubmissionList(search, User_ID);

            if (SubmissionList != null)
            {
                List<SubmissionModel> SubmissionModelList = new List<SubmissionModel>();

                foreach (Submission submission in SubmissionList)
                {
                    SubmissionModelList.Add(GetSubmissionModel(submission));
                }
                return SubmissionModelList;
            }
            return null;
        }


        private static SubmissionModel GetFranchiseSubmissionModel(Submission submission)
        {
            return new SubmissionModel
            {
                FormName = submission.FormName,
                ClientName = submission.ClientName,
                InspectorName = submission.InspectorName,
                ProjectName = submission.ProjectName,
                location = submission.location,
                Date = submission.Date,
                Inspection_ID = submission.Inspection_ID,
                path = submission.path
            };

        }


        public static long? FranchiseID(long? User_ID)
        {
            return (long?)Franchise.FranchiseID(User_ID);
        }

        internal static bool UpdateFranchiseStatus(long Franchise_ID)
        {
            return new Franchise().UpdateFranchiseStatus(Franchise_ID);        
        }

        internal static bool DeActivateFranchiseStatus(long Franchise_ID)
        {
            return new Franchise().DeActivateFranchiseStatus(Franchise_ID);        
        }

        
    }
}