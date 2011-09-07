/*
 * Библиотека удобного доступа к базе данных SQLite
 * Версия 0.5.8
 * История версий:
 * [0.5.8] Добавлен метод VACUUM, который сжимает базу данных         
 *         Добавлен метод Version, возвращающий текущую версию сервера SQLite
 *         Конструктор теперь принимает объект SQLiteConnectionStringBuilder в котором задаются настройки подключения
 * [0.5.7] Добавлены методы открытия (Open) и закрытия (Close) подключения к базе данных. Это позволяет вручную управлять
 *         подключением, открывать и закрывать когда вам будет это нужно
 *         Добавлены методы для управления транзакциями
 *         Возможность управлять свойствами Pragma synchronous
 * [0.5.6] Исправлен конструктор класса для возможности наследования от dbFacade
 *         Список полей при Select можно передавать в виде строки. Раньше можно было только через массив
 *         Оптимизирована работа со строками. Это должно положительно повлиять на скорость создания запросов к базе данных
 * [0.5.5] Добавлен конструктор для задания имени файла базы данных
 *         Добавлена проверка на оператор WHERE
 * [0.5.4] Добавлен Update, в котором параметр where можно передавать через строку, а не через массив.
 *         Проверка на передачу NULL значений. NULL значения можно передавать через Convert.DBNull.
 * [0.5.3] Update возвращает код ошибки или 0 в случае успеха.
 *         Добавлен метод InsertMany, для быстрого добавления массива строк. 
 *         Исправлен алгоритм при составлении связывания таблиц JOIN.
 * [0.5.2] Insert возвращает id последней добавленной строки.
 *         Добавлены методы, возвращающие одну строку в виде ассоциативного массива полей.
 *         Убран метод CreateDatabase, вместо него используется ExecuteNonQuery для создания таблиц.
 * [0.5.1] Исправлены мелкие ошибки.
 * [0.5]beta 
 *       Добавлен класс Select. Помощник в создании сложных запросов выборки.
 * [0.4] Метод для обновления записей. 
 *       Свой класс для обработки исключений ExceptionWarning.
 * [0.3] Методы для удаления данных из базы данных.
 * [0.2] Метод для вставки новых элементов в базу данных.
 *       Вспомогательный класс для параметров.
 * [0.1] Первая версия. Содержит только запросы SELECT
 * 
 * Автор: Евгений Потребенко
 * Распостраняется по лицензии GPL
 * Все права принадлежат Евгению Потребенко (KreZ0n)
 * Официальный сайт: http://krez0n.org.ua
 * Автор не несет ответственности за возможно причененный ущерб данным приложением
 * Ниже представленный код не несет в себе каких-либо деструктивных действий, направленных на уничтожение или
 * изменение данных. Все действия управляются пользователем. Конечный пользователь несет отвественность за
 * использование данного кода. Чтобы обезопасить себя от каких-либо проблем, скачивайте библиотеку только с 
 * официального сайта.
 * Примеры запросов для создание таблиц в SQLite
 * string sql_table1 = @"CREATE TABLE 'Test'(
    'id' INTEGER PRIMARY KEY AUTOINCREMENT,
    'title' TEXT,
    'status' tinyint,
    'topic_id' INTEGER,
    'testdate' datetime NOT null DEFAULT '2009-10-07')";

        string sql_table2 = @"CREATE TABLE 'Test2'(
    'id' INTEGER PRIMARY KEY AUTOINCREMENT,
    'test1id' INTEGER,
    'title' TEXT)";

    //SQL-запрос для индексации поля testdate
    string sql_createindex = "CREATE UNIQUE INDEX indx ON Test (id)";
 */
#define DEBUG
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Text;

/// <summary>
/// Библиотека для быстрой работы с базой данных
/// </summary>
namespace DatabaseLib
{
    /// <summary>
    /// Класс удобного доступа к базе данных. Класс содержит заготовки для получения, удаления, обновления и вставки данных.
    /// </summary>
    public partial class dbFacade
    {
        #region Настройки
        /// <summary>
        /// Путь к файлу базы данных.
        /// </summary>
        private string _filename = Path.Combine(Application.StartupPath, "my.db");
        string ConnectionString = string.Empty;
        private SQLiteConnection connect;
        private SQLiteTransaction transaction;
        private SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder();
        private SQLiteCommand command = new SQLiteCommand();
        #endregion

        #region Конструкторы
        /// <summary>
        /// Настройки подключения.
        /// </summary>
        public dbFacade()
        {
            this.Filename = _filename;
        }

        public dbFacade(string fileName)
        {
            this.Filename = fileName;
        }

        public dbFacade(string fileName, SQLiteConnectionStringBuilder csb)
        {
            this.csb = csb;
            this.Filename = fileName;
        }

        public dbFacade(SQLiteConnectionStringBuilder csb)
        {
            this.csb = csb;
            this.Filename = _filename;
        }
        #endregion

        /// <summary>
        /// Имя файла базы данных
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set
            {
                _filename = value;
                csb.DataSource = _filename;
                csb.UseUTF16Encoding = false;
                csb.Add("New", true);    
            
                connect = new SQLiteConnection(csb.ToString());
            }
        }

        /// <summary>
        /// Версия SQLite
        /// </summary>
        public string Version
        {
            get { return connect.ServerVersion; }
        }

        /// <summary>
        /// Сжимает базу данных
        /// </summary>
        public void Vacuum()
        {
            ConnectionState previousConnectionState = ConnectionState.Closed;
            try
            {
                previousConnectionState = connect.State;
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                command = new SQLiteCommand("VACUUM;", connect);
                command.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Ошибка при выволнении команды VACUUM\n" + error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (previousConnectionState == ConnectionState.Closed)
                {
                    connect.Close();
                }
            }
        }

        #region Создание базы
        /// <summary>
        /// Создание базы данных и таблиц
        /// </summary>
        /// <param name="sql_create_query">Задает строку SQL запроса для создания таблиц БД</param>
        public void CreateDatabase(string sql_create_query)
        {
            // создание файла с новой базой
            SQLiteConnection.CreateFile(_filename);

            ConnectionState previousConnectionState = ConnectionState.Closed;
            using (connect)
            {
                try
                {
                    //проверяем предыдущее состояние
                    previousConnectionState = connect.State;
                    if (connect.State == ConnectionState.Closed)
                    {
                        //открываем соединение
                        connect.Open();
                    }
                    //создаем новые таблицы
                    SQLiteCommand command = new SQLiteCommand(sql_create_query, connect);
                    command.ExecuteNonQuery();
                }
                catch (Exception ext)
                {
                    MessageBox.Show("Ошибка: " + ext.Message);
                }
                finally
                {
                    //закрываем соединение, если оно было закрыто перед открытием
                    if (previousConnectionState == ConnectionState.Closed)
                    {
                        connect.Close();
                    }
                }
            }
        }
        #endregion

        #region Открытие и закрытие подключения
        /// <summary>
        /// Открытие подключения
        /// </summary>
        /// <returns>True если все прошло успешно</returns>
        public bool Open()
        {
            try
            {
                connect.Open();
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Ошибка при открытии подключения к базе данных " + _filename + "\n" + error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Закрытие подключения
        /// </summary>
        /// <returns>True если все прошло успешно</returns>
        public bool Close()
        {
            try
            {
                if (connect.State == ConnectionState.Open)
                    connect.Close();
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Ошибка при закрытии подключения к базе данных " + _filename + "\n" + error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Управление транзакциями
        /// <summary>
        /// Начать транзакцию.
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();
                transaction = connect.BeginTransaction();
            }
            catch (Exception error)
            {
                MessageBox.Show("Ошибка при попытке начать транзакцию!\n" + error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закончить транзакцию.
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (transaction != null && connect.State != ConnectionState.Closed)
                    transaction.Commit();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Отменить транзакцию.
        /// </summary>
        public void RollBack()
        {
            try
            {
                if (transaction != null && connect.State != ConnectionState.Closed)
                    transaction.Rollback();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Выполнение любого запроса
        /// <summary>
        /// Выполняет запрос к базе данных.
        /// </summary>
        /// <param name="query">Строка запроса</param>
        /// <returns>Код ошибки. Если 0, ошибки нет</returns>
        public int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(new string[] { query });
        }

        /// <summary>
        /// Выполняет запрос к базе данных.
        /// </summary>
        /// <param name="queries">Массив запросов</param>
        /// <returns>Код ошибки. Если 0, ошибки нет</returns>
        public int ExecuteNonQuery(object[] queries)
        {
            ConnectionState previousConnectionState = ConnectionState.Closed;
            try
            {
                //проверяем предыдущее состояние
                previousConnectionState = connect.State;
                if (connect.State == ConnectionState.Closed)
                {
                    //открываем соединение
                    connect.Open();
                }
                //выполняем запросы
                SQLiteCommand command = new SQLiteCommand(connect);
                foreach (string query in queries)
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                return 1;
            }
            finally
            {
                //закрываем соединение, если оно было закрыто перед открытием
                if (previousConnectionState == ConnectionState.Closed)
                {
                    connect.Close();
                }
            }
            //если нет ошибки возвращать 0
            return 0;
        }
        #endregion

        #region Получение данных
        /// <summary>
        /// Получает данные из таблицы.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <returns>Таблица с данными</returns>
        public DataTable FetchAll(string tablename)
        {
            return FetchAll(tablename, "", "");
        }

        /// <summary>
        /// Получает данные из таблицы.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="where">Строка условий, начинающихся с WHERE</param>
        /// <returns>Таблица с данными</returns>
        public DataTable FetchAll(string tablename, string where)
        {
            return FetchAll(tablename, where, "");
        }

        /// <summary>
        /// Получает данные из таблицы.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="where">Строка условий, начинающихся с WHERE</param>
        /// <param name="etc">Остальные параметры: сортировка, группировка и т.д.</param>
        /// <returns>Таблица с данными</returns>
        public DataTable FetchAll(string tablename, string where, string etc)
        {
            DataTable dt = new DataTable();
            if (where.Length != 0 && !where.ToLower().Trim().StartsWith("where"))
            {
                where = "WHERE " + where;
            }
            string sql = string.Format("SELECT * FROM {0} {1} {2}", tablename, where, etc);
            return Execute(sql);
        }

        /// <summary>
        /// Получение данных по выбранным полям (колонкам).
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Массив колонок, которые необходимо получить</param>
        /// <returns>Таблица с данными</returns>
        public DataTable FetchByColumn(string tablename, string[] columns)
        {
            return FetchByColumn(tablename, columns, "", "");
        }

        /// <summary>
        /// Получение данных по выбранным полям (колонкам).
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Строка колонок через запятую, которые необходимо получить</param>
        /// <returns>Таблица с данными</returns>
        public DataTable FetchByColumn(string tablename, string columns)
        {
            return FetchByColumn(tablename, columns, "", "");
        }

        /// <summary>
        /// Получение данных по выбранным полям (колонкам).
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Массив колонок, которые необходимо получить</param>
        /// <param name="where">Строка условий, начинающихся с WHERE</param>
        /// <returns>Таблица с данными</returns>
        public DataTable FetchByColumn(string tablename, string[] columns, string where)
        {
            return FetchByColumn(tablename, columns, where, "");
        }

        /// <summary>
        /// Получение данных по выбранным полям (колонкам).
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Строка колонок через запятую, которые необходимо получить</param>
        /// <param name="where">Строка условий, начинающихся с WHERE</param>
        /// <returns>Таблица с данными</returns>
        public DataTable FetchByColumn(string tablename, string columns, string where)
        {
            return FetchByColumn(tablename, columns, where, "");
        }

        /// <summary>
        /// Получение данных по выбранным полям (колонкам).
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Массив колонок, которые необходимо получить</param>
        /// <param name="where">Строка условий, начинающихся с WHERE</param>
        /// <param name="etc">Остальные параметры: сортировка, группировка и т.д.</param>
        /// <returns>Таблица с данными</returns>
        public DataTable FetchByColumn(string tablename, string[] columns, string where, string etc)
        {
            return FetchByColumn(tablename, columnsToLine(columns), where, etc);
        }

        /// <summary>
        /// Получение данных по выбранным полям (колонкам).
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Строка колонок через запятую, которые необходимо получить</param>
        /// <param name="where">Строка условий, начинающихся с WHERE</param>
        /// <param name="etc">Остальные параметры: сортировка, группировка и т.д.</param>
        /// <returns>Таблица с данными</returns>
        public DataTable FetchByColumn(string tablename, string columns, string where, string etc)
        {
            if (where.Length != 0 && !where.ToLower().Trim().StartsWith("where"))
            {
                where = "WHERE " + where;
            }
            string sql = string.Format("SELECT {0} FROM {1} {2} {3}", columns, tablename, where, etc);
            return Execute(sql);
        }

        /// <summary>
        /// Выполняет запрос, созданный с помощью конструктора класса Select.
        /// </summary>
        /// <param name="select">Объект запроса</param>
        /// <returns>Таблица с данными</returns>
        public DataTable Execute(Select select)
        {
            return Execute(select.SelectCommand);
        }

        /// <summary>
        /// Выполняет переданный запрос в виде строки.
        /// </summary>
        /// <param name="query">Строка запроса</param>
        /// <returns>Таблица с данными</returns>
        public DataTable Execute(string query)
        {
            DataTable dt = new DataTable();
            ConnectionState previousConnectionState = ConnectionState.Closed;
            try
            {
                previousConnectionState = connect.State;
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                command = new SQLiteCommand(query, connect);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.Message, "Ошибка при получении данных из базы!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (previousConnectionState == ConnectionState.Closed)
                {
                    connect.Close();
                }
            }
            return dt;
        }

        #region Получение одной строки
        /// <summary>
        /// Вернуть первую строку из полученных данных.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Массив колонок, которые необходимо получить</param>
        /// <returns>Ассоциативный массив</returns>
        public Dictionary<string, object> FetchOneRow(string tablename, string[] columns)
        {
            return FetchOneRow(tablename, columns, "", "");
        }

        /// <summary>
        /// Вернуть первую строку из полученных данных.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Строка колонок через запятую, которые необходимо получить</param>
        /// <returns>Ассоциативный массив</returns>
        public Dictionary<string, object> FetchOneRow(string tablename, string columns)
        {
            return FetchOneRow(tablename, columns, "", "");
        }

        /// <summary>
        /// Вернуть первую строку из полученных данных.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Массив колонок, которые необходимо получить</param>
        /// <param name="where">Строка условий, начинающихся с WHERE</param>
        /// <returns>Ассоциативный массив</returns>
        public Dictionary<string, object> FetchOneRow(string tablename, string[] columns, string where)
        {
            return FetchOneRow(tablename, columns, where, "");
        }

        /// <summary>
        /// Вернуть первую строку из полученных данных.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Строка колонок через запятую, которые необходимо получить</param>
        /// <param name="where">Строка условий, начинающихся с WHERE</param>
        /// <returns>Ассоциативный массив</returns>
        public Dictionary<string, object> FetchOneRow(string tablename, string columns, string where)
        {
            return FetchOneRow(tablename, columns, where, "");
        }

        /// <summary>
        /// Вернуть первую строку из полученных данных.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Массив колонок, которые необходимо получить</param>
        /// <param name="where">Строка условий, начинающихся с WHERE</param>
        /// <param name="etc">Остальные параметры: сортировка, группировка и т.д.</param>
        /// <returns>Ассоциативный массив</returns>
        public Dictionary<string, object> FetchOneRow(string tablename, string[] columns, string where, string etc)
        {
            return FetchOneRow(tablename, columnsToLine(columns), where, etc);
        }

        /// <summary>
        /// Вернуть первую строку из полученных данных.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Строка колонок через запятую, которые необходимо получить</param>
        /// <param name="where">Строка условий, начинающихся с WHERE</param>
        /// <param name="etc">Остальные параметры: сортировка, группировка и т.д.</param>
        /// <returns>Ассоциативный массив</returns>
        public Dictionary<string, object> FetchOneRow(string tablename, string columns, string where, string etc)
        {
            if (where.Length != 0 && !where.Trim().ToLower().StartsWith("where"))
            {
                where = "WHERE " + where;
            }
            string sql = string.Format("SELECT {0} FROM {1} {2} {3}", columns, tablename, where, etc);
            return FetchOneRow(sql);
        }

        /// <summary>
        /// Вернуть первую строку из полученных данных.
        /// </summary>
        /// <param name="select">Объект Select</param>
        /// <returns>Ассоциативный массив</returns>
        public Dictionary<string, object> FetchOneRow(Select select)
        {
            return FetchOneRow(select.SelectCommand);
        }

        /// <summary>
        /// Вернуть первую строку из полученных данных.
        /// </summary>
        /// <param name="query">Строка запроса</param>
        /// <returns>Ассоциативный массив</returns>
        public Dictionary<string, object> FetchOneRow(string query)
        {
            Dictionary<string, object> rowItem = new Dictionary<string, object>();
            ConnectionState previousConnectionState = ConnectionState.Closed;
            try
            {
                previousConnectionState = connect.State;
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                command = new SQLiteCommand(query, connect);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        rowItem.Add(reader.GetName(i), reader[i]);
                    }
                }
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.Message, "Ошибка при получении данных из базы!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (previousConnectionState == ConnectionState.Closed)
                {
                    connect.Close();
                }
            }
            return rowItem;
        }
        #endregion

        #endregion

        #region Вставка данных
        /// <summary>
        /// Вставляет данные в таблицу.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="parameters">Коллекция параметров</param>
        /// <returns>ID последней вставленной строки</returns>
        public int Insert(string tablename, ParametersCollection parameters)
        {
            int lastId = 0;
            ConnectionState previousConnectionState = ConnectionState.Closed;
            try
            {
                previousConnectionState = connect.State;
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                command = new SQLiteCommand(connect);
                bool ifFirst = true;
                StringBuilder queryColumns = new StringBuilder("("); //список полей, в которые вставляются новые значения
                StringBuilder queryValues = new StringBuilder("(");  //список значений для этих полей
                foreach (Parameter iparam in parameters)
                {
                    //добавляем новый параметр
                    command.Parameters.Add("@" + iparam.ColumnName, iparam.DbType).Value = Convert.IsDBNull(iparam.Value) ? Convert.DBNull : iparam.Value;
                    //собираем колонки и значения в одну строку
                    if (ifFirst)
                    {
                        queryColumns.Append(iparam.ColumnName);
                        queryValues.Append("@" + iparam.ColumnName);
                        ifFirst = false;
                    }
                    else
                    {
                        queryColumns.Append("," + iparam.ColumnName);
                        queryValues.Append(",@" + iparam.ColumnName);
                    }
                }
                queryColumns.Append(")");
                queryValues.Append(")");
                //создаем новый запрос
                string sql = string.Format("INSERT INTO {0} {1} VALUES {2}", tablename, queryColumns, queryValues);
                command.CommandText = sql;
                command.ExecuteNonQuery();
                //получение последнего ID
                sql = "SELECT last_insert_rowid();";
                command = new SQLiteCommand(sql, connect);
                lastId = int.Parse(command.ExecuteScalar().ToString());
            }
            catch (Exception error)
            {
                //System.Windows.Forms.MessageBox.Show(error.Message, "Ошибка при вставке нового значения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("ERROR: " + error.Message);
                return 0;
            }
            finally
            {
                if (previousConnectionState == ConnectionState.Closed)
                {
                    connect.Close();
                }
            }
            return lastId;
        }

        /// <summary>
        /// Вставляет несколько записей в таблицу.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="parametersCollection">Массив параметров/записей</param>
        /// <returns>Возвращает 0, если удачно</returns>
        public int InsertMany(string tablename, ParametersCollection[] parametersCollection)
        {
            ConnectionState previousConnectionState = ConnectionState.Closed;
            try
            {
                previousConnectionState = connect.State;
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                command = new SQLiteCommand(connect);
                foreach (ParametersCollection parameters in parametersCollection)
                {
                    bool ifFirst = true;
                    StringBuilder queryColumns = new StringBuilder("("); //список полей, в которые вставляются новые значения
                    StringBuilder queryValues = new StringBuilder("(");  //список значений для этих полей
                    foreach (Parameter iparam in parameters)
                    {
                        //добавляем новый параметр
                        command.Parameters.Add("@" + iparam.ColumnName, iparam.DbType).Value = Convert.IsDBNull(iparam.Value) ? Convert.DBNull : iparam.Value;
                        //собираем колонки и значения в одну строку
                        if (ifFirst)
                        {
                            queryColumns.Append(iparam.ColumnName);
                            queryValues.Append("@" + iparam.ColumnName);
                            ifFirst = false;
                        }
                        else
                        {
                            queryColumns.Append("," + iparam.ColumnName);
                            queryValues.Append(",@" + iparam.ColumnName);
                        }
                    }
                    queryColumns.Append(")");
                    queryValues.Append(")");
                    //создаем новый запрос
                    string sql = string.Format("INSERT INTO {0} {1} VALUES {2}", tablename, queryColumns, queryValues);
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception error)
            {
#if DEBUG
                System.Windows.Forms.MessageBox.Show(error.Message, "Ошибка при вставке новой записи в таблицу " + tablename, MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                return 1;
            }
            finally
            {
                if (previousConnectionState == ConnectionState.Closed)
                {
                    connect.Close();
                }
            }
            return 0;
        }
        #endregion

        #region Удаление данных
        /// <summary>
        /// Удаляет все данные из выбранной таблицы.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <returns>Код ошибки. Если 0, ошибки нет</returns>
        public int Delete(string tablename)
        {
            string sql = string.Format("DELETE FROM {0}", tablename);
            ConnectionState previousConnectionState = ConnectionState.Closed;
            try
            {
                previousConnectionState = connect.State;
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                command = new SQLiteCommand(sql, connect);
                command.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.Message, "Ошибка при удалении данных из таблицы " + tablename, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            finally
            {
                if (previousConnectionState == ConnectionState.Closed)
                {
                    connect.Close();
                }
            }
            return 0;
        }

        /// <summary>
        /// Удаляет все данные по условию.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="where">Строка условий, начинающихся с WHERE</param>
        /// <returns>Код ошибки. Если 0, ошибки нет</returns>
        public int Delete(string tablename, string where)
        {
            if (where.Length != 0 && !where.ToLower().Trim().StartsWith("where"))
            {
                where = "WHERE " + where;
            }
            string sql = string.Format("DELETE FROM {0} {1}", tablename, where);
            ConnectionState previousConnectionState = ConnectionState.Closed;
            try
            {
                previousConnectionState = connect.State;
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                command = new SQLiteCommand(sql, connect);
                command.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.Message, "Ошибка при удалении данных из таблицы " + tablename, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            finally
            {
                if (previousConnectionState == ConnectionState.Closed)
                {
                    connect.Close();
                }
            }
            return 0;
        }

        /// <summary>
        /// Удаляет данные из таблицы с массивом условий.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="column">Название поля</param>
        /// <param name="collection">Массив объектов с условием</param>
        /// <returns>Код ошибки. Если 0, ошибки нет</returns>
        public int Delete(string tablename, string column, object[] collection)
        {
            ConnectionState previousConnectionState = ConnectionState.Closed;
            try
            {
                previousConnectionState = connect.State;
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                #region Создаем строку условий
                bool ifFirst = true;
                StringBuilder where = new StringBuilder();
                foreach (object item in collection)
                {
                    if (ifFirst)
                    {
                        where.Append("WHERE " + column + " = '" + item + "'");
                        ifFirst = false;
                    }
                    else
                    {
                        where.Append(" OR " + column + " = '" + item + "'");
                    }
                }
                #endregion

                string sql = string.Format("DELETE FROM {0} {1}", tablename, where);
                command = new SQLiteCommand(sql, connect);
                command.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.Message, "Ошибка при удалении данных из таблицы " + tablename, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            finally
            {
                if (previousConnectionState == ConnectionState.Closed)
                {
                    connect.Close();
                }
            }
            return 0;
        }
        #endregion

        #region Обновление данных
        /// <summary>
        /// Перезаписывает данные в выбранной таблице.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="collection">Коллекция полей и значений</param>
        /// <param name="where">Строка условия</param>
        /// <returns>Код ошибки. Если 0, ошибки нет</returns>
        public int Update(string tablename, ParametersCollection collection, string where)
        {
            return Update(tablename, collection, new[] { where }, "");
        }

        /// <summary>
        /// Перезаписывает данные в выбранной таблице.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="collection">Коллекция полей и значений</param>
        /// <param name="whereparams">Набор условий</param>
        /// <param name="whereseparator">Разделитель между условиями OR или AND</param>
        /// <returns>Код ошибки. Если 0, ошибки нет</returns>
        public int Update(string tablename, ParametersCollection collection, object[] whereparams, string whereseparator)
        {
            ConnectionState previousConnectionState = ConnectionState.Closed;
            try
            {
                //проверяем переданные аргументы
                if (whereparams.Length == 0) throw (new ExceptionWarning("Ошибка! Не указано ни одно условие"));
                if (whereparams.Length > 1 && whereseparator.Trim().Length == 0) throw (new ExceptionWarning("При использовании нескольких условий, требуется указать разделитель OR или AND"));

                previousConnectionState = connect.State;
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                int i = 0;
                //готовим переменную для сбора полей и их значений
                StringBuilder sql_params = new StringBuilder();
                bool ifFirst = true;
                command = new SQLiteCommand(connect);
                //в цикле создаем строку запроса 
                foreach (Parameter param in collection)
                {
                    if (ifFirst)
                    {
                        sql_params.Append(param.ColumnName + " = @param" + i);
                        ifFirst = false;
                    }
                    else
                    {
                        sql_params.Append("," + param.ColumnName + " = @param" + i);
                    }
                    //и добавляем параметры с таким же названием
                    command.Parameters.Add("@param" + i, param.DbType).Value = Convert.IsDBNull(param.Value) ? Convert.DBNull : param.Value;
                    i++;
                }

                //условия для запроса
                StringBuilder sql_where = new StringBuilder();
                ifFirst = true;
                //собираем строку с условиями
                foreach (object item in whereparams)
                {
                    if (ifFirst)
                    {
                        sql_where.Append(item.ToString());
                        ifFirst = false;
                    }
                    else
                    {
                        sql_where.Append(" " + whereseparator + " " + item);
                    }
                }

                //собираем запрос воедино
                command.CommandText = string.Format("UPDATE {0} SET {1} {2}", tablename, sql_params, "WHERE " + sql_where.ToString());
                //выполняем запрос
                command.ExecuteNonQuery();
            }
            catch (ExceptionWarning message)
            {
                System.Windows.Forms.MessageBox.Show(message.MessageText, "Ошибка при обновлении данных в таблице " + tablename, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 1;
            }
            catch (Exception error)
            {
#if DEBUG
                System.Windows.Forms.MessageBox.Show(error.Message, "Ошибка при обновлении данных в таблице " + tablename, MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                return 2;
            }
            finally
            {
                if (previousConnectionState == ConnectionState.Closed)
                {
                    connect.Close();
                }
            }
            return 0;
        }
        #endregion

        /// <summary>
        /// Массив колонок в строку
        /// </summary>
        /// <param name="columns">Массив колонок</param>
        /// <returns>Колонки через запятую</returns>
        private string columnsToLine(string[] columns)
        {
            string textofcolumns = string.Empty;
            if (columns == null || columns.Length == 0)
                textofcolumns = "*";
            else
            {
                textofcolumns = string.Join(",", columns);
            }
            return textofcolumns.ToString();
        }
    }

    #region Параметры
    /// <summary>
    /// Класс параметра, для передачи конструктору запроса.
    /// </summary>
    public class Parameter
    {
        #region Поля
        string _columnName;
        object _value;
        DbType _dbType;
        #endregion

        /// <summary>
        /// Значение поля.
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Название поля в базе данных.
        /// </summary>
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        /// <summary>
        /// Тип значения. 
        /// </summary>
        public DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }
    }

    /// <summary>
    /// Коллекция параметров.
    /// </summary>
    public class ParametersCollection : CollectionBase
    {
        /// <summary>
        /// Добавляет параметры в коллекцию.
        /// </summary>
        /// <param name="iparam">Параметр</param>
        public virtual void Add(Parameter iparam)
        {
            //добавляем в общую коллекцию
            this.List.Add(iparam);
        }

        /// <summary>
        /// Добавляет параметр в коллекцию.
        /// </summary>
        /// <param name="columnName">Имя поля/колонки</param>
        /// <param name="value">Значение</param>
        /// <param name="dbType">Тип значения</param>
        public virtual void Add(string columnName, object value, DbType dbType)
        {
            //Инициализируем объект с параметром
            Parameter iparam = new Parameter();
            //присваиваем название поля
            iparam.ColumnName = columnName;
            //присваиваем значение
            iparam.Value = value;
            //присваиваем тип значения
            iparam.DbType = dbType;
            //добавляем в общую коллекцию
            //List описан в "родителе"
            this.List.Add(iparam);
        }

        /// <summary>
        /// Возвращает элемент по индексу.
        /// </summary>
        /// <param name="Index">Индекс</param>
        /// <returns>Параметр, для передачи конструктору запроса</returns>
        public virtual Parameter this[int Index]
        {
            get
            {
                //возвращает элемент по индексу
                //используется в конструкции foreach
                return (Parameter)this.List[Index];
            }
        }
    }
    #endregion

    #region Класс исключений
    /// <summary>
    /// Класс исключения для проверки строки запроса для базы данных.
    /// </summary>
    class ExceptionWarning : Exception
    {
        private string _messageText;

        /// <summary>
        /// Текст сообщения об ошибке.
        /// </summary>
        public string MessageText
        {
            get { return _messageText; }
        }

        /// <summary>
        /// Текст сообщения об ошибке.
        /// </summary>
        /// <param name="messagetext">Сообщение об ошибке</param>
        public ExceptionWarning(string messagetext)
            : base()
        {
            _messageText = messagetext;
        }
    }
    #endregion

    /// <summary>
    /// Типы связывания таблиц между собой.
    /// </summary>
    public enum SQLJoinTypes
    {
        INNER_JOIN = 1,
        LEFT_JOIN,
        RIGHT_JOIN,
        FULL_JOIN,
        CROSS_JOIN
        //NATURAL_JOIN
    }

    /// <summary>
    /// Класс для создания строки запроса. Работает как конструктор запроса. 
    /// </summary>
    public class Select
    {
        #region Константы
        const string DISTINCT = "distinct";
        const string COLUMNS = "columns";
        const string FROM = "from";
        const string UNION = "union";
        const string WHERE = "where";
        const string GROUP = "group";
        const string HAVING = "having";
        const string ORDER = "order";
        const string LIMIT_COUNT = "limitcount";
        const string LIMIT_OFFSET = "limitoffset";
        const string FOR_UPDATE = "forupdate";

        const string INNER_JOIN = "inner join";
        const string LEFT_JOIN = "left join";
        const string RIGHT_JOIN = "right join";
        const string FULL_JOIN = "full join";
        const string CROSS_JOIN = "cross join";
        const string NATURAL_JOIN = "natural join";

        const string SQL_WILDCARD = "*";
        const string SQL_SELECT = "SELECT";
        const string SQL_UNION = "UNION";
        const string SQL_UNION_ALL = "UNION ALL";
        const string SQL_FROM = "FROM";
        const string SQL_WHERE = "WHERE";
        const string SQL_DISTINCT = "DISTINCT";
        const string SQL_GROUP_BY = "GROUP BY";
        const string SQL_ORDER_BY = "ORDER BY";
        const string SQL_HAVING = "HAVING";
        const string SQL_FOR_UPDATE = "FOR UPDATE";
        const string SQL_AND = "AND";
        const string SQL_AS = "AS";
        const string SQL_OR = "OR";
        const string SQL_ON = "ON";
        const string SQL_ASC = "ASC";
        const string SQL_DESC = "DESC";
        const string SQL_LIMIT = "LIMIT";
        #endregion

        #region Приватные переменные
        private string _from = string.Empty;
        private string _columns = "*";
        private string _where = string.Empty;
        private string _group = string.Empty;
        private string _having = string.Empty;
        private string _order = string.Empty;
        private int _limitCount = 0, _limitOffset = 0;
        List<JoinObj> _collectionJoin = new List<JoinObj>();
        #endregion

        /// <summary>
        /// Строка запроса.
        /// </summary>
        public string SelectCommand
        {
            get { return _constructor(); }
        }

        private class JoinObj
        {
            string _name, _conditional;
            SQLJoinTypes _type;

            /// <summary>
            /// Условное выражение
            /// </summary>
            public string Conditional
            {
                get { return _conditional; }
            }

            /// <summary>
            /// Имя таблицы
            /// </summary>
            public string Name
            {
                get { return _name; }
            }

            /// <summary>
            /// Тип связывания
            /// </summary>
            public string SQLJoinType
            {
                get
                {
                    switch (_type)
                    {
                        case SQLJoinTypes.INNER_JOIN:
                            return INNER_JOIN;
                        case SQLJoinTypes.LEFT_JOIN:
                            return LEFT_JOIN;
                        case SQLJoinTypes.RIGHT_JOIN:
                            return RIGHT_JOIN;
                        case SQLJoinTypes.FULL_JOIN:
                            return FULL_JOIN;
                        case SQLJoinTypes.CROSS_JOIN:
                            return CROSS_JOIN;
                        //case SQLJoinTypes.NATURAL_JOIN:
                        //    return NATURAL_JOIN;
                        //    break;  
                        default: return "";
                    }
                }
            }

            public JoinObj(string name, string conditional, SQLJoinTypes type)
            {
                this._name = name;
                this._conditional = conditional;
                this._type = type;
            }
        }

        #region Публичные методы
        /// <summary>
        /// Задает имя таблицы.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select From(string tablename)
        {
            this._from = tablename;
            return this;
        }

        /// <summary>
        /// Задает имя таблицы и возвращаемые поля.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Поля, которые нужно получить</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select From(string tablename, string[] columns)
        {
            this._from = tablename;

            if (columns == null || columns.Length == 0)
            {
                this._columns = SQL_WILDCARD;
            }
            else
                this._columns = columnsToLine(columns);
            return this;
        }

        /// <summary>
        /// Задает имя таблицы и возвращаемые поля.
        /// </summary>
        /// <param name="tablename">Имя таблицы</param>
        /// <param name="columns">Поля, которые нужно получить</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select From(string tablename, string columns)
        {
            this._from = tablename;

            if (columns == null || columns.Length == 0)
            {
                this._columns = SQL_WILDCARD;
            }
            else
                this._columns = columns;
            return this;
        }

        /// <summary>
        /// Задает список полей, которые будут возвращены
        /// </summary>
        /// <param name="columns">Поля, которые нужно получить</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select Columns(string[] columns)
        {
            if (columns == null || columns.Length == 0)
            {
                this._columns = SQL_WILDCARD;
            }
            else
                this._columns = columnsToLine(columns);
            return this;
        }

        /// <summary>
        /// Задает список полей, которые будут возвращены
        /// </summary>
        /// <param name="columns">Поля, которые нужно получить</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select Columns(string columns)
        {
            if (columns == null || columns.Length == 0)
            {
                this._columns = SQL_WILDCARD;
            }
            else
                this._columns = columns;
            return this;
        }

        /// <summary>
        /// Условия. 
        /// </summary>
        /// <param name="where">Перечисление условий без WHERE</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select Where(string where)
        {
            this._where = where;
            return this;
        }

        /// <summary>
        /// Связывание таблиц.
        /// </summary>
        /// <param name="name">Имя таблицы с которой связываемся</param>
        /// <param name="conditional">Условия связывания</param>
        /// <param name="type">Тип связывания</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select Join(string name, string conditional, SQLJoinTypes type)
        {
            _collectionJoin.Add(new JoinObj(name, conditional, type));
            return this;
        }

        /// <summary>
        /// Группировка.
        /// </summary>
        /// <param name="group">Поле или поля через запятую для группировки</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select Group(string group)
        {
            this._group = group;
            return this;
        }

        /// <summary>
        /// Вычисление табличного выражения.
        /// </summary>
        /// <param name="having">Условие</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select Having(string having)
        {
            this._having = having;
            return this;
        }

        /// <summary>
        /// Порядок сортировки.
        /// </summary>
        /// <param name="order">Поле для группировки</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select Order(string order)
        {
            this._order = order;
            return this;
        }

        /// <summary>
        /// Лимит на выборку.
        /// </summary>
        /// <param name="count">Количество записей</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select Limit(int count)
        {
            this._limitCount = count;
            this._limitOffset = 0;
            return this;
        }

        /// <summary>
        /// Лимит на выборку.
        /// </summary>
        /// <param name="count">Минимальное количество записей</param>
        /// <param name="offset">Максимальное количество записей</param>
        /// <returns>Объект для создания строки запроса</returns>
        public Select Limit(int count, int offset)
        {
            this._limitCount = count;
            this._limitOffset = offset;
            return this;
        }

        /// <summary>
        /// Конструктор запроса.
        /// </summary>
        /// <returns>Строка запроса к базе данных</returns>
        private string _constructor()
        {
            string sqlcommand = string.Empty;
            if (_from.Length == 0)
            {
                MessageBox.Show("Не определено имя таблицы", "DbFacade", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            sqlcommand = string.Format("SELECT {0} {1} {2} ", _columns, SQL_FROM, _from);
            //собираем Join
            foreach (JoinObj join in _collectionJoin)
            {
                sqlcommand += string.Format("{0} {1} ON {2} ", join.SQLJoinType, join.Name, join.Conditional);
            }

            //условие
            sqlcommand += _where.Length > 0 ? SQL_WHERE + " " + _where : "";

            //группировка
            if (_group.Length > 0)
            {
                sqlcommand += " " + SQL_GROUP_BY + " " + _group;
            }

            //вычисление табличного выражения
            if (_having.Length > 0)
            {
                sqlcommand += " " + SQL_HAVING + " " + _having;
            }

            //сортировка
            if (_order.Length > 0)
            {
                sqlcommand += " " + SQL_ORDER_BY + " " + _order;
            }

            //лимит
            if (_limitCount > 0)
            {
                sqlcommand += " " + SQL_LIMIT + " " + _limitCount;
            }
            if (_limitOffset > 0)
            {
                sqlcommand += "," + _limitOffset;
            }

            return sqlcommand;
        }
        #endregion

        /// <summary>
        /// Массив колонок в строку
        /// </summary>
        /// <param name="columns">Массив колонок</param>
        /// <returns>Колонки через запятую</returns>
        private string columnsToLine(string[] columns)
        {
            string textofcolumns = string.Empty;
            if (columns == null || columns.Length == 0)
                textofcolumns = "*";
            else
            {
                textofcolumns = string.Join(",", columns);
            }
            return textofcolumns.ToString();
        }
    }
}
