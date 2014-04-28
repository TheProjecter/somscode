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
        bool taskMod = false;
        PatternRecognitionLib.Image[] imgs = null;
        Graphics gs;
        public UIForm()
        {
            InitializeComponent();
            gs = drawBox.CreateGraphics();
        }
        private void buildButton_Click(object sender, EventArgs e)
        {
            if (imgs != null)
            {
                if (taskMod == true)
                {
                    LinRule rule = new LinRule(imgs);
                    rule.BuildRules(gamma, omega);
                }
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
            Utilities.SetCanva(drawBox, false, 20);
            Utilities.DrawImage2D(imgs[0], Pens.BlueViolet);
            Utilities.DrawImage2D(imgs[1], Pens.Red);
            taskMod = true;
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.WriteTask(imgs);
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            taskMod = false;
            Utilities.ClearWindow();
        }
    }
}
