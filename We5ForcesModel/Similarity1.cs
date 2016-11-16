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
    public partial class Similarity1 : Form
    {
        public static string[][] Spec = new string[6][];
        public static string[] SimilarData;
        public static double[] Prices;

        public Similarity1()
        {
            InitializeComponent();
        }
        private void StandardCompetition()
        {
            string[] Standard_Competition = new string[]
                         {" - 경쟁무기체계와 동일한 성능을 지니는 무기체계"};

            metroGrid1.Rows.Clear();
            metroGrid1.RowHeadersVisible = false;
            metroGrid1.ColumnCount = 1;

            metroGrid1.Columns[0].Name = "유사무기체계 선정 기준";
            metroGrid1.Columns[0].Width = (int)(metroGrid1.Width * 0.99);

            metroGrid1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 56, 100);

            metroGrid1.Rows.Add(Standard_Competition[0]);

            metroGrid1.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            metroGrid1.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);

            metroGrid1.CurrentCell = null;
        }
        private void dtgCompetition()
        {
            int X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            textBox1.Location = new Point(metroGrid1.Location.X, X + metroGrid1.Location.Y + 15);
            textBox1.Size = metroGrid2.Size;
            metroGrid2.Location = new Point(textBox1.Location.X, textBox1.Location.Y + textBox1.Size.Height + 1);
            metroGrid2.Rows.Clear();

            metroGrid2.RowHeadersVisible = false;
            metroGrid2.ColumnHeadersVisible = false;
            metroGrid2.ColumnCount = 1;

            metroGrid2.Rows.Add("구분");
            // 구분 색깔
            for (int i = 0; i < 1; i++)
            {
                metroGrid2.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                metroGrid2.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(91, 155, 213);
                metroGrid2.Rows[i].Cells[0].Style.ForeColor = Color.White;
            }
            for (int i = 0; i < mainFrm.selectCompetitionAndSimilarity.Length; i++)
            {
                if (mainFrm.selectCompetitionAndSimilarity[i] == true)
                {
                    metroGrid2.Rows.Add(mainFrm.CompetitionData[0, i]);
                    metroGrid2.Rows[metroGrid2.RowCount - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    metroGrid2.Rows[metroGrid2.RowCount - 1].Cells[0].Style.BackColor = Color.FromArgb(222, 235, 247);
                }
            }
            
            // 유사무기에 뭐가 있는지 추출해옴
            SimilarData = (string[])mainFrm.SimilarityWeapon.ToArray();
            /*
            SimilarData = new string[mainFrm.Similar.GetLength(0)];
            for (int i = 1; i < mainFrm.Similar.GetLength(0); i++)
            {
                SimilarData[i - 1] = mainFrm.Similar[i, 3].ToString();
            }
            // 중복을 제거하고 난 뒤, Null 값도 제거하고
            SimilarData = GetDistinctValues<string>(SimilarData);
            SimilarData = SimilarData.Where(condition => condition != null).ToArray();
            */
            // 경쟁무기 개수만큼 열을 추가
            for (int i = 0; i < SimilarData.Length; i++)
            {
                metroGrid2.Columns.Add("", "");
                metroGrid2.Rows[0].Cells[i + 1].Style.BackColor = Color.FromArgb(218, 83, 44);
                metroGrid2.Rows[0].Cells[i + 1].Style.ForeColor = Color.White;
                metroGrid2.Rows[0].DefaultCellStyle.Font = new Font("나눔고딕", 9, FontStyle.Bold);

                // M151을 기준으로 데이터들을 탐색함
                var IndexOf2DArray = ExtensionMethods.CoordinatesOf(mainFrm.CompetitionData, SimilarData[i]);


                int count = 1;
                metroGrid2.Rows[0].Cells[i + 1].Value = SimilarData[i]; // 무기명
                for (int j = 0; j < mainFrm.selectCompetitionAndSimilarity.Length; j++)
                {
                    if (mainFrm.selectCompetitionAndSimilarity[j] == true)
                    {
                        metroGrid2.Rows[count].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, j];
                        count++;
                    }
                }
                /*
                // ★나중에 for문하나로 바꿔야함. 범용성
                metroGrid2.Rows[0].Cells[i + 1].Value = SimilarData[i]; // 무기명
                metroGrid2.Rows[1].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 6];   // 개발국가
                metroGrid2.Rows[2].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 5];   // 제작사
                metroGrid2.Rows[8].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 17];   // 선회
                metroGrid2.Rows[9].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 18];   // 고저
                metroGrid2.Rows[10].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 26];   // 구동속도
                metroGrid2.Rows[11].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 39];   // 안정화 정확도
                metroGrid2.Rows[12].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 38];   // 조준정확도
                metroGrid2.Rows[13].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 7];   // 단가
                */
                for (int j = 1; j < metroGrid2.RowCount; j++)
                {
                    metroGrid2.Rows[j].Cells[i + 1].Style.BackColor = Color.White;
                }
            }

            // Design
            metroGrid2.DefaultCellStyle.BackColor = Color.White;

            // 국내무기체계 필요제원
            metroGrid2.Columns.Add("", "");


            metroGrid2.Rows[0].Cells[metroGrid2.ColumnCount - 1].Value = "국내무기체계";
            metroGrid2.Rows[0].Cells[metroGrid2.ColumnCount - 1].Style.BackColor = Color.FromArgb(0, 176, 80);
            metroGrid2.Rows[0].Cells[metroGrid2.ColumnCount - 1].Style.ForeColor = Color.White;
            for (int j = 1; j < metroGrid2.RowCount; j++)
            {
                metroGrid2.Rows[j].Cells[metroGrid2.ColumnCount - 1].Style.BackColor = Color.White;
            }
            this.metroGrid2.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            for (int i = 0; i < metroGrid2.ColumnCount; i++)
            {
                metroGrid2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            textBox2.Size = metroGrid2.Rows[0].Cells[metroGrid2.ColumnCount - 1].Size;

            int txtBoxWidth = 0;
            for (int i = 0; i < SimilarData.Length; i++) txtBoxWidth += metroGrid2.Columns[i + 1].Width;
            textBox1.Size = new Size(txtBoxWidth, metroGrid2.Size.Height);
            textBox1.Location = new Point(textBox1.Location.X + metroGrid2.Columns[0].Width, textBox1.Location.Y);

            X = metroGrid2.Columns.Cast<DataGridViewColumn>().Sum(r => r.Width);
            textBox2.Location = new Point(metroGrid2.Location.X + X - metroGrid2.Columns[metroGrid2.ColumnCount - 1].Width + 2, textBox1.Location.Y);

            txtBoxWidth = 0;

            // 단가는 Table의 가장 아래로 들어감
            int PriceIndex = -1;
            for (int i = 0; i < metroGrid2.RowCount; i++)
            {
                if (metroGrid2.Rows[i].Cells[0].Value.ToString().IndexOf("Prices") != -1 ||
                    metroGrid2.Rows[i].Cells[0].Value.ToString().IndexOf("단가") != -1 ||
                    metroGrid2.Rows[i].Cells[0].Value.ToString().IndexOf("가격") != -1) { PriceIndex = i; break; }
            }
            if (PriceIndex != -1)
            {
                int _iCurrentRow = PriceIndex;
                DataGridViewRow _dgvRow = metroGrid2.Rows[_iCurrentRow];
                metroGrid2.Rows.RemoveAt(_iCurrentRow);
                metroGrid2.Rows.Insert(metroGrid2.RowCount, _dgvRow);
            }

            for (int i = 1; i < metroGrid2.RowCount; i++)
            {
                if (mainFrm.DomesticSpec[i - 1] != null)
                {
                    metroGrid2.Rows[i].Cells[metroGrid2.ColumnCount - 1].Value = mainFrm.DomesticSpec[i - 1];
                }
            }
       //     metroGrid2.Rows[1].Cells[metroGrid2.ColumnCount - 1].Value = "대한민국";

            metroGrid2.CurrentCell = null;


        }
        private void InitializeSpec()
        {
            if (mainFrm.DomesticSpec == null) { mainFrm.DomesticSpec = new string[mainFrm.selectCompetitionAndSimilarity.Where(c => c).Count()]; }
        }
        public DataGridView Dgv { get; set; }
        private void Conclusion()
        {
            int X = metroGrid2.ColumnHeadersHeight + metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            LblConclusion.Location = new Point(metroGrid2.Location.X, X + metroGrid2.Location.Y + 30);
            ConclusionBox.Location = new Point(LblConclusion.Location.X, LblConclusion.Location.Y + 30);
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

        private void Similarity1_Load(object sender, EventArgs e)
        {
            // null 검사
            if (mainFrm.SimilarityWeapon == null || mainFrm.SimilarityWeapon.Count == 0)
            {
                noSimilarity.Visible = true;
                bunifuImageButton1.Visible = true;
                bunifuCustomLabel2.Visible = true;

                bunifuCustomLabel1.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                LblConclusion.Visible = false;
                ConclusionBox.Visible = false;
            }
            else
            {
                noSimilarity.Visible = false;
                bunifuImageButton1.Visible = false;
                bunifuCustomLabel2.Visible = false;

                bunifuCustomLabel1.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                LblConclusion.Visible = true;
                ConclusionBox.Visible = true;

                StandardCompetition();
                InitializeSpec();
                dtgCompetition();
                Conclusion();

                saveData();
            }
        }
        private void saveData()
        {
            int PricesIndex = -1;
            for (int i = 0; i < metroGrid2.RowCount; i++)
            {
                if (metroGrid2.Rows[i].Cells[0].Value.ToString().IndexOf("Prices") != -1 ||
                    metroGrid2.Rows[i].Cells[0].Value.ToString().IndexOf("단가") != -1 ||
                    metroGrid2.Rows[i].Cells[0].Value.ToString().IndexOf("가격") != -1)
                {
                    PricesIndex = i;
                }
            }
            if (PricesIndex != -1)
            {
                Prices = new double[SimilarData.Length];
                for (int i = 0; i < Prices.Length; i++)
                {
                    Prices[i] = Convert.ToDouble(metroGrid2.Rows[PricesIndex].Cells[i + 1].Value);
                }
            }
            // row 값을 저장함
            mainFrm.SelectedSimilarityMenu = new string[metroGrid2.RowCount - 1]; // 단가는 제외함
            for (int i = 1; i < metroGrid2.RowCount - 1; i++)
            {
                mainFrm.SelectedSimilarityMenu[i - 1] = metroGrid2.Rows[i].Cells[0].Value.ToString();
            }
        }

        private void metroGrid2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Competition2.cntSimilarityWeapon + 1 && e.RowIndex > 0)
            {
                if (metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    mainFrm.DomesticSpec[e.RowIndex - 1] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            // 경쟁무기에 뭐가 있는지 추출해옴
            Competition2.RawWeaponDBData = new string[mainFrm.CompetitionData.GetLength(0)];
            for (int i = 1; i < mainFrm.CompetitionData.GetLength(0); i++)
            {
                Competition2.RawWeaponDBData[i - 1] = mainFrm.CompetitionData[i, 3].ToString();
            }
            // 중복을 제거하고 난 뒤, Null 값도 제거하고
            Competition2.RawWeaponDBData = GetDistinctValues<string>(Competition2.RawWeaponDBData);
            Competition2.RawWeaponDBData = Competition2.RawWeaponDBData.Where(condition => condition != null).ToArray();

            Competition2.isSimilarWeapon = new bool[Competition2.RawWeaponDBData.Length];

            selectCompetition frmSelectedCompetition = new selectCompetition();
            frmSelectedCompetition.ShowDialog();

            // 최초에 대체무기체계의 메뉴를 선정함
            if (mainFrm.CompetitionData != null && mainFrm.CompetitionData.Length != 0)
            {
                if (mainFrm.selectCompetitionAndSimilarity.Where(c => c).Count() < 5)
                {
                    SelectCompetitionAndSimilarMenu frmSelectSubstitutionMenu = new SelectCompetitionAndSimilarMenu();
                    frmSelectSubstitutionMenu.ShowDialog();
                }
            }
            // 향후에는 선택한 스펙만 바꿀수 있또록 수정하여야 함
            // 일단은 이대로
           
            if (mainFrm.DomesticSpec == null) { mainFrm.DomesticSpec = new string[mainFrm.selectCompetitionAndSimilarity.Where(c => c).Count()]; }
            Similarity1_Load(sender, e);
        }

        private void metroGrid2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (metroGrid2.CurrentCell.ColumnIndex == metroGrid2.ColumnCount - 1 &&
                metroGrid2.CurrentCell.RowIndex == metroGrid2.RowCount - 1)
            {
                e.Control.KeyPress += new KeyPressEventHandler(metroGrid2_KeyPress);
            }
        }

        private void metroGrid2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
