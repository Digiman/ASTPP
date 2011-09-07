namespace ProjectNSI
{
    partial class GetReports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.radRadioButton2 = new Telerik.WinControls.UI.RadRadioButton();
            this.radRadioButton1 = new Telerik.WinControls.UI.RadRadioButton();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.closeButton = new Telerik.WinControls.UI.RadButton();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.radListControl1 = new Telerik.WinControls.UI.RadListControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radRadioButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRadioButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radListControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.Controls.Add(this.radRadioButton2);
            this.radGroupBox1.Controls.Add(this.radRadioButton1);
            this.radGroupBox1.Controls.Add(this.radDropDownList1);
            this.radGroupBox1.Controls.Add(this.radLabel1);
            this.radGroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox1.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "Выбор месяца и года для выдачи ведомости";
            this.radGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(463, 81);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.Text = "Выбор месяца и года для выдачи ведомости";
            // 
            // radRadioButton2
            // 
            this.radRadioButton2.Location = new System.Drawing.Point(322, 51);
            this.radRadioButton2.Name = "radRadioButton2";
            this.radRadioButton2.Size = new System.Drawing.Size(98, 18);
            this.radRadioButton2.TabIndex = 5;
            this.radRadioButton2.Text = "Все месяцы";
            this.toolTip1.SetToolTip(this.radRadioButton2, "Для генерация множества ведомостей по всем доступным месяцам");
            this.radRadioButton2.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radRadioButton2_ToggleStateChanged);
            // 
            // radRadioButton1
            // 
            this.radRadioButton1.Location = new System.Drawing.Point(322, 24);
            this.radRadioButton1.Name = "radRadioButton1";
            this.radRadioButton1.Size = new System.Drawing.Size(128, 18);
            this.radRadioButton1.TabIndex = 4;
            this.radRadioButton1.Text = "Выбранный месяц";
            this.toolTip1.SetToolTip(this.radRadioButton1, "Для выдачи ведомости для выбранного из списка месяца");
            this.radRadioButton1.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radRadioButton1_ToggleStateChanged);
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.Location = new System.Drawing.Point(13, 48);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.ShowImageInEditorArea = true;
            this.radDropDownList1.Size = new System.Drawing.Size(214, 21);
            this.radDropDownList1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.radDropDownList1, "Выбор месяца, для выдачи ведомости (список содержит данные из БД)");
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(13, 23);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(76, 19);
            this.radLabel1.TabIndex = 2;
            this.radLabel1.Text = "Месяц и год";
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(177, 99);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(130, 24);
            this.radButton1.TabIndex = 1;
            this.radButton1.Text = "Сгенерировать";
            this.toolTip1.SetToolTip(this.radButton1, "Генерация ведомости (-ей)");
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(385, 99);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(90, 24);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Закрыть";
            this.toolTip1.SetToolTip(this.closeButton, "Закрытие окна");
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.Controls.Add(this.radListControl1);
            this.radGroupBox2.FooterImageIndex = -1;
            this.radGroupBox2.FooterImageKey = "";
            this.radGroupBox2.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox2.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox2.HeaderImageIndex = -1;
            this.radGroupBox2.HeaderImageKey = "";
            this.radGroupBox2.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox2.HeaderText = "Полученные ведомости (список файлов)";
            this.radGroupBox2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radGroupBox2.Location = new System.Drawing.Point(12, 129);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox2.Size = new System.Drawing.Size(463, 151);
            this.radGroupBox2.TabIndex = 3;
            this.radGroupBox2.Text = "Полученные ведомости (список файлов)";
            // 
            // radListControl1
            // 
            this.radListControl1.CaseSensitiveSort = true;
            this.radListControl1.Location = new System.Drawing.Point(13, 23);
            this.radListControl1.Name = "radListControl1";
            this.radListControl1.Size = new System.Drawing.Size(437, 115);
            this.radListControl1.TabIndex = 2;
            this.radListControl1.Text = "radListControl1";
            this.toolTip1.SetToolTip(this.radListControl1, "Список сгенерированных ведомостей (по двойному клику просмотр ведомости)");
            this.radListControl1.DoubleClick += new System.EventHandler(this.radListControl1_DoubleClick);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Информация";
            // 
            // GetReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 290);
            this.Controls.Add(this.radGroupBox2);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.radGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GetReports";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор месяца для выдачи ведомости";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radRadioButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRadioButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radListControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton closeButton;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadRadioButton radRadioButton1;
        private Telerik.WinControls.UI.RadRadioButton radRadioButton2;
        private Telerik.WinControls.UI.RadListControl radListControl1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
