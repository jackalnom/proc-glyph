using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Permutations
{
    public class ShrinkAndMirror : Permutation
    {
        /*
            new Curved().Draw(grid, new GridPoint[] { new GridPoint(1, 1), new GridPoint(5, 3), new GridPoint(1, 5), } )

            new JoinedArc().Draw(grid, new GridPoint[] { new GridPoint(1, 1),  new GridPoint(5, 2), new GridPoint(1, 3), }),
            new JoinedArc().Draw(grid, new GridPoint[] { new GridPoint(1, 3),  new GridPoint(5, 4), new GridPoint(1, 5), })
        */
        public List<GridPoint[]> Permute(List<GridPoint[]> origPointList)
        {
            List<GridPoint[]> pointList = new List<GridPoint[]>();

            foreach (GridPoint[] points in origPointList)
            {
                GridPoint[] shrunkPoints = points.Select(p => new GridPoint(p.X, (p.Y - 1)/2+1)).ToArray();
                GridPoint[] mirrorPoints = shrunkPoints.Select(p => new GridPoint(p.X, 6 - p.Y)).ToArray();

                pointList.Add(shrunkPoints);
                pointList.Add(mirrorPoints);
            }

            return pointList;
        }
    }
}
