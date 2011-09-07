//*****************************************************************************
// Контрол для обработки трудовых нормативов и ведомостей
//*****************************************************************************
using System;
using System.Windows.Forms;
using System.Data;

namespace ProjectNSI
{
    public partial class Complexity : UserControl
    {
        #region Конструктор и инициализация
        public Complexity()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            // загрузка данных из таблицы "ПП" (если она не создана, то и создать ее)
            if (!DBWorker.IsItFATable)
                GlobalFunctions.BuiltTreeAndCreateFATable();
            // вывод таблицы "ПП"
            GlobalFunctions.SelectAndOutFullApplicationTable(fullApplicationGridView);
            // вывод таблицы "СНТИ"
            GlobalFunctions.SelectAndOutComplexityTable(complexityGridView);
            GlobalFunctions.SelectAndOutComplexityShopTable(complexityShopGridView);
        } 
        #endregion

        #region Обработка событий кнопок
        // сводная ведомость нормативной трудоемкости
        private void radButton2_Click(object sender, EventArgs e)
        {
            // генерация ведомости
            HTMLWorker.GenerateReportComplexity();
            // открытие окна просмотра сгенерированной ведомости
            ReportViewerForm form = new ReportViewerForm(GlobalVars.AppDir + @"\Reports\ComplexityReport.html");
            form.Show();
        }

        // ведомость нормативной трудоемкости производственной программы
        private void radButton3_Click(object sender, EventArgs e)
        {
            // создание таблицы БД "НТПП"
            if (!DBWorker.IsItCPTable)
                ComplexityWorker.GenerateComplexityProgramTable();
            // генерация ведомостей
            GetReports form = new GetReports(3);
            form.ShowDialog();
        }

        // ведомость нормативной трудоемкости производственной программы по цехам
        private void radButton4_Click(object sender, EventArgs e)
        {
            // создание таблицы БД "НТППЦ"
            if (!DBWorker.IsItCSPTable)
                ComplexityWorker.GenerateComplexityShopProgramTable();
            // генерация ведомостей
            GetReports form = new GetReports(4);
            form.ShowDialog();
        }
        #endregion
    }

    #region Класс ComplexityWorker
    /// <summary>
    /// Класс для генерации вспомогательных таблиц для по нормативной трудоемкости
    /// </summary>
    public class ComplexityWorker
    {
        /// <summary>
        /// Генерация таблицы "Нормативная трудоемкость производственной программы" и ее создание в БД
        /// </summary>
        public static void GenerateComplexityProgramTable()
        {
            // 1. Создание запроса для сбора данных для таблицы
            string str = @"SELECT
                                PlanProducts.ProductCode,
                                PlanProducts.PlanCount,
                                Complexity.T0 + Complexity.Tv + Complexity.Tpz + Complexity.Totl + Complexity.Tpt + Complexity.Tobs,
                                PlanProducts.Month,
                                PlanProducts.Year
                           FROM
                                PlanProducts
                                INNER JOIN Complexity ON (PlanProducts.ProductCode = Complexity.ProductCode);";
            DataTable dt = DBWorker.dbf.Execute(str);

            // 2. Создание и заполнение данными таблицы в БД
            DBWorker.CreateAndFillComplexityProgramTable(dt);
        }

        /// <summary>
        /// Генерация таблицы "Нормативная трудоемкость производственной программы по цехам" и ее создание в БД
        /// </summary>
        public static void GenerateComplexityShopProgramTable()
        {
            // 1. Создание запроса для сбора данных для таблицы
            string str = @"SELECT
                                PlanProducts.ProductCode,
                                PlanProducts.PlanCount,
                                ComplexityShop.ShopCode,
                                ComplexityShop.T0 + ComplexityShop.Tv + ComplexityShop.Tpz + ComplexityShop.Totl + ComplexityShop.Tpt + ComplexityShop.Tobs,
                                PlanProducts.Month,
                                PlanProducts.Year
                           FROM
                                PlanProducts
                                INNER JOIN ComplexityShop ON (PlanProducts.ProductCode = ComplexityShop.ProductCode);";
            DataTable dt = DBWorker.dbf.Execute(str);

            // 2. Создание и заполнение данными таблицы в БД
            DBWorker.CreateAndFillComplexityShopProgramTable(dt);
        }
    } 
    #endregion
}
