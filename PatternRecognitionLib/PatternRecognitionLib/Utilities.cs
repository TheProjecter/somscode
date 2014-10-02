using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using GraphicsLib;
using PatternRecognitionLib;

namespace Util
{
    //Класс мэнеджер вспомогательных классов
    public class Utilities
    {
        public static List<GraphicsBoard> Boards = new List<GraphicsBoard>();
        static public ManualResetEvent drawDone =
            new ManualResetEvent(false);
        static public ManualResetEvent nextStep =
            new ManualResetEvent(false);
        static public bool multi = true;
        static public bool drawing = true;
        static Thread drawThread;
        static object DrawBox;
        static public object MessageBox;
        #region Фунции работы с файлами
        static public SetOfSigns[] ReadTask(out int cellsize)
        {
            return Parser.ReadTask(out cellsize);
        }
        static public void WriteTask(SetOfSigns[] imgs, int cellsize)
        {
            Parser.WriteTask(imgs, cellsize);
        }
        static public void SaveScreen(Bitmap bmp)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Title = "Сохранить как...";
            sf.Filter = "BMP|*.bmp";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(sf.FileName);
            }
        }
        #endregion
        #region Функции для работы с графикой
        static public vectorObject[] GetNewCoords(vectorObject w, vectorObject x1, vectorObject y1)
        {
            vectorObject p = new vectorObject((x1[0] + y1[0]) / 2, (x1[1] + y1[1]) / 2);
            float A = y1[0] - x1[0];
            float B = y1[1] - x1[1];
            float C = -(A * p[0] + B * p[1]);

            float[] p4 = new float[2];

            p4[0] = -Utilities.Boards[0].Width / 2;
            p4[1] = -(A * p4[0] + C) / B;

            float[] p3 = { Utilities.Boards[0].Width / 2, -(A * (Utilities.Boards[0].Width / 2) + C) / B };

            vectorObject[] be = new vectorObject[2];

            be[0] = new vectorObject(p3);
            be[1] = new vectorObject(p4);

            return be;
        }
        static public vectorObject[] GetNewCoords2(vectorObject w, vectorObject x1, vectorObject y1)
        {

            vectorObject[] be = new vectorObject[2];

            be[0] = x1 + y1;
            float a = (w * be[0]) / 2;
            float yk = (a - w[0] * Boards[0].Width) / w[1];
            float y0 = a / w[1];

            be[0] = new vectorObject(0, y0);
            be[1] = new vectorObject(Boards[0].Width, yk);

            be[0] = ReCoord2D(be[0]);
            be[1] = ReCoord2D(be[1]);

            return be;
        }
        static public vectorObject[] GetNewCoords(vectorObject x, vectorObject y)
        {
            vectorObject[] be = new vectorObject[2];

            be[0] = ReCoord2D(x);
            be[1] = ReCoord2D(y);

            return be;
        }
        /// <summary> Превращает нормальные координаты в координаты для отрисовки </summary>
        static public vectorObject ReCoord2D(vectorObject OldCoord)
        {
            vectorObject NewCoord = new vectorObject(OldCoord.Size);

            NewCoord[0] = OldCoord[0];

            if (OldCoord[1] > 0)
                NewCoord[1] = -OldCoord[1];
            else
                NewCoord[1] = -OldCoord[1];

            return NewCoord;
        }
        /// <summary> Определяет координатную четверть</summary>
        static public vectorObject GetCoords2D(float x, float y)
        {
            vectorObject tmp = new vectorObject(2);

            tmp[0] = x - Boards[0].Width / 2;
            tmp[1] = y - Boards[0].Height / 2;

            return tmp;
        }
        /// <summary> Определяет координатную четверть</summary>
        static public vectorObject GetCoords2D(vectorObject vec)
        {
            vectorObject tmp = new vectorObject(2);

            tmp[0] = vec[0] - Boards[0].Width / 2;
            tmp[1] = vec[1] - Boards[0].Height / 2;

            return tmp;
        }
        /// <summary> Задаем вектор объект</summary>
        static public vectorObject SetVector(int x, int y)
        {
            vectorObject tmp = GetCoords2D((float)x, (float)y);

            return tmp;
        }
        static public void PrepareToDraw(object drawBox)
        {
            DrawBox = drawBox;
            drawThread = new Thread(DrawFunc);
            drawThread.Start();
        }
        static void DrawFunc()
        {
            while (drawing)
            {
                drawDone.WaitOne(10000);
                try
                {
                    lock (Boards)
                    {
                        ((PictureBox)DrawBox).Invalidate();
                    }
                }
                catch(Exception e)
                { }
                drawDone.Reset();
            }
        }
        static public void Message(string msg)
        {
            Label label = (Label)MessageBox;
            label.Text = msg;
        }
        #endregion
    }
    //Класс загрузки/сохранения примера
    class Parser
    {
        static public SetOfSigns[] ReadTask(out int cellsize)
        {
            SetOfSigns[] imgs;
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "Выберите файл";
            of.Filter = "Текстовые файлы|*.txt";

            cellsize = 1;

            if (of.ShowDialog() == DialogResult.OK)
            {
                TextReader tr = new StreamReader(of.FileName);
                cellsize = Int32.Parse(NextString(tr));
                int n = Int32.Parse(NextString(tr));
                imgs = new SetOfSigns[n];
                for (int i = 0; i < n; i++)
                {
                    string tmp = NextString(tr);
                    string[] objs = tmp.Split(';');
                    imgs[i] = new SetOfSigns(objs.Count() - 1);
                    for (int j = 0; j < objs.Count() - 1; j++)
                    {
                        string[] coords = objs[j].Split(',');
                        float[] crds = new float[coords.Count()];
                        for (int k = 0; k < crds.Count(); k++)
                        {
                            crds[k] = float.Parse(coords[k]);
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
        static public void WriteTask(SetOfSigns[] imgs,int cellsize)
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
                    tw.WriteLine(cellsize);
                    tw.WriteLine("//число образов");
                    tw.WriteLine(imgs.Count());
                    for (int i = 0; i < imgs.Count(); i++)
                    {
                        tw.WriteLine("//image" + (i + 1));
                        tw.WriteLine(imgs[i].ToString());
                    }
                    tw.Close();
                }
            }
        }
    }

    class MathUtils
    {
        static float det(float a1, float a2, float b1, float b2)
        {
            return (float)(a1 * b2 - a2 * b1);
        }
        static void input(float[] p1, float[] p2, out float a, out float b, out float c)
        {
            float x0 = p1[0];
            float y0 = p1[1];
            float x1 = p2[0];
            float y1 = p2[1];
            a = y1 - y0;
            b = x0 - x1;
            c = -((x1 - x0) * y0 - (y1 - y0) * x0);
        }
        static public float[] SegmentCross(float[] p1, float[] p2, float[] p3, float[] p4)
        {
            float[] arr1 = new float[3];
            float[] arr2 = new float[3];
            input(p1, p2, out arr1[0], out arr1[1], out arr1[2]);
            input(p3, p4, out arr2[0], out arr2[1], out arr2[2]);

            float d = det(arr1[0], arr1[1], arr2[0], arr2[1]);
            float d1 = det(arr1[2], arr1[1], arr2[2], arr2[1]);
            float d2 = det(arr1[0], arr1[2], arr2[0], arr2[2]);

            float[] rez = { (float)d1 / d, (float)d2 / d };

            return rez;
        }
        static public vectorObject LineCross(vectorObject x0, vectorObject y0, vectorObject p)
        {
            float A = y0[0] - x0[0];
            float B = y0[1] - x0[1];
            float C = -(A * p[0] + B * p[1]);

            float[] p4 = new float[2];

            p4[0] = -Utilities.Boards[0].Width/2;
            p4[1] = -(A * p4[0] + C) / B;

            float[] p1 = {x0[0], x0[1]};
            float[] p2 = {y0[0], y0[1]};
            float[] p3 = { Utilities.Boards[0].Width / 2, -(A * (Utilities.Boards[0].Width / 2) + C) / B };

            vectorObject rez = new vectorObject(SegmentCross(p1, p2, p3, p4));

            return rez;
        }
    }
}
