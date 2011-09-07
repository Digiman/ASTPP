//*****************************************************************************
// Окно для просмотра/редактирования/добавления записи в таблицу СНП
//*****************************************************************************
using System;
using System.Windows.Forms;
using System.Data;

namespace ProjectNSI
{
    public partial class AddProductNameRowForm : Telerik.WinControls.UI.RadForm
    {
        string TableTag;
        FormType FType;
        long Key;
        DataTable dt; // данные из таблицы СНП

        #region Конструкторы
        public AddProductNameRowForm(FormType ftype, string tag)
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

        public AddProductNameRowForm(FormType ftype, string tag, ProductNameRow data, long key)
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
            // данные из таблицы СВП
            radMultiColumnComboBox1.DataSource = DBWorker.SelectDataFromTable("СВП");
            radMultiColumnComboBox1.EditorControl.Columns[0].HeaderText = "Код вида";
            radMultiColumnComboBox1.EditorControl.Columns[0].Width = 100;
            radMultiColumnComboBox1.EditorControl.Columns[1].HeaderText = "Наименование вида";
            radMultiColumnComboBox1.EditorControl.Columns[1].Width = 150;
            // данные из таблицы СТП
            radMultiColumnComboBox2.DataSource = DBWorker.SelectDataFromTable("СТП");
            radMultiColumnComboBox2.EditorControl.Columns[0].HeaderText = "Код вида";
            radMultiColumnComboBox2.EditorControl.Columns[0].Width = 100;
            radMultiColumnComboBox2.EditorControl.Columns[1].HeaderText = "Наименование типа";
            radMultiColumnComboBox2.EditorControl.Columns[1].Width = 150;
            // данные из таблицы СПП
            radMultiColumnComboBox3.DataSource = DBWorker.SelectDataFromTable("СПП");
            radMultiColumnComboBox3.EditorControl.Columns[0].HeaderText = "Код признака";
            radMultiColumnComboBox3.EditorControl.Columns[0].Width = 100;
            radMultiColumnComboBox3.EditorControl.Columns[1].HeaderText = "Наименование признака";
            radMultiColumnComboBox3.EditorControl.Columns[1].Width = 150;
            // не выбран пункт в полях подстановки
            radMultiColumnComboBox1.SelectedIndex = -1;
            radMultiColumnComboBox2.SelectedIndex = -1;
            radMultiColumnComboBox3.SelectedIndex = -1;
            // загрузка данных из таблицы СНП (для проверки корректности новых данных)
            dt = DBWorker.SelectDataFromTable("СНП");
        }

        // заполнение данными контрола
        private void LoadDataToControls(ProductNameRow data)
        {
            radTextBox1.Text = data.Code.ToString();
            radTextBox2.Text = data.Name;
            radTextBox3.Text = data.Designation;
            int i;
            for (i = 0; i < radMultiColumnComboBox1.EditorControl.Rows.Count; i++)
            {
                if (Convert.ToInt64(radMultiColumnComboBox1.EditorControl.Rows[i].Cells[0].Value) == data.ProductCode)
                {
                    radMultiColumnComboBox1.SelectedIndex = i;
                    continue;
                }
            }
            for (i = 0; i < radMultiColumnComboBox2.EditorControl.Rows.Count; i++)
            {
                if (Convert.ToInt64(radMultiColumnComboBox2.EditorControl.Rows[i].Cells[0].Value) == data.TypeCode)
                {
                    radMultiColumnComboBox2.SelectedIndex = i;
                    continue;
                }
            }
            for (i = 0; i < radMultiColumnComboBox3.EditorControl.Rows.Count; i++)
            {
                if (Convert.ToInt64(radMultiColumnComboBox3.EditorControl.Rows[i].Cells[0].Value) == data.SignCode)
                {
                    radMultiColumnComboBox3.SelectedIndex = i;
                    continue;
                }
            }
        }
        #endregion

        #region Обработчики событий кнопок
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (radTextBox1.Text != "" && radTextBox1.Text != " " && radTextBox2.Text != "" && radTextBox2.Text != " " &&
                radTextBox3.Text != "" && radTextBox3.Text != " " &&
                radMultiColumnComboBox1.SelectedIndex >= 0 && radMultiColumnComboBox2.SelectedIndex >= 0 &&
                radMultiColumnComboBox3.SelectedIndex >= 0)
            {
                ProductNameRow row = new ProductNameRow();
                row.Code = Convert.ToInt64(radTextBox1.Text);
                row.Name = radTextBox2.Text;
                row.Designation = radTextBox3.Text;
                row.ProductCode = Convert.ToInt32(radMultiColumnComboBox1.EditorControl.Rows[radMultiColumnComboBox1.SelectedIndex].Cells[0].Value);
                row.TypeCode = Convert.ToInt32(radMultiColumnComboBox2.EditorControl.Rows[radMultiColumnComboBox2.SelectedIndex].Cells[0].Value);
                row.SignCode = Convert.ToInt32(radMultiColumnComboBox3.EditorControl.Rows[radMultiColumnComboBox3.SelectedIndex].Cells[0].Value);
                switch (FType)
                {
                    case FormType.ADDFORM: // вставка данных в ТБД
                        // проверка кода продукции на совпадение с уже имеющимися (pramary key)
                        if (CheckProductCode(Convert.ToInt64(radTextBox1.Text)))
                        {
                            MessageBox.Show("Введеный код продукции уже есть! Ключ продукта должен быть уникален!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        DBWorker.InsertDataRowToTable(TableTag, row);
                        break;
                    case FormType.EDITFORM: // обновление записи в таблице 
                        DBWorker.UpdateDataInRow(TableTag, Key, Converter.ConvertProductNameRowToParameters(row));
                        break;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Не верно введены данные или не заданы совсем!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region Вспомогательные функции
        /// <summary>
        /// Провека кода продукции на уникальность
        /// </summary>
        /// <param name="code">Код продукта для проверки его корректности</param>
        /// <returns>Возвращает True, если код есть, иначе False</returns>
        private bool CheckProductCode(long code)
        {
            bool flag = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (code == Convert.ToInt64(dt.Rows[i].ItemArray.GetValue(0)))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        #endregion
    }
}
