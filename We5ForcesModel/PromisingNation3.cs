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
    public partial class PromisingNation3 : Form
    {
        public PromisingNation3()
        {
            InitializeComponent();
        }
        private void dtgAmerica()
        {
            
            metroGrid1.Location = new Point(lbl1.Location.X, lbl1.Location.Y + 20);

            metroGrid1.RowHeadersVisible = false;
            metroGrid1.ColumnHeadersVisible = false;
            metroGrid1.ColumnCount = 9;

            metroGrid1.Rows.Add("국가", "GDP\n규모", "MOU\n점수", "국방비\n지출규모", "GPI\nIndex", "무기체계\n운용여부", "소요제기\n가능성", "지리적/\n환경적 여건", "자국생산\n가능여부");
            metroGrid1.Rows[0].DefaultCellStyle.Font = new Font("나눔고딕", 9, FontStyle.Bold);

            int c = 0;
            for (int i = 0; i < PromisingNation1.NationDB.GetLength(0) - 1; i++)
            {
                if (PromisingNation1.NationDB[i + 1, 0].ToString() == "미주")
                {
                    c++;
                    metroGrid1.Rows.Add();
                    metroGrid1.Rows[c].Cells[0].Value = PromisingNation1.NationDB[i + 1, 2].ToString();

                    metroGrid1.Rows[c].Cells[0].Style.ForeColor = Color.Black;
                    for (int j = 0; j < metroGrid1.ColumnCount - 1; j++)
                    {
                        metroGrid1.Rows[c].Cells[j + 1].Value = PromisingNation1.NationPoint[i, j].ToString();
                    }
                }
            }

            // 점수를 별로 환산
            for (int i = 1; i < metroGrid1.RowCount; i++)
            {
                for (int j = 0; j < metroGrid1.ColumnCount - 1; j++)
                {
                    metroGrid1.Rows[i].Cells[j + 1].Style.ForeColor = Color.Black;
                    if (j == 4 || j == 7)
                    {
                        if (Convert.ToInt32(metroGrid1.Rows[i].Cells[j + 1].Value) == 3) { metroGrid1.Rows[i].Cells[j + 1].Value = "X"; }
                        else if (Convert.ToInt32(metroGrid1.Rows[i].Cells[j + 1].Value) == 2) { metroGrid1.Rows[i].Cells[j + 1].Value = "△"; }
                        else if (Convert.ToInt32(metroGrid1.Rows[i].Cells[j + 1].Value) == 1) { metroGrid1.Rows[i].Cells[j + 1].Value = "○"; }
                        else if (Convert.ToInt32(metroGrid1.Rows[i].Cells[j + 1].Value) == 0) { metroGrid1.Rows[i].Cells[j + 1].Value = "☆"; metroGrid1.Rows[i].Cells[j + 1].Style.ForeColor = System.Drawing.Color.Red; }
                    }
                    else
                    {
                        if (Convert.ToInt32(metroGrid1.Rows[i].Cells[j + 1].Value) == 0) { metroGrid1.Rows[i].Cells[j + 1].Value = "X"; }
                        else if (Convert.ToInt32(metroGrid1.Rows[i].Cells[j + 1].Value) == 1) { metroGrid1.Rows[i].Cells[j + 1].Value = "△"; }
                        else if (Convert.ToInt32(metroGrid1.Rows[i].Cells[j + 1].Value) == 2) { metroGrid1.Rows[i].Cells[j + 1].Value = "○"; }
                        else if (Convert.ToInt32(metroGrid1.Rows[i].Cells[j + 1].Value) == 3) { metroGrid1.Rows[i].Cells[j + 1].Value = "☆"; metroGrid1.Rows[i].Cells[j + 1].Style.ForeColor = System.Drawing.Color.Red; }
                    }
                }
            }

            this.metroGrid1.DefaultCellStyle.Font = new Font("나눔고딕", 8);

            for (int i = 0; i < metroGrid1.ColumnCount; i++)
            {
                metroGrid1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // 표 상단 색깔

            for (int i = 0; i < metroGrid1.ColumnCount; i++)
            {
                metroGrid1.Rows[0].Cells[i].Style.BackColor = System.Drawing.Color.FromArgb(32, 56, 100);
                metroGrid1.Rows[0].Cells[i].Style.ForeColor = System.Drawing.Color.White;
            }
            for (int i = 1; i < metroGrid1.RowCount; i++)
            {
                for (int j = 0; j < metroGrid1.ColumnCount; j++)
                {
                    metroGrid1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
                metroGrid1.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid1.Rows[i].Cells[0].Style.ForeColor = System.Drawing.Color.Black;
            }

            for (int i = 0; i < metroGrid1.ColumnCount; i++)
            {
                metroGrid1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            int X = metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid1.Height = X + 10;


            X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            lbl2.Location = new Point(metroGrid1.Location.X, X + metroGrid1.Location.Y + 10);
            metroGrid2.Location = new Point(lbl2.Location.X, lbl2.Location.Y + 20);

            for (int i = 1; i < metroGrid1.RowCount; i++)
            {
                if (PromisingNation1.SelectedNationName.IndexOf(metroGrid1.Rows[i].Cells[0].Value.ToString()) != -1)
                {
                    for (int j = 0; j < metroGrid1.ColumnCount; j++)
                    {
                        metroGrid1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(255, 242, 204);
                    }
                }
            }
        }
        private void Conclusion()
        {
            int X = metroGrid2.ColumnHeadersHeight + metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            LblConclusion.Location = new Point(metroGrid2.Location.X, X + metroGrid2.Location.Y + 50);
            ConclusionBox.Location = new Point(LblConclusion.Location.X, LblConclusion.Location.Y + 25);
        }
        private void dtgAfrica()
        {
            metroGrid2.RowHeadersVisible = false;
            metroGrid2.ColumnHeadersVisible = false;
            metroGrid2.ColumnCount = 9;
            metroGrid2.Rows.Add("국가", "GDP\n규모", "MOU\n점수", "국방비\n지출규모", "GPI\nIndex", "무기체계\n운용여부", "소요제기\n가능성", "지리적/\n환경적 여건", "자국생산\n가능여부");
            metroGrid2.Rows[0].DefaultCellStyle.Font = new Font("나눔고딕", 9, FontStyle.Bold);

            int c = 0;
            for (int i = 0; i < PromisingNation1.NationDB.GetLength(0) - 1; i++)
            {
                if (PromisingNation1.NationDB[i + 1, 0].ToString() == "아프리카")
                {
                    metroGrid2.Rows.Add();
                    c++;
                    metroGrid2.Rows[c].Cells[0].Value = PromisingNation1.NationDB[i + 1, 2].ToString();
                    metroGrid2.Rows[c].Cells[0].Style.ForeColor = Color.Black;
                    for (int j = 0; j < metroGrid2.ColumnCount - 1; j++)
                    {
                        metroGrid2.Rows[c].Cells[j + 1].Value = PromisingNation1.NationPoint[i, j].ToString();
                    }
                }
            }

            // 점수를 별로 환산
            for (int i = 1; i < metroGrid2.RowCount; i++)
            {

                for (int j = 0; j < metroGrid2.ColumnCount - 1; j++)
                {
                    metroGrid2.Rows[i].Cells[j + 1].Style.ForeColor = Color.Black;
                    if (j == 4 || j == 7)
                    {
                        if (Convert.ToInt32(metroGrid2.Rows[i].Cells[j + 1].Value) == 3) { metroGrid2.Rows[i].Cells[j + 1].Value = "X"; }
                        else if (Convert.ToInt32(metroGrid2.Rows[i].Cells[j + 1].Value) == 2) { metroGrid2.Rows[i].Cells[j + 1].Value = "△"; }
                        else if (Convert.ToInt32(metroGrid2.Rows[i].Cells[j + 1].Value) == 1) { metroGrid2.Rows[i].Cells[j + 1].Value = "○"; }
                        else if (Convert.ToInt32(metroGrid2.Rows[i].Cells[j + 1].Value) == 0) { metroGrid2.Rows[i].Cells[j + 1].Value = "☆"; metroGrid2.Rows[i].Cells[j + 1].Style.ForeColor = System.Drawing.Color.Red; }
                    }
                    else
                    {
                        if (Convert.ToInt32(metroGrid2.Rows[i].Cells[j + 1].Value) == 0) { metroGrid2.Rows[i].Cells[j + 1].Value = "X"; }
                        else if (Convert.ToInt32(metroGrid2.Rows[i].Cells[j + 1].Value) == 1) { metroGrid2.Rows[i].Cells[j + 1].Value = "△"; }
                        else if (Convert.ToInt32(metroGrid2.Rows[i].Cells[j + 1].Value) == 2) { metroGrid2.Rows[i].Cells[j + 1].Value = "○"; }
                        else if (Convert.ToInt32(metroGrid2.Rows[i].Cells[j + 1].Value) == 3) { metroGrid2.Rows[i].Cells[j + 1].Value = "☆"; metroGrid2.Rows[i].Cells[j + 1].Style.ForeColor = System.Drawing.Color.Red; }
                    }
                }
            }

            this.metroGrid2.DefaultCellStyle.Font = new Font("나눔고딕", 8);

            for (int i = 0; i < metroGrid2.ColumnCount; i++)
            {
                metroGrid2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // 표 상단 색깔

            for (int i = 0; i < metroGrid2.ColumnCount; i++)
            {
                metroGrid2.Rows[0].Cells[i].Style.BackColor = System.Drawing.Color.FromArgb(32, 56, 100);
                metroGrid2.Rows[0].Cells[i].Style.ForeColor = System.Drawing.Color.White;
            }
            for (int i = 1; i < metroGrid2.RowCount; i++)
            {
                for (int j = 0; j < metroGrid2.ColumnCount; j++)
                {
                    metroGrid2.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
                metroGrid2.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid2.Rows[i].Cells[0].Style.ForeColor = System.Drawing.Color.Black;
            }

            for (int i = 0; i < metroGrid2.ColumnCount; i++)
            {
                metroGrid2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            int X = metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid2.Height = X + 10;

            for (int i = 1; i < metroGrid2.RowCount; i++)
            {
                if (PromisingNation1.SelectedNationName.IndexOf(metroGrid2.Rows[i].Cells[0].Value.ToString()) != -1)
                {
                    for (int j = 0; j < metroGrid1.ColumnCount; j++)
                    {
                        metroGrid2.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(255, 242, 204);
                    }
                }
            }

        }

        private void PromisingNation3_Load(object sender, EventArgs e)
        {   
            dtgAmerica();
            dtgAfrica();
            ConclusionBox.Text = mainFrm.ETC_Decision_7;
        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {
            metroGrid1.ClearSelection();
        }

        private void metroGrid2_SelectionChanged(object sender, EventArgs e)
        {
            metroGrid2.ClearSelection();
        }

        private void ConclusionBox_TextChanged(object sender, EventArgs e)
        {
            mainFrm.ETC_Decision_7 = ConclusionBox.Text;
        }
    }
}
