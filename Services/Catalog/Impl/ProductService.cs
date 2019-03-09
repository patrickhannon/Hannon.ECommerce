using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Entities;
using ECommerce.Data.Entities.Catalog;
using ECommerce.Data.Entities.Media;
using ECommerce.Data.Repository;
using Hannon.Utils;

namespace ECommerce.Services.Catalog.Impl
{
    public class ProductService : IProductService
    {
        private readonly PictureBinaryRepository _pictureBinaryRepository;
        private readonly PictureRepository _pictureRepository;
        private readonly ProductAttributeCombinationRepository _productAttributeCombinationRepository;
        private readonly ProductAttributeRepository _productAttributeRepository;
        private readonly ProductAttributeValueRepository _productAttributeValueRepository;
        private readonly ProductAvailabilityRangeRepository _productAvailabilityRangeRepository;
        private readonly ProductCategoryMappingRepository _productCategoryMappingRepository;
        private readonly ProductProductAttributeMappingRepository _productProductAttributeMappingRepository;
        private readonly ProductTagMappingRepository _productProductTagMappingRepository;
        private readonly ProductSpecificationAttributeRepository _productSpecificationAttributeMappingRepository;
        private readonly SpecificationAttributeOptionRepository _specificationAttributeOptionRepository;
        private readonly SpecificationAttributeRepository _specificationAttributeRepository;
        private readonly ProductRepository _productRepository;

        public ProductService(
            PictureBinaryRepository pictureBinaryRepository,
            PictureRepository pictureRepository,
            ProductAttributeCombinationRepository productAttributeCombinationRepository,
            ProductAttributeRepository productAttributeRepository,
            ProductAttributeValueRepository productAttributeValueRepository,
            ProductAvailabilityRangeRepository productAvailabilityRangeRepository,
            ProductCategoryMappingRepository productCategoryMappingRepository,
            ProductProductAttributeMappingRepository productProductAttributeMappingRepository,
            ProductTagMappingRepository productProductTagMappingRepository,
            ProductSpecificationAttributeRepository productSpecificationAttributeMappingRepository,
            SpecificationAttributeOptionRepository specificationAttributeOptionRepository,
            SpecificationAttributeRepository specificationAttributeRepository,
            ProductRepository productRepository
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
        }


        public Product GetProductById(int productId)
        {
            ArgumentValidator.ThrowOnOutOfRange("productId", productId, 1, Int32.MaxValue);
            var product = _productRepository.GetById(productId);
            //ProductCategory productCategory = _productCategoryMappingRepository.GetByProductId(productId);



            //missing ProductPicture
            //ProductAttributeMapping productAttributeMapping = _productProductAttributeMappingRepository.GetByProductId(productId);
            //ProductProductTagMapping productProductTagMapping = _productProductTagMappingRepository.GetByProductId(productId);
            //todo ProductSpecificationAttribute productSpecificationAttribute = _productSpecificationAttributeMappingRepository.GetByProductId(productId);
            //ProductAttribute productAttribute = _productAttributeRepository.GetByProductId(productId);
            //ProductAttributeCombination productAttributeCombination = _productAttributeCombinationRepository.GetByProductId(productId);
            //ProductAttributeValue productAttributeValue = _productAttributeValueRepository.GetByProductId(productId);

            //todo SpecificationAttributeOption specificationAttributeOption = _specificationAttributeOptionRepository.GetById();
            //todo SpecificationAttribute specificationAttribute = _specificationAttributeRepository.GetById();
            //todo missing ProductPicture
            //PictureBinary pictureBinary = _pictureBinaryRepository.GetById();
            //Picture picture = _pictureRepository.GetById();
            
            
            //var productAttributeCombination = _productAttributeCombinationRepository.GetById();
            //var productAttribute = _productAttributeRepository.GetById();
            //var productAttributeValue = _productAttributeValueRepository.GetById();
            //var productAvailabilityRange = _productAvailabilityRangeRepository.GetById();
            //var productCategory = _productCategoryMappingRepository.GetById();
            //var productAttributeMapping = _productProductAttributeMappingRepository.GetById();
            //var productProductTagMapping = _productProductTagMappingRepository.GetById();
            //var productSpecificationAttribute = _productSpecificationAttributeMappingRepository.GetById();
            //var specificationAttributeOption = _specificationAttributeOptionRepository.GetById();
            //var specificationAttribute = _specificationAttributeRepository.GetById();


            return product;
        }
    }
}