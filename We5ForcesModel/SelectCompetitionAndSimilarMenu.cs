using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text; 

namespace We5ForcesModel
{
    public partial class SelectCompetitionAndSimilarMenu : Form
    {
        int cntStandardMenu = 13;

        public static Label[] CustomLabel;
        public static int PricesIndex = 0;

        private static Color[] Theme = new Color[]{
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(255, 255, 255),
            Color.FromArgb(85, 85, 85),
            Color.FromArgb(0, 174, 219),
            Color.FromArgb(0, 177, 89),
            Color.FromArgb(142, 188, 0),
            Color.FromArgb(0, 170, 173),
            Color.FromArgb(243, 119, 53),
            Color.FromArgb(165, 81, 0),
            Color.FromArgb(231, 113, 189),
            Color.FromArgb(255, 0, 148),
            Color.FromArgb(124, 65, 153),
            Color.FromArgb(209, 17, 65),
            Color.FromArgb(255, 196, 37),
            Color.FromArgb(32, 56, 100)
        };
        public SelectCompetitionAndSimilarMenu()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        private void SelectCompetitionAndSimilarMenu_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < mainFrm.CompetitionData.GetLength(1); i++)
            {
                if (mainFrm.CompetitionData[0, i].ToString().Contains("Prices")  ||
                      mainFrm.CompetitionData[0, i].ToString().Contains("단가")  ||
                      mainFrm.CompetitionData[0, i].ToString().Contains("가격"))
                {
                    PricesIndex = i;
                    mainFrm.selectCompetitionAndSimilarity[PricesIndex] = true;
                    break;
                }
            }

            CustomLabel = new Label[mainFrm.CompetitionData.GetLength(1) - cntStandardMenu];

            int count = 0;
            int rowIndex = 0;
            int colIndex = 0;

            Size MaxSize = new Size(100, 0);
            DockStyle _DockStyle = DockStyle.Fill;
            Font _Font = new Font("나눔고딕", 8);
            Cursor _Cursor = Cursors.Hand;
            AnchorStyles _Anchor = AnchorStyles.None;
            ContentAlignment _Alignment = ContentAlignment.MiddleCenter;

            for (int i = 0; i < CustomLabel.Count(); i++)
            {
                CustomLabel[count] = new Label();
                CustomLabel[count].TextAlign = _Alignment;
                CustomLabel[count].Text = mainFrm.CompetitionData[0, i + cntStandardMenu].ToString();

                CustomLabel[count].MaximumSize = MaxSize;
                CustomLabel[count].AutoSize = true;
                CustomLabel[count].Dock = _DockStyle;
                CustomLabel[count].Font = _Font;

                CustomLabel[count].Cursor = _Cursor;
                CustomLabel[count].TabIndex = i + 1 + cntStandardMenu;
                //         CustomLabel[count].ForeColor = Color.FromArgb();
                tableLayoutPanel1.SuspendLayout();
                tableLayoutPanel1.Controls.Add(CustomLabel[count], colIndex, rowIndex);
                tableLayoutPanel1.ResumeLayout();
                //CustomLabel[count].Anchor = AnchorStyles.;
                CustomLabel[count].Anchor = _Anchor;
                CustomLabel[count].Click += MyButtonClick;
                count++;

                colIndex++;

                if (rowIndex == tableLayoutPanel1.RowCount) { rowIndex = 0; }
                if (colIndex == tableLayoutPanel1.ColumnCount)
                {
                    colIndex = 0;
                    rowIndex++;
                }
            }
            for(int i = tableLayoutPanel1.RowCount - 1; i > CustomLabel.Length / tableLayoutPanel1.ColumnCount; i--)
            {
                tableLayoutPanel1.RowStyles.RemoveAt(i);
            }
        }
        void MyButtonClick(object sender, EventArgs e)
        {
            Label button = sender as Label;
            if (button.BackColor == Theme[Theme.Length - 1])
            {
                button.BackColor = Theme[1];
                button.ForeColor = Theme[0];

                mainFrm.selectCompetitionAndSimilarity[button.TabIndex - 1] = false;
            }
            else
            {
                button.BackColor = Theme[Theme.Length - 1];
                button.ForeColor = Theme[1];

                mainFrm.selectCompetitionAndSimilarity[button.TabIndex - 1] = true;
            }
        }
        /// <summary>
        /// Double buffer
        /// </summary>
        [Description("Double buffer")]
        [DefaultValue(true)]
        public bool dBuffer
        {
            get { return this.DoubleBuffered; }
            set { this.DoubleBuffered = value; }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (mainFrm.selectCompetitionAndSimilarity.Where(c => c).Count() < 5)
            {
                MessageBox.Show("제원을 5개 이상 선택해주세요.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.Close();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.tableLayoutPanel1.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Dotted);
        }
    }
}
