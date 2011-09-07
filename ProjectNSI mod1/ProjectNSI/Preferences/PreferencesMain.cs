//*****************************************************************************
// ���� �������� ���������. ������� ��������� ��� ��������� ���������
//*****************************************************************************
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace ProjectNSI
{
    public partial class PreferencesMain : RadForm
    {
        UserControl Main;
        
        public PreferencesMain()
        {
            InitializeComponent();
        }

        //------------- ������ ���� -----------------
        // ������ ������
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // ������ ���������
        private void acceptButton_Click(object sender, EventArgs e)
        {
            // ���������� ��������� � ����������
            PrefWorker.SaveSettings();
        }
        // ������ ��
        private void okButton_Click(object sender, EventArgs e)
        {
            // ���������� ��������� � ����������
            PrefWorker.SaveSettings();
            this.Close();
        }
        //-------------------------------------------
        // ����� � ������ �������� ��������
        private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            switch (radTreeView1.SelectedNode.Text)
            {
                case "���������":
                    break;
                case "��������":
                    break;
                case "��������":
                    Main = new PrefMainFolders();
                    radPanel1.Controls.Clear();
                    radPanel1.Controls.Add(Main);
                    break;
                case "���":
                    Main = new PrefMainView();
                    radPanel1.Controls.Clear();
                    radPanel1.Controls.Add(Main);
                    break;
                case "��":
                    Main = new PrefDB();
                    radPanel1.Controls.Clear();
                    radPanel1.Controls.Add(Main);
                    break;
            }
        }
    }
}
