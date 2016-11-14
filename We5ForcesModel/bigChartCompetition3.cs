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

            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries { Title = "",
                    Values = new ChartValues<double>(Competition2.Prices),
                    DataLabels = true,
                    LabelPoint = point => point.Y + "M"}

            };
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Model",
                Labels = Competition2.CompetitionWeapon,
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
