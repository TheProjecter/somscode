using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLib
{
    public abstract class GraphicsObject
    {
        public GraphicsObject()
        { }
        protected abstract void Draw();
    }
}
