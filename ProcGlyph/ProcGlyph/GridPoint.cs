using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph
{
    public class GridPoint
    {
        public GridPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Valid()
        {
            return (X >= 0 && X < 7 && Y >= 0 && Y < 7);
        }

        public int X
        {
            get; private set;
        }

        public int Y
        {
            get; private set;
        }

        public GridPoint normalize(int weight)
        {
            double mag = Math.Sqrt(X * X + Y * Y);
            return new GridPoint((int)(X * weight / mag), (int)(Y * weight / mag));
        }

        public static GridPoint MidPoint(GridPoint a, GridPoint b)
        {
            return new GridPoint((a.X + b.X) / 2, (a.Y + b.Y) / 2);
        }

        public static GridPoint MidPoint(GridPoint[] points)
        {
            return new GridPoint((int)Math.Floor(points.Average(p => p.X)), (int)Math.Floor(points.Average(p => p.Y)));
        }

        public static int Distance(GridPoint a, GridPoint b)
        {
            return (int)(Math.Sqrt((a.X - b.X)*(a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y)));
        }

        public static GridPoint Direction(GridPoint a, GridPoint b)
        {
            return new GridPoint(a.X - b.X, a.Y - b.Y);
        }
    }
}
