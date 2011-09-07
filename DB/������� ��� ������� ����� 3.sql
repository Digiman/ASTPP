SELECT 
    ProductNames.ProductKey || ' ' || ProductNames.Name || ' ' || ProductNames.Designation AS Product,
    ProdNameStand.MaterialCode || ' ' || ReferenceMaterials.Name AS Material,
    ProdNameStand.Consumption AS Consumption,
    ProdNameStand.Wastes AS Wastes
FROM 
    ProdNameStand
    INNER JOIN ReferenceMaterials ON ( ProdNameStand.MaterialCode = ReferenceMaterials.MaterialCode )
    INNER JOIN ProductNames On ( ProdNameStand.ProductCode = ProductNames.ProductKey )
