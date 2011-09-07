SELECT
    F1 || ' ' || F2 || ' ' || F3 AS Product,
    ProductNames.Name || ' ' ||ProductNames.Designation AS NameDes,
    FullApplication.Count
FROM
    FullApplication
    INNER JOIN ProductNames ON (FullApplication.PackageDetails = ProductNames.ProductKey)
    INNER JOIN (SELECT ProductNames.ProductKey AS F1, ProductNames.Name AS F2, ProductNames.Designation AS F3 
                FROM ProductNames) ON (FullApplication.ProductCode = F1)
