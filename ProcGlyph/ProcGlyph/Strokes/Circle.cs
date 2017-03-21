using Cairo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Strokes
{
    public class Circle : Stroke
    {
        public Circle() {

        }

        private class CircleDraw : Drawable
        {
            private PointD center;
            private double width, height, lineWidth;

            public CircleDraw(PointD center, double width, double height, double lineWidth)
            {
                this.center = center;
                this.width = width;
                this.height = height;
                this.lineWidth = lineWidth;
            }

            public void Draw(Context gr)
            {
                gr.Save();

                gr.Scale(width, height);
                gr.Arc(center.X/width, center.Y/height, 1, 0, 2 * Math.PI);

                gr.Restore();

                gr.LineWidth = lineWidth;
                gr.Stroke();
            }
        }
        
        public int MinPoints()
        {
            return 1;
        }

        public int MaxPoints()
        {
            return 2;
        }

        public Drawable Draw(Grid grid, GridPoint[] points)
        {
            GridPoint center;
            int radius;

            if (points.Count() == 2)
            {
                center = GridPoint.MidPoint(points[0], points[1]);
                radius = GridPoint.Distance(points[0], points[1]) / 2;
            } else
            {
                center = points[0];
                if (!center.Valid())
                {
                    return null;
                }
                return new CircleDraw(grid.TranslateGridPoint(center), 1, 1, grid.LetterLineWidth*2);
            }

            if (center.X + radius > 6 || center.Y + radius > 6)
            {
                return null;
            }

            PointD centerVec = grid.TranslateGridPoint(center);
            double width = Math.Abs(centerVec.X - grid.TranslateGridPoint(new GridPoint(center.X + radius, center.Y)).X);
            double height = Math.Abs(centerVec.Y - grid.TranslateGridPoint(new GridPoint(center.X, center.Y + radius)).Y);

            if ((centerVec.X + width > grid.CanvasWidth) || (centerVec.X - width < 0) || (centerVec.Y + height > grid.CanvasHeight) || (centerVec.Y - width < 0) || width == 0 || height == 0)
            {
                return null;
            }

            return new CircleDraw(centerVec, width, height, grid.LetterLineWidth);
        }
    }
}
