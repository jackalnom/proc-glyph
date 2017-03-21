using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Permutations
{
    public class Merge : Permutation
    {
        public List<GridPoint[]> Permute(List<GridPoint[]> origPointsList)
        {
            if (origPointsList.Count == 1)
            {
                return origPointsList;
            }

            List<GridPoint[]> pointsList = new List<GridPoint[]>();

            for (int j = 0; (j + 1) < origPointsList.Count; j += 2)
            {
                GridPoint[] first = origPointsList[j];
                GridPoint[] second = origPointsList[j + 1];

                List<GridPoint> mergedPoints = new List<GridPoint>();
                mergedPoints.AddRange(first);
                mergedPoints.AddRange(second);

                pointsList.Add(mergedPoints.ToArray());
            }
                
            if (origPointsList.Count() % 2 == 1)
            {
                pointsList.Add(origPointsList[origPointsList.Count - 1]);
            }

            return pointsList;
        }
    }
}
