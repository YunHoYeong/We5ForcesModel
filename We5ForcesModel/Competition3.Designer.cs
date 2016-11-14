namespace We5ForcesModel
{
    partial class Competition3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.myChart = new LiveCharts.WinForms.CartesianChart();
            this.metroGrid2 = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ConclusionBox = new WindowsFormsControlLibrary1.BunifuCustomTextbox();
            this.LblConclusion = new Bunifu.Framework.UI.BunifuCustomLabel();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid2)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(11, 25);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(220, 17);
            this.bunifuCustomLabel1.TabIndex = 5;
            this.bunifuCustomLabel1.Text = "◎ 경쟁무기체계 대비 가격 경쟁력";
            // 
            // myChart
            // 
            this.myChart.Location = new System.Drawing.Point(17, 61);
            this.myChart.Name = "myChart";
            this.myChart.Size = new System.Drawing.Size(796, 206);
            this.myChart.TabIndex = 6;
            this.myChart.Text = "cartesianChart1";
            this.myChart.DataClick += new System.Action<object, LiveCharts.ChartPoint>(this.myChart_DataClick);
            // 
            // metroGrid2
            // 
            this.metroGrid2.AllowUserToAddRows = false;
            this.metroGrid2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.metroGrid2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.metroGrid2.BackgroundColor = System.Drawing.Color.White;
            this.metroGrid2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGrid2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("나눔고딕", 11.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.metroGrid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("나눔고딕", 11.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid2.DefaultCellStyle = dataGridViewCellStyle3;
            this.metroGrid2.DoubleBuffered = true;
            this.metroGrid2.EnableHeadersVisualStyles = false;
            this.metroGrid2.HeaderBgColor = System.Drawing.Color.SeaGreen;
            this.metroGrid2.HeaderForeColor = System.Drawing.Color.White;
            this.metroGrid2.Location = new System.Drawing.Point(14, 315);
            this.metroGrid2.MultiSelect = false;
            this.metroGrid2.Name = "metroGrid2";
            this.metroGrid2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("나눔고딕", 11.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid2.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.metroGrid2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid2.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.metroGrid2.RowTemplate.Height = 23;
            this.metroGrid2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroGrid2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.metroGrid2.Size = new System.Drawing.Size(799, 196);
            this.metroGrid2.TabIndex = 7;
            this.metroGrid2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.metroGrid2_CellValueChanged);
            this.metroGrid2.SelectionChanged += new System.EventHandler(this.metroGrid2_SelectionChanged);
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(328, 56);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(145, 15);
            this.bunifuCustomLabel2.TabIndex = 8;
            this.bunifuCustomLabel2.Text = "<경쟁무기체계 가격정보>";
            this.bunifuCustomLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(14, 296);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(799, 22);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "국내 ROC와 비교할 때";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ConclusionBox
            // 
            this.ConclusionBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ConclusionBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ConclusionBox.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ConclusionBox.Location = new System.Drawing.Point(14, 550);
            this.ConclusionBox.Multiline = true;
            this.ConclusionBox.Name = "ConclusionBox";
            this.ConclusionBox.Size = new System.Drawing.Size(799, 72);
            this.ConclusionBox.TabIndex = 12;
            // 
            // LblConclusion
            // 
            this.LblConclusion.AutoSize = true;
            this.LblConclusion.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblConclusion.Location = new System.Drawing.Point(14, 520);
            this.LblConclusion.Name = "LblConclusion";
            this.LblConclusion.Size = new System.Drawing.Size(68, 17);
            this.LblConclusion.TabIndex = 11;
            this.LblConclusion.Text = "기타 의견";
            // 
            // Competition3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(825, 708);
            this.Controls.Add(this.ConclusionBox);
            this.Controls.Add(this.LblConclusion);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.myChart);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.metroGrid2);
            this.Font = new System.Drawing.Font("나눔고딕", 11.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Competition3";
            this.Text = "Competition3";
            this.Load += new System.EventHandler(this.Competition3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private LiveCharts.WinForms.CartesianChart myChart;
        public Bunifu.Framework.UI.BunifuCustomDataGrid metroGrid2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private System.Windows.Forms.TextBox textBox1;
        private WindowsFormsControlLibrary1.BunifuCustomTextbox ConclusionBox;
        private Bunifu.Framework.UI.BunifuCustomLabel LblConclusion;
    }
}