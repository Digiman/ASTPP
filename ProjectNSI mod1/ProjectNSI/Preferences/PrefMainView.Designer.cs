namespace ProjectNSI
{
    partial class PrefMainView
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem5 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem6 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem7 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem8 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem9 = new Telerik.WinControls.UI.RadListDataItem();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.Controls.Add(this.radButton1);
            this.radGroupBox1.Controls.Add(this.radDropDownList1);
            this.radGroupBox1.Controls.Add(this.radLabel1);
            this.radGroupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox1.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "Стиль приложения";
            this.radGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(514, 94);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.Text = "Стиль приложения";
            // 
            // radDropDownList1
            // 
            radListDataItem1.Text = "Office2010";
            radListDataItem1.TextWrap = true;
            radListDataItem2.Text = "Windows7";
            radListDataItem2.TextWrap = true;
            radListDataItem3.Text = "Desert";
            radListDataItem3.TextWrap = true;
            radListDataItem4.Text = "Telerik";
            radListDataItem4.TextWrap = true;
            radListDataItem5.Text = "Office2007Black";
            radListDataItem5.TextWrap = true;
            radListDataItem6.Text = "Office2007Silver";
            radListDataItem6.TextWrap = true;
            radListDataItem7.Text = "Breeze";
            radListDataItem7.TextWrap = true;
            radListDataItem8.Text = "Aqua";
            radListDataItem8.TextWrap = true;
            radListDataItem9.Text = "Vista";
            radListDataItem9.TextWrap = true;
            this.radDropDownList1.Items.Add(radListDataItem1);
            this.radDropDownList1.Items.Add(radListDataItem2);
            this.radDropDownList1.Items.Add(radListDataItem3);
            this.radDropDownList1.Items.Add(radListDataItem4);
            this.radDropDownList1.Items.Add(radListDataItem5);
            this.radDropDownList1.Items.Add(radListDataItem6);
            this.radDropDownList1.Items.Add(radListDataItem7);
            this.radDropDownList1.Items.Add(radListDataItem8);
            this.radDropDownList1.Items.Add(radListDataItem9);
            this.radDropDownList1.Location = new System.Drawing.Point(185, 32);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.ShowImageInEditorArea = true;
            this.radDropDownList1.Size = new System.Drawing.Size(316, 21);
            this.radDropDownList1.TabIndex = 0;
            this.radDropDownList1.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownList1_SelectedIndexChanged);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(13, 32);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(166, 21);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "Выберите стиль из списка";
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(408, 59);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(93, 24);
            this.radButton1.TabIndex = 2;
            this.radButton1.Text = "Установить";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // PrefMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radGroupBox1);
            this.Name = "PrefMainView";
            this.Size = new System.Drawing.Size(520, 407);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton radButton1;
    }
}
