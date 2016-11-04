using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;
using System.Collections;

namespace We5ForcesModel
{
    public partial class mainFrm : Form
    {
        OleDbConnection ExcelConn;
        public string strCon;
        public string strSQL;

        public static int CurrentMenu = 0;
        public static int CurrentPage = 0;
        public static int CurrentWeapon = 2;
        public static int MaximumMenus= 6;
        public static int[] MaximumPages = new int[] { 3,3,3,3,3,1};

        public mainFrm()
        {
            InitializeComponent();
        }
        // 경쟁/유사/무기체계 DB 데이터
        
        public static object[,] CompetitionData;
        public static object[,] Similar;
        public static object[,] Substitution;

        // 원천기술 DB데이터
        public static object[,] OriginalTechnology;

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal) { this.WindowState = FormWindowState.Maximized; }
            else if (this.WindowState == FormWindowState.Maximized) { this.WindowState = FormWindowState.Normal; }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if(SlideMenu.Width == 50)
            {
                SlideMenu.Visible = false;
                SlideMenu.Width = 260;
                PanelAnimation.ShowSync(SlideMenu);
            }
            else
            {
                SlideMenu.Width = 50;
            }
        }
        private void ReadExcelData()
        {
            object missing = Type.Missing;

            string ExcelPath = "DB\\3.RCWS.xlsx";
            ExcelPath = System.IO.Path.GetFullPath(ExcelPath);
            
            if(File.Exists(ExcelPath))
            {
                strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
                strCon += ExcelPath;
                strCon += ";Extended Properties='Excel 12.0;HDR=No;'";

                ExcelConn = new OleDbConnection(strCon);
                ExcelConn.Open();
                
                // 경쟁무기체계 Sheet를 불러옴
                DataSet DB = new DataSet();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Competition$];", ExcelConn))
                {
                    adapter.Fill(DB);
                }
                // 이를 Object 배열로 변환함.
                CompetitionData = new object[DB.Tables[0].Rows.Count, DB.Tables[0].Rows[0].ItemArray.Count()];

                for(int i = 0; i < DB.Tables[0].Rows.Count; i++)
                {
                    for(int j = 0; j < DB.Tables[0].Rows[0].ItemArray.Count(); j++)
                    {
                        CompetitionData[i, j] = DB.Tables[0].Rows[i].ItemArray[j].ToString();
                    }
                }
                DB.Clear();

                // 유사무기체계 Sheet를 불러옴
                using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Similar$];", ExcelConn))
                {
                    adapter.Fill(DB);
                }
                // 이를 Object 배열로 변환함.
                Similar = new object[DB.Tables[0].Rows.Count, DB.Tables[0].Rows[0].ItemArray.Count()];

                for (int i = 0; i < DB.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < DB.Tables[0].Rows[0].ItemArray.Count(); j++)
                    {
                        Similar[i, j] = DB.Tables[0].Rows[i].ItemArray[j].ToString();
                    }
                }
                DB.Clear();

                // 대체무기체계 Sheet를 불러옴
                using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Substitution$];", ExcelConn))
                {
                    adapter.Fill(DB);
                }
                // 이를 Object 배열로 변환함.
                Substitution = new object[DB.Tables[0].Rows.Count, DB.Tables[0].Rows[0].ItemArray.Count()];

                for (int i = 0; i < DB.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < DB.Tables[0].Rows[0].ItemArray.Count(); j++)
                    {
                        Substitution[i, j] = DB.Tables[0].Rows[i].ItemArray[j].ToString();
                    }
                }
                DB.Clear();
                ExcelConn.Close();
            }
        }


        private void mainFrm_Load(object sender, EventArgs e)
        {
            ReadExcelData();
            bunifuFlatButton1_Click(sender, e);
        }

        #region 1.경쟁무기체계 SubPage
        private void Show_Competition1_Form_In_Panel()
        {
            panel3.Controls.Clear();
            Competition1 SubForm = new Competition1();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        private void Show_Competition2_Form_In_Panel()
        {
            panel3.Controls.Clear();
            Competition2 SubForm = new Competition2();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        private void Show_Competition3_Form_In_Panel()
        {
            panel3.Controls.Clear();
            Competition3 SubForm = new Competition3();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        #endregion
        #region 3.원천기술식별 SubPage
        private void Show_OriginalTechnology1_Form_In_Panel()
        {
            panel3.Controls.Clear();
            OriginalTechnology1 SubForm = new OriginalTechnology1();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        private void Show_OriginalTechnology2_Form_In_Panel()
        {
            panel3.Controls.Clear();
            OriginalTechnology2 SubForm = new OriginalTechnology2();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        private void Show_OriginalTechnology3_Form_In_Panel()
        {
            panel3.Controls.Clear();
            OriginalTechnology3 SubForm = new OriginalTechnology3();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        #endregion
        #region 4.수출유망국가식별 SubPage
        private void Show_PromisingNation1_Form_In_Panel()
        {
            panel3.Controls.Clear();
            PromisingNation1 SubForm = new PromisingNation1();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        private void Show_PromisingNation2_Form_In_Panel()
        {
            panel3.Controls.Clear();
            PromisingNation2 SubForm = new PromisingNation2();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        private void Show_PromisingNation3_Form_In_Panel()
        {
            panel3.Controls.Clear();
            PromisingNation3 SubForm = new PromisingNation3();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        #endregion
        #region SubPage(Previous, Next)

        private void UpdatePagesInfo()
        {
            LblPage.Text = (CurrentPage + 1) + " / " + MaximumPages[CurrentMenu];
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if(CurrentPage + 1 < MaximumPages[CurrentMenu])
            {
                CurrentPage++;
                UpdatePagesInfo();

                // 경쟁무기체계
                if(CurrentMenu==0 && CurrentPage == 1) { Show_Competition2_Form_In_Panel(); }
                if (CurrentMenu == 0 && CurrentPage == 2) { Show_Competition3_Form_In_Panel(); }

                //대체/유사무기체계
                if (CurrentMenu == 1 && CurrentPage == 1) { }
                if (CurrentMenu == 1 && CurrentPage == 2) { }

                //원천기술 분석
                if (CurrentMenu == 2 && CurrentPage == 1) { Show_OriginalTechnology2_Form_In_Panel(); }
                if (CurrentMenu == 2 && CurrentPage == 2) { Show_OriginalTechnology3_Form_In_Panel(); }

                // 수출유망국가 분석
                if (CurrentMenu == 3 && CurrentPage == 1) { Show_PromisingNation2_Form_In_Panel(); }
                if (CurrentMenu == 3 && CurrentPage == 2) { Show_PromisingNation3_Form_In_Panel(); }

                //시장진입가능성 분석
                if (CurrentMenu == 4 && CurrentPage == 1) { }
                if (CurrentMenu == 4 && CurrentPage == 2) { }
            }
        }

        private void PreviouslyButton_Click(object sender, EventArgs e)
        {
            if (CurrentPage - 1 >= 0)
            {
                CurrentPage--;
                UpdatePagesInfo();

                // 경쟁무기 체계
                if (CurrentMenu == 0 && CurrentPage == 0) { Show_Competition1_Form_In_Panel(); }
                if (CurrentMenu == 0 && CurrentPage == 1) { Show_Competition2_Form_In_Panel(); }
                
                //대체/유사무기체계
                if (CurrentMenu == 1 && CurrentPage == 0) { }
                if (CurrentMenu == 1 && CurrentPage == 1) { }

                //원천기술 분석
                if (CurrentMenu == 2 && CurrentPage == 0) { Show_OriginalTechnology1_Form_In_Panel(); }
                if (CurrentMenu == 2 && CurrentPage == 1) { Show_OriginalTechnology2_Form_In_Panel(); }

                // 수출유망국가 분석
                if (CurrentMenu == 3 && CurrentPage == 0) { Show_PromisingNation1_Form_In_Panel(); }
                if (CurrentMenu == 3 && CurrentPage == 1) { Show_PromisingNation2_Form_In_Panel(); }

                //시장진입가능성 분석
                if (CurrentMenu == 4 && CurrentPage == 0) { }
                if (CurrentMenu == 4 && CurrentPage == 1) { }
            }
        }
        #endregion
        #region ButtonMennuClick
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            CurrentMenu = 0;
            CurrentPage = 0;
            UpdatePagesInfo();

            // 1page를 봄
            Show_Competition1_Form_In_Panel();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            CurrentMenu = 1;
            CurrentPage = 0;
            UpdatePagesInfo();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            CurrentMenu = 2;
            CurrentPage = 0;

            UpdatePagesInfo();
            Show_OriginalTechnology1_Form_In_Panel();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            CurrentMenu = 3;
            CurrentPage = 0;
            UpdatePagesInfo();

            // 1page를 봄
            Show_PromisingNation1_Form_In_Panel();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            CurrentMenu = 4;
            CurrentPage = 0;
            UpdatePagesInfo();

        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            CurrentMenu = 5;
            CurrentPage = 0;
            UpdatePagesInfo();
        }
        #endregion
    }
}
