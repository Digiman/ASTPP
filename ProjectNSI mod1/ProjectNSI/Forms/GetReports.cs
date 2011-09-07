//*****************************************************************************
// ������ ��� ��������� ������� �� ������������ � ����������
//*****************************************************************************
using System;
using System.Windows.Forms;
using System.Data;

namespace ProjectNSI
{
    public partial class GetReports : Telerik.WinControls.UI.RadForm
    {
        int ReportCode; // ��� ��������� (1-� ������� �������, 2-���������)
        MNDates[] dates;

        #region ����������� � �������������
        public GetReports(int repcode)
        {
            InitializeComponent();
            ReportCode = repcode;
            InitializeData();
            // ��������� �����������
            radRadioButton1.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            radGroupBox2.Visible = false;
            // �������� ������� �����
            this.Height = 162;
        }

        private void InitializeData()
        {
            // ����������� ������� � ���, ��� ������� ���� ������ ���������
            int count = 0;
            dates = GlobalFunctions.GetMNDatesCount(ref count);
            for (int i = 0; i < dates.Length; i++)
                radDropDownList1.Items.Add(String.Format("{0} {1}", GlobalFunctions.GetMonthString(dates[i].Month), dates[i].Year));
        }
        #endregion

        #region ����������� ������� ������ � ����������� �����
        // ������ �������
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ������ �������������
        private void radButton1_Click(object sender, EventArgs e)
        {
            if (radRadioButton1.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On) // ���� ��� ��������� ����
            {
                if (radDropDownList1.SelectedIndex >= 0)
                {
                    GenerateReport(1);
                }
                else
                    MessageBox.Show("�� ������ ����� � ��� ��� ������ ���������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (radRadioButton2.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On) // ���� ��� ���� ��� � �������
            {
                GenerateReport(2);
            }
        }

        // ������� ���� � ���������� �� ������� ��������������� ����������
        private void radListControl1_DoubleClick(object sender, EventArgs e)
        {
            if (radListControl1.SelectedIndex < 0)
            {
                MessageBox.Show("�� ������ ����!");
                return;
            }
            else
            {
                OpenReportViewer(radListControl1.SelectedItem.Text, false);
            }
        }
        #endregion

        #region �������� �������
        /// <summary>
        /// ��������� ����������
        /// </summary>
        /// <param name="type">��� ������ ���������� (1-���������, 2 - �������������)</param>
        private void GenerateReport(int type)
        {
            switch (ReportCode) // ����� ���� ���������� ���������
            {
                case 1: // ��������� � ������� �������
                    if (type == 1)
                    {
                        HTMLWorker.GenerateMaterialsNeedsReport(dates[radDropDownList1.SelectedIndex], GlobalFunctions.GetMNReportFileName(dates[radDropDownList1.SelectedIndex]));
                        OpenReportViewer(GlobalFunctions.GetMNReportFileName(dates[radDropDownList1.SelectedIndex]), true);
                    }
                    if (type == 2)
                    {
                        string[] files = HTMLWorker.GenerateMaterialsNeedsReports();
                        OpenReportViewerForMultipleFiles(files);
                    }
                    break;
                case 2: // ��������� ��������� �� ����������
                    if (type == 1)
                    {
                        HTMLWorker.GenerateMaterialsNeeds2Report(dates[radDropDownList1.SelectedIndex], GlobalFunctions.GetMN2ReportFileName(dates[radDropDownList1.SelectedIndex]));
                        OpenReportViewer(GlobalFunctions.GetMN2ReportFileName(dates[radDropDownList1.SelectedIndex]), true);
                    }
                    if (type == 2)
                    {
                        string[] files = HTMLWorker.GenerateMaterialsNeeds2Reports();
                        OpenReportViewerForMultipleFiles(files);
                    }
                    break;
                case 3: // ��������� ����������� ������������ ������. ���������
                    if (type == 1)
                    {
                        HTMLWorker.GenerateComplexityProgramReport(dates[radDropDownList1.SelectedIndex], GlobalFunctions.Get�PReportFileName(dates[radDropDownList1.SelectedIndex]));
                        OpenReportViewer(GlobalFunctions.Get�PReportFileName(dates[radDropDownList1.SelectedIndex]), true);
                    }
                    if (type == 2)
                    {
                        string[] files = HTMLWorker.GenerateComplexityProgramReports();
                        OpenReportViewerForMultipleFiles(files);
                    }
                    break;
                case 4: // ��������� ����������� ������������ ������. ��������� �� �����
                    if (type == 1)
                    {
                        HTMLWorker.GenerateComplexityShopProgramReport(dates[radDropDownList1.SelectedIndex], GlobalFunctions.Get�SPReportFileName(dates[radDropDownList1.SelectedIndex]));
                        OpenReportViewer(GlobalFunctions.Get�SPReportFileName(dates[radDropDownList1.SelectedIndex]), true);
                    }
                    if (type == 2)
                    {
                        string[] files = HTMLWorker.GenerateComplexityShopProgramReports();
                        OpenReportViewerForMultipleFiles(files);
                    }
                    break;
            }
        }

        /// <summary>
        /// �������� ���� ������������ ������� � �������� ������ ���������
        /// </summary>
        /// <param name="str">���� � ������������ ����� ���������</param>
        /// <param name="flag">���� ���������� �� ��������� �� ����� ���������</param>
        private void OpenReportViewer(string str, bool flag)
        {
            if (flag)
                MessageBox.Show("������������� ���� ��������� �� ���������� ������ � ����!", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReportViewerForm form = new ReportViewerForm(str);
            form.Show();
        }

        /// <summary>
        /// �������� ������ ��������� �� ��������� � ��������� �����
        /// </summary>
        /// <param name="files">������ ��������������� ���������� (�����)</param>
        private void OpenReportViewerForMultipleFiles(string[] files)
        {
            if (files.Length == 1)
            {
                // �������� ��������� � ������������ �������
                OpenReportViewer(files[0], false);
            }
            else
            {
                MessageBox.Show("������� ��������� ����������! ����� ������� ������ ���������!", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // ����� ������ ����������
                radListControl1.Items.AddRange(files);
                // �������� �����
                this.Height = 324;
                radGroupBox2.Visible = true;
                // �������� ��������� � ������������ �������
                OpenReportViewer(files[0], false);
            }
        } 
        #endregion

        #region ����� ����� � �����
        private void radRadioButton1_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (radRadioButton1.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                radDropDownList1.Enabled = true;
        }

        private void radRadioButton2_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (radRadioButton2.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                radDropDownList1.Enabled = false;
        }

        #endregion
    }
}
