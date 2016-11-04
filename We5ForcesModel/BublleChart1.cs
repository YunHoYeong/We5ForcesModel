using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;


namespace We5ForcesModel
{
    public partial class BublleChart1 : Form
    {
        public BublleChart1()
        {
            InitializeComponent();
            
            Random r = new Random();
            /*
            MariaSeries = new ScatterSeries[2];
            MariaSeries[0] = new ScatterSeries
            {
                Values = new ChartValues<ObservablePoint> { new ObservablePoint(r.NextDouble(), r.NextDouble()) },
            };
            CharlesSeries = new ScatterSeries
            {
                Values = new ChartValues<ObservablePoint> { new ObservablePoint(r.NextDouble(), r.NextDouble()) },
            };
            JohnSeries = new ScatterSeries
            {
                Values = new ChartValues<ObservablePoint> { new ObservablePoint(r.NextDouble(), r.NextDouble()) },
            };

            cartesianChart1.Series = new SeriesCollection();
            cartesianChart1.Series.Add(MariaSeries[0]);
            cartesianChart1.Series.Add(CharlesSeries);
            cartesianChart1.Series.Add(JohnSeries);

            */
            /*
            for (int i = 1; i < 3; i++)
            {
                cartesianChart1.Series.Add(new ScatterSeries
                {
                    Title = i.ToString(),
                    Values = new ChartValues<ObservablePoint> { new ObservablePoint(0.2, 0.3) },
                    PointGeometry = DefaultGeometries.Triangle,
                    StrokeThickness = 2,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(192, 168, 100))
                });
            }
            */
            /*
            var r = new Random();

            foreach (var series in SeriesCollection)
            {
                for (var i = 0; i < 20; i++)
                {
                    series.Values.Add(new ObservablePoint(r.NextDouble(), r.NextDouble()));
                }
            }
            */

            cartesianChart1.AxisX.Add(new Axis
            {
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                        FromValue = 0,
                        ToValue = 2.5,
                        Fill = new SolidColorBrush
                        {
                            Color = System.Windows.Media.Color.FromRgb(254,132,132),
                            Opacity = .1
                        }
                    }
                }
            });
            cartesianChart1.AxisY.Add(new Axis
            {
                Sections = new SectionsCollection
                {
                    new AxisSection
                    {
                        FromValue = 0,
                        ToValue = 2.5,
                        Fill = new SolidColorBrush
                        {
                            Color = System.Windows.Media.Color.FromRgb(254,132,132),
                            Opacity = .1
                        }
                    }
                }
            });
            
            cartesianChart1.LegendLocation = LegendLocation.Top;


            SetAxisLimits();


        }
        public ScatterSeries[] Quartile1 { get; set; }
        public ScatterSeries[] Quartile2 { get; set; }
        public ScatterSeries[] Quartile3 { get; set; }
        public ScatterSeries[] Quartile4 { get; set; }

        private void SetAxisLimits()
        {
            cartesianChart1.AxisX[0].MinValue = -2; // lets force the axis to be 100ms ahead
            cartesianChart1.AxisX[0].MaxValue = 2; //we only care about the last 8 seconds


            cartesianChart1.AxisY[0].MinValue = -2; // lets force the axis to be 100ms ahead
            cartesianChart1.AxisY[0].MaxValue = 2; //we only care about the last 8 seconds
                                                   //    cartesianChart1.DataTooltip = 


        }

        public SeriesCollection SeriesCollection { get; set; }

    }
}
