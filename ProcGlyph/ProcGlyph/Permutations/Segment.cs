using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Permutations
{
    public class Segment : Permutation
    {
        public List<GridPoint[]> Permute(List<GridPoint[]> origPointList)
        {
            List<GridPoint[]> pointsList = new List<GridPoint[]>();

            foreach (GridPoint[] points in origPointList)
            {
                GridPoint lastPoint = null;
                foreach (GridPoint point in points)
                {
                    if (lastPoint != null)
                    {
                        pointsList.Add(new GridPoint[] { lastPoint, point });
                    }
                    lastPoint = point;
                }
            }

            return pointsList;
        }
    }
}
