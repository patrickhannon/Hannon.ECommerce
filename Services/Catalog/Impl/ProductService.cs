using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Core.Data;
using ECommerce.Data.Entities;
using ECommerce.Data.Entities.Catalog;
using ECommerce.Data.Entities.Discounts;
using ECommerce.Data.Entities.Media;
using ECommerce.Data.Entities.Shipping;
using ECommerce.Data.Repository;
using Hannon.Utils;

namespace ECommerce.Services.Catalog.Impl
{
    public class ProductService : IProductService
    {
        private readonly IRepository<PictureBinary> _pictureBinaryRepository;
        private readonly IRepository<Picture> _pictureRepository;
        private readonly IRepository<ProductAttributeCombination> _productAttributeCombinationRepository;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IRepository<ProductAttributeValue> _productAttributeValueRepository;
        private readonly IRepository<ProductAvailabilityRange> _productAvailabilityRangeRepository;
        private readonly IRepository<ProductCategory> _productCategoryMappingRepository;
        private readonly IRepository<ProductAttributeMapping> _productProductAttributeMappingRepository;
        private readonly IRepository<ProductProductTagMapping> _productProductTagMappingRepository;
        private readonly IRepository<ProductSpecificationAttribute> _productSpecificationAttributeMappingRepository;
        private readonly IRepository<SpecificationAttributeOption> _specificationAttributeOptionRepository;
        private readonly IRepository<SpecificationAttribute> _specificationAttributeRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductManufacturer> _productManufacturerMappingRepository;
        private readonly IRepository<ProductPicture> _productPictureRepository;
        private readonly IRepository<ProductReview> _productReviewsRepository;
        private readonly IRepository<TierPrice> _tierPricesRepository;
        private readonly IRepository<DiscountProductMapping> _discountProductMappingRepository;
        private readonly IRepository<ProductWarehouseInventory> _productWarehouseInventoryRepository;

        public ProductService(
            IRepository<PictureBinary> pictureBinaryRepository,
            IRepository<Picture> pictureRepository,
            IRepository<ProductAttributeCombination> productAttributeCombinationRepository,
            IRepository<ProductAttribute> productAttributeRepository,
            IRepository<ProductAttributeValue> productAttributeValueRepository,
            IRepository<ProductAvailabilityRange> productAvailabilityRangeRepository,
            IRepository<ProductCategory> productCategoryMappingRepository,
            IRepository<ProductAttributeMapping> productProductAttributeMappingRepository,
            IRepository<ProductProductTagMapping> productProductTagMappingRepository,
            IRepository<ProductSpecificationAttribute> productSpecificationAttributeMappingRepository,
            IRepository<SpecificationAttributeOption> specificationAttributeOptionRepository,
            IRepository<SpecificationAttribute> specificationAttributeRepository,
            IRepository<Product> productRepository,
            IRepository<ProductManufacturer> productManufacturerMappingRepository,
            IRepository<ProductPicture> productPictureRepository,
            IRepository<ProductReview> productReviewsRepository,
            IRepository<TierPrice> tierPricesRepository,
            IRepository<DiscountProductMapping> discountProductMappingRepository,
            IRepository<ProductWarehouseInventory> productWarehouseInventoryRepository
            )
        {
            _pictureBinaryRepository = pictureBinaryRepository;
            _pictureRepository = pictureRepository;
            _productAttributeCombinationRepository = productAttributeCombinationRepository;
            _productAttributeRepository = productAttributeRepository;
            _productAttributeValueRepository = productAttributeValueRepository;
            _productAvailabilityRangeRepository = productAvailabilityRangeRepository;
            _productCategoryMappingRepository = productCategoryMappingRepository;
            _productProductAttributeMappingRepository = productProductAttributeMappingRepository;
            _productProductTagMappingRepository = productProductTagMappingRepository;
            _productSpecificationAttributeMappingRepository = productSpecificationAttributeMappingRepository;
            _specificationAttributeOptionRepository = specificationAttributeOptionRepository;
            _specificationAttributeRepository = specificationAttributeRepository;
            _productRepository = productRepository;
            _productManufacturerMappingRepository = productManufacturerMappingRepository;
            _productPictureRepository = productPictureRepository;
            _productReviewsRepository = productReviewsRepository;
            _tierPricesRepository = tierPricesRepository;
            _discountProductMappingRepository = discountProductMappingRepository;
            _productWarehouseInventoryRepository = productWarehouseInventoryRepository;
        }


        public Product GetProductById(int productId)
        {
            ArgumentValidator.ThrowOnOutOfRange("productId", productId, 1, Int32.MaxValue);
            var product = _productRepository.GetById(productId);
            var productCategories = _productCategoryMappingRepository.GetByProductId(productId);
            var productManufacturers = _productManufacturerMappingRepository.GetByProductId(productId);
            var productPictures = _productPictureRepository.GetByProductId(productId);
            var productReviews = _productReviewsRepository.GetByProductId(productId);
            var productSpecificationAttributes =
                _productSpecificationAttributeMappingRepository.GetByProductId(productId);

            var productProductTagMappings = _productProductTagMappingRepository.GetByProductId(productId);
            //todo var productAttributeMappings = _productAttributeRepository.GetByProductId(productId);
            var tierPrices = _tierPricesRepository.GetByProductId(productId);
            //todo var discountProductMapping = _discountProductMappingRepository.GetByProductId(productId);
            var productWarehouseInventory = _productWarehouseInventoryRepository.GetByProductId(productId);

            if (productCategories.Any()) product.ProductCategories = productCategories;
            if (productManufacturers.Any()) product.ProductManufacturers = productManufacturers;
            if (productPictures.Any()) product.ProductPictures = productPictures;
            if (productReviews.Any()) product.ProductReviews = productReviews;
            if (productSpecificationAttributes.Any()) product.ProductSpecificationAttributes = productSpecificationAttributes;
            //if (productProductTagMappings.Any()) product.ProductProductTagMappings = productProductTagMappings;
            //if (productAttributeMappings.Any()) product.ProductAttributeMappings = productAttributeMappings;
            if (productCategories.Any()) product.ProductCategories = productCategories;
            if (tierPrices.Any()) product.TierPrices = tierPrices;
            if (productWarehouseInventory.Any()) product.ProductWarehouseInventory = productWarehouseInventory;

           

            return product;
        }
    }
}