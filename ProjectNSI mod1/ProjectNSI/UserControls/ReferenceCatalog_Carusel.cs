//*****************************************************************************
// Модуль для визуального отображения всех справочников на карусели
//*****************************************************************************
using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace ProjectNSI
{
    public partial class ReferenceCatalog_Carusel : UserControl
    {
        Color PrevSelItemForeColor;
        int PrevSelItem;

        public ReferenceCatalog_Carusel()
        {
            InitializeComponent();
            this.radCarousel1.KeyUp += new KeyEventHandler(radCarousel1_KeyUp);
        }
        // создание элементов карусели
        private void CreateCarouselItems()
        {
            int count = 0;

            foreach (Image image in this.radCarousel1.ImageList.Images)
            {
                RadButtonElement carouselItem = new RadButtonElement();
                carouselItem.ImageAlignment = ContentAlignment.MiddleCenter;
                carouselItem.TextAlignment = ContentAlignment.BottomCenter;
                carouselItem.DisplayStyle = DisplayStyle.ImageAndText;
                carouselItem.TextImageRelation = TextImageRelation.ImageAboveText;
                carouselItem.ShowBorder = false;
                carouselItem.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                carouselItem.Image = new Bitmap(image, image.Size.Width / 2, image.Size.Height / 2);
                carouselItem.ToolTipText = GlobalData.CarouselImageNames[count];
                carouselItem.Text = GlobalData.CarouselImageNames[count];
                carouselItem.Tag = GlobalData.CarouselItemsTags[count];
                carouselItem.Click += new EventHandler(radCarousel1_CarouselElement_Click);
                this.radCarousel1.Items.Add(carouselItem);
                count++;
            }
        }

        // загрузка контрола и его настройка
        private void ReferenceCatalog_Carusel_Load(object sender, EventArgs e)
        {
            // создание элиптической орбыты для элементов карусели
            CarouselEllipsePath ellipsePath = new CarouselEllipsePath();
            ellipsePath.Center = new Telerik.WinControls.UI.Point3D(50, 47, 0);
            ellipsePath.FinalAngle = 270;
            ellipsePath.InitialAngle = 270;
            ellipsePath.U = new Telerik.WinControls.UI.Point3D(31, -21, 0);
            ellipsePath.V = new Telerik.WinControls.UI.Point3D(0, 22, 200);
            ellipsePath.ZScale = 300;
            this.radCarousel1.CarouselPath = ellipsePath;
            this.radCarousel1.SelectedIndexChanged += new EventHandler(CarouselElement_SelectedIndexChanged);
            // создание элементов карусели (кнопки)
            this.radCarousel1.ImageList = this.imageList1;
            CreateCarouselItems();
            this.radCarousel1.NavigationButtonsOffset = new Size(10, 10);
        }

        // изменение цвета для выбранного элемента карусели
        void CarouselElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrevSelItemForeColor = radCarousel1.Items[radCarousel1.SelectedIndex].ForeColor;
            radCarousel1.Items[PrevSelItem].ForeColor = PrevSelItemForeColor;
            radCarousel1.Items[radCarousel1.SelectedIndex].ForeColor = Color.Red;
            PrevSelItem = radCarousel1.SelectedIndex;
        }

        // навигация с помощью клавиатуры для карусели
        void radCarousel1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                this.radCarousel1.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.Right)
            {
                this.radCarousel1.SelectedIndex++;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (this.radCarousel1.SelectedItem != null)
                {
                    ((RadButtonElement)this.radCarousel1.SelectedItem).PerformClick();
                }
            }
        }

        // обработка клика по выбранному элементу на карусели (вызов контрола с таблицей)
        void radCarousel1_CarouselElement_Click(object sender, EventArgs e)
        {
            Control parent;
            UserControl tmp;

            //MessageBox.Show(radCarousel1.Items[radCarousel1.SelectedIndex].Tag.ToString());
            switch (radCarousel1.Items[radCarousel1.SelectedIndex].Tag.ToString())
            {
                case "СВП":
                    SetNameTableInSS(0);
                    parent = this.Parent;
                    parent.Controls.Clear();
                    tmp = new ReferenceCatalog_Table("СВП");
                    tmp.Dock = DockStyle.Fill;
                    parent.Controls.Add(tmp);
                    break;
                case "СТП":
                    SetNameTableInSS(1);
                    parent = this.Parent;
                    parent.Controls.Clear();
                    tmp = new ReferenceCatalog_Table("СТП");
                    tmp.Dock = DockStyle.Fill;
                    parent.Controls.Add(tmp);
                    break;
                case "СПП":
                    SetNameTableInSS(2);
                    parent = this.Parent;
                    parent.Controls.Clear();
                    tmp = new ReferenceCatalog_Table("СПП");
                    tmp.Dock = DockStyle.Fill;
                    parent.Controls.Add(tmp);
                    break;
                case "СЕИ":
                    SetNameTableInSS(3);
                    parent = this.Parent;
                    parent.Controls.Clear();
                    tmp = new ReferenceCatalog_Table("СЕИ");
                    tmp.Dock = DockStyle.Fill;
                    parent.Controls.Add(tmp);
                    break;
                case "СНП":
                    SetNameTableInSS(4);
                    parent = this.Parent;
                    parent.Controls.Clear();
                    tmp = new ReferenceCatalog_Table("СНП");
                    tmp.Dock = DockStyle.Fill;
                    parent.Controls.Add(tmp);
                    break;
                case "СТМ":
                    SetNameTableInSS(5);
                    parent = this.Parent;
                    parent.Controls.Clear();
                    tmp = new ReferenceCatalog_Table("СТМ");
                    tmp.Dock = DockStyle.Fill;
                    parent.Controls.Add(tmp);
                    break;
                case "СНиОД":
                    SetNameTableInSS(6);
                    parent = this.Parent;
                    parent.Controls.Clear();
                    tmp = new ReferenceCatalog_Table("СНиОД");
                    tmp.Dock = DockStyle.Fill;
                    parent.Controls.Add(tmp);
                    break;
            }
        }

        // функция поиска по предкам главного окна приложения и установка в статусстипе названия таблицы
        private void SetNameTableInSS(int ind)
        {
            Control par1 = this.Parent; // панель в которой расположен текущий UC
            Control par2 = par1.Parent; // форма главного окна
            Control[] cont = par2.Controls.Find("radStatusStrip1", false);
            // выведем текст с названием просматриваемой таблицы
            ((RadStatusStrip)cont[0]).Items[0].Text = GlobalData.CarouselImageNames[ind];
        }
    }
}
