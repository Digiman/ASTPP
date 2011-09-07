namespace ProjectNSI
{
    partial class ReportViewerForm
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
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            this.radLabelElement1 = new Telerik.WinControls.UI.RadLabelElement();
            this.radCommandBar1 = new Telerik.WinControls.UI.RadCommandBar();
            this.radToolStrip1 = new Telerik.WinControls.UI.RadToolStrip();
            this.radToolStripElement1 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStripItem1 = new Telerik.WinControls.UI.RadToolStripItem();
            this.openButton = new Telerik.WinControls.UI.RadButtonElement();
            this.printButton = new Telerik.WinControls.UI.RadButtonElement();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.AutoSize = true;
            this.radStatusStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radLabelElement1});
            this.radStatusStrip1.LayoutStyle = Telerik.WinControls.UI.RadStatusBarLayoutStyle.Stack;
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 452);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Size = new System.Drawing.Size(627, 26);
            this.radStatusStrip1.TabIndex = 0;
            this.radStatusStrip1.Text = "radStatusStrip1";
            // 
            // radLabelElement1
            // 
            this.radLabelElement1.Margin = new System.Windows.Forms.Padding(1);
            this.radLabelElement1.Name = "radLabelElement1";
            this.radStatusStrip1.SetSpring(this.radLabelElement1, false);
            this.radLabelElement1.Text = "radLabelElement1";
            this.radLabelElement1.TextWrap = true;
            // 
            // radCommandBar1
            // 
            this.radCommandBar1.AutoSize = true;
            this.radCommandBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar1.Name = "radCommandBar1";
            this.radCommandBar1.Size = new System.Drawing.Size(627, 2);
            this.radCommandBar1.TabIndex = 1;
            this.radCommandBar1.Text = "radCommandBar1";
            // 
            // radToolStrip1
            // 
            this.radToolStrip1.AllowDragging = false;
            this.radToolStrip1.AllowFloating = false;
            this.radToolStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radToolStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripElement1});
            this.radToolStrip1.Location = new System.Drawing.Point(0, 2);
            this.radToolStrip1.MinimumSize = new System.Drawing.Size(5, 5);
            this.radToolStrip1.Name = "radToolStrip1";
            this.radToolStrip1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radToolStrip1.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.radToolStrip1.RootElement.MinSize = new System.Drawing.Size(5, 5);
            this.radToolStrip1.ShowOverFlowButton = true;
            this.radToolStrip1.Size = new System.Drawing.Size(627, 43);
            this.radToolStrip1.TabIndex = 3;
            this.radToolStrip1.Text = "radToolStrip1";
            // 
            // radToolStripElement1
            // 
            this.radToolStripElement1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripItem1});
            this.radToolStripElement1.Name = "radToolStripElement1";
            // 
            // radToolStripItem1
            // 
            this.radToolStripItem1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.openButton,
            this.printButton});
            this.radToolStripItem1.Key = "0";
            this.radToolStripItem1.Name = "radToolStripItem1";
            this.radToolStripItem1.Text = "radToolStripItem1";
            // 
            // openButton
            // 
            this.openButton.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.openButton.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.openButton.Image = global::ProjectNSI.Properties.Resources.OpenReportFile;
            this.openButton.Name = "openButton";
            this.openButton.ShowBorder = false;
            this.openButton.Text = "radButtonElement1";
            this.openButton.ToolTipText = "Открыть файл с отчетом";
            this.openButton.Click += new System.EventHandler(this.OpenReportExecute);
            // 
            // printButton
            // 
            this.printButton.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.printButton.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.printButton.Image = global::ProjectNSI.Properties.Resources.PrintReport;
            this.printButton.Name = "printButton";
            this.printButton.ShowBorder = false;
            this.printButton.Text = "radButtonElement2";
            this.printButton.ToolTipText = "Печать отчета";
            this.printButton.Click += new System.EventHandler(this.PrintReportExecute);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 45);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(627, 407);
            this.webBrowser1.TabIndex = 4;
            // 
            // ReportViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 478);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.radToolStrip1);
            this.Controls.Add(this.radCommandBar1);
            this.Controls.Add(this.radStatusStrip1);
            this.Name = "ReportViewerForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Просмотрщик отчетов";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        private Telerik.WinControls.UI.RadCommandBar radCommandBar1;
        private Telerik.WinControls.UI.RadLabelElement radLabelElement1;
        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem1;
        private Telerik.WinControls.UI.RadButtonElement openButton;
        private Telerik.WinControls.UI.RadButtonElement printButton;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}
