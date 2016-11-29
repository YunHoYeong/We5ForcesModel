using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls

namespace We5ForcesModel
{
    public partial class bigChartCompetition3 : Form
    {
        public bigChartCompetition3()
        {
            InitializeComponent();

            double[] ChartPrices;
            string[] ChartWeaponName;
            if (mainFrm.DomesticSpec[mainFrm.DomesticSpec.Length - 1] != null) // 국내무기체계의 가격이 있으면 + 1
            {
                ChartPrices = new double[Competition2.Prices.Length + 1];
                ChartWeaponName = new string[Competition2.Prices.Length + 1];

                for (int i = 0; i < Competition2.Prices.Length; i++) { ChartPrices[i] = Competition2.Prices[i]; }
                ChartPrices[ChartPrices.Length - 1] = Convert.ToDouble(mainFrm.DomesticSpec[mainFrm.DomesticSpec.Length - 1]);

                for (int i = 0; i < Competition2.Prices.Length; i++) { ChartWeaponName[i] = Competition2.CompetitionWeapon[i]; }
                ChartWeaponName[ChartPrices.Length - 1] = "국내무기체계";
            }
            else
            {
                ChartPrices = new double[Competition2.Prices.Length];
                ChartPrices = (double[])Competition2.Prices.Clone();

                ChartWeaponName = new string[Competition2.CompetitionWeapon.Length];
                ChartWeaponName = (string[])Competition2.CompetitionWeapon.Clone();
            }

            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries { Title = "",
                    Values = new ChartValues<double>(ChartPrices),
                    DataLabels = true,
                    LabelPoint = point => point.Y + "M"}

            };
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Model",
                Labels = ChartWeaponName,
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Prices($M)",
                LabelFormatter = value => value.ToString("N")
            });
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
