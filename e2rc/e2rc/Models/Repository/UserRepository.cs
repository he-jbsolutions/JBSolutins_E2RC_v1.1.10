using System;
using System.Web;
using System.Web.Security;
using e2rc.Models.Security;
using e2rcModel.BusinessLayer;
using Newtonsoft.Json;

namespace e2rc.Models.Repository
{
    public static class UserRepository
    {
        public static bool Authenticate(string UserName, string Password)
        {            
            User user = new User(UserName, Password);
            if (user.Authenticate())
            {
                return AuthorizeTicket(user);
            }
            return false;
        }

        public static string GetCompanyLogoImage(long UserID)
        {
            User user = new User();

            return (user.GetCompanyLogoInfo(UserID));
        }

        private static bool AuthorizeTicket(User user)
        {
            try
            {
                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.User_ID = user.User_ID;
                serializeModel.Name = user.Name;
                serializeModel.UserName = user.UserName;
                serializeModel.Role = user.Role.Description;
                serializeModel.Email = user.Email;
                serializeModel.LogoPath = user.LogoPath;

                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                         1,
                         user.Email,
                         DateTime.Now,
                         DateTime.Now.AddMinutes(525600),
                         false,
                         userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                HttpContext.Current.Response.Cookies.Add(faCookie);

                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.User_ID = serializeModel.User_ID;
                newUser.Name = serializeModel.Name;
                newUser.UserName = serializeModel.UserName;
                newUser.Role = serializeModel.Role;
                newUser.LogoPath = serializeModel.LogoPath;

                HttpContext.Current.User = newUser;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public static bool IsUserNameAvailabe(string UserName, long? User_ID = 0)
        {
            return new User().IsUserNameAvailable(UserName, User_ID);
        }
    }
}