SELECT 
  FullApplication.ProductCode,
  ReferenceStandarts.MaterialCode || ' ' || ReferenceMaterials.Name AS FIELD_1,
  ReferenceStandarts.ConsumptionRate * FullApplication.Count AS Consumption,
  ReferenceStandarts.RateOfWaste * FullApplication.Count AS Wastes
FROM
  FullApplication
  INNER JOIN ReferenceStandarts ON (FullApplication.PackageDetails = ReferenceStandarts.ProductCode)
  INNER JOIN ReferenceMaterials ON (ReferenceStandarts.MaterialCode = ReferenceMaterials.MaterialCode)
WHERE
  FullApplication.ProductCode = 77740
