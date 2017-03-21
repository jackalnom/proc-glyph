using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Permutations
{
    public class Rotate90 : Permutation
    {
        public List<GridPoint[]> Permute(List<GridPoint[]> origPointList)
        {
            List<GridPoint[]> pointList = new List<GridPoint[]>();

            foreach (GridPoint[] points in origPointList)
            {
                GridPoint[] newPoints = new GridPoint[points.Count()];
                newPoints = points.Select(p => new GridPoint(p.Y, 6-p.X)).ToArray();

                pointList.Add(newPoints);
            }
            return pointList;
        }
    }
}
