using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using e2rc.Models;
using e2rc.Models.Repository;
using e2rcModel.BusinessLayer;
using System.Configuration;


namespace e2rc.Controllers
{
    public class AccountController : BaseController
    {       
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Session["CompanyLogoPath"] = null;
            LoginModel model = new LoginModel();
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies["Test" + FormsAuthentication.FormsCookieName.ToString()];
            ViewBag.ReturnUrl = returnUrl;
            if (authCookie != null)
            {
                try
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (authTicket != null & !authTicket.Expired)
                    {
                        model.UserName = Convert.ToString(authTicket.UserData).Split(':')[0];
                        model.Password = Convert.ToString(authTicket.UserData).Split(':')[1];
                        model.RememberMe = true;
                        return View(model);
                    }
                }
                catch (Exception)
                {
                }
            }
            return View();
        }

        public void SetCoookies(User user)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(525600), true, user.UserName + ":" + user.Password);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie faCookie1 = new HttpCookie("Test" + FormsAuthentication.FormsCookieName, encTicket);
            faCookie1.Expires = ticket.Expiration;
            faCookie1.Path = ticket.CookiePath;
            HttpContext.Response.Cookies.Add(faCookie1);
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && 
                UserRepository.Authenticate(model.UserName, model.Password))
            {
                if (model.RememberMe)
                {
                    User user = new User(model.UserName, model.Password);
                    SetCoookies(user);
                }

                string Path =  UserRepository.GetCompanyLogoImage(Convert.ToInt64(User.User_ID));
                if (!String.IsNullOrEmpty(Path))
                {
                    Session["CompanyLogoPath"] = ConfigurationManager.AppSettings["CompanyLogo"] + Path;
                }

               // ViewBag.inspectorName = model.UserName;
                Session["Name"] = model.UserName;                
                //TempData["Name"] = model.UserName;
                return RedirectToAction("Index", "Dashboard");
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");            
            return View(model);
        }
             
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["CompanyLogoPath"] = null;
            
            return RedirectToAction("Login", "Account");
        } 
    }
}
