using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace We5ForcesModel
{
    public partial class OpenWeaponDB : Form
    {
        public OpenWeaponDB()
        {
            InitializeComponent();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();

            mainFrm frmMain = new mainFrm();
            frmMain.ShowDialog();

            this.Close();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            //엑셀 유효성 검사
            OpenFileDialog FileOpen = new OpenFileDialog();
            FileOpen.Title = "Open Weapon DB File";
            FileOpen.DefaultExt = "xlsx";
            FileOpen.Filter = "Excel (*.xlsx)|*.xlsx";
            FileOpen.FilterIndex = 0;
            FileOpen.RestoreDirectory = true;

            if(FileOpen.ShowDialog() == DialogResult.OK)
            {
                bunifuFlatButton1.Enabled = true;
            }

        }
    }
}
