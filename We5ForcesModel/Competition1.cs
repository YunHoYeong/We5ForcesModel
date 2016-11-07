using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using System.Xml.Linq;
using LiveCharts.Configurations;
using System.Data.OleDb;

namespace We5ForcesModel
{
    public partial class Competition1 : Form
    {
        public string[] AreaName = new string[] { "미주", "유럽", "아시아", "중동", "아프리카" };
        public static string[] CompetitionDrawMap;

        public static List<string> WorldCode = new List<string>();
        public static List<string> WorldName = new List<string>();
        public Competition1()
        {
            InitializeComponent();
            
            // 국가코드가 필요해서 가져옴
            var xmlStr = File.ReadAllText("Maps/World.xml");
            string pattern = @"<(.*)>(.*)</\1>";
            
            foreach (Match m in Regex.Matches(xmlStr, pattern))
            {
                if (m.Groups[1].Value == "Id") { WorldCode.Add(m.Groups[2].Value); }
                else if (m.Groups[1].Value == "Name") { WorldName.Add(m.Groups[2].Value); }
            }
        

            // 무기체계 DB에서 국가 뭐뭐있는지 추출해옴
            // 경쟁무기에 뭐가 있는지 추출해옴
            CompetitionDrawMap = new string[mainFrm.CompetitionData.GetLength(0)];
            for (int i = 1; i < mainFrm.CompetitionData.GetLength(0); i++)
            {
                CompetitionDrawMap[i - 1] = mainFrm.CompetitionData[i, 1].ToString();
            }
            // 중복을 제거하고 난 뒤, Null 값도 제거하고
            CompetitionDrawMap = GetDistinctValues<string>(CompetitionDrawMap);
            CompetitionDrawMap = CompetitionDrawMap.Where(condition => condition != null).ToArray();

            var r = new Random();
            var values = new Dictionary<string, double>();

            for (int i = 0; i < CompetitionDrawMap.Length; i++)
            {
                values[WorldCode[WorldName.IndexOf(CompetitionDrawMap[i])]] = r.Next(0, 100); ;
            }
            
            
            geoMap1.HeatMap = values;
            geoMap1.Source = "Maps/World.xml";
        }
        public static string[] WeaponName = new string[] { "수직이착륙 UAV", "무인수상정", "복합화기원격사격통제체계", "레이저대공무기", "경어뢰 성능개량", "차기잠수함구조함" };
       
        private void WeaponIntroduction()
        {
            // RCWS 기준
            string[] Introduction = new string[6]
                {"ABCDE" ,
                 "ABCDE",
                "  기관총 또는 자동 유탄 발사기 등의 타격 체계와 감시 체계가 통합된 무장 장치를 외부 또는 전차 및 장갑차에 탑재하여, 타격체계를 사람이 직접 조작하지 않고, 원격통제장치에 의해 조작하는 체계로 전투원의 생존성 보장 및 정밀타격을 위해 운용하는 무기체계"
                ,"","",""};

            //  lbl_Introduction.Text = Introduction[2];
            metroGrid1.Rows.Clear();
            metroGrid1.RowHeadersVisible = false;
            metroGrid1.ColumnCount = 2;

            metroGrid1.Columns[0].Name = "무기체계명";
            metroGrid1.Columns[1].Name = "개 요";

            metroGrid1.Columns[0].Width = (int)(metroGrid1.Width * 0.25);
            metroGrid1.Columns[1].Width = (int)(metroGrid1.Width * 0.74);

            // Clumn Header 변경
            // 개요
            metroGrid1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 56, 100);
            metroGrid1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            
            // 무기체계명 
            DataGridViewColumn dataGridViewColumn = metroGrid1.Columns[0];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.FromArgb(91, 155, 213);
            dataGridViewColumn.HeaderCell.Style.ForeColor = Color.White;

            
            for (int i = 0; i < metroGrid1.Columns.Count; i++)
            {
                metroGrid1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            metroGrid1.Rows.Add(WeaponName[2], Introduction[2]);
            metroGrid1.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 무기체계 작성 Cell 스타일
            metroGrid1.Rows[0].Cells[0].Style.BackColor = Color.FromArgb(222, 235, 247);
            metroGrid1.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);

            metroGrid1.Rows[0].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            
            metroGrid1.CurrentCell = null;
            foreach (DataGridViewColumn column in metroGrid1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        public T[] GetDistinctValues<T>(T[] array)
        {
            List<T> tmp = new List<T>();

            for (int i = 0; i < array.Length; i++)
            {
                if (tmp.Contains(array[i]))
                    continue;
                tmp.Add(array[i]);
            }

            return tmp.ToArray();
        }

        private void DrawPieChart()
        {
            // 무기체계 DB에서 국가 뭐뭐있는지 추출해옴
            // 경쟁무기에 뭐가 있는지 추출해옴
            string[] CompetitionData = new string[mainFrm.CompetitionData.GetLength(0)];
            for (int i = 1; i < mainFrm.CompetitionData.GetLength(0); i++)
            {
                CompetitionData[i - 1] = mainFrm.CompetitionData[i, 5].ToString();
            }
            CompetitionData = CompetitionData.Where(condition => condition != null).ToArray();

            string[] FirmData = GetDistinctValues<string>(CompetitionData);
            int[] Quantity = new int[FirmData.Length];


            for (int i = 1; i < CompetitionData.GetLength(0); i++)
            {
                for (int j = 0; j < FirmData.Length; j++)
                {
                    if (String.Compare(CompetitionData[i], FirmData[j]) == 0)
                    {
                        Quantity[j] += Convert.ToInt32(mainFrm.CompetitionData[i, 2]);
                    }
                }
            }

            //   Func<ChartPoint, string> labelPoint = chartPoint =>
            //    string.Format("{0:#,##0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format("","",null,null);


            pieChart1.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = FirmData[0],
                    Values = new ChartValues<double> { Quantity[0]},
                    PushOut = 0,
                    DataLabels = true,
                    LabelPoint = labelPoint
                }
            };
            for(int i = 1; i < Quantity.Length; i++)
            {
                pieChart1.Series.Add(new PieSeries
                {
                    Title = FirmData[i],
                    Values = new ChartValues<double> { Quantity[i] },
                    PushOut = 0,
                    
                    DataLabels = true,
                    LabelPoint = labelPoint
                });
            }
           

            pieChart1.Font = new Font("나눔고딕", 9);
            pieChart1.LegendLocation = LegendLocation.Bottom;
            
        }


        private void DrawAreaGrid()
        {
            metroGrid2.Rows.Clear();
            metroGrid2.RowHeadersVisible = false;
            metroGrid2.ColumnCount = 5;

            metroGrid2.Columns[0].Name = "권 역";
            metroGrid2.Columns[1].Name = "운용국가";
            metroGrid2.Columns[2].Name = "보유대수";
            metroGrid2.Columns[3].Name = "점유율";
            metroGrid2.Columns[4].Name = "금액규모($M)";
            
            // Clumn Header 변경
            // 개요
            metroGrid2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 56, 100);
            metroGrid2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            metroGrid2.ColumnHeadersDefaultCellStyle.Font = new Font("나눔고딕", 9);
            metroGrid2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in metroGrid2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // 무기체계명 
            DataGridViewColumn dataGridViewColumn = metroGrid2.Columns[0];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.FromArgb(91, 155, 213);
            dataGridViewColumn.HeaderCell.Style.ForeColor = Color.White;

            int[] cntArea = new int[5];
            int[] QuantityArea = new int[5];
            double[] Prices = new double[5];

            for (int i = 1; i < mainFrm.CompetitionData.GetLength(0); i++)
            {
                if(mainFrm.CompetitionData[i, 0].ToString() == "미주") { cntArea[0]++; QuantityArea[0] += Convert.ToInt32(mainFrm.CompetitionData[i, 2]); Prices[0] += Convert.ToDouble(mainFrm.CompetitionData[i, 2]) * Convert.ToDouble(mainFrm.CompetitionData[i, 7]); }
                else if (mainFrm.CompetitionData[i, 0].ToString() == "유럽") { cntArea[1]++; QuantityArea[1] += Convert.ToInt32(mainFrm.CompetitionData[i, 2]); Prices[1] += Convert.ToDouble(mainFrm.CompetitionData[i, 2]) * Convert.ToDouble(mainFrm.CompetitionData[i, 7]); }
                else if (mainFrm.CompetitionData[i, 0].ToString() == "아시아") { cntArea[2]++; QuantityArea[2] += Convert.ToInt32(mainFrm.CompetitionData[i, 2]); Prices[2] += Convert.ToDouble(mainFrm.CompetitionData[i, 2]) * Convert.ToDouble(mainFrm.CompetitionData[i, 7]); }
                else if (mainFrm.CompetitionData[i, 0].ToString() == "중동") { cntArea[3]++; QuantityArea[3] += Convert.ToInt32(mainFrm.CompetitionData[i, 2]); Prices[3] += Convert.ToDouble(mainFrm.CompetitionData[i, 2]) * Convert.ToDouble(mainFrm.CompetitionData[i, 7]); }
                else if (mainFrm.CompetitionData[i, 0].ToString() == "아프리카") { cntArea[4]++; QuantityArea[4] += Convert.ToInt32(mainFrm.CompetitionData[i, 2]); Prices[4] += Convert.ToDouble(mainFrm.CompetitionData[i, 2]) * Convert.ToDouble(mainFrm.CompetitionData[i, 7]); }
            }
            
            metroGrid2.Rows.Add("미주", string.Format("{0:#,##0}", cntArea[0]), string.Format("{0:#,##0}", QuantityArea[0]), String.Format ("{0:P}",(double)QuantityArea[0]/QuantityArea.Sum()), String.Format("{0:F2}", Prices[0]));
            metroGrid2.Rows.Add("유럽", string.Format("{0:#,##0}", cntArea[1]), string.Format("{0:#,##0}", QuantityArea[1]), String.Format("{0:P}", (double)QuantityArea[1] / QuantityArea.Sum()), String.Format("{0:F2}", Prices[1]));
            metroGrid2.Rows.Add("아시아", string.Format("{0:#,##0}", cntArea[2]), string.Format("{0:#,##0}", QuantityArea[2]), String.Format("{0:P}", (double)QuantityArea[2] / QuantityArea.Sum()), String.Format("{0:F2}", Prices[2]));
            metroGrid2.Rows.Add("중동", string.Format("{0:#,##0}", cntArea[3]), string.Format("{0:#,##0}", QuantityArea[3]), String.Format("{0:P}", (double)QuantityArea[3] / QuantityArea.Sum()), String.Format("{0:F2}", Prices[3]));
            metroGrid2.Rows.Add("아프리카", string.Format("{0:#,##0}", cntArea[4]), string.Format("{0:#,##0}", QuantityArea[4]), String.Format("{0:P}", (double)QuantityArea[4] / QuantityArea.Sum()), String.Format("{0:F2}", Prices[4]));
            metroGrid2.Rows.Add("계", string.Format("{0:#,##0}", cntArea.Sum()), string.Format("{0:#,##0}", QuantityArea.Sum()), String.Format("{0:P}", 1), String.Format("{0:F2}", Prices.Sum()));

            
            this.metroGrid2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.metroGrid2.DefaultCellStyle.Font = new Font("나눔고딕", 9);
            for (int i = 0; i < metroGrid2.ColumnCount; i++)
            {
                metroGrid2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            
            for(int i = 0; i < metroGrid2.RowCount; i++)
            {
                metroGrid2.Rows[i].Height = (int)((geoMap1.Height - (metroGrid2.Location.Y - geoMap1.Location.Y) -  metroGrid2.ColumnHeadersHeight) /(double)(metroGrid2.RowCount));
            }
            metroGrid2.CurrentCell = null;

            // 가장 높은 열을 빨간색으로 표시
            int[] MaxIndex = new int[4];
            for(int i = 1; i < metroGrid2.ColumnCount; i++)
            {
                double minValue = -1;
                if (i == 3) continue;
                for (int j = 0; j < metroGrid2.RowCount - 1; j++)
                {
                    if(Convert.ToDouble(metroGrid2.Rows[j].Cells[i].Value) > minValue)
                    {
                        minValue = Convert.ToDouble(metroGrid2.Rows[j].Cells[i].Value);
                        MaxIndex[i - 1] = j;
                    }
                }
            }

            metroGrid2.Rows[MaxIndex[0]].Cells[1].Style.ForeColor = Color.Red;
            metroGrid2.Rows[MaxIndex[1]].Cells[2].Style.ForeColor = Color.Red;
            metroGrid2.Rows[MaxIndex[1]].Cells[3].Style.ForeColor = Color.Red;
            metroGrid2.Rows[MaxIndex[3]].Cells[4].Style.ForeColor = Color.Red;


            DrawColumnChart();
        }
        private void DrawColumnChart()
        {
            // 경쟁무기에 뭐가 있는지 추출해옴
            string[] CompetitionWeapon = new string[mainFrm.CompetitionData.GetLength(0)];
            for (int i = 1; i < mainFrm.CompetitionData.GetLength(0); i++)
            {
                CompetitionWeapon[i - 1] = mainFrm.CompetitionData[i, 3].ToString();
            }
            // 중복을 제거하고 난 뒤, Null 값도 제거하고
            CompetitionWeapon = GetDistinctValues<string>(CompetitionWeapon);
            CompetitionWeapon = CompetitionWeapon.Where(condition => condition != null).ToArray();

            int[][] cntCompetitionWeapon = new int[CompetitionWeapon.Length][];
            for(int i = 0; i < cntCompetitionWeapon.GetLength(0); i++)
            {
                cntCompetitionWeapon[i] = new int[5];
            }

            // 무기 모델별로 권역별 횟수를 Check
            for (int i = 1; i < mainFrm.CompetitionData.GetLength(0); i++)
            {
                for (int j = 0; j < CompetitionWeapon.Length; j++)
                {
                    if (mainFrm.CompetitionData[i, 0].ToString() == "미주")
                    {
                        if (CompetitionWeapon[j].CompareTo(mainFrm.CompetitionData[i, 3]) == 0) { cntCompetitionWeapon[j][0] += Convert.ToInt32(mainFrm.CompetitionData[i, 2]); }
                    }
                    else if (mainFrm.CompetitionData[i, 0].ToString() == "유럽")
                    {
                        if (CompetitionWeapon[j].CompareTo(mainFrm.CompetitionData[i, 3]) == 0) { cntCompetitionWeapon[j][1] += Convert.ToInt32(mainFrm.CompetitionData[i, 2]); }
                    }
                    else if (mainFrm.CompetitionData[i, 0].ToString() == "아시아")
                    {
                        if (CompetitionWeapon[j].CompareTo(mainFrm.CompetitionData[i, 3]) == 0) { cntCompetitionWeapon[j][2] += Convert.ToInt32(mainFrm.CompetitionData[i, 2]); }
                    }
                    else if (mainFrm.CompetitionData[i, 0].ToString() == "중동")
                    {
                        if (CompetitionWeapon[j].CompareTo(mainFrm.CompetitionData[i, 3]) == 0) { cntCompetitionWeapon[j][3] += Convert.ToInt32(mainFrm.CompetitionData[i, 2]); }
                    }
                    else if (mainFrm.CompetitionData[i, 0].ToString() == "아프리카")
                    {
                        if (CompetitionWeapon[j].CompareTo(mainFrm.CompetitionData[i, 3]) == 0) { cntCompetitionWeapon[j][4] += Convert.ToInt32(mainFrm.CompetitionData[i, 2]); }
                    }
                }
            }

            int[] cntAmerica = new int[CompetitionWeapon.Length];
            int[] cntEurope = new int[CompetitionWeapon.Length];
            int[] cntAsia = new int[CompetitionWeapon.Length];
            int[] cntMiddleAsia = new int[CompetitionWeapon.Length];
            int[] cntAfrica = new int[CompetitionWeapon.Length];

            for (int i = 0; i < cntCompetitionWeapon.GetLength(0); i++)
            {
                cntAmerica[i] = cntCompetitionWeapon[i][0];
                cntEurope[i] = cntCompetitionWeapon[i][1];
                cntAsia[i] = cntCompetitionWeapon[i][2];
                cntMiddleAsia[i] = cntCompetitionWeapon[i][3];
                cntAfrica[i] = cntCompetitionWeapon[i][4];
            }

            Func<ChartPoint, string> labelPoint = chartPoint =>
              string.Format("{0:#,##0}", chartPoint.Y);
            
            myChart.Series = new SeriesCollection
            {
                new StackedRowSeries
                {
                    Values = new ChartValues<int> (cntAmerica),
                    DataLabels = true,
                    Title = "미주"
                },
                new StackedRowSeries
                {
                    Values = new ChartValues<int> (cntEurope),
                   DataLabels = true,
                   Title = "유럽"
                },
                new StackedRowSeries
                {
                    Values = new ChartValues<int> (cntAsia),
                    DataLabels = true,
                    Title = "아시아"
                }
                ,
                new StackedRowSeries
                {
                    Values = new ChartValues<int> (cntMiddleAsia),
                    DataLabels = true,
                    Title = "중동"
                }
                ,
                new StackedRowSeries
                {
                    Values = new ChartValues<int> (cntAfrica),
                    DataLabels = true,
                    Title = "아프리카"
                }
            };

            myChart.AxisX.Add(new Axis
            {
                Title = "Quantity",
            });

            myChart.AxisY.Add(new Axis
            {
                Title = "Model",
                Labels = CompetitionWeapon,
            });
            int[] MaxValueArray = new int[] { cntAfrica.Max(), cntEurope.Max(), cntAsia.Max(), cntMiddleAsia.Max(), cntAfrica.Max() };

            myChart.AxisX[0].MaxValue = (int)(MaxValueArray.Max() * 1.1);
            myChart.LegendLocation = LegendLocation.Bottom;
        }
        private void Competition1_Load(object sender, EventArgs e)
        {
            WeaponIntroduction();
            DrawPieChart();
            DrawAreaGrid();
        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {
            metroGrid1.ClearSelection();
        }

        private void geoMap1_LandClick(object arg1, LiveCharts.Maps.MapData arg2)
        {
            bigMap frmBigMap = new bigMap();
            frmBigMap.Show();
        }

        private void metroGrid2_SelectionChanged(object sender, EventArgs e)
        {
            metroGrid2.ClearSelection();
        }

        private void metroGrid2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = metroGrid2.Rows[e.RowIndex];// get you required index
                                                         // check the cell value under your specific column and then you can toggle your colors
            row.DefaultCellStyle.BackColor = Color.White;

        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {

        }

        private void myChart_DataClick(object arg1, ChartPoint arg2)
        {
            bigChart1 FrmbigChart1 = new bigChart1();
            FrmbigChart1.Show();
        }
    }
}
