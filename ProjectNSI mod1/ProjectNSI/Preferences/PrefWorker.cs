//*****************************************************************************
// Модуль для управления настройками и сериализацией файлов
//*****************************************************************************
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ProjectNSI
{
    #region Общие структуры для настроек
    /// <summary>
    /// Структура с переменными программы
    /// </summary>
    public struct GlobalVars
    {
        public static string AppDir;           // каталог с программой
        // файл с настройками программы (хранит структуру Settings)
        public static string PrefFile = @"\Pref.xml";
        public static Settings Prefs;          // загруженные настройки
        public static bool IsPrefChanged;      // изменены ли настройки

        public static string DBName;           // текущая активная БД
    }

    /// <summary>
    /// Cтруктура с настройками программы (сериализуемая)
    /// </summary>
    public struct Settings
    {
        public bool IsAutoOpenDB { get; set; } // подключать ли установленную БД в настройках автоматически 
        public string DB { get; set; }           // рабочая БД (автоматически подключаемая пи старте программы)

        public string FilesCatalog { get; set; } // каталог для файлов исходных данных программы
        public string DBCatalog { get; set; }    // каталог для БД  программы
        
        public string ThemeName { get; set; }    // тема приложения

        public DateTime LastWorkDate { get; set; } // дата последней работы с программой
    } 
    #endregion

    #region Сериализация
    /// <summary>
    /// Класс для сериализации/десериализации объектов в XML-файл
    /// </summary>
    public class ConfigSerialization
    {
        // Лишаем возможности создавать объекты этого класса
        private ConfigSerialization() { }

        // метод для серилизации данных объекта (создания и сохранения файла)
        public static void CreateSettings(object o, string file)
        {
            XmlSerializer myXmlSer = new XmlSerializer(o.GetType());
            //MessageBox.Show(Application.StartupPath + file);
            StreamWriter myWriter = new StreamWriter(Application.StartupPath + file);
            myXmlSer.Serialize(myWriter, o);
            myWriter.Close();
        }
        // метод для десериализации данных в поля объекта (чтение файла параметров Settings)
        public static void LoadSettings(ref Settings o)
        {
            XmlSerializer myXmlSer = new XmlSerializer(typeof(Settings));
            FileStream mySet = new FileStream(Application.StartupPath + GlobalVars.PrefFile, FileMode.Open);
            o = (Settings)myXmlSer.Deserialize(mySet);
            mySet.Close();
        }
    }
    #endregion

    #region Класс для работы с настройками
    /// <summary>
    /// Класс для работы с настройками
    /// </summary>
    class PrefWorker
    {
        /// <summary>
        /// Чтение настроек Settings из файла (или создание его если не был создан)
        /// </summary>
        internal static void LoadSettings()
        {
            try
            {
                // запоминаем каталог с программой
                GlobalVars.AppDir = Application.StartupPath;
                // отмечаем флаг о том, что настройки не изменены
                GlobalVars.IsPrefChanged = false;
                // читаем настройки
                Settings set = new Settings();
                if (File.Exists(GlobalVars.AppDir + GlobalVars.PrefFile)) // проверка существования файла
                {
                    // загружаем параметры из файла
                    ConfigSerialization.LoadSettings(ref set);
                    // устанавливаем параметры
                    GlobalVars.DBName = set.DB;
                    Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = set.ThemeName; // тема приложения (глобально)
                }
                else
                {
                    // пишем значения по умолчанию в теперь уже создаваемый файл настроек (для первого запуска или файла нету)
                    set = SetDefaultParams();
                    GlobalVars.DBName = set.DB;
                    Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = set.ThemeName; // тема приложения (глобально)
                    ConfigSerialization.CreateSettings(set, GlobalVars.PrefFile);
                }
                // прочитанные настройки сохраним в общем месте для программы
                GlobalVars.Prefs = set;
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохранение настроек из глобальных данных
        /// </summary>
        internal static void SaveSettings()
        {
            if (GlobalVars.IsPrefChanged) // если настройки были изменены
                ConfigSerialization.CreateSettings(GlobalVars.Prefs, GlobalVars.PrefFile);
        }

        /// <summary>
        /// Установка параметров по умолчанию (настройки Settings)
        /// </summary>
        /// <returns>Возвращает экземпляр структуры Settings с данными по умолчанию</returns>
        private static Settings SetDefaultParams()
        {
            Settings set = new Settings();
            set.IsAutoOpenDB = true;
            set.DB = String.Format("{0}\\DB\\{1}",GlobalVars.AppDir,"ProjectNSIDB.db3");
            set.DBCatalog = String.Format("{0}\\{1}\\", GlobalVars.AppDir, "Files");
            set.FilesCatalog = String.Format("{0}\\{1}\\", GlobalVars.AppDir, "DB");
            set.LastWorkDate = DateTime.Today;
            set.ThemeName = "Telerik";
            return set;
        }
    }
    #endregion
}
