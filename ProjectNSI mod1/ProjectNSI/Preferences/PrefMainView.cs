//*****************************************************************************
// Контрол с настройками стиля приложения
//*****************************************************************************
using System.Windows.Forms;

namespace ProjectNSI
{
    public partial class PrefMainView : UserControl
    {
        #region Конструктор
        public PrefMainView()
        {
            InitializeComponent();
            // выбор из спика стилей текущего стиля
            radDropDownList1.Text = GlobalVars.Prefs.ThemeName;
        }
        #endregion

        #region Обработчики кнопок и событий компонентов
        private void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = radDropDownList1.SelectedText;
        }

        // кнопка Установить
        private void radButton1_Click(object sender, System.EventArgs e)
        {
            GlobalVars.Prefs.ThemeName = radDropDownList1.SelectedText;
            GlobalVars.IsPrefChanged = true;
            MessageBox.Show("Стиль приложения успешно установлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
