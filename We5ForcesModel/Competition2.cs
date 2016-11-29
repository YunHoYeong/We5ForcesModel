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
    
    public partial class Competition2 : Form
    {
        public Competition2()
        {
            InitializeComponent();
        }
        public static string[][] Spec = new string[6][];
        
        public static string[] RawWeaponDBData;
        public static double[] Prices;

        public static int cntCompetitionWeapon;
        public static int cntSimilarityWeapon;

        public static string[] CompetitionWeapon;

        public static bool[] isSimilarWeapon;
            
        private void StandardCompetition()
        {
            string[] Standard_Competition = new string[]
                         {" ① 복합화기원격사격통제체계가 탑재되는 플랫폼이 장갑차인가?"
                         +"\n ② 국내에서 개발 중인 복합화기원격사격통제체계와 비슷한 성능을 갖고 있는가"};

            metroGrid1.Rows.Clear();
            metroGrid1.RowHeadersVisible = false;
            metroGrid1.ColumnCount = 1;

            metroGrid1.Columns[0].Name = "경쟁무기체계 선정 기준";
            metroGrid1.Columns[0].Width = (int)(metroGrid1.Width * 0.99);

            metroGrid1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 56, 100);

            metroGrid1.Rows.Add(Standard_Competition[0]);

            metroGrid1.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            metroGrid1.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            
            metroGrid1.CurrentCell = null;
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

        private void CellValueBold()
        {
            /*
            List<int> DoubleDataIndex = new List<int>();

            for(int i = 1; i < metroGrid2.RowCount; i++)
            {
                string[] RowData = new string[metroGrid2.RowCount - 1];
                bool isAllRowDataDouble = true;
                for(int j = 1; j < metroGrid2.ColumnCount; j++)
                {
                    RowData[j - 1] = metroGrid2.Rows[i].Cells[j].Value != null ? metroGrid2.Rows[i].Cells[j].Value.ToString() : string.Empty;
                }
            }
            */
        }

        private void dtgCompetition()
        {
            int X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid1.Height = X;
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
            #region 경쟁무기체계 DB에서 추가함

            // 경쟁무기체계를 추가함
            // 경쟁무기에 뭐가 있는지 추출해옴
            CompetitionWeapon = (string[])mainFrm.CompetitionWeapon.ToArray();


            // 경쟁무기 개수만큼 열을 추가
            for (int i = 0; i < CompetitionWeapon.Length; i++)
            {
                metroGrid2.Columns.Add("", "");
                metroGrid2.Rows[0].Cells[i + 1].Style.BackColor = Color.FromArgb(32, 56, 100);
                metroGrid2.Rows[0].Cells[i + 1].Style.ForeColor = Color.White;
                metroGrid2.Rows[0].DefaultCellStyle.Font = new Font("나눔고딕", 9, FontStyle.Bold);
                
                // M151을 기준으로 데이터들을 탐색함
                var IndexOf2DArray = ExtensionMethods.CoordinatesOf(mainFrm.CompetitionData, CompetitionWeapon[i]);
                
                int count = 1;
                metroGrid2.Rows[0].Cells[i + 1].Value = CompetitionWeapon[i]; // 무기명
                for (int j = 0; j < mainFrm.selectCompetitionAndSimilarity.Length; j++)
                {
                    if (mainFrm.selectCompetitionAndSimilarity[j] == true)
                    {
                        metroGrid2.Rows[count].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, j];
                        count++;
                    }
                }
                for (int j = 1; j < metroGrid2.RowCount; j++)
                {
                    metroGrid2.Rows[j].Cells[i + 1].Style.BackColor = Color.White;
                }
            }
            #endregion

            // Design
            metroGrid2.DefaultCellStyle.BackColor = Color.White;
            
            // 국내무기체계 필요제원
            metroGrid2.Columns.Add("","");
            
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
            textBox1.Size = new Size(metroGrid2.Size.Width - textBox2.Size.Width , metroGrid2.Size.Height);
            textBox2.Location = new Point(textBox1.Location.X + textBox1.Size.Width - 1, textBox1.Location.Y);

            // 단가는 Table의 가장 아래로 들어감
            int PriceIndex = -1;
            for(int i = 0; i < metroGrid2.RowCount; i++)
            {
                if (metroGrid2.Rows[i].Cells[0].Value.ToString().IndexOf("Prices") != -1 ||
                    metroGrid2.Rows[i].Cells[0].Value.ToString().IndexOf("단가") != -1 ||
                    metroGrid2.Rows[i].Cells[0].Value.ToString().IndexOf("가격") != -1) { PriceIndex = i; break; }
            }
            if(PriceIndex != -1) // 단가 수정했으면, Domestic 다시 넣자.
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
            // metroGrid2.Rows[1].Cells[metroGrid2.ColumnCount - 1].Value = "대한민국";
            /*
            // 행 변경에 따른 fitting(국내 단가)
            for (int i = 1; i < metroGrid2.RowCount; i++)
            {
                if(metroGrid2.Rows[i].Cells[metroGrid2.ColumnCount - 1].Value != null)
                {
                    mainFrm.DomesticSpec[i - 1] = metroGrid2.Rows[i].Cells[metroGrid2.ColumnCount - 1].Value.ToString();
                }
            }
            for(int i = 0; i < mainFrm.DomesticSpec.Length; i++)
            {
                if(mainFrm.DomesticSpec[i] != null)
                {
                    metroGrid2.Rows[i + 1].Cells[metroGrid2.ColumnCount - 1].Value = mainFrm.DomesticSpec[i];
                }
            }*/

            metroGrid2.CurrentCell = null;
            
        }
        public DataGridView Dgv { get; set; }

        private void Conclusion()
        {
            int X = metroGrid2.ColumnHeadersHeight + metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid2.Height = X;
            LblConclusion.Location = new Point(metroGrid2.Location.X, X + metroGrid2.Location.Y + 30);
            ConclusionBox.Location = new Point(LblConclusion.Location.X, LblConclusion.Location.Y + 30);
        }
        private void InitializeSpec()
        {
            if(mainFrm.DomesticSpec == null) { mainFrm.DomesticSpec = new string[mainFrm.selectCompetitionAndSimilarity.Where(c => c).Count()]; }
        }
        private void SeperateCompetitionAndSimiliarity()
        {
            if (isSimilarWeapon == null)
            {
                // 경쟁무기에 뭐가 있는지 추출해옴
                RawWeaponDBData = new string[mainFrm.CompetitionData.GetLength(0)];
                for (int i = 1; i < mainFrm.CompetitionData.GetLength(0); i++)
                {
                    RawWeaponDBData[i - 1] = mainFrm.CompetitionData[i, 3].ToString();
                }
                // 중복을 제거하고 난 뒤, Null 값도 제거하고
                RawWeaponDBData = GetDistinctValues<string>(RawWeaponDBData);
                RawWeaponDBData = RawWeaponDBData.Where(condition => condition != null).ToArray();

                isSimilarWeapon = new bool[RawWeaponDBData.Length];

                Cursor = Cursors.WaitCursor;

                selectCompetition frmSelectedCompetition = new selectCompetition();
                frmSelectedCompetition.ShowDialog();
                
                Cursor = Cursors.Arrow;
            }
            // 최초에 대체무기체계의 메뉴를 선정함
            if (mainFrm.CompetitionData != null && mainFrm.CompetitionData.Length != 0)
            {
                if (mainFrm.selectCompetitionAndSimilarity.Where(c => c).Count() < 5)
                {

                    Cursor = Cursors.WaitCursor;

                    SelectCompetitionAndSimilarMenu frmSelectSubstitutionMenu = new SelectCompetitionAndSimilarMenu();
                    frmSelectSubstitutionMenu.ShowDialog();

                    Cursor = Cursors.Arrow;
                }
            }
        }
        private void Competition2_Load(object sender, EventArgs e)
        {
            // 1. 경쟁무기체계(중복제거)를 모두 불러옴
            SeperateCompetitionAndSimiliarity();
            metroGrid2.AutoGenerateColumns = false;

            bunifuCustomLabel1.Visible = true;
            textBox1.Visible = true;
            metroGrid1.Visible = true;
            metroGrid2.Visible = true;
            textBox2.Visible = true;
            LblConclusion.Visible = true;
            ConclusionBox.Visible = true;

            InitializeSpec();
            StandardCompetition();
            dtgCompetition();
            Conclusion();

            saveData();

            ConclusionBox.Text = mainFrm.ETC_Decision_1;
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
                Prices = new double[CompetitionWeapon.Length];
                for (int i = 0; i < Prices.Length; i++)
                {
                    Prices[i] = Convert.ToDouble(metroGrid2.Rows[PricesIndex].Cells[i + 1].Value);
                }
            }
            // row 값을 저장함
            mainFrm.SelectedCompetitionMenu = new string[metroGrid2.RowCount - 1]; // 단가는 제외함
            for (int i = 1; i < metroGrid2.RowCount - 1; i++)
            {
                mainFrm.SelectedCompetitionMenu[i - 1] = metroGrid2.Rows[i].Cells[0].Value.ToString();
            }
        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {
            metroGrid1.ClearSelection();
        }
        private void metroGrid2_SelectionChanged(object sender, EventArgs e)
        {
            if (metroGrid2.CurrentCell.ColumnIndex != metroGrid2.ColumnCount - 1)
            {
                metroGrid2.ClearSelection();
            }
        }

        private void metroGrid2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == Competition2.cntCompetitionWeapon + 1 && e.RowIndex > 0)
            {
                if(metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    mainFrm.DomesticSpec[e.RowIndex - 1] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
        }

        private void metroGrid2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            /*
            if(metroGrid2.CurrentCell.ColumnIndex == metroGrid2.ColumnCount &&
                metroGrid2.CurrentCell.RowIndex == metroGrid2.RowCount)
            {
                e.Control.KeyPress += new KeyPressEventHandler(metroGrid2_KeyPress);
            }
            */
        }

        private void metroGrid2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void ConclusionBox_TextChanged(object sender, EventArgs e)
        {
            mainFrm.ETC_Decision_1 = ConclusionBox.Text;
        }
    }
    public static class ExtensionMethods
    {
        public static Tuple<int, int> CoordinatesOf<T>(this T[,] matrix, T value)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y].Equals(value))
                        return Tuple.Create(x, y);
                }
            }

            return Tuple.Create(-1, -1);
        }
    }
}
