using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatternRecognitionLib
{
    //Класс линейного решающего правила минимакса
    public class MinMaxRule
    {
        private Image X, Y;
        private vectorObject x0, y0;
        private vectorObject W;
        public double gamma;
        public double omega;
        public MinMaxRule(Image _X, Image _Y)
        {
            X = _X; 
            Y = _Y; 
            x0 = X[0]; 
            y0 = Y[0];
        }
        public void BuildRule()
        {
            List<object> drawList = new List<object>();
            vectorObject x1;
            vectorObject y1;
            while (true)
            {
                drawList.Add(X);
                drawList.Add(Y);

                vectorObject xp = new vectorObject(x0.Size);
                vectorObject yq = new vectorObject(y0.Size);

                FindMaxPrs(ref xp, ref yq);

                x1 = new vectorObject(x0.Size);
                y1 = new vectorObject(y0.Size);

                FindMinLength(ref xp, ref yq, ref x1, ref y1);
                vectorObject[] x = { x0, xp };
                vectorObject[] y = { y0, yq };
                drawList.Add(x);
                drawList.Add(y);
                W = FindHyperplane(x1, y1);
                vectorObject[] vect1 = { x1, y1 };
                drawList.Add(vect1);
                vectorObject[] vect2 = { W, x1, y1 };
                drawList.Add(vect2);
                if (IsSeparating(omega))
                {
                    if (IsRightPlane(x1, y1))
                    {
                        if (StoppingCriterion(x1, y1, gamma))
                        {
                            break;
                        }
                    }
                    x0 = x1;
                    y0 = y1;
                }
                else
                {
                    break;
                }
                List<object> tmp = new List<object>(drawList);
                Utilities.mainlist.Add(tmp);
                drawList.Clear();
            }
            Utilities.mainlist.Add(drawList);
            Utilities.drawDone.Set();
        }

        #region Функции алгоритма
        //Поиск Xp и Yq
        private void FindMaxPrs(ref vectorObject xp, ref vectorObject yq)
        {
            List<object> drawPList = new List<object>();
            drawPList.Add(X);
            drawPList.Add(Y);

            double max = 0;
            for(int i=0; i<X.Count; i++)
            {
                if ((X[i] - x0) * (y0 - x0) > max)
                {
                    max = (X[i] - x0) * (y0 - x0);
                    xp = X[i];
                }
                if (x0 != X[i])
                {
                    vectorObject[] vect = { x0, X[i] };
                    drawPList.Add(vect);
                }
            }

            max = 0;

            for (int i = 0; i < Y.Count; i++)
            {
                if ((Y[i] - y0) * (x0 - y0) > max)
                {
                    max = (Y[i] - y0) * (x0 - y0);
                    yq = Y[i];
                }
                if (y0 != Y[i])
                {
                    vectorObject[] vect = { y0, Y[i] };
                    drawPList.Add(vect);
                }
            }
            Utilities.mainlist.Add(drawPList);
        }

        //Вычисляем коэфициенты по т. Куна-Такера
        private void  KuhnTucker(ref float l1, ref float l2, vectorObject xp, vectorObject yq)
        {
            #region объявление и инициализация переменных
            float a = (y0 - x0) * (xp - x0);
            float b = (y0 - yq) * (y0 - yq);
            float c = (y0 - x0) * (y0 - yq);
            float d = (xp - x0) * (y0 - yq);
            float e = (xp - x0) * (xp - x0);
            float f = (y0 - xp) * (y0 - yq);
            float h = (yq - x0) * (xp - x0);
            #endregion
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
        private void FindMinLength(ref vectorObject xp, ref vectorObject yq, 
                                    ref vectorObject x1, ref vectorObject y1)
        {
            float l1 = 0;
            float l2 = 0;

            KuhnTucker(ref l1, ref l2, xp, yq);
           
            x1 = x0 + l1 * (xp - x0);
            y1 = y0 + l2 * (yq - y0);
        }

        //Поиск гиперплоскости
        private vectorObject FindHyperplane(vectorObject x1, vectorObject y1)
        {
            vectorObject _w = new vectorObject(x1.Size);
            _w = x1 - y1;
            return _w.Normalized();
        }
        
        //Проверка является ли гиперплоскость разделяющей
        private bool IsRightPlane(vectorObject x1, vectorObject y1)
        {
            for (int i = 0; i < X.Count; i++)
            {
                if ((W * X[i] - W * (x1 + y1) / 2) < 0)
                    return false;
            }
            for (int i = 0; i < Y.Count; i++)
            {
                if ((W * Y[i] - W * (x1 + y1) / 2) > 0)
                    return false;
            }
            return true;
        }

        private bool IsSeparating(double omega)
        {
            return (W.Norm() > omega);
        }
        
        //Критерий останова
        private bool StoppingCriterion(vectorObject x1, vectorObject y1, double gamma)
        {
            double p = Math.Abs((x1 - y1).Norm());
            double min = Math.Abs((W * X[0]) / W.Norm());
            for (int i = 0; i < X.Count; i++)
            {
                if (Math.Abs((W * X[i]) / W.Norm()) < min)
                {
                    min = Math.Abs((W * X[i]) / W.Norm());
                }
            }
            for (int i = 0; i < Y.Count; i++)
            {
                if (Math.Abs((W * Y[i]) / W.Norm()) < min)
                {
                    min = Math.Abs((W * Y[i]) / W.Norm());
                }
            }

            return (2*min>=gamma*p);
        }
        #endregion
    }

    //Класс-менеджер библиотеки
    public class LinRule
    {
        List<MinMaxRule> mxRuleList = new List<MinMaxRule>();

        public LinRule(Image[] imgs)
        {
            for (int i = 0; i < imgs.Count(); i++)
            {
                for (int j = i + 1; j < imgs.Count(); j++)
                {
                    MinMaxRule rl = new MinMaxRule(imgs[i], imgs[j]);
                    mxRuleList.Add(rl);
                }
            }
        }

        public void BuildRules(double gamma, double omega)
        {
            foreach(MinMaxRule rule in mxRuleList)
            {
                rule.gamma = gamma;
                rule.omega = omega;
                Thread mathTread = new Thread(rule.BuildRule);
                mathTread.Start();
            }
        }
    }
}
