using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLib
{
    public class Board : GraphicsObject
    {
        int width, height;
        Board(int w, int h)
            : base()
        {
            width = w;
            height = h;
        }
        protected override void Draw()
        {

        }
    }
}
