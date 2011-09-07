namespace ProjectNSI
{
    partial class MaterialStandarts
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
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.radButton3 = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.fullApplicationGridView = new Telerik.WinControls.UI.RadGridView();
            this.radGroupBox3 = new Telerik.WinControls.UI.RadGroupBox();
            this.prodMatStandGridView = new Telerik.WinControls.UI.RadGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fullApplicationGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fullApplicationGridView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).BeginInit();
            this.radGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prodMatStandGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prodMatStandGridView.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.Controls.Add(this.radButton3);
            this.radGroupBox1.Controls.Add(this.radButton2);
            this.radGroupBox1.Controls.Add(this.radButton1);
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox1.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "Действия";
            this.radGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(326, 209);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.Text = "Действия";
            // 
            // radButton3
            // 
            this.radButton3.Location = new System.Drawing.Point(13, 115);
            this.radButton3.Name = "radButton3";
            this.radButton3.Size = new System.Drawing.Size(300, 45);
            this.radButton3.TabIndex = 2;
            this.radButton3.Text = "Ведомость сводных норм расхода материалов на изделие";
            this.radButton3.TextWrap = true;
            this.toolTip1.SetToolTip(this.radButton3, "Генерация ведомостей по сводным нормам расхода материалов на изделие");
            this.radButton3.Click += new System.EventHandler(this.GetReportForStandartsOnProductExecute);
            // 
            // radButton2
            // 
            this.radButton2.Location = new System.Drawing.Point(13, 85);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(300, 24);
            this.radButton2.TabIndex = 1;
            this.radButton2.Text = "Ведомость подетальных норм расхода материалов";
            this.toolTip1.SetToolTip(this.radButton2, "Генерация ведомости подетальных норм расхода материалов");
            this.radButton2.Click += new System.EventHandler(this.GetReportForStandartsExecute);
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(13, 23);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(300, 24);
            this.radButton1.TabIndex = 0;
            this.radButton1.Text = "Составить таблицу \"СНРМИ\"";
            this.toolTip1.SetToolTip(this.radButton1, "Составление временной таблицы Сводная норм расхода материала на изделия");
            this.radButton1.Click += new System.EventHandler(this.GetStandartsForProductsExecute);
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radGroupBox2.Controls.Add(this.fullApplicationGridView);
            this.radGroupBox2.FooterImageIndex = -1;
            this.radGroupBox2.FooterImageKey = "";
            this.radGroupBox2.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox2.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox2.HeaderImageIndex = -1;
            this.radGroupBox2.HeaderImageKey = "";
            this.radGroupBox2.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox2.HeaderText = "Таблица \"Полная применяемость\"";
            this.radGroupBox2.HeaderTextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radGroupBox2.Location = new System.Drawing.Point(335, 3);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox2.Size = new System.Drawing.Size(619, 209);
            this.radGroupBox2.TabIndex = 1;
            this.radGroupBox2.Text = "Таблица \"Полная применяемость\"";
            // 
            // fullApplicationGridView
            // 
            this.fullApplicationGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fullApplicationGridView.Location = new System.Drawing.Point(10, 20);
            // 
            // fullApplicationGridView
            // 
            this.fullApplicationGridView.MasterTemplate.AllowAddNewRow = false;
            this.fullApplicationGridView.MasterTemplate.AllowColumnReorder = false;
            this.fullApplicationGridView.Name = "fullApplicationGridView";
            this.fullApplicationGridView.ReadOnly = true;
            this.fullApplicationGridView.Size = new System.Drawing.Size(599, 179);
            this.fullApplicationGridView.TabIndex = 0;
            this.fullApplicationGridView.Text = "radGridView1";
            // 
            // radGroupBox3
            // 
            this.radGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radGroupBox3.Controls.Add(this.prodMatStandGridView);
            this.radGroupBox3.FooterImageIndex = -1;
            this.radGroupBox3.FooterImageKey = "";
            this.radGroupBox3.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox3.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox3.HeaderImageIndex = -1;
            this.radGroupBox3.HeaderImageKey = "";
            this.radGroupBox3.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox3.HeaderText = "Таблица \"Сводные нормы расхода материала на изделие\"";
            this.radGroupBox3.HeaderTextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radGroupBox3.Location = new System.Drawing.Point(3, 218);
            this.radGroupBox3.Name = "radGroupBox3";
            this.radGroupBox3.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox3.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox3.Size = new System.Drawing.Size(951, 320);
            this.radGroupBox3.TabIndex = 2;
            this.radGroupBox3.Text = "Таблица \"Сводные нормы расхода материала на изделие\"";
            // 
            // prodMatStandGridView
            // 
            this.prodMatStandGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prodMatStandGridView.Location = new System.Drawing.Point(10, 20);
            // 
            // prodMatStandGridView
            // 
            this.prodMatStandGridView.MasterTemplate.AllowAddNewRow = false;
            this.prodMatStandGridView.MasterTemplate.AllowColumnReorder = false;
            this.prodMatStandGridView.Name = "prodMatStandGridView";
            this.prodMatStandGridView.ReadOnly = true;
            this.prodMatStandGridView.Size = new System.Drawing.Size(931, 290);
            this.prodMatStandGridView.TabIndex = 0;
            this.prodMatStandGridView.Text = "radGridView1";
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Информация";
            // 
            // MaterialStandarts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radGroupBox3);
            this.Controls.Add(this.radGroupBox2);
            this.Controls.Add(this.radGroupBox1);
            this.Name = "MaterialStandarts";
            this.Size = new System.Drawing.Size(957, 541);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fullApplicationGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fullApplicationGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).EndInit();
            this.radGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.prodMatStandGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prodMatStandGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadGridView fullApplicationGridView;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox3;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadButton radButton3;
        private Telerik.WinControls.UI.RadGridView prodMatStandGridView;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
