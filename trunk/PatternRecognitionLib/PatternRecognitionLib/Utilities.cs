using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PatternRecognitionLib
{
    public class Utilities
    {
        public static Image[] ReadTask()
        {
            Image[] img;
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "Выберите файл";
            of.Filter = "Текстовые файлы|*.txt";

            if (of.ShowDialog() == DialogResult.OK)
            {
                TextReader tr = new StreamReader(of.FileName);
                int n = Int32.Parse(NextString(tr));
                img = new Image[n];
                for (int i = 0; i < n; i++)
                {
                    string tmp = NextString(tr);
                    string[] objs = tmp.Split(';');
                    img[i] = new Image(objs.Count() - 1);
                    for (int j = 0; j < objs.Count() - 1; j++)
                    {
                        string[] coords = objs[j].Split(',');
                        double[] crds = new double[coords.Count()];
                        for (int k = 0; k < crds.Count(); k++)
                        {
                            crds[k] = Double.Parse(coords[k]);
                        }
                        img[i][j] = new vectorObject(crds);
                    }
                }
                return img;
            }

            return null;
        }
        private static string NextString(TextReader tr)
        {
            string tmp = tr.ReadLine();
            if (tmp.IndexOf('/')>-1)
            {
                tmp = NextString(tr);
            }
            return tmp;
        }
    }
}
