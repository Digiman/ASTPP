//*****************************************************************************
// Контрол для настроек каталогов программы
//*****************************************************************************
using System;
using System.Windows.Forms;

namespace ProjectNSI
{
    public partial class PrefMainFolders : UserControl
    {
        #region Конструктор
        public PrefMainFolders()
        {
            InitializeComponent();
            InitializeData();
        }
        #endregion

        #region Обработчики кнопок
        // кнопка выбора каталога для БД
        private void radButton1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dg = new FolderBrowserDialog();
            dg.Description = "Выберите каталог для БД";
            dg.ShowNewFolderButton = true;
            dg.ShowDialog();
            if (dg.SelectedPath != "" && dg.SelectedPath != " " && dg.SelectedPath != null)
            {
                radTextBox2.Text = dg.SelectedPath;
                // занесем сведения в настройки
                GlobalVars.Prefs.DBCatalog = dg.SelectedPath;
                GlobalVars.IsPrefChanged = true;
            }
        }
        #endregion

        #region Вспомогательные функции
        // загрузка данных в элементы контрола
        private void InitializeData()
        {
            radTextBox1.Text = GlobalVars.Prefs.FilesCatalog;
            radTextBox2.Text = GlobalVars.Prefs.DBCatalog;
        }
        #endregion
    }
}
