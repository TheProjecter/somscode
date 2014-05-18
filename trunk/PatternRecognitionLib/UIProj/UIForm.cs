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
        double gamma = 0.8;
        double omega = 0.1;
        bool taskMod = false, setCanva = false;
        int ImgNum = -1;
        int NumIdx =-1;
        int[] VecNum = { 0, 0 };
        PatternRecognitionLib.Image[] imgs = null;
        public UIForm()
        {
            InitializeComponent();
        }
        private void buildButton_Click(object sender, EventArgs e)
        {
            if (imgs != null)
            {
                if (taskMod == true || ImgNum!=-1)
                {
                    Utilities.PrepareToDraw();
                    LinRule linRules = new LinRule(imgs);
                    linRules.BuildRules(gamma, omega);
                    setCanva = false;
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
            Utilities.isMulti = cf.isMulti;
        }
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgs = Utilities.ReadTask();
            if (imgs != null)
            {
                Utilities.SetCanva(drawBox, false, Utilities.GetCellNum2D(imgs));
                Utilities.DrawImage2D(imgs[0], Pens.BlueViolet);
                Utilities.DrawImage2D(imgs[1], Pens.Red);
                taskMod = true;
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.WriteTask(imgs);
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            if (taskMod) taskMod = false;
            setCanva = false;
            ImgNum = -1;
            NumIdx = -1;
            VecNum[0] = 0;
            VecNum[1] = 0;
            Utilities.ClearWindow();
        }
        private void drawBox_MouseClick(object sender, MouseEventArgs e)
        {
            if(ImgNum==0)
            {
                imgs[ImgNum][VecNum[NumIdx]] = Utilities.SetVector(e.X, e.Y, Pens.Blue);
                VecNum[NumIdx]++;
            }
            else
            {
                if (ImgNum == 1)
                {
                    imgs[ImgNum][VecNum[NumIdx]] = Utilities.SetVector(e.X, e.Y, Pens.Red);
                    VecNum[NumIdx]++;
                }
                else
                {
                    MessageBox.Show("Сначала выберите образ", "Внимание!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void Image1Button_Click(object sender, EventArgs e)
        {
            if (!setCanva)
            {
                Utilities.SetCanva(drawBox, true);
                setCanva = true;
                imgs = new PatternRecognitionLib.Image[2];
                taskMod = false;
            }
            if (imgs[0] == null)
            {
                imgs[0] = new PatternRecognitionLib.Image();
            }
            ImgNum = 0;
            NumIdx = 0;
        }
        private void Image2Button_Click(object sender, EventArgs e)
        {
            if (!setCanva)
            {
                Utilities.SetCanva(drawBox, true);
                setCanva = true;
                imgs = new PatternRecognitionLib.Image[2];
                taskMod = false;
            }
            if (imgs[1] == null )
            {
                imgs[1] = new PatternRecognitionLib.Image();
            }
            ImgNum = 1;
            NumIdx = 1;
        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            Utilities.iterDone.Set();
        }
    }
}
