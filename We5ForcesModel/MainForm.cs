﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using LinqToExcel;

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
        public static int MaximumMenus = 7;
        public static int[] MaximumPages = new int[] { 3, 3, 3, 3, 3, 1, 1 };

        public static string[] DomesticSpec;

        public mainFrm()
        {
            InitializeComponent();
        }
        // 경쟁/유사/무기체계 DB 데이터

        public static object[,] CompetitionData;
        public static object[,] Similar;
        public static object[,] Substitution;

        public static List<string> CompetitionWeapon;
        public static List<string> SimilarityWeapon;
        public static List<string> SubstitutionWeapon;

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


            if (File.Exists(ExcelPath))
            {
                string sheetName = "Competition";

                var excelFile = new ExcelQueryFactory(ExcelPath);
                var ExcelData = (from a in excelFile.Worksheet(sheetName) select a).ToList();

                int ColumnCount = 0;
                int RowCount = Convert.ToInt32(ExcelData.Count());

                foreach (var data in ExcelData)
                {
                    ColumnCount = data.ColumnNames.Count();
                }
                CompetitionData = new object[RowCount + 1, ColumnCount];

                foreach (var data in ExcelData)
                {
                    string[] temp = data.ColumnNames.ToArray();
                    for (int i = 0; i < temp.Length; i++)
                    {
                        CompetitionData[0, i] = temp[i]; //ColumnName;
                    }
                    for (int i = 0; i < ExcelData.Count(); i++)
                    {
                        for (int j = 0; j < ExcelData[i].Count(); j++)
                        {
                            CompetitionData[i + 1, j] = ExcelData[i][j].ToString();
                        }

                    }
                    break;
                }
                // 대체
                sheetName = "Substitution";
                ExcelData.Clear();
                ExcelData = (from a in excelFile.Worksheet(sheetName) select a).ToList();

                ColumnCount = 0;
                RowCount = Convert.ToInt32(ExcelData.Count());

                foreach (var data in ExcelData)
                {
                    ColumnCount = data.ColumnNames.Count();
                }
                Substitution = new object[RowCount + 1, ColumnCount];

                foreach (var data in ExcelData)
                {
                    string[] temp = data.ColumnNames.ToArray();
                    for (int i = 0; i < temp.Length; i++)
                    {
                        Substitution[0, i] = temp[i]; //ColumnName;
                    }
                    for (int i = 0; i < ExcelData.Count(); i++)
                    {
                        for (int j = 0; j < ExcelData[i].Count(); j++)
                        {
                            Substitution[i + 1, j] = ExcelData[i][j].ToString();
                        }

                    }
                    break;
                }
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
        #region 2.유사무기체계 SubPage
        private void Show_Similarity1_Form_In_Panel()
        {
            panel3.Controls.Clear();
            Similarity1 SubForm = new Similarity1();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        private void Show_Similarity2_Form_In_Panel()
        {
            panel3.Controls.Clear();
            Similarity2 SubForm = new Similarity2();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        #endregion
        #region 3.대체무기체계 SubPage
        private void Show_Substitute1_Form_In_Panel()
        {
            panel3.Controls.Clear();
            Substitute1 SubForm = new Substitute1();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        private void Show_Substitute2_Form_In_Panel()
        {
            panel3.Controls.Clear();
            Substitute2 SubForm = new Substitute2();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        private void Show_Substitute3_Form_In_Panel()
        {
            panel3.Controls.Clear();
            Substitute3 SubForm = new Substitute3();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        #endregion
        #region 4.원천기술식별 SubPage
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
        #region 5.수출유망국가식별 SubPage
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
        #region 6.시장진입가능성 분석
        private void Show_SWOT1_Form_In_Panel()
        {
            panel3.Controls.Clear();
            SWOT SubForm = new SWOT();
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

                //유사무기체계
                if (CurrentMenu == 1 && CurrentPage == 1) { Show_Similarity2_Form_In_Panel(); }
                if (CurrentMenu == 1 && CurrentPage == 2) {  }

                // 대체무기체계
                if (CurrentMenu == 2 && CurrentPage == 1) { Show_Substitute2_Form_In_Panel(); }
                if (CurrentMenu == 2 && CurrentPage == 2) { Show_Substitute3_Form_In_Panel(); }

                //원천기술 분석
                if (CurrentMenu == 3 && CurrentPage == 1) { Show_OriginalTechnology2_Form_In_Panel(); }
                if (CurrentMenu == 3 && CurrentPage == 2) { Show_OriginalTechnology3_Form_In_Panel(); }

                // 수출유망국가 분석
                if (CurrentMenu == 4 && CurrentPage == 1) { Show_PromisingNation2_Form_In_Panel(); }
                if (CurrentMenu == 4 && CurrentPage == 2) { Show_PromisingNation3_Form_In_Panel(); }

                //시장진입가능성 분석
                if (CurrentMenu == 5 && CurrentPage == 1) { Show_SWOT1_Form_In_Panel(); }
                if (CurrentMenu == 5 && CurrentPage == 2) { Show_SWOT1_Form_In_Panel(); }
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

                // 유사무기체계
                if (CurrentMenu == 1 && CurrentPage == 0) { Show_Similarity1_Form_In_Panel(); }
                if (CurrentMenu == 1 && CurrentPage == 1) { Show_Similarity2_Form_In_Panel(); }

                // 대체무기체계
                if (CurrentMenu == 2 && CurrentPage == 0) { Show_Substitute1_Form_In_Panel(); }
                if (CurrentMenu == 2 && CurrentPage == 1) { Show_Substitute2_Form_In_Panel(); }

                //원천기술 분석
                if (CurrentMenu == 3 && CurrentPage == 0) { Show_OriginalTechnology1_Form_In_Panel(); }
                if (CurrentMenu == 3 && CurrentPage == 1) { Show_OriginalTechnology2_Form_In_Panel(); }

                // 수출유망국가 분석
                if (CurrentMenu == 4 && CurrentPage == 0) { Show_PromisingNation1_Form_In_Panel(); }
                if (CurrentMenu == 4 && CurrentPage == 1) { Show_PromisingNation2_Form_In_Panel(); }

                //시장진입가능성 분석
                if (CurrentMenu == 5 && CurrentPage == 0) { Show_SWOT1_Form_In_Panel(); }
                if (CurrentMenu == 5 && CurrentPage == 1) { Show_SWOT1_Form_In_Panel(); }
            }
        }
        #endregion
        #region ButtonMennuClick
        private void bunifuFlatButton1_Click(object sender, EventArgs e) // 경쟁무기체계분석
        {
            CurrentMenu = 0;
            CurrentPage = 0;
            UpdatePagesInfo();

            // 1page를 봄
            Show_Competition1_Form_In_Panel();
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e) // 유사무기체계분석
        {
            CurrentMenu = 1;
            CurrentPage = 0;
            UpdatePagesInfo();

            Show_Similarity1_Form_In_Panel();
        }
        private void bunifuFlatButton2_Click(object sender, EventArgs e) // 대체무기체계 분석
        {
            CurrentMenu = 2;
            CurrentPage = 0;
            UpdatePagesInfo();

            Show_Substitute1_Form_In_Panel();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e) // 원천기술 분석
        {
            CurrentMenu = 3;
            CurrentPage = 0;

            UpdatePagesInfo();
            Show_OriginalTechnology1_Form_In_Panel();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e) // 수출유망 국가분석
        {
            CurrentMenu = 4;
            CurrentPage = 0;
            UpdatePagesInfo();

            // 1page를 봄
            Show_PromisingNation1_Form_In_Panel();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e) // 시장진입 가능성 분석
        {
            CurrentMenu = 5;
            CurrentPage = 0;
            UpdatePagesInfo();

            Show_SWOT1_Form_In_Panel();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e) //레포트 생성
        {
            CurrentMenu = 6;
            CurrentPage = 0;
            UpdatePagesInfo();
        }
       

         #endregion
    }
}
