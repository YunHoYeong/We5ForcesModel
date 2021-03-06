﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

namespace We5ForcesModel
{
    public partial class OriginalTechnology3 : Form
    {
        public OriginalTechnology3()
        {
            InitializeComponent();
        }
        private void drawRadarChart1(string Title, string[] ChartData)
        {
            string[] xValues = { "기술적 파급효과", "경제적 파급효과", "보호등급", "기술도입 가능성", "민군우위기술", "난이도" };
            double[] yValues = new double[xValues.Length];

            for (int i = 2; i < ChartData.Length; i++)
            {
                if (ChartData[i] == "매우 높음") yValues[i - 2] = 5;
                else if (ChartData[i] == "높음") yValues[i - 2] = 4;
                else if (ChartData[i] == "보통") yValues[i - 2] = 3;
                else if (ChartData[i] == "낮음") yValues[i - 2] = 2;
                else if (ChartData[i] == "매우 낮음") yValues[i - 2] = 1;


                if (ChartData[i] == "국방≫민간") yValues[i - 2] = 5;
                else if (ChartData[i] == "국방>민간") yValues[i - 2] = 4;
                else if (ChartData[i] == "국방≒민간") yValues[i - 2] = 3;
                else if (ChartData[i] == "국방<민간") yValues[i - 2] = 2;
                else if (ChartData[i] == "국방≪민간") yValues[i - 2] = 1;
            }
            Title1.Text = Title;
            chart1.Series[0].Points.DataBindXY(xValues, yValues);

            // Set radar chart type
            chart1.Series[0].ChartType = SeriesChartType.Radar;

            // Set radar chart style (Area, Line or Marker)
            chart1.Series[0]["RadarDrawingStyle"] = "Line";
            chart1.Series[0].IsVisibleInLegend = false;
            // Set circular area drawing style (Circle or Polygon)
            chart1.Series[0]["AreaDrawingStyle"] = "Polygon";

            // Set labels style (Auto, Horizontal, Circular or Radial)
            chart1.Series[0]["CircularLabelsStyle"] = "Auto";
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("나눔고딕", 9 );
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("나눔고딕", 9 );
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 5;
            chart1.ChartAreas[0].AxisY.Interval = 1;


            chart1.Series[0].Color = Color.Red;
            chart1.Series[0].BorderWidth = 2;

            chart1.ChartAreas[0].Axes[0].MinorGrid.LineColor = Color.Red;

            chart1.Size = new Size(350, 350);
            Title1.Location = new Point(((int)(this.Width / 4)) - ((int)(Title1.Width / 2)), 66);
            chart1.Location = new Point(Title1.Location.X - 125, Title1.Location.Y - 40);

            if(Convert.ToDouble(ChartData[1]) <= 70 && yValues[3] <= 2 && yValues[5] <= 3)
            {
                bunifuFlatButton1.Enabled = true;
            }
            else if(yValues[0] <= 2 || yValues[4] >= 4)
            {
                bunifuFlatButton4.Enabled = true;
            }
            else if (Convert.ToDouble(ChartData[0]) <= 70 && Convert.ToDouble(ChartData[1]) <= 70 && yValues[1] <= 4)
            {
                bunifuFlatButton3.Enabled = true;
            }
            else if(Convert.ToDouble(ChartData[1]) <= 70 && yValues[3] >= 4 && 20 <= Math.Abs(Convert.ToDouble(ChartData[1]) - Convert.ToDouble(ChartData[0])))
            {
                bunifuFlatButton5.Enabled = true;
            }
            else if (Convert.ToDouble(ChartData[1]) >= 70 && yValues[0] >= 4)
            {
                bunifuFlatButton2.Enabled = true;
            }
            // 5개가 모두 false이면 핵심기술로 개발
            if (bunifuFlatButton1.Enabled == false &&
                bunifuFlatButton2.Enabled == false &&
                bunifuFlatButton3.Enabled == false &&
                bunifuFlatButton4.Enabled == false &&
                bunifuFlatButton5.Enabled == false) { bunifuFlatButton1.Enabled = true; }

        }
        private void drawRadarChart2(string Title, string[] ChartData)
        {
            string[] xValues = { "기술적 파급효과", "경제적 파급효과", "보호등급", "기술도입 가능성", "민군우위기술", "난이도" };
            double[] yValues = new double[xValues.Length];

            for (int i = 2; i < ChartData.Length; i++)
            {
                if (ChartData[i] == "매우 높음") yValues[i - 2] = 5;
                else if (ChartData[i] == "높음") yValues[i - 2] = 4;
                else if (ChartData[i] == "보통") yValues[i - 2] = 3;
                else if (ChartData[i] == "낮음") yValues[i - 2] = 2;
                else if (ChartData[i] == "매우 낮음") yValues[i - 2] = 1;


                if (ChartData[i] == "국방≫민간") yValues[i - 2] = 5;
                else if (ChartData[i] == "국방>민간") yValues[i - 2] = 4;
                else if (ChartData[i] == "국방≒민간") yValues[i - 2] = 3;
                else if (ChartData[i] == "국방<민간") yValues[i - 2] = 2;
                else if (ChartData[i] == "국방≪민간") yValues[i - 2] = 1;
            }
            Title2.Text = Title;
            chart2.Series[0].Points.DataBindXY(xValues, yValues);

            // Set radar chart type
            chart2.Series[0].ChartType = SeriesChartType.Radar;

            // Set radar chart style (Area, Line or Marker)
            chart2.Series[0]["RadarDrawingStyle"] = "Line";
            chart2.Series[0].IsVisibleInLegend = false;
            // Set circular area drawing style (Circle or Polygon)
            chart2.Series[0]["AreaDrawingStyle"] = "Polygon";

            // Set labels style (Auto, Horizontal, Circular or Radial)
            chart2.Series[0]["CircularLabelsStyle"] = "Auto";

            chart2.ChartAreas[0].AxisX.LabelStyle.Font = new Font("나눔고딕", 9 );
            chart2.ChartAreas[0].AxisY.LabelStyle.Font = new Font("나눔고딕", 9 );

            chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.ChartAreas[0].AxisY.Maximum = 5;
            chart2.ChartAreas[0].AxisY.Interval = 1;

            chart2.Series[0].Color = Color.Red;
            chart2.Series[0].BorderWidth = 2;

            chart2.ChartAreas[0].Axes[0].MinorGrid.LineColor = Color.Red;

            chart2.Size = new Size(350, 350);
            Title2.Location = new Point((((int)(this.Width / 4)) * 3) - ((int)(Title2.Width / 2)), 66);
            chart2.Location = new Point(Title2.Location.X - 125, Title2.Location.Y - 40);

            if (Convert.ToDouble(ChartData[1]) <= 70 && yValues[3] <= 2 && yValues[5] <= 3)
            {
                bunifuFlatButton6.Enabled = true;
            }
            else if (yValues[0] <= 2 || yValues[4] >= 4)
            {
                bunifuFlatButton9.Enabled = true;
            }
            else if (Convert.ToDouble(ChartData[0]) <= 70 && Convert.ToDouble(ChartData[1]) <= 70 && yValues[1] <= 4)
            {
                bunifuFlatButton8.Enabled = true;
            }
            else if (Convert.ToDouble(ChartData[1]) <= 70 && yValues[3] >= 4 && 20 <= Math.Abs(Convert.ToDouble(ChartData[1]) - Convert.ToDouble(ChartData[0])))
            {
                bunifuFlatButton10.Enabled = true;
            }
            else if (Convert.ToDouble(ChartData[1]) >= 70 && yValues[0] >= 4)
            {
                bunifuFlatButton7.Enabled = true;
            }
            
            // 5개가 모두 false이면 핵심기술로 개발
            if (bunifuFlatButton6.Enabled == false &&
                bunifuFlatButton7.Enabled == false &&
                bunifuFlatButton8.Enabled == false &&
                bunifuFlatButton9.Enabled == false &&
                bunifuFlatButton10.Enabled == false) { bunifuFlatButton6.Enabled = true; }
        }
        private void drawRadarChart3(string Title, string[] ChartData)
        {
            string[] xValues = { "기술적 파급효과", "경제적 파급효과", "보호등급", "기술도입 가능성", "민군우위기술", "난이도" };
            double[] yValues = new double[xValues.Length];

            for (int i = 2; i < ChartData.Length; i++)
            {
                if (ChartData[i] == "매우 높음") yValues[i - 2] = 5;
                else if (ChartData[i] == "높음") yValues[i - 2] = 4;
                else if (ChartData[i] == "보통") yValues[i - 2] = 3;
                else if (ChartData[i] == "낮음") yValues[i - 2] = 2;
                else if (ChartData[i] == "매우 낮음") yValues[i - 2] = 1;


                if (ChartData[i] == "국방≫민간") yValues[i - 2] = 5;
                else if (ChartData[i] == "국방>민간") yValues[i - 2] = 4;
                else if (ChartData[i] == "국방≒민간") yValues[i - 2] = 3;
                else if (ChartData[i] == "국방<민간") yValues[i - 2] = 2;
                else if (ChartData[i] == "국방≪민간") yValues[i - 2] = 1;
            }
            Title3.Text = Title;
            chart3.Series[0].Points.DataBindXY(xValues, yValues);

            // Set radar chart type
            chart3.Series[0].ChartType = SeriesChartType.Radar;

            // Set radar chart style (Area, Line or Marker)
            chart3.Series[0]["RadarDrawingStyle"] = "Line";
            chart3.Series[0].IsVisibleInLegend = false;
            // Set circular area drawing style (Circle or Polygon)
            chart3.Series[0]["AreaDrawingStyle"] = "Polygon";

            // Set labels style (Auto, Horizontal, Circular or Radial)
            chart3.Series[0]["CircularLabelsStyle"] = "Auto";

            chart3.ChartAreas[0].AxisX.LabelStyle.Font = new Font("나눔고딕", 9 );
            chart3.ChartAreas[0].AxisY.LabelStyle.Font = new Font("나눔고딕", 9 );

            chart3.ChartAreas[0].AxisY.Minimum = 0;
            chart3.ChartAreas[0].AxisY.Maximum = 5;
            chart3.ChartAreas[0].AxisY.Interval = 1;

            chart3.Series[0].Color = Color.Red;
            chart3.Series[0].BorderWidth = 2;

            chart3.ChartAreas[0].Axes[0].MinorGrid.LineColor = Color.Red;

            chart3.Size = new Size(350, 350);
            Title3.Location = new Point(((int)(this.Width / 4)) - ((int)(Title3.Width / 2)), 400);
            chart3.Location = new Point(Title3.Location.X - 125, Title3.Location.Y - 40);

            if (Convert.ToDouble(ChartData[1]) <= 70 && yValues[3] <= 2 && yValues[5] <= 3)
            {
                bunifuFlatButton11.Enabled = true;
            }
           else if (yValues[0] <= 2 || yValues[4] >= 4)
            {
                bunifuFlatButton14.Enabled = true;
            }
            else if (Convert.ToDouble(ChartData[0]) <= 70 && Convert.ToDouble(ChartData[1]) <= 70 && yValues[1] <= 4)
            {
                bunifuFlatButton13.Enabled = true;
            }
            else if (Convert.ToDouble(ChartData[1]) <= 70 && yValues[3] >= 4 && 20 <= Math.Abs(Convert.ToDouble(ChartData[1]) - Convert.ToDouble(ChartData[0])))
            {
                bunifuFlatButton15.Enabled = true;
            }
            else if (Convert.ToDouble(ChartData[1]) >= 70 && yValues[0] >= 4)
            {
                bunifuFlatButton12.Enabled = true;
            }

            // 5개가 모두 false이면 핵심기술로 개발
            if (bunifuFlatButton11.Enabled == false &&
                bunifuFlatButton12.Enabled == false &&
                bunifuFlatButton13.Enabled == false &&
                bunifuFlatButton14.Enabled == false &&
                bunifuFlatButton15.Enabled == false) { bunifuFlatButton11.Enabled = true; }
        }
        private void drawRadarChart4(string Title, string[] ChartData)
        {
            string[] xValues = { "기술적 파급효과", "경제적 파급효과", "보호등급", "기술도입 가능성", "민군우위기술", "난이도" };
            double[] yValues = new double[xValues.Length];

            for (int i = 2; i < ChartData.Length; i++)
            {
                if (ChartData[i] == "매우 높음") yValues[i - 2] = 5;
                else if (ChartData[i] == "높음") yValues[i - 2] = 4;
                else if (ChartData[i] == "보통") yValues[i - 2] = 3;
                else if (ChartData[i] == "낮음") yValues[i - 2] = 2;
                else if (ChartData[i] == "매우 낮음") yValues[i - 2] = 1;


                if (ChartData[i] == "국방≫민간") yValues[i - 2] = 5;
                else if (ChartData[i] == "국방>민간") yValues[i - 2] = 4;
                else if (ChartData[i] == "국방≒민간") yValues[i - 2] = 3;
                else if (ChartData[i] == "국방<민간") yValues[i - 2] = 2;
                else if (ChartData[i] == "국방≪민간") yValues[i - 2] = 1;
            }
            Title4.Text = Title;
            chart4.Series[0].Points.DataBindXY(xValues, yValues);

            // Set radar chart type
            chart4.Series[0].ChartType = SeriesChartType.Radar;

            // Set radar chart style (Area, Line or Marker)
            chart4.Series[0]["RadarDrawingStyle"] = "Line";
            chart4.Series[0].IsVisibleInLegend = false;
            // Set circular area drawing style (Circle or Polygon)
            chart4.Series[0]["AreaDrawingStyle"] = "Polygon";

            // Set labels style (Auto, Horizontal, Circular or Radial)
            chart4.Series[0]["CircularLabelsStyle"] = "Auto";

            chart4.ChartAreas[0].AxisX.LabelStyle.Font = new Font("나눔고딕", 9 );
            chart4.ChartAreas[0].AxisY.LabelStyle.Font = new Font("나눔고딕", 9);

            chart4.ChartAreas[0].AxisY.Minimum = 0;
            chart4.ChartAreas[0].AxisY.Maximum = 5;
            chart4.ChartAreas[0].AxisY.Interval = 1;

            chart4.Series[0].Color = Color.Red;
            chart4.Series[0].BorderWidth = 2;

            chart4.ChartAreas[0].Axes[0].MinorGrid.LineColor = Color.Red;

            chart4.Size = new Size(350, 350);
            Title4.Location = new Point((((int)(this.Width / 4)) * 3) - ((int)(Title4.Width / 2)), 400);
            chart4.Location = new Point(Title4.Location.X - 125, Title4.Location.Y - 40);

            bunifuFlatButton1.Enabled = false;

            if (Convert.ToDouble(ChartData[1]) <= 70 && yValues[3] <= 2 && yValues[5] <= 3)
            {
                bunifuFlatButton16.Enabled = true;
            }
            else if (yValues[0] <= 2 || yValues[4] >= 4)
            {
                bunifuFlatButton19.Enabled = true;
            }
            else if (Convert.ToDouble(ChartData[0]) <= 70 && Convert.ToDouble(ChartData[1]) <= 70 && yValues[1] <= 4)
            {
                bunifuFlatButton18.Enabled = true;
            }
            else if (Convert.ToDouble(ChartData[1]) <= 70 && yValues[3] >= 4 && 20 <= Math.Abs(Convert.ToDouble(ChartData[1]) - Convert.ToDouble(ChartData[0])))
            {
                bunifuFlatButton20.Enabled = true;
            }
            else if (Convert.ToDouble(ChartData[1]) >= 70 && yValues[0] >= 4)
            {
                bunifuFlatButton17.Enabled = true;
            }
         
            // 5개가 모두 false이면 핵심기술로 개발
            if (bunifuFlatButton16.Enabled == false &&
                bunifuFlatButton17.Enabled == false &&
                bunifuFlatButton18.Enabled == false &&
                bunifuFlatButton19.Enabled == false &&
                bunifuFlatButton20.Enabled == false) { bunifuFlatButton16.Enabled = true; }
        }
        
        private void OriginalTechnology3_Load(object sender, EventArgs e)
        {
            List<string> stringChartData = new List<string>();
            List<string> Title = new List<string>();


            Title.AddRange(mainFrm.CriticalTechnology1);

            if (mainFrm.CriticalTechnology1.Count >= 1)
            {
                List<string> sublist1 = new List<string>();
                string[] SubList = new string[] {   
                                                    mainFrm.CriticalTechnology2[0],
                                                    mainFrm.CriticalTechnology3[0],
                                                    mainFrm.CriticalTechnology4[0],
                                                    mainFrm.CriticalTechnology5[0],
                                                    mainFrm.CriticalTechnology6[0],
                                                    mainFrm.CriticalTechnology7[0],
                                                    mainFrm.CriticalTechnology8[0],
                                                    mainFrm.CriticalTechnology9[0]};
                sublist1.AddRange(SubList);
                drawRadarChart1("<" + Title[0] + ">", sublist1.ToArray());

                chart1.Visible = true;
                Title1.Visible = true;
                bunifuFlatButton1.Visible = true;
                bunifuFlatButton2.Visible = true;
                bunifuFlatButton3.Visible = true;
                bunifuFlatButton4.Visible = true;
                bunifuFlatButton5.Visible = true;

            }
            if (mainFrm.CriticalTechnology1.Count >= 2)
            {
                List<string> sublist2 = new List<string>();
                string[] SubList = new string[] {   
                                                    mainFrm.CriticalTechnology2[1],
                                                    mainFrm.CriticalTechnology3[1],
                                                    mainFrm.CriticalTechnology4[1],
                                                    mainFrm.CriticalTechnology5[1],
                                                    mainFrm.CriticalTechnology6[1],
                                                    mainFrm.CriticalTechnology7[1],
                                                    mainFrm.CriticalTechnology8[1],
                                                    mainFrm.CriticalTechnology9[1]};
                sublist2.AddRange(SubList);
                drawRadarChart2("<" + Title[1] + ">", sublist2.ToArray());


                chart2.Visible = true;
                Title2.Visible = true;
                bunifuFlatButton6.Visible = true;
                bunifuFlatButton7.Visible = true;
                bunifuFlatButton8.Visible = true;
                bunifuFlatButton9.Visible = true;
                bunifuFlatButton10.Visible = true;
            }
            if (mainFrm.CriticalTechnology1.Count >= 3)
            {
                List<string> sublist3 = new List<string>();
                string[] SubList = new string[] {   
                                                    mainFrm.CriticalTechnology2[2],
                                                    mainFrm.CriticalTechnology3[2],
                                                    mainFrm.CriticalTechnology4[2],
                                                    mainFrm.CriticalTechnology5[2],
                                                    mainFrm.CriticalTechnology6[2],
                                                    mainFrm.CriticalTechnology7[2],
                                                    mainFrm.CriticalTechnology8[2],
                                                    mainFrm.CriticalTechnology9[2]};
                sublist3.AddRange(SubList);
                drawRadarChart3("<" + Title[2] + ">", sublist3.ToArray());

                chart3.Visible = true;
                Title3.Visible = true;
                bunifuFlatButton11.Visible = true;
                bunifuFlatButton12.Visible = true;
                bunifuFlatButton13.Visible = true;
                bunifuFlatButton14.Visible = true;
                bunifuFlatButton15.Visible = true;
            }
            if (mainFrm.CriticalTechnology1.Count >= 4)
            {
                List<string> sublist4 = new List<string>();
                string[] SubList = new string[] {   
                                                    mainFrm.CriticalTechnology2[3],
                                                    mainFrm.CriticalTechnology3[3],
                                                    mainFrm.CriticalTechnology4[3],
                                                    mainFrm.CriticalTechnology5[3],
                                                    mainFrm.CriticalTechnology6[3],
                                                    mainFrm.CriticalTechnology7[3],
                                                    mainFrm.CriticalTechnology8[3],
                                                    mainFrm.CriticalTechnology9[3]};
                sublist4.AddRange(SubList);
                drawRadarChart4("<" + Title[3] + ">", sublist4.ToArray());

                chart4.Visible = true;
                Title4.Visible = true;
                bunifuFlatButton16.Visible = true;
                bunifuFlatButton17.Visible = true;
                bunifuFlatButton18.Visible = true;
                bunifuFlatButton19.Visible = true;
                bunifuFlatButton20.Visible = true;
            }

            
        }
    }
}
