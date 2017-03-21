using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cairo;

namespace ProcGlyph.Strokes
{
    public class Curved : Stroke
    {
        public Curved()
        {

        }
      
        private class CurvedDraw : Drawable
        {
            private PointD[] points;
            private double lineWidth;

            public CurvedDraw(PointD[] points, double lineWidth)
            {
                this.points = points;
                this.lineWidth = lineWidth;
            }

            public void Draw(Context gr)
            {  
                gr.LineWidth = lineWidth;         
                gr.CurveTo(points[0], points[1], points[2]);
                if (points.Count() == 4)
                {
                    gr.CurveTo(points[2], points[0], points[3]);
                }
                gr.Stroke();      
            }
        }

        public int MinPoints()
        {
            return 2;
        }

        public int MaxPoints()
        {
            return 4;
        }

        private static PointD normalize(PointD a)
        {
            double mag = Math.Sqrt(a.X * a.X + a.Y * a.Y);
            return new PointD(a.X / mag, a.Y / mag);
        }

        public Drawable Draw(Grid grid, GridPoint[] points)
        {
            PointD[] translatedPoints;
            if (points.Count() <= 3)
            {
                translatedPoints = new PointD[3];
                translatedPoints[0] = grid.TranslateGridPoint(points[0]);
                translatedPoints[2] = grid.TranslateGridPoint(points[1]);
                translatedPoints[1] = points.Count() == 2 ? grid.TranslateGridPoint(grid.Gravity) : grid.TranslateGridPoint(points[2]);
            } else
            {
                translatedPoints = new PointD[4];
                translatedPoints[0] = grid.TranslateGridPoint(points[0]);
                translatedPoints[1] = grid.TranslateGridPoint(points[1]);
                translatedPoints[2] = grid.TranslateGridPoint(points[2]);
                translatedPoints[3] = grid.TranslateGridPoint(points[3]);
            }

            return new CurvedDraw(translatedPoints, grid.LetterLineWidth);
        }
    }
}
