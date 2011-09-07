//*****************************************************************************
// Контрол для реализации раскроя листов металла на заготовки
//*****************************************************************************
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProjectNSI
{
    /// <summary>
    /// Контрол для реализации методов раскроя и отображения его вида на экране
    /// </summary>
    public partial class Cutting : UserControl
    {
        InputCuttingParams InPars = new InputCuttingParams();    // исходные (входные) параметры раскроя
        OutputCuttingParams OutPars = new OutputCuttingParams(); // выходные параметры раскроя
        
        #region Конструктор и инициализация
        public Cutting()
        {
            InitializeComponent();
        }
        #endregion

        #region Обработчики событий кнопок и компонентов
        // кнопка Рассчитать без поворота листа
        private void radButton1_Click(object sender, EventArgs e)
        {
            // проверка полей с исходными данными на пустоту
            if (radTextBox1.Text != "" || radTextBox2.Text != "" || radTextBox3.Text != "" || radTextBox4.Text != "")
            {
                InputCuttingParams inpars = new InputCuttingParams();
                GetInputParams(ref inpars);
                // вычисление параметров раскроя
                CalcCutting calc = new CalcCutting(inpars, pictureBox1);
                OutputCuttingParams outpars = calc.Calculate();
                // вывод сведений о расчетах
                //MessageBox.Show(CreateStringWithOutCuttingParams(outpars), "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OutputData(inpars, outpars);
                InPars = inpars;
                OutPars = outpars;
                // рисование
                Draw(inpars, outpars);
            }
            else
                MessageBox.Show("Не заданы параметры расчета!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Изменение размера картинки (перерисовка)
        /// </summary>
        private void pictureBox1_ClientSizeChanged(object sender, EventArgs e)
        {
            try
            {
                Draw(InPars, OutPars);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return;
            }
        }
        #endregion

        #region Вспомогательные функции
        /// <summary>
        /// Считывание исходных данных и заполение структуры InputCuttingParams
        /// </summary>
        /// <param name="inpars">Структура для заполения</param>
        private void GetInputParams(ref InputCuttingParams inpars)
        {
            // считаем введенные параметры
            inpars.A = Convert.ToInt32(radTextBox1.Text);
            inpars.B = Convert.ToInt32(radTextBox2.Text);
            inpars.da = Convert.ToInt32(radTextBox3.Text);
            inpars.db = Convert.ToInt32(radTextBox4.Text);
            // вычислим площади листа и заготвки
            inpars.ListArray = inpars.A * inpars.B;
            inpars.SmallArray = inpars.da * inpars.db;
        }

        /// <summary>
        /// Построение строки с параметрами выхода
        /// </summary>
        /// <param name="pr">Выходные параметры</param>
        /// <returns>Возвращает строку с пояснениями параметров и их значениями</returns>
        private string CreateStringWithOutCuttingParams(OutputCuttingParams pr)
        {
            string str = "";

            str += " m1: ";
            for (int i = 0; i < pr.m1.Length; i++)
                str += pr.m1[i].ToString() + " ";
            str += "\n m2: ";
            for (int i = 0; i < pr.m2.Length; i++)
                str += pr.m2[i].ToString() + " ";
            str += "\n h1: ";
            for (int i = 0; i < pr.h1.Length; i++)
                str += pr.h1[i].ToString() + " ";
            str += "\n h2: ";
            for (int i = 0; i < pr.h2.Length; i++)
                str += pr.h2[i].ToString() + " ";
            str += String.Format("\n Count: {0}\n EfArray: {1}\n RestArray: {2}", pr.Count, pr.EfArray, pr.RestArray);
            
            return str;
        }

        /// <summary>
        /// Вывод данных в компоненты формы
        /// </summary>
        /// <param name="inpars">Входные параметры</param>
        /// <param name="outpars">Расчетные выходные параметры</param>
        private void OutputData(InputCuttingParams inpars, OutputCuttingParams outpars)
        {
            radTextBox5.Text = outpars.Count.ToString();
            radTextBox6.Text = outpars.EfArray.ToString();
            radTextBox7.Text = outpars.RestArray.ToString();
            radTextBox8.Text = inpars.ListArray.ToString();
            radTextBox9.Text = inpars.SmallArray.ToString();
        }

        /// <summary>
        /// Рисование листа, разрезанного на заготовки
        /// </summary>
        /// <param name="inpars">Входные параметры раскроя</param>
        /// <param name="outpars">Выходные параметры раскроя</param>
        private void Draw(InputCuttingParams inpars, OutputCuttingParams outpars)
        {
            if (outpars.p[0] == 1)
                DrawCutting.DrawVariantOne(pictureBox1, inpars, outpars);
            if (outpars.p[0]==2)
                DrawCutting.DrawVariantTwo(pictureBox1, inpars, outpars);
        }
        #endregion
    }

    #region Структуры для работы с раскроем
    /// <summary>
    /// Входные параметры для раскроя
    /// </summary>
    public struct InputCuttingParams
    {
        public int A;   // длина исходного листа
        public int B;   // ширина исходного листа
        public int da;  // длина заготовки
        public int db;  // ширина заготовки

        public int ListArray;  // площадь исходного листа
        public int SmallArray; // площадь заготовки
    }

    /// <summary>
    /// Выходные параметры раскроя
    /// </summary>
    public struct OutputCuttingParams
    {
        public int[] m1;
        public int[] h1;
        public int[] m2;
        public int[] h2;
        public int[] p;        // параметры, говорящие о типах раскрой на каждом этапе (для m1 n1)
        public int Count;      // кол-во элементов
        public int RestArray;  // площадь остатков
        public int EfArray;    // полезная площадь
    }
    #endregion

    #region Класс для выполнения раскроя без поворота листа
    /// <summary>
    /// Класс для расчета раскроя
    /// </summary>
    public class CalcCutting
    {
        int L, H; // длина и ширина исходного листа
        int dl, dh; // длина и ширина заготовки
        
        #region Конструкторы
        /// <summary>
        /// Инициализация параметров расчета раскроя
        /// </summary>
        /// <param name="p1">Длина листа</param>
        /// <param name="p2">Ширина листа</param>
        /// <param name="p3">Длина заготовки</param>
        /// <param name="p4">Ширина заготовки</param>
        public CalcCutting(int p1, int p2, int p3, int p4)
        {
            L = p1;
            H = p2;
            dl = p3;
            dh = p4;
        }

        /// <summary>
        /// Инициализация параметров расчета раскроя
        /// </summary>
        /// <param name="pars">Входные параметры раскроя</param>
        /// <param name="pb">Контрол, в котром производится рисование</param>
        public CalcCutting(InputCuttingParams pars, PictureBox pb)
        {
            L = pars.A;
            H = pars.B;
            dl = pars.da;
            dh = pars.db;
        }
        #endregion

        #region Основные функции
        /// <summary>
        /// Вычисление параметров раскроя
        /// </summary>
        /// <returns>Возвращает структуру с данными о результатах раскроя</returns>
        public OutputCuttingParams Calculate()
        {
            OutputCuttingParams pars = new OutputCuttingParams();
            // выделяем память сразу под параметры раскроя
            InitOutParams(ref pars);

            bool flag = true;
            int sum = 0; // кол-во элементов
            int m1 = 0, m2 = 0, h1 = 0, h2 = 0; // параметры раскроя

            while (flag)
            {
                CalcParams(L, H, ref m1, ref m2, ref h1, ref h2); // вычисление пареметров m1, m2, h1, h2
                if (h1 + h2 < dl)
                {
                    int L11 = L - h1;
                    int H11 = H;
                    int L12 = h1;
                    int H12 = H;
                    int ost1, ost2; // остатки
                    sum += CropArea(L11, H11, 2, m1, m2, dl, out ost1); // режем большой кусок вдоль L
                    sum += CropArea(L12, H12, 1, m1, m2, dl, out ost2); // режем малый кусок вдоль H
                    flag = false; // завершаем резку (все разрезано)
                    GetOutCutingParams(ref pars, m1, m2, h1, h2, 1);
                    pars.RestArray = ost1 + ost2; // остаток (площадь)
                }
                else
                {
                    sum += CropArea2(m1, m2, h1, h2);
                    GetOutCutingParams(ref pars, m1, m2, h1, h2, 2);
                }
            }

            pars.Count = sum;        // количество полученных заготовок (прямоугольных)
            pars.EfArray = sum * dl; // площадь заготовок

            return pars;
        }

        /// <summary>
        /// Инициализация начальных значений выходных параметров
        /// </summary>
        /// <param name="pars">Входные значения</param>
        private void InitOutParams(ref OutputCuttingParams pars)
        {
            pars.m1 = new int[0];
            pars.m2 = new int[0];
            pars.h1 = new int[0];
            pars.h2 = new int[0];
            pars.p = new int[0];
        }

        private void GetOutCutingParams(ref OutputCuttingParams pars, int m1, int m2, int h1, int h2, int p)
        {
            Array.Resize(ref pars.m1, pars.m1.Length + 1);
            pars.m1[pars.m1.Length - 1] = m1;
            Array.Resize(ref pars.m2, pars.m2.Length + 1);
            pars.m2[pars.m2.Length - 1] = m2;
            Array.Resize(ref pars.h1, pars.h1.Length + 1);
            pars.h1[pars.h1.Length - 1] = h1;
            Array.Resize(ref pars.h2, pars.h2.Length + 1);
            pars.h2[pars.h2.Length - 1] = h2;
            // добавим описание параметра о типе раскроя для рисования потом
            Array.Resize(ref pars.p, pars.p.Length + 1);
            pars.p[pars.p.Length - 1] = p;
        }

        /// <summary>
        /// Вычисление параметров раскроя (основных)
        /// </summary>
        /// <param name="l">Длина листа</param>
        /// <param name="h">Ширина листа</param>
        private void CalcParams(int l, int h, ref int m1, ref int m2, ref int h1, ref int h2)
        {
            m1 = l / dl;
            m2 = h / dl;
            h1 = l - m1 * dl;
            h2 = h - m2 * dl;
        }

        /// <summary>
        /// Раскрой прямоугольного листа на заготовки (для варианта №1)
        /// </summary>
        /// <param name="l">Длина листа для резки</param>
        /// <param name="h">Ширина листа для резки</param>
        /// <param name="p">Параметр, указывающий вдоль какого края резать</param>
        /// <returns>Возвращает количество элементов от раскроя</returns>
        private int CropArea(int l, int h, int p, int m1, int m2, int dl, out int ost)
        {
            int count = 0; // количество заготовок
            ost = 0; // остоток
            if (p == 1) // режем вдоль H
            {
                count = m2 * l;
                ost = (l * h) - count * dl;
            }
            if (p == 2) // режем вдоль L
            {
                count = m1 * h;
                ost = 0;
            }
            return count;
        }

        /// <summary>
        /// Резка исходного листа по краям (для варианта раскроя №2)
        /// </summary>
        /// <returns>Возвращает количество полученных элементов в отрезанных кусках</returns>
        private int CropArea2(int m1, int m2, int h1, int h2)
        {
            // режем по краям и вычисляем размеры обрезков
            int c1 = h2 * m1;
            int c2 = h1 * m2;
            int count = 2 * c1 + 2 * c2; // количество элементов в обрезанных кусках
            // пересчитываем новую область листа, который остался от обрезания краев
            L = L - 2 * h1;
            H = H - 2 * h2;
            return count;
        }
        #endregion
    }
    #endregion

    #region Класс для рисования раскроя
    /// <summary>
    /// Класс, реализующий методы рисования раскроя по выходным параметрам расчетов
    /// </summary>
    public class DrawCutting
    {
        static Ratio rat; // коэффициент масштабирования

        /// <summary>
        /// Рисование раскроя для варианта 1
        /// </summary>
        /// <param name="pic">Изображение для вывода в окно</param>
        /// <param name="In">Входные параметры раскроя</param>
        /// <param name="Out">Выходные параметры раскроя</param>
        public static void DrawVariantOne(PictureBox pic, InputCuttingParams In, OutputCuttingParams Out)
        {
            // определение размеров области для рисования (в пикселах)
            int w = pic.Width;
            int h = pic.Height;

            Bitmap bmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(bmp);
            // вычисление коэффициента растяжения (ratio)
            rat = new Ratio();
            rat.X = w / In.A;
            rat.Y = h / In.B;

            Pen p = new Pen(Color.Black, 1); // перо для рисования

            // рисуем первую часть листа
            int i;
            float LimX = In.da * Out.m1[0] * rat.X; // граница части листа по X
            // рисуем горизонтальные линии
            for (i = 0; i <= In.B; i++)
                g.DrawLine(p, 0, i * In.db * rat.Y, LimX, i * In.db * rat.Y);
            // рисуем вертикальные линии
            float LimY = In.B * In.db * rat.Y;
            for (i = 0; i <= Out.m1[0]; i++)
                g.DrawLine(p, i * In.da * rat.X, 0, i * In.da * rat.X, LimY);

            // рисуем вторую часть листа
            float LimX2 = LimX + In.db * Out.h1[0] * rat.X;
            for (i = 0; i <= Out.m2[0]; i++)
                g.DrawLine(p, LimX, i * In.da * rat.Y, LimX2, i * In.da * rat.Y);
            float LimY2 = In.da * Out.m2[0] * rat.Y;
            for (i = 0; i <= Out.h1[0]; i++)
                g.DrawLine(p, LimX + i * In.db * rat.X, 0, LimX + i * In.db * rat.X, LimY2);
            
            // рисуем область остатка
            g.FillRectangle(Brushes.Gray, LimX, LimY2, LimX2 - LimX + 1, LimY - LimY2 + 1);

            // сохранение и вывод изображения
            pic.Image = bmp;
            bmp.Save("VariantOne.png", ImageFormat.Png);
        }

        /// <summary>
        /// Рисование раскроя по варианту 2 (с включением варианта 1)
        /// </summary>
        /// <param name="pic">Контрол для вывода изображения</param>
        /// <param name="In">Входные параметры раскроя</param>
        /// <param name="Out">Выходные параметры раскроя</param>
        public static void DrawVariantTwo(PictureBox pic, InputCuttingParams In, OutputCuttingParams Out)
        {
            // определение размеров области для рисования (в пикселах)
            int w = pic.Width;
            int h = pic.Height;

            Bitmap bmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(bmp);
            // вычисление коэффициента растяжения (ratio)
            rat = new Ratio();
            rat.X = w / In.A;
            rat.Y = h / In.B;

            Pen p = new Pen(Color.Black, 1); // перо для рисования

            //------- рисуем обрезки по краям листа -------
            // рисуем 1-ый прямоугольник
            int i;
            float LimX = Out.m1[0] * In.da * rat.X;
            float LimY = Out.h2[0] * In.db * rat.Y;
            for (i = 0; i <= Out.h2[0]; i++) // горизонтальные полосы
                g.DrawLine(p, 0, i * In.db * rat.Y, LimX, i * In.db * rat.Y);
            for (i = 0; i <= Out.m1[0]; i++) // вертикальные полосы
                g.DrawLine(p, i * In.da * rat.X, 0, i * In.da * rat.X, LimY);
            // рисуем 2-ой прямоугольник
            float LimX2 = LimX + In.db * Out.h1[0] * rat.X;
            float LimY2 = Out.m2[0] * In.da * rat.Y;
            for (i = 0; i <= Out.m2[0]; i++) // горизонтальные полосы
                g.DrawLine(p, LimX, i * In.da * rat.Y, LimX2, i * In.da * rat.Y);
            for (i = 0; i <= Out.h1[0]; i++) // вертикальные полосы
                g.DrawLine(p, LimX + i * In.db * rat.X, 0, LimX + i * In.db * rat.X, LimY2);
            // рисуем 3-ий прямоугольник
            float LimX3 = LimX2 - LimX;
            float LimY3 = LimY + LimY2;
            for (i = 0; i <= Out.h2[0]; i++) // горизонтальные полосы
                g.DrawLine(p, LimX2, LimY2 + i * In.db * rat.Y, LimX3, LimY2 + i * In.db * rat.Y);
            for (i = 0; i <= Out.m1[0]; i++) // вертикальные полосы
                g.DrawLine(p, LimX3 + i * In.da * rat.X, LimY2, LimX3 + i * In.da * rat.X, LimY3);
            // рисуем 4-ый прямоугольник
            for (i = 0; i <= Out.m2[0]; i++) // горизонтальные полосы
                g.DrawLine(p, 0, LimY + i * In.da * rat.Y, LimX3, LimY + i * In.da * rat.Y);
            for (i = 0; i <= Out.h1[0]; i++) // вертикальные полосы
                g.DrawLine(p, i * In.db * rat.X, LimY, i * In.db * rat.X, LimY3);
            
            //------ рисуем центральную часть --------
            // рисуем первую часть листа
            float CLimX = LimX3 + In.da * Out.m1[1] * rat.X; // между LimX3 и LimX
            // рисуем горизонтальные линии
            int B = In.B - 2 * Out.h2[0];
            for (i = 0; i <= B; i++)
                g.DrawLine(p, LimX3, LimY + i * In.db * rat.Y, CLimX, LimY + i * In.db * rat.Y);
            // рисуем вертикальные линии
            float CLimY = LimY + B * In.db * rat.Y; // LimY2
            for (i = 0; i <= Out.m1[1]; i++)
                g.DrawLine(p, LimX3 + i * In.da * rat.X, LimY, LimX3 + i * In.da * rat.X, CLimY);

            // рисуем вторую часть листа
            float CLimX2 = CLimX + In.db * Out.h1[1] * rat.X;
            for (i = 0; i <= Out.m2[1]; i++)
                g.DrawLine(p, CLimX, LimY + i * In.da * rat.Y, CLimX2, LimY + i * In.da * rat.Y);
            float CLimY2 = LimY + In.da * Out.m2[1] * rat.Y;
            for (i = 0; i <= Out.h1[1]; i++)
                g.DrawLine(p, CLimX + i * In.db * rat.X, LimY, CLimX + i * In.db * rat.X, CLimY2);

            // рисуем область остатка
            g.FillRectangle(Brushes.Gray, CLimX, CLimY2, CLimX2 - CLimX + 1, CLimY - CLimY2 + 1);

            // сохранение и вывод изображения
            pic.Image = bmp;
            bmp.Save("VariantTwo.png", ImageFormat.Png);
        }
    }

    /// <summary>
    /// Класс для описания масштаба по осям при рисовании
    /// </summary>
    public class Ratio
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
    #endregion
}
