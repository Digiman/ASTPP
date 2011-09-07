namespace ProjectNSI
{
    partial class MaterialsNeeds
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
            this.planProductsGridView = new Telerik.WinControls.UI.RadGridView();
            this.radGroupBox3 = new Telerik.WinControls.UI.RadGroupBox();
            this.materialsNeedsGridView = new Telerik.WinControls.UI.RadGridView();
            this.radContextMenuManager1 = new Telerik.WinControls.UI.RadContextMenuManager();
            this.radContextMenu1 = new Telerik.WinControls.UI.RadContextMenu(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.radMenuHeaderItem1 = new Telerik.WinControls.UI.RadMenuHeaderItem();
            this.radMenuItem1 = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuItem2 = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuHeaderItem2 = new Telerik.WinControls.UI.RadMenuHeaderItem();
            this.radMenuItem3 = new Telerik.WinControls.UI.RadMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.planProductsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planProductsGridView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).BeginInit();
            this.radGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.materialsNeedsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialsNeedsGridView.MasterTemplate)).BeginInit();
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
            this.radGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(286, 203);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.Text = "Действия";
            // 
            // radButton3
            // 
            this.radButton3.Location = new System.Drawing.Point(25, 152);
            this.radButton3.Name = "radButton3";
            this.radButton3.Size = new System.Drawing.Size(236, 41);
            this.radButton3.TabIndex = 2;
            this.radButton3.Text = "Выдомость потребности в материалах";
            this.toolTip1.SetToolTip(this.radButton3, "Генерация ведомости суммарной потребности в материалах");
            this.radButton3.Click += new System.EventHandler(this.radButton3_Click);
            // 
            // radButton2
            // 
            this.radButton2.Location = new System.Drawing.Point(25, 101);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(236, 45);
            this.radButton2.TabIndex = 1;
            this.radButton2.Text = "Ведомость потребности в материалах в разрезе изделий";
            this.radButton2.TextWrap = true;
            this.toolTip1.SetToolTip(this.radButton2, "Генерация ведомостей потребностей в материалах в разрезе изделий");
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(13, 34);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(260, 24);
            this.radButton1.TabIndex = 0;
            this.radButton1.Text = "Составить таблицу ПМТВ";
            this.toolTip1.SetToolTip(this.radButton1, "Сгенерировать временную таблицу Потребность в материалах на товарный выпуск");
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radGroupBox2.Controls.Add(this.planProductsGridView);
            this.radGroupBox2.FooterImageIndex = -1;
            this.radGroupBox2.FooterImageKey = "";
            this.radGroupBox2.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox2.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox2.HeaderImageIndex = -1;
            this.radGroupBox2.HeaderImageKey = "";
            this.radGroupBox2.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox2.HeaderText = "Таблица \"План выпуска изделий\"";
            this.radGroupBox2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radGroupBox2.Location = new System.Drawing.Point(295, 3);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox2.Size = new System.Drawing.Size(659, 203);
            this.radGroupBox2.TabIndex = 1;
            this.radGroupBox2.Text = "Таблица \"План выпуска изделий\"";
            // 
            // planProductsGridView
            // 
            this.planProductsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.planProductsGridView.Location = new System.Drawing.Point(10, 20);
            // 
            // planProductsGridView
            // 
            this.planProductsGridView.MasterTemplate.AllowAddNewRow = false;
            this.planProductsGridView.MasterTemplate.AllowColumnReorder = false;
            this.planProductsGridView.Name = "planProductsGridView";
            this.radContextMenuManager1.SetRadContextMenu(this.planProductsGridView, this.radContextMenu1);
            this.planProductsGridView.ReadOnly = true;
            this.planProductsGridView.Size = new System.Drawing.Size(639, 173);
            this.planProductsGridView.TabIndex = 0;
            this.planProductsGridView.Text = "radGridView1";
            // 
            // radGroupBox3
            // 
            this.radGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radGroupBox3.Controls.Add(this.materialsNeedsGridView);
            this.radGroupBox3.FooterImageIndex = -1;
            this.radGroupBox3.FooterImageKey = "";
            this.radGroupBox3.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox3.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox3.HeaderImageIndex = -1;
            this.radGroupBox3.HeaderImageKey = "";
            this.radGroupBox3.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox3.HeaderText = "Таблица \"Потребность в материалах на товарный выпуск\"";
            this.radGroupBox3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radGroupBox3.Location = new System.Drawing.Point(3, 212);
            this.radGroupBox3.Name = "radGroupBox3";
            this.radGroupBox3.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox3.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox3.Size = new System.Drawing.Size(951, 326);
            this.radGroupBox3.TabIndex = 2;
            this.radGroupBox3.Text = "Таблица \"Потребность в материалах на товарный выпуск\"";
            // 
            // materialsNeedsGridView
            // 
            this.materialsNeedsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialsNeedsGridView.Location = new System.Drawing.Point(10, 20);
            // 
            // materialsNeedsGridView
            // 
            this.materialsNeedsGridView.MasterTemplate.AllowAddNewRow = false;
            this.materialsNeedsGridView.MasterTemplate.AllowColumnReorder = false;
            this.materialsNeedsGridView.Name = "materialsNeedsGridView";
            this.materialsNeedsGridView.ReadOnly = true;
            this.materialsNeedsGridView.Size = new System.Drawing.Size(931, 296);
            this.materialsNeedsGridView.TabIndex = 0;
            this.materialsNeedsGridView.Text = "radGridView2";
            // 
            // radContextMenu1
            // 
            this.radContextMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuHeaderItem1,
            this.radMenuItem1,
            this.radMenuItem2,
            this.radMenuHeaderItem2,
            this.radMenuItem3});
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Информация";
            // 
            // radMenuHeaderItem1
            // 
            this.radMenuHeaderItem1.Name = "radMenuHeaderItem1";
            this.radMenuHeaderItem1.Text = "Редактирование";
            // 
            // radMenuItem1
            // 
            this.radMenuItem1.Name = "radMenuItem1";
            this.radMenuItem1.Text = "Изменение записи";
            // 
            // radMenuItem2
            // 
            this.radMenuItem2.Name = "radMenuItem2";
            this.radMenuItem2.Text = "Добавление записи";
            // 
            // radMenuHeaderItem2
            // 
            this.radMenuHeaderItem2.Name = "radMenuHeaderItem2";
            this.radMenuHeaderItem2.Text = "Удаление";
            // 
            // radMenuItem3
            // 
            this.radMenuItem3.Name = "radMenuItem3";
            this.radMenuItem3.Text = "Удаление записи";
            // 
            // MaterialsNeeds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radGroupBox3);
            this.Controls.Add(this.radGroupBox2);
            this.Controls.Add(this.radGroupBox1);
            this.Name = "MaterialsNeeds";
            this.Size = new System.Drawing.Size(957, 541);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.planProductsGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planProductsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).EndInit();
            this.radGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.materialsNeedsGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialsNeedsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox3;
        private Telerik.WinControls.UI.RadGridView planProductsGridView;
        private Telerik.WinControls.UI.RadGridView materialsNeedsGridView;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadButton radButton3;
        private Telerik.WinControls.UI.RadContextMenuManager radContextMenuManager1;
        private Telerik.WinControls.UI.RadContextMenu radContextMenu1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Telerik.WinControls.UI.RadMenuHeaderItem radMenuHeaderItem1;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem1;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem2;
        private Telerik.WinControls.UI.RadMenuHeaderItem radMenuHeaderItem2;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem3;
    }
}
