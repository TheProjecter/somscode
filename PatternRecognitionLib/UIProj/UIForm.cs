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
using Util;
using GraphicsLib;
using System.Threading;

namespace UIProj
{
    public partial class UIForm : Form
    {
        double gamma = 0.8;
        double delta = 0.1;
        bool taskMod = false;
        int ImgNum = -1;
        int NumIdx =-1;
        int[] VecNum = { 0, 0 };
        int cellsize = 1;
        Graphics gs;
        PatternRecognitionLib.SetOfSigns[] imgs = null;
        public UIForm()
        {
            InitializeComponent();

            OpenFileDialog of = new OpenFileDialog();
            of.Title = "Выберите файл";
            of.Filter = "PDF files|*.pdf";
            if (of.ShowDialog() == DialogResult.OK)
            {
                this.axAcroPDF1.LoadFile(of.FileName);
                this.axAcroPDF1.src = of.FileName;
                this.axAcroPDF1.setShowToolbar(false);
                this.axAcroPDF1.setView("FitH");
                this.axAcroPDF1.setLayoutMode("SinglePage");
                this.axAcroPDF1.Show();
            }

            gs = drawBox.CreateGraphics();
            Utilities.Boards.Add(new GraphicsBoard(drawBox.Width, drawBox.Height, gs));
            Utilities.Boards[0].Draw(cellsize);
        }
        private void buildButton_Click(object sender, EventArgs e)
        {
            if (imgs != null)
            {
                if (taskMod == true || ImgNum!=-1)
                {
                    Utilities.drawing = true;
                    Utilities.PrepareToDraw(drawBox);

                    LinRule linRules = new LinRule(imgs);
                    linRules.BuildRules(gamma, delta);

                    returnButton.Enabled = false;
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
            ConfigForm cf = new ConfigForm(gamma, delta);
            if (cf.ShowDialog() == DialogResult.OK)
            {
                delta = cf.delta;
                gamma = cf.gamma;
                Utilities.multi = cf.isMulti;
            }
        }
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cs = 0;
            imgs = Utilities.ReadTask(out cs);
            if (imgs != null)
            {
                clearButton_Click(sender, e);
                cellsize = cs;
                taskMod = true;
                Set set1 = new Set(Utilities.Boards[0].Graphics);
                Set set2 = new Set(Utilities.Boards[0].Graphics);
                for(int i = 0; i<imgs[0].Count; i++)
                {
                    if (cellsize > 1)
                        set1.Add(Utilities.ReCoord2D(imgs[0][i])[0], Utilities.ReCoord2D(imgs[0][i])[1], new Pen(Brushes.Red, 3));
                    else
                        set1.Add(imgs[0][i][0], imgs[0][i][1], new Pen(Brushes.Red, 3));
                }
                for (int i = 0; i < imgs[1].Count; i++)
                {
                    if (cellsize > 1)
                        set2.Add(Utilities.ReCoord2D(imgs[1][i])[0], Utilities.ReCoord2D(imgs[1][i])[1], new Pen(Brushes.Blue, 3));
                    else
                        set2.Add(imgs[1][i][0], imgs[1][i][1], new Pen(Brushes.Blue, 3));
                }
                Utilities.Boards[0].AddElem(set1);
                Utilities.Boards[0].AddElem(set2);
                Utilities.Boards[0].Draw(cellsize);
                drawBox.Invalidate();
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.WriteTask(imgs, cellsize);
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            Utilities.drawing = false;
            Utilities.Boards.Clear();
            Utilities.Boards.Add(new GraphicsBoard(drawBox.Width, drawBox.Height, gs));
            if (taskMod) taskMod = false;
            cellsize = 1;
            ImgNum = -1;
            NumIdx = -1;
            VecNum[0] = 0;
            VecNum[1] = 0;
            returnButton.Enabled = true;
            Utilities.Boards[0].Draw(cellsize);
            drawBox.Invalidate();
        }
        private void drawBox_MouseClick(object sender, MouseEventArgs e)
        {
            switch (ImgNum)
            {
                case 0: imgs[0][VecNum[NumIdx]] = Utilities.SetVector(e.X, e.Y);
                        Utilities.Boards[0].AddElem(new Point2f(imgs[0][VecNum[NumIdx]][0], imgs[0][VecNum[NumIdx]][1],
                            new Pen(Brushes.Red, 3)));
                        VecNum[NumIdx]++;
                        break;
                case 1: imgs[1][VecNum[NumIdx]] = Utilities.SetVector(e.X, e.Y);
                        Utilities.Boards[0].AddElem(new Point2f(imgs[1][VecNum[NumIdx]][0], imgs[1][VecNum[NumIdx]][1], 
                            new Pen(Brushes.Blue, 3)));
                        VecNum[NumIdx]++;
                        break;
                default: MessageBox.Show("Сначала выберите множество", "Внимание!",
                         MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                         break;
            }
            Utilities.Boards[0].Draw(cellsize);
            drawBox.Invalidate();
        }
        private void Set1Button_Click(object sender, EventArgs e)
        {
            if (taskMod)
            {
                clearButton_Click(sender, e);
                taskMod = false;
                cellsize = 1;
            }
            if (imgs == null)
            {
                imgs = new PatternRecognitionLib.SetOfSigns[2];
            }
            if (imgs[0] == null)
            {
                imgs[0] = new PatternRecognitionLib.SetOfSigns();
            }
            ImgNum = 0;
            NumIdx = 0;
        }
        private void Set2Button_Click(object sender, EventArgs e)
        {
            if (taskMod)
            {
                clearButton_Click(sender, e);
                taskMod = false;
                cellsize = 1;
            }
            if (imgs == null)
            {
                imgs = new PatternRecognitionLib.SetOfSigns[2];
            }
            if (imgs[1] == null)
            {
                imgs[1] = new PatternRecognitionLib.SetOfSigns();
            }
            ImgNum = 1;
            NumIdx = 1;
        }
        private void сделатьРисунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.SaveScreen(Utilities.Boards[Utilities.Boards.Count-1].bmp);
        }
        private void drawBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Utilities.Boards[Utilities.Boards.Count-1].bmp, new Point(0, 0));
        }
        private void returnButton_Click(object sender, EventArgs e)
        {
            if (!taskMod)
            {
                Point2f rem;
                try
                {
                    switch (ImgNum)
                    {
                        case 0: rem = new Point2f(imgs[0][VecNum[NumIdx] - 1][0], imgs[0][VecNum[NumIdx] - 1][1]);
                            Utilities.Boards[0].RemoveElem(rem);
                            imgs[0][VecNum[NumIdx] - 1] = null;
                            imgs[0].Count--;
                            VecNum[NumIdx]--;
                            break;
                        case 1: rem = new Point2f(imgs[1][VecNum[NumIdx] - 1][0], imgs[1][VecNum[NumIdx] - 1][1]);
                            Utilities.Boards[0].RemoveElem(rem);
                            imgs[1][VecNum[NumIdx] - 1] = null;
                            imgs[1].Count--;
                            VecNum[NumIdx]--;
                            break;
                        default: MessageBox.Show("Сначала выберите множество", "Внимание!",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Utilities.Boards[0].Draw(1);
                drawBox.Invalidate();
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.drawing = false;
            this.Close();
        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            Utilities.nextStep.Set();
        }
    }
}
