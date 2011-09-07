//*****************************************************************************
// ���� ��� ������ � �������� ������� "������ �������"
//*****************************************************************************
using Telerik.WinControls.UI;
using System;
using System.Windows.Forms;
using System.Data;

namespace ProjectNSI
{
    public partial class AddCompositionRowForm : Telerik.WinControls.UI.RadForm
    {
        FormType FType;
        DataTable dtp;

        #region ������������
        public AddCompositionRowForm(FormType ftype)
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
            InitializeData();
            FType = ftype;
        }

        public AddCompositionRowForm(FormType ftype, CompositionRow data)
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
            FType = ftype;
            InitializeData();
            LoadDataToControls(data);
        }
        #endregion

        #region �������� ������ � ����������
        // ������������� ������ � �����������
        private void InitializeData()
        {
            // �������� ���� ������ ��� ������� ��
            dtp = DBWorker.SelectDataFromTable("��");
            // ��������� �������� ������ �� ������� ��� � ������ ������
            LoadProducts1(radMultiColumnComboBox1);
            LoadProducts2(radMultiColumnComboBox2);
            LoadProducts3(radMultiColumnComboBox3);
            // ��������� ���������� �������
            TunuUpHeaderOfTheTables(radMultiColumnComboBox1);
            TunuUpHeaderOfTheTables(radMultiColumnComboBox2);
            TunuUpHeaderOfTheTables(radMultiColumnComboBox3);
            // �� ������� ����
            radMultiColumnComboBox1.SelectedIndex = -1;
            radMultiColumnComboBox2.SelectedIndex = -1;
            radMultiColumnComboBox3.SelectedIndex = -1;
        }

        // �������� ������ �� ��������� ������ � ������� ��� ��������������
        private void LoadDataToControls(CompositionRow data)
        {
            // ���������� ������������ ������ �� ��������� ������ ������� ��
            int i;
            for (i = 0; i < radMultiColumnComboBox1.EditorControl.Rows.Count; i++)
                if (Convert.ToInt64(radMultiColumnComboBox1.EditorControl.Rows[i].Cells[0].Value) == data.RootCode)
                {
                    radMultiColumnComboBox1.SelectedIndex = i;
                    break;
                }
            for (i = 0; i < radMultiColumnComboBox2.EditorControl.Rows.Count; i++)
                if (Convert.ToInt64(radMultiColumnComboBox2.EditorControl.Rows[i].Cells[0].Value) == data.WhereCode)
                {
                    radMultiColumnComboBox2.SelectedIndex = i;
                    break;
                }
            for (i = 0; i < radMultiColumnComboBox3.EditorControl.Rows.Count; i++)
                if (Convert.ToInt64(radMultiColumnComboBox3.EditorControl.Rows[i].Cells[0].Value) == data.WhatCode)
                {
                    radMultiColumnComboBox3.SelectedIndex = i;
                    break;
                }
            radTextBox1.Text = data.Count.ToString();
        }
        #endregion

        #region ����������� ������� ������
        // ������ ��
        private void okButton_Click(object sender, EventArgs e)
        {
            if (radMultiColumnComboBox1.SelectedIndex < 0 || radMultiColumnComboBox2.SelectedIndex < 0 ||
                radMultiColumnComboBox3.SelectedIndex < 0 || radTextBox1.Text != "")
            {
                CompositionRow row = new CompositionRow();
                row.RootCode = Convert.ToInt64(radMultiColumnComboBox1.EditorControl.Rows[radMultiColumnComboBox1.SelectedIndex].Cells[0].Value);
                row.WhereCode = Convert.ToInt64(radMultiColumnComboBox2.EditorControl.Rows[radMultiColumnComboBox2.SelectedIndex].Cells[0].Value);
                row.WhatCode = Convert.ToInt64(radMultiColumnComboBox3.EditorControl.Rows[radMultiColumnComboBox3.SelectedIndex].Cells[0].Value);
                row.Count = Convert.ToInt32(radTextBox1.Text);
                switch (FType)
                {
                    case FormType.ADDFORM:
                        // �������� ������������ ������� ������
                        if (!CheckCorrect(dtp, row))
                        {
                            // ������� ������ � �������
                            DBWorker.InsertDataRowToTable("��", row);
                            //DatabaseLib.ParametersCollection par = Converter.ConvertCompositionRowToParameters(row);
                            //DBWorker.dbf.Insert("CompositionProducts", par);
                        }
                        else
                            MessageBox.Show("����������� ������ ������ ���� �����������! ��� ���� ������ � ������� � ������ �������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case FormType.EDITFORM:
                        DatabaseLib.ParametersCollection par = Converter.ConvertCompositionRowToParameters(row);
                        string where = String.Format("(RootCode = {0}) and (WhereCode = {1}) and (WhatCode = {2})",
                                                     row.RootCode, row.WhereCode, row.WhatCode);
                        DBWorker.UpdateRow("CompositionProducts", where, par);
                        break;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("�� ����� ������� ������ ��� �� ������ ������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ������ ������
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region ��������������� �������
        // ��������� ������ �������
        private void LoadProducts1(RadMultiColumnComboBox dgw)
        {
            string str = @"SELECT
                            ProductNames.ProductKey,
                            ProductNames.Name,
                            ProductNames.Designation,
                            ReferenceProducts.Name
                        FROM
                            ProductNames
                            INNER JOIN ReferenceProducts ON (ProductNames.ViewCode = ReferenceProducts.PKey)
                        WHERE
                            ProductNames.ViewCode = 3;";
            dgw.DataSource = DBWorker.dbf.Execute(str);
        }

        // ��������� ������ ��������� ������� � �������
        private void LoadProducts2(RadMultiColumnComboBox dgw)
        {
            string str = @"SELECT
                            ProductNames.ProductKey,
                            ProductNames.Name,
                            ProductNames.Designation,
                            ReferenceProducts.Name
                        FROM
                            ProductNames
                            INNER JOIN ReferenceProducts ON (ProductNames.ViewCode = ReferenceProducts.PKey)
                        WHERE
                            ProductNames.ViewCode = 2 or ProductNames.ViewCode = 3;";
            dgw.DataSource = DBWorker.dbf.Execute(str);
        }

        // ��������� ������ ��������� ������� � ������
        private void LoadProducts3(RadMultiColumnComboBox dgw)
        {
            string str = @"SELECT
                            ProductNames.ProductKey,
                            ProductNames.Name,
                            ProductNames.Designation,
                            ReferenceProducts.Name
                        FROM
                            ProductNames
                            INNER JOIN ReferenceProducts ON (ProductNames.ViewCode = ReferenceProducts.PKey)
                        WHERE
                            ProductNames.ViewCode = 1 or ProductNames.ViewCode = 2;";
            dgw.DataSource = DBWorker.dbf.Execute(str);
        }

        private void TunuUpHeaderOfTheTables(RadMultiColumnComboBox dgw)
        {
            dgw.EditorControl.Columns[0].HeaderText = "��� ���������";
            dgw.EditorControl.Columns[0].Width = 80;
            dgw.EditorControl.Columns[1].HeaderText = "������������";
            dgw.EditorControl.Columns[1].Width = 150;
            dgw.EditorControl.Columns[2].HeaderText = "�����������";
            dgw.EditorControl.Columns[2].Width = 150;
            dgw.EditorControl.Columns[3].HeaderText = "��� ���������";
            dgw.EditorControl.Columns[3].Width = 150;
        }

        /// <summary>
        /// �������� ������������� ����������� � ������� ������ �� ������
        /// </summary>
        /// <param name="dtp">������� � ��� ���������� �������</param>
        /// <param name="row">������ � ������� ��� ��������</param>
        /// <returns>���������� true ���� ������ ��� ����, ����� false</returns>
        private bool CheckCorrect(DataTable dtp, CompositionRow row)
        {
            bool flag = false;
            for (int i = 0; i < dtp.Rows.Count; i++)
            {
                if (dtp.Rows[i].Field<long>(0) == row.RootCode && dtp.Rows[i].Field<long>(1) == row.WhereCode && dtp.Rows[i].Field<long>(2) == row.WhatCode)
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
