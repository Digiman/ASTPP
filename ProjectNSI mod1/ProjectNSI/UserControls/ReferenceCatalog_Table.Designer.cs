namespace ProjectNSI
{
    partial class ReferenceCatalog_Table
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
            this.radToolStrip1 = new Telerik.WinControls.UI.RadToolStrip();
            this.radToolStripElement1 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStripItem2 = new Telerik.WinControls.UI.RadToolStripItem();
            this.dataTableView = new Telerik.WinControls.UI.RadGridView();
            this.addRowButton = new Telerik.WinControls.UI.RadButtonElement();
            this.deleteRowButton = new Telerik.WinControls.UI.RadButtonElement();
            this.editRowButton = new Telerik.WinControls.UI.RadButtonElement();
            this.radToolStripElement2 = new Telerik.WinControls.UI.RadToolStripElement();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableView.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // radToolStrip1
            // 
            this.radToolStrip1.AllowDragging = false;
            this.radToolStrip1.AllowFloating = false;
            this.radToolStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radToolStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripElement1});
            this.radToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.radToolStrip1.MinimumSize = new System.Drawing.Size(5, 5);
            this.radToolStrip1.Name = "radToolStrip1";
            this.radToolStrip1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radToolStrip1.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.radToolStrip1.RootElement.MinSize = new System.Drawing.Size(5, 5);
            this.radToolStrip1.ShowOverFlowButton = true;
            this.radToolStrip1.Size = new System.Drawing.Size(957, 69);
            this.radToolStrip1.TabIndex = 0;
            this.radToolStrip1.Text = "radToolStrip1";
            // 
            // radToolStripElement1
            // 
            this.radToolStripElement1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripItem2});
            this.radToolStripElement1.Name = "radToolStripElement1";
            // 
            // radToolStripItem2
            // 
            this.radToolStripItem2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.addRowButton,
            this.deleteRowButton,
            this.editRowButton});
            this.radToolStripItem2.Key = "0";
            this.radToolStripItem2.Name = "radToolStripItem2";
            this.radToolStripItem2.Text = "radToolStripItem2";
            // 
            // dataTableView
            // 
            this.dataTableView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataTableView.Location = new System.Drawing.Point(0, 69);
            // 
            // dataTableView
            // 
            this.dataTableView.MasterTemplate.AllowAddNewRow = false;
            this.dataTableView.MasterTemplate.AllowColumnReorder = false;
            this.dataTableView.Name = "dataTableView";
            this.dataTableView.ReadOnly = true;
            this.dataTableView.Size = new System.Drawing.Size(957, 472);
            this.dataTableView.SynchronizeCurrentRowInSplitMode = true;
            this.dataTableView.TabIndex = 1;
            this.dataTableView.Text = "radGridView1";
            // 
            // addRowButton
            // 
            this.addRowButton.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.addRowButton.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.addRowButton.Image = global::ProjectNSI.Properties.Resources.InsertRow;
            this.addRowButton.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.addRowButton.Name = "addRowButton";
            this.addRowButton.ShowBorder = false;
            this.addRowButton.Text = "addRowButton";
            this.addRowButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.addRowButton.ToolTipText = "Добавить строку с данными в таблицу";
            this.addRowButton.Click += new System.EventHandler(this.addRowButton_Click);
            // 
            // deleteRowButton
            // 
            this.deleteRowButton.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.deleteRowButton.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.deleteRowButton.Image = global::ProjectNSI.Properties.Resources.RemoveRow;
            this.deleteRowButton.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.deleteRowButton.Name = "deleteRowButton";
            this.deleteRowButton.ShowBorder = false;
            this.deleteRowButton.Text = "Удалить строку";
            this.deleteRowButton.ToolTipText = "Удаление выбранной строки из таблицы";
            this.deleteRowButton.Click += new System.EventHandler(this.deleteRowButton_Click);
            // 
            // editRowButton
            // 
            this.editRowButton.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.editRowButton.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.editRowButton.Image = global::ProjectNSI.Properties.Resources.EditRecord;
            this.editRowButton.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.editRowButton.Name = "editRowButton";
            this.editRowButton.ShowBorder = false;
            this.editRowButton.Text = "Редактировать запись";
            this.editRowButton.ToolTipText = "Редактирование полей записи";
            this.editRowButton.Click += new System.EventHandler(this.editRowButton_Click);
            // 
            // radToolStripElement2
            // 
            this.radToolStripElement2.Name = "radToolStripElement2";
            // 
            // ReferenceCatalog_Table
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataTableView);
            this.Controls.Add(this.radToolStrip1);
            this.Name = "ReferenceCatalog_Table";
            this.Size = new System.Drawing.Size(957, 541);
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadGridView dataTableView;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem2;
        private Telerik.WinControls.UI.RadButtonElement addRowButton;
        private Telerik.WinControls.UI.RadButtonElement deleteRowButton;
        private Telerik.WinControls.UI.RadButtonElement editRowButton;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement2;

    }
}
