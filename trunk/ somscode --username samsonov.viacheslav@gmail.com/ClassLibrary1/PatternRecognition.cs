using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class vectorObject
    {
        private double[] coords;
        private int n;
        public vectorObject(int _n)
        {
            coords = new double[_n];
            n = _n;
        }
        public int Size()
        {
            return n;
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
    }

    public class Pattern
    {
        private vectorObject[] objects;
        public Pattern()
        {
            objects = new vectorObject[2];
        }
        public vectorObject this[int ObjN]
        {
            get { return objects[ObjN]; }
            set
            {
                if (ObjN < objects.Count())
                    objects[ObjN] = value;
                else
                {
                    vectorObject[] tmp = new vectorObject[ObjN];
                }
            }
        }
    }
}
