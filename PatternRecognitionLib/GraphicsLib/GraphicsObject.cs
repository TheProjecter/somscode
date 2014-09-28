using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLib
{
    public abstract class GraphicsObject
    {
        Graphics gs;
        protected bool hide = false;
        public GraphicsObject()
        { }
        public GraphicsObject(Graphics gs)
        {
            this.gs = gs;
        }
        public void Draw(int cellsize, Bitmap bmp)
        {
            DrawLocal(gs, cellsize, bmp);
        }
        public void Hide(int cellsize, Bitmap bmp)
        {
            hide = true;
            HideLocal(gs, cellsize, bmp);
        }
        protected abstract void DrawLocal(Graphics gs, int cellsize, Bitmap bmp);
        protected abstract void HideLocal(Graphics gs, int cellsize, Bitmap bmp);
    }
}
