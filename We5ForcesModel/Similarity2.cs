using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls

namespace We5ForcesModel
{
    public partial class Similarity2 : Form
    {
        public Similarity2()
        {
            InitializeComponent();
        }
        private void DrawCharts()
        {
            double[] ChartPrices;
            string[] ChartWeaponName;

            if (mainFrm.DomesticSpec[mainFrm.DomesticSpec.Length - 1] != null) // 국내무기체계의 가격이 있으면 + 1
            {
                ChartPrices = new double[Similarity1.Prices.Length + 1];
                ChartWeaponName = new string[Similarity1.Prices.Length + 1];

                for (int i = 0; i < Similarity1.Prices.Length; i++) { ChartPrices[i] = Similarity1.Prices[i]; }
                ChartPrices[ChartPrices.Length - 1] = Convert.ToDouble(mainFrm.DomesticSpec[mainFrm.DomesticSpec.Length - 1]);

                for (int i = 0; i < Similarity1.Prices.Length; i++) { ChartWeaponName[i] = Similarity1.SimilarData[i]; }
                ChartWeaponName[ChartPrices.Length - 1] = "국내무기체계";
            }
            else
            {
                ChartPrices = new double[Similarity1.Prices.Length];
                ChartPrices = (double[])Similarity1.Prices.Clone();

                ChartWeaponName = new string[Similarity1.SimilarData.Length];
                ChartWeaponName = (string[])Similarity1.SimilarData.Clone();
            }

            myChart.Series = new SeriesCollection
            {
                new ColumnSeries { Title = "",
                    Values = new ChartValues<double>(ChartPrices),
                    DataLabels = true,
                    LabelPoint = point => point.Y + "M"}
            };
            myChart.AxisX.Add(new Axis
            {
                Title = "Model",
                Labels = ChartWeaponName,
            });
            myChart.AxisY.Add(new Axis
            {
                Title = "Prices($M)",
                LabelFormatter = value => value.ToString("N")
            });
        }
        private void Conclusion()
        {
            int X = metroGrid2.ColumnHeadersHeight + metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid2.Height = X;

            LblConclusion.Location = new Point(metroGrid2.Location.X, X + metroGrid2.Location.Y + 30);
            ConclusionBox.Location = new Point(LblConclusion.Location.X, LblConclusion.Location.Y + 30);
        }
        private void dtgCompetition()
        {            
            //ComboBox 유형의 셀 만들고
            DataGridViewComboBoxColumn[] ComboBoxCell = new DataGridViewComboBoxColumn[mainFrm.selectCompetitionAndSimilarity.Where(c => c).Count() - 1];
            //DisplayStyle을 ComboBox로 설정
            for (int i = 0; i < ComboBoxCell.Count(); i++)
            {
                ComboBoxCell[i] = new DataGridViewComboBoxColumn();
                ComboBoxCell[i].Name = mainFrm.SelectedSimilarityMenu[i];
                ComboBoxCell[i].Items.Add("열세");
                ComboBoxCell[i].Items.Add("유사");
                ComboBoxCell[i].Items.Add("우세");

                ComboBoxCell[i].DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                ComboBoxCell[i].FlatStyle = FlatStyle.Flat;
            }

            textBox1.Size = metroGrid2.Size;
            metroGrid2.Location = new Point(textBox1.Location.X, textBox1.Location.Y + textBox1.Size.Height + 1);
            metroGrid2.Rows.Clear();


            metroGrid2.RowHeadersVisible = false;
            metroGrid2.ColumnHeadersVisible = true;

            metroGrid2.ColumnCount = 2;

            metroGrid2.Columns[0].Name = "기 종";
            metroGrid2.Columns[1].Name = "단 가";



            for (int i = 0; i < Similarity1.SimilarData.Length; i++)
            {
                metroGrid2.Rows.Add(Similarity1.SimilarData[i]);
                metroGrid2.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                metroGrid2.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(222, 235, 247);
            }

            metroGrid2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 56, 100);
            metroGrid2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            metroGrid2.ColumnHeadersDefaultCellStyle.Font = new Font("나눔고딕", 9, FontStyle.Bold);

            DataGridViewColumn dataGridViewColumn = metroGrid2.Columns[0];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.FromArgb(91, 155, 213);
            dataGridViewColumn.HeaderCell.Style.ForeColor = Color.White;

            // 단가
            DataGridViewColumn dataGridViewColumn2 = metroGrid2.Columns[1];
            dataGridViewColumn2.HeaderCell.Style.BackColor = Color.FromArgb(255, 192, 0);
            dataGridViewColumn2.HeaderCell.Style.ForeColor = Color.Black;
            
           
            // 구분 색깔
            for (int i = 0; i < 1; i++)
            {
                metroGrid2.Rows[i].Cells[0].ReadOnly = true;
                metroGrid2.Rows[i].Cells[1].ReadOnly = true;
            }


            int count = 0;
            for (int i = 0; i < mainFrm.selectCompetitionAndSimilarity.Length; i++)
            {
                if (mainFrm.CompetitionData[0, i].ToString().IndexOf("Prices") != -1 ||
                    mainFrm.CompetitionData[0, i].ToString().IndexOf("단가") != -1 ||
                   mainFrm.CompetitionData[0, i].ToString().IndexOf("가격") != -1) continue;

                if (mainFrm.selectCompetitionAndSimilarity[i] == true)
                {
                    metroGrid2.Columns.Add(ComboBoxCell[count]);
                    count++;
                }
            }

            #region 경쟁무기체계 DB에서 추가함

            // 경쟁무기체계를 추가함

            for (int j = 0; j < metroGrid2.RowCount; j++)
            {
                metroGrid2.Rows[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                metroGrid2.Rows[j].Cells[1].Style.BackColor = Color.White;
                //    metroGrid2.Rows[j].Cells[1].Style.ForeColor = Color.White;

                metroGrid2.Rows[j].Cells[1].ReadOnly = true;
            }

            for (int i = 2; i < metroGrid2.ColumnCount; i++)
            {
                for (int j = 0; j < metroGrid2.RowCount; j++)
                {
                    metroGrid2.Rows[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    metroGrid2.Rows[j].Cells[i].Style.BackColor = Color.White;
                }
            }

            #endregion
            // Design
            metroGrid2.DefaultCellStyle.BackColor = Color.White;


            this.metroGrid2.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            for (int i = 0; i < metroGrid2.RowCount; i++)
            {
                metroGrid2.Rows[i].Cells[0].Style.WrapMode = DataGridViewTriState.False;
            }
            metroGrid2.Columns[0].Width = 300;
            metroGrid2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

            for (int i = 1; i < metroGrid2.ColumnCount; i++)
            {
                metroGrid2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            foreach (DataGridViewColumn column in metroGrid2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            }
            for (int i = 2; i < metroGrid2.ColumnCount; i++)
            {
                for (int j = 0; j < metroGrid2.RowCount; j++)
                {
                    int WeaponIndex = mainFrm.SimilarityWeapon.IndexOf(metroGrid2.Rows[j].Cells[0].Value.ToString());
                    int SpecIndex = Array.IndexOf(mainFrm.FullSpecCompetitionAndSimilarity, metroGrid2.Columns[i].Name.ToString());

                    metroGrid2.Rows[j].Cells[i].Value = mainFrm.CostEffectiveCompetitionAndSimilar[WeaponIndex, SpecIndex];
                }
            }
        }

        private void Similarity2_Load(object sender, EventArgs e)
        {
            DrawCharts();
            dtgCompetition();
            WriteDataGrid();
            Conclusion();

            ConclusionBox.Text = mainFrm.ETC_Decision_4;
        }
        private void WriteDataGrid()
        {
            for (int i = 0; i < metroGrid2.RowCount; i++)
            {
                metroGrid2.Rows[i].Cells[1].Value = Similarity1.Prices[i];
            }
            int X = metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid2.Height = X + 40;
        }

        private void metroGrid2_SelectionChanged(object sender, EventArgs e)
        {
            if (metroGrid2.CurrentCell.ColumnIndex == 0 || metroGrid2.CurrentCell.ColumnIndex == 1)
            {
                metroGrid2.ClearSelection();
            }
        }

        private void metroGrid2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "우세")
            {
                metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(255, 192, 0);
                metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
            }
            if (e.RowIndex >= 0 && e.ColumnIndex > 1)
            {
                int WeaponIndex = mainFrm.SimilarityWeapon.IndexOf(metroGrid2.Rows[e.RowIndex].Cells[0].Value.ToString());
                int SpecIndex = Array.IndexOf(mainFrm.FullSpecCompetitionAndSimilarity, metroGrid2.Columns[e.ColumnIndex].Name.ToString());

                mainFrm.CostEffectiveCompetitionAndSimilar[WeaponIndex, SpecIndex] = metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                metroGrid2.Invalidate();
            }
        }

        private void metroGrid2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            metroGrid2.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void ConclusionBox_TextChanged(object sender, EventArgs e)
        {
            mainFrm.ETC_Decision_4 = ConclusionBox.Text;
        }
    }
}
