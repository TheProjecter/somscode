using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognitionLib
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
        public vectorObject(params double[] _coords)
        {
            coords = _coords;
            n = coords.Count();
        }
        public int Size
        {
            set
            {
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
            for (int i = 0; i < n; i++)
            {
                coords[i] = 0;
            }
        }
        public double this[int Xn]
        {
            get { return coords[Xn]; }
            set { coords[Xn] = value; }
        }
        public static double operator *(vectorObject v1, vectorObject v2)
        {
            double tmp = 0;

            for (int i = 0; i < v1.n; i++)
            {
                tmp += v1[i] * v2[i];
            }

            return tmp;
        }
        public static vectorObject operator *(double alpha, vectorObject v)
        {
            vectorObject tmp = v;

            for (int i = 0; i < v.n; i++)
            {
                tmp[i] = alpha * v[i];
            }

            return tmp;
        }
        public static vectorObject operator +(vectorObject v1, vectorObject v2)
        {
            vectorObject tmp = v1;
            for (int i = 0; i < v1.n; i++)
            {
                tmp[i] += v2[i];
            }
            return tmp;
        }
        public static vectorObject operator -(vectorObject v1, vectorObject v2)
        {
            vectorObject tmp = v1;
            for (int i = 0; i < v1.n; i++)
            {
                tmp[i] -= v2[i];
            }
            return tmp;
        }
    //    public static bool operator ==(vectorObject v1, vectorObject v2)
    //    {
    //        int count = 0;
    //        for (int i = 0; i < v1.n; i++)
    //        {
    //            if (v1[i] == v2[i]) count++;
    //        }
    //        if (count == v1.n) return true;
    //        return false;
    //    }
    //    public static bool operator !=(vectorObject v1, vectorObject v2)
    //    {
    //        int count = 0;
    //        for (int i = 0; i < v1.n; i++)
    //        {
    //            if (v1[i] == v2[i]) count++;
    //        }
    //        if (count == v1.n) return false;
    //        return true;
    //    }
    }
}
