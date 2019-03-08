using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using ECommerce.Services.Menu;
using ECommerce.Services.Menu.Impl;
using Hannon.Utils;
using Ninject;
using System.Windows;

namespace ECommerce
{
    public class MvcApplication : System.Web.HttpApplication
    {
        IKernel _container = new StandardKernel();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureContainer();
            ComposeObjects();
        }

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                //log the error
                Utils.LogToEventLog("Application", exception.Message, EventLogEntryType.Error);
            }
        }

        private void ConfigureContainer()
        {
            _container.Bind<IMenuService>().To<MenuService>();
        }

        private void ComposeObjects()
        {
            
            
        }
    }
}
