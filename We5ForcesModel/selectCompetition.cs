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
    public partial class selectCompetition : Form
    {
        public selectCompetition()
        {
            InitializeComponent();
        }
        
        private void dtgMetroGrid1()
        {
            int cntStandardMenu = 13;
            metroGrid1.Rows.Clear();
            metroGrid1.RowHeadersVisible = false;

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            metroGrid1.Columns.Add(chk);
            chk.HeaderText = "선택";

            metroGrid1.ColumnCount = mainFrm.CompetitionData.GetLength(1) - cntStandardMenu + 2;
            metroGrid1.ScrollBars = ScrollBars.Horizontal;
            metroGrid1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            metroGrid1.DefaultCellStyle.Font = new Font("나눔고딕", 10);

            metroGrid1.Columns[1].Name = "무기명";


            // 개요
            metroGrid1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 56, 100);
            metroGrid1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            for (int i = 2; i < metroGrid1.Columns.Count; i++)
            {
                metroGrid1.Columns[i].Name = mainFrm.CompetitionData[0, i  + cntStandardMenu - 2].ToString();
                    
                metroGrid1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                metroGrid1.Columns[i].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

                metroGrid1.Columns[i].Resizable = DataGridViewTriState.False;

                metroGrid1.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
            ////// DB에 있는 무기체계들을 다 읽어와서 뿌려줌
            string[] CompetitionWeapon = new string[mainFrm.CompetitionData.GetLength(0)];
            for (int i = 1; i < mainFrm.CompetitionData.GetLength(0); i++)
            {
                CompetitionWeapon[i - 1] = mainFrm.CompetitionData[i, 3].ToString();
            }
            CompetitionWeapon = CompetitionWeapon.Where(condition => condition != null).ToArray();

            for (int i = 0; i < Competition2.RawWeaponDBData.Length; i++)
            {
                bool isSimilarWeapon = Competition2.isSimilarWeapon[i] == true ? false : true;
                if(isSimilarWeapon == true)
                {
                    metroGrid1.Rows.Add(false);
                    metroGrid1.Rows[i].Cells[1].Value = Competition2.RawWeaponDBData[i];
                    for (int j = 2; j < metroGrid1.ColumnCount; j++)
                    {
                        int WeaponIndex = Array.IndexOf(CompetitionWeapon, metroGrid1.Rows[i].Cells[1].Value.ToString());
                        
                        metroGrid1.Rows[i].Cells[j].Value = mainFrm.CompetitionData[WeaponIndex + 1, j + cntStandardMenu - 2];
                    }
                }
            }
            //////////////////////////////////////////////////
            
            this.metroGrid1.DefaultCellStyle.Font = new Font("나눔고딕", 9);
   
            for(int i = 0; i < metroGrid1.RowCount; i++)
            {
                metroGrid1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                metroGrid1.Rows[i].DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                metroGrid1.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            }
            for (int i = 1; i < metroGrid1.ColumnCount; i++)
            {
                metroGrid1.Columns[i].Width = 500;
                metroGrid1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                metroGrid1.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                metroGrid1.Columns[i].ReadOnly = true;
            }
            int X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid1.Height = X + 18;

            metroGrid1.CurrentCell = null;
            metroGrid1.Columns[0].Width = 50;

            metroButton1.Location = new Point(metroButton1.Location.X, metroGrid1.Location.Y + metroGrid1.Height + 50);

            //전체 폼 크기 변경
            this.Size = new Size(this.Size.Width, metroGrid1.Height + metroButton1.Height + 150);
            this.CenterToScreen();
        }

        private void selectCompetition_Load(object sender, EventArgs e)
        {
            dtgMetroGrid1();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            int countSelect = 0;
            for(int i = 0; i < metroGrid1.RowCount; i++)
            {
                if((bool)metroGrid1.Rows[i].Cells[0].Value) { countSelect++; }
            }
            if (countSelect < 1)
            {
                MessageBox.Show("경쟁무기체계는 1개 이상이어야 합니다.", "Error");
            }
            else
            {
                mainFrm.CompetitionWeapon = new List<string>();
                mainFrm.SimilarityWeapon = new List<string>();

                for (int i = 0; i < metroGrid1.Rows.Count; i++)
                {
                    if ((bool)metroGrid1.Rows[i].Cells[0].Value) 
                        mainFrm.CompetitionWeapon.Add(metroGrid1.Rows[i].Cells[1].Value.ToString());
                    else
                        mainFrm.SimilarityWeapon.Add(metroGrid1.Rows[i].Cells[1].Value.ToString());
                }

                Competition2.cntCompetitionWeapon = countSelect;
                Competition2.cntSimilarityWeapon = metroGrid1.Rows.Count - countSelect;
            }
            this.Close();
           
        }
        public DataGridViewRow CloneWithValues(DataGridViewRow row)
        {
            DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();

            for (Int32 index = 0; index < row.Cells.Count; index++)
            {
                clonedRow.Cells[index].Value = row.Cells[index].Value;
            }
            return clonedRow;
        }
       
        private void metroGrid1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            metroGrid1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

    }
}
