//*****************************************************************************
// Окно для добавления и редактирования записи из таблицы СТМ
//*****************************************************************************
using Telerik.WinControls.UI;
using System.Windows.Forms;
using System.Data;
using System;

namespace ProjectNSI
{
    public partial class AddMaterialRowForm : RadForm
    {
        string TableTag;
        FormType FType;
        long Key;
        DataTable dt; // данные из таблицы СТМ

        #region Конструкторы
        public AddMaterialRowForm(FormType ftype, string tag)
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

        public AddMaterialRowForm(FormType ftype, string tag, MaterialsRow data, long key)
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
        // загрузка данных для редактируемой записи
        private void LoadDataToControls(MaterialsRow data)
        {
            radTextBox1.Text = data.Code.ToString();
            radTextBox2.Text = data.Designation;
            radMultiColumnComboBox1.SelectedIndex = data.UnitCode - 1;
        }

        // загрузка данных для выбора
        private void InitializeData()
        {
            // загрузка данных из таблицы СЕИ
            //DataTable data = DBWorker.SelectDataFromTable("СЕИ");
            radMultiColumnComboBox1.DataSource = DBWorker.SelectDataFromTable("СЕИ");
            radMultiColumnComboBox1.EditorControl.Columns[0].HeaderText = "Код единицы";
            radMultiColumnComboBox1.EditorControl.Columns[0].Width = 80;
            radMultiColumnComboBox1.EditorControl.Columns[1].HeaderText = "Полное наименование";
            radMultiColumnComboBox1.EditorControl.Columns[1].Width = 150;
            radMultiColumnComboBox1.EditorControl.Columns[2].HeaderText = "Краткое наименование";
            radMultiColumnComboBox1.EditorControl.Columns[2].Width = 150;
            /*string[] strs = new string[data.Rows.Count];
            for (int i = 0; i < data.Rows.Count; i++)
                strs[i] = data.Rows[i][1].ToString();
            radDropDownList1.Items.AddRange(strs);*/
            // загрузка данных из таблицы СТМ для проверки новых данных
            dt = DBWorker.SelectDataFromTable("СТМ");
        } 
        #endregion

        #region Обработчики событий кнопок
        // кнопка Отмена
        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // кнопка ОК
        private void okButton_Click(object sender, System.EventArgs e)
        {
            if (radTextBox1.Text != "" && radTextBox1.Text != " " &&
                radTextBox2.Text != "" && radTextBox2.Text != " " &&
                radMultiColumnComboBox1.SelectedIndex < 0)
            {
                MaterialsRow row = new MaterialsRow();
                row.Code = Convert.ToInt64(radTextBox1.Text);
                row.Designation = radTextBox2.Text;
                row.UnitCode = radMultiColumnComboBox1.SelectedIndex + 1;
                switch (FType)
                {
                    case FormType.ADDFORM: // вставка данных в ТБД
                        // проверка кода продукции на совпадение с уже имеющимися (pramary key)
                        if (CheckMaterialCode(Convert.ToInt64(radTextBox1.Text)))
                        {
                            MessageBox.Show("Введеный код материала уже есть! Ключ материала должен быть уникален!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        DBWorker.InsertDataRowToTable(TableTag, row);
                        break;
                    case FormType.EDITFORM: // обновление записи в таблице 
                        DBWorker.UpdateDataInRow(TableTag, Key, Converter.ConvertMaterialRowToParameters(row));
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
        /// Проверка кода материала на существование
        /// </summary>
        /// <param name="code">Код для проверки</param>
        /// <returns>Возвращает True, если код есть в списке, иначе False</returns>
        private bool CheckMaterialCode(long code)
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
