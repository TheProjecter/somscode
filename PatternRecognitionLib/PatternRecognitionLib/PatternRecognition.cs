using GraphicsLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Util;

namespace PatternRecognitionLib
{
    //Класс линейного решающего правила минимакса
    public class MinMaxRule
    {
        #region Private Fields
        private SetOfSigns X, Y;
        private vectorObject x0, y0;
        private vectorObject W;
        #endregion
        #region Public Fields
        public double gamma;
        public double delta;
        #endregion
        #region Consructor
        public MinMaxRule(SetOfSigns _X, SetOfSigns _Y)
        {
            X = _X; 
            Y = _Y; 
            x0 = X[0]; 
            y0 = Y[0];
        }
        #endregion
        #region Public Methods
        public void BuildRule()
        {
            
            Utilities.Boards.Add(new GraphicsBoard(Utilities.Boards[0].Width, Utilities.Boards[0].Height,
                Utilities.Boards[0].Graphics));  //инициализируем класс-список объектов текущего состояния экрана
            Utilities.Boards[Utilities.Boards.Count - 1].AddElem(Utilities.Boards[0]);  //Добавляем на экран отображение точек множеств
            Pen dpen = new Pen(Brushes.Black, 2);
            dpen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; //пунктирная линия

            vectorObject x1;
            vectorObject y1;
            
            while (true)
            {
                vectorObject xp = new vectorObject(x0.Size);
                vectorObject yq = new vectorObject(y0.Size);

                FindMaxPrs(ref xp, ref yq);

                x1 = new vectorObject(x0.Size);
                y1 = new vectorObject(y0.Size);

                FindMinLength(ref xp, ref yq, ref x1, ref y1);
                W = FindHyperplane(x1, y1);

                vectorObject[] XXPcrds = {x0, xp};
                vectorObject[] YYQcrds = {y0, yq};
                vectorObject[] XYcrds1 = {x1, y1};
                vectorObject[] G = Utilities.GetNewCoords(W, x1, y1);
                if (Utilities.Boards[0].cellsize > 1)
                {
                    XXPcrds = Utilities.GetNewCoords(x0, xp);  //Получаем координаты для отрисовки
                    YYQcrds = Utilities.GetNewCoords(y0, yq);
                    XYcrds1 = Utilities.GetNewCoords(x1, y1);
                    G = Utilities.GetNewCoords2(W, x1, y1);
                }
                Utilities.Boards.Add(new GraphicsBoard(Utilities.Boards[0].Width, Utilities.Boards[0].Height,
                            Utilities.Boards[0].Graphics)); //инициализируем класс-список объектов текущего состояния экрана
                Utilities.Boards[Utilities.Boards.Count - 1].AddElem(Utilities.Boards[0]); //Добавляем на экран отображение точек множеств
                Utilities.Boards[Utilities.Boards.Count - 1].AddElem(new Line(Utilities.Boards[0].Graphics, dpen,
                    new Point2f(XXPcrds[0][0],XXPcrds[0][1], new Pen(Brushes.Green, 3)), 
                    new Point2f(XXPcrds[1][0], XXPcrds[1][1], new Pen(Brushes.Green, 3)))); //прямая (x0,xp)
                Utilities.Boards[Utilities.Boards.Count - 1].AddElem(new Line(Utilities.Boards[0].Graphics, dpen,
                    new Point2f(YYQcrds[0][0], YYQcrds[0][1], new Pen(Brushes.Green, 3)),
                    new Point2f(YYQcrds[1][0], YYQcrds[1][1], new Pen(Brushes.Green, 3)))); //прямая (y0,yq)
                Utilities.Boards[Utilities.Boards.Count - 1].AddElem(new Line(Utilities.Boards[0].Graphics, dpen,
                    new Point2f(XYcrds1[0][0], XYcrds1[0][1], new Pen(Brushes.Green, 3)),
                    new Point2f(XYcrds1[1][0], XYcrds1[1][1], new Pen(Brushes.Green, 3))));
                Utilities.Boards[Utilities.Boards.Count - 1].AddElem(new Line(Utilities.Boards[0].Graphics, new Pen(Brushes.Black, 3),
                    new Point2f(G[0][0], G[0][1]), new Point2f(G[1][0], G[1][1])));
                Utilities.Boards[Utilities.Boards.Count - 1].Draw(Utilities.Boards[0].cellsize);//прямая (y1,x1)
                Utilities.drawDone.Set();
                Thread.Sleep(1500);

                if (IsSeparating(delta))
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
                if (!Utilities.multi)
                {
                    Utilities.nextStep.WaitOne();
                    Utilities.nextStep.Reset();
                }
            }
        }
        #endregion
        #region Функции алгоритма
        //Поиск Xp и Yq
        private void FindMaxPrs(ref vectorObject xp, ref vectorObject yq)
        {
            Pen dpen = new Pen(Brushes.Black, 2);
            dpen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; //пунктирная линия
            
            double maxX = 0;
            double maxY = 0;
            for (int i = 0; i < Math.Max(X.Count, Y.Count); i++)
            {
                Utilities.Boards.Add(new GraphicsBoard(Utilities.Boards[0].Width, Utilities.Boards[0].Height,
                            Utilities.Boards[0].Graphics)); //инициализируем класс-список объектов текущего состояния экрана
                Utilities.Boards[Utilities.Boards.Count - 1].AddElem(Utilities.Boards[0]); //Добавляем на экран отображение точек множеств
                vectorObject Xcrds = x0;
                vectorObject Ycrds = y0;
                if (Utilities.Boards[0].cellsize > 1)
                {
                    Xcrds = Utilities.ReCoord2D(x0); //Получаем координаты для отрисовки
                    Ycrds = Utilities.ReCoord2D(y0);
                }
                Utilities.Boards[Utilities.Boards.Count - 1].AddElem(new Point2f(Xcrds[0], Xcrds[1],
                    new Pen(Brushes.HotPink, 3))); //добавляем на экран точку x0
                Utilities.Boards[Utilities.Boards.Count - 1].AddElem(new Point2f(Ycrds[0], Ycrds[1],
                    new Pen(Brushes.HotPink, 3))); //добавляем на экран точку y0
                Utilities.Boards[Utilities.Boards.Count - 1].AddElem(new Line(Utilities.Boards[0].Graphics, new Pen(Brushes.Orchid, 2), 
                    new Point2f(Xcrds[0], Xcrds[1]), new Point2f(Ycrds[0], Ycrds[1]))); // рисуем линию от x0 до y0
                if (i < X.Count)
                {
                    if ((X[i] - x0) * (y0 - x0) > maxX)
                    {
                        maxX = (X[i] - x0) * (y0 - x0);
                        xp = X[i];
                    }
                    if (x0 != X[i])
                    {
                        vectorObject crds = MathUtils.LineCross(x0, y0, X[i]);
                        vectorObject tmp = X[i];
                        if (Utilities.Boards[0].cellsize > 1)
                        {
                            crds = Utilities.ReCoord2D(crds);
                            tmp = Utilities.ReCoord2D(X[i]);
                        }
                        Utilities.Boards[Utilities.Boards.Count - 1].AddElem(new Line(Utilities.Boards[0].Graphics, new Pen(Brushes.Black, 2),
                        new Point2f(Xcrds[0], Xcrds[1]), new Point2f(tmp[0], tmp[1])));//рисуем линию от x0 до Xi
                        Utilities.Boards[Utilities.Boards.Count - 1].AddElem(new Line(Utilities.Boards[0].Graphics, dpen,
                        new Point2f(tmp[0], tmp[1]), new Point2f(crds[0], crds[1])));//рисуем линию от Xi до проекции Xi на прямую (x0,y0)
                    }
                }
                if (i < Y.Count)
                {
                    if ((Y[i] - y0) * (x0 - y0) > maxY)
                    {
                        maxY = (Y[i] - y0) * (x0 - y0);
                        yq = Y[i];
                    }
                    if (y0 != Y[i])
                    {
                        vectorObject crds = MathUtils.LineCross(x0, y0, Y[i]);
                        vectorObject tmp = Y[i];
                        if (Utilities.Boards[0].cellsize > 1)
                        {
                            crds = Utilities.ReCoord2D(crds);
                            tmp = Utilities.ReCoord2D(Y[i]);
                        }
                        Utilities.Boards[Utilities.Boards.Count - 1].AddElem(new Line(Utilities.Boards[0].Graphics, new Pen(Brushes.Black, 2),
                        new Point2f(Ycrds[0], Ycrds[1]), new Point2f(tmp[0], tmp[1])));//рисуем линию от y0 до Yi
                        Utilities.Boards[Utilities.Boards.Count - 1].AddElem(new Line(Utilities.Boards[0].Graphics, dpen,
                        new Point2f(tmp[0], tmp[1]), new Point2f(crds[0], crds[1])));//рисуем линию от Yi до проекции Yi на прямую (x0,y0)
                    }
                }
                Utilities.Boards[Utilities.Boards.Count - 1].Draw(Utilities.Boards[0].cellsize);
                Utilities.drawDone.Set();
                Thread.Sleep(1000);
                if (!Utilities.multi)
                {
                    Utilities.nextStep.WaitOne();
                    Utilities.nextStep.Reset();
                }
            }
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

        //Не помню, что проверяет, но что-то проверяет..............
        private bool IsSeparating(double delta)
        {
            return (W.Norm() > delta);
        }
        
        //Критерий останова
        private bool StoppingCriterion(vectorObject x1, vectorObject y1, double gamma)
        {
            //vectorObject point = new vectorObject((x1[0] + y1[0]) / 2, (x1[1] + y1[1]) / 2);
            //float A = y1[0] - x1[0];
            //float B = y1[1] - x1[1];
            //float C = -(A * point[0] + B * point[1]);
            //double min = Math.Abs((A * X[0][0] + B * X[0][1] + C) / Math.Sqrt((A * A) - (B * B)));

            double p = (x1-y1).Norm();

            //for (int i = 1; i < X.Count; i++)
            //{
            //    if (Math.Abs((A * X[i][0] + B * X[i][1] + C) / Math.Sqrt((A*A) - (B*B))) < min)
            //    {
            //        min = Math.Abs((A * X[i][0] + B * X[i][1] + C) / Math.Sqrt((A * A) - (B * B)));
            //    }
            //}
            //for (int i = 0; i < Y.Count; i++)
            //{
            //    if (Math.Abs((A * Y[i][0] + B * Y[i][1] + C) / Math.Sqrt((A * A) - (B * B))) < min)
            //    {
            //        min = Math.Abs((A * Y[i][0] + B * Y[i][1] + C) / Math.Sqrt((A * A) - (B * B)));
            //    }
            //}
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
        #region Private Fields
        private List<MinMaxRule> mxRuleList = new List<MinMaxRule>();
        #endregion
        #region Constructor
        public LinRule(SetOfSigns[] imgs)
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
        #endregion
        #region Public Methods
        public void BuildRules(double gamma, double omega)
        {
            foreach(MinMaxRule rule in mxRuleList)
            {
                rule.gamma = gamma;
                rule.delta = omega;
                Thread mathTread = new Thread(rule.BuildRule);
                mathTread.Start();
            }
        }
        #endregion
    }
}
