using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognitionLib
{
    //Класс образа
    public class Image
    {
        private vectorObject[] objects;
        private int count;
        public Image()
        { objects = new vectorObject[2]; count = 2; }
        public Image(int n)
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
}
