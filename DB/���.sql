SELECT 
 ReferenceStandarts.ProductCode,                                
                                ProductNames.Name || ' ' || ProductNames.Designation,
                                ReferenceMaterials.Name,
                                ReferenceStandarts.ConsumptionRate,
                                ReferenceStandarts.RateOfWaste,
                                ReferenceUnits.SmallName
                            FROM
                                ReferenceStandarts
                                INNER JOIN ReferenceMaterials ON (ReferenceStandarts.MaterialCode = ReferenceMaterials.MaterialCode)
                                INNER JOIN ProductNames ON (ReferenceStandarts.ProductCode = ProductNames.ProductKey)
                                INNER JOIN ReferenceUnits ON (ReferenceUnits.PKey = ReferenceMaterials.MaterialCode)