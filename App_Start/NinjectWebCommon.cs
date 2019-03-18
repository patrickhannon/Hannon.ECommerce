using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ECommerce.Data.Core.Data;
using ECommerce.Data.Entities;
using ECommerce.Data.Entities.Catalog;
using ECommerce.Data.Entities.Discounts;
using ECommerce.Data.Entities.Media;
using ECommerce.Data.Entities.Shipping;
using ECommerce.Data.Repository;
using ECommerce.Services;
using ECommerce.Services.Catalog;
using ECommerce.Services.Catalog.Impl;
using ECommerce.Services.Impl;
using ECommerce.Services.Menu;
using ECommerce.Services.Menu.Impl;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

using Microsoft.Web.Infrastructure;
using Ninject.Activation.Caching;
using Ninject.Web.Common.WebHost;




[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ECommerce.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ECommerce.App_Start.NinjectWebCommon), "Stop")]

namespace ECommerce.App_Start
{
    public class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterRepositories(kernel);
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IMenuService>().To<MenuService>();

        }
        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.
        private static void RegisterRepositories(IKernel kernel)
        {
            kernel.Bind<IRepository<Category>>().To<CategoryRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<PictureBinary>>().To<PictureBinaryRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<Picture>>().To<PictureRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductAttributeCombination>>().To<ProductAttributeCombinationRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductAttribute>>().To<ProductAttributeRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductAttributeValue>>().To<ProductAttributeValueRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductAvailabilityRange>>().To<ProductAvailabilityRangeRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductCategory>>().To<ProductCategoryMappingRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductAttributeMapping>>().To<ProductProductAttributeMappingRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductProductTagMapping>>().To<ProductTagMappingRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductSpecificationAttribute>>().To<ProductSpecificationAttributeRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<SpecificationAttributeOption>>().To<SpecificationAttributeOptionRepository>().WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<SpecificationAttribute>>().To<SpecificationAttributeRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductManufacturer>>().To<ProductManufacturerMappingRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductPicture>>().To<ProductPictureRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductReview>>().To<ProductReviewsRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<Product>>().To<ProductRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<TierPrice>>().To<TierPricesRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<DiscountProductMapping>>().To<DiscountProductMappingRepository>().
                WithConstructorArgument("connectionString", _connectionString);

            kernel.Bind<IRepository<ProductWarehouseInventory>>().To<ProductWarehouseInventoryRepository>().
                WithConstructorArgument("connectionString", _connectionString);
        }
    }
}
