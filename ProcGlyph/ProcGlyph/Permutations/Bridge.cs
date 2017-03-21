using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Permutations
{
    public class Bridge : Permutation
    {
        public List<GridPoint[]> Permute(List<GridPoint[]> origPointsList)
        {
            if (origPointsList.Count == 1)
            {
                return origPointsList;
            }

            List<GridPoint[]> pointsList = new List<GridPoint[]>();
            GridPoint[] centerPoints = new GridPoint[origPointsList.Count];
            int i = 0;

            foreach (GridPoint[] points in origPointsList)
            {
                centerPoints[i++] = GridPoint.MidPoint(points);
                pointsList.Add(points);
            }

            pointsList.Add(centerPoints);

            return pointsList;
        }
    }
}
