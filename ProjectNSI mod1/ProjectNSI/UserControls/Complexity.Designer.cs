namespace ProjectNSI
{
    partial class Complexity
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
            this.radButton4 = new Telerik.WinControls.UI.RadButton();
            this.radButton3 = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.fullApplicationGridView = new Telerik.WinControls.UI.RadGridView();
            this.radGroupBox3 = new Telerik.WinControls.UI.RadGroupBox();
            this.complexityGridView = new Telerik.WinControls.UI.RadGridView();
            this.radGroupBox4 = new Telerik.WinControls.UI.RadGroupBox();
            this.complexityShopGridView = new Telerik.WinControls.UI.RadGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButton4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fullApplicationGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fullApplicationGridView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).BeginInit();
            this.radGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.complexityGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.complexityGridView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox4)).BeginInit();
            this.radGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.complexityShopGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.complexityShopGridView.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.Controls.Add(this.radButton4);
            this.radGroupBox1.Controls.Add(this.radButton3);
            this.radGroupBox1.Controls.Add(this.radButton2);
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox1.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "Действия";
            this.radGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(258, 181);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.Text = "Действия";
            // 
            // radButton4
            // 
            this.radButton4.Location = new System.Drawing.Point(13, 117);
            this.radButton4.Name = "radButton4";
            this.radButton4.Size = new System.Drawing.Size(232, 41);
            this.radButton4.TabIndex = 3;
            this.radButton4.Text = "Ведомость нормативной трудоемкости производственной программы по цехам";
            this.radButton4.TextWrap = true;
            this.toolTip1.SetToolTip(this.radButton4, "Генерация ведомостей нормативной трудоемкости производственной программы по цехам" +
                    " по месяцам");
            this.radButton4.Click += new System.EventHandler(this.radButton4_Click);
            // 
            // radButton3
            // 
            this.radButton3.Location = new System.Drawing.Point(13, 70);
            this.radButton3.Name = "radButton3";
            this.radButton3.Size = new System.Drawing.Size(232, 41);
            this.radButton3.TabIndex = 2;
            this.radButton3.Text = "Ведомость нормативной трудоемкости производственной программы";
            this.radButton3.TextWrap = true;
            this.toolTip1.SetToolTip(this.radButton3, "Генерация ведомостей нормативной трудоемкости производственной программы по месяц" +
                    "ам");
            this.radButton3.Click += new System.EventHandler(this.radButton3_Click);
            // 
            // radButton2
            // 
            this.radButton2.Location = new System.Drawing.Point(13, 23);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(232, 41);
            this.radButton2.TabIndex = 1;
            this.radButton2.Text = "Сводная ведомость нормативной трудоемкости на изделие";
            this.radButton2.TextWrap = true;
            this.toolTip1.SetToolTip(this.radButton2, "Генерация сводной ведомости нормативной трудоемкости на изделия");
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
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
            this.radGroupBox2.HeaderText = "ТБД \"Полная применяемость\"";
            this.radGroupBox2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radGroupBox2.Location = new System.Drawing.Point(267, 3);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox2.Size = new System.Drawing.Size(687, 181);
            this.radGroupBox2.TabIndex = 1;
            this.radGroupBox2.Text = "ТБД \"Полная применяемость\"";
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
            this.fullApplicationGridView.Size = new System.Drawing.Size(667, 151);
            this.fullApplicationGridView.TabIndex = 0;
            this.fullApplicationGridView.Text = "radGridView1";
            // 
            // radGroupBox3
            // 
            this.radGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radGroupBox3.Controls.Add(this.complexityGridView);
            this.radGroupBox3.FooterImageIndex = -1;
            this.radGroupBox3.FooterImageKey = "";
            this.radGroupBox3.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox3.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox3.HeaderImageIndex = -1;
            this.radGroupBox3.HeaderImageKey = "";
            this.radGroupBox3.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox3.HeaderText = "ТБД \"Сводная нормативная трудоемкость на изделие\"";
            this.radGroupBox3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radGroupBox3.Location = new System.Drawing.Point(6, 190);
            this.radGroupBox3.Name = "radGroupBox3";
            this.radGroupBox3.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox3.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox3.Size = new System.Drawing.Size(951, 167);
            this.radGroupBox3.TabIndex = 2;
            this.radGroupBox3.Text = "ТБД \"Сводная нормативная трудоемкость на изделие\"";
            // 
            // complexityGridView
            // 
            this.complexityGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.complexityGridView.Location = new System.Drawing.Point(10, 20);
            // 
            // complexityGridView
            // 
            this.complexityGridView.MasterTemplate.AllowAddNewRow = false;
            this.complexityGridView.MasterTemplate.AllowColumnReorder = false;
            this.complexityGridView.Name = "complexityGridView";
            this.complexityGridView.ReadOnly = true;
            this.complexityGridView.Size = new System.Drawing.Size(931, 137);
            this.complexityGridView.TabIndex = 0;
            this.complexityGridView.Text = "radGridView2";
            // 
            // radGroupBox4
            // 
            this.radGroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radGroupBox4.Controls.Add(this.complexityShopGridView);
            this.radGroupBox4.FooterImageIndex = -1;
            this.radGroupBox4.FooterImageKey = "";
            this.radGroupBox4.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox4.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox4.HeaderImageIndex = -1;
            this.radGroupBox4.HeaderImageKey = "";
            this.radGroupBox4.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox4.HeaderText = "ТБД \"Сводная нормативная трудоемкость на изделие по цехам\"";
            this.radGroupBox4.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radGroupBox4.Location = new System.Drawing.Point(6, 363);
            this.radGroupBox4.Name = "radGroupBox4";
            this.radGroupBox4.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox4.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox4.Size = new System.Drawing.Size(951, 175);
            this.radGroupBox4.TabIndex = 3;
            this.radGroupBox4.Text = "ТБД \"Сводная нормативная трудоемкость на изделие по цехам\"";
            // 
            // complexityShopGridView
            // 
            this.complexityShopGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.complexityShopGridView.Location = new System.Drawing.Point(10, 20);
            // 
            // complexityShopGridView
            // 
            this.complexityShopGridView.MasterTemplate.AllowAddNewRow = false;
            this.complexityShopGridView.MasterTemplate.AllowColumnReorder = false;
            this.complexityShopGridView.Name = "complexityShopGridView";
            this.complexityShopGridView.ReadOnly = true;
            this.complexityShopGridView.Size = new System.Drawing.Size(931, 145);
            this.complexityShopGridView.TabIndex = 0;
            this.complexityShopGridView.Text = "radGridView2";
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Информация";
            // 
            // Complexity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radGroupBox4);
            this.Controls.Add(this.radGroupBox3);
            this.Controls.Add(this.radGroupBox2);
            this.Controls.Add(this.radGroupBox1);
            this.Name = "Complexity";
            this.Size = new System.Drawing.Size(957, 541);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radButton4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fullApplicationGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fullApplicationGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).EndInit();
            this.radGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.complexityGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.complexityGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox4)).EndInit();
            this.radGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.complexityShopGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.complexityShopGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox3;
        private Telerik.WinControls.UI.RadGridView fullApplicationGridView;
        private Telerik.WinControls.UI.RadGridView complexityGridView;
        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadButton radButton3;
        private Telerik.WinControls.UI.RadButton radButton4;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox4;
        private Telerik.WinControls.UI.RadGridView complexityShopGridView;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
