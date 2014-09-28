using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLib
{
    public class Set: GraphicsObject
    {
        private List<Point2f> mbrs = new List<Point2f>();
        public Set(Graphics gs) : base(gs)
        {
        }
        public void Add(float x, float y, Pen pen)
        {
            mbrs.Add(new Point2f(x, y, pen));
        }
        protected override void DrawLocal(Graphics gs, int cellsize, Bitmap bmp)
        {
            if (!hide)
                foreach (Point2f vect in mbrs)
                    vect.Draw(cellsize, bmp);
        }
        protected override void HideLocal(Graphics gs, int cellsize, Bitmap bmp)
        {
            foreach (Point2f vect in mbrs)
            {
                vect.Hide(cellsize, bmp);
            }
        }
        public static bool operator ==(Set s1, Set s2)
        {
            if (s1.mbrs.Count != s2.mbrs.Count)
                return false;
            int count = 0;
            foreach (Point2f p in s1.mbrs)
                for(int i=0; i<s2.mbrs.Count; i++)
                    if (p == s2.mbrs[i])
                    {
                        count++;
                        break;
                    }
            return count == s2.mbrs.Count;
        }
        public static bool operator !=(Set s1, Set s2)
        {
            if (s1.mbrs.Count != s2.mbrs.Count)
                return true;
            int count = 0;
            foreach (Point2f p in s1.mbrs)
                for (int i = 0; i < s2.mbrs.Count; i++)
                    if (p == s2.mbrs[i])
                    {
                        count++;
                        break;
                    }
            return !(count == s2.mbrs.Count);
        }
    }
}
