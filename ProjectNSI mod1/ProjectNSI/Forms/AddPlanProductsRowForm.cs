//*****************************************************************************
// ���� ��� ������ � �������� ������� "���� ������� �������"
//*****************************************************************************
using Telerik.WinControls.UI;
using System;
using System.Windows.Forms;
using System.Data;

namespace ProjectNSI
{
    public partial class AddPlanProductsRowForm : Telerik.WinControls.UI.RadForm
    {
        FormType FType;
        DataTable dtp;
        PlanProductsRow Row;

        #region ������������ � �������������
        public AddPlanProductsRowForm(FormType ftype)
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

        public AddPlanProductsRowForm(FormType ftype, PlanProductsRow data)
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

        #region �������� ������ � �� ����������
        private void InitializeData()
        {
            // ��������� �������� ������ �� ������� ��� � ������ ������
            LoadProducts(radMultiColumnComboBox1);
            radMultiColumnComboBox1.SelectedIndex = -1;
            // ��������� ��������� �������
            TunuUpHeaderOfTheTables(radMultiColumnComboBox1);
            // �������� ������ �� ������� "���� ������� �������"
            dtp = DBWorker.SelectDataFromTable("���");
            // ���������� ������ �������
            radDropDownList1.Items.AddRange(GlobalFunctions.Monthes);
        }

        // �������� ������ � �������� ��� ��������������
        private void LoadDataToControls(PlanProductsRow data)
        {
            for (int i = 0; i < radMultiColumnComboBox1.EditorControl.Rows.Count; i++)
                if ((long)radMultiColumnComboBox1.EditorControl.Rows[i].Cells[0].Value == data.ProductCode)
                {
                    radMultiColumnComboBox1.SelectedIndex = i;
                    break;
                }
            radTextBox1.Text = data.PlanCount.ToString();
            radDropDownList1.SelectedIndex = data.Month - 1;
            radTextBox2.Text = data.Year.ToString();
            Row = data;
        }
        #endregion

        #region ��������� ������� ������
        // ������ ������
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ������ ��
        private void okButton_Click(object sender, EventArgs e)
        {
            // �������� ����� �� �������
            if (radMultiColumnComboBox1.SelectedIndex >= 0 || radTextBox1.Text != "" || radTextBox2.Text != "" || radDropDownList1.SelectedIndex >= 0)
            {
                PlanProductsRow row = new PlanProductsRow();
                int ind = radMultiColumnComboBox1.SelectedIndex;
                row.ProductCode = dtp.Rows[ind].Field<long>(0);
                row.PlanCount = Convert.ToInt32(radTextBox1.Text);
                row.Month = Convert.ToInt32(radDropDownList1.SelectedIndex + 1);
                row.Year = Convert.ToInt32(radTextBox2.Text);
                switch (FType)
                {
                	case FormType.ADDFORM:
                        // �������� ������������ ������� ������
                        if (!CheckCorrect(dtp, row))
                        {
                            // ������� ������ � �������
                            DBWorker.InsertDataRowToTable("���", row);
                        }
                        else
                            MessageBox.Show("����������� ������ ������ ���� �����������! ��� ���� ������ � ������� � ������ �������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case FormType.EDITFORM:
                        DatabaseLib.ParametersCollection par = Converter.ConvertPlanProductsRowToParameters(row);
                        string where = String.Format("(ProductCode = {0}) and (PlanCount = {1}) and (Month = {2}) and (Year = {3})",
                                                     Row.ProductCode, Row.PlanCount, Row.Month, Row.Year);
                        DBWorker.UpdateRow("PlanProducts", where, par);
                        break;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("�� ������ �������� ��� ����� ������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
        } 
        #endregion

        #region ��������������� �������
        // ��������� ������ �������
        private void LoadProducts(RadMultiColumnComboBox dgw)
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
        /// �������� ������������ ����������� ������
        /// </summary>
        /// <param name="dtp">������, ��� ��������� � �������</param>
        /// <param name="row">����������� ������</param>
        /// <returns>���������� True ���� ������ ��� ����, ����� False</returns>
        private bool CheckCorrect(DataTable dtp, PlanProductsRow row)
        {
            bool flag = false;
            for (int i = 0; i < dtp.Rows.Count; i++)
            {
                if (dtp.Rows[i].Field<long>(0) == row.ProductCode && 
                    Convert.ToInt32(dtp.Rows[i].ItemArray[2]) == row.Month && 
                    Convert.ToInt32(dtp.Rows[i].ItemArray[3]) == row.Year)
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
