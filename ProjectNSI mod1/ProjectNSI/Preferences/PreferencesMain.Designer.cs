namespace ProjectNSI
{
    partial class PreferencesMain
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
            Telerik.WinControls.UI.RadTreeNode radTreeNode1 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode2 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode3 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode4 = new Telerik.WinControls.UI.RadTreeNode();
            Telerik.WinControls.UI.RadTreeNode radTreeNode5 = new Telerik.WinControls.UI.RadTreeNode();
            this.cancelButton = new Telerik.WinControls.UI.RadButton();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.acceptButton = new Telerik.WinControls.UI.RadButton();
            this.okButton = new Telerik.WinControls.UI.RadButton();
            this.radTreeView1 = new Telerik.WinControls.UI.RadTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.cancelButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.acceptButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.okButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(642, 425);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(91, 24);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // radPanel1
            // 
            this.radPanel1.Location = new System.Drawing.Point(213, 12);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(520, 407);
            this.radPanel1.TabIndex = 1;
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(542, 425);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(94, 24);
            this.acceptButton.TabIndex = 3;
            this.acceptButton.Text = "Применить";
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(469, 425);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(67, 24);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // radTreeView1
            // 
            this.radTreeView1.BackColor = System.Drawing.SystemColors.Window;
            this.radTreeView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radTreeView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radTreeView1.ForeColor = System.Drawing.Color.Black;
            this.radTreeView1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.radTreeView1.LineStyle = Telerik.WinControls.UI.TreeLineStyle.Solid;
            this.radTreeView1.Location = new System.Drawing.Point(12, 12);
            this.radTreeView1.Name = "radTreeView1";
            radTreeNode1.Expanded = true;
            radTreeNode2.Expanded = true;
            radTreeNode3.Checked = true;
            radTreeNode3.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
            radTreeNode3.Text = "Каталоги";
            radTreeNode4.Expanded = true;
            radTreeNode4.Text = "Вид";
            radTreeNode2.Nodes.Add(radTreeNode3);
            radTreeNode2.Nodes.Add(radTreeNode4);
            radTreeNode2.Text = "Основные";
            radTreeNode5.Expanded = true;
            radTreeNode5.Text = "БД";
            radTreeNode1.Nodes.Add(radTreeNode2);
            radTreeNode1.Nodes.Add(radTreeNode5);
            radTreeNode1.Text = "Настройки";
            this.radTreeView1.Nodes.Add(radTreeNode1);
            this.radTreeView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.radTreeView1.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radTreeView1.ShowLines = true;
            this.radTreeView1.Size = new System.Drawing.Size(195, 407);
            this.radTreeView1.TabIndex = 2;
            this.radTreeView1.Text = "radTreeView1";
            this.radTreeView1.SelectedNodeChanged += new Telerik.WinControls.UI.RadTreeView.RadTreeViewEventHandler(this.radTreeView1_SelectedNodeChanged);
            // 
            // PreferencesMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 461);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.radTreeView1);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesMain";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки программы";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.cancelButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.acceptButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.okButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton cancelButton;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton acceptButton;
        private Telerik.WinControls.UI.RadButton okButton;
        private Telerik.WinControls.UI.RadTreeView radTreeView1;
    }
}
