//*****************************************************************************
// Контрол для реализации функций потребностей в материалах
//*****************************************************************************
using System;
using System.Windows.Forms;
using System.Data;

namespace ProjectNSI
{
    public partial class MaterialsNeeds : UserControl
    {
        DataTable dtp;
        
        #region Конструктор
        public MaterialsNeeds()
        {
            InitializeComponent();
            InitializeData();
            if (DBWorker.IsItMNTable)
            {
                radButton2.Enabled = true;
                radButton3.Enabled = true;
                SelectAndOutMaterialsNeedsTableData();
            }
            else
            {
                radButton2.Enabled = false;
                radButton3.Enabled = false;
            }
            // настроим события клика для контекстного меню таблицы "План выпуска изделий"
            radContextMenu1.Items[1].Click += new EventHandler(EditPlanProductsRow_Click);
            radContextMenu1.Items[2].Click += new EventHandler(AddPlanProductsRow_Click);
            radContextMenu1.Items[4].Click += new EventHandler(RemovePlanProductsRow_Click);
        }

        private void InitializeData()
        {
            RefreshPlanProductsTable();
        }
        #endregion

        #region Обработчики событий кнопок
        // составление таблицы ПМТВ
        private void radButton1_Click(object sender, EventArgs e)
        {
            if (!DBWorker.IsItFATable)
                GlobalFunctions.BuiltTreeAndCreateFATable();
            if (!DBWorker.IsItPNSTable)
                GlobalFunctions.CreateTableForProdMatStand();
            // генерация данных для таблицы "ПМТВ"
            CreateMaterialsNeedsTable();
            // вывод полученной таблицы в таблицу
            SelectAndOutMaterialsNeedsTableData();
            // делаем доступной кнопку для генерации отчета
            radButton2.Enabled = true;
            radButton3.Enabled = true;
        }

        // кнопка для ведомости потребностей в материалах в разрезе изделий
        private void radButton2_Click(object sender, EventArgs e)
        {
            // генерация HTML отчета (запуск окна с настройками генерации)
            GetReports form = new GetReports(1);
            form.ShowDialog();
        }
        // кнопка Ведомость потребности в материалах
        private void radButton3_Click(object sender, EventArgs e)
        {
            // проверка создана ли таблица в БД для генерации данной ведомости
            if (!DBWorker.IsItMN2Table)
            {
            	// создадим таблицу
                MaterialsNeeds2Row[] data = WorkMaterials22.CalculateMaterialsNeeds();
                DBWorker.CreateAndFillMaterialsNeeds2Table(data);
            }

            // генерируем HTML отчет (запуск окна с настройками генерации)
            GetReports form = new GetReports(2);
            form.ShowDialog();
        }
        #endregion

        #region Основные функции
        /// <summary>
        /// Создание таблицы "Потребности в материалах на товарный выпуск"
        /// </summary>
        private void CreateMaterialsNeedsTable()
        {
            // подготовка данных для заполнения таблиц
            MaterialsNeedsRow[] data = WorkMaterials2.CalculateMaterialsNeeds();
            MaterialsNeeds2Row[] data2 = WorkMaterials22.CalculateMaterialsNeeds();

            // создание и заполение таблиц
            if (!DBWorker.IsItMNTable) // таблица для ведомости в разрезе изделий
                DBWorker.CreateAndFillMaterialsNeedsTable(data);
            if (!DBWorker.IsItMN2Table) // таблица для суммарного материала по месяцам
                DBWorker.CreateAndFillMaterialsNeeds2Table(data2);
        }

        #endregion

        #region Обработка событий контекстного меню
        // пункт Редактирование записи
        private void EditPlanProductsRow_Click(object sender, EventArgs e)
        {
            // подготовим данные для передачи в окно редактирования
            PlanProductsRow row = new PlanProductsRow();
            int ind = planProductsGridView.SelectedRows[0].Index;
            row.ProductCode = dtp.Rows[ind].Field<long>(0);
            row.PlanCount = Convert.ToInt32(dtp.Rows[ind].ItemArray[1]);
            row.Month = Convert.ToInt32(dtp.Rows[ind].ItemArray[2]);
            row.Year = Convert.ToInt32(dtp.Rows[ind].ItemArray[3]);
            // открытие окна с редактируемыми данными
            AddPlanProductsRowForm form = new AddPlanProductsRowForm(FormType.EDITFORM, row);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
                RefreshPlanProductsTable();
        }

        // пункт Добавление записи
        private void AddPlanProductsRow_Click(object sender, EventArgs e)
        {
            AddPlanProductsRowForm form = new AddPlanProductsRowForm(FormType.ADDFORM);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
                RefreshPlanProductsTable();
        }

        // пункт Удаление записи
        private void RemovePlanProductsRow_Click(object sender, EventArgs e)
        {
            if (planProductsGridView.SelectedRows.Count != 0)
            {
                DialogResult res = MessageBox.Show("Вы действительно хотите удалить выбранную запись?", "Запрос на удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    // удаление выбранной записи из таблицы
                    string where = String.Format("(ProductCode = {0}) and (PlanCount = {1}) and (Month = {2}) and (Year = {3})",
                        Convert.ToInt64(dtp.Rows[planProductsGridView.SelectedRows[0].Index].Field<long>(0)),
                        Convert.ToInt32(planProductsGridView.SelectedRows[0].Cells[1].Value),
                        Convert.ToInt32(planProductsGridView.SelectedRows[0].Cells[2].Value),
                        Convert.ToInt32(planProductsGridView.SelectedRows[0].Cells[3].Value));
                    bool result = DBWorker.DeleteRow("PlanProducts", where);
                    if (result)
                    {
                        MessageBox.Show("Выбранная запись удалена успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshPlanProductsTable(); // обновим данные таблицы
                    }
                }
            }
            else
                MessageBox.Show("Не выбрана строка для удаления данных из таблицы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region Вспомогательные функции
        /// <summary>
        /// Выборка данных из ТБД "ПМТВ" и вывод их в таблицу
        /// </summary>
        private void SelectAndOutMaterialsNeedsTableData()
        {
            // описание запроса на выборку данных
            /*string str = @"SELECT
                            ReferenceMaterials.Name,
                            MaterialsNeeds.PlanCount,
                            MaterialsNeeds.Month,
                            MaterialsNeeds.Year
                         FROM
                            MaterialsNeeds
                            INNER JOIN ReferenceMaterials ON (MaterialsNeeds.MaterialCode = ReferenceMaterials.MaterialCode);";*/
            string str = @"SELECT
                                ProductNames.ProductKey || ' ' || ProductNames.Name || ' ' || ProductNames.Designation AS Product,
                                ReferenceMaterials.MaterialCode || ' ' ||ReferenceMaterials.Name AS Material,
                                MaterialsNeeds.PlanCount,
                                MaterialsNeeds.Month,
                                MaterialsNeeds.Year
                          FROM
                                MaterialsNeeds
                                INNER JOIN ReferenceMaterials ON (MaterialsNeeds.MaterialCode = ReferenceMaterials.MaterialCode)
                                INNER JOIN ProductNames ON (MaterialsNeeds.ProductCode = ProductNames.ProductKey);";
            DataTable dt = DBWorker.dbf.Execute(str);

            // вывод данных в таблицу
            materialsNeedsGridView.DataSource = dt;
            DataGridViewHelper.TuneUpMaterialsNeedsTable(materialsNeedsGridView);
        }

        /// <summary>
        /// Обновление таблицы "План выпуска изделий"
        /// </summary>
        private void RefreshPlanProductsTable()
        {
            // загрузка таблицы "План выпуска изделий"
            GlobalFunctions.SelectAndOutPlanProductsTable(planProductsGridView);
            // загрузка данных из таблицы "План выпуска изделий"
            dtp = DBWorker.SelectDataFromTable("ПВИ");
        }
        #endregion
    }


    #region Вспомогательный класс для работы с материалами (расчет потребности в материалах)
    public static class WorkMaterials2
    {
        /// <summary>
        /// Калькуляция потребностей по материалам на изделия
        /// </summary>
        /// <returns></returns>
        public static MaterialsNeedsRow[] CalculateMaterialsNeeds()
        {
            MaterialsNeedsRow[] res = new MaterialsNeedsRow[0];

            // выберем данные для всех изделий по материалам
            string str = @"SELECT
                            ProdNameStand.ProductCode,                            
                            ProdNameStand.MaterialCode,
                            ProdNameStand.Consumption * PlanProducts.PlanCount AS Stand,
                            PlanProducts.Month,
                            PlanProducts.Year
                         FROM
                            PlanProducts
                            INNER JOIN ProdNameStand ON (PlanProducts.ProductCode = ProdNameStand.ProductCode);";
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

            // вернем массив строк с данными
            return res;
        }

        /// <summary>
        /// Добавление строки с данными в массив
        /// </summary>
        /// <param name="dataRow">Даные из хранилица исходных данных</param>
        /// <param name="res">Редактируемый массив</param>
        private static void AddRowToArray(DataRow dataRow, ref MaterialsNeedsRow[] data)
        {
            // увеличим массив
            Array.Resize(ref data, data.Length + 1);
            // добавим данные в массив
            data[data.Length - 1].ProductCode = dataRow.Field<long>(0);
            data[data.Length - 1].MaterialCode = dataRow.Field<long>(1);
            data[data.Length - 1].PlanCount = Convert.ToSingle(dataRow.ItemArray[2]);
            data[data.Length - 1].Month = Convert.ToInt32(dataRow.ItemArray[3]);
            data[data.Length - 1].Year = Convert.ToInt32(dataRow.ItemArray[4]);
        }

        /// <summary>
        /// Изменение данных в строке для текущего материала и изделия
        /// </summary>
        /// <param name="dataRow">строка с дополнительными данными</param>
        /// <param name="data">Текущий массив строк</param>
        /// <param name="ind">Индекс строки массива, в которую небходимо внести изменения</param>
        private static void ChangeDataInRow(DataRow dataRow, ref MaterialsNeedsRow[] data, int ind)
        {
            // изменим данные в массиве
            data[ind].PlanCount += Convert.ToSingle(dataRow.ItemArray[2]);
        }

        /// <summary>
        /// Поиск строки в массиве по коду продукции и материалу
        /// </summary>
        /// <param name="dataRow">Строка с данным для поиска</param>
        /// <param name="data">Массив в данными, в которым проверяется наличие искомой строки</param>
        /// <param name="ind">Возвращает индекс найденной строки в массиве, иначе -1</param>
        /// <returns>Возвращает True есть ли искомая строка в массиве, иначе False</returns>
        private static bool IsItMaterialForProduct(DataRow dataRow, MaterialsNeedsRow[] data, out int ind)
        {
            bool flag = false;
            ind = -1;

            for (int i = 0; i < data.Length; i++)
            {
                if (Convert.ToInt64(dataRow.ItemArray[0]) == data[i].ProductCode && Convert.ToInt64(dataRow.ItemArray[1]) == data[i].MaterialCode)
                {
                    flag = true;
                    ind = i;
                    break;
                }
            }

            return flag;
        }
    } 
    #endregion

    #region Вспомогательный класс для работы с материалами (расчет потребности в материалах) табл.2
    public static class WorkMaterials22
    {
        /// <summary>
        /// Калькуляция потребностей по материалам на изделия
        /// </summary>
        /// <returns></returns>
        public static MaterialsNeeds2Row[] CalculateMaterialsNeeds()
        {
            MaterialsNeeds2Row[] res = new MaterialsNeeds2Row[0];

            // выберем данные для всех изделий по материалам
            string str = @"SELECT                         
                            ProdNameStand.MaterialCode,
                            ProdNameStand.Consumption * PlanProducts.PlanCount AS Plan,
                            PlanProducts.Month,
                            PlanProducts.Year
                         FROM
                            PlanProducts
                            INNER JOIN ProdNameStand ON (PlanProducts.ProductCode = ProdNameStand.ProductCode);";
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

            // вернем массив строк с данными
            return res;
        }

        /// <summary>
        /// Добавление строки с данными в массив
        /// </summary>
        /// <param name="dataRow">Даные из хранилица исходных данных</param>
        /// <param name="res">Редактируемый массив</param>
        private static void AddRowToArray(DataRow dataRow, ref MaterialsNeeds2Row[] data)
        {
            // увеличим массив
            Array.Resize(ref data, data.Length + 1);
            // добавим данные в массив
            data[data.Length - 1].MaterialCode = dataRow.Field<long>(0);
            data[data.Length - 1].PlanCount = Convert.ToSingle(dataRow.ItemArray[1]);
            data[data.Length - 1].Month = Convert.ToInt32(dataRow.ItemArray[2]);
            data[data.Length - 1].Year = Convert.ToInt32(dataRow.ItemArray[3]);
        }

        /// <summary>
        /// Изменение данных в строке для текущего материала и изделия
        /// </summary>
        /// <param name="dataRow">строка с дополнительными данными</param>
        /// <param name="data">Текущий массив строк</param>
        /// <param name="ind">Индекс строки массива, в которую небходимо внести изменения</param>
        private static void ChangeDataInRow(DataRow dataRow, ref MaterialsNeeds2Row[] data, int ind)
        {
            // изменим данные в массиве
            data[ind].PlanCount += Convert.ToSingle(dataRow.ItemArray[1]);
        }

        /// <summary>
        /// Поиск строки в массиве по коду продукции и материалу
        /// </summary>
        /// <param name="dataRow">Строка с данным для поиска</param>
        /// <param name="data">Массив в данными, в которым проверяется наличие искомой строки</param>
        /// <param name="ind">Возвращает индекс найденной строки в массиве, иначе -1</param>
        /// <returns>Возвращает True есть ли искомая строка в массиве, иначе False</returns>
        private static bool IsItMaterialForProduct(DataRow dataRow, MaterialsNeeds2Row[] data, out int ind)
        {
            bool flag = false;
            ind = -1;

            for (int i = 0; i < data.Length; i++)
            {
                // код материала, месяц и год совпадают? (составной индекс)
                if (Convert.ToInt64(dataRow.ItemArray[0]) == data[i].MaterialCode && Convert.ToInt32(dataRow.ItemArray[2]) == data[i].Month
                    && Convert.ToInt32(dataRow.ItemArray[3]) == data[i].Year)
                {
                    flag = true;
                    ind = i;
                    break;
                }
            }

            return flag;
        }
    }
    #endregion
}
