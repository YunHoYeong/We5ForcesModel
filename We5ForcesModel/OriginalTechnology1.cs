using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace We5ForcesModel
{
    public partial class OriginalTechnology1 : Form
    {
        public OriginalTechnology1()
        {
            InitializeComponent();
        }
        // RCWS 기준
        public static string[][] OriginalTechnology = new string[6][];
        public static string[][][] SubTechnology = new string[6][][];
        public static string[][] DescriptionTechonology = new string[6][];
        public static bool[][] MainTechnology = new bool[6][];
        public static string[][][][] TechnologyLevel = new string[6][][][];

        private void Initialize()
        {
            OriginalTechnology[2] = new string[] { "영상기술", "사격통제기술", "구동/제어기술", "운용기술" };
            DescriptionTechonology[2] = new string[]
            {
                "- 주/야간 표적의 식별 탐지를 위한 기술\n- 주간관측장비, 열영상장비, 거리측정기(LRF), 하우징 조립체로 구성",
                "- K-4와 K-6 기관총을 장착하고, 제어장치로부터 제어입력을 통해 장전과 격발을 수행\n- 완충장비를 사용하여 사격시 충격을 완화시키는 기능",
                "- 구동장치는 구조물 조립체, 구동부 조립체, 슬립링 조립체로 구성\n- 제어장치는 구동장치를 제어하기 위한 안정화 제어기능과 외부를 측정하기 위한 자이로센서로 구성\n- 특히 기동 및 사격시 외부충격 및 진동에 대한 안정화 및 시스템 제어 성능이 요구",
                "- 영상장치로부터 획득한 영상을 전시하고, 사수가 시스템 운용 및 사격을 위한 조이스틱으로 구성"
            };
            MainTechnology[2] = new bool[] { false, false, true, false };
            SubTechnology[2] = new string[][]
             {
                 new string[] { "주간관측기술", "열영상 감지 기능", "거리측정기술", "내부 구성품 장착/보호기술" },
                 new string[] { "원격사격제어기술", "자동추적기술", "탄도보정기술", "표적전시기술" },
                 new string[] { "구동장치기술", "위치센서기술", "안정화제어기술", "자이로센서기술" },
                 new string[] { "전시기용 S/W 기술", "구동설계기술", "운용케이블/전원기술" }
             };
            TechnologyLevel[2] = new string[][][]
            {
                new string[][]
                {

                },
                new string[][]
                {

                },
                new string[][]
                {
                    new string[] {"93","76","보통","높음","보통","낮음","국방>민간","높음"},
                    new string[] {"93","85","보통","낮음","보통","낮음","국방≒민간","보통"},
                    new string[] {"92","75","높음","높음","보통","낮음","국방>민간","높음"},
                    new string[] {"92","81","보통","낮음","보통","낮음","국방≒민간","보통"},
                },
                new string[][]
                {

                }
            };

        }
        private void OriginalTechIntroduction()
        {
            
            //  lbl_Introduction.Text = Introduction[2];
            metroGrid1.Rows.Clear();
            metroGrid1.RowHeadersVisible = false;
            metroGrid1.ColumnCount = 2;


            metroGrid1.Columns[0].Name = "기술명";
            metroGrid1.Columns[1].Name = "기술내용";

            // Clumn Header 변경
            // 개요
            metroGrid1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 56, 100);
            metroGrid1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // 무기체계명 
            DataGridViewColumn dataGridViewColumn = metroGrid1.Columns[0];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.FromArgb(91, 155, 213);
            dataGridViewColumn.HeaderCell.Style.ForeColor = Color.White;


            for (int i = 0; i < metroGrid1.Columns.Count; i++)
            {
                metroGrid1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            
            for(int i = 0; i < mainFrm.OrigrinalTechnology1.Count(); i++)
            {
                metroGrid1.Rows.Add(" " + mainFrm.OrigrinalTechnology1[i], mainFrm.OrigrinalTechnology2[i]);
                if (i == mainFrm.IndexCriticalTechonology) { metroGrid1.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(255, 192, 0); }
            }
            
            /*
            for(int i = 0; i < OriginalTechnology[mainFrm.CurrentWeapon].Length; i++ )
            {
                metroGrid1.Rows.Add(" " + OriginalTechnology[mainFrm.CurrentWeapon][i] + " " , DescriptionTechonology[mainFrm.CurrentWeapon][i]);
                if(MainTechnology[mainFrm.CurrentWeapon][i] == true) { metroGrid1.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(255, 192, 0); }
            }
            */
            if(mainFrm.OrigrinalTechnology1.Count() == 0) { metroGrid1.Rows.Add(); }
            for(int i = 0; i < metroGrid1.RowCount; i++)
            {
                metroGrid1.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            // 무기체계 작성 Cell 스타일
            metroGrid1.Rows[0].Cells[0].Style.BackColor = Color.FromArgb(222, 235, 247);
            metroGrid1.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);

            metroGrid1.Rows[0].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            metroGrid1.CurrentCell = null;
            for (int i = 1; i < metroGrid1.RowCount; i++)
            {
                for (int j = 0; j < metroGrid1.ColumnCount; j++)
                {
                    metroGrid1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
                metroGrid1.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid1.Rows[i].Cells[0].Style.ForeColor = System.Drawing.Color.Black;
            }

            for (int i = 0; i < mainFrm.OrigrinalTechnology1.Count(); i++)
            {
                if (i == mainFrm.IndexCriticalTechonology) { metroGrid1.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(255, 192, 0); }
            }
            foreach (DataGridViewColumn column in metroGrid1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            metroGrid1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[0].Width = (int)(metroGrid1.Width * 0.15);
            metroGrid1.Columns[1].Width = (int)(metroGrid1.Width * 0.84);
            //   metroGrid1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;

            int X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid1.Height = X + 10;


            bunifuImageButton1.Location = new Point(bunifuImageButton1.Location.X, metroGrid1.Height + metroGrid1.Location.Y + 10);
            bunifuImageButton2.Location = new Point(bunifuImageButton2.Location.X, metroGrid1.Height + metroGrid1.Location.Y + 10);

            lbl2.Location = new Point(lbl2.Location.X, bunifuImageButton2.Location.Y + 50);
            metroGrid2.Location = new Point(metroGrid2.Location.X, lbl2.Location.Y + 30);

            bunifuImageButton3.Location = new Point(bunifuImageButton3.Location.X, metroGrid2.Height + metroGrid2.Location.Y + 10);
            bunifuImageButton4.Location = new Point(bunifuImageButton4.Location.X, metroGrid2.Height + metroGrid2.Location.Y + 10);


            this.metroGrid1.DefaultCellStyle.Font = new Font("나눔고딕", 10);
        }
        private void DomesticTechnology()
        {
            //  lbl_Introduction.Text = Introduction[2];
            metroGrid2.Rows.Clear();
            metroGrid2.RowHeadersVisible = false;
            metroGrid2.ColumnCount = 9;

            metroGrid2.Columns[0].Name = "구 분";
            metroGrid2.Columns[1].Name = "선진국\n기술수준";
            metroGrid2.Columns[2].Name = "국내\n기술수준";
            metroGrid2.Columns[3].Name = "기술적\n파급효과";
            metroGrid2.Columns[4].Name = "경제적\n파급효과";
            metroGrid2.Columns[5].Name = "보호등급";
            metroGrid2.Columns[6].Name = "기술도입\n가능성";
            metroGrid2.Columns[7].Name = "민군우위\n기술";
            metroGrid2.Columns[8].Name = "난이도";

            // Clumn Header 변경
            // 개요
            metroGrid2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 56, 100);
            metroGrid2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // 무기체계명 
            /*
            DataGridViewColumn dataGridViewColumn = metroGrid2.Columns[0];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.FromArgb(91, 155, 213);
            dataGridViewColumn.HeaderCell.Style.ForeColor = Color.White;
            */

            for (int i = 0; i < MainTechnology[mainFrm.CurrentWeapon].Length; i++)
            {
                if(MainTechnology[mainFrm.CurrentWeapon][i] == true)
                {
                    for (int j = 0; j < SubTechnology[mainFrm.CurrentWeapon][i].Length; j++)
                    {
                        metroGrid2.Rows.Add(SubTechnology[mainFrm.CurrentWeapon][i][j]);
                        for(int k = 0; k < TechnologyLevel[mainFrm.CurrentWeapon][i][j].Length; k++)
                        {
                            if(k == 0 || k == 1)
                            {
                                metroGrid2.Rows[j].Cells[k + 1].Value = TechnologyLevel[mainFrm.CurrentWeapon][i][j][k] + "%";
                            }
                            else { metroGrid2.Rows[j].Cells[k + 1].Value = TechnologyLevel[mainFrm.CurrentWeapon][i][j][k]; }
                        }
                    }
                }
            }
            metroGrid2.CurrentCell = null;
            for (int i = 0; i < metroGrid2.RowCount; i++)
            {
                for (int j = 1; j < metroGrid2.ColumnCount; j++)
                {
                    metroGrid2.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
                metroGrid2.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid2.Rows[i].Cells[0].Style.ForeColor = System.Drawing.Color.Black;
            }
            foreach (DataGridViewColumn column in metroGrid2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int i = 1; i < metroGrid2.ColumnCount; i++)
            {
                metroGrid2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                metroGrid2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            for (int i = 0; i < metroGrid2.ColumnCount; i++) { metroGrid2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
                
            
            foreach (DataGridViewRow row in metroGrid2.Rows)
            {
                row.Height = (int)(row.Height * 1.5);
            }

            int X = metroGrid2.ColumnHeadersHeight + metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid2.Height = X + 10;

            X = metroGrid2.ColumnHeadersHeight + metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            this.metroGrid2.DefaultCellStyle.Font = new Font("나눔고딕", 10);
        }
        private void OriginalTechnology1_Load(object sender, EventArgs e)
        {
            InitializeList();
            Initialize();
            OriginalTechIntroduction();
         //   DomesticTechnology();
            dtgCompetition();
        }
        private void InitializeList()
        {
            if(mainFrm.OrigrinalTechnology1.Count() == 0)
            {
                // RCWS 임시
                string[] tempOriginalTEchonology1 = new string[] { "영상기술", "사격통제기술", "구동/제어기술", "운용기술" };
                mainFrm.OrigrinalTechnology1.AddRange(tempOriginalTEchonology1);

                string[] tempOriginalTEchonology2 = new string[] {
    "- 주/야간 표적의 식별 탐지를 위한 기술\n- 주간관측장비, 열영상장비, 거리측정기(LRF), 하우징 조립체로 구성",
    "- K-4와 K-6 기관총을 장착하고, 제어장치로부터 제어입력을 통해 장전과 격발을 수행\n- 완충장비를 사용하여 사격시 충격을 완화시키는 기능",
    "- 구동장치는 구조물 조립체, 구동부 조립체, 슬립링 조립체로 구성\n- 제어장치는 구동장치를 제어하기 위한 안정화 제어기능과 외부를 측정하기 위한 자이로센서로 구성\n- 특히 기동 및 사격시 외부충격 및 진동에 대한 안정화 및 시스템 제어 성능이 요구",
    "- 영상장치로부터 획득한 영상을 전시하고, 사수가 시스템 운용 및 사격을 위한 조이스틱으로 구성" };
                mainFrm.OrigrinalTechnology2.AddRange(tempOriginalTEchonology2);

                string[] CriticalTechonology = new string[] { "구동장치기술", "위치센서기술", "안정화제어기술", "자이로센서기술" };
                mainFrm.CriticalTechnology1.AddRange(CriticalTechonology);

                string[] CriticalTechnology2 = new string[] { "93", "93", "92", "92" };
                mainFrm.CriticalTechnology2.AddRange(CriticalTechnology2);

                string[] CriticalTechnology3 = new string[] { "76", "85", "75", "81" };
                mainFrm.CriticalTechnology3.AddRange(CriticalTechnology3);

                string[] CriticalTechnology4 = new string[] { "보통", "보통", "높음", "보통" };
                mainFrm.CriticalTechnology4.AddRange(CriticalTechnology4);

                string[] CriticalTechnology5 = new string[] { "높음", "낮음", "높음", "낮음" };
                mainFrm.CriticalTechnology5.AddRange(CriticalTechnology5);

                string[] CriticalTechnology6 = new string[] { "보통", "보통", "보통", "보통" };
                mainFrm.CriticalTechnology6.AddRange(CriticalTechnology6);

                string[] CriticalTechnology7 = new string[] { "낮음", "낮음", "낮음", "낮음" };
                mainFrm.CriticalTechnology7.AddRange(CriticalTechnology7);

                string[] CriticalTechnology8 = new string[] { "국방>민간", "국방≒민간", "국방>민간", "국방≒민간" };
                mainFrm.CriticalTechnology8.AddRange(CriticalTechnology8);

                string[] CriticalTechnology9 = new string[] { "높음", "보통", "높음", "보통" };
                mainFrm.CriticalTechnology9.AddRange(CriticalTechnology9);
            }
        }
        private void dtgCompetition()
        {
            metroGrid2.EditMode = DataGridViewEditMode.EditOnEnter;
            //ComboBox 유형의 셀 만들고
            DataGridViewComboBoxColumn[] ComboBoxCell = new DataGridViewComboBoxColumn[5];
            DataGridViewComboBoxColumn ComboBoxCell2 = new DataGridViewComboBoxColumn();

            //      ComboBoxCell.Name = mainFrm.SelectedCompetitionMenu[i];
            for(int i = 0; i < ComboBoxCell.Count(); i++)
            {
                ComboBoxCell[i] = new DataGridViewComboBoxColumn();
                ComboBoxCell[i].Items.Add("매우 높음");
                ComboBoxCell[i].Items.Add("높음");
                ComboBoxCell[i].Items.Add("보통");
                ComboBoxCell[i].Items.Add("낮음");
                ComboBoxCell[i].Items.Add("매우 낮음");

                ComboBoxCell[i].DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                ComboBoxCell[i].FlatStyle = FlatStyle.Flat;
            }


            ComboBoxCell2.Items.Add("국방≫민간");
            ComboBoxCell2.Items.Add("국방>민간");
            ComboBoxCell2.Items.Add("국방≒민간");
            ComboBoxCell2.Items.Add("국방<민간");
            ComboBoxCell2.Items.Add("국방≪민간");
            
            ComboBoxCell2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            ComboBoxCell2.FlatStyle = FlatStyle.Flat;


            metroGrid2.Rows.Clear();

            metroGrid2.RowHeadersVisible = false;
            metroGrid2.ColumnHeadersVisible = true;
            metroGrid2.ColumnCount = 3;

            metroGrid2.Columns[0].Name = "구 분";
            metroGrid2.Columns[1].Name = "선진국\n기술수준";
            metroGrid2.Columns[2].Name = "국내\n기술수준";
            

            metroGrid2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 56, 100);
            metroGrid2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            DataGridViewColumn dataGridViewColumn = metroGrid2.Columns[0];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.FromArgb(91, 155, 213);
            dataGridViewColumn.HeaderCell.Style.ForeColor = Color.White;


            metroGrid2.Columns.Add(ComboBoxCell[0]);
            metroGrid2.Columns.Add(ComboBoxCell[1]);
            metroGrid2.Columns.Add(ComboBoxCell[2]);
            metroGrid2.Columns.Add(ComboBoxCell[3]);
            metroGrid2.Columns.Add(ComboBoxCell2);
            metroGrid2.Columns.Add(ComboBoxCell[4]);


            metroGrid2.Columns[3].Name = "기술적\n파급효과";
            metroGrid2.Columns[4].Name = "경제적\n파급효과";
            metroGrid2.Columns[5].Name = "보호등급";
            metroGrid2.Columns[6].Name = "기술도입\n가능성";
            metroGrid2.Columns[7].Name = "민군우위\n기술";
            metroGrid2.Columns[8].Name = "난이도";


            this.metroGrid2.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            for (int i = 0; i < metroGrid2.RowCount; i++)
            {
                metroGrid2.Rows[i].Cells[0].Style.WrapMode = DataGridViewTriState.False;
            }
            metroGrid2.Columns[0].Width = 120;

            for (int i = 1; i < metroGrid2.ColumnCount; i++)
            {
                metroGrid2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            foreach (DataGridViewColumn column in metroGrid2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            }
            if (mainFrm.CriticalTechnology1.Count() == 0)
            {
                metroGrid2.Rows.Add();
                metroGrid2.Rows[0].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
            }
            else
            {
                for (int i = 0; i < mainFrm.CriticalTechnology1.Count(); i++)
                {
                    metroGrid2.Rows.Add(mainFrm.CriticalTechnology1[i],
                                        mainFrm.CriticalTechnology2[i],
                                        mainFrm.CriticalTechnology3[i],
                                        mainFrm.CriticalTechnology4[i],
                                        mainFrm.CriticalTechnology5[i],
                                        mainFrm.CriticalTechnology6[i],
                                        mainFrm.CriticalTechnology7[i],
                                        mainFrm.CriticalTechnology8[i],
                                        mainFrm.CriticalTechnology9[i]);

                    metroGrid2.Rows[metroGrid2.RowCount - 1].DefaultCellStyle.BackColor = Color.White;
                    metroGrid2.Rows[metroGrid2.RowCount - 1].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                }
            }
            foreach (DataGridViewRow row in metroGrid2.Rows)
            {
                row.Height = (int)(row.Height * 1.2);
            }

            int X = metroGrid2.ColumnHeadersHeight + metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid2.Height = X + 10;

            bunifuImageButton3.Location = new Point(bunifuImageButton2.Location.X, metroGrid2.Height + metroGrid2.Location.Y + 10);
            bunifuImageButton4.Location = new Point(bunifuImageButton1.Location.X, metroGrid2.Height + metroGrid2.Location.Y + 10);

            
            metroGrid2.CurrentCell = null;
        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {
            metroGrid1.ClearSelection();
        }

        private void metroGrid2_SelectionChanged(object sender, EventArgs e)
        {
            metroGrid2.ClearSelection();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            bool isContentsFull = false;
            for(int i = 0; i < metroGrid1.ColumnCount; i++)
            {
                if (metroGrid1.Rows[metroGrid1.RowCount - 1].Cells[i].Value != null && metroGrid1.Rows[metroGrid1.RowCount - 1].Cells[i].Value.ToString() != "") isContentsFull = true;
            }
            if(isContentsFull)
            {
                metroGrid1.Rows.Add();

                metroGrid1.Rows[metroGrid1.RowCount - 1].DefaultCellStyle.BackColor = Color.White;
                metroGrid1.Rows[metroGrid1.RowCount - 1].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid1.Rows[metroGrid1.RowCount - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.BottomLeft;

                int X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
                metroGrid1.Height = X + 10;

                bunifuImageButton1.Location = new Point(bunifuImageButton1.Location.X, metroGrid1.Height + metroGrid1.Location.Y + 10);
                bunifuImageButton2.Location = new Point(bunifuImageButton2.Location.X, metroGrid1.Height + metroGrid1.Location.Y + 10);

                lbl2.Location = new Point(lbl2.Location.X, bunifuImageButton2.Location.Y + 50);
                metroGrid2.Location = new Point(metroGrid2.Location.X, lbl2.Location.Y + 30);
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (metroGrid1.RowCount == 1)
            {
                // metroGrid1.Rows.Add();
                for(int j = 0; j < metroGrid1.ColumnCount; j++)
                {
                    metroGrid1.Rows[0].Cells[j].Value = ""; 
                }
                metroGrid1.Rows[metroGrid1.RowCount - 1].DefaultCellStyle.BackColor = Color.White;
                metroGrid1.Rows[metroGrid1.RowCount - 1].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid1.Rows[metroGrid1.RowCount - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.BottomLeft;

            }
            else
            {
                metroGrid1.Rows.Remove(metroGrid1.Rows[metroGrid1.RowCount - 1]);

                // 리스트 삭제
                mainFrm.OrigrinalTechnology1.RemoveAt(metroGrid1.RowCount);
                mainFrm.OrigrinalTechnology2.RemoveAt(metroGrid1.RowCount);

                int X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
                metroGrid1.Height = X + 25;

                bunifuImageButton1.Location = new Point(bunifuImageButton1.Location.X, metroGrid1.Height + metroGrid1.Location.Y + 10);
                bunifuImageButton2.Location = new Point(bunifuImageButton2.Location.X, metroGrid1.Height + metroGrid1.Location.Y + 10);

                lbl2.Location = new Point(lbl2.Location.X, bunifuImageButton2.Location.Y + 50);
                metroGrid2.Location = new Point(metroGrid2.Location.X, lbl2.Location.Y + 30);
            }
        }

        private void metroGrid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid1.Height = X + 10;

            bunifuImageButton1.Location = new Point(bunifuImageButton1.Location.X, metroGrid1.Height + metroGrid1.Location.Y + 10);
            bunifuImageButton2.Location = new Point(bunifuImageButton2.Location.X, metroGrid1.Height + metroGrid1.Location.Y + 10);

            lbl2.Location = new Point(lbl2.Location.X, bunifuImageButton2.Location.Y + 50);
            metroGrid2.Location = new Point(metroGrid2.Location.X, lbl2.Location.Y + 30);

            bunifuImageButton3.Location = new Point(bunifuImageButton2.Location.X, metroGrid2.Height + metroGrid2.Location.Y + 10);
            bunifuImageButton4.Location = new Point(bunifuImageButton1.Location.X, metroGrid2.Height + metroGrid2.Location.Y + 10);


            if (e.RowIndex >= mainFrm.OrigrinalTechnology1.Count())
            {
                mainFrm.OrigrinalTechnology1.Add("");
                mainFrm.OrigrinalTechnology2.Add("");
            }
            if (e.RowIndex < mainFrm.OrigrinalTechnology1.Count())
            {
                if(e.ColumnIndex == 0) { mainFrm.OrigrinalTechnology1[e.RowIndex] = metroGrid1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); }
                if (e.ColumnIndex == 1) { mainFrm.OrigrinalTechnology2[e.RowIndex] = metroGrid1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); }
            }

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if (metroGrid2.RowCount == 1)
            { 
                for (int j = 0; j < metroGrid2.ColumnCount; j++)
                {
                    metroGrid2.Rows[0].Cells[j].Value = "";
                }
                metroGrid2.Rows[metroGrid2.RowCount - 1].DefaultCellStyle.BackColor = Color.White;
                metroGrid2.Rows[metroGrid2.RowCount - 1].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid2.Rows[metroGrid2.RowCount - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.BottomLeft;
            }
            else
            {
                metroGrid2.Rows.Remove(metroGrid2.Rows[metroGrid2.RowCount - 1]);

                // 리스트 삭제
                mainFrm.CriticalTechnology1.RemoveAt(metroGrid2.RowCount);
                mainFrm.CriticalTechnology2.RemoveAt(metroGrid2.RowCount);
                mainFrm.CriticalTechnology3.RemoveAt(metroGrid2.RowCount);
                mainFrm.CriticalTechnology4.RemoveAt(metroGrid2.RowCount);
                mainFrm.CriticalTechnology5.RemoveAt(metroGrid2.RowCount);
                mainFrm.CriticalTechnology6.RemoveAt(metroGrid2.RowCount);
                mainFrm.CriticalTechnology7.RemoveAt(metroGrid2.RowCount);
                mainFrm.CriticalTechnology8.RemoveAt(metroGrid2.RowCount);
                mainFrm.CriticalTechnology9.RemoveAt(metroGrid2.RowCount);

                int X = metroGrid2.ColumnHeadersHeight + metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
                metroGrid2.Height = X + 10;

                bunifuImageButton3.Location = new Point(bunifuImageButton2.Location.X, metroGrid2.Height + metroGrid2.Location.Y + 10);
                bunifuImageButton4.Location = new Point(bunifuImageButton1.Location.X, metroGrid2.Height + metroGrid2.Location.Y + 10);
            }


        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            if(metroGrid2.RowCount == 4)
            {
                MessageBox.Show("최대 4개까지만 가능합니다.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool isContentsFull = false;
                for (int i = 0; i < metroGrid1.ColumnCount; i++)
                {
                    if (metroGrid1.Rows[metroGrid1.RowCount - 1].Cells[i].Value != null && metroGrid1.Rows[metroGrid1.RowCount - 1].Cells[i].Value.ToString() != "") isContentsFull = true;
                }
                if (isContentsFull)
                {
                    metroGrid2.Rows.Add();

                    metroGrid2.Rows[metroGrid2.RowCount - 1].DefaultCellStyle.BackColor = Color.White;
                    metroGrid2.Rows[metroGrid2.RowCount - 1].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                    metroGrid2.Rows[metroGrid2.RowCount - 1].Cells[1].Style.Alignment = DataGridViewContentAlignment.BottomLeft;

                    int X = metroGrid2.ColumnHeadersHeight + metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
                    metroGrid2.Height = X + 10;

                    bunifuImageButton3.Location = new Point(bunifuImageButton2.Location.X, metroGrid2.Height + metroGrid2.Location.Y + 10);
                    bunifuImageButton4.Location = new Point(bunifuImageButton1.Location.X, metroGrid2.Height + metroGrid2.Location.Y + 10);

                    metroGrid2.Rows[metroGrid2.RowCount - 1].Height = (int)(metroGrid2.Rows[metroGrid2.RowCount - 1].Height * 1.2);
                }
            }
        }

        private void metroGrid2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= mainFrm.CriticalTechnology1.Count())
            {
                mainFrm.CriticalTechnology1.Add("");
                mainFrm.CriticalTechnology2.Add("");
                mainFrm.CriticalTechnology3.Add("");
                mainFrm.CriticalTechnology4.Add("");
                mainFrm.CriticalTechnology5.Add("");
                mainFrm.CriticalTechnology6.Add("");
                mainFrm.CriticalTechnology7.Add("");
                mainFrm.CriticalTechnology8.Add("");
                mainFrm.CriticalTechnology9.Add("");
            }
            if (e.RowIndex < mainFrm.CriticalTechnology1.Count())
            {
                if (e.ColumnIndex == 0) { mainFrm.CriticalTechnology1[e.RowIndex] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); }
                if (e.ColumnIndex == 1) { mainFrm.CriticalTechnology2[e.RowIndex] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); }
                if (e.ColumnIndex == 2) { mainFrm.CriticalTechnology3[e.RowIndex] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); }
                if (e.ColumnIndex == 3) { mainFrm.CriticalTechnology4[e.RowIndex] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); }
                if (e.ColumnIndex == 4) { mainFrm.CriticalTechnology5[e.RowIndex] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); }
                if (e.ColumnIndex == 5) { mainFrm.CriticalTechnology6[e.RowIndex] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); }
                if (e.ColumnIndex == 6) { mainFrm.CriticalTechnology7[e.RowIndex] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); }
                if (e.ColumnIndex == 7) { mainFrm.CriticalTechnology8[e.RowIndex] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); }
                if (e.ColumnIndex == 8) { mainFrm.CriticalTechnology9[e.RowIndex] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); }
            }
        }

        private void metroGrid2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        
        }
    }
}
