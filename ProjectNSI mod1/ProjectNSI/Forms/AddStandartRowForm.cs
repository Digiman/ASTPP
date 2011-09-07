//*****************************************************************************
// Окно для просмотра/редактирования/добавления записи в таблицу СНиОД
//*****************************************************************************
using System;
using System.Windows.Forms;
using System.Data;

namespace ProjectNSI
{
    public partial class AddStandartRowForm : Telerik.WinControls.UI.RadForm
    {
        string TableTag;
        FormType FType;
        long Key;

        #region Конструкторы формы
        public AddStandartRowForm(FormType ftype, string tag)
        {
            InitializeComponent();
            switch (ftype)
            {
                case FormType.ADDFORM:
                    this.Text = "Добавление записи в таблицу";
                    break;
                case FormType.EDITFORM:
                    this.Text = "Редактирование записи";
                    break;
            }
            TableTag = tag;
            FType = ftype;
            InitializeData();
        }

        public AddStandartRowForm(FormType ftype, string tag, StandartRow data, long key)
        {
            InitializeComponent();
            switch (ftype)
            {
                case FormType.ADDFORM:
                    this.Text = "Добавление записи в таблицу";
                    break;
                case FormType.EDITFORM:
                    this.Text = "Редактирование записи";
                    break;
            }
            TableTag = tag;
            FType = ftype;
            Key = key;
            InitializeData();
            LoadDataToControls(data);
        }
        #endregion

        #region Инициализация окна и данных в нем
        // загрузка данных в поля подстановки
        private void InitializeData()
        {
            // загрузка данных из таблицы СЕИ
            radMultiColumnComboBox3.DataSource = DBWorker.SelectDataFromTable("СЕИ");
            radMultiColumnComboBox3.EditorControl.Columns[0].IsVisible = false;
            radMultiColumnComboBox3.EditorControl.Columns[1].HeaderText = "Полное название";
            radMultiColumnComboBox3.EditorControl.Columns[1].Width = 100;
            radMultiColumnComboBox3.EditorControl.Columns[2].HeaderText = "Краткое название";
            radMultiColumnComboBox3.EditorControl.Columns[2].Width = 100;
            // данные из таблицы СНП (частично)
            DatabaseLib.Select str = new DatabaseLib.Select();
            str.From("ProductNames", "ProductKey,Name,Designation");
            radMultiColumnComboBox1.DataSource = DBWorker.dbf.Execute(str);
            radMultiColumnComboBox1.EditorControl.Columns[0].HeaderText = "Код продукции";
            radMultiColumnComboBox1.EditorControl.Columns[0].Width = 80;
            radMultiColumnComboBox1.EditorControl.Columns[1].HeaderText = "Наименование";
            radMultiColumnComboBox1.EditorControl.Columns[1].Width = 140;
            radMultiColumnComboBox1.EditorControl.Columns[2].HeaderText = "Обозначение";
            radMultiColumnComboBox1.EditorControl.Columns[2].Width = 150;
            // данные из таблицы СТМ
            radMultiColumnComboBox2.DataSource = DBWorker.SelectDataFromTable("СТМ");
            radMultiColumnComboBox2.EditorControl.Columns[0].HeaderText = "Код материала";
            radMultiColumnComboBox2.EditorControl.Columns[0].Width = 100;
            radMultiColumnComboBox2.EditorControl.Columns[1].HeaderText = "Наименование";
            radMultiColumnComboBox2.EditorControl.Columns[1].Width = 250;
            radMultiColumnComboBox2.EditorControl.Columns[2].IsVisible = false;
            // не выбран пункт в полях подстановки
            radMultiColumnComboBox1.SelectedIndex = -1;
            radMultiColumnComboBox2.SelectedIndex = -1;
            radMultiColumnComboBox3.SelectedIndex = -1;
        }

        // заполнение данными контрола
        private void LoadDataToControls(StandartRow data)
        {
            radTextBox1.Text = data.ConsumptionRate.ToString();
            radTextBox2.Text = data.RateOfWaste.ToString();
            radMultiColumnComboBox3.SelectedIndex = GetUnitForStandartRow(data.MaterialCode);
            int i;
            for (i = 0; i < radMultiColumnComboBox2.EditorControl.Rows.Count; i++)
            {
                if (Convert.ToInt64(radMultiColumnComboBox2.EditorControl.Rows[i].Cells[0].Value) == data.MaterialCode)
                {
                    radMultiColumnComboBox2.SelectedIndex = i;
                    continue;
                }
            }
            for (i = 0; i < radMultiColumnComboBox1.EditorControl.Rows.Count; i++)
            {
                if (Convert.ToInt64(radMultiColumnComboBox1.EditorControl.Rows[i].Cells[0].Value) == data.ProductCode)
                {
                    radMultiColumnComboBox1.SelectedIndex = i;
                    continue;
                }
            }
        }
        #endregion

        #region Обработчики кнопок и событий компонетов формы
        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            if (radTextBox1.Text != "" && radTextBox1.Text != " " && radTextBox2.Text != "" && radTextBox2.Text != " " &&
                radMultiColumnComboBox1.SelectedIndex >= 0 && radMultiColumnComboBox2.SelectedIndex >= 0 &&
                radMultiColumnComboBox3.SelectedIndex >= 0)
            {
                StandartRow row = new StandartRow();
                row.ProductCode = Convert.ToInt64(radMultiColumnComboBox1.EditorControl.Rows[radMultiColumnComboBox1.SelectedIndex].Cells[0].Value);
                row.MaterialCode = Convert.ToInt64(radMultiColumnComboBox2.EditorControl.Rows[radMultiColumnComboBox2.SelectedIndex].Cells[0].Value);
                row.ConsumptionRate = Convert.ToSingle(Converter.CorrectFloatUnit(radTextBox1.Text));
                row.RateOfWaste = Convert.ToSingle(Converter.CorrectFloatUnit(radTextBox1.Text));
                switch (FType)
                {
                    case FormType.ADDFORM: // вставка данных в ТБД
                        DBWorker.InsertDataRowToTable(TableTag, row);
                        break;
                    case FormType.EDITFORM: // обновление записи в таблице 
                        DBWorker.UpdateDataInRow(TableTag, Key, Converter.ConvertStandartRowToParameters(row));
                        break;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Не верно введены данные или не заданы совсем!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void radMultiColumnComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radMultiColumnComboBox2.SelectedIndex < 0) return;
            int UnitCode = Convert.ToInt32(radMultiColumnComboBox2.EditorControl.Rows[radMultiColumnComboBox2.SelectedIndex].Cells[2].Value);
            for (int i = 0; i < radMultiColumnComboBox3.EditorControl.Rows.Count; i++)
            {
                if (Convert.ToInt32(radMultiColumnComboBox3.EditorControl.Rows[i].Cells[0].Value) == UnitCode)
                {
                    radMultiColumnComboBox3.SelectedIndex = i;
                    continue;
                }
            }
        }
        #endregion

        #region Вспомогательные функции
        private int GetUnitForStandartRow(long MaterialCode)
        {
            int UnitCode = -1, result = -1, i;
            for (i = 0; i < radMultiColumnComboBox2.EditorControl.Rows.Count; i++)
            {
                if (Convert.ToInt64(radMultiColumnComboBox2.EditorControl.Rows[i].Cells[0].Value) == MaterialCode)
                {
                    UnitCode = Convert.ToInt32(radMultiColumnComboBox2.EditorControl.Rows[i].Cells[2].Value);
                    break;
                }
            }
            for (i = 0; i < radMultiColumnComboBox3.EditorControl.Rows.Count; i++)
            {
                if (Convert.ToInt32(radMultiColumnComboBox3.EditorControl.Rows[i].Cells[0].Value) == UnitCode)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        #endregion
    }
}
