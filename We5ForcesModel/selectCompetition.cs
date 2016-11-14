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
            metroGrid1.Rows.Clear();
            metroGrid1.RowHeadersVisible = false;

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            metroGrid1.Columns.Add(chk);
            chk.HeaderText = "선택";

            metroGrid1.ColumnCount = 3;

            //metroGrid1.Columns[0].Name = "순 번";
            metroGrid1.Columns[1].Name = "순번";
            metroGrid1.Columns[2].Name = "무기명";
            
            for (int i = 0; i < metroGrid1.Columns.Count - 1; i++)
            {
                metroGrid1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                metroGrid1.Columns[i].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

                metroGrid1.Columns[i].Resizable = DataGridViewTriState.False;
            }

            for (int i = 0; i < Competition2.RawWeaponDBData.Length; i++)
            {
                bool isSimilarWeapon = Competition2.isSimilarWeapon[i] == true ? false : true;
                if(isSimilarWeapon == true)
                {
                    metroGrid1.Rows.Add(false, i + 1, Competition2.RawWeaponDBData[i]);
                }
            }
            this.metroGrid1.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            for (int i = 0; i < metroGrid1.ColumnCount; i++)
            {
                metroGrid1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            metroGrid1.CurrentCell = null;
        }
        private void dtgMetroGrid2()
        {
            metroGrid2.Rows.Clear();
            metroGrid2.RowHeadersVisible = false;

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            metroGrid2.Columns.Add(chk);
            chk.HeaderText = "선택";

            metroGrid2.ColumnCount = 3;

            //metroGrid2.Columns[0].Name = "순 번";
            metroGrid2.Columns[1].Name = "순번";
            metroGrid2.Columns[2].Name = "무기명";

            for (int i = 0; i < metroGrid2.Columns.Count - 1; i++)
            {
                metroGrid2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                metroGrid2.Columns[i].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

                metroGrid2.Columns[i].Resizable = DataGridViewTriState.False;
            }


            for (int i = 0; i < Competition2.RawWeaponDBData.Length; i++)
            {
                bool isSimilarWeapon = Competition2.isSimilarWeapon[i] == true ? false : true;
                if (isSimilarWeapon == false)
                {
                    metroGrid2.Rows.Add(isSimilarWeapon, i + 1, Competition2.RawWeaponDBData[i]);
                }
            }
            this.metroGrid2.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            for (int i = 0; i < metroGrid2.ColumnCount; i++)
            {
                metroGrid2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            metroGrid2.CurrentCell = null;
        }
        private void selectCompetition_Load(object sender, EventArgs e)
        {
            dtgMetroGrid1();
            dtgMetroGrid2();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if(metroGrid1.Rows.Count < 1)
            {
                MessageBox.Show("경쟁무기체계는 1개 이상이어야 합니다.", "Error");
            }
            else
            {
                mainFrm.CompetitionWeapon = new List<string>();
                mainFrm.SimilarityWeapon = new List<string>();

                for (int i = 0; i < metroGrid1.Rows.Count; i++)
                {
                    //                bool isSimilarity = Convert.ToBoolean(metroGrid1.Rows[i].Cells[0].Value) == true ? false : true;
                    //              Competition2.isSimilarWeapon[Competition2.RawWeaponDBData[i].IndexOf(metroGrid1.Rows[i].Cells[2].Value.ToString())] = isSimilarity;

                    mainFrm.CompetitionWeapon.Add(metroGrid1.Rows[i].Cells[2].Value.ToString());
                }
                for (int i = 0; i < metroGrid2.Rows.Count; i++)
                {
                    //            bool isSimilarity = Convert.ToBoolean(metroGrid2.Rows[i].Cells[0].Value) == true ? false : true;
                    //          Competition2.isSimilarWeapon[Competition2.RawWeaponDBData[i].IndexOf(metroGrid2.Rows[i].Cells[2].Value.ToString())] = isSimilarity;

                    mainFrm.SimilarityWeapon.Add(metroGrid2.Rows[i].Cells[2].Value.ToString());
                }
                Competition2.cntCompetitionWeapon = metroGrid1.Rows.Count;
                Competition2.cntSimilarityWeapon = metroGrid2.Rows.Count;

                this.Close();
            }
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
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            List<int> RemoveIndex = new List<int>();

            for(int i = 0; i < metroGrid1.Rows.Count; i++)
            {
                if(Convert.ToBoolean(metroGrid1.Rows[i].Cells[0].Value) == true)
                {
                    RemoveIndex.Add(i);
                }
            }
            for(int i = 0; i < RemoveIndex.Count;i++)
            {
                DataGridViewRow dgvRow = new DataGridViewRow();
                metroGrid1.Rows[RemoveIndex[i]].Cells[0].Value = false;
                dgvRow = CloneWithValues(metroGrid1.Rows[RemoveIndex[i]]);
                metroGrid2.Rows.Add(dgvRow);
            }
            
            RemoveIndex.Reverse();

            for (int i = 0; i < RemoveIndex.Count; i++)
            {
                metroGrid1.Rows.Remove(metroGrid1.Rows[RemoveIndex[i]]);
            }
            metroGrid1.CurrentCell = null;
            metroGrid2.CurrentCell = null;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            List<int> RemoveIndex = new List<int>();

            for (int i = 0; i < metroGrid2.Rows.Count; i++)
            {
                if (Convert.ToBoolean(metroGrid2.Rows[i].Cells[0].Value) == true)
                {
                    RemoveIndex.Add(i);
                }
            }
            for (int i = 0; i < RemoveIndex.Count; i++)
            {
                DataGridViewRow dgvRow = new DataGridViewRow();
                metroGrid2.Rows[RemoveIndex[i]].Cells[0].Value = false;
                dgvRow = CloneWithValues(metroGrid2.Rows[RemoveIndex[i]]);
                metroGrid1.Rows.Add(dgvRow);
            }

            RemoveIndex.Reverse();

            for (int i = 0; i < RemoveIndex.Count; i++)
            {
                metroGrid2.Rows.Remove(metroGrid2.Rows[RemoveIndex[i]]);
            }
            metroGrid1.CurrentCell = null;
            metroGrid2.CurrentCell = null;
        }

        private void metroGrid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
