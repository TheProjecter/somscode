using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognitionLib
{
    //Класс линейного решающего правила минимакса
    public class MinMaxRule
    {
        private Image X, Y;
        private vectorObject x0, y0;
        public MinMaxRule(Image _X, Image _Y)
        { 
            X = _X; 
            Y = _Y; 
            x0 = X[0]; 
            y0 = Y[0]; 
        }
        #region Функции алгоритма
        //Поиск Xp и Yq
        private void FindMaxPrs(ref vectorObject xp, ref vectorObject yq)
        {
            vectorObject[] tmp = new vectorObject[2];
            double max = 0;
            for(int i=0; i<X.Count; i++)
            {
                tmp[0] = X[i];
                if ((xp - x0) * (y0 - x0) > max)
                {
                    max = (xp - x0) * (y0 - x0);
                    xp = tmp[0];
                }
            }
            max = 0;
            for (int i = 0; i < Y.Count; i++)
            {
                tmp[1] = Y[i];
                if ((yq - y0) * (x0 - y0) > max)
                {
                    max = (xp - x0) * (y0 - x0);
                    yq = tmp[1];
                }
            }
        }

        //Вычисляем коэфициенты по т. Куна-Такера
        private void  KuhnTucker(ref double l1, ref double l2, vectorObject xp, vectorObject yq)
        {
            double a = (y0 - x0) * (xp - x0);
            double b = (y0 - yq) * (y0 - yq);
            double c = (y0 - x0) * (y0 - yq);
            double d = (xp - x0) * (y0 - yq);
            double e = (xp - x0) * (xp - x0);
            double f = (y0 - xp) * (y0 - yq);
            double h = (yq - x0) * (xp - x0);
            //36
            if ((b * e) - (d * d) != 0)
            {
                //37
                l1 = ((a * b) - (c * d)) / ((b * e) - (d * d));
                l2 = ((c * e) - (a * d)) / ((b * e) - (d * d));

                if (l1<=0)
                {
                    //38
                    l2 = c / b;
                }
                else
                {
                    if (l1>=1)
                    {
                        //41
                        l2 = f / b;
                    }
                }
            }
            else
            {
                l1 = 0;
                l2 = c / b;
            }
            //40
            if (l2 <= 0) 
            { 
                //42
                l1 = a / e;
            }
            else
            {
                //43
                if (l2>=1)
                {
                    //45
                    l1 = h / e;
                }
            }
            //55
            if (l1 <= 0)
            {
                //56
                l1 = 0;
            }
            else
            {
                if (l1 > 1)
                {
                    l1 = 1;
                }
            }
            //58
            if (l2<=0)
            {
                l2 = 0;
            }
            else
            {
                if (l2>1)
                {
                    l2 = 1;
                }
            }

            //if (!((l1 > 0 & l1 < 1) & (l2 > 0 & l2 < 1)))
            //{
            //    KuhnTucker(ref l1, ref l2, xp, yq);
            //}
        }

        //Поиск минимального расстояния между прямыми xp-x0 и yq-y0
        private void FindMinLength(ref vectorObject x1, ref vectorObject y1)
        {
            vectorObject xp = new vectorObject(x1.Size);
            vectorObject yq = new vectorObject(y1.Size);
            FindMaxPrs(ref xp, ref yq);
            double l1 = 0;
            double l2 = 0;

            KuhnTucker(ref l1, ref l2, xp, yq);
           
            x1 = x0 + l1 * (xp - x0);
            y1 = y0 + l2 * (yq - y0);
        }
        #endregion
    }
    //Класс-менеджер библиотеки
    public class LinRule
    {
        List<MinMaxRule> mxRuleList = new List<MinMaxRule>();
        public LinRule()
        {
            Image[] imgs = Utilities.ReadTask();
            for(int i=0; i<imgs.Count(); i++)
            {
                for (int j=i+1; j<imgs.Count(); j++)
                {
                    MinMaxRule rl = new MinMaxRule(imgs[i], imgs[j]);
                    mxRuleList.Add(rl);
                }
            }
        }
    }
}
