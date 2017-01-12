using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public static class ClientRepository
    {
        public static IEnumerable<ClientModel> Clients
        {
            get
            {
                List<ClientModel> clients = new List<ClientModel>();
                foreach (var client in new Client().clients)
                {
                    clients.Add(new ClientModel { Client_ID = client.Client_ID, Name = client.Name });
                }
                return clients;
            }
        }

        public static bool Create(ClientModel clientModel)
        {
            Client client = GetClient(clientModel);
            return client.Create();
        }

        public static IEnumerable<ClientModel> List(long User_ID,string view)
        {
            IEnumerable<Client> clientList = (IEnumerable<Client>)new Client().List((long)User_ID,view);
            if (clientList != null)
            {
                List<ClientModel> ClientModelList = new List<ClientModel>();

                foreach (Client client in clientList)
                {
                    ClientModelList.Add(GetClientModel(client));
                }
                return ClientModelList;
            }
            return null;
        }

        public static IEnumerable<ClientModel> List(string clientName, long User_ID, string view)
        {
            IEnumerable<Client> ClientList = new Client().List(clientName, (long)User_ID,view);

            if (ClientList != null)
            {
                List<ClientModel> ClientModelList = new List<ClientModel>();

                foreach (Client client in ClientList)
                {
                    ClientModelList.Add(GetClientModel(client));
                }
                return ClientModelList;
            }
            return null;
        }

        public static List<string> SearchByName(string search, long User_ID)
        {
            var ClientNames = new Client().AutoList(search, User_ID);

            if (ClientNames != null)
            {
                return ClientNames.ToList();
            }
            return null;
        }
        public static List<string> SearchByProjectName(string search, long User_ID)
        {
            var ProjectNames = new Client().ProjectAutoList(search, User_ID);

            if (ProjectNames != null)
            {
                return ProjectNames.ToList();
            }
            return null;
        }

        public static ClientModel Single(long? Client_ID, long User_ID)
        {
            Client client = new Client().Single((long)Client_ID, User_ID);
            return GetClientModel(client);
        }
        public static bool Edit(ClientModel clientModel)
        {
            Client client = GetClient(clientModel);
            return client.Edit();
        }

        public static bool Delete(ClientModel clientModel)
        {
            return new Client
            {
                Client_ID = clientModel.Client_ID,
                CreatedBy_ID = clientModel.CreatedBy_ID
            }.Delete();
        }

        private static ClientModel GetClientModel(Client client)
        {
            return new ClientModel
            {
                Client_ID = client.Client_ID,
                IsActive=client.IsActive,
                CompanyName=client.CompanyName,
              //  Name = client.Name,
                FirstName=client.FirstName,
                LastName=client.LastName,
                Date = client.Date,
                UserName = client.UserName,
                Password = client.Password,
                ConfirmPassword = client.Password,
                MobileNumber = client.MobileNumber,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                User_ID = client.User_ID,
                LogoPath = client.LogoPath,
                EditLogoPath = client.EditLogoPath,
                Role = new RoleModel
                {
                    Role_ID = client.Role.Role_ID,
                    Description = client.Role.Description
                },
                Address = new AddressModel
                {
                    City = client.Address.City,
                    MailingAddress = client.Address.MailingAddress,
                    State = new StateModel
                    {
                        State_ID = client.Address.State.State_ID,
                        Code = client.Address.State.Code,
                        Name = client.Address.State.Name
                    },
                    ZipCode = client.Address.ZipCode
                },
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID
            };
        }

        private static Client GetClient(ClientModel clientModel)
        {
            return new Client
            {
                Client_ID = clientModel.Client_ID,
                FirstName=clientModel.FirstName,
                LastName=clientModel.LastName,
                IsActive=clientModel.IsActive,
                //Name = clientModel.Name,
                Date = clientModel.Date,
                UserName = clientModel.UserName,
                Password = clientModel.Password,
                MobileNumber = clientModel.MobileNumber,
                PhoneNumber = clientModel.PhoneNumber,
                Email = clientModel.Email,
                Role = new Role
                {
                    Role_ID = clientModel.Role.Role_ID,
                    Description = clientModel.Role.Description
                },
                Address = new Address
                {
                    City = clientModel.Address.City,
                    MailingAddress = clientModel.Address.MailingAddress,
                    State = new State
                    {
                        State_ID = clientModel.Address.State.State_ID,
                        Code = clientModel.Address.State.Code,
                        Name = clientModel.Address.State.Name
                    },
                    ZipCode = clientModel.Address.ZipCode
                },
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID
            };
        }

        public static IEnumerable<SubmissionModel> SubmissionList(long User_ID)
        {
            IEnumerable<Submission> SubmissionList = new Client().SubmissionList(User_ID);

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
            IEnumerable<Submission> SubmissionList = new Client().SubmissionList(Search,User_ID);

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
                ProjectName = submission.ProjectName,
                location = submission.location,
                Date = submission.Date,
                Inspection_ID = submission.Inspection_ID,
                path=submission.path

            };

        }

        public static IEnumerable<dynamic> GetClientDetails(long Client_ID, long User_ID)
        {
            return new Client().ClientDetails(Client_ID,User_ID);
        }

        public static IEnumerable<ClientModel> sortClientDetails(long User_ID, string Search, string sortOrder,string view)
        {
            IEnumerable<ClientModel> ClientList;
            if (Search != "")
            {
                ClientList = List(Search, User_ID,view);
            }
            else
            {
                ClientList = List(User_ID,view);
            }
            if (ClientList != null)
            {
                switch (sortOrder)
                {
                    case "CompanyName_desc":
                        ClientList = ClientList.OrderByDescending(m => m.CompanyName);
                        break;
                    case "CompanyName":
                        ClientList = ClientList.OrderBy(m => m.CompanyName);
                        break;
                    case "FirstName_desc":
                        ClientList = ClientList.OrderByDescending(m => m.FirstName);
                        break;
                    case "FirstName":
                        ClientList = ClientList.OrderBy(m => m.FirstName);
                        break;
                    case "LastName_desc":
                        ClientList = ClientList.OrderByDescending(m => m.LastName);
                        break;
                    case "LastName":
                        ClientList = ClientList.OrderBy(m => m.LastName);
                        break;
                    case "UserName_desc":
                        ClientList = ClientList.OrderByDescending(m => m.UserName);
                        break;
                    case "UserName":
                        ClientList = ClientList.OrderBy(m => m.UserName);
                        break;
                    case "Email":
                        ClientList = ClientList.OrderBy(m => m.Email);
                        break;
                    case "Email_desc":
                        ClientList = ClientList.OrderByDescending(m => m.Email);
                        break;
                    case "Date":
                        ClientList = ClientList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        ClientList = ClientList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        ClientList = ClientList.OrderByDescending(m => m.Date);
                        break;
                }
                return ClientList;
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
        public static long? ClientID(long? User_ID)
        {
            return (long?)Client.ClientID(User_ID);
        }
        public static long getClient_UserID(long User_ID)
        {
            return (long)Client.getClient_UserID(User_ID);
        }
        internal static bool UpdateClientStatus(long Client_ID )
        {
            return new Client().UpdateClientStatus(Client_ID);
        }
        internal static bool ActivateClientStatus(long Franchise_ID)
        {
            return new Client().ActivateClientStatus(Franchise_ID);
        }


        public static IEnumerable<ClientModel> ActiveClientList(long User_ID)
        {
            IEnumerable<Client> clientList = (IEnumerable<Client>)new Client().ActiveClientList((long)User_ID);
            if (clientList != null)
            {
                List<ClientModel> ClientModelList = new List<ClientModel>();

                foreach (Client client in clientList)
                {
                    ClientModelList.Add(GetClientModel(client));
                }
                return ClientModelList;
            }
            return null;
        }

        public static IEnumerable<ClientModel> ActiveClientList(string clientName, long User_ID)
        {
            IEnumerable<Client> ClientList = new Client().ActiveClientList(clientName, (long)User_ID);

            if (ClientList != null)
            {
                List<ClientModel> ClientModelList = new List<ClientModel>();

                foreach (Client client in ClientList)
                {
                    ClientModelList.Add(GetClientModel(client));
                }
                return ClientModelList;
            }
            return null;
        }


        public static IEnumerable<ClientModel> sortActiveClientList(long User_ID, string Search, string sortOrder)
        {
            IEnumerable<ClientModel> ClientList;
            if (!string.IsNullOrEmpty(Search))
            {
                ClientList = ActiveClientList(Search, User_ID);
            }
            else
            {
                ClientList = ActiveClientList(User_ID);
            }
            if (ClientList != null)
            {
                switch (sortOrder)
                {

                    case "CompanyName_desc":
                        ClientList = ClientList.OrderByDescending(m => m.CompanyName);
                        break;
                    case "CompanyName":
                        ClientList = ClientList.OrderBy(m => m.CompanyName);
                        break;                    
                    case "UserName_desc":
                        ClientList = ClientList.OrderByDescending(m => m.UserName);
                        break;
                    case "UserName":
                        ClientList = ClientList.OrderBy(m => m.UserName);
                        break;
                    case "Email":
                        ClientList = ClientList.OrderBy(m => m.Email);
                        break;
                    case "Email_desc":
                        ClientList = ClientList.OrderByDescending(m => m.Email);
                        break;                    
                    default:
                        ClientList = ClientList.OrderBy(m => m.CompanyName);
                        break;
                }
                return ClientList;
            }
            else
            {
                return null;
            }
        }

    }
}