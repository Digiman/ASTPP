SELECT 
    ProductNames.ProductKey || ' ' || ProductNames.Name || ' ' || ProductNames.Designation AS Product,
    ReferenceStandarts.MaterialCode || ' ' || ReferenceMaterials.Name AS Material,
    ReferenceStandarts.ConsumptionRate * FullApplication.Count AS Consumption,
    ReferenceStandarts.RateOfWaste * FullApplication.Count AS Wastes
FROM 
    FullApplication
    INNER JOIN ReferenceStandarts ON ( FullApplication.PackageDetails = ReferenceStandarts.ProductCode )
    INNER JOIN ReferenceMaterials ON ( ReferenceStandarts.MaterialCode = ReferenceMaterials.MaterialCode )
    INNER JOIN ProductNames ON ( FullApplication.ProductCode = ProductNames.ProductKey );

