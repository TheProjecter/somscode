using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    //Класс вектор-объекта
    public class vectorObject
    {
        private double[] coords;
        private int n = 0;
        public vectorObject(int _n)
        {
            if (_n != 0)
            {
                coords = new double[_n];
            }
            n = _n;
        }
        public int Size
        {
            set {
                if (n == 0)
                {
                    n = value;
                    coords = new double[n];
                }
            }
            get { return n; }
        }
        public void Zero()
        {
            for(int i=0; i<n;i++)
            {
                coords[i] = 0;
            }
        }
        public double this[int Xn]
        {
            get { return coords[Xn]; }
            set { coords[Xn] = value; }
        }
        public static double operator * (vectorObject v1, vectorObject v2)
        {
            double tmp = 0;

            for (int i = 0; i < v1.n; i++)
            {
                tmp+=v1[i]*v2[i];
            }

            return tmp;
        }
        public static vectorObject operator + (vectorObject v1, vectorObject v2)
        {
            vectorObject tmp = v1;
            for (int i = 0; i < v1.n; i++)
            {
                tmp[i] += v2[i];
            }
            return tmp;
        }
        public static vectorObject operator - (vectorObject v1, vectorObject v2)
        {
            vectorObject tmp = v1;
            for (int i = 0; i < v1.n; i++)
            {
                tmp[i] -= v2[i];
            }
            return tmp;
        }
        public static bool operator == (vectorObject v1, vectorObject v2)
        {
            int count = 0;
            for (int i = 0; i < v1.n; i++)
            {
                if (v1[i] == v2[i]) count++;
            }
            if (count == v1.n) return true;
            return false;
        }
        public static bool operator != (vectorObject v1, vectorObject v2)
        {
            int count = 0;
            for (int i = 0; i < v1.n; i++)
            {
                if (v1[i] == v2[i]) count++;
            }
            if (count == v1.n) return false;
            return true;
        }
    }
    //Класс образа
    public class Pattern
    {
        private vectorObject[] objects;
        private int count;
        public Pattern()
        { objects = new vectorObject[2]; count = 2; }
        public Pattern(int n)
        { objects = new vectorObject[n]; count = n; }
        public int Count
        { 
            get { return count; } 
        }
        public vectorObject this[int ObjN]
        {
            get { return objects[ObjN]; }
            set
            {
                if (ObjN < count)
                    objects[ObjN] = value;
                else
                {
                    vectorObject[] tmp = new vectorObject[ObjN];
                }
            }
        }
    }
    //Класс линейного решающего правила минимакса
    public class MinMaxRule
    {
        private Pattern X, Y;
        private vectorObject x0, y0;
        public MinMaxRule(Pattern _X, Pattern _Y)
        { X = _X; Y = _Y; }
        #region Функции алгоритма
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
        private void FindMinLength(vectorObject xp, vectorObject yq)
        {
            double a = (y0 - x0) * (xp - x0);
            double b = (y0 - yq) * (y0 - yq);
            double c = (y0 - x0) * (y0 - yq);
            double d = (xp - x0) * (y0 - yq);
            double e = (xp - x0) * (xp - x0);

            double l1 = ((a * b) - (c * d)) / ((b * e) - (d * d));
            double l2 = ((c * e) - (a * d)) / ((b * e) - (d * d));
        }
        #endregion
    }
}
