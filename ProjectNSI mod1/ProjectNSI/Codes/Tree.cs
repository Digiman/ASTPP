//*****************************************************************************
// Класс для реализации функции разузлования с использованием дерева
//*****************************************************************************
using System;

namespace ProjectNSI
{
    // класс, описывающий методы и структуру узла дерева
    public class TreeNode
    {
        long RootCode;          // код изделия (корневой код, собственно само изделие)
        long ProductCodeWhere;  // код изделия, СЕ, детали (куда входит)
        long ProductCodeWhat;   // код изделия, СЕ, детали (что входит)
        int Count;              // количество единиц, входящих в изделие
        TreeNode[] Nodes;       // узлы "дети"

        #region Конструкторы
        public TreeNode(long root, long where, long what, int count)
        {
            RootCode = root;
            ProductCodeWhere = where;
            ProductCodeWhat = what;
            Count = count;
            Nodes = new TreeNode[0];  // пустой массив дочерних узлов
        }
        #endregion

        #region Методы
        /// <summary>
        /// Добавление дочернего узла к текущему узлу
        /// </summary>
        /// <param name="node">Новый добавляемый узел TreeNode</param>
        /// <returns>Возвращает индекс в массиве дочерних узлов, куда был вставлен новый узел</returns>
        public int AddNode(TreeNode node)
        {
            Array.Resize(ref Nodes, Nodes.Length + 1);
            Nodes[Nodes.Length - 1] = node;
            return Nodes.Length - 1;
        }

        public int GetNodesCount()
        {
            return Nodes.Length;
        }
        #endregion

        #region Свойства
        public long PRootCode
        {
            get
            {
                return RootCode;
            }
            set
            {
                RootCode = value;
            }
        }

        public int PCount
        {
            get
            {
                return Count;
            }
            set
            {
                Count = value;
            }
        }

        public long PProductCodeWhat
        {
            get
            {
                return ProductCodeWhat;
            }
            set
            {
                ProductCodeWhat = value;
            }
        }

        public long PProductCodeWhere
        {
            get
            {
                return ProductCodeWhere;
            }
            set
            {
                ProductCodeWhere = value;
            }
        }
        #endregion

        #region Индексаторы
        /// <summary>
        /// Индексатор для доступа к массиву дочерних узлов (только чтение)
        /// </summary>
        /// <param name="ind">Индекс в массиве дочерних узорв</param>
        /// <returns>Возвращает узел TreeNode</returns>
        public TreeNode this[int ind]
        {
            get
            {
                return Nodes[ind];
            }
        }
        #endregion
    }

    // класс для реализации построение дерева разузлования
    public class Tree
    {
        public TreeNode[] Root = new TreeNode[0]; // корни деревьев
        public bool IsBuild = false;              // определяет построены ли деревья 
        public bool IsCalculated = false;         // определяет расчитана ли применяемость изделий (ТБД "ПП")

        #region Конструкторы
        public Tree(CompositionRow[] RowsData)
        {
            /*// фомируем корень дерева
            Root = new TreeNode(RowsData[0].RootCode, 0, 0, 0);
            // удаляем строку вставленную в дерево в корень из массива
            //CompositionRow[] NewRowsData = GlobalFunctions.TrimArray(RowsData, 0);
            // запускаем цикличный (рекурсивный) алгоритм построения дерева
            BuiltTree(Root, RowsData, RowsData[0].RootCode);
            // отметим флаг построения дерева
            IsBuild = true;*/
            
            // 1. Ищем все корни для деревьев и создаем их
            // определяем количество корней
            int count = 0;
            long[] RootCodes = GetRootsCount(ref count, RowsData);
            // создаем массив корней
            int i;
            for (i = 0; i < RootCodes.Length; i++)
            {
                AddRoot(new TreeNode(RootCodes[i], 0, 0, 0));
            }
            // 2. Для каждого изделия (корня) строим дерево
            for (i = 0; i < Root.Length; i++)
            {
                // запускаем цикличный (рекурсивный) алгоритм построения дерева
                BuiltTree(Root[i], RowsData, Root[i].PRootCode);
            }
            // 3. Отметим флаг построения дерева
            IsBuild = true;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Рекурсивный метод построения дерева
        /// </summary>
        /// <param name="root">Корень для текущей итерации</param>
        /// <param name="RowsData">Строки таблицы СИ для построения дерева</param>
        /// <param name="parent">Код родительского узла</param>
        public void BuiltTree(TreeNode root, CompositionRow[] RowsData, long parent)
        {
            for (int i = 0; i < RowsData.Length; i++)
                if (RowsData[i].WhereCode == parent)
                {
                    TreeNode node = new TreeNode(RowsData[i].RootCode, RowsData[i].WhereCode, RowsData[i].WhatCode, RowsData[i].Count);
                    int ind = root.AddNode(node);
                    BuiltTree(root[ind], RowsData, root[ind].PProductCodeWhat);
                }
        }

        /// <summary>
        /// Подсчет количества деталей в изделии
        /// </summary>
        /// <returns>Возвращает сассив с данными об изделиях и их составе</returns>
        public FullApplicationRow[] CalculateFullApplication()
        {
            FullApplicationRow[] Data = new FullApplicationRow[0];
            int i;
            for (i = 0; i < Root.Length; i++) // просматриваем по деревьям
            {
                GoIntoTree(Root[i], ref Data, 1); // подсчитаем для одного изделия
            }
            IsCalculated = true;
            return Data;
        }

        /// <summary>
        /// Рекурсивный метод обхода дерева для подсчета состава изделия
        /// </summary>
        /// <param name="root">Корневой узел для обхода</param>
        /// <param name="Data">Массив сведений о составе изделия</param>
        /// <param name="count">Счетчик единиц (накапливаемый)</param>
        private void GoIntoTree(TreeNode root, ref FullApplicationRow[] Data, int count)
        {
            // просматриваем дочерние узлы текушего узла root
            for (int i = 0; i < root.GetNodesCount(); i++)
            {
                // проверям, деталь ли это
                if (root[i].GetNodesCount() == 0)
                {
                    InsertNodeDataToFAData(root[i], ref Data, count);
                }
                else // если не деталь смотрим для текущего узла
                {
                    GoIntoTree(root[i], ref Data, root[i].PCount * count);
                }
            }
        }

        /// <summary>
        /// Вставка данных о детали в массив данных для таблицы ПП
        /// </summary>
        /// <param name="node">Вставляемый узел (данные для вставки)</param>
        /// <param name="Data">Собственно сам массив с данными</param>
        /// <param name="count">Счетчик единиц (накапливаемый)</param>
        private void InsertNodeDataToFAData(TreeNode node, ref FullApplicationRow[] Data, int count)
        {
            // проверим, есть ли уже в массиве запись для данной детали
            if (!IsItDetail(node, Data))
            {
                Array.Resize(ref Data, Data.Length + 1);
                Data[Data.Length - 1].ProductCode = node.PRootCode;
                Data[Data.Length - 1].PackageDetails = node.PProductCodeWhat;
                Data[Data.Length - 1].Count = node.PCount * count;
            }
            else // если есть уже запись для данной детали
            {
                for (int i = 0; i < Data.Length; i++)
                    if (node.PRootCode == Data[i].ProductCode && node.PProductCodeWhat == Data[i].PackageDetails)
                    {
                        Data[i].Count += node.PCount * count; // увеличим количество существующих деталей
                        break;
                    }
            }
        }

        /// <summary>
        /// Проверка на существование в массиве сведений о текущей детали
        /// </summary>
        /// <param name="node">Текущий узел (описывает данные текущей детали)</param>
        /// <param name="Data">Массив со сведениями о деталях</param>
        /// <returns>Возвращает True, если деталь уже имеет описание в массиве, в противном случае False</returns>
        private bool IsItDetail(TreeNode node, FullApplicationRow[] Data)
        {
            bool flag = false;

            for (int i = 0; i < Data.Length; i++)
            {
                if (node.PRootCode == Data[i].ProductCode && node.PProductCodeWhat == Data[i].PackageDetails)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }
        #endregion

        #region Работа с корнями
        /// <summary>
        /// Добавление корня в массив корней изделий
        /// </summary>
        /// <param name="node"></param>
        public void AddRoot(TreeNode node)
        {
            Array.Resize(ref Root, Root.Length + 1);
            Root[Root.Length - 1] = node;
        }

        /// <summary>
        /// Поиск количества корневых узлов по входным данным таблице ПП
        /// </summary>
        /// <param name="count">Возвращает количество найденных уникальных корней</param>
        /// <param name="RowsData">Исходная таблица с данными</param>
        /// <returns>Возвращает коды найденных корней деревьев</returns>
        public long[] GetRootsCount(ref int count, CompositionRow[] RowsData)
        {
            long[] RootCodes = new long[0];
            int i;
            // добавим первый узел, для начала
            Array.Resize(ref RootCodes, 1);
            RootCodes[0] = RowsData[0].RootCode;
            count++;
            for (i = 1; i < RowsData.Length; i++)
            {
                if (!IsIt(RootCodes, RowsData[i].RootCode)) // если нет узла с корневым кодом уже, то добавим его
                {
                    Array.Resize(ref RootCodes, RootCodes.Length + 1);
                    RootCodes[RootCodes.Length - 1] = RowsData[i].RootCode;
                    count++;
                }
            }
            return RootCodes;
        }

        /// <summary>
        /// Проверка есть ли в массиве кодов текущий код
        /// </summary>
        /// <param name="Codes">Массив с кодами корней</param>
        /// <param name="p">Текущий код</param>
        /// <returns>Возвращает True, если код есть в массиве, иначе False</returns>
        private bool IsIt(long[] Codes, long p)
        {
            bool flag = false;

            for (int i = 0; i < Codes.Length; i++)
            {
                if (Codes[i] == p)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }
        #endregion
    }
}
