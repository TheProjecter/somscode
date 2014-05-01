using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        static public void DrawLine2D(vectorObject w, vectorObject x1, vectorObject y1)
        {
            vectorObject begin = x1 + y1;
            float a = (w * begin)/2;
            float yk = (a - w[0] * Drawer.Size[0]) / w[1];
            float y0 = a / w[1];

            begin = new vectorObject(0, y0);
            vectorObject end = new vectorObject(Drawer.Size[0], yk);

            begin = Drawer.ReCoord2D(begin);
            end = Drawer.ReCoord2D(end);

            Drawer.DrawLine2D((float)(Drawer.Center[0] + (begin[0] * Drawer.CellSize)),
                (float)(Drawer.Center[1] + (begin[1] * Drawer.CellSize)),
                (float)(Drawer.Center[0] + (end[0] * Drawer.CellSize)),
                (float)(Drawer.Center[1] + (end[1] * Drawer.CellSize)));
        }
        static public void DrawLine2D(vectorObject x, vectorObject y)
        {
            vectorObject begin = Drawer.ReCoord2D(x);
            vectorObject end = Drawer.ReCoord2D(y);

            Drawer.DrawDashLine2D((float)(Drawer.Center[0] + (begin[0] * Drawer.CellSize)),
                (float)(Drawer.Center[1] + (begin[1] * Drawer.CellSize)),
                (float)(Drawer.Center[0] + (end[0] * Drawer.CellSize)),
                (float)(Drawer.Center[1] + (end[1] * Drawer.CellSize)));
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
                        float[] crds = new float[coords.Count()];
                        for (int k = 0; k < crds.Count(); k++)
                        {
                            crds[k] =  float.Parse(coords[k]);
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
        static vectorObject mid = new vectorObject(2);
        static int[] size = new int[2];
        public static int[] Size
        {
            get { return size; }
        }
        public static vectorObject Center
        {
            get { return mid; }
        }
        public static int CellSize
        {
            get { return cellSize; }
        }
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
                }
                size[0] = pBox.Width;
                size[1] = pBox.Height;
                gs = pBox.CreateGraphics();
                mid[0] = pBox.Width/2;
                mid[1] = pBox.Height/2;
                DrawLine2D(mid[0], 0, mid[0], size[1]);
                DrawLine2D(0, mid[1], size[0], mid[1]);
            }
            catch (Exception e)
            { }
            
        }
        static public void DrawImage2D(Image img, Pen pen)
        { 
            for (int i = 0; i<img.Count; i++)
            {
                DrawPoint((float)(mid[0]+(ReCoord2D(img[i])[0] * cellSize) - 2),
                    (float)(mid[1] + (ReCoord2D(img[i])[1] * cellSize) - 2), pen);
            }
        }
        static public void DrawPoint(float x, float y, Pen pen)
        {
            gs.DrawEllipse(pen, x, y, 4 , 4);
        }
        static public void DrawLine2D(float x1, float y1, float x2, float y2)
        {
            Pen pen = new Pen(Brushes.Black, 2);

            gs.DrawLine(pen, x1, y1, x2, y2);
        }
        static public void DrawDashLine2D(float x1, float y1, float x2, float y2)
        {
            Pen pen = new Pen(Brushes.Black, 1);
            pen.DashStyle = DashStyle.Dash;

            gs.DrawLine(pen, x1, y1, x2, y2);
        }
        static public void clr()
        {
            gs.Clear(Color.White);
            DrawLine2D(mid[0], 0,mid[0], size[1]);
            DrawLine2D(0, mid[1], size[0],mid[1]);
        }
        static public vectorObject ReCoord2D(vectorObject OldCoord)
        {
            vectorObject NewCoord = new vectorObject(OldCoord.Size);

            NewCoord[0] = OldCoord[0];
            if (OldCoord[1] > 0)
                NewCoord[1] = - OldCoord[1];
            else
                NewCoord[1] = - OldCoord[1];

            return NewCoord;
        }
        #endregion
    }
}
