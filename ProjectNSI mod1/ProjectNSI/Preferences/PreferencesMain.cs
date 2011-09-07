//*****************************************************************************
// Окно настроек программы. Главный контейнер для вложенных контролов
//*****************************************************************************
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace ProjectNSI
{
    public partial class PreferencesMain : RadForm
    {
        UserControl Main;
        
        public PreferencesMain()
        {
            InitializeComponent();
        }

        //------------- кнопки окна -----------------
        // кнопка Отмена
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // кнопка Применить
        private void acceptButton_Click(object sender, EventArgs e)
        {
            // сохранение изменений в настройках
            PrefWorker.SaveSettings();
        }
        // кнопка ОК
        private void okButton_Click(object sender, EventArgs e)
        {
            // сохранение изменений в настройках
            PrefWorker.SaveSettings();
            this.Close();
        }
        //-------------------------------------------
        // выбор в дереве разделов настроек
        private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            switch (radTreeView1.SelectedNode.Text)
            {
                case "Настройки":
                    break;
                case "Основные":
                    break;
                case "Каталоги":
                    Main = new PrefMainFolders();
                    radPanel1.Controls.Clear();
                    radPanel1.Controls.Add(Main);
                    break;
                case "Вид":
                    Main = new PrefMainView();
                    radPanel1.Controls.Clear();
                    radPanel1.Controls.Add(Main);
                    break;
                case "БД":
                    Main = new PrefDB();
                    radPanel1.Controls.Clear();
                    radPanel1.Controls.Add(Main);
                    break;
            }
        }
    }
}
