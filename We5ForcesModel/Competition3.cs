
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
    public partial class Competition3 : Form
    {
        public Competition3()
        {
            InitializeComponent();
        }
        private void DrawCharts()
        {
            
            myChart.Series = new SeriesCollection
            {
                new ColumnSeries { Title = "",
                    Values = new ChartValues<double>(Competition2.Prices),
                    DataLabels = true,
                    LabelPoint = point => point.Y + "M"}

            };
            myChart.AxisX.Add(new Axis
            {
                Title = "Model",
                Labels = Competition2.CompetitionWeapon,
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
            LblConclusion.Location = new Point(metroGrid2.Location.X, X + metroGrid2.Location.Y + 30);
            ConclusionBox.Location = new Point(LblConclusion.Location.X, LblConclusion.Location.Y + 30);
        }
        private void dtgCompetition()
        {
            textBox1.Size = metroGrid2.Size;
            metroGrid2.Location = new Point(textBox1.Location.X, textBox1.Location.Y + textBox1.Size.Height + 1);
            metroGrid2.Rows.Clear();
    

            metroGrid2.RowHeadersVisible = false;
            metroGrid2.ColumnHeadersVisible = false;
            metroGrid2.ColumnCount = 1;
            
            metroGrid2.Rows.Add("기 종");
            // 구분 색깔
            for (int i = 0; i < 1; i++)
            {
                metroGrid2.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                metroGrid2.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(91, 155, 213);
                metroGrid2.Rows[i].Cells[0].Style.ForeColor = Color.White;

                metroGrid2.Rows[i].Cells[0].ReadOnly = true;
            }
            // 단가
            metroGrid2.Columns.Add("", "");
            metroGrid2.Rows[0].Cells[1].Value = Competition2.Spec[mainFrm.CurrentWeapon][Competition2.Spec[mainFrm.CurrentWeapon].Count() - 1];
            metroGrid2.Rows[0].Cells[1].Style.BackColor = Color.FromArgb(255, 192, 0);
            metroGrid2.Rows[0].Cells[1].Style.ForeColor = Color.Black;
            metroGrid2.Rows[0].DefaultCellStyle.Font = new Font("나눔고딕", 9, FontStyle.Bold);

            // 경쟁무기 개수만큼 열을 추가
            // ★2번째 페이지 대비 어떤 항목을 우세, 열세를 할지
            for (int i = 1; i < Competition2.Spec[mainFrm.CurrentWeapon].Count() - 5; i++)
            {
                metroGrid2.Columns.Add("", "");
                metroGrid2.Rows[0].Cells[i + 1].Value = Competition2.Spec[mainFrm.CurrentWeapon][i];
                metroGrid2.Rows[0].Cells[i + 1].Style.BackColor = Color.FromArgb(32, 56, 100);
                metroGrid2.Rows[0].Cells[i + 1].Style.ForeColor = Color.White;
                metroGrid2.Rows[0].DefaultCellStyle.Font = new Font("나눔고딕", 9, FontStyle.Bold);
            }
            for (int i = 0; i < Competition2.CompetitionWeapon.Length; i++)
            {
                metroGrid2.Rows.Add(Competition2.CompetitionWeapon[i]);
                metroGrid2.Rows[i + 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                metroGrid2.Rows[i + 1].Cells[0].Style.BackColor = Color.FromArgb(222, 235, 247);
            }

            #region 경쟁무기체계 DB에서 추가함

            // 경쟁무기체계를 추가함
            
            for (int j = 1; j < metroGrid2.RowCount; j++)
            {
                metroGrid2.Rows[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                metroGrid2.Rows[j].Cells[1].Style.BackColor = Color.White;
                //    metroGrid2.Rows[j].Cells[1].Style.ForeColor = Color.White;

                metroGrid2.Rows[j].Cells[1].ReadOnly = true;
            }

            for (int i = 2; i < metroGrid2.ColumnCount; i++)
            {
                for (int j = 1; j < metroGrid2.RowCount; j++)
                {
                    metroGrid2.Rows[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    metroGrid2.Rows[j].Cells[i].Style.BackColor = Color.White;
                }
            }

            #endregion
            // Design
            metroGrid2.DefaultCellStyle.BackColor = Color.White;


            this.metroGrid2.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            for (int i = 0; i < metroGrid2.ColumnCount; i++)
            {
                metroGrid2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            metroGrid2.CurrentCell = null;
        }
        private void Competition3_Load(object sender, EventArgs e)
        {
            DrawCharts();
            dtgCompetition();
            WriteDataGrid();
            Conclusion();
            // anotherForm.metroGrid2;
        }
        private void WriteDataGrid()
        {
            for(int i = 1; i < metroGrid2.RowCount; i++)
            {
                metroGrid2.Rows[i].Cells[1].Value = Competition2.Prices[i-1];
            }
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
                metroGrid2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
            }

        }
    }
}
