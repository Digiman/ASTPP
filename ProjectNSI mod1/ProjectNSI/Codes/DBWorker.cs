//*****************************************************************************
// Модуль для описаний функции для работы с БД SQLite
//*****************************************************************************
using System.Data;
using System.IO;
using System.Windows.Forms;
using System;

namespace ProjectNSI
{
    /// <summary>
    /// Класс для работы с БД, реализации всех основных действия с Бд, необходимых для решения задач программы
    /// </summary>
    public static class DBWorker
    {
        #region Переменные и параметры
        public static DatabaseLib.dbFacade dbf; // соединение и методы для БД
        /// <summary>
        /// Подключена ли БД (установлено ли соединение с файлом с БД)
        /// </summary>
        public static bool flag = false;
        /// <summary>
        /// Создана ли таблица БД "Полная применяемость"
        /// </summary>
        public static bool IsItFATable = false;
        /// <summary>
        /// Создана ли таблица БД "Сводные нормы расхода материалов на изделие"
        /// </summary>
        public static bool IsItPNSTable = false;
        /// <summary>
        /// Создана ли таблица "Сводная деталей по материалам для изделия"
        /// </summary>
        public static bool IsItDMPTable = false;
        /// <summary>
        /// Создана ли таблица "Потребность в материалах на товарный выпуск"
        /// </summary>
        public static bool IsItMNTable = false;
        public static bool IsItMN2Table = false;
        /// <summary>
        /// Создана ли таблица "НТПП" 
        /// </summary>
        public static bool IsItCPTable = false;
        /// <summary>
        /// Создана ли таблица "НТППЦ" 
        /// </summary>
        public static bool IsItCSPTable = false;
        #endregion

        #region Подключение/отключение БД
        /// <summary>
        /// Подключение к БД
        /// </summary>
        /// <param name="dbname">Путь и название файла БД</param>
        public static void ConnectToDB(string dbname)
        {
            dbf = new DatabaseLib.dbFacade(dbname);
            bool resut = dbf.Open();
            if (resut == false)
                MessageBox.Show("Подключение не установлено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                flag = true;
        }

        /// <summary>
        /// Отключение от БД, с которой было установлено соединение
        /// </summary>
        public static void DisconnectFromDB()
        {
            if (dbf != null)
            {
                // удаление временных таблиц
                if (IsItFATable)
                    DeleteFullApplicationTable();
                if (IsItPNSTable)
                    DropProdMatStandTable();
                if (IsItDMPTable)
                    DropDetMatOnProdTable();
                if (IsItMNTable)
                    DropMaterialsNeedsTable();
                if (IsItMN2Table)
                    DropMaterialsNeeds2Table();
                if (IsItCPTable)
                    DropComplexityProgramTable();
                if (IsItCSPTable)
                    DropComplexityShopProgramTable();
                dbf.Close();
                //MessageBox.Show("Подключение закрыто успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
            }
        } 
        #endregion

        #region Выборка данных
        /// <summary>
        /// Выборка всех данных из таблицы по ей тегу
        /// </summary>
        /// <param name="TableTag">Тэг для определения таблицы из БД</param>
        /// <returns>Возвращает таблицу с данными System.Data.DataTable с содержимым таблицы из БД</returns>
        public static DataTable SelectDataFromTable(string TableTag)
        {
            DatabaseLib.Select str = new DatabaseLib.Select();
            // создание строки запроса
            switch (TableTag)
            {
                case "СВП":
                    str.From("ReferenceProducts");
                    break;
                case "СТП":
                    str.From("ProductTypes");
                    break;
                case "СПП":
                    str.From("ReferenceSigns");
                    break;
                case "СЕИ":
                    str.From("ReferenceUnits");
                    break;
                case "СНП":
                    str.From("ProductNames");
                    break;
                case "СТМ":
                    str.From("ReferenceMaterials");
                    break;
                case "СНиОД":
                    str.From("ReferenceStandarts");
                    break;
                case "СИ": // выборка из таблицы "Состав изделий"
                    str.From("CompositionProducts");
                    break;
                case "ПП": // выборка из таблицы "Полная применяемость"
                    str.From("FullApplication");
                    break;
                case "ПВИ": // выборка из таблицы "План выпуска изделий"
                    str.From("PlanProducts");
                    break;
                case "СНТИ": // выборка из таблицы "Сводная нормативная трудоемкость на изделие"
                    str.From("Complexity");
                    break;
                case "СНТИЦ": // выборка из таблицы "Сводная нормативная трудоемкость на изделие по цехам"
                    str.From("ComplexityShop");
                    break;
                case "НТПП": // выборка из таблицы "Нормативная трудоемкость производственной программы"
                    str.From("ComplexityProgram");
                    break;
                case "НТППЦ": // выборка из таблицы "Нормативная трудоемкость производственной программы по цехам"
                    str.From("ComplexityShopProgram");
                    break;
            }
            DataTable dt;
            if (flag != false && dbf != null)
            {
                dt = dbf.Execute(str);
            }
            else
                dt = null;
            return dt;
        }
        #endregion

        #region Создание БД
        /// <summary>
        /// Создание новой БД
        /// </summary>
        /// <param name="DBName">Название и путь к файлу новой создаваемой БД</param>
        public static void CreateNewDB(string DBName)
        {
            // 1. формирование строки запроса для создания таблиц БД
            string query = @"CREATE TABLE 'ProductTypes' (
                                'PKey'  integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                'Name'   varchar(30) NOT NULL
                            );
                            CREATE TABLE 'ReferenceProducts' (
                                'PKey'  integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                'Name'   varchar(30) NOT NULL
                            );
                            CREATE TABLE 'ReferenceUnits' (
                                'PKey'      integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                'FullName'   varchar(20) NOT NULL,
                                'SmallName'  varchar(5) NOT NULL
                            );
                            CREATE TABLE 'ReferenceSigns' (
                                'PKey'  integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                'Name'   varchar(30) NOT NULL
                            );
                            CREATE TABLE 'ReferenceMaterials' (
                                'MaterialCode'  integer PRIMARY KEY NOT NULL,
                                'Name'          varchar(50) NOT NULL,
                                'UnitType'      integer NOT NULL,
                                FOREIGN KEY (UnitType)
                                    REFERENCES 'ReferenceUnits'('PKey')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION
                            );
                            CREATE TABLE 'ReferenceStandarts' (
                                'ProductCode'      integer PRIMARY KEY NOT NULL,
                                'MaterialCode'     integer NOT NULL,
                                'ConsumptionRate'  real NOT NULL DEFAULT 0,
                                'RateOfWaste'      real NOT NULL DEFAULT 0,
                                FOREIGN KEY ('MaterialCode')
                                    REFERENCES 'ReferenceMaterials'('MaterialCode')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION
                            );
                            CREATE TABLE 'ProductNames' (
                                'ProductKey'   integer PRIMARY KEY NOT NULL,
                                'Name'         varchar(50) NOT NULL,
                                'Designation'  varchar(50) NOT NULL,
                                'ViewCode'     integer NOT NULL,
                                'TypeCode'     integer NOT NULL,
                                'SignCode'     integer NOT NULL,
                                FOREIGN KEY ('ProductKey')
                                    REFERENCES 'ReferenceStandarts'('ProductCode')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION, 
                                FOREIGN KEY ('TypeCode')
                                    REFERENCES 'ProductTypes'('PKey')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION, 
                                FOREIGN KEY ('ViewCode')
                                    REFERENCES 'ReferenceProducts'('PKey')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION, 
                                FOREIGN KEY ('SignCode')
                                    REFERENCES 'ReferenceSigns'('PKey')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION
                            );
                            CREATE TABLE 'CompositionProducts' (
                                'RootCode'   integer NOT NULL,
                                'WhereCode'  integer NOT NULL,
                                'WhatCode'   integer NOT NULL,
                                'Count'      integer NOT NULL
                            );
                            CREATE TABLE 'PlanProducts' (
                                'ProductCode'  integer NOT NULL,
                                'PlanCount'    integer NOT NULL,
                                'Month'        integer NOT NULL,
                                'Year'         integer NOT NULL
                            );
                            CREATE TABLE 'ShopsReference' (
                                'ShopCode'       integer NOT NULL,
                                'Name'           varchar(50) NOT NULL,
                                'ProductionCode' integer NOT NULL
                            );
                            CREATE TABLE 'ProductionReference' (
                                'ProductionCode'   integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                'Name'             varchar(50) NOT NULL
                            );";
            CreateComplexityTable(); // создание таблицы СНТИ (структуры)
            CreateComplexityShopTable(); // создание таблицы СНТИЦ (структуры)
            // 2. Создание БД
            dbf = new DatabaseLib.dbFacade(DBName);
            dbf.CreateDatabase(query);
        }

        /// <summary>
        /// Создание новой БД и заполнение ее данными
        /// </summary>
        /// <param name="DBName">Полный путь к новой БД</param>
        public static void CreateDBandFill(string DBName)
        {
            // 1. Создание файла БД и таблиц БД
            CreateNewDB(DBName);
            ConnectToDB(DBName);
            // 2. Заполенение данными таблиц БД
            FillReferenceTables();
            FillTablesFromFiles();
        }
        #endregion

        #region Заполнение данными таблиц
        /// <summary>
        /// Заполнение данными справочных таблиц
        /// </summary>
        private static void FillReferenceTables()
        {
            // заполнение таблицы СВП
            DatabaseLib.ParametersCollection[] Data = new DatabaseLib.ParametersCollection[3];
            Data[0] = new DatabaseLib.ParametersCollection();
            Data[0].Add("Name", "Деталь", DbType.String);
            Data[1] = new DatabaseLib.ParametersCollection();
            Data[1].Add("Name", "Сборочная единица", DbType.String);
            Data[2] = new DatabaseLib.ParametersCollection();
            Data[2].Add("Name", "Изделие", DbType.String);
            dbf.InsertMany("ReferenceProducts", Data);
            // заполнение таблицы СТП
            DatabaseLib.ParametersCollection[] Data2 = new DatabaseLib.ParametersCollection[3];
            Data2[0] = new DatabaseLib.ParametersCollection();
            Data2[0].Add("Name", "Стандартное изделие", DbType.String);
            Data2[1] = new DatabaseLib.ParametersCollection();
            Data2[1].Add("Name", "Унифицированное изделие", DbType.String);
            Data2[2] = new DatabaseLib.ParametersCollection();
            Data2[2].Add("Name", "Уникальное", DbType.String);
            dbf.InsertMany("ProductTypes", Data2);
            // заполнение таблицы СПП
            DatabaseLib.ParametersCollection[] Data3 = new DatabaseLib.ParametersCollection[3];
            Data3[0] = new DatabaseLib.ParametersCollection();
            Data3[0].Add("Name", "Покупное", DbType.String);
            Data3[1] = new DatabaseLib.ParametersCollection();
            Data3[1].Add("Name", "Собственного изготовления", DbType.String);
            Data3[2] = new DatabaseLib.ParametersCollection();
            Data3[2].Add("Name", "Заимствованное", DbType.String);
            dbf.InsertMany("ReferenceSigns", Data3);
            // заполнение таблицы СЕИ
            DatabaseLib.ParametersCollection[] Data4 = new DatabaseLib.ParametersCollection[3];
            Data4[0] = new DatabaseLib.ParametersCollection();
            Data4[0].Add("FullName", "киллограмм", DbType.String);
            Data4[0].Add("SmallName", "кг.", DbType.String);
            Data4[1] = new DatabaseLib.ParametersCollection();
            Data4[1].Add("FullName", "метров", DbType.String);
            Data4[1].Add("SmallName", "м.", DbType.String);
            Data4[2] = new DatabaseLib.ParametersCollection();
            Data4[2].Add("FullName", "штук", DbType.String);
            Data4[2].Add("SmallName", "шт.", DbType.String);
            dbf.InsertMany("ReferenceUnits", Data4);
        }

        /// <summary>
        /// Заполнение данными таблиц данных из файлов
        /// </summary>
        private static void FillTablesFromFiles()
        {
            int i;
            // заполнение таблицы СНиОД
            StandartRow[] std = DataLoad.LoadDataForReferenceStandarts(Application.StartupPath + "\\" + GlobalData.RefStandTableFile);
            DatabaseLib.ParametersCollection[] Data = new DatabaseLib.ParametersCollection[std.Length];
            for (i = 0; i < std.Length; i++)
                Data[i] = Converter.ConvertStandartRowToParameters(std[i]);
            dbf.InsertMany("ReferenceStandarts", Data);
            // заполение таблицы СТМ
            MaterialsRow[] mat = DataLoad.LoadDataForReferenceMaterials(Application.StartupPath + "\\" + GlobalData.RefMatTableFile);
            DatabaseLib.ParametersCollection[] Data2 = new DatabaseLib.ParametersCollection[mat.Length];
            for (i = 0; i < mat.Length; i++)
                Data2[i] = Converter.ConvertMaterialRowToParameters(mat[i]);
            dbf.InsertMany("ReferenceMaterials", Data2);
            // заполнение таблицы СНП
            ProductNameRow[] prod = DataLoad.LoadDataForProductNames(Application.StartupPath + "\\" + GlobalData.ProdNameTableFile);
            DatabaseLib.ParametersCollection[] Data3 = new DatabaseLib.ParametersCollection[prod.Length];
            for (i = 0; i < prod.Length; i++)
                Data3[i] = Converter.ConvertProductNameRowToParameters(prod[i]);
            dbf.InsertMany("ProductNames", Data3);
            // заполнение таблицы СИ
            CompositionRow[] comp = DataLoad.LoadDataForCompositionProducts(Application.StartupPath + "\\" + GlobalData.CompositionProdTableFile);
            DatabaseLib.ParametersCollection[] Data4 = new DatabaseLib.ParametersCollection[comp.Length];
            for (i = 0; i < comp.Length; i++)
                Data4[i] = Converter.ConvertCompositionRowToParameters(comp[i]);
            dbf.InsertMany("CompositionProducts", Data4);
            // заполнение таблицы ПВИ
            PlanProductsRow[] rows = DataLoad.LoadDataForPlanProducts(GlobalVars.AppDir + "\\" + GlobalData.PlanProductsTableFile);
            FillPlanProductsTable(rows);
            // заполнение таблицы "Справочник цехов"
            ShopsReferenceRow[] shops = DataLoad.LoadDataForShopsReference(GlobalVars.AppDir + "\\" + GlobalData.ShopsRefenceTableFile);
            DatabaseLib.ParametersCollection[] Data5 = new DatabaseLib.ParametersCollection[shops.Length];
            for (i = 0; i < shops.Length; i++)
                Data5[i] = Converter.ConvertShopsReferenceRowToParameter(shops[i]);
            dbf.InsertMany("ShopsReference", Data5);
            // заполнение данными таблицы "Справочник видов прозводства"
            ProductionReferenceRow[] prodref = DataLoad.LoadDataForProductionReference(GlobalVars.AppDir + "\\" + GlobalData.ProductionReferenceTableFile);
            DatabaseLib.ParametersCollection[] Data6 = new DatabaseLib.ParametersCollection[prodref.Length];
            for (i = 0; i < prodref.Length; i++)
                Data6[i] = Converter.ConvertProductionReferenceRowToParameter(prodref[i]);
            dbf.InsertMany("ProductionReference", Data6);
            // формирование таблицы СНТИ
            DBWorker.CreateAndFillComplexityTableFromFile();
            // формирование таблицы СНТИЦ
            DBWorker.CreateAndFillComplexityShopTableFromFile();
        }
        #endregion

        #region Вставка данных
        /// <summary>
        /// Вставка данных в таблицы по ее названию (тегу)
        /// </summary>
        /// <param name="TableTag">Тэг, описывающий таблицу в которую необходимо вставить данные</param>
        /// <param name="Data">Данные для вставки в таблицу</param>
        public static void InsertDataRowToTable(string TableTag, object Data)
        {
            DatabaseLib.ParametersCollection par = null;// = new DatabaseLib.ParametersCollection();
            string TableName = ""; // название таблицы для вставки (как в БД)

            switch (TableTag)
            {
                case "СВП":
                    TableName = GlobalData.TableNames[0];
                    par = Converter.ConvertProductRowToParameters((ProductRow)Data);
                    break;
                case "СТП":
                    TableName = GlobalData.TableNames[1];
                    par = Converter.ConvertTypeRowToParameters((TypeRow)Data);
                    break;
                case "СПП":
                    TableName = GlobalData.TableNames[2];
                    par = Converter.ConvertSignRowToParameters((SignRow)Data);
                    break;
                case "СЕИ":
                    TableName = GlobalData.TableNames[3];
                    par = Converter.ConvertUnitRowToParameters((UnitRow)Data);
                    break;
                case "СНП":
                    TableName = GlobalData.TableNames[4];
                    par = Converter.ConvertProductNameRowToParameters((ProductNameRow)Data);
                    break;
                case "СТМ":
                    TableName = GlobalData.TableNames[5];
                    par = Converter.ConvertMaterialRowToParameters((MaterialsRow)Data);
                    break;
                case "СНиОД":
                    TableName = GlobalData.TableNames[6];
                    par = Converter.ConvertStandartRowToParameters((StandartRow)Data);
                    break;
                case "СИ": // вставка в таблицу "Состав изделий"
                    TableName = "CompositionProducts";
                    par = Converter.ConvertCompositionRowToParameters((CompositionRow)Data);
                    break;
                case "ПВИ": // вставка в таблицу "План выпуска изделий"
                    TableName = "PlanProducts";
                    par = Converter.ConvertPlanProductsRowToParameters((PlanProductsRow)Data);
                    break;
            }
            dbf.Insert(TableName, par);
        }
        #endregion

        #region Удаление данных
        /// <summary>
        /// Удаление строки из таблицы, заданной тегом, по ключу
        /// </summary>
        /// <param name="TableTag">Тэг, описывающий таблицу из которой необходимо удалить данные</param>
        /// <param name="key">Ключ для условия удаления DELETE FROM ... WHERE @ColumnName@=key</param>
        /// <returns>Возвращает True, если выполнено успешно без ошибок, в противном случае - False</returns>
        public static bool DeleteRowFromTable(string TableTag, long key)
        {
            bool flag = false;
            string TableName = ""; // название таблицы для вставки (как в БД)
            string Where = "";     // условие для SQL-запроса WHERE

            switch (TableTag)
            {
                case "СВП":
                    TableName = GlobalData.TableNames[0];
                    Where = String.Format("PKey={0}", key);
                    break;
                case "СТП":
                    TableName = GlobalData.TableNames[1];
                    Where = String.Format("PKey={0}", key);
                    break;
                case "СПП":
                    TableName = GlobalData.TableNames[2];
                    Where = String.Format("PKey={0}", key);
                    break;
                case "СЕИ":
                    TableName = GlobalData.TableNames[3];
                    Where = String.Format("PKey={0}", key);
                    break;
                case "СНП":
                    TableName = GlobalData.TableNames[4];
                    Where = String.Format("ProductKey={0}", key);
                    break;
                case "СТМ":
                    TableName = GlobalData.TableNames[5];
                    Where = String.Format("MaterialCode={0}", key);
                    break;
                case "СНиОД":
                    TableName = GlobalData.TableNames[6];
                    Where = String.Format("ProductCode={0}", key);
                    break;
            }
            int ErrorCode = dbf.Delete(TableName, Where);
            if (ErrorCode == 0) // нет ошибок, успешное выполнение
                flag = true;

            return flag;
        }

        /// <summary>
        /// Удаление строки из любой таблицы по ключю
        /// </summary>
        /// <param name="TableName">Название таблицы (как в БД на латинице)</param>
        /// <param name="Where">Строка условия SQL-запроса WHERE</param>
        /// <param name="key">Ключ</param>
        /// <returns>Возвращает True, если выполнено успешно без ошибок, в противном случае - False</returns>
        public static bool DeleteRow(string TableName, string Where)
        {
            bool flag = false;

            int ErrorCode = dbf.Delete(TableName, Where);
            if (ErrorCode == 0) // нет ошибок, успешное выполнение
                flag = true;

            return flag;
        }
        #endregion

        #region Обновление данных
        /// <summary>
        /// Обновление данных в таблице по ключу
        /// </summary>
        /// <param name="TableTag">Тэг, описывающий таблицу, в которой необходимо произвести обновление строки</param>
        /// <param name="key">Ключ для условия обновления UPDATE ... WHERE @ColumnName@=key</param>
        /// <returns>Возвращает True, если выполнено успешно без ошибок, в противном случае - False</returns>
        public static bool UpdateDataInRow(string TableTag, long key, DatabaseLib.ParametersCollection par)
        {
            bool flag = false;
            string TableName = ""; // название таблицы для вставки (как в БД)
            string Where = "";     // условие для SQL-запроса WHERE

            switch (TableTag)
            {
                case "СВП":
                    TableName = GlobalData.TableNames[0];
                    Where = String.Format("PKey={0}", key);
                    break;
                case "СТП":
                    TableName = GlobalData.TableNames[1];
                    Where = String.Format("PKey={0}", key);
                    break;
                case "СПП":
                    TableName = GlobalData.TableNames[2];
                    Where = String.Format("PKey={0}", key);
                    break;
                case "СЕИ":
                    TableName = GlobalData.TableNames[3];
                    Where = String.Format("PKey={0}", key);
                    break;
                case "СНП":
                    TableName = GlobalData.TableNames[4];
                    Where = String.Format("ProductKey={0}", key);
                    break;
                case "СТМ":
                    TableName = GlobalData.TableNames[5];
                    Where = String.Format("MaterialCode={0}", key);
                    break;
                case "СНиОД":
                    TableName = GlobalData.TableNames[6];
                    Where = String.Format("ProductCode={0}", key);
                    break;
            }
            int ErrorCode = dbf.Update(TableName, par, Where);
            if (ErrorCode == 0) // нет ошибок, успешное выполнение
                flag = true;

            return flag;
        }

        /// <summary>
        /// Обновление строки в заданной таблице с заданным условием
        /// </summary>
        /// <param name="TableName">Название таблицы (как в БД латиницей)</param>
        /// <param name="Where">Условие SQL-запроса WHERE</param>
        /// <returns>Возвращает True, если выполнено успешно без ошибок, в противном случае - False</returns>
        public static bool UpdateRow(string TableName, string Where, DatabaseLib.ParametersCollection par)
        {
            bool flag = false;

            int ErrorCode = dbf.Update(TableName, par, Where);
            if (ErrorCode == 0) // нет ошибок, успешное выполнение
                flag = true;

            return flag;
        }
        #endregion

        #region Работа с таблицей БД "Полная применяемость"
        /// <summary>
        /// Создание в текущей БД временной таблицы "Полная применяемость"
        /// </summary>
        public static void CreateTableFullApplication()
        {
        	// 1. Формирование запроса на создание таблицы
            string query = @"CREATE TABLE 'FullApplication' (
                                'ProductCode'     integer NOT NULL,
                                'PackageDetails'  integer NOT NULL,
                                'Count'           integer NOT NULL
                            );";
            // 2. Выполнение запроса
            dbf.Execute(query);
            IsItFATable = true;
        }

        /// <summary>
        /// Удаление таблицы "Полная применяемость" из БД
        /// </summary>
        public static void DeleteFullApplicationTable()
        {
            dbf.Execute("DROP TABLE 'FullApplication'");
            IsItFATable = false;
        }

        /// <summary>
        /// Заполнение данными таблицы "Полная применяемость"
        /// </summary>
        /// <param name="data">Данные, для вставки в таблицу</param>
        public static void FillFullApplicationTable(FullApplicationRow[] data)
        {
            DatabaseLib.ParametersCollection[] Pars = new DatabaseLib.ParametersCollection[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                Pars[i] = Converter.ConvertFullApplicationRowToParameters(data[i]);
            }
            dbf.InsertMany("FullApplication", Pars);
        }

        /// <summary>
        /// Создание таблицы "Полная применяемость" и ее заполнение 
        /// </summary>
        /// <param name="data">Данные, для вставки в таблицу</param>
        public static void CreateAndFillFullApplicationTable(FullApplicationRow[] data)
        {
            CreateTableFullApplication();
            FillFullApplicationTable(data);
            IsItFATable = true;
        }
        #endregion

        #region Работа с таблицей БД "Сводные нормы расхода материалов на изделие"
        /// <summary>
        /// Создание таблицы БД "Сводные нормы расхода материалов на изделие"
        /// </summary>
        public static void CreateProdMatStandTable()
        {
            // 1. Формирование запроса на создание таблицы
            string query = @"CREATE TABLE 'ProdNameStand' (
                                'ProductCode'     integer NOT NULL,
                                'MaterialCode'    integer NOT NULL,
                                'Consumption'     real NOT NULL,
                                'Wastes'          real NOT NULL
                            );";
            // 2. Выполнение запроса
            dbf.Execute(query);
            IsItPNSTable = true;
        }

        /// <summary>
        /// Удаление из БД таблицы "Сводные нормы расхода материалов на изделие"
        /// </summary>
        public static void DropProdMatStandTable()
        {
            dbf.Execute("DROP TABLE 'ProdNameStand'");
            IsItPNSTable = false;
        }

        /// <summary>
        /// Заполнение данными таблицы "Сводные нормы расхода материалов на изделие"
        /// </summary>
        /// <param name="data">Массив строк для таблицы с данными</param>
        public static void FillProdMatStandTable(ProductMaterialStandartsRow[] data)
        {
            DatabaseLib.ParametersCollection[] Pars = new DatabaseLib.ParametersCollection[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                Pars[i] = Converter.ConverеProductMaterialStandartsRowToParameters(data[i]);
            }
            dbf.InsertMany("ProdNameStand", Pars);
        }

        /// <summary>
        /// Заполнение данными таблицы "Сводные нормы расходма материалов на изделие"
        /// </summary>
        /// <param name="data">Данные, полученные запросом из БД</param>
        public static void FillProdMatStandTable(DataTable dt)
        {
            DatabaseLib.ParametersCollection[] Pars = Converter.ConverеProductMaterialStandartsRowToParameters(dt);
            dbf.InsertMany("ProdNameStand", Pars);
        }

        public static void CreateAndFillPradNameStandTable(ProductMaterialStandartsRow[] data)
        {
            CreateProdMatStandTable();
            FillProdMatStandTable(data);
            IsItPNSTable = true;
        }

        public static void CreateAndFillPradNameStandTable(DataTable dt)
        {
            CreateProdMatStandTable();
            FillProdMatStandTable(dt);
            IsItPNSTable = true;
        }

        #endregion

        #region Работа с таблицей БД "Сводная деталей по материалам для изделия"
        /// <summary>
        /// Создание таблицы БД "Сводная деталей по материалам для изделия"
        /// </summary>
        public static void CreateDetMatOnProdTable()
        {
            // 1. Формирование запроса на создание таблицы
            string query = @"CREATE TABLE 'DetMatOnProd' (
                                'ProductCode'     integer NOT NULL,
                                'MaterialCode'    integer NOT NULL,
                                'DetailCode'      integer NOT NULL,
                                'Count'           integer NOT NULL,
                                'Consumption'     real NOT NULL DEFAULT 0,
                                'Wastes'          real NOT NULL DEFAULT 0
                            );";
            // 2. Выполнение запроса
            dbf.Execute(query);
            IsItDMPTable = true;
        }

        /// <summary>
        /// Удаление из БД таблицы "Сводная деталей по материалам для изделия"
        /// </summary>
        public static void DropDetMatOnProdTable()
        {
            dbf.Execute("DROP TABLE 'DetMatOnProd'");
            IsItDMPTable = false;
        }

        /// <summary>
        /// Заполнение данными таблицы "Сводная деталей по материалам для изделия"
        /// </summary>
        /// <param name="dt">Таблица с данными для заполенения</param>
        public static void FillDetMatOnProdTable(DataTable dt)
        {
            DatabaseLib.ParametersCollection[] Pars = Converter.ConverеDetailsMaterialOnProductRowToParameters(dt);
            dbf.InsertMany("DetMatOnProd", Pars);
        }

        /// <summary>
        /// Создание и заполение ТБД "Сводная деталей по материалам для изделия"
        /// </summary>
        /// <param name="dt">Таблица с данными для сохранения</param>
        public static void CreateAndFillDetMatOnProdTable(DataTable dt)
        {
            CreateDetMatOnProdTable();
            FillDetMatOnProdTable(dt);
        }
        #endregion

        #region Работа с таблицей БД "План выпуска изделий"
        /// <summary>
        /// Создание таблицы БД "План выпуска изделий"
        /// </summary>
        public static void CreatePlanProductsTable()
        {
            // 1. Формирование запроса на создание таблицы
            string query = @"CREATE TABLE 'PlanProducts' (
                                'ProductCode'  integer NOT NULL,
                                'PlanCount'    integer NOT NULL,
                                'Month'        integer NOT NULL,
                                'Year'         integer NOT NULL
                            );";
            // 2. Выполнение запроса
            dbf.Execute(query);
        }

        /// <summary>
        /// Удаление из БД таблицы "План выпуска изделий"
        /// </summary>
        public static void DropPlanProductsTable()
        {
            dbf.Execute("DROP TABLE 'PlanProducts'");
        }

        public static void FillPlanProductsTable(PlanProductsRow[] data)
        {
            DatabaseLib.ParametersCollection[] pars = new DatabaseLib.ParametersCollection[data.Length];
            for (int i = 0; i < data.Length; i++)
                pars[i] = Converter.ConvertPlanProductsRowToParameters(data[i]);
            dbf.InsertMany("PlanProducts", pars);
        }

        public static void CreateAndFillPlanProductsTable(PlanProductsRow[] data)
        {
            CreatePlanProductsTable();
            FillPlanProductsTable(data);
        }

        public static void CreateAndFillPlanProductsTableFromFile()
        {
            PlanProductsRow[] rows = DataLoad.LoadDataForPlanProducts(GlobalVars.AppDir + "\\" + GlobalData.PlanProductsTableFile);
            CreateAndFillPlanProductsTable(rows);
        }
        #endregion

        #region Работа с таблицей БД "Потребность в материалах на товарный выпуск"
        /// <summary>
        /// Создание таблицы БД "Потребность в материалах на товарный выпуск"
        /// </summary>
        public static void CreateMaterialsNeedsTable()
        {
            // 1. Формирование запроса на создание таблицы
            string query = @"CREATE TABLE 'MaterialsNeeds' (
                                'ProductCode'   integer NOT NULL,
                                'MaterialCode'  integer NOT NULL,
                                'PlanCount'     real NOT NULL,
                                'Month'         integer NOT NULL,
                                'Year'          integer NOT NULL
                            );";
            // 2. Выполнение запроса
            dbf.Execute(query);
            IsItMNTable = true;
        }

        /// <summary>
        /// Удаление из БД таблицы "План выпуска изделий"
        /// </summary>
        public static void DropMaterialsNeedsTable()
        {
            dbf.Execute("DROP TABLE 'MaterialsNeeds'");
            IsItMNTable = false;
        }

        public static void FillMaterialsNeedsTable(MaterialsNeedsRow[] data)
        {
            DatabaseLib.ParametersCollection[] pars = Converter.ConvertMaterialsNeedsRowToParameters(data);
            dbf.InsertMany("MaterialsNeeds", pars);
        }

        public static void CreateAndFillMaterialsNeedsTable(MaterialsNeedsRow[] data)
        {
            CreateMaterialsNeedsTable();
            FillMaterialsNeedsTable(data);
        }
        #endregion
        
        #region Работа с таблицей БД "Потребность в материалах на товарный выпуск" табл.2
        /// <summary>
        /// Создание таблицы БД "Потребность в материалах на товарный выпуск"
        /// </summary>
        public static void CreateMaterialsNeeds2Table()
        {
            // 1. Формирование запроса на создание таблицы
            string query = @"CREATE TABLE 'MaterialsNeeds2' (
                                'MaterialCode'  integer NOT NULL,
                                'PlanCount'     real NOT NULL,
                                'Month'         integer NOT NULL,
                                'Year'          integer NOT NULL
                            );";
            // 2. Выполнение запроса
            dbf.Execute(query);
            IsItMN2Table = true;
        }

        /// <summary>
        /// Удаление из БД таблицы "План выпуска изделий"
        /// </summary>
        public static void DropMaterialsNeeds2Table()
        {
            dbf.Execute("DROP TABLE 'MaterialsNeeds2'");
            IsItMN2Table = false;
        }

        public static void FillMaterialsNeeds2Table(MaterialsNeeds2Row[] data)
        {
            DatabaseLib.ParametersCollection[] pars = Converter.ConvertMaterialsNeeds2RowToParameters(data);
            dbf.InsertMany("MaterialsNeeds2", pars);
        }

        public static void CreateAndFillMaterialsNeeds2Table(MaterialsNeeds2Row[] data)
        {
            CreateMaterialsNeeds2Table();
            FillMaterialsNeeds2Table(data);
        }
        #endregion

        #region Работа с таблицей БД "Сводная нормативная трудоемкость на изделие"
        /// <summary>
        /// Cоздание таблицы БД "Сводная нормативная трудоемкость на изделие"
        /// </summary>
        public static void CreateComplexityTable()
        {
            // 1. Формирование запроса на создание таблицы
            string query = @"CREATE TABLE 'Complexity' (
                                'ProductCode'  integer NOT NULL PRIMARY KEY,
                                'T0'           real NOT NULL,
                                'Tv'           real NOT NULL,
                                'Tpz'          real NOT NULL,
                                'Totl'         real NOT NULL,
                                'Tpt'          real NOT NULL,
                                'Tobs'         real NOT NULL
                            );";
            // 2. Выполнение запроса
            dbf.Execute(query);
            //IsItCTable = true;
        }

        /// <summary>
        /// Удаление из БД таблицы "СНТИ"
        /// </summary>
        public static void DropComplexityTable()
        {
            dbf.Execute("DROP TABLE 'Complexity'");
            //IsItCTable = false;
        }

        /// <summary>
        /// Заполнение данными таблицы "СНТИ"
        /// </summary>
        /// <param name="data">Массив строк с данными для заполнения таблицы</param>
        public static void FillComplexityTable(ComplexityRow[] data)
        {
            DatabaseLib.ParametersCollection[] pars = Converter.ConvertComplexityRowToParameters(data);
            dbf.InsertMany("Complexity", pars);
        }

        /// <summary>
        /// Создание ТБД "СНТИ" и ее заполнение
        /// </summary>
        /// <param name="data">Массив строк с данными для заполнения таблицы</param>
        public static void CreateAndFillComplexityTable(ComplexityRow[] data)
        {
            CreateComplexityTable();
            FillComplexityTable(data);
        }

        /// <summary>
        /// Создание и заполение из файла таблицы "СНТИ" данными
        /// </summary>
        public static void CreateAndFillComplexityTableFromFile()
        {
            ComplexityRow[] rows = DataLoad.LoadDataForComplexity(GlobalVars.AppDir + "\\" + GlobalData.ComplexityTableFile);
            CreateAndFillComplexityTable(rows);
        }
        #endregion

        #region Работа с таблицей БД "Сводная нормативная трудоемкость на изделие по цехам"
        /// <summary>
        /// Cоздание таблицы БД "Сводная нормативная трудоемкость на изделие по цехам"
        /// </summary>
        public static void CreateComplexityShopTable()
        {
            // 1. Формирование запроса на создание таблицы
            string query = @"CREATE TABLE 'ComplexityShop' (
                                'ProductCode'  integer NOT NULL,
                                'ShopCode'     integer NOT NULL,
                                'T0'           real NOT NULL,
                                'Tv'           real NOT NULL,
                                'Tpz'          real NOT NULL,
                                'Totl'         real NOT NULL,
                                'Tpt'          real NOT NULL,
                                'Tobs'         real NOT NULL
                            );";
            // 2. Выполнение запроса
            dbf.Execute(query);
            //IsItCHTable = true;
        }

        /// <summary>
        /// Удаление из БД таблицы "СНТИЦ"
        /// </summary>
        public static void DropComplexityShopTable()
        {
            dbf.Execute("DROP TABLE 'ComplexityShop'");
            //IsItCHTable = false;
        }

        /// <summary>
        /// Заполнение данными таблицы "СНТИЦ"
        /// </summary>
        /// <param name="data">Массив строк с данными для заполнения таблицы</param>
        public static void FillComplexityShopTable(ComplexityShopRow[] data)
        {
            DatabaseLib.ParametersCollection[] pars = Converter.ConvertComplexityShopRowToParameters(data);
            dbf.InsertMany("ComplexityShop", pars);
        }

        /// <summary>
        /// Создание ТБД "СНТИЦ" и ее заполнение
        /// </summary>
        /// <param name="data">Массив строк с данными для заполнения таблицы</param>
        public static void CreateAndFillComplexityShopTable(ComplexityShopRow[] data)
        {
            CreateComplexityShopTable();
            FillComplexityShopTable(data);
        }

        /// <summary>
        /// Создание и заполение из файла таблицы "СНТИЦ" данными
        /// </summary>
        public static void CreateAndFillComplexityShopTableFromFile()
        {
            ComplexityShopRow[] rows = DataLoad.LoadDataForComplexityShop(GlobalVars.AppDir + "\\" + GlobalData.ComplexityShopTableFile);
            CreateAndFillComplexityShopTable(rows);
        }
        #endregion

        #region Работа с таблицей БД "Нормативная трудоемкость производственной программы"
        /// <summary>
        /// Cоздание таблицы БД "Нормативная трудоемкость производственной программы"
        /// </summary>
        public static void CreateComplexityProgramTable()
        {
            // 1. Формирование запроса на создание таблицы
            string query = @"CREATE TABLE 'ComplexityProgram' (
                                'ProductCode'  integer NOT NULL,
                                'PlanCount'    integer NOT NULL,
                                'Tshk'         real NOT NULL,
                                'Month'        integer NOT NULL,
                                'Year'         integer NOT NULL
                            );";
            // 2. Выполнение запроса
            dbf.Execute(query);
            IsItCPTable = true;
        }

        /// <summary>
        /// Удаление из БД таблицы "НТПП"
        /// </summary>
        public static void DropComplexityProgramTable()
        {
            dbf.Execute("DROP TABLE 'ComplexityProgram'");
            IsItCPTable = false;
        }

        /// <summary>
        /// Заполнение данными таблицы "НТПП"
        /// </summary>
        /// <param name="data">Массив строк с данными для заполнения таблицы</param>
        public static void FillComplexityProgramTable(DataTable data)
        {
            DatabaseLib.ParametersCollection[] pars = Converter.ConvertComplexityProgramDataTableToParameters(data);
            dbf.InsertMany("ComplexityProgram", pars);
        }

        /// <summary>
        /// Создание ТБД "НТПП" и ее заполнение
        /// </summary>
        /// <param name="data">Массив строк с данными для заполнения таблицы</param>
        public static void CreateAndFillComplexityProgramTable(DataTable data)
        {
            CreateComplexityProgramTable();
            FillComplexityProgramTable(data);
        }
        #endregion

        #region Работа с таблицей БД "Нормативная трудоемкость производственной программы по цехам"
        /// <summary>
        /// Cоздание таблицы БД "Нормативная трудоемкость производственной программы по цехам"
        /// </summary>
        public static void CreateComplexityShopProgramTable()
        {
            // 1. Формирование запроса на создание таблицы
            string query = @"CREATE TABLE 'ComplexityShopProgram' (
                                'ProductCode'  integer NOT NULL,
                                'ShopCode'     integer NOT NULL,
                                'PlanCount'    integer NOT NULL,
                                'Tshk'         real NOT NULL,
                                'Month'        integer NOT NULL,
                                'Year'         integer NOT NULL
                            );";
            // 2. Выполнение запроса
            dbf.Execute(query);
            IsItCSPTable = true;
        }

        /// <summary>
        /// Удаление из БД таблицы "НТППЦ"
        /// </summary>
        public static void DropComplexityShopProgramTable()
        {
            dbf.Execute("DROP TABLE 'ComplexityShopProgram'");
            IsItCSPTable = false;
        }

        /// <summary>
        /// Заполнение данными таблицы "НТППЦ"
        /// </summary>
        /// <param name="data">Массив строк с данными для заполнения таблицы</param>
        public static void FillComplexityShopProgramTable(DataTable data)
        {
            DatabaseLib.ParametersCollection[] pars = Converter.ConvertComplexityShopProgramDataTableToParameters(data);
            dbf.InsertMany("ComplexityShopProgram", pars);
        }

        /// <summary>
        /// Создание ТБД "НТППЦ" и ее заполнение
        /// </summary>
        /// <param name="data">Массив строк с данными для заполнения таблицы</param>
        public static void CreateAndFillComplexityShopProgramTable(DataTable data)
        {
            CreateComplexityShopProgramTable();
            FillComplexityShopProgramTable(data);
        }
        #endregion
    }

    #region Класс DataLoad для загрузки данных из файлов
    // класс для реализации методов чтения из файлов данных для заполнения таблиц
    public static class DataLoad
    {
        /// <summary>
        /// Загрузка данных из файла для таблицы "Справочник технических материалов"
        /// </summary>
        /// <param name="fname">Полный путь к тектовому файлу с данными для заполнения</param>
        /// <returns>Возвращает массив структур struct MaterialsRow, описывающий строки таблицы "Справочник материалов"</returns>
        public static MaterialsRow[] LoadDataForReferenceMaterials(string fname)
        {
            StreamReader file = new StreamReader(fname);
            int count = 0;
            string[] mas = new string[0];
            while (!file.EndOfStream)
            {
                string str = file.ReadLine();
                count++;
                Array.Resize(ref mas, count);
                mas[count - 1] = str;
            }
            file.Close();

            MaterialsRow[] Mat = new MaterialsRow[count];
            for (int i = 0; i < count; i++)
            {
                string[] tmp = mas[i].Split('\t');
                Mat[i].Code = Convert.ToInt64(tmp[0]);
                Mat[i].Designation = tmp[1];
                Mat[i].UnitCode = Convert.ToInt32(tmp[2]);
            }

            return Mat;
        }

        /// <summary>
        /// Загрузка данных из файла для таблицы "Справочник норм и расходов материала"
        /// </summary>
        /// <param name="fname">Полный путь к тектовому файлу с данными для заполнения</param>
        /// <returns>Возвращает массив структур StandartRow с данными строк таблицы СНиОД</returns>
        public static StandartRow[] LoadDataForReferenceStandarts(string fname)
        {
            StreamReader file = new StreamReader(fname);
            int count = 0;
            string[] mas = new string[0];
            while (!file.EndOfStream)
            {
                string str = file.ReadLine();
                count++;
                Array.Resize(ref mas, count);
                mas[count - 1] = str;
            }
            file.Close();

            StandartRow[] Std = new StandartRow[count];
            for (int i = 0; i < count; i++)
            {
                string[] tmp = mas[i].Split('\t');
                Std[i].ProductCode = Convert.ToInt64(tmp[0]);
                Std[i].MaterialCode = Convert.ToInt64(tmp[1]);
                Std[i].ConsumptionRate = Convert.ToSingle(tmp[2]);
                Std[i].RateOfWaste = Convert.ToSingle(tmp[3]);
            }

            return Std;
        }

        /// <summary>
        /// Загрузка данных из файла для таблицы "Справочник наименований продукции"
        /// </summary>
        /// <param name="fname">Полный путь к тектовому файлу с данными для заполнения</param>
        /// <returns>Возвращает массив структур struct ProductNameRow</returns>
        public static ProductNameRow[] LoadDataForProductNames(string fname)
        {
            StreamReader file = new StreamReader(fname);
            int count = 0;
            string[] mas = new string[0];
            while (!file.EndOfStream)
            {
                string str = file.ReadLine();
                count++;
                Array.Resize(ref mas, count);
                mas[count - 1] = str;
            }
            file.Close();

            ProductNameRow[] Prod = new ProductNameRow[count];
            for (int i = 0; i < count; i++)
            {
                string[] tmp = mas[i].Split('\t');
                Prod[i].Code = Convert.ToInt64(tmp[0]);
                Prod[i].Designation = tmp[1];
                Prod[i].Name = tmp[2];
                Prod[i].ProductCode = Convert.ToInt32(tmp[3]);
                Prod[i].TypeCode = Convert.ToInt32(tmp[4]);
                Prod[i].SignCode = Convert.ToInt32(tmp[5]);
            }

            return Prod;
        }

        /// <summary>
        /// Загрузка данных из файла для таблицы "Состав изделий"
        /// </summary>
        /// <param name="fname">Полный путь к файлу, содержащим данные для таблицы</param>
        /// <returns>Возвращает массив структур CompositionRow</returns>
        public static CompositionRow[] LoadDataForCompositionProducts(string fname)
        {
            StreamReader file = new StreamReader(fname);
            int count = 0;
            string[] mas = new string[0];
            while (!file.EndOfStream)
            {
                string str = file.ReadLine();
                count++;
                Array.Resize(ref mas, count);
                mas[count - 1] = str;
            }
            file.Close();

            CompositionRow[] comp = new CompositionRow[count];
            for (int i = 0; i < count; i++)
            {
                string[] tmp = mas[i].Split('\t');
                comp[i].RootCode = Convert.ToInt64(tmp[0]);
                comp[i].WhereCode = Convert.ToInt64(tmp[1]);
                comp[i].WhatCode = Convert.ToInt64(tmp[2]);
                comp[i].Count = Convert.ToInt32(tmp[3]);
            }

            return comp;
        }

        /// <summary>
        /// Загрузка данных из файла для таблицы "План выпуска изделий"
        /// </summary>
        /// <param name="fname">Полный путь к файлу, содержащим данные для таблицы</param>
        /// <returns>Возвращает массив структур PlanProductsRow</returns>
        public static PlanProductsRow[] LoadDataForPlanProducts(string fname)
        {
            StreamReader file = new StreamReader(fname);
            int count = 0;
            string[] mas = new string[0];
            while (!file.EndOfStream)
            {
                string str = file.ReadLine();
                count++;
                Array.Resize(ref mas, count);
                mas[count - 1] = str;
            }
            file.Close();

            PlanProductsRow[] prod = new PlanProductsRow[count];
            for (int i = 0; i < count; i++)
            {
                string[] tmp = mas[i].Split('\t');
                prod[i].ProductCode = Convert.ToInt64(tmp[0]);
                prod[i].PlanCount = Convert.ToInt32(tmp[1]);
                prod[i].Month = Convert.ToInt32(tmp[2]);
                prod[i].Year = Convert.ToInt32(tmp[3]);
            }

            return prod;
        }

        /// <summary>
        /// Загрузка данных из файла для таблицы "Сводная нормативная трудоемкость на изделие"
        /// </summary>
        /// <param name="fname">Полный путь и имя файла с данными</param>
        /// <returns>Возвращает массив структур, описывающих данные таблицы</returns>
        public static ComplexityRow[] LoadDataForComplexity(string fname)
        {
            StreamReader file = new StreamReader(fname);
            int count = 0;
            string[] mas = new string[0];
            while (!file.EndOfStream)
            {
                string str = file.ReadLine();
                count++;
                Array.Resize(ref mas, count);
                mas[count - 1] = str;
            }
            file.Close();

            ComplexityRow[] comp = new ComplexityRow[count];
            for (int i = 0; i < count; i++)
            {
                string[] tmp = mas[i].Split('\t');
                comp[i].ProductCode = Convert.ToInt64(tmp[0]);
                comp[i].To = Convert.ToSingle(tmp[1]);
                comp[i].Tv = Convert.ToSingle(tmp[2]);
                comp[i].Tpz = Convert.ToSingle(tmp[3]);
                comp[i].Totl = Convert.ToSingle(tmp[4]);
                comp[i].Tpt = Convert.ToSingle(tmp[5]);
                comp[i].Tobs = Convert.ToSingle(tmp[6]);
            }

            return comp;
        }

        /// <summary>
        /// Загрузка данных из файла для таблицы "Сводная нормативная трудоемкость на изделие"
        /// </summary>
        /// <param name="fname">Полный путь и имя файла с данными</param>
        /// <returns>Возвращает массив структур, описывающих данные таблицы</returns>
        public static ComplexityShopRow[] LoadDataForComplexityShop(string fname)
        {
            StreamReader file = new StreamReader(fname);
            int count = 0;
            string[] mas = new string[0];
            while (!file.EndOfStream)
            {
                string str = file.ReadLine();
                count++;
                Array.Resize(ref mas, count);
                mas[count - 1] = str;
            }
            file.Close();

            ComplexityShopRow[] comp = new ComplexityShopRow[count];
            for (int i = 0; i < count; i++)
            {
                string[] tmp = mas[i].Split('\t');
                comp[i].ProductCode = Convert.ToInt64(tmp[0]);
                comp[i].ShopCode = Convert.ToInt32(tmp[1]);
                comp[i].To = Convert.ToSingle(tmp[2]);
                comp[i].Tv = Convert.ToSingle(tmp[3]);
                comp[i].Tpz = Convert.ToSingle(tmp[4]);
                comp[i].Totl = Convert.ToSingle(tmp[5]);
                comp[i].Tpt = Convert.ToSingle(tmp[6]);
                comp[i].Tobs = Convert.ToSingle(tmp[7]);
            }

            return comp;
        }

        /// <summary>
        ////Загрузка данных из файла для таблицы "Справочник цехов"
        /// </summary>
        /// <param name="fname">Полный путь и имя файла с данными для таблицы СЦ</param>
        /// <returns>Возвращает массив данных для строк таблицы СЦ</returns>
        internal static ShopsReferenceRow[] LoadDataForShopsReference(string fname)
        {
            StreamReader file = new StreamReader(fname);
            int count = 0;
            string[] mas = new string[0];
            while (!file.EndOfStream)
            {
                string str = file.ReadLine();
                count++;
                Array.Resize(ref mas, count);
                mas[count - 1] = str;
            }
            file.Close();

            ShopsReferenceRow[] shops = new ShopsReferenceRow[count];
            for (int i = 0; i < count; i++)
            {
                string[] tmp = mas[i].Split('\t');
                shops[i].ShopCode = Convert.ToInt32(tmp[0]);
                shops[i].Name = tmp[1];
                shops[i].ProductionCode = Convert.ToInt32(tmp[2]);
            }

            return shops;
        }

        /// <summary>
        /// Загрузка из файла данных для таблицы "Справочник видов производства" СВП
        /// </summary>
        /// <param name="fname">Путь и название файла с данными для таблицы СВП</param>
        /// <returns>Возвращает массив</returns>
        internal static ProductionReferenceRow[] LoadDataForProductionReference(string fname)
        {
            StreamReader file = new StreamReader(fname);
            int count = 0;
            string[] mas = new string[0];
            while (!file.EndOfStream)
            {
                string str = file.ReadLine();
                count++;
                Array.Resize(ref mas, count);
                mas[count - 1] = str;
            }
            file.Close();

            ProductionReferenceRow[] prod = new ProductionReferenceRow[count];
            for (int i = 0; i < count; i++)
            {
                prod[i].Name = mas[i].Trim();
            }

            return prod;
        }
    }
    #endregion
}
