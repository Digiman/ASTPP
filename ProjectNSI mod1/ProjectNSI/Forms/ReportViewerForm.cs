//*****************************************************************************
// ���� ��� ��������� � ������ ���������� � ������� � HTML
//*****************************************************************************
using System.Text;
using System.Windows.Forms;
using System;

namespace ProjectNSI
{
    public partial class ReportViewerForm : Telerik.WinControls.UI.RadForm
    {
        bool WBLoadFlag;

        #region ������������
        public ReportViewerForm()
        {
            InitializeComponent();
            printButton.Enabled = false;
            radStatusStrip1.Items[0].Text = "������ ��������";
            SetWindowSize();
        }

        public ReportViewerForm(string fname)
        {
            InitializeComponent();
            LoadReport(fname);
            printButton.Enabled = true;
            SetWindowSize();
        }

        private void SetWindowSize()
        {
            this.Height = (int)Math.Round(Screen.PrimaryScreen.WorkingArea.Height * 0.7);
            this.Width = (int)Math.Round(Screen.PrimaryScreen.WorkingArea.Width * 0.7);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.SetDesktopLocation(100, 100);
        }
        #endregion

        #region ����������� ������� ������
        // ������ �������� ������
        private void OpenReportExecute(object sender, EventArgs e)
        {
            OpenFileDialog dg = new OpenFileDialog();
            dg.Title = "�������� ���� HTML � �������";
            dg.Filter = "HTML Files|*.htm;*.html";
            dg.ShowDialog();
            if (dg.FileName != "")
            {
                LoadReport(dg.FileName);
                printButton.Enabled = true;
            }
        }

        // ������ ������ ������
        private void PrintReportExecute(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
        }
        #endregion
        
        #region �������� �������
        /// <summary>
        /// �������� ������ ������ �� ����� � ���� �����
        /// </summary>
        /// <param name="fname">���� � ����� ������</param>
        private void LoadReport(string fname)
        {
            // �������� �������� � ������� ����
            webBrowser1.DocumentCompleted += wb_DocumentCompleted;
            webBrowser1.Visible = false;
            WBLoadFlag = false;
            webBrowser1.Navigate(GetStringPath(fname));
            while (!WBLoadFlag)
                Application.DoEvents();
            webBrowser1.Visible = true;
            // ��������� ������ �����������
            radStatusStrip1.Items[0].Text = webBrowser1.DocumentTitle;
        }
        #endregion

        #region ��������������� �������
        /// <summary>
        /// ����� ��� �������� ������ � ����� � ����� ��� ������
        /// </summary>
        private string GetStringPath(string str)
        {
            StringBuilder tmp = new StringBuilder("file://" + str);
            for (int i = 0; i < tmp.Length; i++)
                if (tmp[i] == '\\')
                    tmp[i] = '/';
            return tmp.ToString();
        }

        //������� � �������� �������� ������ � WebBrowser
        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WBLoadFlag = true;
        }
        #endregion
    }
}
