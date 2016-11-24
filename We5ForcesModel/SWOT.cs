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
    public partial class SWOT : Form
    {
        public SWOT()
        {
            InitializeComponent();
        }

        private void SWOT_Load(object sender, EventArgs e)
        {
            textBox5.Text = mainFrm.SWOT[0];
            textBox6.Text = mainFrm.SWOT[1];
            textBox7.Text = mainFrm.SWOT[2];
            textBox8.Text = mainFrm.SWOT[3];
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            mainFrm.SWOT[0] = textBox5.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            mainFrm.SWOT[1] = textBox6.Text;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            mainFrm.SWOT[2] = textBox6.Text;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            mainFrm.SWOT[3] = textBox6.Text;
        }
    }
}
