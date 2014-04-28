using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace PatternRecognitionLib
{
    //Класс мэнеджер вспомогательных классов
    public class Utilities
    {
        #region Фунции работы с файлами
        static public Image[] ReadTask()
        {
            return Parser.ReadTask();
        }
        static public void WriteTask(Image[] imgs)
        {
            Parser.WriteTask(imgs);
        }
        #endregion
        #region Функции работы с графикой
        static public void SetCanva(object canva, bool UseDefaultCellSize, int cellNum=0)
        {
            Drawer.SetCanva(canva, UseDefaultCellSize, cellNum);
        }
        static public void DrawImage2D(Image img, Pen pen)
        {
            Drawer.DrawImage2D(img, pen);
        }
        static public void ClearWindow()
        {
            Drawer.clr();
        }
        #endregion
    }
    //Класс загрузки/сохранения примера
    class Parser
    {
        static public Image[] ReadTask()
        {
            Image[] imgs;
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "Выберите файл";
            of.Filter = "Текстовые файлы|*.txt";

            if (of.ShowDialog() == DialogResult.OK)
            {
                TextReader tr = new StreamReader(of.FileName);
                int n = Int32.Parse(NextString(tr));
                imgs = new Image[n];
                for (int i = 0; i < n; i++)
                {
                    string tmp = NextString(tr);
                    string[] objs = tmp.Split(';');
                    imgs[i] = new Image(objs.Count() - 1);
                    for (int j = 0; j < objs.Count() - 1; j++)
                    {
                        string[] coords = objs[j].Split(',');
                        double[] crds = new double[coords.Count()];
                        for (int k = 0; k < crds.Count(); k++)
                        {
                            crds[k] = Double.Parse(coords[k]);
                        }
                        imgs[i][j] = new vectorObject(crds);
                    }
                }
                tr.Close();
                return imgs;
            }

            return null;
        }
        static private string NextString(TextReader tr)
        {
            string tmp = tr.ReadLine();
            if (tmp.IndexOf('/') > -1)
            {
                tmp = NextString(tr);
            }
            return tmp;
        }
        static public void WriteTask(Image[] imgs)
        {
            if (imgs != null)
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.Title = "Выберите файл";
                sf.Filter = "Текстовые файлы|*.txt";

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    TextWriter tw = new StreamWriter(sf.FileName);
                    tw.WriteLine("//число образов");
                    tw.WriteLine(imgs.Count());
                    for(int i=0; i<imgs.Count(); i++)
                    {
                        tw.WriteLine("//image" + (i + 1));
                        tw.WriteLine(imgs[i].ToString());
                    }
                    tw.Close();
                }
            }
        }
    }

    //Класс работы с графикой
    class Drawer
    {
        #region Поля класса отрисовки
        static object canva;
        static Graphics gs;
        static int cellSize = 1;
        static int midX, midY;
        #endregion
        #region Методы отрисовки
        static public void SetCanva(object _canva, bool UseDefaultCellSize, int cellNum)
        {
            canva = _canva;
            try
            {
                PictureBox pBox = (PictureBox)canva;
                if (UseDefaultCellSize != true)
                {

                    int max = 0;
                    if (pBox.Height > pBox.Width)
                    {
                        max = pBox.Height;
                    }
                    else
                    {
                        max = pBox.Width;
                    }
                    cellSize = max / cellNum;
                    gs = pBox.CreateGraphics();
                }

            }
            catch (Exception e)
            { }
            
        }
        static public void DrawImage2D(Image img, Pen pen)
        { 
            for (int i = 0; i<img.Count; i++)
            {
                gs.DrawEllipse(pen, (float)((img[i][0]*cellSize)-2), (float)((img[i][1]*cellSize)-2), 4, 4);
            }
        }
        static public void clr()
        {
            gs.Clear(Color.White);
        }
        #endregion
    }
}
