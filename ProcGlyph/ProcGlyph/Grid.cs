using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cairo;
using ProcGlyph;

namespace ProcGlyph
{
    public class Grid : Drawable
    {
        private float width, height;
        private Drawable[] gridLines;
        private static Color color = new Color(0.8f, 0, 0, 0.1f);

        public double LetterLineWidth { get; private set; }
        public double CanvasWidth { get; private set; }
        public double CanvasHeight { get; private set; }
        public bool DrawGridLines { get; set; }
        public GridPoint Offset { get; private set; }
        public GridPoint Gravity { get; private set; }

        private double[] translateX = new double[] { 1, .5, 1, 2, 1, .5, 1 };
        private double[] gridToPointX = new double[7];

        private double[] translateY = new double[] { 1, 1, 1, 1, 1, 1, 1 };
        private double[] gridToPointY = new double[7];

        public Grid(float width, float height, GridPoint offset, GridPoint gravity)
        {
            Offset = offset;
            Gravity = gravity;
            LetterLineWidth = 5;
            DrawGridLines = true;

            this.width = width/5;
            this.height = height/5;

            gridLines = new Drawable[14];

            Random random = new Random();

            for (int x = 0; x < 7; x++)
            {
                translateX[x] = .5 + random.NextDouble();
            }

            for (int y = 0; y < 7; y++)
            {
                translateY[y] = .5 + random.NextDouble();
            }


            double currentX = 0;
            for (int x = 0; x < 7; x++)
            {
                gridToPointX[x] = currentX + 1 * this.width;
                currentX = gridToPointX[x];
            }

            double currentY = 0;
            for (int y = 0; y < 7; y++)
            {
                gridToPointY[y] = currentY + 1 * this.height;
                currentY = gridToPointY[y];
            }
            CanvasWidth = currentX;
            CanvasHeight = currentY;

            for (int x = 0; x < 7; x++)
            {
                gridLines[x] = gridLine(new GridPoint(x, 0), new GridPoint(x, 6));
            }

            for (int y = 0; y < 7; y++)
            {
                gridLines[7 + y] = gridLine(new GridPoint(0, y), new GridPoint(6, y));
            }


        }


        private GridLine gridLine(GridPoint a, GridPoint b)
        {
            return new GridLine(TranslateGridPoint(a), TranslateGridPoint(b));
        }

        public bool InBounds(PointD vec)
        {
            return (vec.X >= 0 && vec.Y >= 0 && vec.X < CanvasWidth && vec.Y < CanvasHeight);
        }

        public PointD TranslateGridPoint(GridPoint gp)
        {
            return new PointD(gridToPointX[gp.X], gridToPointY[gp.Y]);
        }

        public void Draw(Cairo.Context gr)
        {
            if (DrawGridLines)
            {
                foreach (Drawable line in gridLines)
                {
                    line.Draw(gr);
                }
            }
        }
    }
}
