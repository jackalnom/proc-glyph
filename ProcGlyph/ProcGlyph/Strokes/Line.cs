using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cairo;

namespace ProcGlyph.Strokes
{
    public class Line : Stroke
    {
        public Line()
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
            private double lineWidth;

            public LineDraw(PointD[] points, double lineWidth)
            {
                this.points = points;
                this.lineWidth = lineWidth;
            }

            public void Draw(Context gr)
            {  
                gr.LineWidth = lineWidth;       

                gr.MoveTo(points[0]);    

                foreach (PointD point in points)
                {
                    gr.LineTo(point); 
                }

                gr.Stroke(); 
                /*
                gr.Antialias = Antialias.Subpixel;
                gr.LineWidth = lineWidth;

                gr.MoveTo(points[0]);
                for (int i = 1; i < points.Count(); i++)
                {
                    gr.CurveTo(points[i-1], new PointD(points[i].X - 10, points[i].Y - 10), points[i]);
                }

                gr.Stroke();*/
            }
        }

        public Drawable Draw(Grid grid, GridPoint[] points)
        {
            return new LineDraw(points.Select(p => grid.TranslateGridPoint(p)).ToArray(), grid.LetterLineWidth);

        }
    }
}
