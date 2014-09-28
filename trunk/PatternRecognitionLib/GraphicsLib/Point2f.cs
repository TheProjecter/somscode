using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLib
{
    public class Point2f : GraphicsObject
    {
        private Pen pen = Pens.Black;
        public readonly float x, y;
        public Point2f(float x, float y)
            : base()
        {
            this.x = x;
            this.y = y;
        }
        public Point2f(float x, float y, Pen pen)
            : base()
        {
            this.x = x;
            this.y = y;
            this.pen = pen;
        }
        public Pen Pen
        {
            get { return pen; }
            set { pen = value; }
        }
        protected override void DrawLocal(Graphics gs, int cellsize, Bitmap bmp)
        {
            if (!hide)
            {
                gs = Graphics.FromImage(bmp);
                gs.DrawEllipse(pen, (float)(bmp.Width / 2 + (x * cellsize) - 2), (float)(bmp.Height/2 + (y * cellsize) - 2), 4, 4);
                
                    
                gs.Dispose();
            }
        }
        protected override void HideLocal(Graphics gs, int cellsize, Bitmap bmp)
        {
            gs = Graphics.FromImage(bmp);
            gs.DrawEllipse(new Pen(Brushes.White, pen.Width), (float)(bmp.Width / 2 + (x * cellsize) - 2), 
                (float)(bmp.Height / 2 + (y * cellsize) - 2), 4, 4);
            gs.Dispose();
        }
        public static bool operator == (Point2f p1, Point2f p2)
        {
            return (p1.x == p2.x & p1.y == p2.y);
        }
        public static bool operator !=(Point2f p1, Point2f p2)
        {
            return !(p1.x == p2.x & p1.y == p2.y);
        }
    }
}
