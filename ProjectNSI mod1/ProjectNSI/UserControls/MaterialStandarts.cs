//*****************************************************************************
// Котрол для реализации функций материального планирования
//*****************************************************************************
using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectNSI
{
    public partial class MaterialStandarts : UserControl
    {
        #region Конструкторы
        public MaterialStandarts()
        {
            InitializeComponent();
            InitiaizeData();
            if (DBWorker.IsItPNSTable)
            {
                radButton2.Enabled = true;
                radButton3.Enabled = true;
                GlobalFunctions.SelectAndOutProductMaterialsStandartsTable(prodMatStandGridView);
            }
            else
            {
                radButton2.Enabled = false;
                radButton3.Enabled = false;
            }
        }
        #endregion

        #region Загрузка данных и настройка формы
        // загрузка данных в компоненты окна и их генерации если необходимо
        private void InitiaizeData()
        {
            // загрузка данных из таблицы ПП
            if (DBWorker.IsItFATable)
            {
                GlobalFunctions.SelectAndOutFullApplicationTable(fullApplicationGridView);
            }
            else // генерация данных для таблицы ПП и выполнеение разузлования
            {
                GlobalFunctions.BuiltTreeAndCreateFATable();
                GlobalFunctions.SelectAndOutFullApplicationTable(fullApplicationGridView);
            }
        }
        #endregion

        #region Обработчики событий кнопок
        // получение таблицы "Сводные нормы расхода на изделие"
        private void GetStandartsForProductsExecute(object sender, EventArgs e)
        {
            // создание ее в БД
            CreateTableForProdMatStand();
            // вывод в красивом виде таблицы
            GlobalFunctions.SelectAndOutProductMaterialsStandartsTable(prodMatStandGridView);
            // делаем доступными кнопки
            radButton2.Enabled = true;
            radButton3.Enabled = true;
        }

        // создание ведомости подетальных норм расхода на изделие
        private void GetReportForStandartsExecute(object sender, EventArgs e)
        {
            // генерируем вспомогательную таблицу
            GenerateHelpMaterialsStandartsTable();
            // генерируем HTML файл
            HTMLWorker.GeneratePeportDetailStandarts();
            // открываем его в окне просмотра отчетов
            ReportViewerForm form = new ReportViewerForm(GlobalVars.AppDir + "\\" + "Reports\\DetailsStandartsReport.html");
            form.Show();
        }

        // создание ведомости сводных норм расхода материалов на изделие
        private void GetReportForStandartsOnProductExecute(object sender, EventArgs e)
        {
            // создание вспомогательной таблицы для ведомости
            GenerateHelpMaterialsStandartsTable();
            // генерируем HTML файл
            HTMLWorker.GenerateReportMaterialsStandartsOnProduct();
            // открываем его в окне просмотра отчетов
            ReportViewerForm form = new ReportViewerForm(GlobalVars.AppDir + "\\" + "Reports\\MaterialsStandartsOnProductReport.html");
            form.Show();
        }
        #endregion

        #region Основные функции
        /// <summary>
        /// Создание и заполнение таблицы "СНРМИ"
        /// </summary>
        private void CreateTableForProdMatStand()
        {
            // калькуляция данных для таблицы
            ProductMaterialStandartsRow[] data = WorkMaterials.CalculateStandartsForMaterials();
            
            // создание таблицы в БД
            if (!DBWorker.IsItPNSTable)
                DBWorker.CreateAndFillPradNameStandTable(data);
        }
        
        /// <summary>
        /// Генерация вспомогательной таблицы "Сводная деталей по материалам для изделия" ("СДМИ")
        /// </summary>
        private void GenerateHelpMaterialsStandartsTable()
        {
            // соаздадим запрос на выборку и выберем данные из БД для таблицы
            string str = @"SELECT DISTINCT
                            FullApplication.ProductCode,
                            ProdNameStand.MaterialCode,
                            FullApplication.PackageDetails,
                            FullApplication.Count,
                            ReferenceStandarts.ConsumptionRate,
                            ReferenceStandarts.RateOfWaste
                        FROM
                            FullApplication
                            INNER JOIN ProdNameStand ON (ProdNameStand.MaterialCode = ReferenceStandarts.MaterialCode)
                            INNER JOIN ReferenceStandarts ON (FullApplication.PackageDetails = ReferenceStandarts.ProductCode)";
            DataTable dt = DBWorker.dbf.Execute(str);

            // создадим и заполним БД полученными данными
            if (!DBWorker.IsItDMPTable)
                DBWorker.CreateAndFillDetMatOnProdTable(dt);
        }
        #endregion
    }

    #region Вспомогательный класс для работы с материалами (подсчет норм материалов по изделиям)
    public static class WorkMaterials
    {
        /// <summary>
        /// Построение данных для таблицы СНРМИ
        /// </summary>
        /// <returns>Возвращает массив строк таблицы</returns>
        public static ProductMaterialStandartsRow[] CalculateStandartsForMaterials()
        {
            ProductMaterialStandartsRow[] res = new ProductMaterialStandartsRow[0];

            // определим количество изделий
            int count = 0;
            long[] Roots = GetProdCount(ref count);

            // для каждого изделия проведем подсчет его материалов
            for (int i = 0; i < count; i++)
            {
                // делаем выборку данных для текущего изделия
                string str = String.Format(@"SELECT
                                                FullApplication.ProductCode,
                                                ReferenceStandarts.MaterialCode,
                                                ReferenceStandarts.ConsumptionRate * FullApplication.Count AS Consumption,
                                                ReferenceStandarts.RateOfWaste * FullApplication.Count AS Wastes
                                            FROM 
                                                FullApplication
                                                INNER JOIN ReferenceStandarts ON ( FullApplication.PackageDetails = ReferenceStandarts.ProductCode )
                                                INNER JOIN ReferenceMaterials ON ( ReferenceStandarts.MaterialCode = ReferenceMaterials.MaterialCode )
                                            WHERE
                                                FullApplication.ProductCode = {0}", Roots[i]);
                DataTable dt = DBWorker.dbf.Execute(str);

                // подсчитываем материалы для текущего изделия (просуммируем одинаковые материалы)
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    int ind;
                    bool IsIt = IsItMaterialForProduct(dt.Rows[j], res, out ind);
                    if (!IsIt) // если нет такого материала для данного изделия в массиве
                    {
                        AddRowToArray(dt.Rows[j], ref res);
                    }
                    else // если есть такой материал уже в массиве
                    {
                        ChangeDataInRow(dt.Rows[j], ref res, ind);
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Добавление строки с данными в массив
        /// </summary>
        /// <param name="dataRow">Даные из хранилица исходных данных</param>
        /// <param name="res">Редактируемый массив</param>
        private static void AddRowToArray(DataRow dataRow, ref ProductMaterialStandartsRow[] data)
        {
            // увеличим массив
            Array.Resize(ref data, data.Length + 1);
            // добавим данные в массив
            data[data.Length - 1].ProductCode = dataRow.Field<long>(0);
            data[data.Length - 1].MaterialCode = dataRow.Field<long>(1);
            data[data.Length - 1].Comsumption = Convert.ToSingle(dataRow.ItemArray[2]);
            data[data.Length - 1].Waste = Convert.ToSingle(dataRow.ItemArray[3]);
        }

        /// <summary>
        /// Изменение данных в строке для текущего материала и изделия
        /// </summary>
        /// <param name="dataRow">строка с дополнительными данными</param>
        /// <param name="data">Текущий массив строк</param>
        /// <param name="ind">Индекс строки массива, в которую небходимо внести изменения</param>
        private static void ChangeDataInRow(DataRow dataRow, ref ProductMaterialStandartsRow[] data, int ind)
        {
            // изменим данные в массиве
            data[ind].Comsumption += Convert.ToSingle(dataRow.ItemArray[2]);
            data[ind].Waste += Convert.ToSingle(dataRow.ItemArray[3]);
        }

        /// <summary>
        /// Поиск строки в массиве по коду продукции и материалу
        /// </summary>
        /// <param name="dataRow">Строка с данным для поиска</param>
        /// <param name="data">Массив в данными, в которым проверяется наличие искомой строки</param>
        /// <param name="ind">Возвращает индекс найденной строки в массиве, иначе -1</param>
        /// <returns>Возвращает True есть ли искомая строка в массиве, иначе False</returns>
        private static bool IsItMaterialForProduct(DataRow dataRow, ProductMaterialStandartsRow[] data, out int ind)
        {
            bool flag = false;
            ind = -1;

            for (int i = 0; i < data.Length; i++)
            {
                if (dataRow.Field<long>(0) == data[i].ProductCode && dataRow.Field<long>(1) == data[i].MaterialCode)
                {
                    flag = true;
                    ind = i;
                    break;
                }
            }

            return flag;
        }

        /// <summary>
        /// Определение числа корневых изделий по ТБД "ПП"
        /// </summary>
        /// <param name="count">Искомое количество изделей</param>
        /// <returns>Коды найденных корневых изделий</returns>
        private static long[] GetProdCount(ref int count)
        {
            string str = "SELECT DISTINCT FullApplication.ProductCode FROM FullApplication";
            DataTable dt = DBWorker.dbf.Execute(str);

            count = dt.Rows.Count;
            long[] res = new long[count];
            for (int i = 0; i < count; i++)
                res[i] = dt.Rows[i].Field<long>(0);
            return res;
        }
    }
    #endregion
}
