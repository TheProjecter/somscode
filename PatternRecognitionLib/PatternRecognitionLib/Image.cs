﻿using System;
using System.Collections;
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
        { 
            objects = new vectorObject[2]; 
            count = 2; 
        }
        public Image(int n)
        { 
            objects = new vectorObject[n]; 
            count = n; 
        }
        public int Count
        {
            get { return count; }
        }
        public vectorObject this[int ObjN]
        {
            get { return objects[ObjN]; }
            set
            {
                if (ObjN >= count)
                {
                    vectorObject[] New = new vectorObject[ObjN+1];
                    for(int i=0; i<count; i++)
                    {
                        New[i] = objects[i];
                    }
                    objects = New;
                    count = ObjN + 1;
                }
                objects[ObjN] = value;
            }
        }

        public string ToString()
        {
            string tmp = "";

            for (int i = 0; i < count; i++)
            {
                tmp += objects[i].ToString()+";";
            }
            return tmp;
        }
    }
}
