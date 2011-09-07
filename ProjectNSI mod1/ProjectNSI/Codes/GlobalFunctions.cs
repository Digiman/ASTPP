//***************************************************************************************
// Модуль, содержащий глобальные функции и методы, часто используемые в программе
//***************************************************************************************
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ProjectNSI
{
    #region Класс с общими для программы действиями
    /// <summary>
    /// Общие функции программы
    /// </summary>
    public static class GlobalFunctions
    {
        // список мемяцев для формирования строк по коду месяца
        public static string[] Monthes = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

        #region Обработка файлов
        /// <summary>
        /// Выделение из строки с путем к файлу имени файла и его расширения
        /// </summary>
        /// <param name="str">Строка с полным путем к файлу</param>
        /// <returns>Вовращает строку с именем файла и его расширением</returns>
        public static string ExtactFileName(string str)
        {
            int ind = str.LastIndexOf("\\");
            str = str.Substring(ind + 1);
            return str;
        }

        /// <summary>
        /// Метод для извлечения имени из имени файла без расширения
        /// </summary>
        /// <param name="str">Строка с полным путем к файлу</param>
        /// <returns>Возврящает имя файла без расширения</returns>
        public static string GetFileName(string str)
        {
            string[] s = str.Split('.');
            return s[0];
        } 
        #endregion

        /// <summary>
        /// Построение строки с названием месяца и его номером
        /// </summary>
        /// <param name="month">Номер месяца</param>
        /// <returns>Возвращает строку вида: [название месяца] (номер)</returns>
        public static string GetMonthString(int month)
        {
            return String.Format("{0} ({1})", Monthes[month - 1], month);
        }

        /// <summary>
        /// Функция для уменьшения (обрезки) массива из строк ТБД CИ по индексу
        /// </summary>
        /// <param name="Rows">Массив строк таблицы Состав изделий</param>
        /// <param name="ind">Индекс элмента массива, который необходимо удалить</param>
        /// <returns>Новый массив строк таблицы меньший на одну запись, чем входной</returns>
        public static CompositionRow[] TrimArray(CompositionRow[] Rows, int ind)
        {
            CompositionRow[] comp = new CompositionRow[Rows.Length - 1];
            int k = 0;
            for (int i = 0; i < Rows.Length; i++)
            {
                if (i != ind)
                {
                    comp[k] = Rows[i];
                    k++;
                }
            }
            return comp;
        }

        #region Функции для работы с потребностями в материалах
        /// <summary>
        /// Функция для определения количества и дат для ведомостей по потребности в материалах по месяцам
        /// </summary>
        /// <param name="count">Найденное количество дат (=>ведомостей)</param>
        /// <returns>Возвращает массив найденных дат</returns>
        public static MNDates[] GetMNDatesCount(ref int count)
        {
            // выберем данные из таблицы "ПВИ"
            DatabaseLib.Select sel = new DatabaseLib.Select();
            sel.From("PlanProducts");
            DataTable dt = DBWorker.dbf.Execute(sel);

            MNDates[] data = new MNDates[0];
            // подсчитаем количество уникальных месяцев для производства изделий
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!IsItMNDate(data, dt.Rows[i])) // если нет такой даты,
                    AddMNDate(ref data, dt.Rows[i]); // то добавим ее в массив
            }

            count = data.Length;
            return data;
        }

        private static bool IsItMNDate(MNDates[] data, DataRow row)
        {
            bool flag = false;

            for (int i = 0; i < data.Length; i++)
                if (data[i].Month == Convert.ToInt32(row.ItemArray[2]) && data[i].Year == Convert.ToInt32(row.ItemArray[3]))
                {
                    flag = true;
                    break;
                }

            return flag;
        }

        private static void AddMNDate(ref MNDates[] data, DataRow rows)
        {
            Array.Resize(ref data, data.Length + 1);
            data[data.Length - 1].Month = Convert.ToInt32(rows.ItemArray[2]);
            data[data.Length - 1].Year = Convert.ToInt32(rows.ItemArray[3]);
        }

        /// <summary>
        /// Генерация строки с названием файла и путем с ведомостью для потребностей материалов для заданного месяца
        /// </summary>
        /// <param name="date">Заданная дата отчета (месяц и год)</param>
        /// <returns>Возвращает путь и название файла отчета</returns>
        public static string GetMNReportFileName(MNDates date)
        {
            return String.Format(@"{0}\Reports\MaterialsNeedsReport_{1}_{2}.html", GlobalVars.AppDir, date.Month, date.Year);
        }
        
        /// <summary>
        /// Генерация строки с названием и путем к файлу с ведомостью по потребностям в материалах (суммарным)
        /// </summary>
        /// <param name="date">Заданная дата отчета (месяц и год)</param>
        /// <returns>Возвращает путь к файлу с ведомостью</returns>
        public static string GetMN2ReportFileName(MNDates date)
        {
            return String.Format(@"{0}\Reports\MaterialsNeedsReportSummary_{1}_{2}.html", GlobalVars.AppDir, date.Month, date.Year);
        }
        #endregion

        #region Функции для работы с трудоемкостью
        /// <summary>
        /// Генерация строки с названием и путем к файлу с ведомостью нормативной трудоемкости произв. программы
        /// </summary>
        /// <param name="date">Заданная дата отчета (месяц и год)</param>
        /// <returns>Возвращает путь к файлу с ведомостью</returns>
        public static string GetСPReportFileName(MNDates date)
        {
            return String.Format(@"{0}\Reports\ComplexityProgramReport_{1}_{2}.html", GlobalVars.AppDir, date.Month, date.Year);
        }

        /// <summary>
        /// Генерация строки с названием и путем к файлу с ведомостью нормативной трудоемкости произв. программы по цехам
        /// </summary>
        /// <param name="date">Заданная дата отчета (месяц и год)</param>
        /// <returns>Возвращает путь к файлу с ведомостью</returns>
        public static string GetСSPReportFileName(MNDates date)
        {
            return String.Format(@"{0}\Reports\ComplexityShopProgramReport_{1}_{2}.html", GlobalVars.AppDir, date.Month, date.Year);
        }
        #endregion

        #region Создание таблиц БД
        /// <summary>
        /// Построение дерева и создание ТБД "ПП"
        /// </summary>
        public static void BuiltTreeAndCreateFATable()
        {
            // загрузим данные из ТБД "Состав изделий"
            DatabaseLib.Select str = new DatabaseLib.Select();
            str.From("CompositionProducts");
            DataTable dt = DBWorker.dbf.Execute(str);
            // построим дерево классом для деревьев
            CompositionRow[] data = Converter.ConvertDataTableToCompositionRow(dt);
            Tree tr = new Tree(data);
            // подсчитаем полную применяемость изделий
            if (!tr.IsCalculated) // если еще не рассчитана полная применяемость
            {
                // подсчитаем кол-во деталей в изделиях
                FullApplicationRow[] calc = tr.CalculateFullApplication();
                // заполним таблицу БД ПП
                DBWorker.CreateAndFillFullApplicationTable(calc);
            }
        }

        /// <summary>
        /// Создание и заполнение таблицы "СНРМИ"
        /// </summary>
        public static void CreateTableForProdMatStand()
        {
            // калькуляция данных для таблицы
            ProductMaterialStandartsRow[] data = WorkMaterials.CalculateStandartsForMaterials();

            // создание таблицы в БД
            if (!DBWorker.IsItPNSTable)
                DBWorker.CreateAndFillPradNameStandTable(data);
        }

        #endregion

        #region Выборка данных из таблиц и вывод их
        /// <summary>
        /// Загрузка и вывод данных из таблицы "План выпуска изделий"
        /// </summary>
        /// <param name="dgw">Таблица, куда следует выводить данные</param>
        public static void SelectAndOutPlanProductsTable(Telerik.WinControls.UI.RadGridView dgw)
        {
            // формирование запроса на выборку данны из БД
            string str = @"SELECT
                            ProductNames.Name || ' ' || ProductNames.Designation,
                            PlanProducts.PlanCount,
                            PlanProducts.Month,
                            PlanProducts.Year
                        FROM
                            PlanProducts
                            INNER JOIN ProductNames ON (PlanProducts.ProductCode = ProductNames.ProductKey);";
            DataTable dt = DBWorker.dbf.Execute(str);

            // вывод
            dgw.DataSource = dt;
            DataGridViewHelper.TuneUpPlanProductsTable(dgw);
        }

        /// <summary>
        /// Загрузка и вывод данных из таблицы "Полная применяемость"
        /// </summary>
        /// <param name="dgw">Таблица, куда следует выводить данные</param>
        public static void SelectAndOutFullApplicationTable(Telerik.WinControls.UI.RadGridView dgw)
        {
            // сделаем выборку данных из таблицы
            /*string str = @"SELECT
                                FullApplication.ProductCode,
                                ProductNames.Name || ' ' ||ProductNames.Designation AS NameDes,
                                FullApplication.Count
                              FROM
                                FullApplication
                                INNER JOIN ProductNames ON (FullApplication.PackageDetails = ProductNames.ProductKey)";*/
            string str = @"SELECT
                            F1 || ' ' || F2 || ' ' || F3 AS Product,
                            ProductNames.Name || ' ' ||ProductNames.Designation AS NameDes,
                            FullApplication.Count
                         FROM
                            FullApplication
                            INNER JOIN ProductNames ON (FullApplication.PackageDetails = ProductNames.ProductKey)
                            INNER JOIN (SELECT ProductNames.ProductKey AS F1, ProductNames.Name AS F2, ProductNames.Designation AS F3 
                                        FROM ProductNames) ON (FullApplication.ProductCode = F1);";
            dgw.DataSource = DBWorker.dbf.Execute(str);
            DataGridViewHelper.TuneUpFullApplicationTable(dgw);
        }

        /// <summary>
        /// Выборка данных для отображаемой таблицы "СНРМИ"
        /// </summary>
        /// <param name="dgw">Таблица, куда следует выводить данные</param>
        public static void SelectAndOutProductMaterialsStandartsTable(Telerik.WinControls.UI.RadGridView dgw)
        {
            string str = @"SELECT 
                                ProductNames.ProductKey || ' ' || ProductNames.Name || ' ' || ProductNames.Designation AS Product,
                                ProdNameStand.MaterialCode || ' ' || ReferenceMaterials.Name AS Material,
                                ProdNameStand.Consumption AS Consumption,
                                ProdNameStand.Wastes AS Wastes
                            FROM 
                                ProdNameStand
                                INNER JOIN ReferenceMaterials ON ( ProdNameStand.MaterialCode = ReferenceMaterials.MaterialCode )
                                INNER JOIN ProductNames On ( ProdNameStand.ProductCode = ProductNames.ProductKey )";
            dgw.DataSource = DBWorker.dbf.Execute(str);
            DataGridViewHelper.TuneUpProdMatStandTable(dgw);
        }

        /// <summary>
        /// Выборка данных из таблицы "Сводная нормативная трудоемкость на изделия"
        /// </summary>
        /// <param name="dgw">Таблица, куда следует выводить данные</param>
        internal static void SelectAndOutComplexityTable(Telerik.WinControls.UI.RadGridView dgw)
        {
            string str = @"SELECT
                                ProductNames.ProductKey || ' ' || ProductNames.Name || ' ' || ProductNames.Designation,
                                Complexity.T0,
                                Complexity.Tv,
                                Complexity.Tpz,
                                Complexity.Totl,
                                Complexity.Tpt,
                                Complexity.Tobs
                          FROM
                                Complexity
                                INNER JOIN ProductNames ON (Complexity.ProductCode = ProductNames.ProductKey);";
            // выборка и вывод в таблицу
            dgw.DataSource = DBWorker.dbf.Execute(str);
            DataGridViewHelper.TuneUpCompexityTable(dgw);
        }

        /// <summary>
        /// Выборка данных из таблицы "Сводная нормативная трудоемкость на изделия по цехам"
        /// </summary>
        /// <param name="dgw">Таблица, куда следует выводить данные</param>
        internal static void SelectAndOutComplexityShopTable(Telerik.WinControls.UI.RadGridView dgw)
        {
            string str = @"SELECT DISTINCT
                            ProductNames.ProductKey || ' ' || ProductNames.Name || ' ' || ProductNames.Designation AS Product,
                            ShopsReference.ShopCode || ' ' || ShopsReference.Name AS Shop,
                            ComplexityShop.T0,
                            ComplexityShop.Tv,
                            ComplexityShop.Tpz,
                            ComplexityShop.Totl,
                            ComplexityShop.Tpt,
                            ComplexityShop.Tobs
                        FROM
                            ComplexityShop
                            INNER JOIN ProductNames ON (ComplexityShop.ProductCode = ProductNames.ProductKey)
                            INNER JOIN ShopsReference ON (ComplexityShop.ShopCode = ShopsReference.ShopCode);";
            // выборка и вывод в таблицу
            dgw.DataSource = DBWorker.dbf.Execute(str);
            DataGridViewHelper.TuneUpCompexityShopTable(dgw);
        }

        /// <summary>
        /// Выборка данных и вывод их в таблицу для ТБД "Состав изделий"
        /// </summary>
        /// <param name="dgw">Таблица,к уда выводить данные</param>
        internal static void SelectAndOutCompositionProductsTable(Telerik.WinControls.UI.RadGridView dgw)
        {
            string str = @"SELECT
                            F1 || ' ' || F2 || ' ' || F3,
                            F12 || ' ' || F22 || ' ' || F32,
                            F13 || ' ' || F23 || ' ' || F33,
                            CompositionProducts.Count
                        FROM
                            CompositionProducts
                            INNER JOIN ProductNames ON (CompositionProducts.RootCode = ProductNames.ProductKey)
                            INNER JOIN (SELECT ProductNames.ProductKey AS F1, ProductNames.Name AS F2, ProductNames.Designation AS F3 
                                        FROM ProductNames) ON (CompositionProducts.RootCode = F1)
                            INNER JOIN (SELECT ProductNames.ProductKey AS F12, ProductNames.Name AS F22, ProductNames.Designation AS F32
                                        FROM ProductNames) ON (CompositionProducts.WhereCode = F12)
                            INNER JOIN (SELECT ProductNames.ProductKey AS F13, ProductNames.Name AS F23, ProductNames.Designation AS F33
                                        FROM ProductNames) ON (CompositionProducts.WhatCode = F13)";
            // выборка и вывод в таблицу
            dgw.DataSource = DBWorker.dbf.Execute(str);
            DataGridViewHelper.TuneUpCompositionProductsTable(dgw);
        }
        #endregion

        #region Функция по работе с БД (общие)
        /// <summary>
        /// Подключение к БД
        /// </summary>
        /// <returns></returns>
        public static string ConnectToDB()
        {
            DialogResult res = MessageBox.Show("Хотите подключить БД по настройкам?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            bool flag = false;
            string str = "";
            if (res == DialogResult.Yes)
            {
                str = ConnectToDB(GlobalVars.DBName, ref flag);
            }
            else
                if (MessageBox.Show("Хотите выбрать файл с БД для подключения к ней?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OpenFileDialog dg = new OpenFileDialog();
                    dg.Filter = "SQLite v.3 DB|*.db3";
                    dg.Title = "Выберите БД для подключения к ней";
                    dg.DefaultExt = "*.db3";
                    dg.ShowDialog();
                    if (dg.FileName != null && dg.FileName != "" && dg.FileName != " ")
                    {
                        str = ConnectToDB(dg.FileName, ref flag);
                    }
                }
            return str;
        }

        /// <summary>
        /// Подключение к БД с параметрами
        /// </summary>
        /// <param name="DBName">Путь к файлу БД</param>
        /// <param name="exflag">Флаг об исключении</param>
        /// <returns>Возвращает ошибку, кототая возникает при попытке подключиться к БД</returns>
        public static string ConnectToDB(string DBName, ref bool exflag)
        {
            string str = "";
            if (File.Exists(DBName))
            {
                // подключение к БД
                DBWorker.ConnectToDB(DBName);
                string status = String.Format("Состояние БД: Подключено | Файл БД: {0} | Версия SQLite: {1}",
                                              GlobalFunctions.ExtactFileName(DBWorker.dbf.Filename), DBWorker.dbf.Version);
                str = status;
                exflag = false;
            }
            else
            {
                Exception ex = new Exception("Указанного файла с БД не существует! Проверьте правильность пути и название файла!");
                str = ex.Message;
                exflag = true;
            }
            return str;
        }
        #endregion
    }
    #endregion
}
