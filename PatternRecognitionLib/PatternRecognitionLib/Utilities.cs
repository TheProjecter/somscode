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
    public class Utilities
    {
        static public Image[] ReadTask()
        {
            return Parser.ReadTask();
        }
        static public void WriteTask(Image[] imgs)
        {
            Parser.WriteTask(imgs);
        }
    }
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

    class Drawer
    {
    }
}
