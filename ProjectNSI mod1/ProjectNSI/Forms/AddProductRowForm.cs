//*****************************************************************************
// ���� ��� ���������/��������������/���������� ������ � ������� ���
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
                    this.Text = "���������� ������ � �������";
                    break;
                case FormType.EDITFORM:
                    this.Text = "�������������� ������";
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
                    this.Text = "���������� ������ � �������";
                    break;
                case FormType.EDITFORM:
                    this.Text = "�������������� ������";
                    break;
            }
            TableTag = tag;
            FType = ftype;
            Key = key;
            LoadDataToControls(data);
        }

        // ���������� ������� ��������
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
                    case FormType.ADDFORM: // ������� ������ � ���
                        DBWorker.InsertDataRowToTable(TableTag, row);
                        break;
                    case FormType.EDITFORM: // ���������� ������ � ������� 
                        DBWorker.UpdateDataInRow(TableTag, Key, Converter.ConvertProductRowToParameters(row));
                        break;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("�� ������� ������ ��� ���������� ��������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
