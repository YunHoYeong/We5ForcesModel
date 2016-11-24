using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Data.OleDb;
using System.IO;
using LinqToExcel;
using System.Runtime.InteropServices;

namespace We5ForcesModel
{
    public partial class mainFrm : Form
    {
        //Create your private font collection object.
       public static PrivateFontCollection pfc = new PrivateFontCollection();

        public static int CurrentMenu = 0;
        public static int CurrentPage = 0;
        public static int CurrentWeapon = 2;
        public static int MaximumMenus = 7;
        public static int[] MaximumPages = new int[] { 3, 2, 2, 3, 3, 1, 1 };

        public static string[] DomesticSpec;
        public static string[] SubstituteDomesticSpec;

        // 각 페이지 기타 의견
        public static string ETC_Decision_1 = "";
        public static string ETC_Decision_2 = "";
        public static string ETC_Decision_3 = "";
        public static string ETC_Decision_4 = "";
        public static string ETC_Decision_5 = "";
        public static string ETC_Decision_6 = "";
        public static string ETC_Decision_7 = "";

        // 가성비를 구하기 위한 항목을 기술경쟁력 표에서 가져옴
        public static string[] SelectedCompetitionMenu;
        public static string[] SelectedSimilarityMenu;
        public static string[] SelectedSubstitutionMenu;

        // 가성비는 모든 데이터를 담는걸로..
        public static string[,] CostEffectiveCompetitionAndSimilar;
        public static string[,] CostEffectiveSubstitution;
        public static string[] SubstitutionDescrition;

        // 원천기술 내용

        public static int IndexCriticalTechonology;

        public static List<string> OrigrinalTechnology1 = new List<string>();
        public static List<string> OrigrinalTechnology2 = new List<string>();

        public static List<string> CriticalTechnology1 = new List<string>();
        public static List<string> CriticalTechnology2 = new List<string>();
        public static List<string> CriticalTechnology3 = new List<string>();
        public static List<string> CriticalTechnology4 = new List<string>();
        public static List<string> CriticalTechnology5 = new List<string>();
        public static List<string> CriticalTechnology6 = new List<string>();
        public static List<string> CriticalTechnology7 = new List<string>();
        public static List<string> CriticalTechnology8 = new List<string>();
        public static List<string> CriticalTechnology9 = new List<string>();

        //SWOT 분석
        public static string[] SWOT = new string[4];

        public mainFrm()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            InitCustomLabelFont();
        }
        // 경쟁/유사/무기체계 DB 데이터

        public static object[,] CompetitionData;
        public static object[,] Similar;
        public static object[,] Substitution;

        public static bool[] selectSubstitution;
        public static bool[] selectCompetitionAndSimilarity;

        public static List<string> CompetitionWeapon;
        public static List<string> SimilarityWeapon;
        public static List<string> SubstitutionWeapon;

        public static string[] FullSpecCompetitionAndSimilarity;
        public static string[] FullSpecSubstitution;

        // 원천기술 DB데이터
        //  public static object[,] OriginalTechnology;

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal) { this.WindowState = FormWindowState.Maximized; }
            else if (this.WindowState == FormWindowState.Maximized) { this.WindowState = FormWindowState.Normal; }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (SlideMenu.Width == 50)
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
                selectCompetitionAndSimilarity = new bool[ColumnCount];

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
                // 가성비 초기화
                // 중복을 제거하고 난 뒤, Null 값도 제거하고
                string[] CompetitionWeapon = new string[mainFrm.CompetitionData.GetLength(0)];
                for (int i = 1; i < mainFrm.CompetitionData.GetLength(0); i++)
                {
                    CompetitionWeapon[i - 1] = mainFrm.CompetitionData[i, 3].ToString();
                }
                CompetitionWeapon = GetDistinctValues<string>(CompetitionWeapon);
                CompetitionWeapon = CompetitionWeapon.Where(condition => condition != null).ToArray();

                int[][] cntCompetitionWeapon = new int[CompetitionWeapon.Length][];
                for (int i = 0; i < cntCompetitionWeapon.GetLength(0); i++)
                {
                    cntCompetitionWeapon[i] = new int[5];
                }
                // 가성비 초기화 (모든 무기 대비, 모든 사항)
                mainFrm.CostEffectiveCompetitionAndSimilar = new string[CompetitionWeapon.Length, mainFrm.CompetitionData.GetLength(1)];
                FullSpecCompetitionAndSimilarity = new string[mainFrm.CompetitionData.GetLength(1)];
                for (int i = 0; i < FullSpecCompetitionAndSimilarity.Length; i++)
                {
                    FullSpecCompetitionAndSimilarity[i] = mainFrm.CompetitionData[0, i].ToString();
                }
                // 

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
                selectSubstitution = new bool[ColumnCount];

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
                // 대체무기에 뭐가 있는지 추출해옴
                string[] SubstitutionData = new string[Substitution.GetLength(0)];
                for (int i = 1; i < mainFrm.Substitution.GetLength(0); i++)
                {
                    SubstitutionData[i - 1] = mainFrm.Substitution[i, 3].ToString();
                }
                // 중복을 제거하고 난 뒤, Null 값도 제거하고
                SubstitutionData = GetDistinctValues<string>(SubstitutionData);
                SubstitutionData = SubstitutionData.Where(condition => condition != null).ToArray();
                SubstitutionWeapon = SubstitutionData.ToList();

                mainFrm.CostEffectiveSubstitution = new string[SubstitutionData.Length, mainFrm.Substitution.GetLength(1)];

                FullSpecSubstitution = new string[Substitution.GetLength(1)];
                SubstitutionDescrition = new string[Substitution.GetLength(1)];

                for (int i = 0; i < FullSpecSubstitution.Length; i++)
                {
                    FullSpecSubstitution[i] = mainFrm.Substitution[0, i].ToString();
                }
            }
        }
        public T[] GetDistinctValues<T>(T[] array)
        {
            List<T> tmp = new List<T>();

            for (int i = 0; i < array.Length; i++)
            {
                if (tmp.Contains(array[i]))
                    continue;
                tmp.Add(array[i]);
            }
            return tmp.ToArray();
        }
        public static void InitCustomLabelFont()
        {

            //Select your font from the resources.
            //My font here is "Digireu.ttf"
            int fontLength = Properties.Resources.NanumGothic.Length;

            // create a buffer to read in to
            byte[] fontdata = Properties.Resources.NanumGothic;

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            // pass the font to the font collection
            pfc.AddMemoryFont(data, fontLength);

            // free up the unsafe memory
            Marshal.FreeCoTaskMem(data);
        }
        private void mainFrm_Load(object sender, EventArgs e)
        {
            PrivateFontCollection privateFonts = new PrivateFontCollection();            
            privateFonts.AddFontFile("NanumGothic.ttf");

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
            this.Cursor = Cursors.WaitCursor;
            // 최초에 대체무기체계의 메뉴를 선정함
            if (mainFrm.Substitution != null && mainFrm.Substitution.Length != 0)
            {
                if (mainFrm.selectSubstitution.Where(c => c).Count() < 5)
                {
                    SelectSubstitutionMenu frmSelectSubstitutionMenu = new SelectSubstitutionMenu();
                    frmSelectSubstitutionMenu.ShowDialog();
                }
            }
            this.Cursor = Cursors.Arrow;

            Substitute1 SubForm = new Substitute1();
            SubForm.TopLevel = false;
            SubForm.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Controls.Add(SubForm);
            SubForm.Show();
        }
        private void Show_Substitute2_Form_In_Panel()
        {
            panel3.Controls.Clear();
            Substitute3 SubForm = new Substitute3();
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
                if (CurrentMenu == 1 && CurrentPage == 1)
                {
                    if (mainFrm.SimilarityWeapon == null || mainFrm.SimilarityWeapon.Count == 0)
                    {
                        MessageBox.Show("유사무기체계를 선택해주세요.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CurrentPage--;
                    }
                    else { Show_Similarity2_Form_In_Panel(); }
                }
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
