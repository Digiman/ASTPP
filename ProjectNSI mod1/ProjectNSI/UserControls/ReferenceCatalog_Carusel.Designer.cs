namespace ProjectNSI
{
    partial class ReferenceCatalog_Carusel
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Telerik.WinControls.UI.CarouselEllipsePath carouselEllipsePath2 = new Telerik.WinControls.UI.CarouselEllipsePath();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReferenceCatalog_Carusel));
            this.radCarousel1 = new Telerik.WinControls.UI.RadCarousel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radCarousel1)).BeginInit();
            this.SuspendLayout();
            // 
            // radCarousel1
            // 
            carouselEllipsePath2.Center = new Telerik.WinControls.UI.Point3D(50.574712643678161, 49.53789279112754, 0);
            carouselEllipsePath2.FinalAngle = -100;
            carouselEllipsePath2.InitialAngle = -90;
            carouselEllipsePath2.U = new Telerik.WinControls.UI.Point3D(-10.9717868338558, -20.517560073937155, -50);
            carouselEllipsePath2.V = new Telerik.WinControls.UI.Point3D(38.975966562173461, -18.484288354898336, -60);
            carouselEllipsePath2.ZScale = 500;
            this.radCarousel1.CarouselPath = carouselEllipsePath2;
            this.radCarousel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radCarousel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radCarousel1.EasingType = Telerik.WinControls.RadEasingType.Linear;
            this.radCarousel1.Location = new System.Drawing.Point(0, 0);
            this.radCarousel1.Name = "radCarousel1";
            // 
            // 
            // 
            this.radCarousel1.RootElement.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            this.radCarousel1.SelectedIndex = 0;
            this.radCarousel1.Size = new System.Drawing.Size(957, 541);
            this.radCarousel1.TabIndex = 0;
            ((Telerik.WinControls.UI.RadRepeatButtonElement)(this.radCarousel1.GetChildAt(0).GetChildAt(3))).Interval = 100;
            ((Telerik.WinControls.UI.RadRepeatButtonElement)(this.radCarousel1.GetChildAt(0).GetChildAt(3))).Image = global::ProjectNSI.Properties.Resources.LeftArrow;
            ((Telerik.WinControls.UI.RadRepeatButtonElement)(this.radCarousel1.GetChildAt(0).GetChildAt(3))).ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.UI.RadRepeatButtonElement)(this.radCarousel1.GetChildAt(0).GetChildAt(4))).Interval = 100;
            ((Telerik.WinControls.UI.RadRepeatButtonElement)(this.radCarousel1.GetChildAt(0).GetChildAt(4))).Image = global::ProjectNSI.Properties.Resources.RightArrow;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Table_СВП.png");
            this.imageList1.Images.SetKeyName(1, "Table_СТП.png");
            this.imageList1.Images.SetKeyName(2, "Table_СПП.png");
            this.imageList1.Images.SetKeyName(3, "Table_СЕИ.png");
            this.imageList1.Images.SetKeyName(4, "Table_СНП.png");
            this.imageList1.Images.SetKeyName(5, "Table_СТМ.png");
            this.imageList1.Images.SetKeyName(6, "Table_СНиОД.png");
            // 
            // ReferenceCatalog_Carusel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radCarousel1);
            this.Name = "ReferenceCatalog_Carusel";
            this.Size = new System.Drawing.Size(957, 541);
            this.Load += new System.EventHandler(this.ReferenceCatalog_Carusel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radCarousel1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadCarousel radCarousel1;
        private System.Windows.Forms.ImageList imageList1;
    }
}
