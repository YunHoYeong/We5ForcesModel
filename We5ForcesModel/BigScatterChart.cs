using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.IO;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
namespace We5ForcesModel
{
    public partial class BigScatterChart : Form
    {
        public BigScatterChart()
        {
            InitializeComponent();

            for (int i = 0; i < PromisingNation1.SelectedNationIndex.Count(); i++)
            {
                string NationName = PromisingNation1.SelectedNationName[i];
                PromisingNation1.Quartile1[i] = new ScatterSeries
                {
                    Title = "",
                    Values = new ChartValues<ObservablePoint> { new ObservablePoint(PromisingNation1.zPoint1[PromisingNation1.SelectedNationIndex[i]], PromisingNation1.zPoint2[PromisingNation1.SelectedNationIndex[i]]) },
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Transparent,
                    DataLabels = false,
                    LabelPoint = value => NationName,
                    DataContext = "",
                };
            }
            int j = 0;
            int k = 0;
            for (int i = 0; i < PromisingNation1.NationPoint.GetLength(0); i++)
            {
                string NationName = PromisingNation1.NationDB[i + 1, 2].ToString();
                if (PromisingNation1.zPoint1[i] <= 0 || PromisingNation1.zPoint2[i] <= 0)
                {
                    if (PromisingNation1.zPoint1[i] <= 0 && PromisingNation1.zPoint2[i] <= 0)
                    {
                        PromisingNation1.Quartile4[k] = new ScatterSeries
                        {
                            Title = "",
                            Values = new ChartValues<ObservablePoint> { new ObservablePoint(PromisingNation1.zPoint1[i], PromisingNation1.zPoint2[i]) },
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
                        PromisingNation1.Quartile2[j] = new ScatterSeries
                        {
                            Title = "",
                            Values = new ChartValues<ObservablePoint> { new ObservablePoint(PromisingNation1.zPoint1[i], PromisingNation1.zPoint2[i]) },
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
            cartesianChart1.Series.AddRange(PromisingNation1.Quartile1);
            cartesianChart1.Series.AddRange(PromisingNation1.Quartile2);
            cartesianChart1.Series.AddRange(PromisingNation1.Quartile4);
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
                        ToValue = PromisingNation1.zPoint1.Max() * 1.1,
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
                        ToValue = PromisingNation1.zPoint2.Max() * 1.15,
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
            cartesianChart1.AxisX[0].MinValue = PromisingNation1.zPoint1.Min() * 0.9; // lets force the axis to be 100ms ahead
            cartesianChart1.AxisX[0].MaxValue = PromisingNation1.zPoint1.Max() * 1.1; //we only care about the last 8 seconds

            cartesianChart1.AxisY[0].MinValue = PromisingNation1.zPoint2.Min() * 0.9; // lets force the axis to be 100ms ahead
            cartesianChart1.AxisY[0].MaxValue = PromisingNation1.zPoint2.Max() * 1.15; //we only care about the last 8 seconds
        }
    

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
