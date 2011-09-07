//*****************************************************************************
// ���� ��� ���������/��������������/���������� ������ � ������� ���
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
                    this.Text = "���������� ������ � �������";
                    break;
                case FormType.EDITFORM:
                    this.Text = "�������������� ������";
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
                    case FormType.ADDFORM: // ������� ������ � ���
                        DBWorker.InsertDataRowToTable(TableTag, row);
                        break;
                    case FormType.EDITFORM: // ���������� ������ � ������� 
                        DBWorker.UpdateDataInRow(TableTag, Key, Converter.ConvertSignRowToParameters(row));
                        break;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("�� ������� ������ ��� ���������� ��������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
