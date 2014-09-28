using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIProj
{
    public partial class ConfigForm : Form
    {
        public double gamma = 0.8;
        public double delta = 0.1;
        public bool isMulti = true;
        public ConfigForm()
        {
            InitializeComponent();
        }
        public ConfigForm(double _gamma, double _omega)
        {
            InitializeComponent();
            gamma = _gamma;
            delta = _omega;
            gammaBox.Text = gamma.ToString();
            omegaBox.Text = delta.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            gamma = Double.Parse(gammaBox.Text);
            delta = Double.Parse(omegaBox.Text);
            this.Close();
        }

        private void multiButton_CheckedChanged(object sender, EventArgs e)
        {
            isMulti = true;
        }

        private void iterButton_CheckedChanged(object sender, EventArgs e)
        {
            isMulti = false;
        }

    }
}
