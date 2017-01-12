using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public static class InspectorRepository
    {
        public static IEnumerable<InspectorModel> inspectors
        {
            get
            {
                List<InspectorModel> inspectors = new List<InspectorModel>();
                foreach (var inspector in new Inspector().Inspectors)
                {
                    inspectors.Add(new InspectorModel { Inspector_ID=inspector.Inspector_ID , Name = inspector.Name });
                }
                return inspectors;
            }
        }

        public static bool Create(InspectorModel inspectorModel)
        {
            if (inspectorModel.Role.Role_ID == 3 || inspectorModel.Role.Role_ID == 4)
            {
                
                Inspector inspector = GetInspector(inspectorModel);
                Client c = new Client();
                c.CompanyName = inspector.CompanyName;
                c.Client_ID = inspector.Client_ID;
                c.FirstName = inspector.FirstName;
                c.LastName=inspector.LastName;           
                c.Date = inspector.Date;               
                c.UserName = inspector.UserName;
                c. Password = inspector.Password;
                c. MobileNumber = inspector.MobileNumber;
                c.PhoneNumber = inspector.PhoneNumber;
                c.Email = inspector.Email;
                c.IsActive = inspector.IsActive;
                c.Role=new Role{               
                 Role_ID = inspector.Role.Role_ID,
                 Description = inspector.Role.Description,
                };
                c.Address = new Address               
                {
                    City = inspector.Address.City,
                    ZipCode = inspector.Address.ZipCode,
                    MailingAddress = inspector.Address.MailingAddress,
                };
               c.Address.State = new State {
                State_ID = inspector.Address.State.State_ID,
                Code = inspector.Address.State.Code,
                Name = inspector.Address.State.Name,
               };
                c.CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID;
                if (inspectorModel.FileName != string.Empty)
                {
                    inspectorModel.SaveClientLogo();

                    string fileName = System.IO.Path.GetFileNameWithoutExtension(inspectorModel.FileName).ToString();
                    string fileExt = System.IO.Path.GetExtension(inspectorModel.FileName).ToString();

                    c.EditLogoPath = "/Client/Logo/" + fileName + "_" + inspector.CompanyName + fileExt;
                }

                return c.Create();                
            }
            else  
            {
                if (inspectorModel.FileName != string.Empty)
                {
                    inspectorModel.SaveFile();
                    inspectorModel.UploadSignPath = "/Inspection/Signature/" + inspectorModel.FileName;
                }
                Inspector inspector = GetInspector(inspectorModel);
                return inspector.Create();
            }
           
        }

        

        public static IEnumerable<InspectorModel> List(long User_ID,string view)
        {
            IEnumerable<Inspector> InspectorList = (IEnumerable<Inspector>)new Inspector().List((long)User_ID,view);
            if (InspectorList != null)
            {
                List<InspectorModel> InspectorModelList = new List<InspectorModel>();

                foreach (Inspector inspector in InspectorList)
                {
                    InspectorModelList.Add(GetInspectorModel(inspector));
                }
                return InspectorModelList;
            }
            return null;
        }

        public static List<string> SearchByName(string search, long User_ID)
        {
            var InspectorNames = new Inspector().AutoList(search, User_ID);

            if (InspectorNames != null)
            {
                return InspectorNames.ToList();
            }
            return null;
        }
        public static List<string> SearchByProjectName(string search, long User_ID)
        {
            var LocationNames = new Inspector().LocationAutoList(search, User_ID);

            if (LocationNames != null)
            {
                return LocationNames.ToList();
            }
            return null;
        }

        public static List<string> SearchByProjectNameStationBased(string search, long User_ID)
        {
            var LocationNames = new Inspector().LocationAutoListStationBased(search, User_ID);

            if (LocationNames != null)
            {
                return LocationNames.ToList();
            }
            return null;
        }

        public static List<string> SearchByProjectNameEdit(string search, long User_ID)
        {
            var LocationNames = new Inspector().LocationAutoCompleteList(search, User_ID);

            if (LocationNames != null)
            {
                return LocationNames.ToList();
            }
            return null;
        }

        public static List<string> SearchByProjectNameForStationBased(string search, long User_ID)
        {
            var LocationNames = new Inspector().LocationAutoCompleteListStationBased(search, User_ID);

            if (LocationNames != null)
            {
                return LocationNames.ToList();
            }
            return null;
        }


        public static IEnumerable<InspectorModel> List(string inspectorName, long User_ID,string view)
        {
            IEnumerable<Inspector> InspectorList = new Inspector().List(inspectorName, User_ID, view);

            if (InspectorList != null)
            {
                List<InspectorModel> InspectorModelList = new List<InspectorModel>();

                foreach (Inspector inspector in InspectorList)
                {
                    InspectorModelList.Add(GetInspectorModel(inspector));
                }
                return InspectorModelList;
            }
            return null;
        }

        public static InspectorModel Single(long? Inspector_ID, long User_ID)
        {
            Inspector inspector = new Inspector().Single((long)Inspector_ID,User_ID);
            return GetInspectorModel(inspector);
        }

        public static bool Edit(InspectorModel inspectorModel)
        {
            if (inspectorModel.Role.Role_ID == 3 || inspectorModel.Role.Role_ID == 4)
            {
                Inspector inspector = GetInspector(inspectorModel);
                Client c = new Client();
                c.Client_ID = inspectorModel.Client_ID;
                c.CompanyName = inspectorModel.CompanyName;
                c.IsActive = inspectorModel.IsActive;
                c.FirstName = inspector.FirstName;
                c.LastName = inspector.LastName;
                c.Date = inspector.Date;
                c.UserName = inspector.UserName;
                c.Password = inspector.Password;
                c.MobileNumber = inspector.MobileNumber;
                c.PhoneNumber = inspector.PhoneNumber;
                c.Email = inspector.Email;
                c.Role = new Role
                {
                    Role_ID = inspector.Role.Role_ID,
                    Description = inspector.Role.Description,
                };
                c.Address = new Address
                {
                    City = inspector.Address.City,
                    ZipCode = inspector.Address.ZipCode,
                    MailingAddress = inspector.Address.MailingAddress,
                };
                c.Address.State = new State
                {
                    State_ID = inspector.Address.State.State_ID,
                    Code = inspector.Address.State.Code,
                    Name = inspector.Address.State.Name,
                };
                c.CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID;

                if (inspectorModel.FileName != string.Empty)
                {

                    inspectorModel.SaveClientLogo();

                    string fileName = System.IO.Path.GetFileNameWithoutExtension(inspectorModel.FileName).ToString();

                    string fileExt = System.IO.Path.GetExtension(inspectorModel.FileName).ToString();

                    c.EditLogoPath = "/Client/Logo/" + fileName + "_" + inspector.CompanyName + fileExt;

                }

                return c.Edit();
            }
            else
            {

                if (inspectorModel.FileName != string.Empty)
                {

                    inspectorModel.SaveSign();
                    inspectorModel.EditSignPath = "/Inspection/Signature/" + inspectorModel.FileName;

                }
                Inspector inspector = GetInspector(inspectorModel);
                inspectorModel.slstLocationID = inspector.sLocationID;
                return inspector.Edit();
            }
        }

        public static bool Delete(InspectorModel inspectorModel)
        {
            return new Inspector
            {
                Inspector_ID = inspectorModel.Inspector_ID,
                CreatedBy_ID = inspectorModel.CreatedBy_ID
            }.Delete();
        }

        private static Inspector GetInspector(InspectorModel inspectorModel)
        {
            return new Inspector
            {
                Inspector_ID = inspectorModel.Inspector_ID,
                InspectorTitle = inspectorModel.InspectorTitle,
                Client_ID = inspectorModel.Client_ID,
                CompanyName = inspectorModel.CompanyName,
                //Name = inspectorModel.Name,
                FirstName = inspectorModel.FirstName,
                LastName = inspectorModel.LastName,
                Date = inspectorModel.Date,
                UserName = inspectorModel.UserName,
                Password = inspectorModel.Password,
                MobileNumber = inspectorModel.MobileNumber,
                PhoneNumber = inspectorModel.PhoneNumber,
                Email = inspectorModel.Email,
                IsActive = inspectorModel.IsActive,
                sLocationID = inspectorModel.lstLocation_ID != null ? string.Join(",", inspectorModel.lstLocation_ID) : string.Empty,
                Qualification = inspectorModel.Qualification,
                UploadSignPath = ((inspectorModel.EditSignPath) == null ? inspectorModel.UploadSignPath : inspectorModel.EditSignPath),
                //UploadSignPath=inspectorModel.UploadSignPath,
                Role = new Role
                {
                    Role_ID = inspectorModel.Role.Role_ID,
                    Description = inspectorModel.Role.Description
                },
                Address = new Address
                {
                    City = inspectorModel.Address.City,
                    MailingAddress = inspectorModel.Address.MailingAddress,
                    State = new State
                    {
                        State_ID = inspectorModel.Address.State.State_ID,
                        Code = inspectorModel.Address.State.Code,
                        Name = inspectorModel.Address.State.Name
                    },
                    ZipCode = inspectorModel.Address.ZipCode
                },
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID
            };
        }

        private static InspectorModel GetInspectorModel(Inspector inspector)
        {
            return new InspectorModel
            {
                Inspector_ID = inspector.Inspector_ID,
                InspectorTitle = inspector.InspectorTitle,
                //Name = inspector.Name,
                FirstName = inspector.FirstName,
                LastName = inspector.LastName,
                Date = inspector.Date,
                UserName = inspector.UserName,
                Password = inspector.Password,
                ConfirmPassword = inspector.Password,
                MobileNumber = inspector.MobileNumber,
                PhoneNumber = inspector.PhoneNumber,
                Qualification = inspector.Qualification,
                UploadSignPath = inspector.UploadSignPath,
                SignPath = inspector.SignPath,
                Email = inspector.Email,
                User_ID = inspector.User_ID,
                IsActive = inspector.IsActive,
                slstLocationID = inspector.sLocationID, 
                Role = new RoleModel
                {
                    Role_ID = inspector.Role.Role_ID,
                    Description = inspector.Role.Description
                },
                Address = new AddressModel
                {
                    City = inspector.Address.City,
                    MailingAddress = inspector.Address.MailingAddress,
                    State = new StateModel
                    {
                        State_ID = inspector.Address.State.State_ID,
                        Code = inspector.Address.State.Code,
                        Name = inspector.Address.State.Name
                    },
                    ZipCode = inspector.Address.ZipCode
                },
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID
            };
        }

        public static IEnumerable<SubmissionModel> SubmissionList(long User_ID)
        {
            IEnumerable<Submission> SubmissionList = new Inspector().SubmissionList(User_ID);

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

        public static IEnumerable<SubmissionModel> SubmissionList(string Search,long User_ID)
        {
            IEnumerable<Submission> SubmissionList = new Inspector().SubmissionList(Search,User_ID);

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
       
        public static IEnumerable<SubmissionModel> CompleteList(long User_ID)
        {
            IEnumerable<Submission> SubmissionList = new Inspector().CompleteList(User_ID);

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

        public static IEnumerable<SubmissionModel> CompleteList(string Search,long User_ID)
        {
            IEnumerable<Submission> SubmissionList = new Inspector().CompleteList(Search,User_ID);

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

        private static SubmissionModel GetSubmissionModel(Submission submission)
        {
            return new SubmissionModel
            {
                FormName = submission.FormName,
                InspectorName = submission.InspectorName,
                ClientName=submission.ClientName,
                CompanyName=submission.CompanyName,
                ProjectName = submission.ProjectName,
                location = submission.location,
                Date = submission.Date,
                Inspection_ID = submission.Inspection_ID,
                IsComplete=submission.IsComplete,
                Auto=submission.IsAutoresponder,
                path=submission.path
            };
        }

        private static StationSubmissionModel GetStationSubmissionModel(Submission submission)
        {
            return new StationSubmissionModel
            {
                FormName = submission.FormName,
                InspectorName = submission.InspectorName,
                ProjectName = submission.ProjectName,
                location = submission.location,
                Date = submission.Date,
                Inspection_ID = submission.Inspection_ID,
                IsComplete = submission.IsComplete,
                Auto = submission.IsAutoresponder
            };
        }

        //for Station Inspection Form 

        public static IEnumerable<StationSubmissionModel> StationSubmissionList(long User_ID)
        {
            IEnumerable<Submission> StatioSubmissionList = new Inspector().StationSubmissionList(User_ID);

            if (StatioSubmissionList != null)
            {
                List<StationSubmissionModel> StationSubmissionModelList = new List<StationSubmissionModel>();

                foreach (Submission submission in StatioSubmissionList)
                {
                    StationSubmissionModelList.Add(GetStationSubmissionModel(submission));
                }
                return StationSubmissionModelList;
            }
            return null;
        }

        public static IEnumerable<StationSubmissionModel> StationSubmissionList(string Search, long User_ID)
        {
            IEnumerable<Submission> StationSubmissionList = new Inspector().StationSubmissionList(Search, User_ID);

            if (StationSubmissionList != null)
            {
                List<StationSubmissionModel> StatioSubmissionModelList = new List<StationSubmissionModel>();

                foreach (Submission submission in StationSubmissionList)
                {
                    StatioSubmissionModelList.Add(GetStationSubmissionModel(submission));
                }
                return StatioSubmissionModelList;
            }
            return null;
        }

        public static IEnumerable<StationSubmissionModel> StationCompleteList(long User_ID)
        {
            IEnumerable<Submission> StationSubmissionList = new Inspector().StationCompleteList(User_ID);

            if (StationSubmissionList != null)
            {
                List<StationSubmissionModel> StationSubmissionModelList = new List<StationSubmissionModel>();

                foreach (Submission submission in StationSubmissionList)
                {
                    StationSubmissionModelList.Add(GetStationSubmissionModel(submission));
                }
                return StationSubmissionModelList;
            }
            return null;
        }

        public static IEnumerable<StationSubmissionModel> StationCompleteList(string Search, long User_ID)
        {
            IEnumerable<Submission> StationSubmissionList = new Inspector().StationCompleteList(Search, User_ID);

            if (StationSubmissionList != null)
            {
                List<StationSubmissionModel> StationSubmissionModelList = new List<StationSubmissionModel>();

                foreach (Submission submission in StationSubmissionList)
                {
                    StationSubmissionModelList.Add(GetStationSubmissionModel(submission));
                }
                return StationSubmissionModelList;
            }
            return null;
        }

        public static IEnumerable<InspectorModel> sortInspetorDetails(long User_ID ,string Search, string sortOrder,string view)
        {
            IEnumerable<InspectorModel> InspectorList;
            if (!string.IsNullOrEmpty(Search))
            {
                InspectorList = List(Search, User_ID, view);
            }
            else
            {
                InspectorList = List(User_ID, view);
            }
            if (InspectorList != null)
            {
                switch (sortOrder)
                {
                    // TrackingNo               
                    case "FirstName_desc":
                        InspectorList = InspectorList.OrderByDescending(m => m.FirstName);
                        break;
                    case "FirstName":
                        InspectorList = InspectorList.OrderBy(m => m.FirstName);
                        break;
                    case "LastName_desc":
                        InspectorList = InspectorList.OrderByDescending(m => m.LastName);
                        break;
                    case "LastName":
                        InspectorList = InspectorList.OrderBy(m => m.LastName);
                        break;
                    case "UserName_desc":
                        InspectorList = InspectorList.OrderByDescending(m => m.UserName);
                        break;
                    case "Email":
                        InspectorList = InspectorList.OrderBy(m => m.Email);
                        break;
                    case "Email_desc":
                        InspectorList = InspectorList.OrderByDescending(m => m.Email);
                        break;
                    case "UserName":
                        InspectorList = InspectorList.OrderBy(m => m.UserName);
                        break;
                    case "Date":
                        InspectorList = InspectorList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        InspectorList = InspectorList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        InspectorList = InspectorList.OrderByDescending(m => m.Date);
                        break;
                }
                return InspectorList;
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
                    case "CompanyName_desc":
                        SubmissionmodelList = SubmissionmodelList.OrderByDescending(m => m.CompanyName);
                        break;
                    case "CompanyName":
                        SubmissionmodelList = SubmissionmodelList.OrderBy(m => m.CompanyName);
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

        public static IEnumerable<StationSubmissionModel> sortStationSubmissionList(long User_ID, string Search, string sortOrder)
        {
            IEnumerable<StationSubmissionModel> StationSubmissionmodelList;
            if (Search != "")
            {
                StationSubmissionmodelList = StationSubmissionList(Search, User_ID);
            }
            else
            {
                StationSubmissionmodelList = StationSubmissionList(User_ID);
            }
            if (StationSubmissionmodelList != null)
            {
                switch (sortOrder)
                {
                    // TrackingNo               
                    case "FormName_desc":
                        StationSubmissionmodelList = StationSubmissionmodelList.OrderByDescending(m => m.FormName);
                        break;
                    case "FormName":
                        StationSubmissionmodelList = StationSubmissionmodelList.OrderBy(m => m.FormName);
                        break;
                    case "ProjectName_desc":
                        StationSubmissionmodelList = StationSubmissionmodelList.OrderByDescending(m => m.ProjectName);
                        break;
                    case "ProjectName":
                        StationSubmissionmodelList = StationSubmissionmodelList.OrderBy(m => m.ProjectName);
                        break;
                    case "location_desc":
                        StationSubmissionmodelList = StationSubmissionmodelList.OrderByDescending(m => m.location);
                        break;
                    case "location":
                        StationSubmissionmodelList = StationSubmissionmodelList.OrderBy(m => m.location);
                        break;
                    case "InspectorName_desc":
                        StationSubmissionmodelList = StationSubmissionmodelList.OrderByDescending(m => m.InspectorName);
                        break;
                    case "InspectorName":
                        StationSubmissionmodelList = StationSubmissionmodelList.OrderBy(m => m.InspectorName);
                        break;
                    case "Date":
                        StationSubmissionmodelList = StationSubmissionmodelList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        StationSubmissionmodelList = StationSubmissionmodelList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        StationSubmissionmodelList = StationSubmissionmodelList.OrderByDescending(m => m.Date);
                        break;
                }
                return StationSubmissionmodelList;
            }

            else
            {
                return null;
            }
        }

        public static IEnumerable<SubmissionModel> sortCompleteList(long User_ID, string Search, string sortOrder)
        {
            IEnumerable<SubmissionModel> SubmissionCompleteList;
            if (!string.IsNullOrEmpty(Search))
            {
                SubmissionCompleteList = CompleteList(Search, User_ID);
            }
            else
            {
                SubmissionCompleteList = CompleteList(User_ID);
            }
            if (SubmissionCompleteList != null)
            {
                switch (sortOrder)
                {
                    // TrackingNo               
                    case "FormName_desc":
                        SubmissionCompleteList = SubmissionCompleteList.OrderByDescending(m => m.FormName);
                        break;
                    case "FormName":
                        SubmissionCompleteList = SubmissionCompleteList.OrderBy(m => m.FormName);
                        break;
                    case "CompanyName_desc":
                        SubmissionCompleteList = SubmissionCompleteList.OrderByDescending(m => m.CompanyName);
                        break;
                    case "CompanyName":
                        SubmissionCompleteList = SubmissionCompleteList.OrderBy(m => m.CompanyName);
                        break;
                    case "ProjectName_desc":
                        SubmissionCompleteList = SubmissionCompleteList.OrderByDescending(m => m.ProjectName);
                        break;
                    case "ProjectName":
                        SubmissionCompleteList = SubmissionCompleteList.OrderBy(m => m.ProjectName);
                        break;
                    case "location_desc":
                        SubmissionCompleteList = SubmissionCompleteList.OrderByDescending(m => m.location);
                        break;
                    case "location":
                        SubmissionCompleteList = SubmissionCompleteList.OrderBy(m => m.location);
                        break;
                    case "InspectorName_desc":
                        SubmissionCompleteList = SubmissionCompleteList.OrderByDescending(m => m.InspectorName);
                        break;
                    case "InspectorName":
                        SubmissionCompleteList = SubmissionCompleteList.OrderBy(m => m.InspectorName);
                        break;
                    case "Date":
                        SubmissionCompleteList = SubmissionCompleteList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        SubmissionCompleteList = SubmissionCompleteList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        SubmissionCompleteList = SubmissionCompleteList.OrderByDescending(m => m.Date);
                        break;
                }
                return SubmissionCompleteList;
            }

            else
            {
                return null;
            }
        }

        public static IEnumerable<StationSubmissionModel> sortStationCompleteList(long User_ID, string Search, string sortOrder)
        {
            IEnumerable<StationSubmissionModel> StationSubmissionCompleteList;
            if (Search != "")
            {
                StationSubmissionCompleteList = StationCompleteList(Search, User_ID);
            }
            else
            {
                StationSubmissionCompleteList = StationCompleteList(User_ID);
            }
            if (StationSubmissionCompleteList != null)
            {
                switch (sortOrder)
                {
                    // TrackingNo               
                    case "FormName_desc":
                        StationSubmissionCompleteList = StationSubmissionCompleteList.OrderByDescending(m => m.FormName);
                        break;
                    case "FormName":
                        StationSubmissionCompleteList = StationSubmissionCompleteList.OrderBy(m => m.FormName);
                        break;
                    case "ProjectName_desc":
                        StationSubmissionCompleteList = StationSubmissionCompleteList.OrderByDescending(m => m.ProjectName);
                        break;
                    case "ProjectName":
                        StationSubmissionCompleteList = StationSubmissionCompleteList.OrderBy(m => m.ProjectName);
                        break;
                    case "location_desc":
                        StationSubmissionCompleteList = StationSubmissionCompleteList.OrderByDescending(m => m.location);
                        break;
                    case "location":
                        StationSubmissionCompleteList = StationSubmissionCompleteList.OrderBy(m => m.location);
                        break;
                    case "InspectorName_desc":
                        StationSubmissionCompleteList = StationSubmissionCompleteList.OrderByDescending(m => m.InspectorName);
                        break;
                    case "InspectorName":
                        StationSubmissionCompleteList = StationSubmissionCompleteList.OrderBy(m => m.InspectorName);
                        break;
                    case "Date":
                        StationSubmissionCompleteList = StationSubmissionCompleteList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        StationSubmissionCompleteList = StationSubmissionCompleteList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        StationSubmissionCompleteList = StationSubmissionCompleteList.OrderByDescending(m => m.Date);
                        break;
                }
                return StationSubmissionCompleteList;
            }

            else
            {
                return null;
            }
        }


        public static long? InspectorID(long? User_ID)
        {
            return (long?)Inspector.InspectorID(User_ID);
        }

        public static long getInspector_UserID(long User_ID)
        {
            return (long)Inspector.getInspector_UserID(User_ID);
        }

        internal static bool UpdateInspectorStatus(long Inspector_ID)
        {
            return new Inspector().UpdateInspectorStatus(Inspector_ID);
        }

        internal static bool DeActivateInspectorStatus(long Inspector_ID)
        {
            return new Inspector().DeActivateInspectorStatus(Inspector_ID);
        }
    }
}