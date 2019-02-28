using System;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using ECommerce.Store;
using Microsoft.AspNet.Identity;
using ECommerce.Models;
using ECommerce.Data.Entities;
using ECommerce.Data.Repository;
using System.Configuration;

namespace ECommerce.Filters
{
    internal class ECommerceAuthorizeAttribute : AuthorizeAttribute
    {
        public string AccessLevel { get; set; }
        private RolesRepository _rolesRepository;
        private readonly string _connectionString;

        public ECommerceAuthorizeAttribute() : 
            this(new UserManager<User>(new UserStore()))
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _rolesRepository = new RolesRepository(_connectionString);
        }

        public ECommerceAuthorizeAttribute(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            if (!httpContext.User.Identity.IsAuthenticated) return false;
            var user = httpContext.User.Identity;

            var role = new UserRole()
            {
                UserId = Guid.Parse(user.GetUserId()),
                Role = AccessLevel
            };
            string privilegeLevels = string.Join("", _rolesRepository.GetRoles(role)); 
            //return true;
            return privilegeLevels.Contains(this.AccessLevel);
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Error",
                        action = "Unauthorised"
                    })
            );
        }

        public UserManager<User> UserManager { get; private set; }
    }
}