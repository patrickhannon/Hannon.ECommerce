﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hannon.TwoFactorAuth.Models;
using ECommerce.Models;
using hannon._2factorAuth.Models;
using System.Configuration;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using hannon._2factorAuth;
using Microsoft.Owin.Security;
using ECommerce.Store;
using ECommerce.Data.Entities;
using ECommerce.Data.Repository;

namespace ECommerce.Controllers
{
    public class AccountController : Controller
    {
        private int _twoFactorAuthTimeSpan;
        private string _twoFactorAuthCookie;
        private string _twoFactorAuthSmtpHost;
        private string _twoFactorAuthFromEmail;
        private string _twoFactorAuthFromPhone;
        private string _authToken;
        private string _accountSID;
        private string _emailPassword;
        private readonly int _twoFactorTimeOut;
        private bool _twoFactorEnabled;
        //private TranparentIdentity _identity;
        /*
         * 
         * AppSettings table name 
         */
        //youtube video will be on using the nuget package...
        private ITwoFactorAuth _twoFactorAuth;
        // GET: Account

        private RolesRepository _rolesRepository;
        private string _connectionString;

        public AccountController() 
            : this(new UserManager<User>(new UserStore()))
        {
            int.TryParse(ConfigurationManager.AppSettings["TwoFactorAuthTimeSpan"], out _twoFactorAuthTimeSpan);
            int.TryParse(ConfigurationManager.AppSettings["TwoFactorTimeOut"], out _twoFactorTimeOut);
            bool.TryParse(ConfigurationManager.AppSettings["TwoFactorEnabled"], out _twoFactorEnabled);
            _twoFactorAuthCookie = ConfigurationManager.AppSettings["TwoFactorAuthCookie"];
            _twoFactorAuthSmtpHost = ConfigurationManager.AppSettings["TwoFactorAuthSmtpHost"];
            _twoFactorAuthFromEmail = ConfigurationManager.AppSettings["TwoFactorAuthFromEmail"];
            _twoFactorAuthFromPhone = ConfigurationManager.AppSettings["TwoFactorAuthFromPhone"];
            _emailPassword = ConfigurationManager.AppSettings["EmailPassword"];
            _authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
            _accountSID = ConfigurationManager.AppSettings["TwilioAccountSID"];
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            _rolesRepository = new RolesRepository(_connectionString);
            var smsConfigs = new InitTwoFactor()
            {
                TwoFactorAuthTimeSpan = _twoFactorAuthTimeSpan,
                TwoFactorAuthCookie = _twoFactorAuthCookie,
                TwoFactorAuthSmtpHost = _twoFactorAuthSmtpHost,
                TwoFactorAuthFromEmail = _twoFactorAuthFromEmail,
                TwoFactorAuthFromPhone = _twoFactorAuthFromPhone,
                AuthToken = _authToken,
                AccountSID = _accountSID,
                TwoFactorEnabled = _twoFactorEnabled,
                EmailPassword = _emailPassword
            };
            //_identity = new TranparentIdentity();
            _twoFactorAuth = new TwoFactorAuth(smsConfigs);  
        }

        public AccountController(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<User> UserManager { get; private set; }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = model.UserName,
                    CellPhone = model.CellPhone,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Create user Role Customer...
                    var role = new UserRole()
                    {
                        UserId = user.UserId,
                        Role = "Customer"
                    };
                    await _rolesRepository.AddToRoleAsync(role);
                    //var user = await Task.Run(() => UserManager.FindByNameAsync(User.Identity.GetUserName()));

                    await SignInAsync(user, isPersistent: false);
                    //verification is required for initial register...
                    Session["User"] = user;
                    return RedirectToAction("TwoFactor", "Account");
                }
                else
                {
                    AddErrors(result);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult TwoFactor()
        {
            TwoFactorViewModel model = new TwoFactorViewModel();

            var user = Session["User"] as User;
            if (user != null)
            {
                model.CellPhone = user.CellPhone;
                model.Email = user.Email;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult TwoFactor(TwoFactorViewModel model)
        {
            //what user value are we using: email or sms
            if (ModelState.IsValid)
            {
                var provider = model.Provider.Equals("Email") ? Provider.Email : Provider.SMS;
                var user = Session["User"] as User;
                var request = new TwoFactorRequestModel()
                {
                    Provider = provider,
                    UserValue = provider == Provider.Email ? user.Email : user.CellPhone
                };
                var response = _twoFactorAuth.CreateTwoFactorAuth(request, Session);
                //if code is good update UtcDate and Verified
            }
            //Save is validated to database, and utc saved time to associated tables..
            return RedirectToAction("VerifyCode", "Account");
        }

        public ActionResult VerifyCode()
        {
            var model = new VerifyCodeViewModel();
            //Save is validated to database, and utc saved time to associated tables..
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            var request = new TwoFactorRequestModel()
            {
                Code = model.Code
            };
            if (ModelState.IsValid)
            {
                var response = _twoFactorAuth.VerifyCode(model.Code, Session, Response);
                //Save is validated to database, and utc saved time to associated tables..
                model.Status = response.Status;
                model.Message = response.Message;
                var user = await Task.Run(() => UserManager.FindByNameAsync(User.Identity.GetUserName()));

                user.Verified = true;
                user.UtcDateExpire = DateTime.UtcNow.AddDays(_twoFactorAuthTimeSpan);
                await UserManager.UpdateAsync(user);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    Session["User"] = user;
                    //make sure two factor is in place...
                    await SignInAsync(user, model.RememberMe);
                    if (!_twoFactorAuth.VerifyTwoFactor(Request, user.UtcDateExpire, user.Verified).Status)
                    {
                        //redirect to two factor authentication
                        return RedirectToAction("TwoFactor", "Account", user);
                    };
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        private void SetUserSessionInfo()
        {

        }
    }
}