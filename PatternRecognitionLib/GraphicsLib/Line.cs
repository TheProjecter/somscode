using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsLib
{
    public class Line:GraphicsObject
    {
        protected Pen pen;
        protected Point2f b, e;
        public Line(Graphics gs, Pen pen, Point2f x, Point2f y) : base(gs)
        {
            this.pen = pen;
            this.b = x;
            this.e = y;
        }
        protected override void DrawLocal(Graphics gs, int cellsize, Bitmap bmp)
        {
            if (!hide)
            {
                try
                {
                    gs = Graphics.FromImage(bmp);
                    gs.DrawLine(pen, (float)(bmp.Width / 2 + (b.x * cellsize)), (float)(bmp.Height / 2 + (b.y * cellsize)),
                        (float)(bmp.Width / 2 + (e.x * cellsize)), (float)(bmp.Height / 2 + (e.y * cellsize)));
                    b.Draw(cellsize, bmp);
                    e.Draw(cellsize, bmp);
                    gs.Dispose();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        protected override void HideLocal(Graphics gs, int cellsize, Bitmap bmp)
        {
            gs = Graphics.FromImage(bmp);
            gs.DrawLine(Pens.White, b.x, b.y, e.x, e.y);
            gs.Dispose();
        }
        public static bool operator == (Line l1, Line l2)
        {
            return (((l1.b == l2.b) & (l1.e == l2.e)) || ((l1.b == l2.e) & (l1.e == l2.b)));
        }
        public static bool operator !=(Line l1, Line l2)
        {
            return !(((l1.b == l2.b) & (l1.e == l2.e)) || ((l1.b == l2.e) & (l1.e == l2.b)));
        }
    }
}
