using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public class DirectorRepository
    {
         public static bool Create(DirectorModel directorModel)
        {
            Director director = GetDirector(directorModel);
            return director.Create();
        }
         private static Director GetDirector(DirectorModel directorModel)
         {
             return new Director
             {
                 Director_ID = directorModel.Director_ID,
                 IsActive=directorModel.IsActive,
                 FirstName = directorModel.FirstName,
                 LastName = directorModel.LastName,               
                 Date = directorModel.Date,
                 UserName = directorModel.UserName,
                 Password = directorModel.Password,
                 MobileNumber = directorModel.MobileNumber,
                 PhoneNumber = directorModel.PhoneNumber,
                 Email = directorModel.Email,
                 Role = new Role
                 {
                     Role_ID = directorModel.Role.Role_ID,
                     Description = directorModel.Role.Description
                 },
                 Address = new Address
                 {
                     City = directorModel.Address.City,
                     MailingAddress = directorModel.Address.MailingAddress,
                     State = new State
                     {
                         State_ID = directorModel.Address.State.State_ID,
                         Code = directorModel.Address.State.Code,
                         Name = directorModel.Address.State.Name
                     },
                     ZipCode = directorModel.Address.ZipCode
                 },
                 CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID
             };
         }

         public static IEnumerable<DirectorModel> List(string DirectorName, long User_ID,string view)
         {
             IEnumerable<Director> DirectorList = new Director().List(DirectorName, (long)User_ID,view);

             if (DirectorList != null)
             {
                 List<DirectorModel> DirectorModelList = new List<DirectorModel>();

                 foreach (Director director in DirectorList)
                 {
                     DirectorModelList.Add(GetDirectiveModel(director));
                 }
                 return DirectorModelList;
             }
             return null;
         }

         public static IEnumerable<DirectorModel> List(long User_ID,string view)
         {
             IEnumerable<Director> DirectorList = (IEnumerable<Director>)new Director().List((long)User_ID,view);
             if (DirectorList != null)
             {
                 List<DirectorModel> DirectorListModelList = new List<DirectorModel>();

                 foreach (Director Director in DirectorList)
                 {
                     DirectorListModelList.Add(GetDirectiveModel(Director));
                 }
                 return DirectorListModelList;
             }
             return null;
         }

         private static DirectorModel GetDirectiveModel(Director director)
         {
             return new DirectorModel
             {
                 Director_ID = director.Director_ID,
                 IsActive=director.IsActive,
                 //  Name = client.Name,
                 FirstName = director.FirstName,
                 LastName = director.LastName,
                 Date = director.Date,
                 UserName = director.UserName,
                 Password = director.Password,
                 ConfirmPassword = director.Password,
                 MobileNumber = director.MobileNumber,
                 PhoneNumber = director.PhoneNumber,
                 Email = director.Email,
                 User_ID = director.User_ID,
                 Role = new RoleModel
                 {
                     Role_ID = director.Role.Role_ID,
                     Description = director.Role.Description
                 },
                 Address = new AddressModel
                 {
                     City = director.Address.City,
                     MailingAddress = director.Address.MailingAddress,
                     State = new StateModel
                     {
                         State_ID = director.Address.State.State_ID,
                         Code = director.Address.State.Code,
                         Name = director.Address.State.Name
                     },
                     ZipCode = director.Address.ZipCode
                 },
                 CreatedBy_ID = ((e2rc.Models.Security.CustomPrincipal)HttpContext.Current.User).User_ID
             };
         }

         public static IEnumerable<DirectorModel> sortDirectorDetails(long User_ID, string Search, string sortOrder,string view)
         {
             IEnumerable<DirectorModel> DirectorList;
             if (!string.IsNullOrEmpty(Search))
             {
                 DirectorList = List(Search, User_ID,view);
             }
             else
             {
                 DirectorList = List(User_ID,view);
             }
             if (DirectorList != null)
             {
                 switch (sortOrder)
                 {

                     case "FirstName_desc":
                         DirectorList = DirectorList.OrderByDescending(m => m.FirstName);
                         break;
                     case "FirstName":
                         DirectorList = DirectorList.OrderBy(m => m.FirstName);
                         break;
                     case "LastName_desc":
                         DirectorList = DirectorList.OrderByDescending(m => m.LastName);
                         break;
                     case "LastName":
                         DirectorList = DirectorList.OrderBy(m => m.LastName);
                         break;
                     case "UserName_desc":
                         DirectorList = DirectorList.OrderByDescending(m => m.UserName);
                         break;
                     case "UserName":
                         DirectorList = DirectorList.OrderBy(m => m.UserName);
                         break;
                     case "Email":
                         DirectorList = DirectorList.OrderBy(m => m.Email);
                         break;
                     case "Email_desc":
                         DirectorList = DirectorList.OrderByDescending(m => m.Email);
                         break;
                     case "Date":
                         DirectorList = DirectorList.OrderBy(m => m.Date);
                         break;
                     case "Date_desc":
                         DirectorList = DirectorList.OrderByDescending(m => m.Date);
                         break;
                     default:
                         DirectorList = DirectorList.OrderByDescending(m => m.Date);
                         break;
                 }
                 return DirectorList;
             }
             else
             {
                 return null;
             }
         }

         public static DirectorModel Single(long? Director_ID, long User_ID)
         {
             Director director = new Director().Single((long)Director_ID, User_ID);
             return GetDirectiveModel(director);
         }
         public static bool Edit(DirectorModel DirectorModel)
         {
             Director Director = GetDirector(DirectorModel);
             return Director.Edit();
         }
         public static bool Delete(DirectorModel DirectorModel)
         {
             return new Director
             {
                 Director_ID = DirectorModel.Director_ID,
                 CreatedBy_ID = DirectorModel.CreatedBy_ID
             }.Delete();
         }


         public static List<string> SearchByName(string search, long User_ID)
         {
             var dirctorNames = new Director().AutoList(search.Trim(), User_ID);

             if (dirctorNames != null)
             {
                 return dirctorNames.ToList();
             }
             return null;
         }

         internal static bool UpdateDirectorStatus(long Director_ID, string view)
         {
             return new Director().UpdateDirectorStatus(Director_ID,view);
         }

         public static long? DirectorID(long? User_ID)
         {
             return (long?)Director.DirectorID(User_ID);
         }
         public static long getDirector_UserID(long User_ID)
         {
             return (long)Director.getDirector_UserID(User_ID);
         }

    }
}