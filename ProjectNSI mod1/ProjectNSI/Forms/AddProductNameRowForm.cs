//*****************************************************************************
// ���� ��� ���������/��������������/���������� ������ � ������� ���
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
        DataTable dt; // ������ �� ������� ���

        #region ������������
        public AddProductNameRowForm(FormType ftype, string tag)
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
            InitializeData();
        }

        public AddProductNameRowForm(FormType ftype, string tag, ProductNameRow data, long key)
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
            InitializeData();
            LoadDataToControls(data);
        }
        #endregion

        #region ������������� ���� � ������ � ���
        // �������� ������ � ���� �����������
        private void InitializeData()
        {
            // ������ �� ������� ���
            radMultiColumnComboBox1.DataSource = DBWorker.SelectDataFromTable("���");
            radMultiColumnComboBox1.EditorControl.Columns[0].HeaderText = "��� ����";
            radMultiColumnComboBox1.EditorControl.Columns[0].Width = 100;
            radMultiColumnComboBox1.EditorControl.Columns[1].HeaderText = "������������ ����";
            radMultiColumnComboBox1.EditorControl.Columns[1].Width = 150;
            // ������ �� ������� ���
            radMultiColumnComboBox2.DataSource = DBWorker.SelectDataFromTable("���");
            radMultiColumnComboBox2.EditorControl.Columns[0].HeaderText = "��� ����";
            radMultiColumnComboBox2.EditorControl.Columns[0].Width = 100;
            radMultiColumnComboBox2.EditorControl.Columns[1].HeaderText = "������������ ����";
            radMultiColumnComboBox2.EditorControl.Columns[1].Width = 150;
            // ������ �� ������� ���
            radMultiColumnComboBox3.DataSource = DBWorker.SelectDataFromTable("���");
            radMultiColumnComboBox3.EditorControl.Columns[0].HeaderText = "��� ��������";
            radMultiColumnComboBox3.EditorControl.Columns[0].Width = 100;
            radMultiColumnComboBox3.EditorControl.Columns[1].HeaderText = "������������ ��������";
            radMultiColumnComboBox3.EditorControl.Columns[1].Width = 150;
            // �� ������ ����� � ����� �����������
            radMultiColumnComboBox1.SelectedIndex = -1;
            radMultiColumnComboBox2.SelectedIndex = -1;
            radMultiColumnComboBox3.SelectedIndex = -1;
            // �������� ������ �� ������� ��� (��� �������� ������������ ����� ������)
            dt = DBWorker.SelectDataFromTable("���");
        }

        // ���������� ������� ��������
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

        #region ����������� ������� ������
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
                    case FormType.ADDFORM: // ������� ������ � ���
                        // �������� ���� ��������� �� ���������� � ��� ���������� (pramary key)
                        if (CheckProductCode(Convert.ToInt64(radTextBox1.Text)))
                        {
                            MessageBox.Show("�������� ��� ��������� ��� ����! ���� �������� ������ ���� ��������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        DBWorker.InsertDataRowToTable(TableTag, row);
                        break;
                    case FormType.EDITFORM: // ���������� ������ � ������� 
                        DBWorker.UpdateDataInRow(TableTag, Key, Converter.ConvertProductNameRowToParameters(row));
                        break;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("�� ����� ������� ������ ��� �� ������ ������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region ��������������� �������
        /// <summary>
        /// ������� ���� ��������� �� ������������
        /// </summary>
        /// <param name="code">��� �������� ��� �������� ��� ������������</param>
        /// <returns>���������� True, ���� ��� ����, ����� False</returns>
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
