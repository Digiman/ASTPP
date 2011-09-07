//*****************************************************************************
// Окно для просмотра и печати ведомостей и отчетов в HTML
//*****************************************************************************
using System.Text;
using System.Windows.Forms;
using System;

namespace ProjectNSI
{
    public partial class ReportViewerForm : Telerik.WinControls.UI.RadForm
    {
        bool WBLoadFlag;

        #region Конструкторы
        public ReportViewerForm()
        {
            InitializeComponent();
            printButton.Enabled = false;
            radStatusStrip1.Items[0].Text = "Пустая страница";
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

        #region Обработчики событий кнопок
        // кнопка открытия отчета
        private void OpenReportExecute(object sender, EventArgs e)
        {
            OpenFileDialog dg = new OpenFileDialog();
            dg.Title = "Выберите файл HTML с отчетом";
            dg.Filter = "HTML Files|*.htm;*.html";
            dg.ShowDialog();
            if (dg.FileName != "")
            {
                LoadReport(dg.FileName);
                printButton.Enabled = true;
            }
        }

        // кнопка печати отчета
        private void PrintReportExecute(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
        }
        #endregion
        
        #region Основные функции
        /// <summary>
        /// Загрузка текста отчета из файла в окно формы
        /// </summary>
        /// <param name="fname">Путь к файлу отчета</param>
        private void LoadReport(string fname)
        {
            // загрузка страницы в браузер окна
            webBrowser1.DocumentCompleted += wb_DocumentCompleted;
            webBrowser1.Visible = false;
            WBLoadFlag = false;
            webBrowser1.Navigate(GetStringPath(fname));
            while (!WBLoadFlag)
                Application.DoEvents();
            webBrowser1.Visible = true;
            // установка текста компонентов
            radStatusStrip1.Items[0].Text = webBrowser1.DocumentTitle;
        }
        #endregion

        #region Вспомогательные функции
        /// <summary>
        /// Метод для создания строки с путем к файлу для печати
        /// </summary>
        private string GetStringPath(string str)
        {
            StringBuilder tmp = new StringBuilder("file://" + str);
            for (int i = 0; i < tmp.Length; i++)
                if (tmp[i] == '\\')
                    tmp[i] = '/';
            return tmp.ToString();
        }

        //событие о процессе загрузки данных в WebBrowser
        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WBLoadFlag = true;
        }
        #endregion
    }
}
