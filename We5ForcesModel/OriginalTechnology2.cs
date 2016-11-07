using System;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Drawing;
using System.Collections.Generic;

namespace We5ForcesModel
{
    public partial class OriginalTechnology2 : Form
    {
        OleDbConnection ExcelConn;

        public static string Year = DateTime.Now.ToString("yyyy");

        public string strCon;
        public string strSQL;
        public static object[,] ScurveData;
        public static double GotoWarPeriod = 90;
        public static List<string> Title;
        public OriginalTechnology2()
        {
            InitializeComponent();
        }
        
        private void ReadExcelData()
        {
            object missing = Type.Missing;

            string ExcelPath = "DB\\scurve.xlsx";
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
                using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [RCWS$];", ExcelConn))
                {
                    adapter.Fill(DB);
                }
                // 이를 Object 배열로 변환함.
                ScurveData = new object[DB.Tables[0].Rows.Count, DB.Tables[0].Rows[0].ItemArray.Count()];

                for (int i = 0; i < DB.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < DB.Tables[0].Rows[0].ItemArray.Count(); j++)
                    {
                        ScurveData[i, j] = DB.Tables[0].Rows[i].ItemArray[j].ToString();
                    }
                }

                DB.Clear();
                ExcelConn.Close();
            }
        }

        private void DrawSCurveData1(string[] D1, string[] D2)
        {
            double[] Domestic = Array.ConvertAll(D1, x => Convert.ToDouble(x));
            double[] International = Array.ConvertAll(D2, x => Convert.ToDouble(x));

            int SeperatorIndex = Domestic.Length - 1;
            int DomesticIndex = Domestic.Length - 1;
            int InternationalIndex = Domestic.Length - 1;

            for (int i = 0; i < Domestic.Length; i++)
            {
                if (Domestic[i] >= International[i])
                { SeperatorIndex = i; break; }
            }
            for (int i = 0; i < Domestic.Length; i++)
            {
                if (Domestic[i] >= GotoWarPeriod)
                { DomesticIndex = i; break; }
            }
            for (int i = 0; i < Domestic.Length; i++)
            {
                if (International[i] >= GotoWarPeriod)
                { InternationalIndex = i; break; }
            }


            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "국내 기술 수준",
                    Values = new ChartValues<double>(Domestic),
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    LabelPoint = point => String.Format("{0:F2}", point.Y) + "%",
                },
                new LineSeries
                {
                    Title = "원천 기술 보유국 수준",
                    Values = new ChartValues<double>(International),
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    LabelPoint = point => String.Format("{0:F2}", point.Y) + "%",
                }
            };
            
            string[] X_Label = new string[Domestic.Length];
            for (int j = 0; j < ScurveData.GetLength(0); j++)
            {
                X_Label[j] = ScurveData[j, 0].ToString();
            }
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Year",
                Labels = X_Label,
             //   IsMerged = true,
                Separator = new Separator
                {
                    Step = DomesticIndex,
                    //   Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                },
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                        Label = "국내전력화시기",
                        FromValue = DomesticIndex,
                        ToValue = DomesticIndex,

                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(33,149,242)),
                        StrokeThickness = 1,
                        StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 3 }),
                    },
                    new AxisSection
                    {
                        Label = "해외전력화시기",
                        FromValue = InternationalIndex,
                        ToValue = InternationalIndex,
                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(243,67,54)),
                        StrokeThickness = 1,
                        StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 3 }),
                    }
                },
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Level",
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                       // Label = "전력화 기술수준",
                        FromValue = 90,
                        ToValue = 90,
                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(153,180,51)),
                        StrokeThickness = 1,
                    },
                    new AxisSection
                    {
                        Label = "전력화 기술수준",
                        FromValue = 95,
                        ToValue = 95,
                        Fill = new SolidColorBrush
                        {
                            Color = System.Windows.Media.Color.FromRgb(0,0,0),
                            Opacity = .0
                        }
                    }
                },
                Separator = new Separator
                {
                    Step = 10,
                }
            });

            cartesianChart1.AxisY[0].MinValue = 0; // lets force the axis to be 100ms ahead
            cartesianChart1.AxisY[0].MaxValue = 100;
            cartesianChart1.Font = new System.Drawing.Font("나눔고딕", 9);

            cartesianChart1.LegendLocation = LegendLocation.Bottom;
            Title1.Text = "<" + Title[0] + ">";
            Title1.Location = new Point(((int)(this.Width / 4)) - ((int)(Title1.Width / 2)), 45);

            metroGrid1.Rows.Clear();
            metroGrid1.RowHeadersVisible = false;
            metroGrid1.ColumnCount = 4;

            metroGrid1.Columns[0].Name = "";
            metroGrid1.Columns[1].Name = "기술보유국";
            metroGrid1.Columns[2].Name = "국내수준";
            metroGrid1.Columns[3].Name = "기술격차";
            
            metroGrid1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(32, 56, 100);
            metroGrid1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;

            int YearIndex = Convert.ToInt32(Year) - Convert.ToInt32(X_Label[0]) ;

            metroGrid1.Rows.Add("현재", String.Format("{0:F2}", International[YearIndex]) + "%", String.Format("{0:F2}", Domestic[YearIndex]) + "%", String.Format("{0:F2}", International[YearIndex] - Domestic[YearIndex]) + "%");
            metroGrid1.Rows.Add("5년후", String.Format("{0:F2}", International[YearIndex + 5]) + "%", String.Format("{0:F2}", Domestic[YearIndex + 5]) + "%", String.Format("{0:F2}", International[YearIndex + 5] - Domestic[YearIndex + 5]) + "%");
            metroGrid1.Rows.Add("전력화 예상시점", Convert.ToInt32(X_Label[0]) + InternationalIndex + "년", Convert.ToInt32(X_Label[0]) +  DomesticIndex + "년", DomesticIndex - InternationalIndex + "년");


            metroGrid1.CurrentCell = null;
            for (int i = 0; i < metroGrid1.RowCount; i++)
            {
                for (int j = 0; j < metroGrid1.ColumnCount; j++)
                {
                    metroGrid1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
                metroGrid1.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid1.Rows[i].Cells[0].Style.ForeColor = System.Drawing.Color.Black;
            }

            foreach (DataGridViewColumn column in metroGrid1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            
            metroGrid1.Columns[0].Width = (int)(metroGrid1.Width * 0.26);
            metroGrid1.Columns[1].Width = (int)(metroGrid1.Width * 0.24);
            metroGrid1.Columns[2].Width = (int)(metroGrid1.Width * 0.24);
            metroGrid1.Columns[3].Width = (int)(metroGrid1.Width * 0.24);


            int X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid1.Height = X + 10;
            this.metroGrid1.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            this.metroGrid1.ColumnHeadersDefaultCellStyle.Font = new Font("나눔고딕", 9);
        }
        private void DrawSCurveData2(string[] D1, string[] D2)
        {
            double[] Domestic = Array.ConvertAll(D1, x => Convert.ToDouble(x));
            double[] International = Array.ConvertAll(D2, x => Convert.ToDouble(x));

            int SeperatorIndex = Domestic.Length - 1;
            int DomesticIndex = Domestic.Length - 1;
            int InternationalIndex = Domestic.Length - 1;

            for (int i = 0; i < Domestic.Length; i++)
            {
                if (Domestic[i] >= International[i])
                { SeperatorIndex = i; break; }
            }
            for (int i = 0; i < Domestic.Length; i++)
            {
                if (Domestic[i] >= GotoWarPeriod)
                { DomesticIndex = i; break; }
            }
            for (int i = 0; i < Domestic.Length; i++)
            {
                if (International[i] >= GotoWarPeriod)
                { InternationalIndex = i; break; }
            }


            cartesianChart2.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "국내 기술 수준",
                    Values = new ChartValues<double>(Domestic),
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    LabelPoint = point => String.Format("{0:F2}", point.Y) + "%",
                },
                new LineSeries
                {
                    Title = "원천 기술 보유국 수준",
                    Values = new ChartValues<double>(International),
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    LabelPoint = point => String.Format("{0:F2}", point.Y) + "%",
                }
            };

            string[] X_Label = new string[Domestic.Length];
            for (int j = 0; j < ScurveData.GetLength(0); j++)
            {
                X_Label[j] = ScurveData[j, 0].ToString();
            }
            cartesianChart2.AxisX.Add(new Axis
            {
                Title = "Year",
                Labels = X_Label,
                //   IsMerged = true,
                Separator = new Separator
                {
                    Step = DomesticIndex,
                    //   Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                },
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                        Label = "국내전력화시기",
                        FromValue = DomesticIndex,
                        ToValue = DomesticIndex,

                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(33,149,242)),
                        StrokeThickness = 1,
                        StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 3 }),
                    },
                    new AxisSection
                    {
                        Label = "해외전력화시기",
                        FromValue = InternationalIndex,
                        ToValue = InternationalIndex,
                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(243,67,54)),
                        StrokeThickness = 1,
                        StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 3 }),
                    }
                },
            });

            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "Level",
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                       // Label = "전력화 기술수준",
                        FromValue = 90,
                        ToValue = 90,
                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(153,180,51)),
                        StrokeThickness = 1,
                    },
                    new AxisSection
                    {
                        Label = "전력화 기술수준",
                        FromValue = 95,
                        ToValue = 95,
                        Fill = new SolidColorBrush
                        {
                            Color = System.Windows.Media.Color.FromRgb(0,0,0),
                            Opacity = .0
                        }

                    }
                },
                Separator = new Separator
                {
                    Step = 10,
                }
            });

            cartesianChart2.AxisY[0].MinValue = 0; // lets force the axis to be 100ms ahead
            cartesianChart2.AxisY[0].MaxValue = 100;
            cartesianChart2.Font = new System.Drawing.Font("나눔고딕", 9);
            
            cartesianChart2.LegendLocation = LegendLocation.Bottom;


            Title2.Text = "<" + Title[1] + ">";
            Title2.Location = new Point((((int)(this.Width / 4)) * 3) - ((int)(Title2.Width / 2)), 45);

            metroGrid2.Rows.Clear();
            metroGrid2.RowHeadersVisible = false;
            metroGrid2.ColumnCount = 4;

            metroGrid2.Columns[0].Name = "";
            metroGrid2.Columns[1].Name = "기술보유국";
            metroGrid2.Columns[2].Name = "국내수준";
            metroGrid2.Columns[3].Name = "기술격차";

            metroGrid2.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(32, 56, 100);
            metroGrid2.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;

            int YearIndex = Convert.ToInt32(Year) - Convert.ToInt32(X_Label[0]);

            metroGrid2.Rows.Add("현재", String.Format("{0:F2}", International[YearIndex]) + "%", String.Format("{0:F2}", Domestic[YearIndex]) + "%", String.Format("{0:F2}", International[YearIndex] - Domestic[YearIndex]) + "%");
            metroGrid2.Rows.Add("5년후", String.Format("{0:F2}", International[YearIndex + 5]) + "%", String.Format("{0:F2}", Domestic[YearIndex + 5]) + "%", String.Format("{0:F2}", International[YearIndex + 5] - Domestic[YearIndex + 5]) + "%");
            metroGrid2.Rows.Add("전력화 예상시점", Convert.ToInt32(X_Label[0]) + InternationalIndex + "년", Convert.ToInt32(X_Label[0]) + DomesticIndex + "년", DomesticIndex - InternationalIndex + "년");


            metroGrid2.CurrentCell = null;
            for (int i = 0; i < metroGrid2.RowCount; i++)
            {
                for (int j = 0; j < metroGrid2.ColumnCount; j++)
                {
                    metroGrid2.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
                metroGrid2.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid2.Rows[i].Cells[0].Style.ForeColor = System.Drawing.Color.Black;
            }

            foreach (DataGridViewColumn column in metroGrid2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            metroGrid2.Columns[0].Width = (int)(metroGrid2.Width * 0.26);
            metroGrid2.Columns[1].Width = (int)(metroGrid2.Width * 0.24);
            metroGrid2.Columns[2].Width = (int)(metroGrid2.Width * 0.24);
            metroGrid2.Columns[3].Width = (int)(metroGrid2.Width * 0.24);


            int X = metroGrid2.ColumnHeadersHeight + metroGrid2.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid2.Height = X + 10;
            this.metroGrid2.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            this.metroGrid2.ColumnHeadersDefaultCellStyle.Font = new Font("나눔고딕", 9);
        }
        private void DrawSCurveData3(string[] D1, string[] D2)
        {
            double[] Domestic = Array.ConvertAll(D1, x => Convert.ToDouble(x));
            double[] International = Array.ConvertAll(D2, x => Convert.ToDouble(x));

            int SeperatorIndex = Domestic.Length - 1;
            int DomesticIndex = Domestic.Length - 1;
            int InternationalIndex = Domestic.Length - 1;

            for (int i = 0; i < Domestic.Length; i++)
            {
                if (Domestic[i] >= International[i])
                { SeperatorIndex = i; break; }
            }
            for (int i = 0; i < Domestic.Length; i++)
            {
                if (Domestic[i] >= GotoWarPeriod)
                { DomesticIndex = i; break; }
            }
            for (int i = 0; i < Domestic.Length; i++)
            {
                if (International[i] >= GotoWarPeriod)
                { InternationalIndex = i; break; }
            }


            cartesianChart3.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "국내 기술 수준",
                    Values = new ChartValues<double>(Domestic),
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    LabelPoint = point => String.Format("{0:F2}", point.Y) + "%",
                },
                new LineSeries
                {
                    Title = "원천 기술 보유국 수준",
                    Values = new ChartValues<double>(International),
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    LabelPoint = point => String.Format("{0:F2}", point.Y) + "%",
                }
            };

            string[] X_Label = new string[Domestic.Length];
            for (int j = 0; j < ScurveData.GetLength(0); j++)
            {
                X_Label[j] = ScurveData[j, 0].ToString();
            }
            cartesianChart3.AxisX.Add(new Axis
            {
                Title = "Year",
                Labels = X_Label,
                //   IsMerged = true,
                Separator = new Separator
                {
                    Step = DomesticIndex,
                    //   Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                },
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                        Label = "국내전력화시기",
                        FromValue = DomesticIndex,
                        ToValue = DomesticIndex,

                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(33,149,242)),
                        StrokeThickness = 1,
                        StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 3 }),
                    },
                    new AxisSection
                    {
                        Label = "해외전력화시기",
                        FromValue = InternationalIndex,
                        ToValue = InternationalIndex,
                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(243,67,54)),
                        StrokeThickness = 1,
                        StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 3 }),
                    }
                },
            });

            cartesianChart3.AxisY.Add(new Axis
            {
                Title = "Level",
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                       // Label = "전력화 기술수준",
                        FromValue = 90,
                        ToValue = 90,
                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(153,180,51)),
                        StrokeThickness = 1,
                    },
                    new AxisSection
                    {
                        Label = "전력화 기술수준",
                        FromValue = 95,
                        ToValue = 95,
                        Fill = new SolidColorBrush
                        {
                            Color = System.Windows.Media.Color.FromRgb(0,0,0),
                            Opacity = .0
                        }

                    }
                },
                Separator = new Separator
                {
                    Step = 10,
                }
            });

            cartesianChart3.AxisY[0].MinValue = 0; // lets force the axis to be 100ms ahead
            cartesianChart3.AxisY[0].MaxValue = 100;
            cartesianChart3.Font = new System.Drawing.Font("나눔고딕", 9);

            cartesianChart3.LegendLocation = LegendLocation.Bottom;

            Title3.Text = "<" + Title[2] + ">";
            Title3.Location = new Point(((int)(this.Width / 4)) - ((int)(Title3.Width / 2)), 395);

            metroGrid3.Rows.Clear();
            metroGrid3.RowHeadersVisible = false;
            metroGrid3.ColumnCount = 4;

            metroGrid3.Columns[0].Name = "";
            metroGrid3.Columns[1].Name = "기술보유국";
            metroGrid3.Columns[2].Name = "국내수준";
            metroGrid3.Columns[3].Name = "기술격차";

            metroGrid3.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(32, 56, 100);
            metroGrid3.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;

            int YearIndex = Convert.ToInt32(Year) - Convert.ToInt32(X_Label[0]);

            metroGrid3.Rows.Add("현재", String.Format("{0:F2}", International[YearIndex]) + "%", String.Format("{0:F2}", Domestic[YearIndex]) + "%", String.Format("{0:F2}", International[YearIndex] - Domestic[YearIndex]) + "%");
            metroGrid3.Rows.Add("5년후", String.Format("{0:F2}", International[YearIndex + 5]) + "%", String.Format("{0:F2}", Domestic[YearIndex + 5]) + "%", String.Format("{0:F2}", International[YearIndex + 5] - Domestic[YearIndex + 5]) + "%");
            metroGrid3.Rows.Add("전력화 예상시점", Convert.ToInt32(X_Label[0]) + InternationalIndex + "년", Convert.ToInt32(X_Label[0]) + DomesticIndex + "년", DomesticIndex - InternationalIndex + "년");


            metroGrid3.CurrentCell = null;
            for (int i = 0; i < metroGrid3.RowCount; i++)
            {
                for (int j = 0; j < metroGrid3.ColumnCount; j++)
                {
                    metroGrid3.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
                metroGrid3.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid3.Rows[i].Cells[0].Style.ForeColor = System.Drawing.Color.Black;
            }

            foreach (DataGridViewColumn column in metroGrid3.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            metroGrid3.Columns[0].Width = (int)(metroGrid3.Width * 0.26);
            metroGrid3.Columns[1].Width = (int)(metroGrid3.Width * 0.24);
            metroGrid3.Columns[2].Width = (int)(metroGrid3.Width * 0.24);
            metroGrid3.Columns[3].Width = (int)(metroGrid3.Width * 0.24);


            int X = metroGrid3.ColumnHeadersHeight + metroGrid3.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid3.Height = X + 10;
            this.metroGrid3.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            this.metroGrid3.ColumnHeadersDefaultCellStyle.Font = new Font("나눔고딕", 9);

        }
        private void DrawSCurveData4(string[] D1, string[] D2)
        {
            double[] Domestic = Array.ConvertAll(D1, x => Convert.ToDouble(x));
            double[] International = Array.ConvertAll(D2, x => Convert.ToDouble(x));

            int SeperatorIndex = Domestic.Length - 1;
            int DomesticIndex = Domestic.Length - 1;
            int InternationalIndex = Domestic.Length - 1;

            for (int i = 0; i < Domestic.Length; i++)
            {
                if (Domestic[i] >= International[i])
                { SeperatorIndex = i; break; }
            }
            for (int i = 0; i < Domestic.Length; i++)
            {
                if (Domestic[i] >= GotoWarPeriod)
                { DomesticIndex = i; break; }
            }
            for (int i = 0; i < Domestic.Length; i++)
            {
                if (International[i] >= GotoWarPeriod)
                { InternationalIndex = i; break; }
            }


            cartesianChart4.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "국내 기술 수준",
                    Values = new ChartValues<double>(Domestic),
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    LabelPoint = point => String.Format("{0:F2}", point.Y) + "%",
                },
                new LineSeries
                {
                    Title = "원천 기술 보유국 수준",
                    Values = new ChartValues<double>(International),
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    LabelPoint = point => String.Format("{0:F2}", point.Y) + "%",
                }
            };

            string[] X_Label = new string[Domestic.Length];
            for (int j = 0; j < ScurveData.GetLength(0); j++)
            {
                X_Label[j] = ScurveData[j, 0].ToString();
            }
            cartesianChart4.AxisX.Add(new Axis
            {
                Title = "Year",
                Labels = X_Label,
                //   IsMerged = true,
                Separator = new Separator
                {
                    Step = DomesticIndex,
                    //   Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                },
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                        Label = "국내전력화시기",
                        FromValue = DomesticIndex,
                        ToValue = DomesticIndex,

                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(33,149,242)),
                        StrokeThickness = 1,
                        StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 3 }),
                    },
                    new AxisSection
                    {
                        Label = "해외전력화시기",
                        FromValue = InternationalIndex,
                        ToValue = InternationalIndex,
                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(243,67,54)),
                        StrokeThickness = 1,
                        StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 3 }),
                    }
                },
            });

            cartesianChart4.AxisY.Add(new Axis
            {
                Title = "Level",
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                       // Label = "전력화 기술수준",
                        FromValue = 90,
                        ToValue = 90,
                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(153,180,51)),
                        StrokeThickness = 1,
                    },
                    new AxisSection
                    {
                        Label = "전력화 기술수준",
                        FromValue = 95,
                        ToValue = 95,
                        Fill = new SolidColorBrush
                        {
                            Color = System.Windows.Media.Color.FromRgb(0,0,0),
                            Opacity = .0
                        }

                    }
                },
                Separator = new Separator
                {
                    Step = 10,
                }
            });

            cartesianChart4.AxisY[0].MinValue = 0; // lets force the axis to be 100ms ahead
            cartesianChart4.AxisY[0].MaxValue = 100;
            cartesianChart4.Font = new System.Drawing.Font("나눔고딕", 9);
            
            cartesianChart4.LegendLocation = LegendLocation.Bottom;

            Title4.Text = "<" + Title[3] + ">";
            Title4.Location = new Point((((int)(this.Width / 4)) * 3) - ((int)(Title4.Width / 2)), 395);


            metroGrid4.Rows.Clear();
            metroGrid4.RowHeadersVisible = false;
            metroGrid4.ColumnCount = 4;

            metroGrid4.Columns[0].Name = "";
            metroGrid4.Columns[1].Name = "기술보유국";
            metroGrid4.Columns[2].Name = "국내수준";
            metroGrid4.Columns[3].Name = "기술격차";

            metroGrid4.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(32, 56, 100);
            metroGrid4.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;

            int YearIndex = Convert.ToInt32(Year) - Convert.ToInt32(X_Label[0]);

            metroGrid4.Rows.Add("현재", String.Format("{0:F2}", International[YearIndex]) + "%", String.Format("{0:F2}", Domestic[YearIndex]) + "%", String.Format("{0:F2}", International[YearIndex] - Domestic[YearIndex]) + "%");
            metroGrid4.Rows.Add("5년후", String.Format("{0:F2}", International[YearIndex + 5]) + "%", String.Format("{0:F2}", Domestic[YearIndex + 5]) + "%", String.Format("{0:F2}", International[YearIndex + 5] - Domestic[YearIndex + 5]) + "%");
            metroGrid4.Rows.Add("전력화 예상시점", Convert.ToInt32(X_Label[0]) + InternationalIndex + "년", Convert.ToInt32(X_Label[0]) + DomesticIndex + "년", DomesticIndex - InternationalIndex + "년");


            metroGrid4.CurrentCell = null;
            for (int i = 0; i < metroGrid4.RowCount; i++)
            {
                for (int j = 0; j < metroGrid4.ColumnCount; j++)
                {
                    metroGrid4.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
                metroGrid4.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid4.Rows[i].Cells[0].Style.ForeColor = System.Drawing.Color.Black;
            }

            foreach (DataGridViewColumn column in metroGrid4.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            metroGrid4.Columns[0].Width = (int)(metroGrid4.Width * 0.26);
            metroGrid4.Columns[1].Width = (int)(metroGrid4.Width * 0.24);
            metroGrid4.Columns[2].Width = (int)(metroGrid4.Width * 0.24);
            metroGrid4.Columns[3].Width = (int)(metroGrid4.Width * 0.24);


            int X = metroGrid4.ColumnHeadersHeight + metroGrid4.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid4.Height = X + 10;
            this.metroGrid4.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            this.metroGrid4.ColumnHeadersDefaultCellStyle.Font = new Font("나눔고딕", 9);
        }


        private void OriginalTechnology2_Load(object sender, EventArgs e)
        {
            Title = new List<string>();
            for (int i = 0; i < OriginalTechnology1.MainTechnology[mainFrm.CurrentWeapon].Length; i++)
            {
                if (OriginalTechnology1.MainTechnology[mainFrm.CurrentWeapon][i] == true)
                {
                    for (int j = 0; j < OriginalTechnology1.SubTechnology[mainFrm.CurrentWeapon][i].Length; j++)
                    {
                        Title.Add(OriginalTechnology1.SubTechnology[mainFrm.CurrentWeapon][i][j]);
                    }

                }

            }

            ReadExcelData();
            
            string[] Domestic = new string[ScurveData.GetLength(0)];
            string[] International = new string[ScurveData.GetLength(0)];
            for (int j = 0; j < ScurveData.GetLength(0); j++)
            {
                Domestic[j] = ScurveData[j, 2].ToString();
                International[j] = ScurveData[j, 3].ToString();
            }
            DrawSCurveData1(Domestic, International);

            Domestic.Initialize();
            International.Initialize();

            for (int j = 0; j < ScurveData.GetLength(0); j++)
            {
                Domestic[j] = ScurveData[j, 4].ToString();
                International[j] = ScurveData[j, 5].ToString();
            }
            DrawSCurveData2(Domestic, International);

            Domestic.Initialize();
            International.Initialize();

            for (int j = 0; j < ScurveData.GetLength(0); j++)
            {
                Domestic[j] = ScurveData[j, 6].ToString();
                International[j] = ScurveData[j, 7].ToString();
            }
            DrawSCurveData3(Domestic, International);

            Domestic.Initialize();
            International.Initialize();

            for (int j = 0; j < ScurveData.GetLength(0); j++)
            {
                Domestic[j] = ScurveData[j, 8].ToString();
                International[j] = ScurveData[j, 9].ToString();
            }
            DrawSCurveData4(Domestic, International);

            /*
            double[] xInitial = new double[] { -1, 1 };
            double[] xLower = new double[] { -1, -1 };
            double[] xUpper = new double[] { 1, 1 };

            var solution = NelderMeadSolver.Solve(
x => (100 * Math.Pow(x[1] - Math.Pow(x[0], 2), 2)) + Math.Pow(1 - x[0], 2), xInitial, xLower, xUpper);

            Console.WriteLine(solution.Result);
            Console.WriteLine("solution = {0}", solution.GetSolutionValue(0));
            Console.WriteLine("x = {0}", solution.GetValue(1));
            Console.WriteLine("y = {0}", solution.GetValue(2));

            */
            /*
            var solver = SolverContext.GetContext();
            solver.ClearModel();
            var model = solver.CreateModel();

            double InputValue1 = 69;
            double InputValue2 = 80;

            int StartYear = 1983;
            int EndYear = 2050;

            int ObjectiveYear1 = 2013;
            int ObjectiveYear2 = 2018;

            int Index1 = ObjectiveYear1 - StartYear + 1;
            int Index2 = ObjectiveYear2 - StartYear + 1;

            
            Decision d1 = new Decision(Domain.RealNonnegative, "d1");
            Decision d2 = new Decision(Domain.RealNonnegative, "d2");
                        
            model.AddDecisions(d1, d2);

            Term a = 100 * Model.Exp(-d1 * Model.Exp(-d2 * 31));
            Term b = 100 * Model.Exp(-d1 * Model.Exp(-d2 * 36));

            Term Goal = (a - InputValue1) + (b - InputValue2);
            model.AddConstraints("total",

                0 <= Goal
               
           
                );
            
            


            model.AddGoal("goal", GoalKind.Minimize, Goal);

            var solution = solver.Solve();

            int aasd = 10;
            Report report = solution.GetReport();
            */

        }
    }
}

