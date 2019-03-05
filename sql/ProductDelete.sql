use ECommerce
GO
--EXEC sp_fkeys @pktable_name = 'Product', @pktable_owner = 'dbo'
--EXEC sp_fkeys @pktable_name = 'SpecificationAttribute', @pktable_owner = 'dbo'

DELETE FROM Product_Category_Mapping
DELETE FROM Product_Picture_Mapping
DELETE FROM ProductAttributeValue
DELETE FROM Product_ProductTag_Mapping
DELETE FROM Product_SpecificationAttribute_Mapping
DELETE FROM ProductAttributeCombination
DELETE FROM PictureBinary
DELETE FROM Product_ProductTag_Mapping
DELETE FROM ProductAttributeCombination
DELETE FROM ProductAttributeValue
DELETE FROM ProductAvailabilityRange
DELETE FROM Product_SpecificationAttribute_Mapping
DELETE FROM SpecificationAttribute
DELETE FROM SpecificationAttribute
DELETE FROM SpecificationAttributeOption
DELETE FROM ProductAttribute
DELETE FROM Product_ProductAttribute_Mapping
DELETE FROM Product
DELETE FROM Picture

SELECT COUNT(*) FROM Product_Category_Mapping
SELECT COUNT(*) FROM Product_Picture_Mapping
SELECT COUNT(*) FROM ProductAttributeValue
SELECT COUNT(*) FROM Product_ProductTag_Mapping
SELECT COUNT(*) FROM Product_SpecificationAttribute_Mapping
SELECT COUNT(*) FROM ProductAttributeCombination
SELECT COUNT(*) FROM PictureBinary
SELECT COUNT(*) FROM Product_ProductTag_Mapping
SELECT COUNT(*) FROM ProductAttributeCombination
SELECT COUNT(*) FROM ProductAttributeValue
SELECT COUNT(*) FROM ProductAvailabilityRange
SELECT COUNT(*) FROM Product_SpecificationAttribute_Mapping
SELECT COUNT(*) FROM SpecificationAttribute
SELECT COUNT(*) FROM SpecificationAttribute
SELECT COUNT(*) FROM SpecificationAttributeOption
SELECT COUNT(*) FROM ProductAttribute
SELECT COUNT(*) FROM Product_ProductAttribute_Mapping
SELECT COUNT(*) FROM Product
SELECT COUNT(*) FROM Picture




--Cannot DELETE FROM 'Product' because it is being referenced by a FOREIGN KEY constraint.