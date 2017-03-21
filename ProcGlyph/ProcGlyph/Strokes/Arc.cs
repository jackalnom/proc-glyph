using Cairo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Strokes
{
    public class Arc : Stroke
    {
        public Arc() {

        }

        private class ArcDraw : Drawable
        {
            private PointD center;
            private double width, height, lineWidth, startAngle, endAngle;

            public ArcDraw(PointD center, double width, double height, double startAngle, double endAngle, double lineWidth)
            {
                this.center = center;
                this.width = width;
                this.height = height;
                this.lineWidth = lineWidth;
                this.startAngle = startAngle;
                this.endAngle = endAngle;
            }

            public void Draw(Context gr)
            {
                gr.Save();
                gr.Scale(width, height);
                gr.Arc(center.X/width, center.Y/height, 1, startAngle, endAngle);  
                gr.Restore();

                gr.LineWidth = lineWidth;
                gr.Stroke();
            }
        }


        public int MinPoints()
        {
            return 2;
        }

        public int MaxPoints()
        {
            return 3;
        }

        private static PointD normalize(PointD a)
        {
            double mag = Math.Sqrt(a.X * a.X + a.Y * a.Y);
            return new PointD(a.X / mag, a.Y / mag);
        }

        public Drawable Draw(Grid grid, GridPoint[] points)
        {
            GridPoint a;
            GridPoint b;
            GridPoint c;

            if (points.Count() == 3)
            {
                a = points[0];
                b = points[1];
                c = points[2];
            } else
            {
                a = points[0];
                b = grid.Gravity;
                c = points[1];
            }

            GridPoint gap = GridPoint.MidPoint(a, c);
            GridPoint center = GridPoint.MidPoint(b, gap);
            int radius = GridPoint.Distance(b, gap) / 2;

            if (center.X + radius > 6 || center.Y + radius > 6)
            {
                return null;
            }

            PointD centerVec = grid.TranslateGridPoint(center);
            double width = Math.Abs(centerVec.X - grid.TranslateGridPoint(new GridPoint(center.X + radius, center.Y)).X);
            double height = Math.Abs(centerVec.Y - grid.TranslateGridPoint(new GridPoint(center.X, center.Y + radius)).Y);

            PointD directionToLeftArm = normalize(new PointD(a.X - center.X, a.Y - center.Y));
            PointD directionToRightArm = normalize(new PointD(c.X - center.X, c.Y - center.Y));

            double startAngle = (Math.Atan2(directionToRightArm.Y, -directionToRightArm.X));
            double endAngle = (Math.Atan2(directionToLeftArm.Y, -directionToLeftArm.X));

            if ((centerVec.X + width > grid.CanvasWidth) || (centerVec.X - width < 0) || (centerVec.Y + height > grid.CanvasHeight) || (centerVec.Y - width < 0) || width == 0 || height == 0)
            {
                return null;
            }

            return new ArcDraw(centerVec, width, height, startAngle, endAngle, grid.LetterLineWidth);
        }
    }
}
