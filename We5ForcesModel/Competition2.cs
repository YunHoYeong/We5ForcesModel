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
        public static string[] CompetitionWeapon;
        public static double[] Prices;

            
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
            for (int i = 0; i < Spec[mainFrm.CurrentWeapon].Count(); i++)
            {
                metroGrid2.Rows.Add(Spec[mainFrm.CurrentWeapon][i]);
                metroGrid2.Rows[i + 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                metroGrid2.Rows[i + 1].Cells[0].Style.BackColor = Color.FromArgb(222, 235, 247);
            }
            #region 경쟁무기체계 DB에서 추가함

            // 경쟁무기체계를 추가함

            // 경쟁무기에 뭐가 있는지 추출해옴
            CompetitionWeapon = new string[mainFrm.CompetitionData.GetLength(0)];
            for (int i = 1; i < mainFrm.CompetitionData.GetLength(0); i++)
            {
                CompetitionWeapon[i - 1] = mainFrm.CompetitionData[i, 3].ToString();
            }
            // 중복을 제거하고 난 뒤, Null 값도 제거하고
            CompetitionWeapon = GetDistinctValues<string>(CompetitionWeapon);
            CompetitionWeapon = CompetitionWeapon.Where(condition => condition != null).ToArray();


            // 경쟁무기 개수만큼 열을 추가
            for (int i = 0; i < CompetitionWeapon.Length; i++)
            {
                metroGrid2.Columns.Add("", "");
                metroGrid2.Rows[0].Cells[i + 1].Style.BackColor = Color.FromArgb(32, 56, 100);
                metroGrid2.Rows[0].Cells[i + 1].Style.ForeColor = Color.White;
                metroGrid2.Rows[0].DefaultCellStyle.Font = new Font("나눔고딕", 9, FontStyle.Bold);
                
                // M151을 기준으로 데이터들을 탐색함
                var IndexOf2DArray = ExtensionMethods.CoordinatesOf(mainFrm.CompetitionData, CompetitionWeapon[i]);

                // ★나중에 for문하나로 바꿔야함. 범용성
                metroGrid2.Rows[0].Cells[i + 1].Value = CompetitionWeapon[i]; // 무기명
                metroGrid2.Rows[1].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 6];   // 개발국가
                metroGrid2.Rows[2].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 5];   // 제작사
                metroGrid2.Rows[8].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 17];   // 선회
                metroGrid2.Rows[9].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 18];   // 고저
                metroGrid2.Rows[10].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 26];   // 구동속도
                metroGrid2.Rows[11].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 39];   // 안정화 정확도
                metroGrid2.Rows[12].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 38];   // 조준정확도
                metroGrid2.Rows[13].Cells[i + 1].Value = mainFrm.CompetitionData[IndexOf2DArray.Item1, 7];   // 단가

                for(int j = 1; j < metroGrid2.RowCount; j++)
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

            metroGrid2.CurrentCell = null;
            
        }
        public DataGridView Dgv { get; set; }

        private void Conclusion()
        {
            int X = metroGrid2.ColumnHeadersHeight + metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            LblConclusion.Location = new Point(metroGrid2.Location.X, X + metroGrid2.Location.Y + 30);
            ConclusionBox.Location = new Point(LblConclusion.Location.X, LblConclusion.Location.Y + 30);
        }
        private void InitializeSpec()
        {
            Spec[0] = new string[] { "개발국가", "제작사", "탑재중량", "운용고도", "운용반경", "체공시간", "최대속도", "단가 ($M)" };
            Spec[1] = new string[] { "개발국가", "제작사", "길이", "중량", "페이로드", "최대속력", "항속거리", "지속시간", "단가 ($M)" };
            Spec[2] = new string[] { "개발국가", "제작사", "장착가능화기", "원격 및 수동사격", "표적정보\n디지털전시", "화기별 자동장전", "포탑구동\n안정화장치보유", "선회", "고저", "구동속도", "안정화 정확도", "조준 정확도", "단가 ($M)" };
            Spec[3] = new string[] { "개발국가", "제작사", "레이저 발진 파워", "유효사격 거리", "단가 ($M)" };
            Spec[4] = new string[] { "개발국가", "제작사", "중량", "직경", "길이", "속력", "항주거리", "운용수심", "추진방식", "유도방식", "호밍방식", "단가 ($M)" };
            Spec[5] = new string[] { "개발국가", "만재톤수", "건조년도(년)", "최대속력(Kts)", "항속거리(NM)", "진/회수방식", "단가 ($M)" };
        }
        private void Competition2_Load(object sender, EventArgs e)
        {
            metroGrid2.AutoGenerateColumns = false;
            InitializeSpec();
            StandardCompetition();
            dtgCompetition();
            Conclusion();

            saveData();
        }
        private void saveData()
        {
            Prices = new double[CompetitionWeapon.Length];
            for (int i = 0; i < Prices.Length; i++)
            {
                Prices[i] = Convert.ToDouble(metroGrid2.Rows[metroGrid2.RowCount - 1].Cells[i + 1].Value);
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
