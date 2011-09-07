//*****************************************************************************
// ���� ��� ���������� � �������������� ������ �� ������� ���
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
        DataTable dt; // ������ �� ������� ���

        #region ������������
        public AddMaterialRowForm(FormType ftype, string tag)
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

        public AddMaterialRowForm(FormType ftype, string tag, MaterialsRow data, long key)
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
        // �������� ������ ��� ������������� ������
        private void LoadDataToControls(MaterialsRow data)
        {
            radTextBox1.Text = data.Code.ToString();
            radTextBox2.Text = data.Designation;
            radMultiColumnComboBox1.SelectedIndex = data.UnitCode - 1;
        }

        // �������� ������ ��� ������
        private void InitializeData()
        {
            // �������� ������ �� ������� ���
            //DataTable data = DBWorker.SelectDataFromTable("���");
            radMultiColumnComboBox1.DataSource = DBWorker.SelectDataFromTable("���");
            radMultiColumnComboBox1.EditorControl.Columns[0].HeaderText = "��� �������";
            radMultiColumnComboBox1.EditorControl.Columns[0].Width = 80;
            radMultiColumnComboBox1.EditorControl.Columns[1].HeaderText = "������ ������������";
            radMultiColumnComboBox1.EditorControl.Columns[1].Width = 150;
            radMultiColumnComboBox1.EditorControl.Columns[2].HeaderText = "������� ������������";
            radMultiColumnComboBox1.EditorControl.Columns[2].Width = 150;
            /*string[] strs = new string[data.Rows.Count];
            for (int i = 0; i < data.Rows.Count; i++)
                strs[i] = data.Rows[i][1].ToString();
            radDropDownList1.Items.AddRange(strs);*/
            // �������� ������ �� ������� ��� ��� �������� ����� ������
            dt = DBWorker.SelectDataFromTable("���");
        } 
        #endregion

        #region ����������� ������� ������
        // ������ ������
        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ������ ��
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
                    case FormType.ADDFORM: // ������� ������ � ���
                        // �������� ���� ��������� �� ���������� � ��� ���������� (pramary key)
                        if (CheckMaterialCode(Convert.ToInt64(radTextBox1.Text)))
                        {
                            MessageBox.Show("�������� ��� ��������� ��� ����! ���� ��������� ������ ���� ��������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        DBWorker.InsertDataRowToTable(TableTag, row);
                        break;
                    case FormType.EDITFORM: // ���������� ������ � ������� 
                        DBWorker.UpdateDataInRow(TableTag, Key, Converter.ConvertMaterialRowToParameters(row));
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
        /// �������� ���� ��������� �� �������������
        /// </summary>
        /// <param name="code">��� ��� ��������</param>
        /// <returns>���������� True, ���� ��� ���� � ������, ����� False</returns>
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
