//*****************************************************************************
// Главное окно приложения
//*****************************************************************************
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace ProjectNSI
{
    /// <summary>
    /// Главная форма приложения
    /// </summary>
    public partial class Fmain : RadRibbonForm
    {
        UserControl Main; // контрол, который загружается в главное окно

        #region Конструктор
        public Fmain()
        {
            InitializeComponent();
        }
        #endregion

        #region Кнопки ApplicationMenu
        // кнопка Выход (в главном меню)
        public void radRibbonBar1_ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // кнопка Настройки (в главном меню)
        private void radRibbonBar1_OptionsButton_Click(object sender, EventArgs e)
        {
            PreferencesMain form = new PreferencesMain();
            form.ShowDialog();
        }

        #endregion

        #region События формы
        /// <summary>
        /// Установка параметров при загрузке приложения
        /// </summary>
        private void Fmain_Load(object sender, EventArgs e)
        {
            // подключение БД (если автоматическое)
            if (GlobalVars.Prefs.IsAutoOpenDB)
            {
                ConnectToDB(GlobalVars.DBName);
                radStatusStrip1.Items[2].Text = "Дата последней работы с программой: " + GlobalVars.Prefs.LastWorkDate.ToShortDateString();
            }
            else
            {
                // настройка остальных компонентов
                radStatusStrip1.Items[1].Text = "Состояние БД: Отключено";
                radStatusStrip1.Items[2].Text = "";
            }
            // загрузка главного UC для выбора действий
            Main = new UC_Main(disconnectFromDBButton, radStatusStrip1);
            Main.Dock = DockStyle.Fill;
            radPanel1.Controls.Clear();
            radPanel1.Controls.Add(Main);
            radStatusStrip1.Items[0].Text = "Главная страница";
            // сохранение текущей даты как последней даты работы с программой
            GlobalVars.Prefs.LastWorkDate = DateTime.Now;
            GlobalVars.IsPrefChanged = true;
        }

        // обработка события закрытия окна (и соответственно - приложения)
        private void Fmain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // отключение от БД и удаление временных таблиц
            if (DBWorker.flag)
                DBWorker.DisconnectFromDB();
            // сохранение настроек (если изменены были)
            PrefWorker.SaveSettings();
        }

        // закрытие формы (происходит до ее закрытия)
        private void Fmain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из программы?","Подтверждение выхода", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
        #endregion

        #region События кнопок RadRibbonBar

        #region Панель Функции
        // кнопка для вызова окна справочников (Справочники)
        private void ReferencesButton_Click(object sender, EventArgs e)
        {
            radPanel1.Controls.Clear();
            Main = new ReferenceCatalog_Carusel();
            Main.Dock = DockStyle.Fill;
            radPanel1.Controls.Add(Main);
            radStatusStrip1.Items[0].Text = "Справочники";
        }
        // кнопка Разузлование
        private void ExplodeButton_Click(object sender, EventArgs e)
        {
            Main = new NodesViewer();
            Main.Dock = DockStyle.Fill;
            radPanel1.Controls.Clear();
            radPanel1.Controls.Add(Main);
            radStatusStrip1.Items[0].Text = "Разузлование";
        }
        // кнопка Материальное нормирование
        private void MaterialValuationButton_Click(object sender, EventArgs e)
        {
            Main = new MaterialStandarts();
            Main.Dock = DockStyle.Fill;
            radPanel1.Controls.Clear();
            radPanel1.Controls.Add(Main);
            radStatusStrip1.Items[0].Text = "Материальное нормирование";
        }
        // кнопка Потребность в материалах
        private void MaterialsNeedsButton_Click(object sender, EventArgs e)
        {
            Main = new MaterialsNeeds();
            Main.Dock = DockStyle.Fill;
            radPanel1.Controls.Clear();
            radPanel1.Controls.Add(Main);
            radStatusStrip1.Items[0].Text = "Потребность в материалах";
        }
        // кнопка Трудовое нормирование
        private void ComplexityButton_Click(object sender, EventArgs e)
        {
            Main = new Complexity();
            Main.Dock = DockStyle.Fill;
            radPanel1.Controls.Clear();
            radPanel1.Controls.Add(Main);
            radStatusStrip1.Items[0].Text = "Трудовое нормирование";
        }
        // кнопка Раскрой
        private void CuttingButton_Click(object sender, EventArgs e)
        {
            Main = new Cutting();
            Main.Dock = DockStyle.Fill;
            radPanel1.Controls.Clear();
            radPanel1.Controls.Add(Main);
            radStatusStrip1.Items[0].Text = "Раскрой";
        }
        // кнопка Домой
        private void HomeButton_Click(object sender, EventArgs e)
        {
            Main = new UC_Main(disconnectFromDBButton, radStatusStrip1);
            Main.Dock = DockStyle.Fill;
            radPanel1.Controls.Clear();
            radPanel1.Controls.Add(Main);
            radStatusStrip1.Items[0].Text = "Главная страница";
        }
        #endregion
        
        #region Панель Управление программой
        // кнопка Просмотрщик отчетов
        private void ReportViewer_Click(object sender, EventArgs e)
        {
            ReportViewerForm form = new ReportViewerForm();
            form.Show();
        }
        #endregion
        
        #region Панель БД
        // кнопка Создать БД
        private void CreateDBButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dg = new SaveFileDialog();
            dg.Title = "Введите название файла новой БД";
            dg.DefaultExt = "*.db3";
            dg.Filter = "SQLite v.3 DB|*.db3";
            dg.ShowDialog();
            if (dg.FileName != null && dg.FileName != "" && dg.FileName != " ")
            {
                MessageBox.Show(dg.FileName);
                // создание новой пустой БД
                DBWorker.CreateNewDB(dg.FileName);
                // подключение к созданной БД
                ConnectToDB(dg.FileName);
            }
        }
        // кнопка подключения к БД
        private void connectToDBButton_Click(object sender, EventArgs e)
        {
            string status = GlobalFunctions.ConnectToDB();
            if (DBWorker.flag)
            {
                disconnectFromDBButton.Enabled = true;
            }
            else
            {
                disconnectFromDBButton.Enabled = false;
            }
            radStatusStrip1.Items[1].Text = status;
            InitializeButtons();
        }
        // кнопка Отключение от БД
        private void disconnectFromDBButton_Click(object sender, EventArgs e)
        {
            if (DBWorker.flag)
                DBWorker.DisconnectFromDB();
            disconnectFromDBButton.Enabled = false;
            radStatusStrip1.Items[1].Text = "Состояние БД: Отключено";
        }
        #endregion
        
        #endregion

        #region Вспомогательные функции
        /// <summary>
        /// Подключение к заданной БД
        /// </summary>
        /// <param name="DBName">Путь к файлу с БД</param>
        public void ConnectToDB(string DBName)
        {
            bool exflag = false;
            string str = GlobalFunctions.ConnectToDB(DBName, ref exflag);
            radStatusStrip1.Items[1].Text = str;
            InitializeButtons();
        }

        /// <summary>
        /// Инициализация кнопок на панели
        /// </summary>
        private void InitializeButtons()
        {
            if (!DBWorker.flag)
            {
                MaterialsNeedsButton.Enabled = false;
                MaterialValuationButton.Enabled = false;
                ReferencesButton.Enabled = false;
                ExplodeButton.Enabled = false;
                ComplexityButton.Enabled = false;
                CuttingButton.Enabled = false;
                disconnectFromDBButton.Enabled = false;
            }
            else
            {
                MaterialsNeedsButton.Enabled = true;
                MaterialValuationButton.Enabled = true;
                ReferencesButton.Enabled = true;
                ExplodeButton.Enabled = true;
                ComplexityButton.Enabled = true;
                CuttingButton.Enabled = true;
                disconnectFromDBButton.Enabled = true;
            }
        }

        /// <summary>
        /// Отображение времени по тику таймера
        /// </summary>
        protected virtual void timer1_Tick(object sender, EventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToLongTimeString();
            InitializeButtons();
        }
        #endregion
    }
}
