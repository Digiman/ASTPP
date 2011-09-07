//*****************************************************************************
// Контрол домашней страницы, с основными функциями программы
//*****************************************************************************
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace ProjectNSI
{
    /// <summary>
    /// Главный контрол для управления программой в главном окне
    /// </summary>
    public partial class UC_Main : UserControl
    {
        RadButtonElement RibbonButton;
        RadStatusStrip StatusStrip;

        #region Конструктор и инициализация
        public UC_Main(RadButtonElement discButton, RadStatusStrip statusStrip)
        {
            InitializeComponent();
            RibbonButton = discButton;
            StatusStrip = statusStrip;
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            if (DBWorker.flag)
            {
                radButton7.Enabled = true;
                radButton1.Enabled = true;
                radButton2.Enabled = true;
                radButton3.Enabled = true;
                radButton4.Enabled = true;
                radButton12.Enabled = true;
                radButton13.Enabled = true;
            }
            else
            {
                radButton7.Enabled = false;
                radButton1.Enabled = false;
                radButton2.Enabled = false;
                radButton3.Enabled = false;
                radButton4.Enabled = false;
                radButton12.Enabled = false;
                radButton13.Enabled = false;
            }
        }
        #endregion

        #region События кнопок для работы с БД
        // кнопка Создать БД
        private void radButton5_Click(object sender, EventArgs e)
        {
            SaveFileDialog dg = new SaveFileDialog();
            dg.Title = "Введите название файла новой БД";
            dg.DefaultExt = "*.db3";
            dg.Filter = "SQLite v.3 DB|*.db3";
            dg.ShowDialog();
            if (dg.FileName != null && dg.FileName != "" && dg.FileName != " ")
            {
                // создание новой пустой БД
                DBWorker.CreateNewDB(dg.FileName);
                // подключение к созданной БД
                ConnectToDB(dg.FileName);
            }
        }

        // кнопка Создать и заполнить исходными данными
        private void radButton10_Click(object sender, EventArgs e)
        {
            SaveFileDialog dg = new SaveFileDialog();
            dg.Title = "Введите название файла новой БД";
            dg.DefaultExt = "*.db3";
            dg.Filter = "SQLite v.3 DB|*.db3";
            dg.ShowDialog();
            if (dg.FileName != null && dg.FileName != "" && dg.FileName != " ")
            {
                // заполнение данными таблиц БД
                DBWorker.CreateDBandFill(dg.FileName);
                // настройка окна и его компонентов
                RibbonButton.Enabled = true;
                radButton7.Enabled = true;
                string status = String.Format("Состояние БД: Подключено | Файл БД: {0} | Версия SQLite: {1}",
                    GlobalFunctions.ExtactFileName(DBWorker.dbf.Filename), DBWorker.dbf.Version);
                SetDBStatus(status);
            }
        }

        // кнопка Подключение к БД
        private void radButton6_Click(object sender, EventArgs e)
        {
            string status = GlobalFunctions.ConnectToDB();
            if (DBWorker.flag)
            {
                RibbonButton.Enabled = true;
                radButton7.Enabled = true;
            }
            else
            {
                RibbonButton.Enabled = false;
                radButton7.Enabled = false;
            }
            InitializeButtons();
            SetDBStatus(status);
        }

        // кнопка Отключение от БД
        private void radButton7_Click(object sender, EventArgs e)
        {
            if (DBWorker.flag)
                DBWorker.DisconnectFromDB();
            // настройка окна и его компонентов
            InitializeButtons();
            RibbonButton.Enabled = false;
            radButton7.Enabled = false;
            string status = "Состояние БД: Отключено";
            SetDBStatus(status);
        }
        #endregion

        #region События кнопок для функций программы
        // переход к списку справочников
        private void radButton1_Click(object sender, EventArgs e)
        {
            Control par = this.Parent;
            par.Controls.Clear();
            UserControl uc = new ReferenceCatalog_Carusel();
            uc.Dock = DockStyle.Fill;
            par.Controls.Clear();
            par.Controls.Add(uc);
            string status = "Справочники";
            SetStatusTabName(status);
        }

        // кнопка Разузлование
        private void radButton2_Click(object sender, EventArgs e)
        {
            Control par = this.Parent;
            par.Controls.Clear();
            UserControl uc = new NodesViewer();
            uc.Dock = DockStyle.Fill;
            par.Controls.Clear();
            par.Controls.Add(uc);
            string status = "Разузлование";
            SetStatusTabName(status);
        }

        // кнопка Материальное нормирование
        private void radButton3_Click(object sender, EventArgs e)
        {
            Control par = this.Parent;
            par.Controls.Clear();
            UserControl uc = new MaterialStandarts();
            uc.Dock = DockStyle.Fill;
            par.Controls.Clear();
            par.Controls.Add(uc);
            string status = "Материальное нормирование";
            SetStatusTabName(status);
        }

        // кнопка Потребность в материале
        private void radButton4_Click(object sender, EventArgs e)
        {
            Control par = this.Parent;
            par.Controls.Clear();
            UserControl uc = new MaterialsNeeds();
            uc.Dock = DockStyle.Fill;
            par.Controls.Clear();
            par.Controls.Add(uc);
            string status = "Потребность в материалах";
            SetStatusTabName(status);
        }

        // кнопка Трудовое нормирование
        private void radButton12_Click(object sender, EventArgs e)
        {
            Control par = this.Parent;
            par.Controls.Clear();
            UserControl uc = new Complexity();
            uc.Dock = DockStyle.Fill;
            par.Controls.Clear();
            par.Controls.Add(uc);
            string status = "Трудовое нормирование";
            SetStatusTabName(status);
        }

        // кнопка Раскрой
        private void radButton13_Click(object sender, EventArgs e)
        {
            Control par = this.Parent;
            par.Controls.Clear();
            UserControl uc = new Cutting();
            uc.Dock = DockStyle.Fill;
            par.Controls.Clear();
            par.Controls.Add(uc);
            string status = "Раскрой";
            SetStatusTabName(status);
        }
        #endregion

        #region Кнопки для управления программой
        // кнопка Выход
        private void radButton9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // кнопка Настройки
        private void radButton8_Click(object sender, EventArgs e)
        {
            PreferencesMain form = new PreferencesMain();
            form.ShowDialog();
        }

        #endregion

        #region Вспомогательные функции
        /// <summary>
        /// Подключение к заданному файлу с БД
        /// </summary>
        /// <param name="DBName">Путь к файлу с БД</param>
        public void ConnectToDB(string DBName)
        {
            bool exflag = false;
            string status = GlobalFunctions.ConnectToDB(DBName, ref exflag);
            if (exflag)
            {
                RibbonButton.Enabled = true;
                radButton7.Enabled = true;
            }
            else
            {
                RibbonButton.Enabled = false;
                radButton7.Enabled = false;
            }
            InitializeButtons();
            SetDBStatus(status);
        }

        /// <summary>
        ////Установка в статусстипе названия активной вкладки
        /// </summary>
        /// <param name="text">Текст для отображения статуса текущей вкладки</param>
        private void SetStatusTabName(string text)
        {
            StatusStrip.Items[0].Text = text;
        }

        /// <summary>
        /// функция поиска по предкам главного окна приложения и установка в статусстипе статуса БД
        /// </summary>
        /// <param name="status">Строка с текстом статуса БД</param>
        private void SetDBStatus(string status)
        {
            Control par1 = this.Parent; // панель в которой расположен текущий UC
            Control par2 = par1.Parent; // форма главного окна
            Control[] cont = par2.Controls.Find("radStatusStrip1", false);
            // выведем текст с состоянием БД
            ((RadStatusStrip)cont[0]).Items[1].Text = status;
        }

        /// <summary>
        /// События таймера для коррекстной обработки состояния кнопок
        /// </summary>
        protected virtual void timer1_Tick(object sender, EventArgs e)
        {
            InitializeButtons();
        }
        #endregion
    }
}
