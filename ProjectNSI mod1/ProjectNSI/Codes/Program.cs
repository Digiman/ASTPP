//*****************************************************************************
// Главный модуль всего приложения. Запускает на выполнение программу.
//*****************************************************************************
using System;
using System.Windows.Forms;

namespace ProjectNSI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // загрузка настроек приложения
            PrefWorker.LoadSettings();
            // старт главного окна приложения
            Application.Run(new Fmain());
        }
    }
}
