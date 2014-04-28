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
        public double omega = 0.1;

        public ConfigForm()
        {
            InitializeComponent();
        }
        public ConfigForm(double _gamma, double _omega)
        {
            InitializeComponent();
            gamma = _gamma;
            omega = _omega;
            gammaBox.Text = gamma.ToString();
            omegaBox.Text = omega.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            gamma = Double.Parse(gammaBox.Text);
            omega = Double.Parse(omegaBox.Text);
            this.Close();
        }
    }
}
