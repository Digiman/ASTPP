//*****************************************************************************
// Окно для просмотра/редактирования/добавления записи в таблицу СПП
//*****************************************************************************
using System.Windows.Forms;

namespace ProjectNSI
{
    public partial class AddSignRowForm : Telerik.WinControls.UI.RadForm
    {
        string TableTag;
        FormType FType;
        long Key;

        public AddSignRowForm(FormType ftype, string tag)
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

        public AddSignRowForm(FormType ftype, string tag, SignRow data, long key)
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
        private void LoadDataToControls(SignRow data)
        {
            radTextBox1.Text = data.Name;
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            if (radTextBox1.Text != "" && radTextBox1.Text != " ")
            {
                SignRow row = new SignRow();
                row.Name = radTextBox1.Text;
                switch (FType)
                {
                    case FormType.ADDFORM: // вставка данных в ТБД
                        DBWorker.InsertDataRowToTable(TableTag, row);
                        break;
                    case FormType.EDITFORM: // обновление записи в таблице 
                        DBWorker.UpdateDataInRow(TableTag, Key, Converter.ConvertSignRowToParameters(row));
                        break;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Не введены данные для выполнения операции!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
