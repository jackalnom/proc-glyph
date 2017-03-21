using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Shapes
{
    public class Triad : Shape
    {
        private Random random = new Random();
        private List<GridPoint[]> points = new List<GridPoint[]>()
                {
                    { new GridPoint[] {new GridPoint(1, 5), new GridPoint(1, 1), new GridPoint(5, 1) } },
                    { new GridPoint[] {new GridPoint(1, 1), new GridPoint(1, 5), new GridPoint(5, 5) } },
                    { new GridPoint[] {new GridPoint(5, 1), new GridPoint(5, 5), new GridPoint(1, 5) } },
                    { new GridPoint[] {new GridPoint(1, 1), new GridPoint(5, 1), new GridPoint(5, 5) } },
                    { new GridPoint[] {new GridPoint(1, 5), new GridPoint(3, 1), new GridPoint(5, 5) } },
                    { new GridPoint[] {new GridPoint(5, 5), new GridPoint(1, 3), new GridPoint(5, 1) } },
                    { new GridPoint[] {new GridPoint(5, 1), new GridPoint(3, 5), new GridPoint(1, 1) } },
                    { new GridPoint[] {new GridPoint(1, 1), new GridPoint(5, 3), new GridPoint(1, 5) } },
                };

        public List<GridPoint[]> All()
        {
            return points;
        }
    }
}
