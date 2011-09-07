namespace ProjectNSI
{
    partial class UC_Main
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
            this.radButton13 = new Telerik.WinControls.UI.RadButton();
            this.radButton12 = new Telerik.WinControls.UI.RadButton();
            this.radButton10 = new Telerik.WinControls.UI.RadButton();
            this.radButton9 = new Telerik.WinControls.UI.RadButton();
            this.radButton8 = new Telerik.WinControls.UI.RadButton();
            this.radButton7 = new Telerik.WinControls.UI.RadButton();
            this.radButton6 = new Telerik.WinControls.UI.RadButton();
            this.radButton5 = new Telerik.WinControls.UI.RadButton();
            this.radButton4 = new Telerik.WinControls.UI.RadButton();
            this.radButton3 = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radButton13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // radButton13
            // 
            this.radButton13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radButton13.Image = global::ProjectNSI.Properties.Resources.Cutting;
            this.radButton13.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radButton13.Location = new System.Drawing.Point(494, 325);
            this.radButton13.Name = "radButton13";
            this.radButton13.Size = new System.Drawing.Size(165, 83);
            this.radButton13.TabIndex = 9;
            this.radButton13.Text = "Раскрой";
            this.radButton13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.radButton13, "Расчет параметров раскроя материала на заготовки");
            this.radButton13.Click += new System.EventHandler(this.radButton13_Click);
            // 
            // radButton12
            // 
            this.radButton12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radButton12.Image = global::ProjectNSI.Properties.Resources.Activity;
            this.radButton12.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radButton12.Location = new System.Drawing.Point(300, 325);
            this.radButton12.Name = "radButton12";
            this.radButton12.Size = new System.Drawing.Size(165, 83);
            this.radButton12.TabIndex = 8;
            this.radButton12.Text = "Трудовое нормирование";
            this.radButton12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.radButton12, "Просмотр сведений о трудовом нормировании и генерация ведомостей");
            this.radButton12.Click += new System.EventHandler(this.radButton12_Click);
            // 
            // radButton10
            // 
            this.radButton10.Image = global::ProjectNSI.Properties.Resources.CreateDBandFill;
            this.radButton10.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radButton10.Location = new System.Drawing.Point(62, 176);
            this.radButton10.Name = "radButton10";
            this.radButton10.Size = new System.Drawing.Size(165, 98);
            this.radButton10.TabIndex = 6;
            this.radButton10.Text = "Создать и заполнить исходными данными";
            this.radButton10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.radButton10.TextWrap = true;
            this.toolTip1.SetToolTip(this.radButton10, "Создание файла новой БД и заполнение его необходимой начальной инфомацией");
            this.radButton10.Click += new System.EventHandler(this.radButton10_Click);
            // 
            // radButton9
            // 
            this.radButton9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radButton9.Image = global::ProjectNSI.Properties.Resources.Exit2;
            this.radButton9.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radButton9.Location = new System.Drawing.Point(726, 385);
            this.radButton9.Name = "radButton9";
            this.radButton9.Size = new System.Drawing.Size(165, 83);
            this.radButton9.TabIndex = 3;
            this.radButton9.Text = "Выход";
            this.radButton9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.radButton9, "Выход из программы");
            this.radButton9.Click += new System.EventHandler(this.radButton9_Click);
            // 
            // radButton8
            // 
            this.radButton8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radButton8.Image = global::ProjectNSI.Properties.Resources.Preferences2;
            this.radButton8.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radButton8.Location = new System.Drawing.Point(726, 73);
            this.radButton8.Name = "radButton8";
            this.radButton8.Size = new System.Drawing.Size(165, 83);
            this.radButton8.TabIndex = 5;
            this.radButton8.Text = "Настройки";
            this.radButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.radButton8, "Настройки программы");
            this.radButton8.Click += new System.EventHandler(this.radButton8_Click);
            // 
            // radButton7
            // 
            this.radButton7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radButton7.Image = global::ProjectNSI.Properties.Resources.DisconnectFromDB;
            this.radButton7.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radButton7.Location = new System.Drawing.Point(62, 397);
            this.radButton7.Name = "radButton7";
            this.radButton7.Size = new System.Drawing.Size(165, 83);
            this.radButton7.TabIndex = 4;
            this.radButton7.Text = "Отключение от БД";
            this.radButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.radButton7, "Отключение от текущей рабочей БД");
            this.radButton7.Click += new System.EventHandler(this.radButton7_Click);
            // 
            // radButton6
            // 
            this.radButton6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radButton6.Image = global::ProjectNSI.Properties.Resources.ConnectToDB;
            this.radButton6.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radButton6.Location = new System.Drawing.Point(62, 294);
            this.radButton6.Name = "radButton6";
            this.radButton6.Size = new System.Drawing.Size(165, 83);
            this.radButton6.TabIndex = 3;
            this.radButton6.Text = "Подключение к БД";
            this.radButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.radButton6, "Подключение к БД SQLite, созданной данной программой");
            this.radButton6.Click += new System.EventHandler(this.radButton6_Click);
            // 
            // radButton5
            // 
            this.radButton5.Image = global::ProjectNSI.Properties.Resources.CreateDB;
            this.radButton5.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radButton5.Location = new System.Drawing.Point(62, 73);
            this.radButton5.Name = "radButton5";
            this.radButton5.Size = new System.Drawing.Size(165, 83);
            this.radButton5.TabIndex = 2;
            this.radButton5.Text = "Создать БД";
            this.radButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.radButton5, "Создать пустой файл с начальной структурой БД");
            this.radButton5.Click += new System.EventHandler(this.radButton5_Click);
            // 
            // radButton4
            // 
            this.radButton4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.radButton4.Image = global::ProjectNSI.Properties.Resources.MaterialNeeds;
            this.radButton4.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radButton4.Location = new System.Drawing.Point(494, 199);
            this.radButton4.Name = "radButton4";
            this.radButton4.Size = new System.Drawing.Size(165, 83);
            this.radButton4.TabIndex = 1;
            this.radButton4.Text = "Потребность в материалах";
            this.radButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.radButton4, "Просмотр сведений по потребностям в материалах и генерация ведомостей");
            this.radButton4.Click += new System.EventHandler(this.radButton4_Click);
            // 
            // radButton3
            // 
            this.radButton3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radButton3.Image = global::ProjectNSI.Properties.Resources.Cash;
            this.radButton3.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radButton3.Location = new System.Drawing.Point(300, 199);
            this.radButton3.Name = "radButton3";
            this.radButton3.Size = new System.Drawing.Size(165, 83);
            this.radButton3.TabIndex = 1;
            this.radButton3.Text = "Материальное нормирование";
            this.radButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.radButton3, "Просмотр данных о материальном нормировании и генерация ведомостей");
            this.radButton3.Click += new System.EventHandler(this.radButton3_Click);
            // 
            // radButton2
            // 
            this.radButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radButton2.Image = global::ProjectNSI.Properties.Resources.RelationNodes;
            this.radButton2.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radButton2.Location = new System.Drawing.Point(494, 73);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(165, 83);
            this.radButton2.TabIndex = 1;
            this.radButton2.Text = "Разузлование";
            this.radButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.radButton2, "Выполение операции разузлования и построение списка полной применяемости");
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // radButton1
            // 
            this.radButton1.Image = global::ProjectNSI.Properties.Resources.Book;
            this.radButton1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radButton1.Location = new System.Drawing.Point(300, 73);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(165, 83);
            this.radButton1.TabIndex = 0;
            this.radButton1.Text = "Справочники";
            this.radButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.radButton1, "Просмотр и редактирование списка справочников БД редактора НСИ");
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Информация";
            // 
            // UC_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radButton13);
            this.Controls.Add(this.radButton12);
            this.Controls.Add(this.radButton10);
            this.Controls.Add(this.radButton9);
            this.Controls.Add(this.radButton8);
            this.Controls.Add(this.radButton7);
            this.Controls.Add(this.radButton6);
            this.Controls.Add(this.radButton5);
            this.Controls.Add(this.radButton4);
            this.Controls.Add(this.radButton3);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radButton1);
            this.Name = "UC_Main";
            this.Size = new System.Drawing.Size(957, 541);
            ((System.ComponentModel.ISupportInitialize)(this.radButton13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadButton radButton3;
        private Telerik.WinControls.UI.RadButton radButton4;
        private Telerik.WinControls.UI.RadButton radButton5;
        private Telerik.WinControls.UI.RadButton radButton6;
        private Telerik.WinControls.UI.RadButton radButton7;
        private Telerik.WinControls.UI.RadButton radButton8;
        private Telerik.WinControls.UI.RadButton radButton9;
        private Telerik.WinControls.UI.RadButton radButton10;
        private Telerik.WinControls.UI.RadButton radButton12;
        private Telerik.WinControls.UI.RadButton radButton13;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
