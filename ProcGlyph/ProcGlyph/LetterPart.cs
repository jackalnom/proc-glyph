using ProcGlyph.Strokes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph
{
    public class LetterPart
    {
        public GridPoint[] Points { get; set; }
        public Stroke StrokeType { get; set; }

        public LetterPart(GridPoint[] points, Stroke strokeType)
        {
            Points = points;
            StrokeType = strokeType;
        }

        public Drawable Render(Grid grid)
        {
            foreach (GridPoint point in Points)
            {
                if (!point.Valid())
                {
                    return null;
                }
            }
            return StrokeType.Draw(grid, Points);
        }
    }
}
