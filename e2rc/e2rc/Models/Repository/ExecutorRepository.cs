using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public class ExecutorRepository
    {

        public static bool Create(ExecutorModel ExecutorModel)
        {
            Executor Executor = GetExecutor(ExecutorModel);
            return Executor.Create();
        }
        private static Executor GetExecutor(ExecutorModel ExecutorModel)
        {
            return new Executor
            {
                Executor_ID = ExecutorModel.Executor_ID,
                IsActive=ExecutorModel.IsActive,
                FirstName = ExecutorModel.FirstName,
                LastName = ExecutorModel.LastName,
                Date = ExecutorModel.Date,
                UserName = ExecutorModel.UserName,
                Password = ExecutorModel.Password,
                MobileNumber = ExecutorModel.MobileNumber,
                PhoneNumber = ExecutorModel.PhoneNumber,
                Email = ExecutorModel.Email,
                Role = new Role
                {
                    Role_ID = ExecutorModel.Role.Role_ID,
                    Description = ExecutorModel.Role.Description
                },
                Address = new Address
                {
                    City = ExecutorModel.Address.City,
                    MailingAddress = ExecutorModel.Address.MailingAddress,
                    State = new State
                    {
                        State_ID = ExecutorModel.Address.State.State_ID,
                        Code = ExecutorModel.Address.State.Code,
                        Name = ExecutorModel.Address.State.Name
                    },
                    ZipCode = ExecutorModel.Address.ZipCode
                },
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID
            };
        }

        public static IEnumerable<ExecutorModel> List(string ExecutorName, long User_ID,string view)
        {
            IEnumerable<Executor> ExecutorList = new Executor().List(ExecutorName, (long)User_ID, view);

            if (ExecutorList != null)
            {
                List<ExecutorModel> ExecutorModelList = new List<ExecutorModel>();

                foreach (Executor Executor in ExecutorList)
                {
                    ExecutorModelList.Add(GetExecutorModel(Executor));
                }
                return ExecutorModelList;
            }
            return null;
        }

        public static IEnumerable<ExecutorModel> List(long User_ID,string view)
        {
            IEnumerable<Executor> ExecutorList = (IEnumerable<Executor>)new Executor().List((long)User_ID,view);
            if (ExecutorList != null)
            {
                List<ExecutorModel> ExecutorModelList = new List<ExecutorModel>();

                foreach (Executor Executor in ExecutorList)
                {
                    ExecutorModelList.Add(GetExecutorModel(Executor));
                }
                return ExecutorModelList;
            }
            return null;
        }

        private static ExecutorModel GetExecutorModel(Executor Executor)
        {
            return new ExecutorModel
            {
                Executor_ID = Executor.Executor_ID,  
                IsActive=Executor.IsActive,
                FirstName = Executor.FirstName,
                LastName = Executor.LastName,
                Date = Executor.Date,
                UserName = Executor.UserName,
                Password = Executor.Password,
                ConfirmPassword = Executor.Password,
                MobileNumber = Executor.MobileNumber,
                PhoneNumber = Executor.PhoneNumber,
                Email = Executor.Email,
                User_ID = Executor.User_ID,
                Role = new RoleModel
                {
                    Role_ID = Executor.Role.Role_ID,
                    Description = Executor.Role.Description
                },
                Address = new AddressModel
                {
                    City = Executor.Address.City,
                    MailingAddress = Executor.Address.MailingAddress,
                    State = new StateModel
                    {
                        State_ID = Executor.Address.State.State_ID,
                        Code = Executor.Address.State.Code,
                        Name = Executor.Address.State.Name
                    },
                    ZipCode = Executor.Address.ZipCode
                },
                CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID
            };
        }

        public static ExecutorModel Single(long? Executor_ID, long User_ID)
        {
            Executor Executor = new Executor().Single((long)Executor_ID, User_ID);
            return GetExecutorModel(Executor);
        }

        public static IEnumerable<ExecutorModel> sortExecutorDetails(long User_ID, string Search, string sortOrder,string view)
        {
            IEnumerable<ExecutorModel> ExecutorList;
            if (!string.IsNullOrEmpty(Search))
            {
                ExecutorList = List(Search, User_ID,view);
            }
            else
            {
                ExecutorList = List(User_ID,view);
            }
            if (ExecutorList != null)
            {
                switch (sortOrder)
                {
                    case "FirstName_desc":
                        ExecutorList = ExecutorList.OrderByDescending(m => m.FirstName);
                        break;
                    case "FirstName":
                        ExecutorList = ExecutorList.OrderBy(m => m.FirstName);
                        break;
                    case "LastName_desc":
                        ExecutorList = ExecutorList.OrderByDescending(m => m.LastName);
                        break;
                    case "LastName":
                        ExecutorList = ExecutorList.OrderBy(m => m.LastName);
                        break;
                    case "UserName_desc":
                        ExecutorList = ExecutorList.OrderByDescending(m => m.UserName);
                        break;
                    case "UserName":
                        ExecutorList = ExecutorList.OrderBy(m => m.UserName);
                        break;
                    case "Email":
                        ExecutorList = ExecutorList.OrderBy(m => m.Email);
                        break;
                    case "Email_desc":
                        ExecutorList = ExecutorList.OrderByDescending(m => m.Email);
                        break;
                    case "Date":
                        ExecutorList = ExecutorList.OrderBy(m => m.Date);
                        break;
                    case "Date_desc":
                        ExecutorList = ExecutorList.OrderByDescending(m => m.Date);
                        break;
                    default:
                        ExecutorList = ExecutorList.OrderByDescending(m => m.Date);
                        break;
                }
                return ExecutorList;
            }
            else
            {
                return null;
            }
        }

        public static bool Edit(ExecutorModel ExecutorModel)
        {
            Executor Executor = GetExecutor(ExecutorModel);
            return Executor.Edit();
        }

        public static bool Delete(ExecutorModel ExecutorModel)
        {
            return new Executor
            {
                Executor_ID = ExecutorModel.Executor_ID,
                CreatedBy_ID = ExecutorModel.CreatedBy_ID
            }.Delete();
        }

        internal static bool UpdateExecutorStatus(long Executor_ID, string view)
        {
            return new Executor().UpdateExecutorStatus(Executor_ID, view);
        }

        public static List<string> SearchByName(string search, long User_ID)
        {
            var ExecutorNames = new Executor().AutoList(search, User_ID);

            if (ExecutorNames != null)
            {
                return ExecutorNames.ToList();
            }
            return null;
        }

        public static long? ExecutorID(long? User_ID)
        {
            return (long?)Executor.ExecutorID(User_ID);
        }
        public static long getExecutor_UserID(long User_ID)
        {
            return (long)Executor.getExecutor_UserID(User_ID);
        }
    }
}