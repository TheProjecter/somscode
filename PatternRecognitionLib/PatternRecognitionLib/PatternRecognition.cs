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
        { X = _X; Y = _Y; }
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
        private void  KuhnTucker(ref double l1, ref double l2)
        {

        }
        //Поиск минимального расстояния между прямыми xp-x0 и yq-y0
        private void FindMinLength(vectorObject xp, vectorObject yq, 
                                   ref vectorObject x1, ref vectorObject y1)
        {
            double a = (y0 - x0) * (xp - x0);
            double b = (y0 - yq) * (y0 - yq);
            double c = (y0 - x0) * (y0 - yq);
            double d = (xp - x0) * (y0 - yq);
            double e = (xp - x0) * (xp - x0);
            double l1;
            double l2;

            if ((b * e) - (d * d) != 0)
            {
                l1 = ((a * b) - (c * d)) / ((b * e) - (d * d));
                l2 = ((c * e) - (a * d)) / ((b * e) - (d * d));
            }
            else
            {
                l1 = 0;
                l2 = ((x0 - y0) * (xp - x0)) / ((yq - y0) * (xp - x0));
            }
            if (!((l1 >= 0 & l1 < 1) & (l2 >= 0 & l2 < 1)))
            {
                KuhnTucker(ref l1, ref l2);
            }
            x1 = x0 + l1 * (xp - x0);
            y1 = y0 + l2 * (yq - y0);
        }
        #endregion
    }
}
