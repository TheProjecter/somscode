using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLib
{
    public class GraphicsBoard
    {
        private List<GraphicsObject> elems = new List<GraphicsObject>();
        private GraphicsObject curr;
        private Graphics gs;
        public readonly int Width, Height;
        public readonly Bitmap bmp;
        public int cellsize = 5;
        public GraphicsBoard(int width, int height, Graphics gs)
        {
            this.Width = width;
            this.Height = height;
            bmp = new Bitmap(width, height);
            this.gs = gs;
            DrawAxis();
        }
        public Graphics Graphics
        {
            get { return gs;}
            set { gs = value; }
        }
        public void AddElem(GraphicsObject newElem)
        {
            elems.Add(newElem);
        }
        public void AddElem(GraphicsBoard board)
        {
            foreach (GraphicsObject go in board.elems)
                this.elems.Add(go);
        }
        public void RemoveElem(GraphicsObject remElem)
        {
            curr = remElem;
            int idx = elems.FindIndex(new Predicate<GraphicsObject>(FindElem));
            if (idx >= 0)
            {
                elems[idx].Hide(cellsize, bmp);
                elems.RemoveAt(idx);
            }
          
        }
        public void Draw(int cellsize)
        {
            this.cellsize = cellsize;
            DrawAxis();
            foreach (GraphicsObject elem in elems)
                elem.Draw(cellsize, bmp);
        }
        public void HideElem(GraphicsObject hidElem)
        {
            curr = hidElem;
            int idx = elems.FindIndex(new Predicate<GraphicsObject>(FindElem));
            if (idx >= 0)
                elems[idx].Hide(cellsize, bmp);
        }
        private bool FindElem(GraphicsObject elem)
        {
            try 
            {
                Point2f p = (Point2f)elem;
                Point2f c = (Point2f)curr;
                return p == c;
            }
            catch(Exception exLvl1)
            {
                try
                {
                    Line p = (Line)elem;
                    Line c = (Line)curr;
                    return p == c;
                }
                catch(Exception exLvl2)
                {
                    try
                    {
                        Set p = (Set)elem;
                        Set c = (Set)elem;
                        return p == c;
                    }
                    catch(Exception exLvl3)
                    {
                        return false;
                    }
                }
            }
        }
        private void DrawAxis()
        {
            Line y = new Line(gs, new Pen(Brushes.Black, 3), new Point2f(0, -Height / 2), new Point2f(0, Height/2));
            Line x = new Line(gs, new Pen(Brushes.Black, 3), new Point2f(-Width / 2, 0), new Point2f(Width/2, 0));
            y.Draw(1, bmp);
            x.Draw(1, bmp);
        }
        public void HideAll()
        {
            foreach (GraphicsObject go in elems)
                go.Hide(cellsize, bmp);
        }
        public void RemoveAll()
        {
            HideAll();
            elems.Clear();
            DrawAxis();
        }
    }
}
