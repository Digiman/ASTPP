//*****************************************************************************
// Контрол для работы с настройками для БД
//*****************************************************************************
using System.Windows.Forms;

namespace ProjectNSI
{
    public partial class PrefDB : UserControl
    {
        #region Конструктор
        public PrefDB()
        {
            InitializeComponent();
            InitializeData();
        }
        #endregion

        #region Обработчики кнопок и компонентов
        // клики по чеку выбора БД
        private void radCheckBox1_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (!radCheckBox1.Checked)
            {
                radTextBox1.Enabled = false;
                radButton1.Enabled = false;
            }
            else
            {
                radTextBox1.Enabled = true;
                radButton1.Enabled = true;
            }
        }

        // кнопка выбора БД
        private void radButton1_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog dg = new OpenFileDialog();
            dg.Title = "Выберите файл с БД";
            dg.Filter = "SQLite v.3 DB|*.db3";
            dg.ShowDialog();
            if (dg.FileName != null && dg.FileName != "" && dg.FileName != " ")
            {
                radTextBox1.Text = dg.FileName;
                // отметим в структуре с настройками это дело

            }
        }
        #endregion

        #region Вспомогательные функции
        // загрузка данных в поля контрола
        private void InitializeData()
        {
            radCheckBox1.Checked = GlobalVars.Prefs.IsAutoOpenDB;
            if (GlobalVars.Prefs.IsAutoOpenDB)
            {
                radTextBox1.Enabled = true;
                radTextBox1.Text = GlobalVars.Prefs.DB;
                radButton1.Enabled = true;
            }
            else
            {
                radTextBox1.Enabled = false;
                radButton1.Enabled = false;
            }
        }
        #endregion
    }
}
