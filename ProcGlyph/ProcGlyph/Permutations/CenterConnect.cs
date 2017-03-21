using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Permutations
{
    public class CenterConnect : Permutation
    {
        public List<GridPoint[]> Permute(List<GridPoint[]> origPointsList)
        {
            List<GridPoint[]> pointsList = new List<GridPoint[]>();

            foreach (GridPoint[] points in origPointsList)
            {
                GridPoint center = GridPoint.MidPoint(points);

                foreach (GridPoint point in points)
                {
                    pointsList.Add(new GridPoint[] { point, center });
                }
            }

            return pointsList;
        }
    }
}
