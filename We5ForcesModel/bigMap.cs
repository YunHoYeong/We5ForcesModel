using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace We5ForcesModel
{
    public partial class bigMap : Form
    {
        public bigMap()
        {
            InitializeComponent();

            var r = new Random();
            var values = new Dictionary<string, double>();
            for (int i = 0; i < Competition1.CompetitionDrawMap.Length; i++)
            {
                if (Competition1.WorldName.IndexOf(Competition1.CompetitionDrawMap[i]) != -1)
                {
                    values[Competition1.WorldCode[Competition1.WorldName.IndexOf(Competition1.CompetitionDrawMap[i])]] = r.Next(0, 100); ;
                }
            }
            geoMap1.HeatMap = values;
            geoMap1.Source = "Maps/World.xml";
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
