﻿using System;
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
    public partial class Substitute1 : Form
    {
        public Substitute1()
        {
            InitializeComponent();
        }
        //  public static bool[] isCompetition = new bool[]

        public static string[][] Spec = new string[6][];
        public static string[] SubstitutionData;
        public static double[] Prices;

        private void StandardCompetition()
        {
            string[] Standard_Competition = new string[]
                         {" - 상륙돌격장갑차가 아닌 소형전술차량을 플랫폼으로 하는 소형 복합화기원격사격통제체계를 선정"};

            metroGrid1.Rows.Clear();
            metroGrid1.RowHeadersVisible = false;
            metroGrid1.ColumnCount = 1;

            metroGrid1.Columns[0].Name = "대체무기체계 선정 기준";
            metroGrid1.Columns[0].Width = (int)(metroGrid1.Width * 0.99);
            metroGrid1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            metroGrid1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 56, 100);

            metroGrid1.Rows.Add(Standard_Competition[0]);

            metroGrid1.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            metroGrid1.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);

            /*
            metroGrid1.Rows.Add("전문가 인터뷰 내용");

            metroGrid1.Rows[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            metroGrid1.Rows[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 204);
            metroGrid1.Rows[1].DefaultCellStyle.ForeColor =  Color.FromArgb(0, 0, 0);

            metroGrid1.Rows.Add();

            metroGrid1.Rows[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            metroGrid1.Rows[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            */
            metroGrid1.CurrentCell = null;
        }
        private void dtgCompetition()
        {
            int X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);

            textBox1.Location = new Point(metroGrid1.Location.X, metroGrid1.Location.Y + X + 20);
            metroGrid2.Location = new Point(metroGrid1.Location.X, textBox1.Location.Y + textBox1.Height);
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
            for (int i = 0; i < mainFrm.selectSubstitution.Length ; i++)
            {
                if(mainFrm.selectSubstitution[i] == true)
                {
                    metroGrid2.Rows.Add(mainFrm.Substitution[0,i]);
                    metroGrid2.Rows[metroGrid2.RowCount - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    metroGrid2.Rows[metroGrid2.RowCount - 1].Cells[0].Style.BackColor = Color.FromArgb(222, 235, 247);
                }
            }

            #region 대체무기체계 DB에서 추가함

            // 경쟁무기체계를 추가함

            // 경쟁무기에 뭐가 있는지 추출해옴
            SubstitutionData = new string[mainFrm.Substitution.GetLength(0)];
            for (int i = 1; i < mainFrm.Substitution.GetLength(0); i++)
            {
                SubstitutionData[i - 1] = mainFrm.Substitution[i, 3].ToString();
            }
            // 중복을 제거하고 난 뒤, Null 값도 제거하고
            SubstitutionData = GetDistinctValues<string>(SubstitutionData);
            SubstitutionData = SubstitutionData.Where(condition => condition != null).ToArray();


            // 경쟁무기 개수만큼 열을 추가
            for (int i = 0; i < SubstitutionData.Length; i++)
            {
                metroGrid2.Columns.Add("", "");
                metroGrid2.Rows[0].Cells[i + 1].Style.BackColor = Color.FromArgb(227, 162, 26);
                metroGrid2.Rows[0].Cells[i + 1].Style.ForeColor = Color.White;
                metroGrid2.Rows[0].DefaultCellStyle.Font = new Font("나눔고딕", 9, FontStyle.Bold);

                // M151을 기준으로 데이터들을 탐색함
                var IndexOf2DArray = ExtensionMethods.CoordinatesOf(mainFrm.Substitution, SubstitutionData[i]);

                int count = 1;
                metroGrid2.Rows[0].Cells[i + 1].Value = SubstitutionData[i]; // 무기명
                for (int j = 0; j < mainFrm.selectSubstitution.Length; j++)
                {
                    
                    if (mainFrm.selectSubstitution[j] == true)
                    {
                        metroGrid2.Rows[count].Cells[i + 1].Value = mainFrm.Substitution[IndexOf2DArray.Item1, j];
                        count++;
                    }
                }
                /*
                // ★나중에 for문하나로 바꿔야함. 범용성
                metroGrid2.Rows[0].Cells[i + 1].Value = SubstitutionData[i]; // 무기명
                metroGrid2.Rows[1].Cells[i + 1].Value = mainFrm.Substitution[IndexOf2DArray.Item1, 6];   // 개발국가
                metroGrid2.Rows[2].Cells[i + 1].Value = mainFrm.Substitution[IndexOf2DArray.Item1, 5];   // 제작사
                metroGrid2.Rows[8].Cells[i + 1].Value = mainFrm.Substitution[IndexOf2DArray.Item1, 17];   // 선회
                metroGrid2.Rows[9].Cells[i + 1].Value = mainFrm.Substitution[IndexOf2DArray.Item1, 18];   // 고저
                metroGrid2.Rows[10].Cells[i + 1].Value = mainFrm.Substitution[IndexOf2DArray.Item1, 26];   // 구동속도
                metroGrid2.Rows[11].Cells[i + 1].Value = mainFrm.Substitution[IndexOf2DArray.Item1, 39];   // 안정화 정확도
                metroGrid2.Rows[12].Cells[i + 1].Value = mainFrm.Substitution[IndexOf2DArray.Item1, 38];   // 조준정확도
                metroGrid2.Rows[13].Cells[i + 1].Value = mainFrm.Substitution[IndexOf2DArray.Item1, 7];   // 단가
                */
                for (int j = 1; j < metroGrid2.RowCount; j++)
                {
                    metroGrid2.Rows[j].Cells[i + 1].Style.BackColor = Color.White;
                }
            }
            #endregion

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

            //DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            // 국내무기체계 필요제원
            metroGrid2.Columns.Add("", "");

            metroGrid2.Rows[0].Cells[metroGrid2.ColumnCount - 1].Value = "설 명";
            metroGrid2.Rows[0].Cells[metroGrid2.ColumnCount - 1].Style.BackColor = Color.FromArgb(255, 242, 204);
            metroGrid2.Rows[0].Cells[metroGrid2.ColumnCount - 1].Style.ForeColor = Color.Black;

            for (int j = 1; j < metroGrid2.RowCount; j++)
            {
                metroGrid2.Rows[j].Cells[metroGrid2.ColumnCount - 1].Style.BackColor = Color.White;
            }
            this.metroGrid2.DefaultCellStyle.Font = new Font("나눔고딕", 9);

            for (int i = 0; i < metroGrid2.ColumnCount - 1; i++)
            {
                metroGrid2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                metroGrid2.Columns[i].Width = (int)(metroGrid2.Columns[i].Width * 1.2);
            }
            metroGrid2.Columns[metroGrid2.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            textBox2.Size = metroGrid2.Rows[0].Cells[metroGrid2.ColumnCount - 2].Size;

            int txtBoxWidth = 0;
            for (int i = 0; i < SubstitutionData.Length; i++) txtBoxWidth += metroGrid2.Columns[i + 1].Width;
            textBox1.Size = new Size(txtBoxWidth +1, metroGrid2.Size.Height);
            textBox1.Location = new Point(textBox1.Location.X + metroGrid2.Columns[0].Width, textBox1.Location.Y);

            X = metroGrid2.Columns.Cast<DataGridViewColumn>().Sum(r => r.Width);
            textBox2.Location = new Point(metroGrid2.Location.X + X - metroGrid2.Columns[metroGrid2.ColumnCount - 1].Width - metroGrid2.Columns[metroGrid2.ColumnCount - 2].Width, textBox1.Location.Y);

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
                // metroGrid2.Rows[_iCurrentRow + 1].Selected = true;
                //  metroGrid2.CurrentCell = metroGrid2[metroGrid2.CurrentCell.ColumnIndex, _iCurrentRow + 1];
            }
            for (int i = 1; i < metroGrid2.RowCount; i++)
            {
                if (mainFrm.DomesticSpec != null)
                {
                    if (mainFrm.DomesticSpec[i - 1] != null)
                    {
                        metroGrid2.Rows[i].Cells[metroGrid2.ColumnCount - 1].Value = mainFrm.DomesticSpec[i - 1];
                    }
                }
            }
            metroGrid2.CurrentCell = null;
        }
        private void InitializeSpec()
        {
            Spec[0] = new string[] { "개발국가", "제작사", "탑재중량", "운용고도", "운용반경", "체공시간", "최대속도", "단가 ($M)" };
            Spec[1] = new string[] { "개발국가", "제작사", "길이", "중량", "페이로드", "최대속력", "항속거리", "지속시간", "단가 ($M)" };
            Spec[2] = new string[] { "개발국가", "제작사", "장착가능화기", "원격 및 수동사격", "표적정보 디지털전시", "화기별 자동장전", "포탑구동 안정화장치", "선회", "고저", "구동속도", "안정화 정확도", "조준 정확도", "단가 ($M)" };
            Spec[3] = new string[] { "개발국가", "제작사", "레이저 발진 파워", "유효사격 거리", "단가 ($M)" };
            Spec[4] = new string[] { "개발국가", "제작사", "중량", "직경", "길이", "속력", "항주거리", "운용수심", "추진방식", "유도방식", "호밍방식", "단가 ($M)" };
            Spec[5] = new string[] { "개발국가", "만재톤수", "건조년도(년)", "최대속력(Kts)", "항속거리(NM)", "진/회수방식", "단가 ($M)" };
        }
        public DataGridView Dgv { get; set; }

        private void Substitute1_Load(object sender, EventArgs e)
        {
            if(mainFrm.Substitution == null || mainFrm.Substitution.Length == 0)
            {
                noSimilarity.Visible = true;

                bunifuCustomLabel1.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                LblConclusion.Visible = false;
                metroGrid1.Visible = false;
                metroGrid2.Visible = false;
                ConclusionBox.Visible = false;
            }
            else
            {
                noSimilarity.Visible = false;

                bunifuCustomLabel1.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                LblConclusion.Visible = true;
                ConclusionBox.Visible = true;

                metroGrid1.Visible = true;
                metroGrid2.Visible = true;

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
            if(PricesIndex != -1)
            {
                Prices = new double[SubstitutionData.Length];
                for (int i = 0; i < Prices.Length; i++)
                {
                    Prices[i] = Convert.ToDouble(metroGrid2.Rows[PricesIndex].Cells[i + 1].Value);
                }
            }
            // row 값을 저장함
            mainFrm.SelectedSubstitutionMenu = new string[metroGrid2.RowCount - 1]; // 단가는 제외함
            for (int i = 1; i < metroGrid2.RowCount - 1; i++)
            {
                mainFrm.SelectedSubstitutionMenu[i - 1] = metroGrid2.Rows[i].Cells[0].Value.ToString();
            }
        }
    
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

        private void metroGrid2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == SubstitutionData.Length + 1 && e.RowIndex > 0)
            {
                mainFrm.DomesticSpec[e.RowIndex - 1] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }

        private void metroGrid2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (metroGrid2.CurrentCell.ColumnIndex == metroGrid2.ColumnCount - 2 &&
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
