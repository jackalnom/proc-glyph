using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cairo;

namespace ProcGlyph
{
    public class GridLine : Drawable
    {
        private PointD a, b;

        public GridLine(PointD a, PointD b)
        {
            this.a = a;
            this.b = b;
        }

        public void Draw(Context gr)
        {
            gr.Antialias = Antialias.Subpixel;    // sets the anti-aliasing method
            gr.LineWidth = 1;          // sets the line width
            gr.SetSourceRGBA(1, 0, 0, 1);   // red, green, blue, alpha
            gr.MoveTo(a);          // sets the Context's start point.
            gr.LineTo(b);          // draws a "virtual" line from 5,5 to 20,30
            gr.Stroke();          //stroke the line to the image surface
        }
    }

}
