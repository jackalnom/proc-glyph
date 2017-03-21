using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Strokes
{
    public interface Stroke
    {
        int MinPoints();
        int MaxPoints();
        Drawable Draw(Grid grid, GridPoint[] points);
    }
}
