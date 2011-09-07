//***************************************************************************************
// Контрол для отображения данных о разузловании и подробностей о сборке и составе сборок
//***************************************************************************************
using System;
using System.Windows.Forms;
using System.Data;
using Telerik.WinControls.UI;
using System.Drawing;

namespace ProjectNSI
{
    public partial class NodesViewer : UserControl
    {
        bool flag = false; // флаг для определения построено ли дерево
        Tree tr;
        WorkTreeView trv;
        bool IsBuiltTreeView = false;

        #region Конструктор и инициализация
        public NodesViewer()
        {
            InitializeComponent();
            InitializeData();
            LoadTreeData();
            // настроим события клика для контекстного меню таблицы "Состав изделий"
            radContextMenu1.Items[1].Click += new EventHandler(EditCompositionRow_Click);
            radContextMenu1.Items[2].Click += new EventHandler(AddCompositionRow_Click);
            radContextMenu1.Items[4].Click += new EventHandler(RemoveCompositionRow_Click);
            // настроим компоненты формы
            radLabel1.Visible = false;
        }

        private void InitializeData()
        {
            if (DBWorker.flag)
            {
                // загрузим данные из ТБД "Состав изделий"
                GlobalFunctions.SelectAndOutCompositionProductsTable(CompositionProductsGridView);
            }
        }
        #endregion

        #region Загрузка данных и размещение их в компонентах
        // загрузка данных для отображения разузлования
        public void LoadTreeData()
        {
            if (DBWorker.flag)
            {
                // загрузим данные из ТБД "Состав изделий"
                DataTable dt = DBWorker.SelectDataFromTable("СИ");
                if (!DBWorker.IsItFATable)
                {
                    // создадим таблицу БД "Полная применяемость"
                    DBWorker.CreateTableFullApplication();
                }
                // построим дерево (структуру Tree)
                BuiltTreeData(dt);
                // построим и отобразим дерево в TreeView
                BuiltTreeView();
                // подсчитаем полную применяемость изделий
                CalculateFullApplication();
            }
            else
                MessageBox.Show("Не открыто соединение с БД! Подключитесь к БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void BuiltTreeData(DataTable dt)
        {
            if (!flag) // если дерево не построено, строим структуру его
            {
                // построим дерево классом для деревьев
                CompositionRow[] data = Converter.ConvertDataTableToCompositionRow(dt);
                tr = new Tree(data);
                if (tr.IsBuild) flag = true;
            }
        }

        private void BuiltTreeView()
        {
            if (!IsBuiltTreeView)
            {
                // построение дерева TreeView для отображения из Tree
                trv = new WorkTreeView(tr);
                fullTreeStructure.Nodes.Clear();
                fullTreeStructure.ShowItemToolTips = true;
                fullTreeStructure.Nodes.Add(trv.Root);
                IsBuiltTreeView = true;
            }
            else
            {
                fullTreeStructure.Nodes.Clear();
                fullTreeStructure.ShowItemToolTips = true;
                fullTreeStructure.Nodes.Add(trv.Root);
            }
        }

        private void CalculateFullApplication()
        {
            if (flag) // если дерево построено
            {
                if (!tr.IsCalculated) // если еще не рассчитана полная применяемость
                {
                    // подсчитаем кол-во деталей в изделиях
                    FullApplicationRow[] calc = tr.CalculateFullApplication();
                    // заполним таблицу БД ПП
                    DBWorker.FillFullApplicationTable(calc);
                }
                // сделаем выборку данных из таблицы и выведем ее
                GlobalFunctions.SelectAndOutFullApplicationTable(FullApplicationGridView);
            }
        }
        #endregion

        #region Обработка событий кнопок
        // генерация ведомости ПП
        private void radButton1_Click(object sender, EventArgs e)
        {
            // генерируем HTML файл
            HTMLWorker.GenerateFullApplicationReport(tr);
            // открываем его в окне просмотра отчетов
            ReportViewerForm form = new ReportViewerForm(GlobalVars.AppDir + "\\" + "Reports\\FullApllicationTableReport.html");
            form.Show();
        }

        // кнопка Обновить дерево
        private void radButton2_Click(object sender, EventArgs e)
        {
            RefreshTree();
        }
        #endregion

        #region Обработка событий контекстного меню
        // добавление записи в таблицу
        private void AddCompositionRow_Click(object sender, EventArgs e)
        {
            AddCompositionRowForm form = new AddCompositionRowForm(FormType.ADDFORM);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
                RefreshCompositionProductsTable();
        }

        // редактирование записи (только поле с количеством(!))
        private void EditCompositionRow_Click(object sender, EventArgs e)
        {
            // подготовим данные для передачи
            CompositionRow row = new CompositionRow();
            row.RootCode = Convert.ToInt64(CompositionProductsGridView.SelectedRows[0].Cells[0].Value);
            row.WhereCode = Convert.ToInt64(CompositionProductsGridView.SelectedRows[0].Cells[1].Value);
            row.WhatCode = Convert.ToInt64(CompositionProductsGridView.SelectedRows[0].Cells[2].Value);
            row.Count = Convert.ToInt32(CompositionProductsGridView.SelectedRows[0].Cells[3].Value);
            // вызываем окно редактирования и передаем данные о выбранной строке таблицы
            AddCompositionRowForm form = new AddCompositionRowForm(FormType.EDITFORM, row);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
                RefreshCompositionProductsTable();
        }

        // удаление выбранной записи из таблицы
        private void RemoveCompositionRow_Click(object sender, EventArgs e)
        {
            if (CompositionProductsGridView.SelectedRows.Count != 0)
            {
                DialogResult res = MessageBox.Show("Вы действительно хотите удалить выбранную запись?", "Запрос на удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    // удаление выбранной записи из таблицы
                    string where = String.Format("(RootCode = {0}) and (WhereCode = {1}) and (WhatCode = {2})",
                        Convert.ToInt64(CompositionProductsGridView.SelectedRows[0].Cells[0].Value),
                        Convert.ToInt64(CompositionProductsGridView.SelectedRows[0].Cells[1].Value),
                        Convert.ToInt64(CompositionProductsGridView.SelectedRows[0].Cells[2].Value));
                    bool result = DBWorker.DeleteRow("CompositionProducts", where);
                    if (result)
                    {
                        MessageBox.Show("Выбранная запись удалена успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshCompositionProductsTable(); // обновим данные таблицы
                    }
                }
            }
            else
                MessageBox.Show("Не выбрана строка для удаления данных из таблицы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region Вспомогательные функции
        // обновление таблицы "Состав изделий"
        private void RefreshCompositionProductsTable()
        {
            // загрузка и вывод обновленных данных для таблицы СИ
            GlobalFunctions.SelectAndOutCompositionProductsTable(CompositionProductsGridView);
            // подсветим на форме текст с необходимостью обновления дерева
            radLabel1.Text = "Таблицы Состав изделий изменена! Перестройте дерево!";
            radLabel1.Visible = true;
            radLabel1.ForeColor = Color.Red;
        }

        // обновление дерева (его перестройка)
        private void RefreshTree()
        {
            // если таблица "Полная прмиеняемость" построена, то удалаем ее
            if (DBWorker.IsItFATable)
                DBWorker.DeleteFullApplicationTable();
            // отметим флаг о том, что дерево TreeView не построено (будет перестроено)
            IsBuiltTreeView = false;
            flag = false;
            tr.IsBuild = false;
            // строим новое дерево Tree, TreeView и создаем новую таблицу ПП
            LoadTreeData();
            // уберем надпись о необходимости обновлении дерева
            radLabel1.Visible = false;
        }
        #endregion
    }

    // класс для работы с деревьями TreeView
    public class WorkTreeView
    {
        public RadTreeNode Root;
        DataTable dt = DBWorker.SelectDataFromTable("СНП");

        #region Конструкторы
        public WorkTreeView(Tree TreeData)
        {
            CreateTreeViewFromTree(TreeData);
        }
        #endregion

        #region Построение дерева TreeView из Tree
        public void CreateTreeViewFromTree(Tree tr)
        {
            // создание корня для всех изделий
            Root = new RadTreeNode("Изделия");
            Root.ToolTipText = "Дерево изделий и их состав";
            // теперь создание корней каждого изделия и обход деревьев
            RadTreeNode Node;
            for (int i = 0; i < tr.Root.Length; i++)
            {
                Node = Root.Nodes.Add(GetProductName(tr.Root[i].PRootCode));//tr.Root[i].PRootCode.ToString());
                Node.ToolTipText = GenerateToolTip(tr.Root[i]);
                InsertNodes(Node, tr.Root[i]);
            }
        }
        // Node - куда, TreeNode - что
        private void InsertNodes(RadTreeNode Node, TreeNode TreeNode)
        {
            RadTreeNode newnode;
            for (int i = 0; i < TreeNode.GetNodesCount(); i++)
            {
                newnode = Node.Nodes.Add(GetProductName(TreeNode[i].PProductCodeWhat));//TreeNode[i].PProductCodeWhat.ToString());
                newnode.ToolTipText = GenerateToolTip(TreeNode[i]);
                if (TreeNode[i].GetNodesCount() != 0) // если есть у текущего узла еще узлы
                    InsertNodes(newnode, TreeNode[i]);
            }
        } 
        #endregion

        #region Вспомогательные функции
        /// <summary>
        /// Функция для генерации текста всплывающей подсказки в дереве TreeView
        /// </summary>
        /// <param name="node">Узел, хранящий сведения</param>
        /// <returns>Возвращает строку подсказки</returns>
        public string GenerateToolTip(TreeNode node)
        {
            string str = String.Format("Код изделия: {0}\nКод изделия, СЕ (куда входит): {1}\nКод СЕ, детали (что входит): {2}\nКоличество: {3}",
                node.PRootCode, node.PProductCodeWhere, node.PProductCodeWhat, node.PCount);
            return str;
        }

        /// <summary>
        ////Получение строки с именем и обозначением изделия
        /// </summary>
        /// <param name="code">Код изделия, для которого необходимо получить сведения</param>
        /// <returns>Возвращает строку вида: {Название} {Обозначение} </returns>
        private string GetProductName(long code)
        {
            string str = "";
            for (int i = 0; i < dt.Rows.Count; i++)
                if (code == Convert.ToInt64(dt.Rows[i].ItemArray.GetValue(0)))
                {
                    str = String.Format("{0} {1}", dt.Rows[i].ItemArray.GetValue(1).ToString(), dt.Rows[i].ItemArray.GetValue(2).ToString());
                    break;
                }
            return str;
        } 
        #endregion
    }
}
