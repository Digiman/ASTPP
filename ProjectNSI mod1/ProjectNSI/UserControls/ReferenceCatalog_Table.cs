//*****************************************************************************
// Модуль для работы с таблицами из БД (Справочниками)
//*****************************************************************************
using System;
using System.Windows.Forms;
using System.Data;

namespace ProjectNSI
{
    public partial class ReferenceCatalog_Table : UserControl
    {
        string TableTag;

        #region Конструктор
        public ReferenceCatalog_Table(string _TableTag)
        {
            InitializeComponent();
            TableTag = _TableTag;
            bool flag = LoadDataFromDatabase();
            if (!flag)
                return;
        }
        #endregion

        #region Загрузка данных в таблицу
        // загрузка таблицы с данными (имя таблицы задано тегом)
        private bool LoadDataFromDatabase()
        {
            if (DBWorker.flag == false)
            {
                MessageBox.Show("Соединение с БД не установлено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string str = "";
            // загрузка данных в таблицу DataTable
            switch (TableTag)
            {
                case "СВП":
                    dataTableView.DataSource = DBWorker.SelectDataFromTable("СВП");
                    DataGridViewHelper.TuneUpReferenceProductTable(dataTableView);
                    break;
                case "СТП":
                    dataTableView.DataSource = DBWorker.SelectDataFromTable("СТП");
                    DataGridViewHelper.TuneUpProductTypesTable(dataTableView);
                    break;
                case "СПП":
                    dataTableView.DataSource = DBWorker.SelectDataFromTable("СПП");
                    DataGridViewHelper.TuneUpReferenceSignTable(dataTableView);
                    break;
                case "СЕИ":
                    dataTableView.DataSource = DBWorker.SelectDataFromTable("СЕИ");
                    DataGridViewHelper.TuneUpReferenceUnitsTable(dataTableView);
                    break;
                case "СНП":
                    //dataTableView.DataSource = DBWorker.SelectDataFromTable("СНП");
                    str = @"SELECT ProductNames.ProductKey,
                                   ProductNames.Name,
                                   ProductNames.Designation,
                                   ReferenceProducts.Name,
                                   ProductTypes.Name,
                                   ReferenceSigns.Name
                                FROM
                                   ProductNames
                                   INNER JOIN ProductTypes ON (ProductNames.TypeCode = ProductTypes.PKey)
                                   INNER JOIN ReferenceProducts ON (ProductNames.ViewCode = ReferenceProducts.PKey)
                                   INNER JOIN ReferenceSigns ON (ProductNames.SignCode = ReferenceSigns.PKey)";
                    dataTableView.DataSource = DBWorker.dbf.Execute(str);
                    DataGridViewHelper.TuneUpProductNamesTable(dataTableView);
                    break;
                case "СТМ":
                    //dataTableView.DataSource = DBWorker.SelectDataFromTable("СТМ");
                    str = @"SELECT 
                                ReferenceMaterials.MaterialCode,
                                ReferenceMaterials.Name,
                                ReferenceUnits.FullName
                            FROM
                                ReferenceMaterials
                                INNER JOIN ReferenceUnits ON (ReferenceMaterials.UnitType = ReferenceUnits.PKey)";
                    dataTableView.DataSource = DBWorker.dbf.Execute(str);
                    DataGridViewHelper.TuneUpReferenceMaterialsTable(dataTableView);
                    break;
                case "СНиОД":
                    //dataTableView.DataSource = DBWorker.SelectDataFromTable("СНиОД");
                    str = @"SELECT 
                                ReferenceStandarts.ProductCode,                                
                                ProductNames.Name || ' ' || ProductNames.Designation,
                                ReferenceMaterials.Name,
                                ReferenceStandarts.ConsumptionRate,
                                ReferenceStandarts.RateOfWaste
                            FROM
                                ReferenceStandarts
                                INNER JOIN ReferenceMaterials ON (ReferenceStandarts.MaterialCode = ReferenceMaterials.MaterialCode)
                                INNER JOIN ProductNames ON (ReferenceStandarts.ProductCode = ProductNames.ProductKey);";
                    dataTableView.DataSource = DBWorker.dbf.Execute(str);
                    DataGridViewHelper.TuneUpReferenceStandartsTable(dataTableView);
                    break;
            }
            return true;
        }
        #endregion

        #region Обработчики событий кнопок
        // кнопка добавления строки в таблицу
        private void addRowButton_Click(object sender, System.EventArgs e)
        {
            switch (TableTag)
            {
                case "СВП":
                    AddProductRowForm prod = new AddProductRowForm(FormType.ADDFORM, TableTag);
                    prod.ShowDialog();
                    if (prod.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                        LoadDataFromDatabase();
                    break;
                case "СТП":
                    AddTypeRowForm type = new AddTypeRowForm(FormType.ADDFORM, TableTag);
                    type.ShowDialog();
                    if (type.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                        LoadDataFromDatabase();
                    break;
                case "СПП":
                    AddSignRowForm sign = new AddSignRowForm(FormType.ADDFORM, TableTag);
                    sign.ShowDialog();
                    if (sign.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                        LoadDataFromDatabase();
                    break;
                case "СЕИ":
                    AddUnitRowForm urow = new AddUnitRowForm(FormType.ADDFORM, TableTag);
                    urow.ShowDialog();
                    if (urow.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                        LoadDataFromDatabase();
                    break;
                case "СНП":
                    AddProductNameRowForm pnrow = new AddProductNameRowForm(FormType.ADDFORM, TableTag);
                    pnrow.ShowDialog();
                    if (pnrow.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                        LoadDataFromDatabase();
                    break;
                case "СТМ":
                    AddMaterialRowForm mrow = new AddMaterialRowForm(FormType.ADDFORM, TableTag);
                    mrow.ShowDialog();
                    if (mrow.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                        LoadDataFromDatabase();
                    break;
                case "СНиОД":
                    AddStandartRowForm mtrow = new AddStandartRowForm(FormType.ADDFORM, TableTag);
                    mtrow.ShowDialog();
                    if (mtrow.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                        LoadDataFromDatabase();
                    break;
            }
        }

        // кнопка удаления выбранной строки (записи) из таблицы
        private void deleteRowButton_Click(object sender, System.EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы действительно хотите удалить выбранную запись?", "Подтверждение операции удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
                if (dataTableView.SelectedRows.Count == 0)
                    MessageBox.Show("Не выбрана запись для удаления из таблицы!\r\nВыберите запись!", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    // удаление выбранной записи из таблицы
                    long key = Convert.ToInt64(dataTableView.SelectedRows[0].Cells[0].Value);
                    bool result = DBWorker.DeleteRowFromTable(TableTag, key);
                    if (result)
                    {
                        MessageBox.Show("Выбранная запись удалена успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataFromDatabase(); // обновим данные таблицы
                    }
                }
        }

        // кнопка редактирования данных выбранной записи
        private void editRowButton_Click(object sender, EventArgs e)
        {
            if (dataTableView.SelectedRows.Count == 0)
                MessageBox.Show("Не выбрана запись для редактирования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                long key;
                // редактирование выбранной записи для текущей таблицы
                switch (TableTag)
                {
                    case "СВП":
                        ProductRow prow = Converter.ConvertRowInfoToProductRow(dataTableView.SelectedRows[0]);
                        key = Convert.ToInt64(dataTableView.SelectedRows[0].Cells[0].Value);
                        AddProductRowForm prowf = new AddProductRowForm(FormType.EDITFORM, TableTag, prow, key);
                        prowf.ShowDialog();
                        if (prowf.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                            LoadDataFromDatabase();
                        break;
                    case "СТП":
                        TypeRow trow = Converter.ConvertRowInfoToTypeRow(dataTableView.SelectedRows[0]);
                        key = Convert.ToInt64(dataTableView.SelectedRows[0].Cells[0].Value);
                        AddTypeRowForm trowf = new AddTypeRowForm(FormType.EDITFORM, TableTag, trow, key);
                        trowf.ShowDialog();
                        if (trowf.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                            LoadDataFromDatabase();
                        break;
                    case "СПП":
                        SignRow srow = Converter.ConvertRowInfoToSignRow(dataTableView.SelectedRows[0]);
                        key = Convert.ToInt64(dataTableView.SelectedRows[0].Cells[0].Value);
                        AddSignRowForm srowf = new AddSignRowForm(FormType.EDITFORM, TableTag, srow, key);
                        srowf.ShowDialog();
                        if (srowf.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                            LoadDataFromDatabase();
                        break;
                    case "СЕИ":
                        UnitRow urow = Converter.ConvertRowInfoToUnitRow(dataTableView.SelectedRows[0]);
                        key = Convert.ToInt64(dataTableView.SelectedRows[0].Cells[0].Value);
                        AddUnitRowForm urowf = new AddUnitRowForm(FormType.EDITFORM, TableTag, urow, key);
                        urowf.ShowDialog();
                        if (urowf.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                            LoadDataFromDatabase();
                        break;
                    case "СНП":
                        key = Convert.ToInt64(dataTableView.SelectedRows[0].Cells[0].Value);
                        ProductNameRow pnrow = ConvertTableRowToProductNameRow(key);
                        //ProductNameRow pnrow = Converter.ConvertRowInfoToProductNameRow(dataTableView.SelectedRows[0]);
                        AddProductNameRowForm pnrowf = new AddProductNameRowForm(FormType.EDITFORM, TableTag, pnrow, key);
                        pnrowf.ShowDialog();
                        if (pnrowf.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                            LoadDataFromDatabase();
                        break;
                    case "СТМ":
                        key = Convert.ToInt64(dataTableView.SelectedRows[0].Cells[0].Value);
                        MaterialsRow mrow = ConvertTableRowToMaterialRow(key);
                        //MaterialsRow mrow = Converter.ConvertRowInfoToMaterialRow(dataTableView.SelectedRows[0]);
                        AddMaterialRowForm mrowf = new AddMaterialRowForm(FormType.EDITFORM, TableTag, mrow, key);
                        mrowf.ShowDialog();
                        if (mrowf.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                            LoadDataFromDatabase();
                        break;
                    case "СНиОД":
                        key = Convert.ToInt64(dataTableView.SelectedRows[0].Cells[0].Value);
                        StandartRow mtrow = ConvertTableRowToStandartRow(key);
                        //StandartRow mtrow = Converter.ConvertRowInfoToStandartRow(dataTableView.SelectedRows[0]);
                        AddStandartRowForm mtrowf = new AddStandartRowForm(FormType.EDITFORM, TableTag, mtrow, key);
                        mtrowf.ShowDialog();
                        if (mtrowf.DialogResult == DialogResult.OK) // обновление таблицы и ее данных
                            LoadDataFromDatabase();
                        break;
                }
            }
        }
        #endregion

        #region Вспомогательные функции
        // функция поиска по предкам главного окна приложения и установка в статусстипе названия таблицы
        private void SetNameTableInSS(int ind)
        {
            Control par1 = this.Parent; // панель в которой расположен текущий UC
            Control par2 = par1.Parent; // форма главного окна
            Control[] cont = par2.Controls.Find("radLabelElement1", false);
            MessageBox.Show(cont.Length.ToString());
        }

        // создание параметров для окна редактирования из выборки из ТБД по ключу (таблица СНП)
        private ProductNameRow ConvertTableRowToProductNameRow(long key)
        {
            DatabaseLib.Select sel = new DatabaseLib.Select();
            sel.From("ProductNames");
            sel.Where(String.Format("ProductKey={0}", key));
            ProductNameRow pr = Converter.ConvertDataTableRowToProductNameRow(DBWorker.dbf.Execute(sel));
            return pr;
        }

        // создание параметров для окна редактирования из выборки из ТБД по ключу (таблица СТМ)
        private MaterialsRow ConvertTableRowToMaterialRow(long key)
        {
            DatabaseLib.Select sel = new DatabaseLib.Select();
            sel.From("ReferenceMaterials");
            sel.Where(String.Format("MaterialCode={0}", key));
            MaterialsRow mr = Converter.ConvertDataTableRowToMaterialRow(DBWorker.dbf.Execute(sel));
            return mr;
        }
        // создание параметров для окна редактирования из выборки из ТБД по ключу (таблица СНиОД)
        private StandartRow ConvertTableRowToStandartRow(long key)
        {
            DatabaseLib.Select sel = new DatabaseLib.Select();
            sel.From("ReferenceStandarts");
            sel.Where(String.Format("ProductCode={0}", key));
            StandartRow sr = Converter.ConvertDataTableRowToStandartRow(DBWorker.dbf.Execute(sel));
            return sr;
        } 
        #endregion
    }
}
