//*****************************************************************************
// Окно для просмотра/редактирования/добавления записи в таблицу СВП
//*****************************************************************************
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace ProjectNSI
{
    public partial class AddProductRowForm : RadForm
    {
        string TableTag;
        FormType FType;
        long Key;

        public AddProductRowForm(FormType ftype, string tag)
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
        }

        public AddProductRowForm(FormType ftype, string tag, ProductRow data, long key)
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
            LoadDataToControls(data);
        }

        // заполнение данными контрола
        private void LoadDataToControls(ProductRow data)
        {
            radTextBox1.Text = data.Name;
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            if (radTextBox1.Text != "" && radTextBox1.Text != " ")
            {
                ProductRow row = new ProductRow();
                row.Name = radTextBox1.Text;
                switch (FType)
                {
                    case FormType.ADDFORM: // вставка данных в ТБД
                        DBWorker.InsertDataRowToTable(TableTag, row);
                        break;
                    case FormType.EDITFORM: // обновление записи в таблице 
                        DBWorker.UpdateDataInRow(TableTag, Key, Converter.ConvertProductRowToParameters(row));
                        break;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Не введены данные для выполнения операции!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
