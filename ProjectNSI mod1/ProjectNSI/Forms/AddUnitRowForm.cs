//*****************************************************************************
// ���� ��� ���������� ��������������/�������� ������ � ������� ���
//*****************************************************************************
using Telerik.WinControls.UI;
using System.Windows.Forms;

namespace ProjectNSI
{
    public partial class AddUnitRowForm : RadForm
    {
        string TableTag;
        FormType FType;
        long Key;

        public AddUnitRowForm(FormType ftype, string tag)
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

        public AddUnitRowForm(FormType ftype, string tag, UnitRow data, long key)
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
        private void LoadDataToControls(UnitRow data)
        {
            radTextBox1.Text = data.FullName;
            radTextBox2.Text = data.SmallName;
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private void okButton_Click(object sender, System.EventArgs e)
        {
            if (radTextBox1.Text != "" && radTextBox2.Text != "" &&
                radTextBox1.Text != " " && radTextBox2.Text != " ")
            {
                UnitRow row = new UnitRow();
                row.FullName = radTextBox1.Text;
                row.SmallName = radTextBox2.Text;
                switch (FType)
                {
                    case FormType.ADDFORM: // ������� ������ � ���
                        DBWorker.InsertDataRowToTable(TableTag, row);
                        break;
                    case FormType.EDITFORM: // ���������� ������ � ������� 
                        DBWorker.UpdateDataInRow(TableTag, Key, Converter.ConvertUnitRowToParameters(row));
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
