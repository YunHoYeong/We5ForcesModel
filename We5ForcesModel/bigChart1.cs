using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace We5ForcesModel
{
    public partial class bigChart1 : Form
    {
        public bigChart1()
        {
            InitializeComponent();

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
            for (int i = 0; i < cntCompetitionWeapon.GetLength(0); i++)
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

        private void bigChart1_Load(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
