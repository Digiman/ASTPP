//*****************************************************************************
// Модуль для генерации отчетов по потребностям в материалах
//*****************************************************************************
using System;
using System.Windows.Forms;
using System.Data;

namespace ProjectNSI
{
    public partial class GetReports : Telerik.WinControls.UI.RadForm
    {
        int ReportCode; // код ведомости (1-в разрезе изделий, 2-суммарная)
        MNDates[] dates;

        #region Конструктор и инициализация
        public GetReports(int repcode)
        {
            InitializeComponent();
            ReportCode = repcode;
            InitializeData();
            // настройка компонентов
            radRadioButton1.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            radGroupBox2.Visible = false;
            // нстройка размера формы
            this.Height = 162;
        }

        private void InitializeData()
        {
            // определение месяцов и лет, для которых надо выдать ведомость
            int count = 0;
            dates = GlobalFunctions.GetMNDatesCount(ref count);
            for (int i = 0; i < dates.Length; i++)
                radDropDownList1.Items.Add(String.Format("{0} {1}", GlobalFunctions.GetMonthString(dates[i].Month), dates[i].Year));
        }
        #endregion

        #region Обработчики событий кнопок и компонентов формы
        // кнопка Закрыть
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // кнопка Сгенерировать
        private void radButton1_Click(object sender, EventArgs e)
        {
            if (radRadioButton1.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On) // если для выбранной даты
            {
                if (radDropDownList1.SelectedIndex >= 0)
                {
                    GenerateReport(1);
                }
                else
                    MessageBox.Show("Не выбран месяц и год для выдачи ведомости!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (radRadioButton2.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On) // если для всех дат и изделий
            {
                GenerateReport(2);
            }
        }

        // двойной клик в компоненте со списокм сгенерированных ведомостей
        private void radListControl1_DoubleClick(object sender, EventArgs e)
        {
            if (radListControl1.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран файл!");
                return;
            }
            else
            {
                OpenReportViewer(radListControl1.SelectedItem.Text, false);
            }
        }
        #endregion

        #region Основные функции
        /// <summary>
        /// Генерация ведомостей
        /// </summary>
        /// <param name="type">Тип выдачи ведомостей (1-единичная, 2 - множественная)</param>
        private void GenerateReport(int type)
        {
            switch (ReportCode) // выбор вида выдаваемой ведомости
            {
                case 1: // ведомость в разрезе изделий
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
                case 2: // ведомость суммарная по материалам
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
                case 3: // ведомость нормативной трудоемкости произв. программы
                    if (type == 1)
                    {
                        HTMLWorker.GenerateComplexityProgramReport(dates[radDropDownList1.SelectedIndex], GlobalFunctions.GetСPReportFileName(dates[radDropDownList1.SelectedIndex]));
                        OpenReportViewer(GlobalFunctions.GetСPReportFileName(dates[radDropDownList1.SelectedIndex]), true);
                    }
                    if (type == 2)
                    {
                        string[] files = HTMLWorker.GenerateComplexityProgramReports();
                        OpenReportViewerForMultipleFiles(files);
                    }
                    break;
                case 4: // ведомость нормативной трудоемкости произв. программы по цехам
                    if (type == 1)
                    {
                        HTMLWorker.GenerateComplexityShopProgramReport(dates[radDropDownList1.SelectedIndex], GlobalFunctions.GetСSPReportFileName(dates[radDropDownList1.SelectedIndex]));
                        OpenReportViewer(GlobalFunctions.GetСSPReportFileName(dates[radDropDownList1.SelectedIndex]), true);
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
        /// Открытие окна просмотрщика отчетов с заданным файлом ведомости
        /// </summary>
        /// <param name="str">Путь к необходимому файлу ведомости</param>
        /// <param name="flag">Флаг показывать ли сообщение об одной ведомости</param>
        private void OpenReportViewer(string str, bool flag)
        {
            if (flag)
                MessageBox.Show("Сгенерирована одна ведомость по выбранному месяцу и году!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReportViewerForm form = new ReportViewerForm(str);
            form.Show();
        }

        /// <summary>
        /// Открытие первой ведомости из множества и настройка формы
        /// </summary>
        /// <param name="files">Список сгенерированных ведомостей (файлы)</param>
        private void OpenReportViewerForMultipleFiles(string[] files)
        {
            if (files.Length == 1)
            {
                // открытие ведомости в просмотрщике отчетов
                OpenReportViewer(files[0], false);
            }
            else
            {
                MessageBox.Show("Создано несколько ведомостей! Будет открыта первая ведомость!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // вывод списка ведомостей
                radListControl1.Items.AddRange(files);
                // настойка формы
                this.Height = 324;
                radGroupBox2.Visible = true;
                // открытие ведомости в просмотрщике отчетов
                OpenReportViewer(files[0], false);
            }
        } 
        #endregion

        #region Выбор чеков в датах
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
