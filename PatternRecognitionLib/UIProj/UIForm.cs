using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatternRecognitionLib;

namespace UIProj
{
    public partial class UIForm : Form
    {
        double gamma = 0.1;
        double omega = 0.8;
        PatternRecognitionLib.Image[] imgs = null;
        public UIForm()
        {
            InitializeComponent();
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            if (imgs != null)
            {
                LinRule rule = new LinRule(imgs);
                rule.BuildRules(gamma, omega);
            }
            else
            {
                MessageBox.Show("Введите образы перед началом построения решающего правила", "Внимание!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void конфигурацияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConfigForm cf = new ConfigForm(gamma, omega);
            cf.ShowDialog();
            omega = cf.omega;
            gamma = cf.gamma;
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgs = Utilities.ReadTask();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.WriteTask(imgs);
        }
    }
}
