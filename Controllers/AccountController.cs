using System;
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
        private int _twoFactorTimeOut;
        private bool _twoFactorEnabled;
        //private TranparentIdentity _identity;
        /*
         * 
         * AppSettings table name 
         */
        //youtube video will be on using the nuget package...
        private ITwoFactorAuth _twoFactorAuth;
        // GET: Account

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
            _authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
            _accountSID = ConfigurationManager.AppSettings["TwilioAccountSID"];

            var smsConfigs = new InitTwoFactor()
            {
                TwoFactorAuthTimeSpan = _twoFactorAuthTimeSpan,
                TwoFactorAuthCookie = _twoFactorAuthCookie,
                TwoFactorAuthSmtpHost = _twoFactorAuthSmtpHost,
                TwoFactorAuthFromEmail = _twoFactorAuthFromEmail,
                TwoFactorAuthFromPhone = _twoFactorAuthFromPhone,
                AuthToken = _authToken,
                AccountSID = _accountSID,
                TwoFactorEnabled = _twoFactorEnabled
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

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            //model = Mocks.Mocks.GetRegisterViewModelMocks();
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
                    await SignInAsync(user, isPersistent: false);

                    //verification is required for initial register...
                    return RedirectToAction("TwoFactor", "Account", user);
                }
                else
                {
                    AddErrors(result);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult TwoFactor(User user)
        {
            TwoFactorViewModel model = new TwoFactorViewModel();

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
            var request = new TwoFactorRequestModel()
            {
                Provider = Provider.SMS,
                //UserValue = model.UserValue
                UserValue = "18174120313"
            };
            var response = _twoFactorAuth.CreateTwoFactorAuth(request, Session);

            //if code is good update UtcDate and Verified

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
        public ActionResult VerifyCode(VerifyCodeViewModel model)
        {
            var request = new TwoFactorRequestModel()
            {
                Code = model.Code
            };
            var response = _twoFactorAuth.VerifyCode(model.Code, Session);
            //Save is validated to database, and utc saved time to associated tables..
            model.Status = response.Status;
            model.Message = response.Message;
            var user = UserManager.FindByNameAsync(User.Identity.GetUserName());
            user.Result.Verified = true;
            user.Result.UtaDateExpire = DateTime.UtcNow.AddDays(_twoFactorAuthTimeSpan);
            UserManager.UpdateAsync(user.Result);
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
                    await SignInAsync(user, model.RememberMe);
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
    }
}