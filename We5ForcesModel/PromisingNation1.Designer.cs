namespace We5ForcesModel
{
    partial class PromisingNation1
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
            this.lbl_Introduction_Title = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.metroGrid1 = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.customersTooltip1 = new Wpf.CartesianChart.CustomTooltipAndLegend.CustomersTooltip();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Introduction_Title
            // 
            this.lbl_Introduction_Title.AutoSize = true;
            this.lbl_Introduction_Title.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Introduction_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.lbl_Introduction_Title.Location = new System.Drawing.Point(12, 392);
            this.lbl_Introduction_Title.Name = "lbl_Introduction_Title";
            this.lbl_Introduction_Title.Size = new System.Drawing.Size(151, 17);
            this.lbl_Introduction_Title.TabIndex = 1;
            this.lbl_Introduction_Title.Text = "◎ 수출유망국가 - 전체";
            // 
            // metroGrid1
            // 
            this.metroGrid1.AllowUserToAddRows = false;
            this.metroGrid1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.metroGrid1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.metroGrid1.BackgroundColor = System.Drawing.Color.White;
            this.metroGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("나눔고딕", 11.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.metroGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("나눔고딕", 11.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.DefaultCellStyle = dataGridViewCellStyle3;
            this.metroGrid1.DoubleBuffered = true;
            this.metroGrid1.EnableHeadersVisualStyles = false;
            this.metroGrid1.HeaderBgColor = System.Drawing.Color.SeaGreen;
            this.metroGrid1.HeaderForeColor = System.Drawing.Color.White;
            this.metroGrid1.Location = new System.Drawing.Point(14, 412);
            this.metroGrid1.MultiSelect = false;
            this.metroGrid1.Name = "metroGrid1";
            this.metroGrid1.ReadOnly = true;
            this.metroGrid1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("나눔고딕", 11.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.metroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.metroGrid1.RowTemplate.Height = 23;
            this.metroGrid1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.metroGrid1.Size = new System.Drawing.Size(806, 284);
            this.metroGrid1.TabIndex = 6;
            this.metroGrid1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.metroGrid1_CellMouseEnter);
            this.metroGrid1.SelectionChanged += new System.EventHandler(this.metroGrid1_SelectionChanged);
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(14, 47);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(806, 323);
            this.cartesianChart1.TabIndex = 7;
            this.cartesianChart1.Text = "cartesianChart1";
            this.cartesianChart1.DataClick += new System.Action<object, LiveCharts.ChartPoint>(this.cartesianChart1_DataClick);
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(56)))), ((int)(((byte)(100)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(336, 27);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(136, 17);
            this.bunifuCustomLabel1.TabIndex = 8;
            this.bunifuCustomLabel1.Text = "<수출유망국가식별>";
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.StripAmpersands = true;
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Lime;
            this.metroToolTip1.UseAnimation = false;
            this.metroToolTip1.UseFading = false;
            this.metroToolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.metroToolTip1_Popup);
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(200, 100);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.customersTooltip1;
            // 
            // PromisingNation1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(825, 708);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.cartesianChart1);
            this.Controls.Add(this.metroGrid1);
            this.Controls.Add(this.lbl_Introduction_Title);
            this.Font = new System.Drawing.Font("나눔고딕", 11.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PromisingNation1";
            this.Text = "PromisingNation1";
            this.Load += new System.EventHandler(this.PromisingNation1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCustomLabel lbl_Introduction_Title;
        private Bunifu.Framework.UI.BunifuCustomDataGrid metroGrid1;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private Wpf.CartesianChart.CustomTooltipAndLegend.CustomersTooltip customersTooltip1;
    }
}