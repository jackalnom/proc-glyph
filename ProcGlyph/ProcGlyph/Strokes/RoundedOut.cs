using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cairo;

namespace ProcGlyph.Strokes
{
    public class RoundedOut : Stroke
    {
        public RoundedOut()
        {

        }

        public int MinPoints()
        {
            return 2;
        }

        public int MaxPoints()
        {
            return 10;
        }

        private class LineDraw : Drawable
        {
            private PointD[] points;
            private PointD center;
            private double lineWidth;

            public LineDraw(PointD[] points, PointD center, double lineWidth)
            {
                this.points = points;
                this.lineWidth = lineWidth;
                this.center = center;
            }

            public void Draw(Context gr)
            {  
                gr.LineWidth = lineWidth;

                gr.MoveTo(points[0]);

                for (int i = 1; i < points.Count(); i++)
                {
                    PointD midPoint = new PointD((points[i - 1].X + points[i].X) / 2, (points[i - 1].Y + points[i].Y) / 2);
                    PointD dir = new PointD(center.X - midPoint.X, center.Y - midPoint.Y);
                    PointD archTo = new PointD(midPoint.X - dir.X, midPoint.Y - dir.Y);
                    gr.CurveTo(points[i-1], archTo, points[i]);
                }

                gr.Stroke();
            }
        }

        public Drawable Draw(Grid grid, GridPoint[] points)
        {
            GridPoint center = GridPoint.MidPoint(points);
            return new LineDraw(points.Select(p => grid.TranslateGridPoint(p)).ToArray(), grid.TranslateGridPoint(center), grid.LetterLineWidth);

        }
    }
}
