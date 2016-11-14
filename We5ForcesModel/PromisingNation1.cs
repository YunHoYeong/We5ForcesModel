using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Media;
using System.IO;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace We5ForcesModel
{
    public partial class PromisingNation1 : Form
    {
        OleDbConnection ExcelConn;

        public static int[,] NationPoint;
        public static double[] zPoint1;
        public static double[] zPoint2;

        public static int cntMinusMinus = 0;

        public static List<int> SelectedNationIndex = new List<int>();
        public static List<string> SelectedNationName = new List<string>();
        public static List<int> indexOfMinusMinus = new List<int>();

        public string strCon;
        public string strSQL;
        public static object[,] NationDB;
        
        public PromisingNation1()
        {
            InitializeComponent();
        }
        private void ReadExcelData()
        {
            object missing = Type.Missing;

            string ExcelPath = "DB\\0.NationDB.xlsx";
            ExcelPath = System.IO.Path.GetFullPath(ExcelPath);

            if (File.Exists(ExcelPath))
            {
                strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
                strCon += ExcelPath;
                strCon += ";Extended Properties='Excel 12.0;HDR=No;'";

                ExcelConn = new OleDbConnection(strCon);
                ExcelConn.Open();

                // 경쟁무기체계 Sheet를 불러옴
                DataSet DB = new DataSet();
             
                using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Promising Nation$];", ExcelConn))
                {
                    adapter.Fill(DB);
                }
                // 이를 Object 배열로 변환함.
                NationDB = new object[DB.Tables[0].Rows.Count, DB.Tables[0].Rows[0].ItemArray.Count()];

                for (int i = 0; i < DB.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < DB.Tables[0].Rows[0].ItemArray.Count(); j++)
                    {
                        NationDB[i, j] = DB.Tables[0].Rows[i].ItemArray[j].ToString();
                    }
                }
            //    DB.Clear();
                ExcelConn.Close();
            }

        }
        public static SeriesCollection SeriesCollection { get; set; }
        public static ScatterSeries[] Quartile1 { get; set; }
        public static ScatterSeries[] Quartile2 { get; set; }
        public static ScatterSeries[] Quartile3 { get; set; }
        public static ScatterSeries[] Quartile4 { get; set; }

        private void DrawScatterChart()
        {
            Quartile1 = new ScatterSeries[SelectedNationIndex.Count()];
            Quartile2 = new ScatterSeries[NationPoint.GetLength(0) - SelectedNationIndex.Count() - indexOfMinusMinus.Count()];
            Quartile4 = new ScatterSeries[indexOfMinusMinus.Count()];

            for (int i = 0; i < SelectedNationIndex.Count(); i++)
            {
                string NationName = metroGrid1.Rows[i + 1].Cells[0].Value.ToString();
                Quartile1[i] = new ScatterSeries
                {
                    Title = "",
                    Values = new ChartValues<ObservablePoint> { new ObservablePoint(zPoint1[SelectedNationIndex[i]], zPoint2[SelectedNationIndex[i]]) },
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Transparent,
                    DataLabels = false,
                    LabelPoint = value => NationName,
                    DataContext = "",
                };
            }
            int j = 0;
            int k = 0;
            for(int i = 0; i < NationPoint.GetLength(0); i++)
            {
                string NationName = NationDB[i + 1, 2].ToString();
                if (zPoint1[i] <= 0 || zPoint2[i] <= 0)
                {
                    if (zPoint1[i] <= 0 && zPoint2[i] <= 0)
                    {
                        Quartile4[k] = new ScatterSeries
                        {
                            Title = "",
                            Values = new ChartValues<ObservablePoint> { new ObservablePoint(zPoint1[i], zPoint2[i]) },
                            StrokeThickness = 1,
                            PointGeometry = DefaultGeometries.Cross,
                            Fill = System.Windows.Media.Brushes.Black,
                            Stroke = System.Windows.Media.Brushes.Black,
                            DataLabels = false,
                            LabelPoint = value => NationName,
                        };
                        k++;
                    }
                    else
                    {
                        Quartile2[j] = new ScatterSeries
                        {
                            Title = "",
                            Values = new ChartValues<ObservablePoint> { new ObservablePoint(zPoint1[i], zPoint2[i]) },
                            StrokeThickness = 1,
                            PointGeometry = DefaultGeometries.Triangle,
                            Stroke = System.Windows.Media.Brushes.Transparent,
                            DataLabels = false,
                            LabelPoint = value => NationName,
                        };
                        j++;
                    }
                }
            }

            cartesianChart1.Series = new SeriesCollection();
            cartesianChart1.Series.AddRange(Quartile1);
            cartesianChart1.Series.AddRange(Quartile2);
            cartesianChart1.Series.AddRange(Quartile4);
            /*
            for (int i = 0; i < Quartile1.Length; i++) { cartesianChart1.Series.Add(Quartile1[i]); }
            for (int i = 0; i < Quartile2.Length; i++) { cartesianChart1.Series.Add(Quartile2[i]); }
            for (int i = 0; i < Quartile4.Length; i++) { cartesianChart1.Series.Add(Quartile4[i]); }
            */

            #region ScatterChart의 X, Y축
            cartesianChart1.AxisX.Add(new Axis
            {
                LabelFormatter = value => string.Format("{0:F3}", value),
                
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                        FromValue = 0,
                        ToValue = zPoint1.Max() * 1.1,
                        Fill = new SolidColorBrush
                        {
                            Color = System.Windows.Media.Color.FromRgb(254,132,132),
                            Opacity = .1
                        }
                    }
                },

            });
            cartesianChart1.AxisY.Add(new Axis
            {

                LabelFormatter = value => string.Format("{0:F3}", value),
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                        FromValue = 0,
                        ToValue = zPoint2.Max() * 1.15,
                        Fill = new SolidColorBrush
                        {
                            Color = System.Windows.Media.Color.FromRgb(254,132,132),
                            Opacity = .1
                        }
                    }
                }
            });
            #endregion
            
            // cartesianChart1.LegendLocation = LegendLocation.Bottom;
            SetAxisLimits();
        //    cartesianChart1.DataTooltip = new Wpf.CartesianChart.CustomTooltipAndLegend.CustomersTooltip();
        }
        private void SetAxisLimits()
        {
            cartesianChart1.AxisX[0].MinValue = zPoint1.Min() * 0.9; // lets force the axis to be 100ms ahead
            cartesianChart1.AxisX[0].MaxValue = zPoint1.Max() * 1.1; //we only care about the last 8 seconds
            
            cartesianChart1.AxisY[0].MinValue = zPoint2.Min() * 0.9; // lets force the axis to be 100ms ahead
            cartesianChart1.AxisY[0].MaxValue = zPoint2.Max() * 1.15; //we only care about the last 8 seconds
        }

        private void dtgPromisingNation()
        {
            metroGrid1.RowHeadersVisible = false;
            metroGrid1.ColumnHeadersVisible = false;
            metroGrid1.ColumnCount = 9;
            metroGrid1.Rows.Add("국가", "GDP\n규모", "MOU\n점수", "국방비\n지출규모", "GPI\nIndex", "무기체계\n운용여부", "소요제기\n가능성", "지리적/\n환경적 여건", "자국생산\n가능여부");
            metroGrid1.Rows[0].DefaultCellStyle.Font = new Font("나눔고딕", 9, FontStyle.Bold);
            int c = 0;
            for (int i = 0; i < NationPoint.GetLength(0); i++)
            {
                if (zPoint1[i] > 0.0 && zPoint2[i] > 0.0)
                {
                    c++;
                    metroGrid1.Rows.Add();
                    for (int j = 0; j < NationPoint.GetLength(1); j++)
                    {
                        if(j == 4 || j == 7)
                        {
                            if (NationPoint[i, j] == 3) { metroGrid1.Rows[c].Cells[j + 1].Value = "X"; }
                            else if (NationPoint[i, j] == 2) { metroGrid1.Rows[c].Cells[j + 1].Value = "△"; }
                            else if (NationPoint[i, j] == 1) { metroGrid1.Rows[c].Cells[j + 1].Value = "○"; }
                            else if (NationPoint[i, j] == 0) { metroGrid1.Rows[c].Cells[j + 1].Value = "☆"; metroGrid1.Rows[c].Cells[j + 1].Style.ForeColor = System.Drawing.Color.Red; }
                        }
                        else
                        {
                            if (NationPoint[i, j] == 0) { metroGrid1.Rows[c].Cells[j + 1].Value = "X"; }
                            else if (NationPoint[i, j] == 1) { metroGrid1.Rows[c].Cells[j + 1].Value = "△"; }
                            else if (NationPoint[i, j] == 2) { metroGrid1.Rows[c].Cells[j + 1].Value = "○"; }
                            else if (NationPoint[i, j] == 3) { metroGrid1.Rows[c].Cells[j + 1].Value = "☆"; metroGrid1.Rows[c].Cells[j + 1].Style.ForeColor = System.Drawing.Color.Red; }
                        }

                    }
                    metroGrid1.Rows[c].Cells[0].Value = NationDB[i + 1, 2];
                }
            }
            this.metroGrid1.DefaultCellStyle.Font = new Font("나눔고딕", 8);

            for (int i = 0; i < metroGrid1.ColumnCount; i++)
            {
                metroGrid1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // 표 상단 색깔

            for(int i = 0; i < metroGrid1.ColumnCount; i++)
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
            this.metroGrid1.ShowCellToolTips = false;

            this.metroGrid1.CellMouseEnter += new DataGridViewCellEventHandler(metroGrid1_CellMouseEnter);
        }
        private void CalculateNationPoint()
        {
            // update datagridview Formatting
            // calculate Quartile
            // 항목의 개수만큼
            int tempMenu = 4;
            double[,] Quartiles = new double[tempMenu, 3];
            double[,] tempData = new double[tempMenu, NationDB.GetLength(0) - 1];

            for (int i = 0; i < tempMenu; i++)
            {
                for (int j = 0; j < tempData.GetLength(1); j++)
                {
                    tempData[i, j] = Convert.ToDouble(NationDB[j + 1, i + 3]);
                }
            }


            for (int i = 0; i < tempMenu; i++)
            {
                double[] tempData2 = new double[tempData.GetLength(1)];
                for (int j = 0; j < tempData2.Length; j++)
                {
                    tempData2[j] = tempData[i, j];
                }
                Array.Sort(tempData2);

                if (tempData2.Length % 2 == 0)
                {
                    // 2분위수
                    Quartiles[i, 1] = (tempData2[(int)(tempData2.Length / 2)] + tempData2[(int)(tempData2.Length / 2) - 1]) / 2;

                    // 1분위수
                    double p = 0.25;
                    double n = tempData2.Length;
                    double temp = (n - 1) * p;
                    double jj = Math.Truncate(temp);
                    double g = temp - jj;

                    Quartiles[i, 0] = ((1 - g) * tempData2[(int)(jj + 0)]) + (g * tempData2[(int)(jj + 1)]);

                    // 3분위수
                    p = 0.75;
                    n = tempData2.Length;
                    temp = (n - 1) * p;
                    jj = Math.Truncate(temp);
                    g = temp - jj;

                    Quartiles[i, 2] = ((1 - g) * tempData2[(int)(jj + 0)]) + (g * tempData2[(int)(jj + 1)]);
                }
                else
                {
                    Quartiles[i, 1] = tempData2[tempData2.Length / 2];
                    Quartiles[i, 0] = tempData2[(tempData2.Length / 4)];
                    Quartiles[i, 2] = tempData2[(tempData2.Length / 4 * 3)];
                }
            }

            NationPoint = new int[NationDB.GetLength(0) - 1, 8];

            // Update Cell Formatting 3분위수 보다 크면 ☆, 2분위수와 3분위수 사이면 O, △, X
            // 역으로 계산할 건 따로 해놔야됨
            string[] SpeicalDigit = new string[] { "X", "△", "○", "☆" };
            for (int i = 0; i < NationPoint.GetLength(0); i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Convert.ToString(NationDB[i + 1, j + 3]) == "X" || Convert.ToString(NationDB[i + 1, j + 3]) == "△" || Convert.ToString(NationDB[i + 1, j + 3]) == "○" || Convert.ToString(NationDB[i + 1, j + 3]) == "☆")
                    {
                        if (j == 4 || j == 7)
                        {
                            if (Convert.ToString(NationDB[i + 1, j + 3]) == "X") { NationPoint[i, j] = 3; }
                            if (Convert.ToString(NationDB[i + 1, j + 3]) == "△") { NationPoint[i, j] = 2; }
                            if (Convert.ToString(NationDB[i + 1, j + 3]) == "○") { NationPoint[i, j] = 1; }
                            if (Convert.ToString(NationDB[i + 1, j + 3]) == "☆") { NationPoint[i, j] = 0; }
                        }
                        else
                        {
                            if (Convert.ToString(NationDB[i + 1, j + 3]) == "X") { NationPoint[i, j] = 0; }
                            if (Convert.ToString(NationDB[i + 1, j + 3]) == "△") { NationPoint[i, j] = 1; }
                            if (Convert.ToString(NationDB[i + 1, j + 3]) == "○") { NationPoint[i, j] = 2; }
                            if (Convert.ToString(NationDB[i + 1, j + 3]) == "☆") { NationPoint[i, j] = 3; }
                        }                        
                    }
                    if (j == 1) // 외교관계는 0, 0.5, 1, 2로 등급을 메김     
                    {
                        if (Convert.ToDouble(NationDB[i + 1, j + 3]) == 0) { NationPoint[i, j] = 0; }
                        else if (Convert.ToDouble(NationDB[i + 1, j + 3]) == 0.5) { NationPoint[i, j] = 1; }
                        else if (Convert.ToDouble(NationDB[i + 1, j + 3]) == 1) { NationPoint[i, j] = 2; }
                        else if (Convert.ToDouble(NationDB[i + 1, j + 3]) == 2) { NationPoint[i, j] = 3; }
                    }
                    else if (j < tempMenu)
                    {
                        if (Convert.ToDouble(NationDB[i + 1, j + 3]) <= Quartiles[j, 0]) { NationPoint[i, j] = 0; }
                        else if (Convert.ToDouble(NationDB[i + 1, j + 3]) <= Quartiles[j, 1]) { NationPoint[i, j] = 1; }
                        else if (Convert.ToDouble(NationDB[i + 1, j + 3]) <= Quartiles[j, 2]) { NationPoint[i, j] = 2; }
                        else if (Convert.ToDouble(NationDB[i + 1, j + 3]) > Quartiles[j, 2]) { NationPoint[i, j] = 3; }
                    }

                }
            }
            // 0 ~ 3 까지가 공통식별요인 ==> Z변환점수
            // 4 ~ 7 까지가 무기체계식별요인 ==> Z변환점수
            int[] SumzPoint1 = new int[NationPoint.GetLength(0)];
            int[] SumzPoint2 = new int[NationPoint.GetLength(0)];
            zPoint1 = new double[NationPoint.GetLength(0)];
            zPoint2 = new double[NationPoint.GetLength(0)];

            double devSumzPoint1 = 0.0;
            double devSumzPoint2 = 0.0;

            for (int i = 0; i < NationPoint.GetLength(0); i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    SumzPoint1[i] += NationPoint[i, j];
                }
                for (int j = 4; j < 8; j++)
                {
                    SumzPoint2[i] += NationPoint[i, j];
                }
            }
            for (int i = 0; i < NationPoint.GetLength(0); i++)
            {
                devSumzPoint1 += Math.Pow(SumzPoint1[i] - SumzPoint1.Average(), 2);
                devSumzPoint2 += Math.Pow(SumzPoint2[i] - SumzPoint2.Average(), 2);
            }
            for (int i = 0; i < NationPoint.GetLength(0); i++)
            {
                zPoint1[i] = (SumzPoint1[i] - SumzPoint1.Average()) / devSumzPoint1;
                zPoint2[i] = (SumzPoint2[i] - SumzPoint2.Average()) / devSumzPoint2;
            }
            SelectedNationIndex.Clear();
            indexOfMinusMinus.Clear();
            SelectedNationName.Clear();

            for (int i = 0; i < NationPoint.GetLength(0); i++)
            {
                if (zPoint1[i] > 0.0 && zPoint2[i] > 0.0)
                {
                    SelectedNationIndex.Add(i);
                    SelectedNationName.Add(NationDB[i + 1, 2].ToString());
                }
                if (zPoint1[i] < 0.0 && zPoint2[i] < 0.0)
                {
                    indexOfMinusMinus.Add(i);
                }
            }
        }
        private void PromisingNation1_Load(object sender, EventArgs e)
        {
            ReadExcelData();
            CalculateNationPoint();
            dtgPromisingNation();
            DrawScatterChart();
        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {
            metroGrid1.ClearSelection();
        }

        private void metroGrid1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            metroToolTip1.ToolTipTitle = "무기체계";
            metroToolTip1.ToolTipIcon = ToolTipIcon.Info;
            
            string Column4 = "  ◦ 무기체계 운용여부 (정량)\n" +
                "- ☆(매우 높음) : 검토대상국가가 해당 무기체계를 보유한 경우(상륙돌격장갑차 탑재용)\n" +
              "- ○(높음) : 유사 / 대체 무기체계를 보유한 경우(소형전술차량탑재 / 지상거치용)\n" +
               " - △(보통) : 기타 비슷한 형태의 무기체계를 보유한 경우\n" +
              "- X(낮음) : 해당 무기체계와 동일하거나 유사/ 대체 또는 비슷한 형태의 무기체계를 미보유한 경우\n";
            if (e.RowIndex == 0)
            {
                var cell = metroGrid1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                metroToolTip1.SetToolTip(this.metroGrid1, Column4);
                metroToolTip1.ShowAlways = true;
            }
            else
            {
                metroToolTip1.Hide(this.metroGrid1);
            }
            

        }

        private void metroToolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void cartesianChart1_DataClick(object arg1, ChartPoint arg2)
        {
            BigScatterChart FrmBSC = new BigScatterChart();
            FrmBSC.Show();
        }
    }
}
