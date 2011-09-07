//*****************************************************************************
// Модуль для хранения общих параметров и описаний данных
//*****************************************************************************
using DatabaseLib;
using System.Data;
using Telerik.WinControls.UI;
using System;

namespace ProjectNSI
{
    /// <summary>
    /// Глобальные переменне
    /// </summary>
    public struct GlobalData
    {
        // описание названий справочников
        public static string[] CarouselImageNames = { "Справочник видов продукции", "Справочник типов продукции",
            "Справочник признаков продукции", "Справочник единиц измерения", "Справочник наименований продукции",
            "Справочник технологических материалов", "Справочник норм и отходов на деталь"};
        public static string[] CarouselItemsTags = { "СВП", "СТП", "СПП", "СЕИ", "СНП", "СТМ", "СНиОД" };

        // описание названия таблиц
        public static string[] TableNames = { "ReferenceProducts", "ProductTypes", "ReferenceSigns", "ReferenceUnits", "ProductNames",
                                            "ReferenceMaterials", "ReferenceStandarts"};

        // описание названий файлов с данными для таблиц по умолчанию
        public static string ProdNameTableFile = @"Files\ProductNames.txt";
        public static string RefMatTableFile = @"Files\ReferenceMaterials.txt";
        public static string RefStandTableFile = @"Files\ReferenceStandarts.txt";
        public static string CompositionProdTableFile = @"Files\CompositionProducts.txt";
        public static string PlanProductsTableFile = @"Files\PlanProducts.txt";
        public static string ComplexityTableFile = @"Files\Complexity.txt";
        public static string ComplexityShopTableFile = @"Files\ComplexityShop.txt";
        public static string ShopsRefenceTableFile = @"Files\ShopsReference.txt";
        public static string ProductionReferenceTableFile = @"Files\ProductionRefence.txt";
    }

    #region Описание структур полей для таблиц БД
    // описание полей для строки таблицы СНП
    public struct ProductNameRow
    {
        public long Code;          // код продукции
        public string Name;        // название
        public string Designation; // обозначение продуции
        public int ProductCode;    // код вида
        public int TypeCode;       // код типа
        public int SignCode;       // код признака
    }

    // описание полей для строки таблицы СТМ
    public struct MaterialsRow
    {
        public long Code;          // код материала
        public string Designation; // обозначение материала
        public int UnitCode;       // код единицы измерения
    }

    // описание полей для строки таблицы СНиОД
    public struct StandartRow
    {
        public long ProductCode;      // код продукции (детали)
        public long MaterialCode;     // код материала
        public float ConsumptionRate; // норма расхода
        public float RateOfWaste;     // норма отходов
    }

    // описание полей для строки таблицы "Справочник единиц измерения" СЕИ
    public struct UnitRow
    {
        public string FullName;  // полное название единицы
        public string SmallName; // сокращенное обозаначение
    }

    // описание полей для строки таблицы "Справочник видов продукции" СВП
    public struct ProductRow
    {
        public string Name;      // название вида продукции
    }

    // описание полей для строки таблицы "Справочник типов продукции" СВП
    public struct TypeRow
    {
        public string Name;     // наименование типа
    }

    // описание полей для строки таблицы "Справочник признаков продукции" СПП
    public struct SignRow
    {
        public string Name;     // наименование признака
    }

    // описание полей таблицы "Справочник цехов" СЦ
    public struct ShopsReferenceRow
    {
        public int ShopCode;         // код цеха
        public string Name;          // название цеха
        public int ProductionCode;   // код видов производства
    }

    // описание полей таблицы "Справочник видов производства" СВП
    public struct ProductionReferenceRow
    {
        public string Name;    // наименование вида производства
    }

    // описание полей для строки таблицы "Состав изделия" СИ
    public struct CompositionRow
    {
        public long RootCode;  // корневое изделие
        public long WhereCode; // куда входит
        public long WhatCode;  // что входит
        public int Count;      // количество
    }

    // описание полей для строки таблицы "Полная применяемость" ПП
    public struct FullApplicationRow
    {
        public long ProductCode;    // код изделия (корневого)
        public long PackageDetails; // код детали
        public long Count;          // количество деталей
    }

    // описание полей таблицы "Сводные нормы расхода материала на изделие" СНРМИ
    public struct ProductMaterialStandartsRow
    {
        public long ProductCode;   // код изделия
        public long MaterialCode;  // код материала
        public float Comsumption;  // потребление материала
        public float Waste;        // отходы материала
    }

    // описание полей таблицы "Сводная деталей по материалам для изделия"
    public struct DetailsMaterialOnProductRow
    {
        public long ProductCode;     // код изделия
        public long MaterialCode;    // код материала
        public long DetailCode;      // код детали
        public int Count;            // количество деталей в изделии
        public float DetConsumption; // потребление на деталь
        public float DetWastes;      // отходы на деталь
    }

    // описание полей таблицы "План выпуска изделий"
    public struct PlanProductsRow
    {
        public long ProductCode;  // код изделия
        public int PlanCount;     // план выпуска
        public int Month;         // месяц (1-12)
        public int Year;          // год
    }

    // описание полей таблицы "Потребность в материалах на товарный выпуск"
    public struct MaterialsNeedsRow
    {
        public long ProductCode;   // код изделия
        public long MaterialCode;  // код материала
        public float PlanCount;    // план выпуска
        public int Month;          // месяц (1-12)
        public int Year;           // год
    }

    // описание полей таблицы "Потребность в материалах на товарный выпуск" табл.2
    public struct MaterialsNeeds2Row
    {
        public long MaterialCode;  // код материала
        public float PlanCount;    // план выпуска
        public int Month;          // месяц (1-12)
        public int Year;           // год
    }

    // описание полей таблицы "Сводная нормативная трудоемкость на изделие"
    public struct ComplexityRow
    {
        public long ProductCode;  // код изделия
        public float To;
        public float Tv;
        public float Tpz;
        public float Totl;
        public float Tpt;
        public float Tobs;
    }

    // описание полей таблицы "Сводная нормативная трудоемкость на изделие по цехам"
    public struct ComplexityShopRow
    {
        public long ProductCode;  // код изделия
        public int ShopCode;     // код цеха
        public float To;
        public float Tv;
        public float Tpz;
        public float Totl;
        public float Tpt;
        public float Tobs;
    }

    // описание полей таблицы "Нормативная трудоемкость производственной программы"
    public struct ComplexityProgramRow
    {
        public long ProductCode;  // код изделия
        public int PlanCount;     // план выпуска
        public float Tshk;        // трудоемкость (Тшк)
        public int Month;         // месяц
        public int Year;          // год
    }

    // описание полей таблицы "Нормативная трудоемкость производственной программы по цехам"
    public struct ComplexityShopProgramRow
    {
        public long ProductCode;  // код изделия
        public int ShopCode;      // код цеха
        public int PlanCount;     // план выпуска
        public float Tshk;        // трудоемкость (Тшк)
        public int Month;         // месяц
        public int Year;          // год
    }
    #endregion

    #region Перечисления
    /// <summary>
    /// Описания типов форм
    /// </summary>
    public enum FormType
    {
        ADDFORM = 1, // форма для добавления
        EDITFORM = 2 // форма для редактирования
    }

    /// <summary>
    /// Даты (месяц и год) для потребностей в материалах
    /// </summary>
    public struct MNDates
    {
        public int Month; // месяц
        public int Year;  // год
    } 
    #endregion

    /// <summary>
    /// Класс для конвертации данных в необходимые в том или ином месте типы данных
    /// </summary>
    public static class Converter
    {
        #region Конвертация данных таблицы "Справочник единиц измерения"
        public static UnitRow ConvertRowInfoToUnitRow(GridViewRowInfo row)
        {
            UnitRow res = new UnitRow();

            res.FullName = row.Cells[1].Value.ToString();
            res.SmallName = row.Cells[2].Value.ToString();

            return res;
        }

        public static DatabaseLib.ParametersCollection ConvertUnitRowToParameters(UnitRow row)
        {
            DatabaseLib.ParametersCollection par = new DatabaseLib.ParametersCollection();
            par.Add("FullName", row.FullName, DbType.String);
            par.Add("SmallName", row.SmallName, DbType.String);
            return par;
        }
        #endregion

        #region Конвертация данных таблицы "Справочник технических материалов"
        public static MaterialsRow ConvertRowInfoToMaterialRow(GridViewRowInfo row)
        {
            MaterialsRow res = new MaterialsRow();

            res.Code = (long)row.Cells[0].Value;
            res.Designation = row.Cells[1].Value.ToString();
            res.UnitCode = Convert.ToInt32(row.Cells[2].Value);

            return res;
        }

        public static DatabaseLib.ParametersCollection ConvertMaterialRowToParameters(MaterialsRow row)
        {
            DatabaseLib.ParametersCollection par = new DatabaseLib.ParametersCollection();
            par.Add("MaterialCode", row.Code, DbType.UInt64);
            par.Add("Name", row.Designation, DbType.String);
            par.Add("UnitType", row.UnitCode, DbType.Byte);
            return par;
        }

        public static MaterialsRow ConvertDataTableRowToMaterialRow(DataTable dt)
        {
            MaterialsRow res = new MaterialsRow();

            res.Code = dt.Rows[0].Field<long>(0);
            res.Designation = dt.Rows[0].Field<string>(1);
            res.UnitCode = Convert.ToInt32(dt.Rows[0].ItemArray.GetValue(2));

            return res;
        }
        #endregion

        #region Конвертация данных таблицы "Справочник норм и расходов на деталь"
        public static StandartRow ConvertRowInfoToStandartRow(GridViewRowInfo row)
        {
            StandartRow res = new StandartRow();

            res.ProductCode = (long)row.Cells[0].Value;
            res.MaterialCode = (long)row.Cells[1].Value;
            res.ConsumptionRate = (float)row.Cells[2].Value;
            res.RateOfWaste = (float)row.Cells[3].Value;

            return res;
        }

        public static DatabaseLib.ParametersCollection ConvertStandartRowToParameters(StandartRow row)
        {
            DatabaseLib.ParametersCollection par = new DatabaseLib.ParametersCollection();
            par.Add("ProductCode", row.ProductCode, DbType.UInt64);
            par.Add("MaterialCode", row.MaterialCode, DbType.UInt64);
            par.Add("ConsumptionRate", row.ConsumptionRate, DbType.Single);
            par.Add("RateOfWaste", row.RateOfWaste, DbType.Single);
            return par;
        }

        public static StandartRow ConvertDataTableRowToStandartRow(DataTable dt)
        {
            StandartRow res = new StandartRow();

            res.ProductCode= dt.Rows[0].Field<long>(0);
            res.MaterialCode = dt.Rows[0].Field<long>(1);
            res.ConsumptionRate = dt.Rows[0].Field<float>(2);
            res.RateOfWaste = dt.Rows[0].Field<float>(3);

            return res;
        }
        #endregion

        #region Конвертация данных таблицы "Справочник наименований продукции"
        public static ProductNameRow ConvertRowInfoToProductNameRow(GridViewRowInfo row)
        {
            ProductNameRow res = new ProductNameRow();

            res.Code = (long)row.Cells[0].Value;
            res.Name = row.Cells[1].Value.ToString();
            res.Designation = row.Cells[2].Value.ToString();
            res.ProductCode = Convert.ToInt32(row.Cells[3].Value);
            res.TypeCode = Convert.ToInt32(row.Cells[4].Value);
            res.SignCode = Convert.ToInt32(row.Cells[5].Value);

            return res;
        }

        public static DatabaseLib.ParametersCollection ConvertProductNameRowToParameters(ProductNameRow row)
        {
            DatabaseLib.ParametersCollection par = new DatabaseLib.ParametersCollection();
            par.Add("ProductKey", row.Code, DbType.UInt64);
            par.Add("Name", row.Name, DbType.String);
            par.Add("Designation", row.Designation, DbType.String);
            par.Add("ViewCode", row.ProductCode, DbType.Byte);
            par.Add("TypeCode", row.TypeCode, DbType.Byte);
            par.Add("SignCode", row.SignCode, DbType.Byte);
            return par;
        }

        public static ProductNameRow ConvertDataTableRowToProductNameRow(DataTable dt)
        {
            ProductNameRow res = new ProductNameRow();

            res.Code = dt.Rows[0].Field<long>(0);
            res.Name = dt.Rows[0].Field<string>(1);
            res.Designation = dt.Rows[0].Field<string>(2);
            res.ProductCode = Convert.ToInt32(dt.Rows[0].ItemArray.GetValue(3));
            res.TypeCode = Convert.ToInt32(dt.Rows[0].ItemArray.GetValue(4));
            res.SignCode = Convert.ToInt32(dt.Rows[0].ItemArray.GetValue(5));

            return res;
        }
        #endregion

        #region Конвертация данных таблицы "Справочник признаков продукции"
        public static SignRow ConvertRowInfoToSignRow(GridViewRowInfo row)
        {
            SignRow res = new SignRow();

            res.Name = row.Cells[1].Value.ToString();

            return res;
        }

        public static DatabaseLib.ParametersCollection ConvertSignRowToParameters(SignRow row)
        {
            DatabaseLib.ParametersCollection par = new DatabaseLib.ParametersCollection();
            par.Add("Name", row.Name, DbType.String);
            return par;
        }
        #endregion

        #region Конвертация данных таблицы "Справочник типов продукции"
        public static TypeRow ConvertRowInfoToTypeRow(GridViewRowInfo row)
        {
            TypeRow res = new TypeRow();

            res.Name = row.Cells[1].Value.ToString();

            return res;
        }

        public static DatabaseLib.ParametersCollection ConvertTypeRowToParameters(TypeRow row)
        {
            DatabaseLib.ParametersCollection par = new DatabaseLib.ParametersCollection();
            par.Add("Name", row.Name, DbType.String);
            return par;
        }
        #endregion

        #region Конвертация данных таблицы "Справочник видов продукции"
        public static ProductRow ConvertRowInfoToProductRow(GridViewRowInfo row)
        {
            ProductRow res = new ProductRow();

            res.Name = row.Cells[1].Value.ToString();

            return res;
        }

        public static DatabaseLib.ParametersCollection ConvertProductRowToParameters(ProductRow row)
        {
            DatabaseLib.ParametersCollection par = new DatabaseLib.ParametersCollection();
            par.Add("Name", row.Name, DbType.String);
            return par;
        }
        #endregion

        #region Конвертация данных таблицы "Состав изделий"
        public static CompositionRow ConvertRowInfoToCompositionRow(GridViewRowInfo row)
        {
            CompositionRow res = new CompositionRow();

            res.RootCode = (long)row.Cells[0].Value;
            res.WhereCode = (long)row.Cells[1].Value;
            res.WhatCode = (long)row.Cells[2].Value;
            res.Count = Convert.ToInt32(row.Cells[3].Value);

            return res;
        }

        public static DatabaseLib.ParametersCollection ConvertCompositionRowToParameters(CompositionRow row)
        {
            DatabaseLib.ParametersCollection par = new DatabaseLib.ParametersCollection();
            par.Add("RootCode", row.RootCode, DbType.UInt64);
            par.Add("WhereCode", row.WhereCode, DbType.UInt64);
            par.Add("WhatCode", row.WhatCode, DbType.UInt64);
            par.Add("Count", row.Count, DbType.UInt32);
            return par;
        }

        public static CompositionRow[] ConvertDataTableToCompositionRow(DataTable dt)
        {
            CompositionRow[] res = new CompositionRow[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                res[i].RootCode = dt.Rows[i].Field<long>(0);
                res[i].WhereCode = dt.Rows[i].Field<long>(1);
                res[i].WhatCode = dt.Rows[i].Field<long>(2);
                res[i].Count = Convert.ToInt32(dt.Rows[i].ItemArray.GetValue(3));
            }

            return res;
        }
        #endregion

        #region Конвертация данных таблицы "Полная применяемость"
        public static FullApplicationRow ConvertRowInfoToFullApplicationRow(GridViewRowInfo row)
        {
            FullApplicationRow res = new FullApplicationRow();

            res.ProductCode = (long)row.Cells[0].Value;
            res.PackageDetails = (long)row.Cells[1].Value;
            res.Count = Convert.ToInt32(row.Cells[2].Value);

            return res;
        }

        public static DatabaseLib.ParametersCollection ConvertFullApplicationRowToParameters(FullApplicationRow row)
        {
            DatabaseLib.ParametersCollection par = new DatabaseLib.ParametersCollection();
            par.Add("ProductCode", row.ProductCode, DbType.UInt64);
            par.Add("PackageDetails", row.PackageDetails, DbType.UInt64);
            par.Add("Count", row.Count, DbType.UInt32);
            return par;
        }
        #endregion

        #region Конвертация данных таблицы "Сводная норма расходов материалов на изделие"
        public static DatabaseLib.ParametersCollection ConverеProductMaterialStandartsRowToParameters(ProductMaterialStandartsRow row)
        {
            DatabaseLib.ParametersCollection par = new DatabaseLib.ParametersCollection();
            par.Add("ProductCode", row.ProductCode, DbType.UInt64);
            par.Add("MaterialCode", row.MaterialCode, DbType.UInt64);
            par.Add("Consumption", row.Comsumption, DbType.Single);
            par.Add("Wastes", row.Waste, DbType.Single);
            return par;
        }

        public static DatabaseLib.ParametersCollection[] ConverеProductMaterialStandartsRowToParameters(DataTable dt)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("ProductCode", dt.Rows[i].ItemArray[0], DbType.UInt64);
                par[i].Add("MaterialCode", dt.Rows[i].ItemArray[1], DbType.UInt64);
                par[i].Add("Consumption", dt.Rows[i].ItemArray[2], DbType.Single);
                par[i].Add("Wastes", dt.Rows[i].ItemArray[3], DbType.Single);
            }
            return par;
        }

        public static ProductMaterialStandartsRow[] ConvertDataTableToProductMaterialStandartsRow(DataTable dt)
        {
            ProductMaterialStandartsRow[] res = new ProductMaterialStandartsRow[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                res[i].ProductCode = dt.Rows[i].Field<long>(0);
                res[i].MaterialCode = dt.Rows[i].Field<long>(1);
                res[i].Comsumption = dt.Rows[i].Field<float>(2);
                res[i].Waste = dt.Rows[i].Field<float>(3);
            }

            return res;
        }
        #endregion

        #region Конвертация данных таблицы "Сводная деталей по материалам для изделия"
        public static ParametersCollection[] ConverеDetailsMaterialOnProductRowToParameters(DataTable dt)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("ProductCode", dt.Rows[i].ItemArray[0], DbType.UInt64);
                par[i].Add("MaterialCode", dt.Rows[i].ItemArray[1], DbType.UInt64);
                par[i].Add("DetailCode", dt.Rows[i].ItemArray[2], DbType.UInt64);
                par[i].Add("Count", dt.Rows[i].ItemArray[3], DbType.UInt32);
                par[i].Add("Consumption", dt.Rows[i].ItemArray[4], DbType.Single);
                par[i].Add("Wastes", dt.Rows[i].ItemArray[5], DbType.Single);
            }
            return par;
        }
        #endregion

        #region Конвертация данных таблицы "План выпуска изделий"
        public static DatabaseLib.ParametersCollection ConvertPlanProductsRowToParameters(PlanProductsRow row)
        {
            DatabaseLib.ParametersCollection par = new DatabaseLib.ParametersCollection();
            par.Add("ProductCode", row.ProductCode, DbType.UInt64);
            par.Add("PlanCount", row.PlanCount, DbType.UInt64);
            par.Add("Month", row.Month, DbType.Single);
            par.Add("Year", row.Year, DbType.Single);
            return par;
        }

        public static PlanProductsRow[] ConvertDataTableToPlanProductsRow(DataTable dt)
        {
            PlanProductsRow[] res = new PlanProductsRow[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                res[i].ProductCode = dt.Rows[i].Field<long>(0);
                res[i].PlanCount = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
                res[i].Month = Convert.ToInt32(dt.Rows[i].ItemArray[2]);
                res[i].Year = Convert.ToInt32(dt.Rows[i].ItemArray[3]);
            }

            return res;
        }
        #endregion

        #region Конвертация данных таблицы "Потребность на товарный выпуск"
        public static ParametersCollection[] ConvertMaterialNeedsDataTableToParameters(DataTable dt)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("ProductCode", dt.Rows[i].ItemArray[0], DbType.UInt64);
                par[i].Add("MaterialCode", dt.Rows[i].ItemArray[1], DbType.UInt64);
                par[i].Add("PlanCount", dt.Rows[i].ItemArray[2], DbType.Single);
                par[i].Add("Month", dt.Rows[i].ItemArray[3], DbType.UInt64);
                par[i].Add("Year", dt.Rows[i].ItemArray[4], DbType.UInt32);
            }
            return par;
        }

        public static DatabaseLib.ParametersCollection[] ConvertMaterialsNeedsRowToParameters(MaterialsNeedsRow[] rows)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("ProductCode", rows[i].ProductCode, DbType.UInt64);
                par[i].Add("MaterialCode", rows[i].MaterialCode, DbType.UInt64);
                par[i].Add("PlanCount", rows[i].PlanCount, DbType.Single);
                par[i].Add("Month", rows[i].Month, DbType.UInt32);
                par[i].Add("Year", rows[i].Year, DbType.UInt32);
            }
            return par;
        }

        public static ParametersCollection[] ConvertMaterialNeeds2DataTableToParameters(DataTable dt)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("MaterialCode", dt.Rows[i].ItemArray[0], DbType.UInt64);
                par[i].Add("PlanCount", dt.Rows[i].ItemArray[1], DbType.Single);
                par[i].Add("Month", dt.Rows[i].ItemArray[2], DbType.UInt64);
                par[i].Add("Year", dt.Rows[i].ItemArray[3], DbType.UInt32);
            }
            return par;
        }

        public static DatabaseLib.ParametersCollection[] ConvertMaterialsNeeds2RowToParameters(MaterialsNeeds2Row[] rows)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("MaterialCode", rows[i].MaterialCode, DbType.UInt64);
                par[i].Add("PlanCount", rows[i].PlanCount, DbType.Single);
                par[i].Add("Month", rows[i].Month, DbType.UInt32);
                par[i].Add("Year", rows[i].Year, DbType.UInt32);
            }
            return par;
        }
        #endregion

        #region Конвертация данных таблицы "Сводная нормативная трудоемкость на изделие"
        public static ParametersCollection[] ConvertComplexityDataTableToParameters(DataTable dt)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("ProductCode", dt.Rows[i].ItemArray[0], DbType.UInt64);
                par[i].Add("T0", dt.Rows[i].ItemArray[1], DbType.Single);
                par[i].Add("Tv", dt.Rows[i].ItemArray[2], DbType.Single);
                par[i].Add("Tpz", dt.Rows[i].ItemArray[3], DbType.Single);
                par[i].Add("Totl", dt.Rows[i].ItemArray[4], DbType.Single);
                par[i].Add("Tpt", dt.Rows[i].ItemArray[5], DbType.Single);
                par[i].Add("Tobs", dt.Rows[i].ItemArray[6], DbType.Single);
            }
            return par;
        }

        public static DatabaseLib.ParametersCollection[] ConvertComplexityRowToParameters(ComplexityRow[] rows)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("ProductCode", rows[i].ProductCode, DbType.UInt64);
                par[i].Add("T0", rows[i].To, DbType.Single);
                par[i].Add("Tv", rows[i].Tv, DbType.Single);
                par[i].Add("Tpz", rows[i].Tpz, DbType.Single);
                par[i].Add("Totl", rows[i].Totl, DbType.Single);
                par[i].Add("Tpt", rows[i].Tpt, DbType.Single);
                par[i].Add("Tobs", rows[i].Tobs, DbType.Single);
            }
            return par;
        }
        #endregion

        #region Конвертация данных таблицы "Сводная нормативная трудоемкость на изделие по цехам"
        public static ParametersCollection[] ConvertComplexityShopDataTableToParameters(DataTable dt)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("ProductCode", dt.Rows[i].ItemArray[0], DbType.UInt64);
                par[i].Add("ShopCode", dt.Rows[i].ItemArray[1], DbType.UInt64);
                par[i].Add("T0", dt.Rows[i].ItemArray[2], DbType.Single);
                par[i].Add("Tv", dt.Rows[i].ItemArray[3], DbType.Single);
                par[i].Add("Tpz", dt.Rows[i].ItemArray[4], DbType.Single);
                par[i].Add("Totl", dt.Rows[i].ItemArray[5], DbType.Single);
                par[i].Add("Tpt", dt.Rows[i].ItemArray[6], DbType.Single);
                par[i].Add("Tobs", dt.Rows[i].ItemArray[7], DbType.Single);
            }
            return par;
        }

        public static DatabaseLib.ParametersCollection[] ConvertComplexityShopRowToParameters(ComplexityShopRow[] rows)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("ProductCode", rows[i].ProductCode, DbType.UInt64);
                par[i].Add("ShopCode", rows[i].ShopCode, DbType.UInt64);
                par[i].Add("T0", rows[i].To, DbType.Single);
                par[i].Add("Tv", rows[i].Tv, DbType.Single);
                par[i].Add("Tpz", rows[i].Tpz, DbType.Single);
                par[i].Add("Totl", rows[i].Totl, DbType.Single);
                par[i].Add("Tpt", rows[i].Tpt, DbType.Single);
                par[i].Add("Tobs", rows[i].Tobs, DbType.Single);
            }
            return par;
        }
        #endregion

        #region Конвертация данных таблицы "Нормативная трудоемкость производственной программы"
        public static ParametersCollection[] ConvertComplexityProgramDataTableToParameters(DataTable dt)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("ProductCode", dt.Rows[i].ItemArray[0], DbType.UInt64);
                par[i].Add("PlanCount", dt.Rows[i].ItemArray[1], DbType.UInt32);
                par[i].Add("Tshk", dt.Rows[i].ItemArray[2], DbType.Single);
                par[i].Add("Month", dt.Rows[i].ItemArray[3], DbType.UInt32);
                par[i].Add("Year", dt.Rows[i].ItemArray[4], DbType.UInt32);
            }
            return par;
        }
        #endregion

        #region Конвертация данных таблицы "Нормативная трудоемкость производственной программы по цехам"
        public static ParametersCollection[] ConvertComplexityShopProgramDataTableToParameters(DataTable dt)
        {
            DatabaseLib.ParametersCollection[] par = new DatabaseLib.ParametersCollection[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                par[i] = new DatabaseLib.ParametersCollection();
                par[i].Add("ProductCode", dt.Rows[i].ItemArray[0], DbType.UInt64);
                par[i].Add("ShopCode", dt.Rows[i].ItemArray[1], DbType.UInt32);
                par[i].Add("PlanCount", dt.Rows[i].ItemArray[2], DbType.UInt32);
                par[i].Add("Tshk", dt.Rows[i].ItemArray[3], DbType.Single);
                par[i].Add("Month", dt.Rows[i].ItemArray[4], DbType.UInt32);
                par[i].Add("Year", dt.Rows[i].ItemArray[5], DbType.UInt32);
            }
            return par;
        }
        #endregion

        #region Конвертация данных таблицы "Справочник цехов"
        public static ParametersCollection ConvertShopsReferenceRowToParameter(ShopsReferenceRow shops)
        {
            ParametersCollection par = new ParametersCollection();

            par.Add("ShopCode", shops.ShopCode, DbType.UInt32);
            par.Add("Name", shops.Name, DbType.String);
            par.Add("ProductionCode", shops.ProductionCode, DbType.UInt32);

            return par;
        }
        #endregion

        #region Конвертация данных таблицы "Справочник видов производства"
        public static ParametersCollection ConvertProductionReferenceRowToParameter(ProductionReferenceRow prodref)
        {
            ParametersCollection par = new ParametersCollection();

            par.Add("Name", prodref.Name, DbType.String);

            return par;
        }
        #endregion

        #region Вспомогательные функции
        /// <summary>
        /// Корректировка вещественного числа (замена точки на запятую)
        /// </summary>
        /// <param name="str">Строка с вещественным числом для корректировки</param>
        /// <returns>Возвращает исправленную строку</returns>
        public static string CorrectFloatUnit(string str)
        {
            return str.Replace('.', ',');
        }

        #endregion
    }

    #region Класс для описания дополнительных возможностей по отображению таблиц в RadDataGrid
    /// <summary>
    /// Класс для настройки таблиц, перед загрузкой в них данных
    /// </summary>
    public static class DataGridViewHelper
    {
        public static void TuneUpReferenceProductTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Код вида";
            dataTableView.Columns[0].Width = 80;
            dataTableView.Columns[1].HeaderText = "Наименование вида";
            dataTableView.Columns[1].Width = 200;
        }

        public static void TuneUpProductTypesTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Код типа";
            dataTableView.Columns[0].Width = 80;
            dataTableView.Columns[1].HeaderText = "Наименование типа продукции";
            dataTableView.Columns[1].Width = 200;
        }

        public static void TuneUpReferenceSignTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Код признака";
            dataTableView.Columns[0].Width = 80;
            dataTableView.Columns[1].HeaderText = "Наименование признака";
            dataTableView.Columns[1].Width = 200;
        }

        public static void TuneUpReferenceUnitsTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Код е. и.";
            dataTableView.Columns[0].Width = 80;
            dataTableView.Columns[1].HeaderText = "Наименование е. и.";
            dataTableView.Columns[1].Width = 150;
            dataTableView.Columns[2].HeaderText = "Краткое наименование";
            dataTableView.Columns[2].Width = 150;
        }

        public static void TuneUpReferenceMaterialsTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Код материала";
            dataTableView.Columns[0].Width = 100;
            dataTableView.Columns[1].HeaderText = "Обозначение материала";
            dataTableView.Columns[1].Width = 300;
            dataTableView.Columns[2].HeaderText = "Код единицы измерения";
            dataTableView.Columns[2].Width = 200;
        }

        public static void TuneUpReferenceStandartsTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Код продукции";
            dataTableView.Columns[0].Width = 100;
            dataTableView.Columns[1].HeaderText = "Наименование продукции";
            dataTableView.Columns[1].Width = 200;
            dataTableView.Columns[2].HeaderText = "Наименование материала";
            dataTableView.Columns[2].Width = 250;
            dataTableView.Columns[3].HeaderText = "Норма расхода";
            dataTableView.Columns[3].Width = 150;
            dataTableView.Columns[4].HeaderText = "Норма отходов";
            dataTableView.Columns[4].Width = 150;
        }

        public static void TuneUpProductNamesTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Код продукции";
            dataTableView.Columns[0].Width = 100;
            dataTableView.Columns[1].HeaderText = "Наименование";
            dataTableView.Columns[1].Width = 200;
            dataTableView.Columns[2].HeaderText = "Обозначение";
            dataTableView.Columns[2].Width = 200;
            dataTableView.Columns[3].HeaderText = "Код вида";
            dataTableView.Columns[3].Width = 150;
            dataTableView.Columns[4].HeaderText = "Код типа";
            dataTableView.Columns[4].Width = 150;
            dataTableView.Columns[5].HeaderText = "Код признака";
            dataTableView.Columns[5].Width = 150;
        }

        public static void TuneUpCompositionProductsTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Изделие";
            dataTableView.Columns[0].Width = 200;
            dataTableView.Columns[1].HeaderText = "Изделие, СЕ (куда входит)";
            dataTableView.Columns[1].Width = 250;
            dataTableView.Columns[2].HeaderText = "Деталь, СЕ (что входит)";
            dataTableView.Columns[2].Width = 250;
            dataTableView.Columns[3].HeaderText = "Количество";
            dataTableView.Columns[3].Width = 80;
        }

        public static void TuneUpFullApplicationTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Изделие";
            dataTableView.Columns[0].Width = 200;
            dataTableView.Columns[1].HeaderText = "Название и обозначение детали";
            dataTableView.Columns[1].Width = 250;
            dataTableView.Columns[2].HeaderText = "Количество";
            dataTableView.Columns[2].Width = 100;
        }

        public static void TuneUpProdMatStandTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Изделие";
            dataTableView.Columns[0].Width = 170;
            dataTableView.Columns[1].HeaderText = "Материал";
            dataTableView.Columns[1].Width = 350;
            dataTableView.Columns[2].HeaderText = "Расходы, кг";
            dataTableView.Columns[2].Width = 100;
            dataTableView.Columns[3].HeaderText = "Отходы, кг";
            dataTableView.Columns[3].Width = 100;
        }

        public static void TuneUpDetMatOnProdTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Изделие";
            dataTableView.Columns[0].Width = 100;
            dataTableView.Columns[1].HeaderText = "Материал";
            dataTableView.Columns[1].Width = 100;
            dataTableView.Columns[2].HeaderText = "Деталь";
            dataTableView.Columns[2].Width = 100;
            dataTableView.Columns[3].HeaderText = "Количество";
            dataTableView.Columns[3].Width = 100;
            dataTableView.Columns[4].HeaderText = "Расходы, кг";
            dataTableView.Columns[4].Width = 100;
            dataTableView.Columns[5].HeaderText = "Отходы, кг";
            dataTableView.Columns[5].Width = 100;
        }

        public static void TuneUpPlanProductsTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Изделие";
            dataTableView.Columns[0].Width = 200;
            dataTableView.Columns[1].HeaderText = "Кол-во по плану";
            dataTableView.Columns[1].Width = 100;
            dataTableView.Columns[2].HeaderText = "Месяц";
            dataTableView.Columns[2].Width = 100;
            dataTableView.Columns[3].HeaderText = "Год";
            dataTableView.Columns[3].Width = 100;
        }

        public static void TuneUpMaterialsNeedsTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Изделие";
            dataTableView.Columns[0].Width = 250;
            dataTableView.Columns[1].HeaderText = "Материал";
            dataTableView.Columns[1].Width = 250;
            dataTableView.Columns[2].HeaderText = "Кол-во по плану";
            dataTableView.Columns[2].Width = 100;
            dataTableView.Columns[3].HeaderText = "Месяц";
            dataTableView.Columns[3].Width = 80;
            dataTableView.Columns[4].HeaderText = "Год";
            dataTableView.Columns[4].Width = 50;
        }

        public static void TuneUpMaterialsNeeds2Table(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Материал";
            dataTableView.Columns[0].Width = 300;
            dataTableView.Columns[1].HeaderText = "Кол-во по плану";
            dataTableView.Columns[1].Width = 100;
            dataTableView.Columns[2].HeaderText = "Месяц";
            dataTableView.Columns[2].Width = 80;
            dataTableView.Columns[3].HeaderText = "Год";
            dataTableView.Columns[3].Width = 50;
        }

        public static void TuneUpCompexityTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Изделие";
            dataTableView.Columns[0].Width = 300;
            dataTableView.Columns[1].HeaderText = "То";
            dataTableView.Columns[1].Width = 50;
            dataTableView.Columns[2].HeaderText = "Tв";
            dataTableView.Columns[2].Width = 50;
            dataTableView.Columns[3].HeaderText = "Тпз";
            dataTableView.Columns[3].Width = 50;
            dataTableView.Columns[4].HeaderText = "Тотл";
            dataTableView.Columns[4].Width = 50;
            dataTableView.Columns[5].HeaderText = "Тпт";
            dataTableView.Columns[5].Width = 50;
            dataTableView.Columns[6].HeaderText = "Тобс";
            dataTableView.Columns[6].Width = 50;
        }

        public static void TuneUpCompexityShopTable(RadGridView dataTableView)
        {
            dataTableView.Columns[0].HeaderText = "Изделие";
            dataTableView.Columns[0].Width = 300;
            dataTableView.Columns[1].HeaderText = "Цех";
            dataTableView.Columns[1].Width = 150;
            dataTableView.Columns[2].HeaderText = "То";
            dataTableView.Columns[2].Width = 50;
            dataTableView.Columns[3].HeaderText = "Tв";
            dataTableView.Columns[3].Width = 50;
            dataTableView.Columns[4].HeaderText = "Тпз";
            dataTableView.Columns[4].Width = 50;
            dataTableView.Columns[5].HeaderText = "Тотл";
            dataTableView.Columns[5].Width = 50;
            dataTableView.Columns[6].HeaderText = "Тпт";
            dataTableView.Columns[6].Width = 50;
            dataTableView.Columns[7].HeaderText = "Тобс";
            dataTableView.Columns[7].Width = 50;
        }
    }
    #endregion
}
